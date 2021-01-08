using System;
using System.Drawing;
using LogiFrame;

namespace EliteDockingLCDCore.LCD
{
    abstract class LCDController
    {
        public static LCDApp App = new LCDApp("Elite Docking LCD", true, false, false);
        protected static LCDTabControl TabCtrl = new LCDTabControl();



         public static void InitLcdApp()
        {
            // A blocking call. Waits for the LCDApp instance to be disposed. (optional)
            // LCDApp.WaitForClose();
            App.ButtonDown += LCDApp_ButtonDown;

            TabCtrl.TabPages.Add(MainTab.Build());
            TabCtrl.SelectedTab = MainTab.Tab;
            App.Controls.Add(TabCtrl);
        }

        private static void TabCtrl_SelectedTabChanged(object sender, EventArgs e)
        {
            Console.Write($"selected tab = {TabCtrl.SelectedTab}");
        }

        private static void LCDApp_ButtonDown(object sender, ButtonEventArgs e)
        {

            switch (e.Button)
            {
                case 0:
                    LandingPad.Destroy();
                    break;
                case 2:
                    System.Environment.Exit(1);
                    break;
                default:
                    return;
            }
       }
    }
}
