using System.Drawing;
using LogiFrame;
using LogiFrame.Drawing;

namespace EliteDockingLCDCore.LCD
{
    class MainTab : LCDBase
    {
        private readonly LCDLabel msg = new LCDLabel
        {
            Location = new Point(1, 25),
            Font = PixelFonts.Small,
            Text = "- standby -",
            AutoSize = true,
        };

        private Messages messageText;
        internal Messages MessageText
        {
            get => messageText;
            set
            {
                msg.Text = value switch
                {
                    Messages.update => "- Update Available -",
                    _ => "- standby -",
                };
                messageText = value;
            }
        }

        public MainTab() : base()
        {
            MergeMethod = MergeMethods.Transparent;
        }

        protected override void Init()
        {
            items.Clear();

            var title = new LCDLabel
            {
                Location = new Point(1, 5),
                Font = PixelFonts.Title,
                Text = "Elite Docking LCD",
                AutoSize = true,
            };

            items.Add(title);

            var description = new LCDLabel
            {
                Location = new Point(1, 15),
                Font = PixelFonts.Small,
                Text = "Shows Docking Pad Location",
                AutoSize = true,
            };

            items.Add(description);
            items.Add(msg);
        }
    }

    enum Messages
    {
        standby,
        update,
    }
}
