using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorageService _storageService;
        private readonly string _containerName;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration,
            IAzureStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
            _storageService = storageService;
        }

        public async Task<List<TeacherViewModel>> GetTeacherList()
        {
            return _mapper.Map<List<TeacherViewModel>>(await _unitOfWork.TeacherRepositories.GetTeacherList());
        }

        public async Task<TeacherViewModel?> GetTeacherById(int id)
        {
            var teacher = await _unitOfWork.TeacherRepositories.GetByIdNoTracking(id);
            if (teacher != null)
            {
                var teach = _mapper.Map<TeacherViewModel>(teacher);
                return teach;
            }
            return null;
        }

        public async Task<bool> IsExistByEmail(string email)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByEmail(email);
            if (acc != null) return true;
            return false;
        }

        public async Task Create(CreateNewTeacher newTea)
        {
            if (newTea == null)
                throw new ArgumentNullException(nameof(newTea), "The new teacher info cannot be null.");

            var tea = new Teacher();
            _mapper.Map(newTea, tea);
            tea.Account.RoleId = await _unitOfWork.RoleRepositories.GetRoleIdByRoleName(CEGConstants.ACCOUNT_ROLE_TEACHER);

            // Save to the database
            try
            {
                _unitOfWork.TeacherRepositories.Create(tea);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating teacher account.", ex);
            }
            /*if (certImage != null && certImage.Length > 0)
            {
                string imageUrl = await _storageService.UploadToBlobAsync(certImage, "certificate/");
                acc.Certificate = imageUrl;
            }
            else
            {
                acc.Certificate = "";
            }*/
        }

        public void Update(TeacherViewModel teacher, UpdateTeacher teacherNewInfo)
        {
            var tea = _mapper.Map<Teacher>(teacher);
            if (teacherNewInfo != null)
            {
                tea.TeacherId = _unitOfWork.TeacherRepositories.GetIdByAccountId(tea.Account.AccountId).Result;
                tea.AccountId = tea.Account.AccountId;
                tea.Account.Fullname = teacherNewInfo.Account.Fullname;
                tea.Account.Gender = teacherNewInfo.Account.Gender;
                tea.Phone = teacherNewInfo.Phone;
                tea.Phone = teacherNewInfo.Address;
            }
            _unitOfWork.TeacherRepositories.Update(tea);
            _unitOfWork.Save();
        }

        public async Task<List<GetTeacherNameOption>?> GetTeacherNameOptionList()
        {
            return _mapper.Map<List<GetTeacherNameOption>>(await _unitOfWork.TeacherRepositories.GetTeacherNameOptionList());
        }

        public async Task<bool> IsExistByFullname(string fullname)
        {
            return await _unitOfWork.TeacherRepositories.GetByFullname(fullname) != null;
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _unitOfWork.TeacherRepositories.GetByIdNoTracking(id) != null;
        }

        public async Task<TeacherViewModel?> GetByAccountId(int id)
        {
            var teacher = await _unitOfWork.TeacherRepositories.GetByAccountIdNoTracking(id);
            return teacher != null ? _mapper.Map<TeacherViewModel>(teacher) : null;
        }
        public async Task<List<GetStudentActivity>> GetStudentActivityListByScheduleId(int schId)
        {
            // Fetch the student activities for the schedule and map to the view model
            var stuActList = _mapper.Map<List<GetStudentActivity>>(
                await _unitOfWork.AttendanceRepositories.GetListByScheduleIdNoTracking(schId)
            );

            // Fetch the homework IDs associated with the schedule
            List<int> homIds = await _unitOfWork.HomeworkRepositories.GetIdListByScheduleId(schId);

            // Fetch the student progress records for the relevant homework IDs and map to the view model
            var stuProList = _mapper.Map<List<StudentProgressViewModel>>(
                await _unitOfWork.StudentProgressRepositories.GetListByMultipleHomeworkId(homIds.ToArray())
            );

            // Iterate through each student activity and enrich it with additional data
            foreach (var stuAct in stuActList)
            {
                // Set the total homework count
                stuAct.HomeworkAmount = homIds.Count;

                // Find the matching student progress for the current student
                var matchingStuPro = stuProList.FirstOrDefault(stuPro => stuPro.StudentId == stuAct.StudentId);
                if (matchingStuPro != null)
                {
                    stuAct.StudentProgress = matchingStuPro;

                    var stuHomList = matchingStuPro.StudentHomeworks.Where(stuHom => homIds.Contains(stuHom.HomeworkId)).ToList();

                    // Calculate the current homework progress for the student
                    stuAct.HomeworkCurrentProgress = stuHomList
                        .Count(stuHom => stuHom.Status == CEGConstants.STUDENT_HOMEWORK_STATUS_SUBMITTED);

                    // Assign homework numbers if there are any student homeworks
                    for (int i = 0; i < stuHomList.Count; i++)
                    {
                        stuHomList[i].Homework.HomeworkNumber = i + 1;
                    }
                    matchingStuPro.StudentHomeworks = stuHomList;
                }
            }
            return stuActList;
        }
        public async Task UploadToBlobAsync(string teacherName, IFormFile certificate, string imageType)
        {
            var tea = await _unitOfWork.TeacherRepositories.GetByFullname(teacherName) ??
                throw new ArgumentNullException(teacherName, "Teacher goes by fullname not found.");

            // Injected BlobServiceClient
            var blobContainerClient = _storageService.GetBlobContainerClient();
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            // Generate a unique file name
            string fileName = Guid.NewGuid() + "-" + Path.GetExtension(certificate.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Upload the file to Blob Storage
            using (var stream = certificate.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            if(imageType.Equals(CEGConstants.TEACHER_IMAGE_CERTIFICATE_TYPE))
                tea.Certificate = blobClient.Uri.ToString();

            // Save to the database
            try
            {
                _unitOfWork.TeacherRepositories.Update(tea);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while saving teacher certificate url link.", ex);
            }
        }
    }
}
