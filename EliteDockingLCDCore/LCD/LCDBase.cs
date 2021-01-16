using System.Collections.Generic;
using LogiFrame;

namespace EliteDockingLCDCore.LCD
{
    abstract class LCDBase : LCDTabPage
    {
        public List<LCDControl> items = new List<LCDControl>();

        protected LCDBase()
        {
            Init();
            items.ForEach(i => Controls.Add(i));
        }
        protected abstract void Init();
    }
}
