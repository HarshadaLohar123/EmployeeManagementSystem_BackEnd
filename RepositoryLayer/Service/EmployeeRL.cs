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
    public class EmployeeRL:IEmployeeRL
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

        public bool DeleteEmployee(int EmployeeId)
        {
            Connection = new SqlConnection(this.configuration["ConnectionStrings:EmployeeManagement"]);
            try
            {

                using (Connection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteEmployee", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    Connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    Connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateEmployeeModel UpdateEmployee(UpdateEmployeeModel updateEmployee)
        {
            Connection = new SqlConnection(this.configuration["ConnectionStrings:EmployeeManagement"]);
            try
            {

                using (Connection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", updateEmployee.EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", updateEmployee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", updateEmployee.LastName);
                    cmd.Parameters.AddWithValue("@Email", updateEmployee.Email);
                    cmd.Parameters.AddWithValue("@Password", updateEmployee.Password);
                    cmd.Parameters.AddWithValue("@EmpAddress", updateEmployee.EmpAddress);
                    cmd.Parameters.AddWithValue("@Gender", updateEmployee.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", updateEmployee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Position", updateEmployee.Position);
                    cmd.Parameters.AddWithValue("@Salary", updateEmployee.Salary);
                    cmd.Parameters.AddWithValue("@PhoneNumber", updateEmployee.PhoneNumber);

                    Connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    Connection.Close();
                    if (result != 0)
                    {
                        return updateEmployee;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

