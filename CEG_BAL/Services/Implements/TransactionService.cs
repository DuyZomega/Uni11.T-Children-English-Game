using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Transaction;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<int> Create(TransactionViewModel model, CreateTransaction newTran)
        {
            var trans = _mapper.Map<Transaction>(model);
            if (newTran != null)
            {
                trans.ParentId = await _unitOfWork.ParentRepositories.GetIdByFullname(newTran.ParentFullname);
                trans.VnpayId = newTran.VnpayId;
                trans.TransactionAmount = newTran.TransactionAmount;
                trans.TransactionDate = DateTime.Now;
                trans.TransactionStatus = "Completed";
                trans.TransactionType = newTran.TransactionType;
                trans.ConfirmDate = DateTime.Now;
            }
            _unitOfWork.TransactionRepositories.Create(trans);
            _unitOfWork.Save();

            model.TransactionId = trans.TransactionId;
            return trans.TransactionId;
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
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountId(id);
            if (parentId == 0) return null;
            return _mapper.Map<List<TransactionViewModel>>(await _unitOfWork.TransactionRepositories.GetTransactionByParentId(parentId));
        }

        public async Task<TransactionViewModel?> GetTransactionByVnpayId(string? vnpayId)
        {
            return _mapper.Map<TransactionViewModel>(await
                _unitOfWork.TransactionRepositories.GetTransactionByVnpayId(vnpayId));
        }

        public void Update(TransactionViewModel model)
        {
            var pay = _mapper.Map<Transaction>(model);
            _unitOfWork.TransactionRepositories.Update(pay);
            _unitOfWork.Save();
        }
    }
}
