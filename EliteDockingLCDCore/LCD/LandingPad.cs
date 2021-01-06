using System.Drawing;
using LogiFrame;
using LogiFrame.Drawing;
using EliteDockingLCDCore.Properties;

namespace EliteDockingLCDCore.LCD
{
    class LandingPad : LCDController
    {

        private static Timer PadTimer = null;
        private static LCDTabPage BlinkTab = null;

        public static void Build(long i)
        {
            var picX = 111;
            var picY = 0;

            Bitmap padPic = GetImg(i);
            Bitmap lightsPic = Resources.pad_blinklights;

            var lcdPicPad = new LCDPicture
            {
                Location = new Point(picX, picY),
                Size = padPic.Size,
                Image = padPic,
                MergeMethod = MergeMethods.Transparent,
            };
            var lcdPicLights = new LCDPicture
            {
                Location = new Point(picX, picY),
                Size = lightsPic.Size,
                Image = lightsPic,
                MergeMethod = MergeMethods.Transparent,
            };
            var lcdPicEmpty = new LCDPicture
            {
                Location = new Point(picX, picY),
                Size = Resources.pad_empty.Size,
                Image = Resources.pad_empty,
            };

            var txtTitle = new LCDLabel
            {
                Location = new Point(1, LCDApp.DefaultSize.Height / 4),
                Font = PixelFonts.Title,
                Text = "Docking Pad Display",
                AutoSize = true,
            };
            var txtDescription = new LCDMarquee
            {
                Location = new Point(1, LCDApp.DefaultSize.Height / 2),
                Font = PixelFonts.Small,
                Text = "Blinking lights are the port side (red).",
                Size = new Size(108, 10),
                BreakAtEnd = false,
            };
            var txtHide = new LCDLabel
            {
                Location = new Point(6/*11*/, LCDApp.DefaultSize.Height - 8),
                Font = PixelFonts.Small,
                Text = "[Hide]",
                AutoSize = true,
            };
            var txtExit = new LCDLabel
            {
                Location = new Point(90, LCDApp.DefaultSize.Height - 8),
                Font = PixelFonts.Small,
                Text = "[Exit]",
                AutoSize = true,
            };

            BlinkTab = new LCDTabPage();

            BlinkTab.Controls.Add(lcdPicEmpty);
            BlinkTab.Controls.Add(lcdPicPad);
            BlinkTab.Controls.Add(lcdPicLights);
            BlinkTab.Controls.Add(txtTitle);
            BlinkTab.Controls.Add(txtDescription);
            BlinkTab.Controls.Add(txtHide);
            BlinkTab.Controls.Add(txtExit);

            PadTimer = new Timer { Interval = 500, Enabled = true };
            PadTimer.Tick += (sender, args) =>
            {
                if (lcdPicPad.Visible)
                {
                    lcdPicPad.Hide();
                    lcdPicLights.Hide();
                }

                else
                {
                    lcdPicPad.Show();
                    lcdPicLights.Show();
                }
            };



            if (TabCtrl.TabPages.Contains(BlinkTab))
            {
                App.PushToForeground();
                return;
            };

            TabCtrl.TabPages.Add(BlinkTab);
            TabCtrl.SelectedTab = BlinkTab;
            App.PushToForeground();
        }

        public static void Destroy()
        {
            if (PadTimer == null) return;
            PadTimer.Stop();
            PadTimer = null;
            TabCtrl.SelectedIndex = 0;
            TabCtrl.TabPages.Remove(BlinkTab);
            BlinkTab = null;
            App.PushToBackground();
        }

        private static Bitmap GetImg(long i)
        {
            return i switch
            {
                var n when n <= 2 => Resources.pad_01_02,
                var n when n <= 4 => Resources.pad_03_04,
                var n when n <= 6 => Resources.pad_05_06,
                var n when n <= 7 => Resources.pad_07,
                var n when n == 8 => Resources.pad_08_10,
                var n when n == 9 => Resources.pad_09,
                var n when n == 10 => Resources.pad_08_10,
                var n when n <= 12 => Resources.pad_11_12,
                var n when n <= 13 => Resources.pad_13,
                var n when n <= 15 => Resources.pad_14_15,
                var n when n <= 17 => Resources.pad_16_17,
                var n when n <= 19 => Resources.pad_18_19_23,
                var n when n <= 21 => Resources.pad_20_21,
                var n when n <= 22 => Resources.pad_22,
                var n when n == 23 => Resources.pad_18_19_23,
                var n when n <= 24 => Resources.pad_24,
                var n when n <= 25 => Resources.pad_25,
                var n when n <= 27 => Resources.pad_26_27,
                var n when n <= 28 => Resources.pad_28,
                var n when n <= 30 => Resources.pad_29_30_33_34,
                var n when n <= 32 => Resources.pad_31_32,
                var n when n <= 34 => Resources.pad_29_30_33_34,
                var n when n <= 36 => Resources.pad_35_36,
                var n when n <= 37 => Resources.pad_37,
                var n when n <= 38 => Resources.pad_38,
                var n when n <= 39 => Resources.pad_39,
                var n when n <= 40 => Resources.pad_40_44_45,
                var n when n <= 42 => Resources.pad_41_42,
                var n when n <= 43 => Resources.pad_43,
                var n when n <= 44 => Resources.pad_40_44_45,
                _ => Resources.pad_empty,
            };
        }
    }
}
