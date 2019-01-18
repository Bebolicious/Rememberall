using Rememberall.Domains;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;


namespace Rememberall
{
    class Engine
    {

        DataAccess _dataAccess = new DataAccess();
        internal void Run()
        {
            //Startup();
            LoginScreen();
        }

        private void LoginScreen()
        {
            Header("\n\nPlease log in or create an account");
            Header2("  A) Log in", "\n   B) Create account", "\n   ESC) Exit");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Header1("Rememberall", "Version 2.3");

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
            Header("Log In\nPress 'Enter' Twice to go back.");
            Write("Enter Username:");
            string username = Console.ReadLine();
            string pass = SetHiddenPassLogin();

            if (username == "" && pass == "")
            {
                LoginScreen();
            }

            string Hashpass = HashPass(pass);
            bool Username = DataAccess.MatchUsername(username, Hashpass);

            if (Username == true)
            {

                Users.CurrentUserId = DataAccess.SetCurrentUser(username);
                MainMenu();
            }
            else
            {

                if (Users.ResettedUserint == 2)
                {
                    Header("Password reset");
                    Writeline("You have entered a wrong password too many times, do you want to reset your password?");
                    Writeline("A) Yes");
                    Writeline("B) No");

                    ConsoleKey command = Console.ReadKey(true).Key;
                    if (command == ConsoleKey.A)
                        PasswordReset();

                    if (command == ConsoleKey.B)
                    {
                        Users.ResettedUserint = 0;
                        Login();
                    }
                    else
                    {
                        Users.ResettedUserint = 0;
                        Login();
                    }

                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("Wrong Username or Password, try again.");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1500);
                Users.ResettedUserint++;
                Login();


            }
        }

        private void PasswordReset()
        {
            Header("Password Reset");
            Write("Enter your username: ");
            Console.WriteLine();
            string username = Console.ReadLine();

            bool Username = DataAccess.ResetPassword(username);
            if (Username == true)
            {

                string resettedpass = SetHiddenPass();

                string finalpass = HashPass(resettedpass);

                _dataAccess.ResetUsersPassword(finalpass);
                Writeline(" - Password reset complete");
                Thread.Sleep(400);
                Users.ResettedUserint = 0;
                Login();

            }
            else
            {
                Writeline("There is no such user.");
                Thread.Sleep(500);
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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\t\t\t\t\t\tInstructions:");
                Console.WriteLine("\t\t\t\t\t\t- Password has to be atleast 8 characters long,");
                Console.WriteLine("\t\t\t\t\t\t- Contain atleast one upper and lower case letter,");
                Console.WriteLine("\t\t\t\t\t\t- Contain atleast one number and one special character.");
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                string input = SetHiddenPass();

                string Newpassword = HashPass(input);

                _dataAccess.CreateNewUser(Newuser, Newpassword);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User has been created");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
                LoginScreen();
            }

            catch (System.Data.SqlClient.SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
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
            int v = 0;
            DisplayCalendar(v);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();

            Writeline("A) Show my Calendar");
            Writeline("B) Show my Activities");
            Writeline("C) Show my Alarms\n");
            Console.Write("D) User settings\n\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("E) Change user\nF) Log out");
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
            if (command == ConsoleKey.F)
            {
                Users.CurrentUserId = null;
                LoginScreen();

            }
        }

        private void EditUserAlarms()
        {
            int TempId = Users.CurrentUserId.Value;
            var Cu = DataAccess.GetCurrentUserById(TempId);
            DateTime Currenttime = DateTime.UtcNow;
            Header($"{Cu.Username}'s Alarms");
            ShowAllAlarms();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("What do you want to do?");
            Console.ForegroundColor = ConsoleColor.White;
            Writeline(Currenttime.ToString().PadLeft(50));
            Writeline("A) Set alarm");
            Writeline("B) Edit alarm\n");
            Writeline("C) Go back");

            ConsoleKey alarmcommand = Console.ReadKey(true).Key;
            if (alarmcommand == ConsoleKey.A)
                SetNewAlarm();
            if (alarmcommand == ConsoleKey.B)
                MainMenu();
            if (alarmcommand == ConsoleKey.C)
                MainMenu();

            EditUserAlarms();
        }

        public void ShowAllAlarms()
        {
            int TempId = Users.CurrentUserId.Value;
            var _dataAccess = new DataAccess();
            List<Alarms> Alarmlist = _dataAccess.GetAllAlarms(TempId);
            Console.WriteLine("Alarm Name:".PadRight(20) + "Alarm Time".PadRight(23) + "Day".PadRight(20) + "Date");
            foreach (Alarms alarm in Alarmlist)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(alarm.Alarmname.PadRight(20) + alarm.Alarmtime.Hours.ToString() + ":" + alarm.Alarmtime.Minutes.ToString().PadRight(20) + alarm.DateId.DayOfWeek.ToString().PadRight(20) + alarm.DateId.Day.ToString() + "/" + alarm.DateId.Month.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void SetNewAlarm()
        {
            Header("Set new alarm");
            Writeline("Do you want to name your alarm?");
            Writeline("A) Yes");
            Writeline("B) No");
            ConsoleKey alarmcommand2 = Console.ReadKey(true).Key;

            string alarmname;
            if (alarmcommand2 == ConsoleKey.A)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Write("Name your alarm: ");
                alarmname = Console.ReadLine();
            }
            else
            {
                alarmname = "Alarm";
            }
            Header("Set new alarm");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Set date: ");
            try
            {
                DateTime alarmdate = DateTime.Parse(Console.ReadLine());
                Write("Set time for the alarm: ");
                string time = Console.ReadLine();
                int TempId = Users.CurrentUserId.Value;
                DataAccess.SetAlarmDate(alarmdate, alarmname, time, TempId);
            }
            catch (Exception)
            {
                Header("Fel format");
                Console.WriteLine();
                ErrorMessage("Fel format för alarmet, ange format i HH/MM");
                Thread.Sleep(1000);
                SetNewAlarm();
            }
        }


        private void SetNewActivityAlarm(int activityId, DateTime Newdate)
        {
            Header("Set new alarm");
            Writeline("Do you want to name your alarm?");
            Writeline("A) Yes");
            Writeline("B) No");
            ConsoleKey alarmcommand2 = Console.ReadKey(true).Key;

            string alarmname;
            if (alarmcommand2 == ConsoleKey.A)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Write("Name your alarm: ");
                alarmname = Console.ReadLine();
            }
            else
            {
                alarmname = "Alarm";
            }
            Header("Set new alarm");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Do you want the alarm for the same date as your activity?");
            Writeline("\nA) Yes\nB) No, i want to set the alarm for another date.");
            ConsoleKey alarmcommand3 = Console.ReadKey(true).Key;

            if (alarmcommand3 == ConsoleKey.A)
            {
                Header("Set new alarm");
                DateTime alarmdate = Newdate;
                Write("Set time for the alarm: ");
                string time = Console.ReadLine();
                int TempId = Users.CurrentUserId.Value;
                DataAccess.SetActivityAlarmDate2(alarmdate, alarmname, time, TempId, activityId);
            }
            else
            {
                Header("Set new alarm");
                Write("Set a new date for your alarm (YYYY/MM/DD): ");
                DateTime alarmdate = DateTime.Parse(Console.ReadLine());
                Write("Set time for the alarm:");
                string time = Console.ReadLine();
                int TempId = Users.CurrentUserId.Value;
                DataAccess.SetActivityAlarmDate2(alarmdate, alarmname, time, TempId, activityId);
            }
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
            Console.WriteLine();


            Writeline("What do you want to do?");
            Console.WriteLine();
            Writeline("A) Add activity");
            Writeline("B) Edit activity");
            Writeline("C) Delete activity");
            Console.WriteLine();
            Writeline("D) Go back");

            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.A)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Header($"{Cu.Username}'s Activities");
                ShowUserActivity();
                Writeline("Name your activity");
                string newActivity = Console.ReadLine();
                int acktivity = _dataAccess.AddUserActivity(newActivity);


                Console.WriteLine("Add a date and time for the activity(YYYY-MM-DD HH:MM)");
                DateTime newDateTime = DateTime.Parse(Console.ReadLine());

                _dataAccess.AddManyActivities(acktivity, newDateTime, Users.CurrentUserId);
                Header($"{Cu.Username}'s Activities");
                Console.WriteLine("Do you want to set an alarm for this activity?");
                Console.WriteLine("A) Yes");
                Console.WriteLine("B) No");
                ConsoleKey alarmcommand = Console.ReadKey(true).Key;
                if (alarmcommand == ConsoleKey.A)
                    SetNewActivityAlarm(acktivity, newDateTime);
                else
                {
                    ManageActivities();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDin aktivitet har sparats");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                ManageActivities();
            }
            if (command == ConsoleKey.B)
            {
                EditUserActivities();
            }

            if (command == ConsoleKey.C)
            {
                DeleteActivityFromActivities();
            }
            if (command == ConsoleKey.D)
            {
                MainMenu();
            }
        }

        private void EditUserActivities()
        {
            int TempId = Users.CurrentUserId.Value;
            var rowdic = ShowUserActivity();

            var Cu = DataAccess.GetCurrentUserById(TempId);

            try
            {
                Header($"{Cu.Username}'s Activities");
                ShowUserActivity(); //Snyggt att man ser sina aktiviteter direkt :)
                Console.WriteLine("Which activity do you want do edit? Choose from above");

                int rownumber = int.Parse(Console.ReadLine());
                int activityId = rowdic[rownumber];
                Activities activity = _dataAccess.GetUserActivitiesById(activityId);

                Writeline("Rename: ");
                string newName = Console.ReadLine();
                Writeline("Update time: ");
                DateTime newDate = DateTime.Parse(Console.ReadLine());
                activity.Activityname = newName;
                activity.Date = newDate;

                _dataAccess.UpdateActivityName(activity);
                _dataAccess.UpdateActivityDate(activity);
                ManageActivities();
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose by entering the number for the activity you want to edit");
                Thread.Sleep(2000);
                Console.ResetColor();
                EditUserActivities();

            }
        }

        private void DeleteActivityFromActivities()
        {
            int TempId = Users.CurrentUserId.Value;

            var Cu = DataAccess.GetCurrentUserById(TempId);
            var rowdic = ShowUserActivity();


            Header($"{Cu.Username}'s Activities");
            ShowUserActivity();
            Writeline("Which activity do you want to delete? Choose from above");
            try
            {
                int rownumber = int.Parse(Console.ReadLine());
                int activityId = rowdic[rownumber];
                // int activityId = int.Parse(Console.ReadLine());
                Activities deleteActivity = _dataAccess.GetUserActivitiesById(activityId);
                _dataAccess.DeleteActivity(deleteActivity);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your activity has been deleted");
                Thread.Sleep(1000);
                Console.ResetColor();
                ManageActivities();

            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose by entering the number for the activity you want to delete");
                Thread.Sleep(2000);
                Console.ResetColor();
                DeleteActivityFromActivities();
            }
        }

        private void ShowUserCalendar()
        {

            Header($"{Current.UserName}s Calendar");

            int move = 0;
            DisplayCalendar(move);
            DisplayCalendar(move + 1);
            DisplayCalendar(move + 2);
            DisplayCalendar(move + 3);
            DisplayCalendar(move + 4);
            DisplayCalendar(move + 5);
            DisplayCalendar(move + 6);
            DisplayCalendar(move + 7);
            DisplayCalendar(move + 8);
            DisplayCalendar(move + 9);
            DisplayCalendar(move + 10);
            DisplayCalendar(move + 11);

            Console.WriteLine();
            Console.WriteLine("Press ESC to go back");
            ConsoleKey command = Console.ReadKey(true).Key;
            if (command == ConsoleKey.Escape)
            {
                MainMenu();
            }
        }

        private Dictionary<int, int> ShowUserActivity()
        {
            List<Activities> list = _dataAccess.GetUserActivities(Users.CurrentUserId);

            Dictionary<int, int> rowdic = new Dictionary<int, int>();

            int rownumber = 1;

            Console.WriteLine(" ".PadRight(5) + "Activityname".PadRight(30) + "Date".PadRight(20) + "Activity Start".PadRight(10));
            Console.WriteLine();

            foreach (Activities item in list)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                rowdic.Add(rownumber, item.Id);

                //Console.WriteLine($"{rownumber.ToString().PadRight(10)}{item.Activityname.PadRight(40)}{item.Date}");
                Console.WriteLine(rownumber.ToString().PadRight(5) + item.Activityname.PadRight(30) + item.Date.DayOfWeek.ToString().PadRight(10) + item.Date.Day.ToString() + "/" + item.Date.Month.ToString().PadRight(15) + item.Date.ToString("HH:mm"));
                rownumber++;
            }
            Console.WriteLine();
            Console.WriteLine();

            return rowdic;
        }

        public string SetHiddenPass()
        {
            Write("Please choose a password:");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
                Console.Write("*");
            }

            bool reggedpass = RegexPass(input);

            if (reggedpass == false)
            {
                SetHiddenPass();

            }

            return input.ToString();
        }

        public string SetHiddenPassLogin()
        {
            Write("Please enter password:");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
                Console.Write("*");
            }
            return input.ToString();
        }



        private bool RegexPass(StringBuilder input)
        {
            string password = input.ToString();
            string ErrorMessage;
            Console.ForegroundColor = ConsoleColor.Red;
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password should not be empty");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,8}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(password))
            {
                ErrorMessage = "  - Password should contain At least one lower case letter";
                Console.WriteLine(ErrorMessage);

                return false;

            }
            else if (!hasUpperChar.IsMatch(password))
            {
                ErrorMessage = "  - Password should contain At least one upper case letter";
                Console.WriteLine(ErrorMessage);

                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(password))
            {
                ErrorMessage = "  - Password should not be less than or greater than 8 characters";
                Console.WriteLine(ErrorMessage);

                return false;
            }
            else if (!hasNumber.IsMatch(password))
            {
                ErrorMessage = "  - Password should contain At least one numeric value";
                Console.WriteLine(ErrorMessage);

                return false;
            }

            else if (!hasSymbols.IsMatch(password))
            {
                ErrorMessage = "  - Password should contain At least one special case characters";
                Console.WriteLine(ErrorMessage);

                return false;
            }
            else
            {
                return true;
            }
        }

        public void ErrorMessage(string v)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(v);
        }

        static string HashPass(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input)); //GOOD: smart kod!
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

        private void Write(string v)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(v);

        }

        public static void Header1(string title, string subtitle = "")
        {
            Console.WriteLine("\n\n\n\n\n\n\n");
            int windowWidth = 90 - 2;
            string titleContent = string.Format("║{0," + ((windowWidth / 2) + (title.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (title.Length / 2) + 7) + "}", title, "║");
            string subtitleContent = string.Format("║{0," + ((windowWidth / 2) + (subtitle.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (subtitle.Length / 2) + 7) + "}", subtitle, "║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(titleContent);

            if (!string.IsNullOrEmpty(subtitle))
            {
                Console.WriteLine(subtitleContent);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        public static void Header2(string v, string b, string c)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔═════════════════════╗");
            Console.WriteLine("║" + v + b + c + "          ║");     //GOOD: Snygga "ramar" runt 
            Console.WriteLine("╚═════════════════════╝");
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
        private static void DisplayUsage()
        {
            Console.WriteLine("Console Calendar");
            Console.WriteLine("usage: calendar <year> <month>");
            Console.WriteLine();
            Console.WriteLine("Year must be a 4-digit year.");
            Console.WriteLine("Month must be a number between 1 and 12.");
        }

        private static void DisplayCalendar(int v)
        {
            int CurrentYear = DateTime.Today.Year;
            int CurrentMonth = DateTime.Today.Month + v; //  int v + v-------------------Ändra här för att ändra månad +1= februari, +2=mars etc.
            int CurrentDay = DateTime.Today.Day;

            DisplayCalendar(CurrentYear, CurrentMonth, CurrentDay);
        }

        private static void DisplayCalendar(int TheYear, int TheMonth)
        {
            DisplayCalendar(TheYear, TheMonth, 1);
        }

        private static void DisplayCalendar(int TheYear, int TheMonth, int TheDay)
        {

            Int16 FirstDayOfMonth = 1;
            Int32 NumberOfDaysInMonth = DateTime.DaysInMonth(TheYear, TheMonth);
            DateTime FullDateToUse = new DateTime(TheYear, TheMonth, FirstDayOfMonth);

            Int32 StartDay = Convert.ToInt32(FullDateToUse.DayOfWeek);

            Int32 NumberOfTabs = StartDay;

            DisplayHeader(GetMonthName(TheMonth), TheYear.ToString(), true);

            //-------------------------------------------------
            int DayOfWeek = StartDay;
            for (int Counter = 2; Counter <= NumberOfDaysInMonth; Counter++)
            {
                string DayString = "";

                if (Counter == 2) // 
                {
                    String Padding = new String('\t', NumberOfTabs);
                    DayString = String.Concat(Padding, Counter.ToString());
                }
                else
                {
                    DayString = Counter.ToString();
                }

                // highlight todays date (using *) ------------------- Lägga till aktivitet
                if (TheDay != 1 && Counter == TheDay && TheMonth == 1)
                {
                    DayString = String.Concat("*", DayString);
                }

                if (DayOfWeek % 7 == 0 && Counter > 1)
                {
                    DayString = String.Concat("\n", Counter.ToString());
                }

                Console.Write("{0}\t", DayString);

                DayOfWeek++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        // ÄNDRA HÄR---------------------------------------------------

        private static void DisplayHeader(string theMonthName, string theYear, bool ShowCurrentDate)
        {
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
            //Console.WriteLine("*=Current day");
            Console.WriteLine(Days);
            Console.WriteLine(Divider);
        }

        private static string GetMonthName(int theMonth)
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            string month = info.MonthNames[theMonth - 1];
            return month;
        }

        public void Startup()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string text = (@"
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
            string loading = (@"█████████████████████████████████████████████████████████");

            Console.WriteLine("Loading: ");

            foreach (char b in loading)
            {
                Console.Write(b);
                Thread.Sleep(90);
            }
            Console.Clear();

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(2);
            }
        }
    }

}
