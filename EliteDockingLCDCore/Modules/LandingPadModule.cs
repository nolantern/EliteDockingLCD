using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteAPI.Abstractions;
using EliteAPI.Event.Attributes;
using EliteAPI.Event.Models;
using EliteAPI.Event.Module;
using Microsoft.Extensions.Logging;

using EliteDockingLCDCore.LCD;

namespace EliteDockingLCDCore.Modules
{
    public class LandingPadModule : EliteDangerousEventModule
    {
        private readonly ILogger<LandingPadModule> _log;



        /// <inheritdoc />
        public LandingPadModule(IEliteDangerousAPI api, ILogger<LandingPadModule> log) : base(api)
        {
            _log = log;
        }
            

        [EliteDangerousEvent]
        public void DockingGrantedEvent(DockingGrantedEvent e)
        {
            if (e.LandingPad > 0 && e.LandingPad <= 45 && (e.StationType.ToLower() == "coriolis" || e.StationType.ToLower() == "orbis" || e.StationType.ToLower() == "ocellus" || e.StationType.ToLower() == "asteroidbase"))
                LandingPad.Build(e.LandingPad);
        }

        [EliteDangerousEvent]
        public void DockedEvent(DockedEvent e)
        {
            LandingPad.Destroy();
        }

        [EliteDangerousEvent]
        public void DockingCancelledEvent(DockingCancelledEvent e)
        {
            LandingPad.Destroy();
        }

    }
}
