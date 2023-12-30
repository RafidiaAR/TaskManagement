using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using TaskManagement.API.Domain.User.Entities;

namespace TaskManagement.API.Domain.User.Repositories
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionString:DefaultConnection");
        }

        public bool ValidateLogin(string password, string hashedPassword, string salt) 
        {
            bool verifyPass = VerifyPassword(password, Convert.FromBase64String(salt), hashedPassword.Trim());

            return verifyPass;
        }
        public async Task<UserEntity> GetByUsername(string username) 
        {
            var user = new UserEntity();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("User_GetByUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                user.UserID = new Guid(reader["UserId"].ToString());
                                user.Username = reader["Username"].ToString();
                                user.FullName = reader["Fullname"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.Salt = reader["Salt"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return user;
        }
        public void CreateUser(UserEntity data) 
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    byte[] salt = GenerateSalt();
                    string password = HashPassword(data.Password, salt);
                    string convertedSalt = Convert.ToBase64String(salt);

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("User_Create", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", data.Username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@FullName", data.FullName);
                        command.Parameters.AddWithValue("@Email", data.Email);
                        command.Parameters.AddWithValue("@Division", data.Division);
                        command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
                        command.Parameters.AddWithValue("@Salt", convertedSalt);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
                finally {
                    connection.Close();
                }
            }

        }

        static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        static string HashPassword(string password, byte[] salt)
        {
            int iterations = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        static bool VerifyPassword(string enteredPassword, byte[] salt, string storedHash)
        {
            string enteredPasswordHash = HashPassword(enteredPassword, salt);
            return enteredPasswordHash == storedHash;
        }
    }
}
