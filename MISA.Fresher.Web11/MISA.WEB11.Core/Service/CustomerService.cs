using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Exceptions;
using MISA.WEB11.Core.Interfaces.Infrastructure;
using MISA.WEB11.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Service
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        //khai báo thông tin lỗi
        List<string> errorMsgs = new List<string>();
        #endregion

        #region Contructor

        #endregion

        #region Methods
        public int DeleteService(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int InsertService(Customer customer)
        {
            var isValid = true;
            //validate dữ liệu
            //1.Kiểm tra mã có trùng hay không
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                errorMsgs.Add("Mã khách hàng không được phép để trống");
            }
            else
            {
                //Thực hiện kiểm tra trùng mã trong database
                var isDuplicate = _customerRepository.CheckCustomercodeDuplicate(customer.CustomerCode);
                if (isDuplicate == true)
                {
                    errorMsgs.Add("Mã khách hàng không được phép trùng");
                }

            }
            //2.Kiểm tra mã khách hàng k đc để trống

            //3.SĐT k đc để trống
            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                errorMsgs.Add("SĐT không được phép để trống");
            }
            //4.Email phải đúng định dạng
            //5.Ngày sinh phải nhỏ hơn ngày hiện tại

            if (errorMsgs.Count > 0)
            {
                var result = new
                {
                    userMsg = "Dữ liệu không hợp lệ vui lòng kiểm tra lại",
                    data = errorMsgs
                };
                throw new MISAValidateException(result);
            }

            //thêm mới dữ liệu vào database
            if (isValid == true)
            {
                return _customerRepository.Insert(customer);

            }
            else
            {
                //trả về thông tin lỗi nghiệp vụ
                throw new MISAValidateException(null);

            }
        }

        public int UpdateService(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
