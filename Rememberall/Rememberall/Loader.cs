using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Rememberall
{
    public class Loader
    {
        public static void Run()
        {

            PreparingRocket();

            TakeOff();

        }
        

        private static void TakeOff()
        {
            AZSpaceloader(100, 100);
            Console.Write("Rememberall Ready!               ");
            Thread.Sleep(2000);
            Console.WriteLine();
        }

        public static void PreparingRocket()
        {

            Loader.AZSpaceloader(10, 100);
            Console.Write("Forgetting things                ");
            Thread.Sleep(2000);

            AZSpaceloader(20, 100);
            Console.Write("It's leviOsa, not leviosaaa               ");
            Thread.Sleep(3000);

            AZSpaceloader(50, 100);
            Thread.Sleep(300);
            AZSpaceloader(55, 100);
            Thread.Sleep(200);
            AZSpaceloader(60, 100);
            Thread.Sleep(500);
            AZSpaceloader(65, 100);
            Console.Write("Setting up Tri-wizard tournament               ");
            Thread.Sleep(3000);
            AZSpaceloader(70, 100);
        }

        public static void AZSpaceloader(int progress, int tot)
        {

            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = 32;
            Console.Write("]");
            Console.CursorLeft = 1;
            float onechunk = 30.0f / tot;


            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }


            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }


            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + "% of " + tot.ToString() + "%    ");
        }


    }



}
