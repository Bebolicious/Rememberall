﻿using Rememberall.Domains;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;


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

            if (Username == true)
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

            displayCalendar();

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
            int TempId = Users.CurrentUserId.Value;
            var Cu = DataAccess.GetCurrentUserById(TempId);
            DateTime Currenttime = DateTime.UtcNow;
            Header($"{Cu.Username}'s Alarms");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("What do you want to do?");
            Console.ForegroundColor = ConsoleColor.White;
            Writeline(Currenttime.ToString().PadLeft(50));
            Writeline("A) Set alarm");
            Writeline("B) Edit alarm\n");
            Writeline("C) Go back");

            ConsoleKey alarmcommand = Console.ReadKey(true).Key;
            if (alarmcommand == ConsoleKey.A)
            {
                Header("Set new alarm");
                Writeline("Do you want to name your alarm?");
                Writeline("A) Yes");
                Writeline("B) No");
                ConsoleKey alarmcommand2 = Console.ReadKey(true).Key;

                string alarmname;
                if (alarmcommand2 == ConsoleKey.A)
                {
                    Write("Name your alarm: ");
                    alarmname = Console.ReadLine();
                }
                else
                {
                    alarmname = "DEFAULT";
                }
                // Händer alltid
                Header("Set new alarm");
                Console.Write("Set date: ");
                DateTime alarmdate = DateTime.Parse(Console.ReadLine());
                Write("Set time for the alarm: ");
                DateTime alarmtime = DateTime.Parse(Console.ReadLine());
                DataAccess.SetAlarmDate(alarmdate, alarmname, alarmtime);
            }
            if (alarmcommand == ConsoleKey.B)
            { }
            if (alarmcommand == ConsoleKey.C)
                MainMenu();

        }

        private void ManageActivities()
        {
            int TempId = Users.CurrentUserId.Value;

            var Cu = DataAccess.GetCurrentUserById(TempId);
            Header($"{Cu.Username}'s Activities");

            Console.WriteLine("You have the following activities:");
            Console.WriteLine();
            //GetUserActivities(); // till dataaccess
            var rowdic = ShowUserActivity();


            Writeline("What do you want to do?");
            Writeline("A) Add activity");
            Writeline("B) Edit activity");
            Writeline("C) Delete activity");
            Writeline("D) Go back");

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

                int rownumber = int.Parse(Console.ReadLine());
                int activityId = rowdic[rownumber];
                //int activityId = int.Parse(Console.ReadLine());
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
                Console.WriteLine("Which activity do you want to delete? Choose from above");
                int rownumber = int.Parse(Console.ReadLine());
                int activityId = rowdic[rownumber];
               // int activityId = int.Parse(Console.ReadLine());
                Activities deleteActivity = _dataAccess.GetUserActivitiesById(activityId);
                _dataAccess.DeleteActivity(deleteActivity);
                Console.WriteLine("Your activity has been deleted");
                Thread.Sleep(2000);
                MainMenu();
            }
            if (command == ConsoleKey.D)
            {
                MainMenu();

            }




        }

        private void ShowUserCalendar()
        {
            //GetUserCalendar(); //Printar ut calendern samt visar dagar som användaren har aktiviter på(?)
            //PrintUserCalender(); //Printar kalendern veckovis eller månadsvis, markerar dagens datum
            // ShowUserActivity();
            
            displayCalendar();
        }


        private Dictionary<int, int> ShowUserActivity()
        {
            List<Activities> list = _dataAccess.GetUserActivities(Users.CurrentUserId);

            Dictionary<int, int> rowdic = new Dictionary<int, int>();

            int rownumber = 1;

            foreach (Activities item in list)
            {
                rowdic.Add(rownumber, item.Id);
                Console.WriteLine("(" + rownumber + ")     " +  item.Activityname + "     " + item.Date);
            }
            return rowdic;
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


        //private void DisplayCalendar(string[] args)
        //{
        //    Int16 theYear = 0;
        //    Int16 theMonth = 0;

        //    switch (args.Length)
        //    {
        //        case 0:
        //            displayCalendar();
        //            break;
        //        case 1:
        //            // show help
        //            if (args[0] == "-?" || args[0] == "/?" || args[0] == "?" || args[0] == "-h")
        //            {
        //                displayUsage();
        //            }
        //            break;
        //        case 2:
        //            // see if Year is the first parameter
        //            if (args[0].Length > 2)
        //            {
        //                theYear = Convert.ToInt16(args[0]);
        //            }
        //            else
        //            {
        //                theYear = Convert.ToInt16(args[1]);
        //            }
        //            // be realistic
        //            if (theYear < 1900 || theYear > 3000)
        //            {
        //                displayUsage();
        //                return;
        //            }
        //            if (args[0].Length <= 2)
        //            {
        //                theMonth = Convert.ToInt16(args[0]);
        //            }
        //            else
        //            {
        //                theMonth = Convert.ToInt16(args[1]);
        //            }
        //            // only allow valid months
        //            if (theMonth < 1 || theMonth > 12)
        //            {
        //                displayUsage();
        //                return;
        //            }
        //            displayCalendar(theYear, theMonth);
        //            break;
        //        default:
        //            displayUsage();
        //            break;
        //    }
        //}

        // help
        private static void displayUsage()
        {
            Console.WriteLine("Console Calendar (c) Copyright 2002 Michael Eaton");
            Console.WriteLine("usage: calendar <year> <month>");
            Console.WriteLine();
            Console.WriteLine("Year must be a 4-digit year.");
            Console.WriteLine("Month must be a number between 1 and 12.");
        }

        // the guts (overloaded methods)
        // default to the current month
        private static void displayCalendar()
        {
            int CurrentYear = DateTime.Today.Year;
            int CurrentMonth = DateTime.Today.Month;
            int CurrentDay = DateTime.Today.Day;
            displayCalendar(CurrentYear, CurrentMonth, CurrentDay);
        }

        private static void displayCalendar(int TheYear, int TheMonth)
        {
            displayCalendar(TheYear, TheMonth, 1);
        }

        private static void displayCalendar(int TheYear, int TheMonth, int TheDay)
        {
            // default to the first of the month
            Int16 FirstDayOfMonth = 1;
            Int32 NumberOfDaysInMonth = DateTime.DaysInMonth(TheYear, TheMonth);
            DateTime FullDateToUse = new DateTime(TheYear, TheMonth, FirstDayOfMonth);

            // this is the day of week we're gonna start with (0-6)
            Int32 StartDay = Convert.ToInt32(FullDateToUse.DayOfWeek);

            // this indicates how much padding we need for
            // the first day of the month.
            Int32 NumberOfTabs = StartDay;

            // this will display the month name and
            // the headings for the days of the week.
            displayHeader(getMonthName(TheMonth), TheYear.ToString(), true);

            // accumulator used so we'll know when to wrap 
            // to the next week.
            //-------------------------------------------------
            int DayOfWeek = StartDay;
            for (int Counter = 2; Counter <= NumberOfDaysInMonth; Counter++)// Ändrade till 2 ist för 1
            {
                string DayString = "";
                // if it's the first day of the month, we'll need
                // padding so we start on the correct "day"
                if (Counter == 2) // Ändrade till 2 ist för 1
                {
                    String Padding = new String('\t', NumberOfTabs);
                    DayString = String.Concat(Padding, Counter.ToString());
                }
                else
                {
                    DayString = Counter.ToString();
                }

                // highlight todays date (using *)
                if (TheDay != 1 && Counter == TheDay)
                {
                    DayString = String.Concat("*", DayString);
                }

                // start a new line only if this isn't the first day
                if (DayOfWeek % 7 == 0 && Counter > 1)
                {
                    DayString = String.Concat("\n", Counter.ToString());
                }

                // separate each day with a tab
                Console.Write("{0}\t", DayString);

                DayOfWeek++;
            }
            // blank line after the calendar has been printed
            Console.WriteLine();
        }
        // ÄNDRA HÄR---------------------------------------------------
        // header for the calendar display
        private static void displayHeader(string theMonthName, string theYear, bool ShowCurrentDate)
        {
            // the Month, year
            string Header = String.Concat(theMonthName, ", ");
            Header = String.Concat(Header, theYear);
            String Days = "M\tT\tW\tTh\tF\tS\tS"; // flttade om här-
            String Divider = new String('-', 55);

            if (ShowCurrentDate)
            {
                Console.WriteLine();
                Console.WriteLine("\t\tToday is {1}/{0}/{2}.", DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year);
            }

            Console.WriteLine();
            Console.WriteLine(String.Concat("\t\t", Header));
            Console.WriteLine(Days);
            Console.WriteLine(Divider);
        }

        private static string getMonthName(int theMonth)
        {
            // changes suggested by Brandon Croft.  Much shorter than
            // using the arraylist!
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            string month = info.MonthNames[theMonth - 1];
            return month;
        }
    
    }
}
