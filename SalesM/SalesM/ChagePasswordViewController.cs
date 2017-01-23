using System;
using SalesM.SalesMWebService_Global;
//using SalesM.SalesMWebService_Local;
using UIKit;

namespace SalesM
{
    public partial class ChagePasswordViewController : UIViewController
    {
        public ChagePasswordViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void UIButton4660_TouchUpInside(UIButton sender)
        {
            string Username = string.Empty;
            string oldPasswordHashed = Helper.GetStringedHash(oldPasswordBox.Text.Trim());
            string newPasswordHashed = Helper.GetStringedHash(newPasswordBox.Text.Trim());
            string rPassword = rPasswordBox.Text.Trim();

            if (rPasswordBox.Text.Trim() != rPasswordBox.Text.Trim())
                cErrLbl.Text = "Passwords do not match!";
            else
            {
                SalesWebService service = new SalesWebService();
                var result = service.ChangePassword(Username, oldPasswordHashed, oldPasswordHashed);
            }
            //throw new NotImplementedException();
        }
    }
}