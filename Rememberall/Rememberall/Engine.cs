using System;
using System.Collections.Generic;
using System.Text;

namespace Rememberall
{
    class Engine
    {
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
