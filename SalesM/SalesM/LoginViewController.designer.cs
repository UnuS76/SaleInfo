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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ErrorLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LoginNameBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordBox { get; set; }

        [Action ("UIButton2799_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2799_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton4030_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton4030_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ErrorLbl != null) {
                ErrorLbl.Dispose ();
                ErrorLbl = null;
            }

            if (LoginNameBox != null) {
                LoginNameBox.Dispose ();
                LoginNameBox = null;
            }

            if (PasswordBox != null) {
                PasswordBox.Dispose ();
                PasswordBox = null;
            }
        }
    }
}