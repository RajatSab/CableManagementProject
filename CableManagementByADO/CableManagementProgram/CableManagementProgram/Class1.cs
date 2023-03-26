using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableManagementProgram
{
    public static class DAO
    {


        public static void SignUp(string username, string password)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  
                SqlCommand cm = new SqlCommand($"insert into [dbo].Users (Username, Password) values ('{username}', '{password}')", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Record Inserted Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }


        public static bool LogIn(string username, string password)
        {
            string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
            string query = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // User authenticated successfully, retrieve the user details
                        reader.Read();
                        int userTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));

                        Console.WriteLine("Login for " + username + " successful" + "\n Press Enter to Continue.");
                        Console.ReadLine();
                        Program.UserMenu(username, userTypeId);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Check Credentials and try again");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                return false;
            }
        }


        public static void AddNewAgent()
        {
            int index = 0;
            string AgentName;
            do
            {
                Console.WriteLine("Enter Username:");
                AgentName = Console.ReadLine();
                index = DAO.CheckStringInColumn("Users", "Username", AgentName);
                if (index > 0)
                {
                    Console.WriteLine("Agent Name Already Exists. Please try Again...");
                }
            } while (index > 0);

            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  

                SqlCommand cm = new SqlCommand("EXEC [dbo].[AddNewAgent] " + AgentName, con);

                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Agent Added Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        public static void AddNewArea()
        {
            int index = 0;
            string AreaName;
            do
            {
                Console.WriteLine("Enter Name of Area");
                AreaName = Console.ReadLine();
                index = DAO.CheckStringInColumn("Users", "Username", AreaName);
                if (index > 0)
                {
                    Console.WriteLine("Area Name Already Exists. Please try Again...");
                }
            } while (index > 0);
            Console.WriteLine("Enter Name of Agent to be Assigned at "+AreaName);
            string AgentName = Console.ReadLine();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  

                SqlCommand cm = new SqlCommand("EXEC [dbo].[AddNewAgent] " + AgentName, con);

                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Area Added Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        public static void AddNewCustomer()
        {
            throw new NotImplementedException();
        }

        public static void AddNewPackage()
        {
            throw new NotImplementedException();
        }

        public static int CheckStringInColumn(string tableName, string columnName, string searchString)
        {
            int rowIndex = 0;
            string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CheckStringInColumn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@ColumnName", columnName);
                    command.Parameters.AddWithValue("@SearchString", searchString);
                    command.Parameters.Add("@RowIndex", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    rowIndex = Convert.ToInt32(command.Parameters["@RowIndex"].Value);
                }
            }

            return rowIndex;
        }

        public static void AddPayment()
        {
            Console.WriteLine("Please Enter Customer Id:");
            int customerId;
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine("Enter Month from 1 to 12:\n" +
                "MonthId\tMonthName\r\n1\tJanuary\r\n2\tFebruary\r\n3\tMarch\r\n4\tApril\r\n5\tMay\r\n6\tJune\r\n7\tJuly\r\n8\tAugust\r\n9\tSeptember\r\n10\tOctober\r\n11\tNovember\r\n12\tDecember");
            int monthId;
            int.TryParse(Console.ReadLine(), out monthId);
            int year;
            Console.WriteLine("Enter Year:");
            int.TryParse(Console.ReadLine(), out year);
            Console.WriteLine("Enter Select Plan:\n" +
                "PackageId\tPackageName\tRate\r\n1\tPlatinum\t500\r\n2\tGold\t300\r\n3\tSilver\t200");
            int planId;
            int.TryParse(Console.ReadLine(), out planId);
            Console.WriteLine("Choose Mode of Payment:\n" +
                "\n1\tCash\r\n2\tGoogle Pay\r\n3\tPhonepe\r\n4\tPaytm\r\n5\tDebit Card\r\n6\tCredit Card");
            int PaymentMode;
            int.TryParse(Console.ReadLine(), out PaymentMode);

            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  

                SqlCommand cm = new SqlCommand("EXEC InsertIntoRajatMonthlyCollection " + customerId + "," + monthId + "," + year + "," + planId + "," + PaymentMode, con);

                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Payment to Customer Id :" + customerId + " added Successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        public static void AgentEfficiency()
        {
            throw new NotImplementedException();
        }
        //Using Dapper Micro ORM Tool 
        //public static List<RajatComplaint> GetAllComplaintsByArea()
        //{
        //    Console.WriteLine("Unresolved Complaints in Your Area :");
        //    string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        List<RajatComplaint> categories = connection.Query<RajatComplaint>("select * from RajatComplaint").ToList();

        //        return categories;
        //    }

        //}

        public static void PendingPayments()
        {

        }

        public static void RaiseComplaint()
        {
            Console.WriteLine("Please Elaborate your Issue with D2H Service");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Customer Id:");
            int customerId;
            int.TryParse(Console.ReadLine(), out customerId);
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  

                SqlCommand cm = new SqlCommand("EXEC [dbo].[CustomerComplaint] " + customerId + ", '" + description + "'", con);

                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Complaint Raised Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        public static void ResolveComplaintByComplaintId()
        {
            Console.WriteLine("Enter Complaint Id:");
            int ComplaintId;
            int.TryParse(Console.ReadLine(), out ComplaintId);
            Console.WriteLine("Enter Agent Id:");
            int AgentId;
            int.TryParse(Console.ReadLine(), out AgentId);
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True");
                // writing sql query  

                SqlCommand cm = new SqlCommand("EXEC [dbo].[ResolveComplaint] " + ComplaintId + ", " + AgentId + "", con);

                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();
                // Displaying a message  
                Console.WriteLine("Complaint Resolved Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        internal static void ShowAllComplaintsInArea(int id)
        {
            string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ShowAllCasesInArea", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AreaId", id);

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", "Raised Date", "Complaint ID", "Description", "Customer ID", "First Name", "Area Name", "Area ID", "Resolved Status");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", reader["RaisedDate"], reader["ComplaintId"], reader["Description"], reader["CustomerId"], reader["FirstName"], reader["AreaName"], reader["AreaId"], reader["Resolved Status"]);
                }

                reader.Close();
            }
        }

        internal static void CustomerProfitLossReport()
        {
            string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("customerprofitlossreport", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Customer ID: {0}, Total Earnings: {1}, Total Expenses: {2}, Net Profit/Loss: {3}", reader["customerid"], reader["amt"], reader["Total_Expenses"], reader[3]);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        //public static List<RajatComplaint> ComplaintStatusById(int id)
        //{
        //    string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        List<RajatComplaint> complaints = connection.Query<RajatComplaint>("SELECT ComplaintId, Description, RaisedDate," +
        //            " ResolvedDate, CASE WHEN ResolvedDate IS NULL THEN 'Not Resolved' ELSE 'Resolved on ' + CONVERT(VARCHAR(10)," +
        //            " ResolvedDate, 103) END AS ResolvedStatus FROM RajatComplaint WHERE CustomerId" + id).ToList();

        //        return complaints;
        //    }
        //}
        //Using Dapper Micro ORM Tool
        //public static int GetCustomerIdByName(string name)
        //{

        //    string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        List<User> users = connection.Query<User>("select * from Users").ToList();
        //        foreach (User user in users)
        //        {
        //            if (user.Username.Contains(name))
        //            {
        //                return user.Id;
        //            }

        //        }
        //        return -1;
        //    }

        //}

        //public static List<RajatComplaint> ShowwAllComplaintsForId(int id)
        //{
        //    string connectionString = "Data Source=DESKTOP-NDO9FE4;Initial Catalog=Northwindm;Integrated Security=True";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        List<RajatComplaint> complaints = connection.Query<RajatComplaint>("select * from RajatComplaint where CustomerId = " + id).ToList();

        //        return complaints;
        //    }
        //}

    }
}
