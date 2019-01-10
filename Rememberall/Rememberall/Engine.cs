using Rememberall.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Rememberall
{
    class Engine
    {
     
        public static System.Drawing.Point Position { get; set; }
        DataAccess _dataAccess = new DataAccess();
        internal void Run()
        { 
            LoginScreen();           
        }

        private void LoginScreen()
        {
            
            Header("\n\nPlease log in or create an account");
            Writeline("A) Log In");
            Writeline("B) Create Account");
            Writeline("\nESC) Exit");
            ConsoleKey command = Console.ReadKey(true).Key;
                if (command == ConsoleKey.A)
                    Login();
                if (command == ConsoleKey.B)
                    CreateAccount();
                if (command == ConsoleKey.Escape)
                Console.WriteLine();

            else
            {
                LoginScreen();
            }

                           

}


        private void Login()
        {
            Header("Log In");
            Write("Enter Username:");

            string username = Console.ReadLine();
            Write("Enter Password:");
            string pass = SetHiddenPass();

            string Hashpass = HashPass(pass);


            bool Username = DataAccess.MatchUsername(username, Hashpass);

            if (Username==true)
            {
                
                Users.CurrentUserId = DataAccess.SetCurrentUser(username);
                MainMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Username or Password, try again.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Login();
            }


        }


        private void CreateAccount()
        {

            try
            {

                Header("Create new account");
                Write("Please choose a username:");
                string Newuser = Console.ReadLine();
                Header("Create new account");
                Write("Please choose a password:");
                string input = SetHiddenPass();

                string Newpassword = HashPass(input);

                _dataAccess.CreateNewUser(Newuser, Newpassword);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User has been created");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                LoginScreen();
                
            }

            catch (System.Data.SqlClient.SqlException)
            {
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username is taken");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
        CreateAccount();


    }
}

        private void MainMenu()
        {
            int TempId = Users.CurrentUserId.Value;

            var Cu = DataAccess.GetCurrentUserById(TempId);
             Header($"Welcome {Cu.Username}");
            
            Console.WriteLine("What do you want to do?");
            Writeline("A) Calendar");
            Writeline("B) Activities");
            Writeline("C) Alarms\n");
            Console.Write("D) User settings\n\nE)");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Log out");
            Console.ForegroundColor = ConsoleColor.White;

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.A)
            {
                ShowUserCalendar();
            }
            if (command == ConsoleKey.B)
            {
                ManageActivities();
            }
            if (command == ConsoleKey.C)
            {
                EditUserAlarms();
            }
            if (command == ConsoleKey.D)
            {
                EditUserAlarms();
            }
            if (command == ConsoleKey.E)
            {
                LoginScreen();
            }

        }

        private void EditUserAlarms()
        {
            throw new NotImplementedException();
        }

        private void ManageActivities()
        {
            int TempId = Users.CurrentUserId.Value;

            var Cu = DataAccess.GetCurrentUserById(TempId);
            Header($"{Cu.Username}'s Activities");

            Console.WriteLine("You have the following activities:");
            Console.WriteLine();
            //GetUserActivities(); // till dataaccess
            ShowUserActivity();


            Writeline("What do you want to do?");
                Writeline("A) Add activity");
            Writeline("B) Edit activity\n");
            Writeline("C) Go back");

            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.A)
            {
                Console.WriteLine("Name your activity");
                string newActivity = Console.ReadLine();
                int acktivity = _dataAccess.AddUserActivity(newActivity);


                Console.WriteLine("Add a date for the activity");
                DateTime newDateTime = DateTime.Parse(Console.ReadLine());

                _dataAccess.AddManyActivities(acktivity, newDateTime, Users.CurrentUserId);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Din aktivitet har sparats");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                MainMenu();
            }
            if (command == ConsoleKey.B)
            {
                Console.WriteLine("Which activity do you want do edit? Choose from above");
                int activityId = int.Parse(Console.ReadLine());
                Activities activity = _dataAccess.GetUserActivitiesById(activityId);

                Console.WriteLine("Rename");
                string newName = Console.ReadLine();
                Console.WriteLine("Update time");
                DateTime newDate = DateTime.Parse(Console.ReadLine());
                activity.Activityname = newName;
                activity.Date = newDate;

                _dataAccess.UpdateActivityName(activity);
                _dataAccess.UpdateActivityDate(activity);
                MainMenu();

            }
            if (command == ConsoleKey.C)
            {
                MainMenu();
            }




        }

        private void ShowUserCalendar()
        {
            //GetUserCalendar(); //Printar ut calendern samt visar dagar som användaren har aktiviter på(?)
            //PrintUserCalender(); //Printar kalendern veckovis eller månadsvis, markerar dagens datum
           // ShowUserActivity();
        }

        private void ShowUserActivity()
        {
            List<Activities> list = _dataAccess.GetUserActivities(Users.CurrentUserId);

            foreach (Activities item in list)
            {
                Console.WriteLine(item.Id+ "     " +  item.Activityname + "     " + item.Date);
            }
        }

        public string SetHiddenPass()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }
        static string HashPass(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        public void Header(string v)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
██▀███  ▓█████  ███▄ ▄███▓▓█████  ███▄ ▄███▓ ▄▄▄▄   ▓█████  ██▀███   ▄▄▄       ██▓     ██▓    
▓██ ▒ ██▒▓█   ▀ ▓██▒▀█▀ ██▒▓█   ▀ ▓██▒▀█▀ ██▒▓█████▄ ▓█   ▀ ▓██ ▒ ██▒▒████▄    ▓██▒    ▓██▒    
▓██ ░▄█ ▒▒███   ▓██    ▓██░▒███   ▓██    ▓██░▒██▒ ▄██▒███   ▓██ ░▄█ ▒▒██  ▀█▄  ▒██░    ▒██░    
▒██▀▀█▄  ▒▓█  ▄ ▒██    ▒██ ▒▓█  ▄ ▒██    ▒██ ▒██░█▀  ▒▓█  ▄ ▒██▀▀█▄  ░██▄▄▄▄██ ▒██░    ▒██░    
░██▓ ▒██▒░▒████▒▒██▒   ░██▒░▒████▒▒██▒   ░██▒░▓█  ▀█▓░▒████▒░██▓ ▒██▒ ▓█   ▓██▒░██████▒░██████▒
░ ▒▓ ░▒▓░░░ ▒░ ░░ ▒░   ░  ░░░ ▒░ ░░ ▒░   ░  ░░▒▓███▀▒░░ ▒░ ░░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░ ▒░▓  ░░ ▒░▓  ░
  ░▒ ░ ▒░ ░ ░  ░░  ░      ░ ░ ░  ░░  ░      ░▒░▒   ░  ░ ░  ░  ░▒ ░ ▒░  ▒   ▒▒ ░░ ░ ▒  ░░ ░ ▒  ░
  ░░   ░    ░   ░      ░      ░   ░      ░    ░    ░    ░     ░░   ░   ░   ▒     ░ ░     ░ ░   
   ░        ░  ░       ░      ░  ░       ░    ░         ░  ░   ░           ░  ░    ░  ░    ░  ░
                                                   ░                                           ");

        
        Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();   
            Console.WriteLine(v.ToUpper()); 
            Console.WriteLine();
        }
        public void Writeline(string v)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(v);
        }
        private void CloseApp()
        {
            Console.WriteLine();
        }

        private void Write(string v)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(v);
        }
    }
}
