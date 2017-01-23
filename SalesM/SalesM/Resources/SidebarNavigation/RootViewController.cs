﻿using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using SidebarNavigation;
namespace SalesM
{
    public partial class RootViewController : UIViewController
    {
        private UIStoryboard _storyboard;


        // the sidebar controller for the app
        public SidebarNavigation.SidebarController SidebarController { get; private set; }

        // the navigation controller
        public NavController NavController { get; private set; }

        // the storyboard
        public override UIStoryboard Storyboard
        {
            get
            {
                if (_storyboard == null)
                    _storyboard = UIStoryboard.FromName("Main", null);
                return _storyboard;
            }
        }

        public RootViewController() : base(null, null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //var introController = (MasterViewController)Storyboard.InstantiateViewController("MasterViewController");
            //var menuController = (SideMenuController)Storyboard.InstantiateViewController("SideMenuController");

            // create a slideout navigation controller with the top navigation controller and the menu view controller
            //NavController = new NavController();
            //NavController.PushViewController(introController, false);
            //SidebarController = new SidebarNavigation.SidebarController(this, NavController, menuController);
            SidebarController = new SidebarController(this, new NavController(), new SideMenuController());
            SidebarController.MenuWidth = 220;
            SidebarController.ReopenOnRotate = false;


        }
    }
}
