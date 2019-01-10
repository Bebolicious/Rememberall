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
        {   // kan vara bra för att skriva ut veckodagar
            //string s = "hello|world";
            //Console.Setcursorposition((Console.windowwidth - s.length) / 2, console.cursortop);
            //Console.Writeline(s);

            LoginScreen();
            MainMenu();
        }

        private void LoginScreen()
        {
            LoginScreenArt();
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
                Console.Clear();
                
            }
        }


        private void Login()
        {
            Header("Log In");
            Write("Enter Username:");

            string username = Console.ReadLine();
            Write("Enter Password:");
            string pass = SetHiddenPass();

            string Hashpass = Hash(pass);


            bool Username = DataAccess.MatchUsername(username, Hashpass);

            if (Username==true)
            {
                MainMenu();
            }
            else
            {
                Writeline("Wrong Username or Password, try again.");
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
                Console.Clear();
                Header("Create new account");
                Write("Please choose a password:");
                string input = SetHiddenPass();

                string Newpassword = Hash(input);

                _dataAccess.CreateNewUser(Newuser, Newpassword);

                Writeline("User has been created");
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
      Header("Welcome ");

            Console.WriteLine("What do you want to do?");
            Writeline("A) Show my calendar");
            Writeline("B) Show my activities");
            Writeline("C) Edit my alarms");

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
            
        }

        private void EditUserAlarms()
        {
            throw new NotImplementedException();
        }

        private void ManageActivities()
        {
            Header("Usernames Activities");

            Console.WriteLine("You have the following activities:");
            Console.WriteLine();
            //GetUserActivities(); // till dataaccess
            ShowUserActivity();


            Writeline("What do you want to do?");
                Writeline("A) Add activity");
            Writeline("B) Edit activity");

            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.A)
            {
                Console.WriteLine("Name your activity");
                string newActivity = Console.ReadLine();
                int acktivity = _dataAccess.AddUserActivity(newActivity);


                Console.WriteLine("Add a date for the activity");
                DateTime newDateTime = DateTime.Parse(Console.ReadLine());

                _dataAccess.AddManyActivities(acktivity, newDateTime, Users.CurrentUserId);

                Console.WriteLine("Din aktivitet har sparats");
                Thread.Sleep(2000);

               
            }

              

            
            if (command == ConsoleKey.B)
            {
                Console.WriteLine("Which activity do you want do edit? Choose from above");

            }




        }

        private void ShowUserCalendar()
        {
            //GetUserCalendar(); //Printar ut calendern samt visar dagar som användaren har aktiviter på(?)
            //PrintUserCalender(); //Printar kalendern veckovis eller månadsvis, markerar dagens datum
            ShowUserActivity();
        }

        private void ShowUserActivity()
        {
            List<Activities> list = _dataAccess.GetUserActivities(Users.CurrentUserId);

            foreach (Activities item in list)
            {
                Console.WriteLine(item.Activityname + "     " + item.Date);
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
        static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        public void Header(string v)
        {
            Console.Clear();
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
        public void LoginScreenArt()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"______                              _                    _ _ 
| ___ \                            | |                  | | |
| |_/ /___ _ __ ___   ___ _ __ ___ | |__   ___ _ __ __ _| | |
|    // _ \ '_ ` _ \ / _ \ '_ ` _ \| '_ \ / _ \ '__/ _` | | |
| |\ \  __/ | | | | |  __/ | | | | | |_) |  __/ | | (_| | | |
\_| \_\___|_| |_| |_|\___|_| |_| |_|_.__/ \___|_|  \__,_|_|_|");

        }
        private void Write(string v)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(v);
        }
    }
}
