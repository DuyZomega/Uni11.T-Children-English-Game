using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
using CEG_BAL.ViewModels.Teacher.Transaction;
using CEG_BAL.ViewModels.Transaction;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CEG_BAL.Services.Implements
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public TransactionService(
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
        public async Task<int> Create(CreateTransaction newTra)
        {
            if (newTra == null)
                throw new ArgumentNullException(nameof(newTra), "The new transaction info cannot be null.");

            var tra = new Transaction();
            _mapper.Map(newTra, tra);
            var par = await _unitOfWork.AccountRepositories.GetIdByFullname(newTra.ParentFullname);
            tra.AccountId = par != 0 ? 
                par : 
                throw new ArgumentNullException(nameof(newTra.ParentFullname), "The new transaction info contains invalid Parent Fullname.");

            _unitOfWork.TransactionRepositories.Create(tra);
            // Save to the database
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating transaction.", ex);
            }
            return tra.TransactionId;
        }

        public async Task<List<TransactionViewModel>> GetTransactionList()
        {
            return _mapper.Map<List<TransactionViewModel>>(await _unitOfWork.TransactionRepositories.GetListNoTracking());
        }

        public async Task<TransactionViewModel?> GetTransactionById(int id)
        {
            var user = await _unitOfWork.TransactionRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<TransactionViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<TransactionViewModel>> GetTransactionByParentAccountId(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(id);
            if (parentId == 0) return null;
            return _mapper.Map<List<TransactionViewModel>>(await _unitOfWork.TransactionRepositories.GetAllByParentId(parentId));
        }

        public async Task<List<EarningViewModel>> GetAllByTeacherAccountId(int id)
        {
            var teaId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teaId == 0) return new List<EarningViewModel>();
            return _mapper.Map<List<EarningViewModel>>(await _unitOfWork.TransactionRepositories.GetAllByTeacherId(teaId));
        }

        public async Task<TransactionViewModel?> GetByVnpayId(string? vnpayId)
        {
            return _mapper.Map<TransactionViewModel>(await
                _unitOfWork.TransactionRepositories.GetByVnpayId(vnpayId));
        }

        public void Update(TransactionViewModel model)
        {
            var pay = _mapper.Map<Transaction>(model);
            _unitOfWork.TransactionRepositories.Update(pay);
            _unitOfWork.Save();
        }

        public async Task<int> GetTotalAmount()
        {
            return await _unitOfWork.TransactionRepositories.GetTotalAmount();
        }

        public async Task<int> GetTotalAmountByAccountId(int id)
        {
            return await _unitOfWork.TransactionRepositories.GetTotalAmountByAccountId(id);
        }

        public async Task<int> GetSumValue()
        {
            return await _unitOfWork.TransactionRepositories.GetSumValue();
        }
    }
}
