using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmployeeBL:IEmployeeBL
    {
        private readonly IEmployeeRL EmployeeRL;

        public EmployeeBL(IEmployeeRL EmployeeRL)
        {
            this.EmployeeRL = EmployeeRL;
        }

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            try
            {
                return this.EmployeeRL.AddEmployee(employee);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
