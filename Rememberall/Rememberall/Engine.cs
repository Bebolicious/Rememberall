using System;
using System.Collections.Generic;
using System.Text;


namespace Rememberall
{
    class Engine
    {
        DataAccess _dataAccess = new DataAccess();
        internal void Run()
        {
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
                LoginScreen();
            }
        }


        private void Login()
        {
            Header("Log In");
            Write("Enter Username:");
            string Username = Console.ReadLine();

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
          
        }

        private static string GetHiddenPass()
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
