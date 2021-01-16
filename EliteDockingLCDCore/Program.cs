using EliteAPI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

using EliteDockingLCDCore;
using EliteDockingLCDCore.Modules;
using Valsom.Logging.PrettyConsole;
using Valsom.Logging.PrettyConsole.Formats;
using Valsom.Logging.PrettyConsole.Themes;

using System.Threading;
using System;
using System.Collections.Generic;
using EliteDockingLCDCore.LCD;


// Allow only one App instance using Mutex
using (var mutex = new Mutex(false, "nolantern.elitedockinglcd"))
{
    bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
    if (isAnotherInstanceOpen)
    {
        // Do something when another instance is already running and return.
        return;
    }
    // App entry point (after Mutex pattern)

    try
    {
        // Build the host for dependency injection
        var host = Host.CreateDefaultBuilder()

        .ConfigureLogging((context, logger) =>
        {
            logger.ClearProviders();
            logger.SetMinimumLevel(LogLevel.Error);
            //logger.AddPrettyConsole(ConsoleFormats.Default, ConsoleThemes.OneDarkPro);
        })


        .ConfigureServices((context, services) =>
        {
            services.AddEliteAPI(configuration =>
            {
                configuration.AddEventModule<LandingPadModule>();
                //configuration.AddEventModule<ChatModule>();
                //configuration.AddEventModule<StartypeModule>();
            });
        })

        .Build();
        // Do work async
        var coreTask = ActivatorUtilities.CreateInstance<Core>(host.Services).Run();
        var lcdTask = Task.Run( () => LCDController.InitLcdApp());
        var updateTask = VersionCheck.UpdateAvailabe();

        var taskList = new List<Task> { coreTask, lcdTask, updateTask };
        while (taskList.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(taskList);
            taskList.Remove(finishedTask);
        }
        
        if (await updateTask)
        {
            LCDController.ShowUpdateAvailable();
        }

        LCDController.AddTabChangers();
        
        await Task.Delay(-1);
    }

    finally
    {
        mutex.ReleaseMutex();
    }
}