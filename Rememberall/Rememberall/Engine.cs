using System;
using System.Collections.Generic;
using System.Text;


namespace Rememberall
{
    class Engine
    {
        DataAccess _dataAccess = new DataAccess();
        internal void Run()
        {   // Kan vara bra för att skriva ut veckodagar
            //string s = "Hello|World";
            //Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            //Console.WriteLine(s);

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

            string input1 = Console.ReadLine();
            Write("Enter Password:");
            string input2 = Console.ReadLine();
            bool Username = DataAccess.MatchUsername(input1, input2);

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
            Header("Create new account");
            Write("Please choose a username:");
            string Newuser = Console.ReadLine();
            Console.Clear();
            Header("Create new account");
            Write("Please choose a password:");
            string Newpassword = GetHiddenPass();

            _dataAccess.CreateNewUser(Newuser, Newpassword);

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
            //ShowUserActivites(); // till dataaccess
            //GetUserActivities(); // till dataaccess

            Writeline("What do you want to do?");
                Writeline("A) Add activity");
            Writeline("B) Edit activity");

            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.A)
              // AddUserActivity(); DataAccess

            
            if (command == ConsoleKey.B)
            {
                Console.WriteLine("Which activity do you want do edit? Choose from above");

            }




        }

        private void ShowUserCalendar()
        {
            //GetUserCalendar(); //Printar ut calendern samt visar dagar som användaren har aktiviter på(?)
        }


        public string GetHiddenPass()
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
