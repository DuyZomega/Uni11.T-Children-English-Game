using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
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
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public PaymentService(
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
        public void Create(PaymentViewModel model)
        {
            var pay = _mapper.Map<Payment>(model);
            _unitOfWork.PaymentRepositories.Create(pay);
            _unitOfWork.Save();
        }

        public async Task<List<PaymentViewModel>> GetAllPayment()
        {
            return _mapper.Map<List<PaymentViewModel>>(await  _unitOfWork.PaymentRepositories.GetPaymentsList());
        }

        public async Task<PaymentViewModel> GetPaymentById(int id)
        {
            var user = await _unitOfWork.PaymentRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<PaymentViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(PaymentViewModel model)
        {
            var pay = _mapper.Map<Payment>(model);
            _unitOfWork.PaymentRepositories.Update(pay);
            _unitOfWork.Save();
        }
    }
}
