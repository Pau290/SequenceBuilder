using Dockers.Drivers;
using Dockers.Libraries;
using Dockers.Library.DTOs;
using Sequence.Builder;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sequence.Cmd
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
            IAsyncFuncBuilder<ControlPoint> controlBuilder = new AsyncFuncBuilder<ControlPoint>();


            controlBuilder.RunInitialTask(async () => await library.GetControlPoint());   
            
            Task taskGetShips = controlBuilder.GetTaskForSubject((subject) => library.GetShips(subject));

            Task taskSetDockers = controlBuilder.GetTaskForSubject((subject) => driver.SetDockers(subject));

            Task taskLoadDocks = controlBuilder.GetTaskForSubject((subject) => driver.LoadDocks(subject));

            Task taskSetupDocks = controlBuilder.GetTaskForSubject((subject) => driver.SetupDocks(subject));

            Task taskInitExoAndroids = controlBuilder.GetTaskForSubject((subject) => driver.InitExoAndroids(subject));

            Task taskFreeDockers = controlBuilder.GetTaskForSubject((subject) => driver.FreeDockers(subject));

            controlBuilder

                .AddActionTask(taskGetShips, getShips)

                .AddActionTask(taskSetDockers, setDockers)

                .AddActionTask(taskLoadDocks, loadDocks)

                .AddActionTask(taskSetupDocks, setupDocks)

                .AddActionTask(taskInitExoAndroids, initExoAndroids)

                .AddActionTask(taskFreeDockers, freeDockers)

                    .RunWhen(loadDocks, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(setupDocks, loadDocks, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(initExoAndroids, setDockers, (task) => task.IsCompletedSuccessfully)

                    .RunWhen(freeDockers, setDockers, (task) => task.IsCompletedSuccessfully)

                ;


            controlBuilder.RunAllAndContinue();


            #region ↕ 

            // bottle neck -> setup docks

            #endregion

            int theGateClosesIn = 12000;

            print($"The gates close in {theGateClosesIn / 1000} seconds to finish",
                ConsoleColor.DarkRed);

            //IncomeShips(20, controlBuilder);

            controlBuilder.RunParalelTasks();

            Thread.Sleep(theGateClosesIn);

            ControlPoint controlPoint = controlBuilder.GetSubject();

            print(controlPoint, ConsoleColor.White);

            return Task.CompletedTask;
        }

        static void IncomeShips(int numOfShips, IAsyncFuncBuilder<ControlPoint> controlBuilder)
        {
            Parallel.For(1, numOfShips, (i) =>
            {
                Task taskShip = controlBuilder.GetTaskForSubject((subject) => library.GetShips(subject));

               // controlBuilder.AddParalelTask(taskShip, $"paralel task.{i}");
            });
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
    }
}
