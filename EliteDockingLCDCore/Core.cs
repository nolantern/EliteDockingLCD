using System;
﻿using System.Diagnostics;
using System.Threading.Tasks;
using EliteAPI.Abstractions;
using Microsoft.Extensions.Logging;

namespace EliteDockingLCDCore
{
    // Core class of our application
    public class Core
    {
        private readonly IEliteDangerousApi _api;
        private readonly ILogger<Core> _log;

        public Core(ILogger<Core> log, IEliteDangerousApi api)
        {
            // Get our dependencies through dependency injection
            _log = log;
            _api = api;
        }

        public async Task Run()
        {
            Console.WriteLine("To use Elite Docking LCD keep this console window open or minimize it. Close it to exit.");

            DisableAutostartOnDebug();
            LCD.LCDController.InitLcdApp();

            /*
            // Log whenever we change the landing gear
            _api.Status.Gear.OnChange += (sender, isDeployed) =>
            {
                _log.LogInformation(isDeployed ? "Landing gear deployed" : "Landing gear retracted");
            };

            _api.Events.DockingGrantedEvent += (sender, e) =>
            {
                _log.LogInformation("Landing garanted Event XXX");
            };
            */
            

            // Start EliteAPI
            await _api.StartAsync();
        }

        [Conditional("DEBUG")]
        private void DisableAutostartOnDebug()
        {
            LCD.LCDController.AutoStart = false;
        }
    }
}