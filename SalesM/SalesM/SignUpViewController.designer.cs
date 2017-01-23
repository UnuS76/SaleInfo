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
    [Register ("SignUpViewController")]
    partial class SignUpViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel rErrorLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField sLoginNameBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField sPasswordBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField sRepeatPasswordBox { get; set; }

        [Action ("UIButton2803_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2803_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (rErrorLbl != null) {
                rErrorLbl.Dispose ();
                rErrorLbl = null;
            }

            if (sLoginNameBox != null) {
                sLoginNameBox.Dispose ();
                sLoginNameBox = null;
            }

            if (sPasswordBox != null) {
                sPasswordBox.Dispose ();
                sPasswordBox = null;
            }

            if (sRepeatPasswordBox != null) {
                sRepeatPasswordBox.Dispose ();
                sRepeatPasswordBox = null;
            }
        }
    }
}