using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace SalesM
{
    public class TableSource : UITableViewSource
    {
        string[] TableItems;
        string CellIdentifier = "TableCell";

        public TableSource(string[] items)
        {
            TableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //string[] tableItems = new string[] { "TATEOSSIAN LONDON", "THOMPSON LONDON", "DOSE OF COLORS", "KAREN MILLEN ENGLANG" };
            //UIAlertController okAlertController = UIAlertController.Create("Row Selected", tableItems[indexPath.Row], UIAlertControllerStyle.Alert);
            //okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            //tableView.DeselectRow(indexPath, true);
            //owner.PresentViewController(okAlertController, true, null);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Row];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, CellIdentifier);
            }

            return cell;
        }
    }
}
