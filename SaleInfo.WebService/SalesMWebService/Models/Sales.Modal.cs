using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesMWebService.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int DiscountId { get; set; }
        public string Info { get; set; }
        public bool HasDiscount { get; set; }
        public byte[] Banner { get; set; }
    }

    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int DiscountPercent { get; set; }
        public string Token { get; set; }
        public bool IsDeleted { get; set; }
        public string DisCreateDate { get; set; }
        public string DisModifyDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Notes { get; set; }
    }

    public class Favotites
    {
        public int FavId { get; set; }
        public int CustomerId { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int Zoom { get; set; }
        public string Address { get; set; }
    }

    public class Settings
    {
        public int Id { get; set;  }
        public int CustomerId { get; set; }
        public string Font { get; set; }
        public string Logo { get; set; }
        public string Appearance { get; set; }
    }

    public class Statistics
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CPosition { get; set; }
        public int CClickedCount { get; set; }
        public string SyncDate { get; set; }
    }

    public class Users
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int UserDiscount { get; set; }
        public bool IsLoggedOn { get; set; }
        public bool isAlive { get; set; }
        public string LastAccessDate { get; set; }
        public int SessionExpTime { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte Avatar { get; set; }
        public int Favorites { get; set; }
        public string DeviceOS { get; set; }
    }

    public class Visitors
    {
        public int Visitor_Id;
        public string Visitor_Mac_Address;
        public string Last_Access; 
    }

    public class DiscountModel
    {
        public byte[] Banner { get; set; }
        public int DiscountPercent { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int Zoom { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

    }
}