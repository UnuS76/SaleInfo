using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

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

        public static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);

            return result;
        }

    }

    public class returning_XML
    {
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2003/05/soap-envelope", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public partial class EnvelopeBody
        {

            private AddCustomer addCustomerField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
            public AddCustomer AddCustomer
            {
                get
                {
                    return this.addCustomerField;
                }
                set
                {
                    this.addCustomerField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
        public partial class AddCustomer
        {

            private AddCustomerNewCustomer newCustomerField;

            /// <remarks/>
            public AddCustomerNewCustomer newCustomer
            {
                get
                {
                    return this.newCustomerField;
                }
                set
                {
                    this.newCustomerField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        public partial class AddCustomerNewCustomer
        {

            private object idField;

            private string customerNameField;

            private string loginField;

            private string passwordField;

            private object discountIdField;

            private string infoField;

            private object hasDiscountField;

            private byte[] bannerField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public object Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string CustomerName
            {
                get
                {
                    return this.customerNameField;
                }
                set
                {
                    this.customerNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string Login
            {
                get
                {
                    return this.loginField;
                }
                set
                {
                    this.loginField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string Password
            {
                get
                {
                    return this.passwordField;
                }
                set
                {
                    this.passwordField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public object DiscountId
            {
                get
                {
                    return this.discountIdField;
                }
                set
                {
                    this.discountIdField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public string Info
            {
                get
                {
                    return this.infoField;
                }
                set
                {
                    this.infoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public object HasDiscount
            {
                get
                {
                    return this.hasDiscountField;
                }
                set
                {
                    this.hasDiscountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public byte[] Banner
            {
                get
                {
                    return this.bannerField;
                }
                set
                {
                    this.bannerField = value;
                }
            }
        }
    }
}
