using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace SalesM
{
    public partial class MyCellView : UITableViewCell
    {
        UILabel headingLabel, subheadingLabel;
        UIImageView imageView;

        public MyCellView() : base()
        {
        }

        public MyCellView(IntPtr handle) : base(handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            //ContentView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/CellBackground.png"));
            imageView = new UIImageView();

            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica Neue", 15f),
                TextColor = UIColor.FromRGB(255, 255, 255),
                BackgroundColor = UIColor.Clear
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("Palatino-Bold", 30f),
                TextColor = UIColor.FromRGB(16, 79, 84),
                TextAlignment = UITextAlignment.Right,
                BackgroundColor = UIColor.Clear
            };

            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel  });
        }

        public MyCellView (NSString cellId) : base (UITableViewCellStyle.Value1, cellId)
        {
        }
        public void UpdateCell(string subtitle, byte[] image) //string caption, UIImage image
        {
            subheadingLabel.Text = subtitle;
            if (image != null)
                imageView.Image = Helper.GetUIImage(image);
            //headingLabel.Text = caption;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imageView.Frame = new CGRect(ContentView.Bounds.Width - 120, 2, 60, 60);
            headingLabel.Frame = new CGRect(8, 15, ContentView.Bounds.Width - 63, ContentView.Bounds.Height/2);
            subheadingLabel.Frame = new CGRect(ContentView.Bounds.Width-105, 22, 100, ContentView.Bounds.Height/2);
        }
    }
}