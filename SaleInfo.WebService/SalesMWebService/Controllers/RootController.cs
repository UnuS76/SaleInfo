using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using SalesMWebService.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using System.Security.Cryptography;
using SalesM;
using System.Xml;


namespace SalesMWebService.Controllers
{
    public class RootController : ApiController
    {
        public SqlConnection db_Connect()
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public List<DiscountModel> GetDiscounts()
        {
            List<DiscountModel> discountList = new List<DiscountModel>();
            try
            {
                var connection = db_Connect();

                connection.Open();
                
                SqlCommand queryCommand = connection.CreateCommand();
                queryCommand.CommandText = "select d.DiscountPercent, d.StartDate, d.EndDate, l.Lat, l.Long, l.Zoom, l.Address, c.CustomerName, c.Banner, c.Id FROM customers AS c LEFT JOIN discount AS d ON d.CustomerId = c.Id LEFT JOIN location AS l ON l.CustomerId = c.Id";
                SqlDataReader reader = queryCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                foreach (DataRow row in dataTable.Rows)
                {
                    discountList.Add(new DiscountModel()
                    {
                        CustomerName = (row["CustomerName"] == DBNull.Value) ? "" : row["CustomerName"].ToString(),
                        CustomerId = (row["Id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["Id"]),
                        Address = (row["Address"] == DBNull.Value) ? "" : row["Address"].ToString(),
                        Zoom = (row["Zoom"] == DBNull.Value) ? 0 : Convert.ToInt32(row["Zoom"]),
                        Lat = (row["Lat"] == DBNull.Value) ? "" : row["Lat"].ToString(),
                        Long = (row["Long"] == DBNull.Value) ? "" : row["Long"].ToString(),
                        EndDate = (row["EndDate"] == DBNull.Value) ? "" : row["EndDate"].ToString(),
                        StartDate = (row["StartDate"] == DBNull.Value) ? "" : row["StartDate"].ToString(),
                        DiscountPercent = (row["DiscountPercent"] == DBNull.Value) ? 0 : Convert.ToInt32(row["DiscountPercent"]),
                        Banner = (row["Banner"] == DBNull.Value) ? null : (byte[])row["Banner"],
                    });
                }
                connection.Close();
            }

            catch(Exception ex)
            {

            }

            return discountList;
        }

        public string GetUser(string username, string hashedPassword)
        {
            #region Code without store procedure
            /*
            string result = "False";
            var connection = db_Connect();
            List<Users> userList = new List<Users>();

            connection.Open();

            SqlCommand queryCommand = connection.CreateCommand();
            queryCommand.CommandText ="Select LoginName,Password FROM users WHERE LoginName=" + username + " AND Password=" + password + "";
            try
            {
                SqlDataReader reader = queryCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                    if (dataTable.Rows.Count == 1)
                        result = "True";
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid column name" + "'" + username + "'")
                {
                    result = "Wrong Username!";
                }
                else if (e.Message == "Invalid column name" + "'" + password + "'")
                {
                    result = "Wrong Passwrod";
                }
            }
*/
            #endregion 
            string result = "True";
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoginName", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                switch (userId)
                {
                    case -1:
                        result = "Bad_Credentials";
                        break;
                    case -2:
                        result = "Not_Active_Account";
                        break;
                    default:
                        result = "Success";
                        break;
                }
            }
            //connection.Close();

            return result;
        }

        public string RegisterUser(Users NewUser) // Thinking of changing public to protected.
        {
            //string result = "Failed";
            int userId = 0;
            var con = db_Connect();
            byte value = byte.MaxValue;
            
            using (SqlCommand cmd = new SqlCommand("Add_User"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoginName", NewUser.LoginName.Trim());
                    cmd.Parameters.AddWithValue("@Password", NewUser.Password.Trim());
                    cmd.Parameters.AddWithValue("@UserDiscount", ""); //
                    cmd.Parameters.AddWithValue("@FirstName", NewUser.FirstName.Trim());
                    cmd.Parameters.AddWithValue("@LastName", NewUser.LastName.Trim());
                    cmd.Parameters.AddWithValue("@Age", NewUser.Age);
                    cmd.Parameters.AddWithValue("@Address", NewUser.Address.Trim());
                    cmd.Parameters.AddWithValue("@Phone", NewUser.Phone.Trim());
                    cmd.Parameters.AddWithValue("@Email", NewUser.Email.Trim());
                    cmd.Parameters.AddWithValue("@DeviceOS", ""); 
                    cmd.Parameters.AddWithValue("@Avatar", value); //
                    cmd.Parameters.AddWithValue("@Favorites", NewUser.Favorites); //
                    cmd.Parameters.AddWithValue("@IsLoggedOn", 0); //
                    cmd.Parameters.AddWithValue("@IsAlive", 0); // 
                    cmd.Parameters.AddWithValue("@LastAccessDate", ""); //
                    cmd.Parameters.AddWithValue("@SessionExpTime", 0); //
                    
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            string message = string.Empty;
            switch (userId)
            {
                case -1:
                    message = "Username already exists.\\nPlease choose a different username.";
                    break;
                case -2:
                    message = "Supplied email address has already been used.";
                    break;
                default:
                    message = "Registration successful.\\nUser Id: " + userId.ToString();
                    break;
            }


            return message;
        }

        public string GetNewPassword(string username)
        {
            string targetEmail = string.Empty;
            string newPassword = Membership.GeneratePassword(6, 2);
            string oldHashedPassword = string.Empty;
            string newHashedPassword = Helper.GetStringedHash(newPassword);

            var connection = db_Connect();
            connection.Open();

            #region /* Retrieving the email , using provided username */ 
            SqlCommand queryCommand = connection.CreateCommand();
            queryCommand.CommandText = "Select Email,Password FROM users Where LoginName='" + username + "'";
            //targetEmail = queryCommand.ExecuteScalar().ToString();
            var reader = queryCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            foreach (DataRow row in dataTable.Rows)
            {
                targetEmail = row[0].ToString();
                oldHashedPassword = row[1].ToString();
            }

            #endregion

            #region /* Updating the users password with the new one */

            //TODO:: Just left to change the password value in DB , using ChangePassword function
            ChangePassword(username, oldHashedPassword, newHashedPassword);

            #endregion


            #region /* Sending the new password to the user */
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("armsaleinfo@gmail.com");
            mail.To.Add(targetEmail);
            mail.Subject = "New Password Request";
            //mail.Body = "This is for testing SMTP mail from GMAIL";
            mail.IsBodyHtml = true;

            string htmlBody;
            htmlBody = "<html>" + newPassword + "</html>";
            mail.Body = htmlBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("armsalesinfo@gmail.com", "2theCl0ud");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
#endregion


            return newPassword;
        }

        public string ChangePassword(string username, string hashedPassword, string newHashedPassword)
        {
            string result = "Successfully changed!";
            string targetEmail = string.Empty;

            var connection = db_Connect();
            connection.Open();

            #region /* Changing the password value with the new one */
            SqlCommand queryCommand = connection.CreateCommand();
            queryCommand.CommandText = "UPDATE users SET Password='" + newHashedPassword + "' WHERE LoginName='" + username + "'";
            targetEmail = queryCommand.ExecuteScalar().ToString();
            #endregion

            return result;
        }

        public void HelloWorld()
        {
            //Customers newCustomer = new Customers();

            //newCustomer.CustomerName = "Hamlet";
            //newCustomer.Login = "Hamlet";
            //newCustomer.Password = "Password";
            //newCustomer.Info = "Info";
            //newCustomer.Banner = byte.MaxValue;

            //this.AddCustomer(newCustomer);
        }

        public void incrementStoreClick(int CustomerId)
        {
            var connection = db_Connect();
            connection.Open();

            SqlCommand queryCommand = connection.CreateCommand();
            queryCommand.CommandText = "UPDATE [dbo].[statistics] SET CClickedCount=CClickedCount+1 WHERE CustomerId='" + CustomerId + "'";
            var queryResult = queryCommand.ExecuteScalar();

            connection.Close();
        }

        public string AddCustomer(Customers newCustomer)
        {
            string message = string.Empty;
            var connection = db_Connect();

            SqlCommand cmd = new SqlCommand("Add_Customer");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerName", newCustomer.CustomerName);
            cmd.Parameters.AddWithValue("@Login", newCustomer.Login);
            cmd.Parameters.AddWithValue("@Password", newCustomer.Password);
            cmd.Parameters.AddWithValue("@Info", newCustomer.Info);
            cmd.Parameters.AddWithValue("@Banner", newCustomer.Banner);
            

            cmd.Connection = connection;
            connection.Open();
            var id = cmd.ExecuteScalar();

            //Write a log about query execution.
            switch (Convert.ToInt32(id))
            {
                case -1:
                    message = "Customer Name already exists.Please choose a different username.";
                    break;
                case -2:
                    message = "Supplied Login Name address has already been used.";
                    break;
                default:
                    message = "Registration successful.User Id: " + id.ToString();
                    break;
            }

            connection.Close();
            return message;
        }

        public void AddMaccAddress(string MacAddress)
        {
            string message = string.Empty;
            var connection = db_Connect();


            SqlCommand cmd = new SqlCommand("Add_Visitor");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MacAddress", MacAddress);
            cmd.Connection = connection;
            connection.Open();
            var result = cmd.ExecuteScalar();

            //Write a log about query execution.
            if (result.ToString() == "Created")
                message = "User was added.";
            else if (result.ToString() == "Updated")
                message = "User was updated.";
            else
                message = "Something went wrong.";

            connection.Close();
        }

    }

}
