using Dockers.Drivers;
using Dockers.Libraries;
using Dockers.Library.DTOs;
using Sequence.Builder;
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

            for (int i = 0; i < 10; i++)
            {
                print("press any key to finish simulation", ConsoleColor.Blue);
            }            

            Console.ReadKey();
        }



        #region ↕ test

        static IControlPointLibrary library = new ControlPointLibrary();

        static IControlPointDriver driver = new ControlPointDriver();

        static Task TestUsability()
        {
            IAsyncFuncBuilder2<ControlPoint> builder = new AsyncFuncBuilder2<ControlPoint>();

            builder.RunInitialTask(async () => await library.GetControlPoint());

            builder

                .AddAction((subject) => driver.SetDockers(subject), setDockers)

                .AddAction((subject) => driver.LoadDocks(subject), loadDocks)

                .AddAction((subject) => driver.SetupDocks(subject), setupDocks)

                .AddAction((subject) => driver.InitExoAndroids(subject), initAndroids)

                .AddAction((subject) => driver.InitRadar(subject), initTaskRadar)

                .AddAction((subject) => driver.FreeDockers(subject), freeDockers)

                    .RunWhen(loadDocks, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(setupDocks, loadDocks, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(initAndroids, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(freeDockers, setDockers, (task) => task.IsCompletedSuccessfully)

                ;


            builder.RunAllAndContinue();


            getInfoRadar(builder);


            print("Gates close in 2 seconds", ConsoleColor.DarkMagenta);

            Thread.Sleep(2000);

            ControlPoint controlPoint = builder.GetSubject();

            print(controlPoint, ConsoleColor.White);

            print("Gates closed", ConsoleColor.DarkCyan);

            print("Begin set Context", ConsoleColor.White);

            driver.SetContext(builder.GetSubject());

            print("Begin auto Organize", ConsoleColor.Blue);

            driver.BeginAutoOrganizationContext();

            return Task.CompletedTask;
        }

        static void getInfoRadar(IAsyncFuncBuilder2<ControlPoint> controlBuilder)
        {
            Thread.Sleep(8000);

            Task taskGetRadarInfo = controlBuilder.GetTaskForSubjectV2((subject) => driver.InformShipsToControlPoint(subject));

            controlBuilder.RunTaskInline(taskGetRadarInfo);
        }
    
        static void print(object @object, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(@object);
        }

        static string setDockers => "set dockers";
        static string loadDocks => "load docks";
        static string setupDocks => "setup docks";
        static string initAndroids => "init androids";
        static string getShips => "get ships";
        static string freeDockers => "free dockers";
        static string initTaskRadar => "init radar";


        #endregion test
    }
}
