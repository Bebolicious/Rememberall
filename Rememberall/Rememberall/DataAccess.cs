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
