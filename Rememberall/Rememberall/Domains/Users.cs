using System;
using System.Collections.Generic;
using System.Text;

namespace Rememberall.Domains
{
    public class Users
    {
        public static string ResettedUser { get; set; }
        public static int? CurrentUserId { get; set; }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class Current
    {
        public static int? UserId { get; set; }
        public static string UserName { get; set; }

    }
}
