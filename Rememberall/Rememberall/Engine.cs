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
            Header("Start");
            
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
        }
    }
}
