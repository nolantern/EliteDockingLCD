using System.Drawing;
using LogiFrame;

namespace EliteDockingLCDCore.LCD
{
    class MainTab : LCDController
    {
        public static LCDTabPage Tab = null;

        public static LCDTabPage Build()
        {
            
            if (Tab == null)
            {
                var title = new LCDLabel
                {
                    Location = new Point(1, 5),
                    Font = PixelFonts.Title,
                    Text = "Elite Docking LCD",
                    AutoSize = true,
                };
                var description = new LCDLabel
                {
                    Location = new Point(1, 15),
                    Font = PixelFonts.Small,
                    Text = "Shows Docking Pad Location",
                    AutoSize = true,
                };
                var msg = new LCDLabel
                {
                    Location = new Point(1, 25),
                    Font = PixelFonts.Small,
                    Text = "- standby -",
                    AutoSize = true,
                };

                var txtExit = new LCDLabel
                {
                    Location = new Point(90, LCDApp.DefaultSize.Height - 8),
                    Font = PixelFonts.Small,
                    Text = "[Exit]",
                    AutoSize = true,
                };

                Tab = new LCDTabPage();
                Tab.Controls.Add(title);
                Tab.Controls.Add(description);
                Tab.Controls.Add(msg);
                Tab.Controls.Add(txtExit);
            }
            return Tab;
        }
    }
}
