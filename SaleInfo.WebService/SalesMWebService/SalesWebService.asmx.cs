using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using SalesMWebService.Models;
using SalesMWebService.Controllers;
using SalesM;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace SalesMWebService
{
    /// <summary>
    /// Summary description for SalesWebService, version 1.3
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SalesWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public void HelloWorld()
        {
            var rootController = new RootController();

            rootController.HelloWorld();
        }

        [WebMethod]
        public List<DiscountModel> GetDiscounts()
        {
            var rootController = new RootController();
            var customers = rootController.GetDiscounts();

            return customers;
        }

        [WebMethod]
        public string LoginUser(string username, string password)
        {
            var rootController = new RootController();
            var result = rootController.GetUser(username, password);

            return result;
        }

        [WebMethod]
        public string AddUser(Users NewUser)
        {
            string result = "False";

            var rootController = new RootController();

            result = rootController.RegisterUser(NewUser);

            return result;
        }

        [WebMethod]
        public string GetNewPassword(string username)
        {
            string result = "False";

            var rootController = new RootController();
            result = rootController.GetNewPassword(username);

            return result;
        }

        [WebMethod]
        public string ChangePassword(string username, string password, string newPassword)
        {
            string result = "False";

            var rootController = new RootController();
            result = rootController.ChangePassword(username, password ,newPassword);

            return result;
        }

        [WebMethod]
        public void IncrementStoreClick(int CustomerId)
        {
            var rootController = new RootController();
            rootController.incrementStoreClick(CustomerId);
        }

        [WebMethod]
        public string AddCustomer(returning_XML.AddCustomerNewCustomer newCustomer)
        {
            string result = "The parameter is null";
            if (newCustomer == null)
                return result;

            var rootController = new RootController();
            Customers customer = new Customers();
            returning_XML.AddCustomerNewCustomer data = new returning_XML.AddCustomerNewCustomer();

            try
            {
                customer.CustomerName = newCustomer.CustomerName;
                customer.Login = newCustomer.Login;
                customer.Password = newCustomer.Password;
                customer.Info = newCustomer.Info;
                customer.Banner = newCustomer.Banner;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            result = rootController.AddCustomer(customer);

            return result;
        }

        [WebMethod]
        public void AddMaccAddress(string MacAddress)
        {
            var rootController = new RootController();
            rootController.AddMaccAddress(MacAddress);
        }
    }
}
