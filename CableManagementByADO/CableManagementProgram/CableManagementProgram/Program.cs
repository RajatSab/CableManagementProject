using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CableManagementProgram
{
    public static class Program
    {
        public static void Main()
        {

            string Homepage = "-:Welcome To D2H Management System:-";
            Console.WriteLine(Homepage.DrawInConsoleBox());
            Console.WriteLine("Make Your Choice:\n" +
                            "1. Log In\n" +
                            "2. Sign Up");
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = ConsoleColor.White;
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter Username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Password:");
                        string Password = Console.ReadLine();
                        WaitingAnnimation();

                        DAO.LogIn(username, Password);
                        Main();
                        break;
                    }
                case 2:
                    {
                        int index = 0;
                        string username;
                        do
                        {
                            Console.WriteLine("Enter Username:");
                            username = Console.ReadLine();
                            index = DAO.CheckStringInColumn("Users", "Username", username);
                            if (index > 0)
                            {
                                Console.WriteLine("Username Already Exists. Please try Again...");
                            }
                        } while (index > 0);
                        Console.WriteLine("Enter Password:");
                        string Password = Console.ReadLine();

                        DAO.SignUp(username, Password);
                        break;
                    }
                default:
                    {

                        Console.WriteLine("Invalid Choice. Please Try Again..");
                        Thread.Sleep(2500);
                        Console.Clear();
                        Main();
                        break;
                    }
            }
            Console.ReadLine();
        }


        public   static void UserMenu(string name, int type)
        {
            Console.Clear();
            DrawInConsoleBox("Welcome to D2H Management System " + name + type + "\n" +
                "");

            switch (type)
            {
                case 1:
                    {
                        AdminMenu(name);
                        break;
                    }
                case 2:
                    {
                        AgentMenu(name);
                        break;
                    }
                case 3:
                    {
                        CustomerMenu(name);
                        break;
                    }
                default:
                    {
                        DrawInConsoleBox("Invalid Input. Try Again");
                        UserMenu(name, type);
                        break;
                    }
            }
        }

        private static void CustomerMenu(string name)
        {
            Console.WriteLine("Welcome To Pi D2H Complaint Management Service " + name + "\n" +
                "\n");
            Console.WriteLine("Enter 1 to Raise Comaplint\n" +
                                "     2 to Check Status of your Complaint\n" +
                                "      3 Logout");
            int choice;
            //int id = DAO.GetCustomerIdByName(name);

            int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    {
                        DAO.RaiseComplaint();
                        CustomerMenu(name);
                        break;
                    }
                case 2:
                    {
                        //DAO.ShowwAllComplaintsForId(id);
                        int Id;
                        Console.WriteLine("Enter Complaint Id:");
                        int.TryParse(Console.ReadLine(), out Id);
                        //DAO.ComplaintStatusById(Id);
                        CustomerMenu(name);
                        break;
                    }
                case 3:
                    {
                        Main();
                        break;
                    }
            }


        }

        private static void AgentMenu(string name)
        {
            Console.WriteLine("Welcome To Pi D2H Complaint Management Service " + name + "\n" +
                "\nPlease Make your Choice:\n" +
                "1. Show All Complaintsssss in your Area\n" +
                "2. Resolve complaint by Id\n" +
                "3. Logout");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1)
            {
                int id;
                global::System.Console.WriteLine("Enter AreaId:");
                int.TryParse((string)Console.ReadLine(), out id);
                DAO.ShowAllComplaintsInArea(id);
                Console.WriteLine("Press Enter to go to MainMenu");
                Console.ReadLine();
                AgentMenu(name);
            }
            else if (choice == 2)
            {
                DAO.ResolveComplaintByComplaintId();
            }
            else if (choice == 3)
            {
                Main();
            }
        }

        private static void AdminMenu(string name)
        {
            Console.WriteLine("Welcome To Pi D2H Service " + name + "\n" +
                "\nPlease Make your Choice:\n" +
                "1. Add Masters\n" +
                "2. Add Transaction\n" +
                "3. Reports\n" +
                "4. Logout");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Make your Choice\n" +
                            "1. Add New Customer\n" +
                            "2. Add Area\n" +
                            "3. Add Agent\n" +
                            "4. Add Package\n" +
                            "5. Go Back");
                        int.TryParse(Console.ReadLine(), out choice);
                        switch (choice)
                        {
                            case 1:
                                {

                                    DAO.AddNewCustomer();
                                    AdminMenu(name);
                                    break;
                                }
                            case 2:
                                {
                                    DAO.AddNewArea();
                                    AdminMenu(name);
                                    break;
                                }
                            case 3:
                                {
                                    DAO.AddNewAgent();
                                    AdminMenu(name);
                                    break;
                                }
                            case 4:
                                {
                                    DAO.AddNewPackage();
                                    AdminMenu(name);
                                    break;
                                }
                            case 5:
                                {
                                    AdminMenu(name);
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invalid Choice. Press Enter to try Again.");
                                    Console.ReadLine();
                                    AdminMenu(name);
                                    break;
                                }

                        }

                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Make your Choice\n" +
                            "1. Raise Complaint\n" +
                            "2. Resolve Compaint\n" +
                            "3. Add Payment\n" +
                            "4. Go Back");
                        int.TryParse(Console.ReadLine(), out choice);
                        switch (choice)
                        {
                            case 1:
                                {

                                    DAO.RaiseComplaint();
                                    AdminMenu(name);
                                    break;
                                }
                            case 2:
                                {
                                    DAO.ResolveComplaintByComplaintId();
                                    AdminMenu(name);
                                    break;
                                }
                            case 3:
                                {
                                    DAO.AddPayment();
                                    AdminMenu(name);
                                    break;
                                }
                            case 4:
                                {
                                    AdminMenu(name);
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invalid Choice Please try Again");
                                    AdminMenu(name);
                                    break;
                                }

                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Make your Choice\n" +
                            "1. Pending Payments\n" +
                            "2. Agent Efficiency(Under Construction)\n" +
                            "3. Customer Profit Loss Report\n" +
                            "4. Go Back to Main Menu");
                        int.TryParse(Console.ReadLine(), out choice);
                        switch (choice)
                        {
                            case 1:
                                {
                                    DAO.PendingPayments();
                                    AdminMenu(name);
                                    break;
                                }
                            case 2:
                                {
                                    DAO.AgentEfficiency();
                                    AdminMenu(name);
                                    break;
                                }
                            case 3:
                                {
                                    DAO.CustomerProfitLossReport();
                                    AdminMenu(name);
                                    break;
                                }
                            case 4:
                                {
                                    AdminMenu(name);
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invalid Choice Please try Again");
                                    Console.ReadLine();
                                    AdminMenu(name);
                                    break;
                                }

                        }
                        break;
                    }
                case 4:
                    {
                        Main();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Choice Please try Again");
                        AdminMenu(name);
                        break;
                    }
            }
        }

        public static string DrawInConsoleBox(this string s)
        {
            string ulCorner = "╔";
            string llCorner = "╚";
            string urCorner = "╗";
            string lrCorner = "╝";
            string vertical = "║";
            string horizontal = "═";

            string[] lines = s.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Black;

            int longest = 0;
            foreach (string line in lines)
            {
                if (line.Length > longest)
                    longest = line.Length;
            }
            int width = longest + 2; // 1 space on each side


            string h = string.Empty;
            for (int i = 0; i < width; i++)
                h += horizontal;

            // box top
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ulCorner + h + urCorner);

            // box contents
            foreach (string line in lines)
            {
                double dblSpaces = (((double)width - (double)line.Length) / (double)2);
                int iSpaces = Convert.ToInt32(dblSpaces);

                if (dblSpaces > iSpaces) // not an even amount of chars
                {
                    iSpaces += 1; // round up to next whole number
                }

                string beginSpacing = "";
                string endSpacing = "";
                for (int i = 0; i < iSpaces; i++)
                {
                    beginSpacing += " ";

                    if (!(iSpaces > dblSpaces && i == iSpaces - 1)) // if there is an extra space somewhere, it should be in the beginning
                    {
                        endSpacing += " ";
                    }
                }
                // add the text line to the box
                sb.AppendLine(vertical + beginSpacing + line + endSpacing + vertical);
            }

            // box bottom
            sb.AppendLine(llCorner + h + lrCorner);

            // the finished box
            return sb.ToString();
        }

        public static void WaitingAnnimation()
        {
            Console.Clear();
            Console.WriteLine("Loading");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading.");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading....");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading........");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading..............");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading....................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading..........................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading..............................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading...................................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading.............................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading......................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading................");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading...........");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading.....");
            Thread.Sleep(100);

            Console.Clear();
            Console.WriteLine("Loading..");
            Thread.Sleep(100);

            Console.Clear();

        }







    }
}
