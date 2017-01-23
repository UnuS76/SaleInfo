using System;
using System.Text;
using Foundation;
using Mono;
using System.Security.Cryptography;
using SalesM.SalesMWebService_Global;
//using SalesM.SalesMWebService_Local;
using UIKit;



namespace SalesM
{
    public partial class SignUpViewController : UIViewController
    {
        public SignUpViewController(IntPtr handle) : base(handle)
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

            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void UIButton2803_TouchUpInside(UIButton sender)
        {
            string result = string.Empty;
            string Username = sLoginNameBox.Text.Trim();
            string Password = sPasswordBox.Text.Trim();
            string RepeatPasword = sRepeatPasswordBox.Text.Trim();

            var PasswordBytes = Encoding.UTF8.GetBytes(Password);

            var stringedPassword = Helper.GetStringedHash(Password);

            if (Password != RepeatPasword)
            {
                result = "Passwords do not match!";
                rErrorLbl.Text = result;
                //rPasswordBox.Layer.BorderColor = UIColor.Red.CGColor;
                //rPasswordBox.Layer.BorderWidth = 1f;
            }
            else
            {
                rErrorLbl.Text = "";
                SalesWebService service = new SalesWebService();
                
                Users newUser = new Users();
                newUser.LoginName = (Username != "") ? Username : "";
                newUser.Password = (stringedPassword != "") ? stringedPassword : "";
                newUser.LastName = "";
                newUser.FirstName = "";
                newUser.Age = 0;
                newUser.Address = "";
                newUser.Phone = "";
                newUser.Email = "";
                newUser.Avatar = byte.MinValue;
                newUser.Favorites = 0;

                result = service.AddUser(newUser);

            }

            //throw new NotImplementedException();
        }
    }
}