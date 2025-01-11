using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class VnpayService : IVnpayService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public VnpayService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public HttpContext HttpContext => _contextAccessor.HttpContext;

        public string CreatePaymentUrl(TransactionRequest request)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnpayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ParentReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)request.TransactionAmount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", HttpContext.Connection.RemoteIpAddress.ToString());
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", CEGConstants.TRANSACTION_PAYER_LABEL + CEGConstants.TRANSACTION_USER_PARENT_NAME_LABEL + $"{request.ParentFullname}," +
                CEGConstants.TRANSACTION_AMOUNT_LABEL + $"{request.TransactionAmount}," +
                $"{request.TransactionType}," +
                CEGConstants.TRANSACTION_RECEIVER_STUDENT_NAME_FOR_ENROLLMENT_LABEL + $"{request.StudentFullname}," +
                CEGConstants.TRANSACTION_DESCRIPTION_ENROLLING_CLASS_NAME_LABEL + $"{request.Classname}"
            );
            pay.AddRequestData("vnp_OrderType", request.TransactionType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public TransactionResponse PaymentExecute()
        {
            var pay = new VnpayLibrary();
            var response = pay.GetFullResponseData(HttpContext.Request.Query, _configuration["Vnpay:HashSecret"]);
            return response;
        }
    }
}
