// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SalesM
{
    [Register ("ChagePasswordViewController")]
    partial class ChagePasswordViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel cErrLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField newPasswordBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField oldPasswordBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField rPasswordBox { get; set; }

        [Action ("UIButton4660_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton4660_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (cErrLbl != null) {
                cErrLbl.Dispose ();
                cErrLbl = null;
            }

            if (newPasswordBox != null) {
                newPasswordBox.Dispose ();
                newPasswordBox = null;
            }

            if (oldPasswordBox != null) {
                oldPasswordBox.Dispose ();
                oldPasswordBox = null;
            }

            if (rPasswordBox != null) {
                rPasswordBox.Dispose ();
                rPasswordBox = null;
            }
        }
    }
}