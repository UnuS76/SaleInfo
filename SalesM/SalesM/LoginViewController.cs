using Foundation;
using Mono;
using System;
using System.Security.Cryptography;
using UIKit;
using System.Text;
using SalesM.SalesMWebService_Global;
//using SalesM.SalesMWebService_Local;

namespace SalesM
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController (IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            UIGraphics.BeginImageContext(this.View.Frame.Size);
            UIImage i = UIImage.FromFile("Images/NewDesign/Background.tif");
            i = i.Scale(this.View.Frame.Size);

            this.View.BackgroundColor = UIColor.FromPatternImage(i);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        partial void UIButton2799_TouchUpInside(UIButton sender)
        {
            #region  Hiding the keyboard

            LoginNameBox.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            PasswordBox.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            #endregion

            base.ShouldPerformSegue("showAccountMenu", sender);
            //throw new NotImplementedException();
        }

        /* Password renewal Button */
        partial void UIButton4030_TouchUpInside(UIButton sender)
        {
            //Handle the password renewal proccess.
            string username = LoginNameBox.Text.Trim();
            SalesWebService service = new SalesWebService();
            var result = service.GetNewPassword(username);


            //throw new NotImplementedException();
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            string LoginName = LoginNameBox.Text.Trim();
            var Password = PasswordBox.Text;
            var result = false;

            var stringedPassword = Helper.GetStringedHash(Password);

            SalesWebService service = new SalesWebService();
            var response = service.LoginUser(LoginName, stringedPassword);

            switch (response)
            {
                case "Success":
                    {

                        result = true;
                    }
                    break;
                case "Bad_Credentials":
                    {
                        ErrorLbl.Text = "Username and / or password is incorrect.";
                        LoginNameBox.Layer.BorderColor = UIColor.Red.CGColor;
                        LoginNameBox.Layer.BorderWidth = 1f;
                        PasswordBox.Layer.BorderColor = UIColor.Red.CGColor;
                        PasswordBox.Layer.BorderWidth = 1f;
                        result = false;
                    }
                    break;
                case "Not_Active_Account":
                    {
                        ErrorLbl.Text = "Account has not been activated.";
                        result = false;
                    }
                    break;
            }

            return result;
            //return base.ShouldPerformSegue(segueIdentifier, sender);
        }
    }
}