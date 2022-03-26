using Dockers.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class ScanDriver : IScanDriver
    {
        public Task ScanStore(ControlPoint controlPoint)
        {
            printDockers(controlPoint);

            return Task.CompletedTask;
        }

        private void printDockers(ControlPoint controlPoint)
        {
            printLine("<<DOCKERS>>");
            newLine();
            printLine("░░░░░░░░░▓░░░░░░░░░░░░░░░▓░░░░░░░░░░░░░░░░░░▓░░░░░░░░░");

            this.printAndroids(controlPoint);

            this.printDocks(controlPoint);

            newLine();
            newLine();
            printLine("░░░░░░░░░▓░░░░░░░░░░░░░░░▓░░░░░░░░░░░░░░░░░░▓░░░░░░░░░");
            printLine("::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            printLine("░░░░░░░░░▓░░░░░░░░░░░░░░░▓░░░░░░░░░░░░░░░░░░▓░░░░░░░░░");
        }

        private void printAndroids(ControlPoint controlPoint)
        {
            Dictionary<Docker, List<ExoAndroid>> docker_and_androids = controlPoint.Dockers.ToDictionary(d => d, d => d.ExoAndroids);

            foreach (var docker_androids in docker_and_androids)
            {
                Console.ForegroundColor = this.colors[new Random().Next(0, 4)];

                printLine($"▓▓▓░░░░░░░░░░░{docker_androids.Key.Name}░░░░░ with {docker_androids.Key.InDockerShips.Count} ships ░░░░░░▓▓▓");

                newLine();

                string ships = string.Join(" * ", docker_androids.Key.InDockerShips.Select(ship => ship.Name));

                newLine();

                foreach (ExoAndroid android
                    in docker_androids.Value
                        .Where(android => android.DockerId == docker_androids.Key.Id 
                            &&(android.DockedWeight > 0 || android.LoadedWeight > 0)))
                {
                    newLine();
                    print($"¥ {android.Name} >>>>>> for {docker_androids.Key.Name} ---->");

                    if (android.DockedWeight > 0)
                    {
                        newLine();
                        print($"├ Docked goods >> {android.DockedGoods.Count}");

                        newLine();

                        foreach (Good dockedGood in android.DockedGoods)
                        {
                            print(" ▀ ");
                        }

                        newLine();

                        print("In Docks:"); 

                        newLine();

                        string docks = string.Join($" ◙ {Environment.NewLine}", android.DockedGoods.Select(good => good.DockId.ToString()).Distinct());

                        printLine(docks);

                        newLine();

                        print($">> {android.DockedWeight} ts. <<");

                        newLine();
                    }
                    if (android.LoadedWeight > 0)
                    {
                        newLine();
                        print($"├ Loaded goods >> {android.LoadedGoods.Count}");

                        newLine();

                        foreach (Good loadedGood in android.LoadedGoods)
                        {
                            print(" ▀ ");
                        }

                        newLine();

                        print($">> {android.LoadedWeight} ts. <<");

                        newLine();
                    }
                }

                printLine($"▓▓▓░░░░░░░░░░░░░░░░░░░░░░▓▓▓");

                newLine();
                newLine();
            }
        }

        ConsoleColor[] colors = new ConsoleColor[] { ConsoleColor.DarkCyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Green };

        private void printDocks(ControlPoint controlPoint)
        {
            Dictionary<Docker, List<Dock>> dockers_and_docks = controlPoint.Dockers.ToDictionary(d => d, d => d.Docks);

            foreach (var docker_docks in dockers_and_docks)
            {
                Console.ForegroundColor = this.colors[new Random().Next(0, 3)];

                printLine($"▓▓▓░░░░░░░░░░░{docker_docks.Key.Name}░░░░░ with {docker_docks.Key.InDockerShips.Count} ships ░░░░░░▓▓▓");

                newLine();

                string ships = string.Join(" ▬ ", docker_docks.Key.InDockerShips.Select(ship => ship.Name));

                newLine();

                foreach (Dock dock in docker_docks.Value)
                {
                    printLine($"╠═════════╣");

                    printLine($"[{dock.Index}] Dock {dock.Description} collected for {dock.InJobAndroids.Count} androids");

                    foreach (ExoAndroid android in dock.InJobAndroids)
                    {
                        printLine($"¥ [{android.Index}] . {android.Name} docked here:: {android.DockedGoods.Count(good => good.DockId.Equals(dock.Id))}");

                        newLine();

                        foreach (var docked_good in android.DockedGoods.Where(good => good.DockId.Equals(dock.Id)))
                        {

                            print(" · ▀ · ");
                        }

                        newLine();

                        double dockedWeight = android.WeightInDock(dock.Id);

                        print($">> {dockedWeight} ts. <<");

                        

                        newLine();
                    }

                    printLine(".............");
                    print($"░░░░Docked a total of  : {dock.GoodsHistorial.Count}");
                    printLine(".............");
                    int line = 0;
                    foreach (Good good in dock.GoodsHistorial)
                    {
                        print($" ├ ▄ ┤ ");

                        if (line++ > 20)
                        {
                            line = 0;
                            newLine();
                        }
                    }
                    newLine();

                    print($">>>>>>>> {dock.GoodsHistorialWeight} ts. <<<<<<<<<");

                    print($">>>>>>> {dock.WeightCapacity} ts. <<<<<<<<");

                    newLine();

                    newLine();
                    printLine(".............");

                    printLine($"╠═════════╣");
                }

                printLine($"▓▓▓░░░░░░░░░░░░░░░░░░░░░░▓▓▓");
                newLine();
                newLine();
            }
        }

        #region ↕

        //Console.ForegroundColor = ConsoleColor.DarkGreen;

        //string not_in_docker_ships = string.Join(" -> ", ControlPoint.Ships.Where(ship => !ship.IsInDocker).Select(ship => ship.Name));

        //Console.ForegroundColor = ConsoleColor.DarkGray;
        //Console.WriteLine($":::: Ships still in wait: {not_in_docker_ships}");


        //var left_goods_data =
        //    ControlPoint.Dockers
        //        .Select(the_docker =>
        //            new
        //            {
        //                docker = the_docker,

        //                ships = the_docker.InDockerShips.Where(ship => ship.Goods.Any(good => !good.IsWithAndroid))
        //                    .Select(the_ship =>
        //                    new
        //                    {
        //                        ship = the_ship,
        //                        leftGoods = the_ship
        //                                        .Goods
        //                                        .Where(good => !good.IsWithAndroid)

        //                    })
        //            }

        //        )

        //    ;

        //var gs = new List<Good>();

        //int ls = left_goods_data.Select(d => d.ships.Sum(ship => ship.leftGoods.Count())).Count();

        //Enumerable.Range(1, ls).Select(d =>
        //    {
        //        gs.AddRange(left_goods_data.Select(d => d.ships.Select(ship => ship.leftGoods));
        //    }
        //));


        //Console.ForegroundColor = ConsoleColor.White;

        //Console.WriteLine("Left Goods In Docker Ships");

        //var goods =
        //    ControlPoint.Dockers
        //        .Select(the_docker =>
        //            new
        //            {
        //                docker = the_docker,

        //                ships = the_docker.InDockerShips.Where(ship => ship.Goods.Any(good => !good.IsWithAndroid))
        //                    .Select(the_ship =>
        //                    new
        //                    {
        //                        ship = the_ship,
        //                        leftGoods = the_ship
        //                                        .Goods
        //                                        .Where(good => !good.IsWithAndroid)
        //                    })
        //            }

        //        ).ToDictionary(d => d.docker, d => d.ships)

        //    ;


        ////foreach (var left_good_data in left_goods_data)
        ////{
        ////    Console.WriteLine(left_good_data.Key.Name);

        ////    if (left_good_data.Value != null && left_good_data.Value.Any())
        ////    {
        ////        foreach (var ship_and_goods in left_good_data.Value)
        ////        {
        ////            Console.WriteLine(ship_and_goods.ship);

        ////            Console.WriteLine($"{ship_and_goods.ship.Name}.Left {ship_and_goods.leftGoods.Count()} Goods");                        
        ////        }
        ////    }
        ////}

        //var dockers_androids = ControlPoint.Dockers
        //    .Select(docker => new { 
        //        key = docker,
        //        value = docker.ExoAndroids
        //    })
        //    .ToDictionary(d => d.key, d => d.value);

        #endregion



        private void newLine()
        {
            Console.WriteLine(Environment.NewLine);
        }

        private void printLine(string print)
        {
            Console.WriteLine(print);
        }

        private void print(string print)
        {
            Console.Write(print);
        }
    }
}
