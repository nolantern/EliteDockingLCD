using System.Drawing;
using EliteDockingLCDCore.Properties;
using LogiFrame;
using LogiFrame.Drawing;

namespace EliteDockingLCDCore.LCD
{
    class LandingPad : LCDBase
    {
        private readonly Timer PadTimer = new Timer { Interval = 500, Enabled = true };
        private static readonly Point PicPoint = new Point(111, 0);

        private readonly LCDPicture lcdPicLights = new LCDPicture
        {
            Location = PicPoint,
            Size = Resources.pad_blinklights.Size,
            Image = Resources.pad_blinklights,
            MergeMethod = MergeMethods.Transparent,
        };
        private readonly LCDPicture lcdPicEmpty = new LCDPicture
        {
            Location = PicPoint,
            Size = Resources.pad_empty.Size,
            Image = Resources.pad_empty,
        };
        private readonly LCDLabel txtTitle = new LCDLabel
        {
            Location = new Point(1, LCDApp.DefaultSize.Height / 4),
            Font = PixelFonts.Title,
            Text = "Docking Pad Display",
            AutoSize = true,
        };
        private readonly LCDMarquee txtDescription = new LCDMarquee
        {
            Location = new Point(1, LCDApp.DefaultSize.Height / 2),
            Font = PixelFonts.Small,
            Text = "Blinking lights are the port side (red).",
            Size = new Size(108, 10),
            BreakAtEnd = false,
        };
        private readonly LCDPicture lcdPicPad = new LCDPicture
        {
            MergeMethod = MergeMethods.Transparent,
        };

        public LandingPad() : base()
        {
            // enable blink behavior
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

            MergeMethod = MergeMethods.Overlay;
        }

        public void Blink(long i)
        {
            var img = GetImg(i);
            lcdPicPad.Size = img.Size;
            lcdPicPad.Image = img;
            lcdPicPad.Location = PicPoint;

            // enable visibility
            Visible = true;
        }

        protected override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            PadTimer.Enabled = Visible;
        }

        protected override void Init()
        {
            items.Add(lcdPicEmpty);
            items.Add(lcdPicPad);
            items.Add(lcdPicLights);
            items.Add(txtTitle);
            items.Add(txtDescription);
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
                var n when n <= 45 => Resources.pad_40_44_45,
                _ => Resources.pad_empty,
            };
        }
    }
}
