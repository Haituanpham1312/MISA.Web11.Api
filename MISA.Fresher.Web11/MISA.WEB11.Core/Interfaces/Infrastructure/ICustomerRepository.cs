using MISA.WEB11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface xử dụng cho khách hàng
    /// Createdby: PTHAI 
    /// </summary>
   public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetCustomers();
        int Insert(Customer customer);
        int Update(Customer customer, Guid customerId);
        int Delete(Guid customerId);
        /// <summary>
        /// Kiểm tra mã khách hàng có hay chưa
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>True-đã tồn tại; False- k bị trùng</returns>
        bool CheckCustomercodeDuplicate(string customerCode);
    }
}
