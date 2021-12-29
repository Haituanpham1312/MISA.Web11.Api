using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WEB11.Core.Entities;

namespace MISA.WEB11.Core.Interfaces.Service
{
    public interface ICustomerService
    {
        /// <summary>
        /// Xử lý nghiệp vụ thêm mới dữ liệu
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        int InsertService(Customer customer);
        int UpdateService(Customer customer,Guid customerId);
        int DeleteService(Guid customerId);


    }
}
