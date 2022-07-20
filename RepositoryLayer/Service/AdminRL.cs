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
    public class AdminRL : IAdminRL
    {
        private SqlConnection Connection;
        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public AdminLoginModel Adminlogin(AdminResponse adminResponse)
        {
            this.Connection = new SqlConnection(this.Configuration["ConnectionStrings:EmployeeManagement"]);

            try
            {
                SqlCommand command = new SqlCommand("LoginAdmin", this.Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Email", adminResponse.Email);
                command.Parameters.AddWithValue("@Password", adminResponse.Password);
                this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                AdminLoginModel admin = new AdminLoginModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminId = Convert.ToInt32(reader["AdminId"]);
                        admin.FullName = Convert.ToString(reader["FullName"]);
                        admin.Email = Convert.ToString(reader["Email"]);
                        admin.MobileNumber = Convert.ToString(reader["MobileNumber"]);
                    }

                    this.Connection.Close();
                    return admin;
                }
                else
                {
                    throw new Exception("Email Or Password Is Wrong");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
