using System;

using UIKit;

namespace SalesM
{
    public partial class HelpViewController : UIViewController
    {
        public HelpViewController(IntPtr handle) : base(handle)
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
    }
}