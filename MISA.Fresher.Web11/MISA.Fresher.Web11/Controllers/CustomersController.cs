using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("search")]
        public int? GetNumber(string q, int? number)
        {
            return number;
        }

        [HttpPost()]
        public object PostCustomer([FromHeader] string accessToken, List<Customer> customers)
        {
            return new
            {
                Token = accessToken,
                Data = customers
            };
        }

        [HttpPut()]
        public object UpdateEmployee(string? employeeCode, string? fullName)
        {
            return "";
        }

        [HttpDelete()]
        public void DeleteEmployee(string? employeeCode)
        {

        }


    }
}