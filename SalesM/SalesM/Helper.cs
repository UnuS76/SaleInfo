using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using UIKit;
using Foundation;

namespace SalesM
{
    public class Helper
    {
        public static string GetStringedHash(string password)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);

            var sha512 = new SHA512Managed();
            var hashedNewPassword = sha512.ComputeHash(bytePassword);


            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hashedNewPassword.Length; i++)
            {
                result.Append(hashedNewPassword[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string GetDeviceMacAddress()
        {
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    var address = netInterface.GetPhysicalAddress();
                    return BitConverter.ToString(address.GetAddressBytes());

                }
            }

            return "NoMac";
        }

        public static UIImage GetUIImage(byte[] img)
        {
            //byte[] img = Tools.HexStringToBytes(vc);
            var data = NSData.FromArray(img);
            var uiimage = UIImage.LoadFromData(data);

            return uiimage;
        }
    }
}
