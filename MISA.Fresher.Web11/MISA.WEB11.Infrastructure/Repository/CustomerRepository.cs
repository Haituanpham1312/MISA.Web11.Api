using Dapper;
using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public int Delete(Guid customerId)
        {
            //kết nối với database
            //1.Khai báo thông tin database:
            var connectionString = "Server=47.241.69.179;" +
                 "Port=3306;" +
                 "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                 "User Id=dev;" +
                 "Password=manhmisa";
            //2.Khởi tạo kết nối với database(dùng MariaDB)
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            //3.Thực hiện xóa dữ liệu trong database
            var sqlCommand = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res != null ? 1 : 0;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //kết nối với database
            //1.Khai báo thông tin database
            var connectionString = "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev;" +
                "Password=manhmisa";
            //2.Khởi tạo kết nối với database(dùng MariaDB)
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            //3.Thực hiện lấy dữ liệu trong database
            var sqlCommand = "SELECT * FROM Customer";
            var customers = sqlConnection.Query<Customer>(sqlCommand);
           return customers;
        }

        public int Insert(Customer customer)
        {
            //sinh Id mới cho đối tượng:
            customer.CustomerId = Guid.NewGuid();
            //khai báo thông tin lỗi
            List<string> errorMsgs = new List<string>();
            //1.Khai báo thông tin database
            var connectionString = "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev;" +
                "Password=manhmisa";
            //2.Khởi tạo kết nối với database(dùng MariaDB)
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            //3.Thực hiện lấy dữ liệu trong database
            var sqlCommand = "INSERT Customer (CustomerId, CustomerCode, FullName, PhoneNumber) " +
                                 "VALUES (@CustomerId, @CustomerCode, @FullName, @PhoneNumber)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customer.CustomerId);
            parameters.Add("@CustomerCode", customer.CustomerCode);
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@PhoneNumber", customer.PhoneNumber);

            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }

        public int Update(Customer customer, Guid customerId)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev;" +
                "Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Lấy dữ liệu trong db
            var sqlCommand = "UPDATE Customer SET CustomerCode=@CustomerCode, FullName=@FullName, PhoneNumber=@PhoneNumber " +
                             "WHERE CustomerId=@CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@CustomerCode", customer.CustomerCode);
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@PhoneNumber", customer.PhoneNumber);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }

        public bool CheckCustomercodeDuplicate(string customerCode) 
        {
            //1.Khai báo thông tin database
            var connectionString = "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev;" +
                "Password=manhmisa";
            //2.Khởi tạo kết nối với database(dùng MariaDB)
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
            DynamicParameters paramCheck = new DynamicParameters();
            paramCheck.Add("@CustomerCode", customerCode);
            var customerDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: paramCheck);
            if (customerDuplicate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
