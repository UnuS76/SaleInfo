using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Mono;
using SidebarNavigation;
using System.Net.NetworkInformation;
using SalesM.SalesMWebService_Global;

namespace SalesM
{
    public partial class MasterViewController : UITableViewController
    {
        DataSource dataSource;

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Stores", "Stores");
        }

        public SalesMWebService_Global.DiscountModel[] GetDiscounts_FromDB()
        {
            SalesWebService salesWeb = new SalesWebService();
            var discounts = salesWeb.GetDiscounts();
            return discounts;
        }

        public override void ViewDidLoad()
        {
            /* Navigation bar transparency*/

            //this.NavigationController.NavigationBar.SetBackgroundImage(UIImage.FromFile("Images/NewDesign/Background.tif"), UIBarMetrics.Default);
            //this.NavigationController.View.BackgroundColor = UIColor.Clear;
            //this.NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
            //this.NavigationController.NavigationBar.ShadowImage = new UIImage();


            #region checking connectivity of the mobile device
            /* bool check = NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                //they have a connection we can use

            }
            else
            {
                return;
            }
            */
            #endregion

            var discounts = GetDiscounts_FromDB();
            TableView.RowHeight = 84; 
            List<TableDataModel> TableData = new List<TableDataModel>();
            base.ViewDidLoad();
            for (int i = 0; i < discounts.Length; i++)
            {
                TableData.Add(new TableDataModel()
                {
                    CustomerId = discounts[i].CustomerId,
                    CustomerName = discounts[i].CustomerName,
                    Address = discounts[i].Address,
                    Zoom = discounts[i].Zoom,
                    Lat = discounts[i].Lat,
                    Long = discounts[i].Long,
                    StartDate = discounts[i].StartDate,
                    EndDate = discounts[i].EndDate,
                    DiscountPercent = discounts[i].DiscountPercent + "%",
                    Banner =  discounts[i].Banner,
                });
            }
            //TableData.Add(new TableDataModel() { StoreName = "TATEOSSIAN LONDON", Discount = "15%", ImagePath = "Stores/1.png" });
            TableView.RegisterNibForCellReuse(UINib.FromName("MyCellView", null), "MyCellView"); // Cell Registration
            TableView.Source = dataSource = new DataSource(this);
            foreach (var objName in TableData)
            {               
                dataSource.Objects.Add(objName);
                using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                    TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            string deviceMacAddress = Helper.GetDeviceMacAddress();
            SalesWebService webService = new SalesWebService();
            webService.AddMaccAddress(deviceMacAddress);

            base.EdgesForExtendedLayout = UIRectEdge.None; //For table view to be on top of translucent navigation bar. 
            
            UIGraphics.BeginImageContext(this.View.Frame.Size);
            UIImage i = UIImage.FromFile("Images/NewDesign/Background.tif");
            i = i.Scale(this.View.Frame.Size);

            this.View.BackgroundColor = UIColor.FromPatternImage(i);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Objects[indexPath.Row];

                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("MyCellView");
            readonly List<TableDataModel> objects = new List<TableDataModel>();
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller)
            {
                this.controller = controller;
            }

            public IList<TableDataModel> Objects
            {
                get { return objects; }
            }

            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return objects.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                controller.PerformSegue("showDetail", indexPath);
                tableView.DeselectRow(indexPath, true);
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                //var discounts = controller.GetDiscounts_FromDB();
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath) as MyCellView; //Default Cell initialization.

                if (cell == null)
                {
                    cell = new MyCellView(CellIdentifier);
                }
                var item = objects[indexPath.Row];


                cell.UpdateCell(item.DiscountPercent.ToString(), item.Banner);
                //objects[indexPath.Row].CustomerName, 
                //UIImage.FromFile("Images/" + objects[indexPath.Row].ImagePath)); //Must take the image from DB.

                UIGraphics.BeginImageContext(cell.ContentView.Frame.Size);


                if (item.Banner != null)
                {
                    try
                    {
                        //UIImage i = UIImage.FromFile("Images/DemoBanner.png"); //Must be taken from DB for each cell.
                        UIImage i = Helper.GetUIImage(item.Banner); //Must be taken from DB for each cell.
                        i = i.Scale(cell.ContentView.Frame.Size);
                        cell.BackgroundColor = UIColor.FromPatternImage(i);
                        }
                    catch (Exception ex)
                    {

                    }
                }

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }
        }

        public class TableDataModel
        {
            public byte[] Banner { get; set; }
            public string DiscountPercent { get; set; }
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
}

