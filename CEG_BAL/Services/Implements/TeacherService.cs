using AutoMapper;
using Azure.Storage.Blobs;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
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
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
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

        public async Task<bool> IsTeacherExistByEmail(string email)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByEmail(email);
            if (acc != null) return true;
            return false;
        }

        public async void Create(TeacherViewModel teacher, CreateNewTeacher newTeach, IFormFile certImage)
        {
            var acc = _mapper.Map<Teacher>(teacher);
            acc.Account.CreatedDate = DateTime.Now;
            acc.Account.Status = "Active";
            acc.Account.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName("Teacher").Result;
            if (newTeach != null)
            {
                acc.Account.Fullname = newTeach.Account.Fullname;
                acc.Account.Username = newTeach.Account.Username;
                acc.Account.Gender = newTeach.Account.Gender;
                acc.Account.Password = newTeach.Account.Password;
                acc.Email = newTeach.Email;
                acc.Phone = newTeach.Phone;
                acc.Address = newTeach.Address;
            }

            if (certImage != null && certImage.Length > 0)
            {
                string imageUrl = await UploadToBlobAsync(certImage);
                acc.Certificate = imageUrl;
            }
            else
            {
                acc.Certificate = "";
            }

            _unitOfWork.TeacherRepositories.Create(acc);
            _unitOfWork.Save();
        }

        public void Update(TeacherViewModel teacher)
        {
            var acc = _mapper.Map<Teacher>(teacher);
            _unitOfWork.TeacherRepositories.Update(acc);
            _unitOfWork.Save();
        }

        public async Task<List<string>> GetTeacherNameList()
        {
            return await _unitOfWork.TeacherRepositories.GetTeacherNameList();
        }

        public async Task<bool> IsTeacherExistByFullname(string fullname)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByFullname(fullname);
            if (acc != null) return true;
            return false;
        }

        public async Task<TeacherViewModel?> GetTeacherByAccountId(int id)
        {
            var teacher = await _unitOfWork.TeacherRepositories.GetByAccountIdNoTracking(id);
            if (teacher != null)
            {
                var teach = _mapper.Map<TeacherViewModel>(teacher);
                return teach;
            }
            return null;
        }

        public async Task<string> UploadToBlobAsync(IFormFile file)
        {
            // Injected BlobServiceClient
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("certificate");
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // Generate a unique file name
            string fileName = Guid.NewGuid() + "-" + Path.GetExtension(file.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Upload the file to Blob Storage
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString(); // Return the file URL
        }
    }
}
