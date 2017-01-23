using System;
using UIKit;
using static SalesM.MasterViewController;
using SalesM.SalesMWebService_Global;
//using SalesM.SalesMWebService_Local;

namespace SalesM
{
    public partial class DetailViewController : UIViewController
    {
        public TableDataModel DetailItem { get; set; }

        public DetailViewController(IntPtr handle) : base(handle)
        {
        }

        public void SetDetailItem(TableDataModel newDetailItem)
        {
            if (DetailItem != newDetailItem)
            {
                DetailItem = newDetailItem;

                // Update the view
                ConfigureView();
            }
        }

        void ConfigureView()
        {
            // Update the user interface for the detail item
            if (IsViewLoaded && DetailItem != null)
                detailDescriptionLabel.Text = DetailItem.CustomerName;
            if (DetailItem.Banner != null)
                DetailImageContainer.Image = Helper.GetUIImage(DetailItem.Banner);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this.InvokeOnMainThread(delegate {
            //    this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Background.png"));
            //});


            UIGraphics.BeginImageContext(this.View.Frame.Size);
            UIImage i = new UIImage();

            i = UIImage.FromFile("Images/NewDesign/Background.tif");
            i = i.Scale(this.View.Frame.Size);

            this.View.BackgroundColor = UIColor.FromPatternImage(i);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
            SalesWebService webService = new SalesWebService();
            webService.IncrementStoreClick(DetailItem.CustomerId);
            //Trace user clicks here
            //Get the statistics table using the store name, and increment the CClicked count.

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


