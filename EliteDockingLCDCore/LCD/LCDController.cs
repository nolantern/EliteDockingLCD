using System;
using System.Drawing;
using LogiFrame;

namespace EliteDockingLCDCore.LCD
{
    abstract class LCDController
    {
        public static LCDApp App = new LCDApp("Elite Docking LCD", AutoStart, false, false);
        public static bool AutoStart = true;

        protected static LCDTabControl TabCtrl = new LCDTabControl();
        private static readonly MainTab main = new MainTab();
        private static readonly LandingPad blinky = new LandingPad();

        private static readonly LCDLabel txtHide = new LCDLabel
        {
            Location = new Point(6, LCDApp.DefaultSize.Height - 8),
            Font = PixelFonts.Small,
            Text = "[Hide]",
            AutoSize = true,
        };
        private static readonly LCDLabel txtExit = new LCDLabel
        {
            Location = new Point(90, LCDApp.DefaultSize.Height - 8),
            Font = PixelFonts.Small,
            Text = "[Exit]",
            AutoSize = true,
        };
        private static readonly LCDLabel txtUpdate = new LCDLabel
        {
            Location = new Point(40, LCDApp.DefaultSize.Height - 8),
            Font = PixelFonts.Small,
            Text = "[Update]",
            AutoSize = true,
            Visible = false,
        };

        public static void InitLcdApp()
        {
            // Only on Debug
            if (!AutoStart)
                App.PushToForeground();

            // Add TabControl to App
            TabCtrl.TabPages.ItemAdded += (o, e) =>
            {
                TabCtrl.SelectedIndex = TabCtrl.TabPages.Count - 1;
            };
            TabCtrl.TabPages.ItemRemoved += (o, e) =>
            {
                TabCtrl.SelectedIndex = TabCtrl.TabPages.Count - 1;
                if (TabCtrl.SelectedTab == main)
                {
                    App.PushToBackground();
                }
            };
            TabCtrl.TabPages.Add(main);
            App.Controls.Add(TabCtrl);

            // Add Buttons
            App.Controls.Add(txtHide);
            App.Controls.Add(txtExit);
            App.ButtonPress += App_ButtonPress;

        }

        private static void App_ButtonPress(object sender, ButtonEventArgs e)
        {
            switch (e.Button)
            {
                case 0:
                    App.PushToBackground();
                    break;
                case 1:
                    break;
                case 2:
                    Environment.Exit(1);
                    break;
                case 3:
                    break;
            }
        }
        public static void Blink(long i)
        {
            blinky.Blink(i);
            TabCtrl.TabPages.Add(blinky);
            App.PushToForeground();
        }

        public static void UnBlink()
        {
            TabCtrl.TabPages.Remove(blinky);
        }
    }
}
