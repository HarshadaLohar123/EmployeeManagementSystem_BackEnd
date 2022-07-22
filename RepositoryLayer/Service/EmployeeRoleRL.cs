using DatabaseLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmployeeRoleRL:IEmployeeRoleRL
    {
        private SqlConnection Connection;
        private readonly IConfiguration configuration;
        public EmployeeRoleRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        //Employee Login
        public string Login(EmployeeLoginModel employeeLoginModel)
        {
            this.Connection = new SqlConnection(this.configuration["ConnectionStrings:EmployeeManagement"]);
            try
            {

                SqlCommand command = new SqlCommand("EmployeeLogin", this.Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", employeeLoginModel.Email);
                command.Parameters.AddWithValue("@Password", employeeLoginModel.Password);

                this.Connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    int EmployeeId = 0;
                    EmployeeLoginModel employee = new EmployeeLoginModel();
                    while (dataReader.Read())
                    {
                        employee.Email = Convert.ToString(dataReader["Email"]);
                        employee.Password = Convert.ToString(dataReader["Password"]);
                        EmployeeId = Convert.ToInt32(dataReader["EmployeeId"]);
                    }

                    this.Connection.Close();
                    var Token = this.GenerateJWTToken(employee.Email, EmployeeId);
                    return Token;
                }
                else
                {
                    this.Connection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
        }

      

        private string GenerateJWTToken(string Email, int EmployeeId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("This is My Key To Generate Token");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  
                    new Claim("Email", Email),
                    new Claim("EmployeeId", EmployeeId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
