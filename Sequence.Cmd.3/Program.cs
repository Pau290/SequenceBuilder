using Dockers.Drivers;
using Dockers.Libraries;
using Dockers.Library.DTOs;
using Sequence.V2;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sequence.Cmd._2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TestUsability().ConfigureAwait(false);

            print("Main Task finished", ConsoleColor.DarkMagenta);

            Console.ReadKey();
        }

        static IControlPointLibrary library = new ControlPointLibrary();

        static IControlPointDriver driver = new ControlPointDriver();

        static Task TestUsability()
        {
            IAsyncFuncBuilder2<ControlPoint> controlBuilder = new AsyncFuncBuilder2<ControlPoint>();


            controlBuilder.RunInitialTask(async () => await library.GetControlPoint().ConfigureAwait(false));

            Task taskSetDockers = controlBuilder.GetTaskForSubject((subject) => driver.SetDockers(subject));

            Task taskLoadDocks = controlBuilder.GetTaskForSubject((subject) => driver.LoadDocks(subject));

            Task taskSetupDocks = controlBuilder.GetTaskForSubject((subject) => driver.SetupDocks(subject));

            Task taskInitExoAndroids = controlBuilder.GetTaskForSubject((subject) => driver.InitExoDrivers(subject));

            Task taskFreeDockers = controlBuilder.GetTaskForSubject((subject) => driver.FreeDockers(subject));

            Task taskRadar = controlBuilder.GetTaskForSubject((subject) => driver.InitRadar(subject));

            controlBuilder

                .AddActionTask(taskSetDockers, setDockers)

                .AddActionTask(taskLoadDocks, loadDocks)

                .AddActionTask(taskSetupDocks, setupDocks)

                .AddActionTask(taskInitExoAndroids, initExoAndroids)

                .AddActionTask(taskRadar, initTaskRadar)

                .AddActionTask(taskFreeDockers, freeDockers)

                    .RunWhen(loadDocks, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(setupDocks, loadDocks, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(initExoAndroids, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(freeDockers, setDockers, (task) => task.IsCompletedSuccessfully)

                ;


            controlBuilder.RunAllAndContinue();

            getInfoRadar(controlBuilder);

            controlBuilder.RunParalelTasks();

            print("Gates close in 2 seconds", ConsoleColor.DarkCyan);

            Thread.Sleep(2000);

            ControlPoint controlPoint = controlBuilder.GetSubject();

            print(controlPoint, ConsoleColor.White);

            print("Gates closed", ConsoleColor.DarkCyan);

            return Task.CompletedTask;
        }

        static void getInfoRadar(IAsyncFuncBuilder<ControlPoint> controlBuilder)
        {
            Thread.Sleep(8000);

            Task taskGetRadarInfo = controlBuilder.GetTaskForSubject((subject) => driver.InformShipsToControlPoint(subject));

            controlBuilder.AddParalelTask(taskGetRadarInfo, "get radar info");
        }


        static void print(object @object, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(@object);
        }


        static string setDockers => "set dockers";
        static string loadDocks => "load docks";
        static string setupDocks => "setup docks";
        static string initExoAndroids => "init exo androids";
        static string getShips => "get ships";
        static string freeDockers => "free dockers";
        static string initTaskRadar => "init radar";
    }
}
