using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IPaymentService
    {
        void Create(PaymentViewModel model);
        void Update(PaymentViewModel model);
        Task<List<PaymentViewModel>> GetPaymentList();
        Task<PaymentViewModel?> GetPaymentById(int id);
        Task<List<PaymentViewModel>> GetPaymentByParentAccountId(int id);
    }
}
