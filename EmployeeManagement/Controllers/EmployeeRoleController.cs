using BusinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeRoleController : Controller
    {
        private readonly IEmployeeRoleBL employeeBL;
        public EmployeeRoleController(IEmployeeRoleBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        [HttpPost("EmployeeLogin")]
        public IActionResult EmployeeLogin(EmployeeLoginModel employeeLogin)
        {
            try
            {
                var result = this.employeeBL.Login(employeeLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Employee Login Successful", Token = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Sorry!!! Login Failed", Token = result });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = "Sorry!!! Login Failed,Please enter valid Email and password" });

            }
        }
    }
}
