using KartalApiNew.Entities;
using KartalApiNew.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KartalApiNew.DataAccess
{
    public class UserManager
    {
        public static User GetUserByEmailPassword(string email, string password)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = $"SELECT * FROM [User] WHERE Email=@Email AND Password=@Password AND IsApproved=1";
            User user = new User();

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    user.Birthdate = dr["Birthdate"] != DBNull.Value ? Convert.ToDateTime(dr["Birthdate"].ToString()) : DateTime.MinValue;
                    user.Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : default;
                    user.Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"].ToString()) : default;
                    user.IsApproved = dr["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dr["IsApproved"]) : default;
                    user.Lastname = dr["Lastname"] != DBNull.Value ? dr["Lastname"].ToString() : default;
                    user.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : default;
                    user.Password = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : default;
                    user.PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : default;
                    user.RegistrationDate = dr["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationDate"].ToString()) : DateTime.MinValue;

                }
            }
            connection.Close();
            return user;
        }

        public static User GetUserById(int id)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = $"SELECT * FROM [User] WHERE Id=@Id";
            User user = new User();

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    user.Birthdate = dr["Birthdate"] != DBNull.Value ? Convert.ToDateTime(dr["Birthdate"].ToString()) : DateTime.MinValue;
                    user.Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : default;
                    user.Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"].ToString()) : default;
                    user.IsApproved = dr["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dr["IsApproved"]) : default;
                    user.Lastname = dr["Lastname"] != DBNull.Value ? dr["Lastname"].ToString() : default;
                    user.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : default;
                    user.Password = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : default;
                    user.PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : default;
                    user.RegistrationDate = dr["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationDate"].ToString()) : DateTime.MinValue;

                }
            }
            connection.Close();
            return user;
        }

        public static bool IsExistUserByEmail(string email)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var userCount = 0;

            var commandString = "SELECT COUNT(*) FROM [User] WHERE Email=@Email";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                userCount = Convert.ToInt32(command.ExecuteScalar());

            }

            connection.Close();

            if (userCount == 0)
                return false;
            else
                return true;

        }

        public static void InsertUser(CreateUserModel model)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();


            var commandString = @"INSERT INTO [User] 
(Name,
Lastname,
Email,
PhoneNumber,
IsApproved,
Birthdate,
RegistrationDate,
Password)
VALUES(
@Name,
@Lastname,
@Email,
@PhoneNumber,
@IsApproved,
@BirthDate,
GETDATE(),
@Password
)";


            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Lastname", model.Lastname);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@BirthDate", model.Birthdate);
                command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("@IsApproved", false);
                command.Parameters.AddWithValue("@Password", model.Password);

                command.ExecuteNonQuery();
            }
            connection.Close();

        }

        public static void UpdatePassword(string email, string password)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET Password=@Password WHERE Email=@Email";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static void UpdateUser(UpdateUserRequestModel model)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET Name=@Name,
Lastname=@Lastname,
PhoneNumber=@PhoneNumber
WHERE Id=@Id"
;

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Lastname", model.Lastname);
                command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("@Id", model.UserId);



                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        public static void ActivateUser(string email)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET IsApproved=1 WHERE Email=@Email"
;
            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        public static void DeactivateUser(int id)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET IsApproved=0 WHERE Id=@Id"
;
            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        public static void ChangeEmail(string oldMail,string newMail)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET Email=@NewEmail WHERE Email=@OldEmail"
;
            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@OldEmail", oldMail);
                command.Parameters.AddWithValue("@NewEmail", newMail);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static void ChangePassword(string oldPassword,int userId ,string newPassword)
        {
            var connection = new SqlConnection(GlobalSettings.ConnectionString);
            connection.Open();

            var commandString = @"UPDATE [User] SET Password=@NewPassword WHERE Id=@Id AND Password=@Password"
;
            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", userId);
                command.Parameters.AddWithValue("@Password", oldPassword);
                command.Parameters.AddWithValue("@NewPassword", newPassword);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

    }
}