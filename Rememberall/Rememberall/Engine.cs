using System;
using System.Collections.Generic;
using System.Text;

namespace Rememberall
{
    class Engine
    {
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
            else
            {
                Console.ReadLine();
            }
        }


        private void Login()
        {
            throw new NotImplementedException();
        }

        private void CreateAccount()
        {
            throw new NotImplementedException();
        }

        private void MainMenu()
        {
            Header("Welcome User");

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
            ShowUserActivites(); // till dataaccess
            GetUserActivities(); // till dataaccess

            Writeline("What do you want to do?");
                Writeline("A) Add activity");
            Writeline("B) Edit activity");

            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.A) ;
            {
                AddUserActivity(); DataAccess

            }
            if (command == ConsoleKey.B)
            {
                Console.WriteLine("Which activity do you want do edit? Choose from above");

            }




        }

        private void ShowUserCalendar()
        {
            GetUserCalendar(); //Printar ut calendern samt visar dagar som användaren har aktiviter på(?)
        }

        private void Header(string v)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();   
            Console.WriteLine(v.ToUpper());
            Console.WriteLine();
        }
        private void Writeline(string v)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(v);
        }
        private void LoginScreenArt()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"______                              _                    _ _ 
| ___ \                            | |                  | | |
| |_/ /___ _ __ ___   ___ _ __ ___ | |__   ___ _ __ __ _| | |
|    // _ \ '_ ` _ \ / _ \ '_ ` _ \| '_ \ / _ \ '__/ _` | | |
| |\ \  __/ | | | | |  __/ | | | | | |_) |  __/ | | (_| | | |
\_| \_\___|_| |_| |_|\___|_| |_| |_|_.__/ \___|_|  \__,_|_|_|");
        }
    }
}
