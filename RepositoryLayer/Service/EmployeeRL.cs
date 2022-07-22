using DatabaseLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmployeeRL : IEmployeeRL
    {
        private SqlConnection Connection;
        private readonly IConfiguration configuration;
        public EmployeeRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            this.Connection = new SqlConnection(this.configuration["ConnectionStrings:EmployeeManagement"]);
            try
            {
                SqlCommand command = new SqlCommand("AddEmployee", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Password", employee.Password);
                command.Parameters.AddWithValue("@EmpAddress", employee.EmpAddress);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);

                Connection.Open();
                int result = command.ExecuteNonQuery();
                Connection.Close();
                if (result != 0)
                {
                    return employee;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
