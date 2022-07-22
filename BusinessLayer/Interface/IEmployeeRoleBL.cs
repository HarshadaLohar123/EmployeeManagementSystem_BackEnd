using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IEmployeeRoleBL
    {
        //Employee Login
        public string Login(EmployeeLoginModel employeeLoginModel);
    }
}
