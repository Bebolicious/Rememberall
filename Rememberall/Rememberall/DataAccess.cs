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
        public int TrueId = 0;

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

        public static Users GetCurrentUserById(int? Id)
        {

            var sql = @"SELECT Username
                        FROM Users 
                        WHERE Id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", Id));

                SqlDataReader reader = command.ExecuteReader();

               

                while (reader.Read())
                {
                    var UC = new Users
                    {
                        Username = reader.GetSqlString(0).Value,

                    };
                    return UC;
                }
                return null;
            }
        }

        public static int? SetCurrentUser(string Username)
        {
            var sql = @"SELECT Id
                        FROM Users
                        WHERE Username=@Username";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Username", Username));


                SqlDataReader reader = command.ExecuteReader();

                
                while (reader.Read())
                {
                    var Currentuser = new Users
                    {

                        Id = reader.GetSqlInt32(0).Value,

                    };
                    return Currentuser.Id;
                }
                return null;
                
            }
    

        }

        internal List<Activities> GetUserActivities(int? currentUserId)
        {
            var sql = @"Select ActivityId, Activityname, ActivityDate from Activities
                        Join ManyActivities on Activities.Id = ManyActivities.ActivityId
                        join Users on ManyActivities.UserId = Users.Id
                         Where UserId = @Id";

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
                        Id = reader.GetSqlInt32(0).Value,
                        Activityname = reader.GetSqlString(1).Value,
                        Date = reader.GetSqlDateTime(2).Value
                    };
                    list.Add(ap);
                }

                return list;
            }
        }

        internal int AddUserActivity(string newActivity)
        {


            var sql = @"INSERT Into Activities(Activityname) OUTPUT INSERTED.ID VALUES (@Activityname)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Activityname", newActivity));

                int activityId = (int)command.ExecuteScalar();
                return activityId;
            }
        }

        internal static void SetAlarmDate(DateTime alarmdate, string alarmname, string alarmtime)
        {
            var sql = @"INSERT INTO Alarms(DateId, Alarmname, Alarmtime) VALUES (@DateId, @Alarmname, @Alarmtime)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("DateId", alarmdate));
                command.Parameters.Add(new SqlParameter("Alarmname", alarmname));
                command.Parameters.Add(new SqlParameter("Alarmtime", alarmtime));


                command.ExecuteNonQuery();

            }


        }

        internal void AddManyActivities(int acktivity, DateTime newDateTime, int? currentUserId)
        {
            var sql = @"INSERT Into ManyActivities(ActivityId, ActivityDate, UserId) VALUES (@ActivityId, @ActivityDate, @UserId)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("ActivityId", acktivity));
                command.Parameters.Add(new SqlParameter("ActivityDate", newDateTime));
                command.Parameters.Add(new SqlParameter("UserId", currentUserId));

                command.ExecuteNonQuery();

            }
        }

        //internal List<Activities> GetUserAlarms(int UserId)
        //{
            
        //}

        internal void DeleteActivity(Activities deleteActivity)
        {
            var sql = @"DELETE FROM Activities WHERE Id = @Id";

            //ActivityId foreign key references Activities(Id) on delete cascade
            

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", deleteActivity.Id));

                command.ExecuteNonQuery();
            }
        }
    
        internal Activities GetUserActivitiesById(int activityId)
        {
            var sql = @"SELECT ActivityId, Activityname, ActivityDate
                        FROM Activities
                        Join ManyActivities on Activities.Id = ManyActivities.ActivityId
                        WHERE ActivityId=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", activityId));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var activity = new Activities
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Activityname = reader.GetSqlString(1).Value,
                        Date = reader.GetSqlDateTime(2).Value,

                    };
                    return activity;
                }
                return null;
            }
        }

        internal void UpdateActivityDate(Activities activity)
        {
            var sql = @"Update ManyActivities
                        SET ActivityDate = @ActivityDate
                            Where ActivityId = @Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("ActivityDate", activity.Date));
                command.Parameters.Add(new SqlParameter("Id", activity.Id));


                command.ExecuteNonQuery();
            }
        }

        internal void UpdateActivityName(Activities activity)
        {
            var sql = @"UPDATE Activities
                        SET Activityname = @Activityname 
                        WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Activityname", activity.Activityname));
                command.Parameters.Add(new SqlParameter("Id", activity.Id));


                command.ExecuteNonQuery();
            }
        }

        //internal void UpdateActivity(Activities activity)
        //{
        //    var sql = @"UPDATE ManyActivities
        //                SET  ActivityDate = @Date
        //                WHERE ActivityId = @Id";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.Add(new SqlParameter("Date", activity.Date));
        //        command.Parameters.Add(new SqlParameter("Id", activity.Id));


        //        command.ExecuteNonQuery();
        //    }
        //}
        
    }
}
