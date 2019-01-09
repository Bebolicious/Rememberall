using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using Rememberall.Domains;

namespace Rememberall
{

    public class DataAccess
    {
        private const string conString = "Server=(localdb)\\mssqllocaldb; Database=Rememberall";

        public void CreateNewUser(string NewUser, string NewPassword)
        {
            var sql = @"
                        INSERT INTO Users (Username, Password)
                        VALUES (@Username, @Password)";


            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Username", NewUser));
                command.Parameters.Add(new SqlParameter("Password", NewPassword));
                command.ExecuteNonQuery();
            }
        }

        public static bool MatchUsername(string Inputtedusername, string Inputtedpass)
        {

            var sql = @"
                         Select Username, Password
                         FROM Users
                    WHERE Username=@Username AND password=@Password";


            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Username", Inputtedusername));
                command.Parameters.Add(new SqlParameter("Password", Inputtedpass));
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();


                var Credentials = new Users();

                while (reader.Read())
                {

                    string Username = reader.GetSqlString(0).Value;
                    string Password = reader.GetSqlString(1).Value;

                    if (Username == Inputtedusername && Password == Inputtedpass)
                    {
                        Credentials.Username = Username;
                        Credentials.Password = Password;
                        return true;


                    }

                }
                return false;

            }
        }


        internal List<Activities> GetUserActivities(int? currentUserId)
        {
            var sql = @"Select Activityname, Date from Acitivities
                        Join ManyActivities on Activities.Id = ManyActivities.AcitivityId
                        join Users on ManyActivities.UserId = Users.Id
                         Where Id = @Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", currentUserId));

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Activities>();

                while (reader.Read())
                {
                    var ap = new Activities
                    {
                        Activityname = reader.GetSqlString(0).Value,
                        Date = reader.GetSqlDateTime(1).Value
                    };
                    list.Add(ap);
                }

                return list;
            }
        }
    }
}
