using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;
using Dapper;
using MySqlConnector;
using System.Data;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetCustomer()
        {
            try
            {
                //kết nối với database
                //1.Khai báo thông tin database
                var connectionString = "Server=47.241.69.179;" +
                    "Port=3306;" +
                    "Database=MISA.WEB11.NMDUC;" +
                    "User Id=dev;" +
                    "Password=manhmisa";
                //2.Khởi tạo kết nối với database(dùng MariaDB)
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);
                //3.Thực hiện lấy dữ liệu trong database
                var sqlCommand = "SELECT * FROM Customer";
                var customers = sqlConnection.Query<object>(sqlCommand);
                //4.Trả dữ liệu về cho client
                //-nếu có dữ liệu trả về mã 200 kèm theo dữ liệu
                //-nếu không có dữ liệu thì trả về 204
                //-nếu có lỗi thì trả về mã 500 kèm status
                return Ok(customers);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa để được trợ giúp",
                    data = ""
                };
                return StatusCode(500,result);
            }
           
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(Guid customerId)
        {
            try 
            {
                //kết nối với database
                //1.Khai báo thông tin database
                var connectionString = "Server=47.241.69.179;" +
                    "Port=3306;" +
                    "Database=MISA.WEB11.NMDUC;" +
                    "User Id=dev;" +
                    "Password=manhmisa";
                //2.Khởi tạo kết nối với database(dùng MariaDB)
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);
                //3.Thực hiện lấy dữ liệu trong database
                var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                var customer = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);
                //4.Trả dữ liệu về cho client
                //-nếu có dữ liệu trả về mã 200 kèm theo dữ liệu
                //-nếu không có dữ liệu thì trả về 204
                //-nếu có lỗi thì trả về mã 500 kèm status
                return Ok(customer);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }   
            
        }


        [HttpPost()]
        public object PostCustomer(Customer customer)
        {
            try
            {
                //khai báo thông tin lỗi
                List<string> errorMsgs = new List<string>();
                //1.Khai báo thông tin database
                var connectionString = "Server=47.241.69.179;" +
                    "Port=3306;" +
                    "Database=MISA.WEB11.NMDUC;" +
                    "User Id=dev;" +
                    "Password=manhmisa";
                //2.Khởi tạo kết nối với database(dùng MariaDB)
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);
                //validate dữ liệu
                //1.Kiểm tra mã có trùng hay không
                var customerCode = customer.CustomerCode;
                if(string.IsNullOrEmpty(customerCode))
                {
                    errorMsgs.Add("Mã khách hàng không được phép để trống");
                }
                else
                {
                    var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                    DynamicParameters paramCheck = new DynamicParameters();
                    paramCheck.Add("@CustomerCode", customerCode);
                    var customerDuplicate = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, param: paramCheck);
                    if(customerDuplicate != null)
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
                    return StatusCode(400, result);
                }

                //sinh Id mới cho đối tượng:
                customer.CustomerId = Guid.NewGuid();
                //kết nối với database
                
                //3.Thực hiện lấy dữ liệu trong database
                var sqlCommand = "" +
                    "INSERT Customer (" +
                        "CustomerId," +
                        "CustomerCode," +
                        "FullName," +
                        "PhoneNumber) " +
                    "VALUES(" +
                        "@CustomerId," +
                        "@CustomerCode," +
                        "@FullName," +
                        "@PhoneNumber); ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.CustomerId);
                parameters.Add("@CustomerCode", customer.CustomerCode);
                parameters.Add("@FullName", customer.FullName);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                //4.Trả dữ liệu về cho client
                //-nếu có dữ liệu trả về mã 200 kèm theo dữ liệu
                //-nếu không có dữ liệu thì trả về 204
                //-nếu có lỗi thì trả về mã 500 kèm status
                if(res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);

                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ Misa để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }
    }
}