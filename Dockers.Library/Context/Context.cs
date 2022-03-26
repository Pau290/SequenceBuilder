using Dockers.Drivers;
using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Dockers.Library.DTOs
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public class Context
    {
        #region socket

        private static void InitSocket()
        {
            server = new WebSocketServer("ws://0.0.0.0:8181");

            server.Start(socket =>
            {
                socket.OnOpen = () => {
                    connections.Add(socket);
                    Console.WriteLine("Radar ON!");

                    setDocks();
                    setAndroids();
                };
                socket.OnClose = () => {
                    //connections.Remove(socket);
                    Console.WriteLine("Close connection");
                };

                socket.OnMessage = message => socket.Send(message);

                socket.OnError = (ex) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                };
            });

            Console.WriteLine("INIT THE RADAR");

            Thread.Sleep(1000);
        }


        static void setAndroidLoadGoods(int android, int goods, int ship)
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            var androidGoods = new
            {
                type = "load.goods",
                android,
                goods,
                ship
            };

            string order_android = JsonSerializer.Serialize(androidGoods);

            socket.Send(order_android);
        }


        static void setShips()
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];           

            Docker docker = ControlPoint.Dockers[0];


            var initShips = new
            {
                type = "init.ships",
                num = docker.InDockerShips.Count
            };

            string order_ships = JsonSerializer.Serialize(initShips);

            socket.Send(order_ships);
        }

        static void setAndroids()
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            Docker docker = ControlPoint.Dockers[0];


            var initAndroids = new
            {
                type = "init.androids",
                num = docker.ExoAndroids.Count
            };

            string order_androids = JsonSerializer.Serialize(initAndroids);

            socket.Send(order_androids);
        }

        static void setDocks()
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            Docker docker = ControlPoint.Dockers[0];


            var initDocks = new
            {
                type = "init.docks",
                num = docker.Docks.Count
            };

            string order_docks = JsonSerializer.Serialize(initDocks);

            socket.Send(order_docks);
        }

        static void androidToShip(int android, int ship)
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            var androidShip = new
            {
                android,
                ship
            };

            string order_android_ship = JsonSerializer.Serialize(androidShip);

            socket.Send(order_android_ship);
        }

        static void androidToSearch(int android)
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            var androidSearch = new
            {
                type = "android.search",
                android
            };

            string order_android_search = JsonSerializer.Serialize(androidSearch);

            socket.Send(order_android_search);
        }

        static void androidToDock(int android, int dock)
        {
            if (!connections.Any())
            {
                return;
            }

            var socket = connections[0];

            var androidDock = new
            {
                android,
                dock
            };

            string order_android_dock = JsonSerializer.Serialize(androidDock);

            socket.Send(order_android_dock);
        }


        static WebSocketServer server = null;

        static List<IWebSocketConnection> connections = new List<IWebSocketConnection>();

        #endregion socket

        #region control point

        #region ↕

        // More docks in use:
        // ------------- ▲ time free dock

        // Ships left goods
        // ------------- ▼ time begin dock ins


        #endregion

        /// <summary>
        /// Control Point with Dockers
        /// </summary>
        private static ControlPoint ControlPoint { get; set; }


        /// <summary>
        /// Drivers
        /// </summary>

        private static IExoAndroidDriver androidDriver;

        private static IDockDriver dockDriver;

        private static IScanDriver scaner;


        /// <summary>
        /// Set the Control Point
        /// </summary>
        /// <param name="controlPoint"></param>
        public static void SetContext(ControlPoint controlPoint)
        {
            ControlPoint = controlPoint;

            // init the socket

            InitSocket();
        }

        /// <summary>
        /// Timer to init incomes and dock ins
        /// </summary>
        static System.Timers.Timer timerInit;

        /// <summary>
        /// Timer to keep 
        /// </summary>
        static System.Timers.Timer timerKeep;

        //static System.Timers.Timer timerReport;

        public static void Start()
        {
            androidDriver = new ExoAndroidDriver();

            dockDriver = new DockDriver();

            scaner = new ScanDriver();

            timerInit = new System.Timers.Timer(10000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("START IN 10 seconds");

            timerInit.Elapsed += Timer_Init_Elapsed;
            timerInit.Enabled = true;
        }

        //private static void TimerReport_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    ReportAndroidsInSearchOfGood();
        //    //timerReport.Stop();
        //}

        private static void Timer_Init_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // stop search goods and store
            timerInit.Stop();

            // keep with report after n seconds
            timerKeep = new System.Timers.Timer(120 * 1000);
            timerKeep.Elapsed += TimerKeep_Elapsed;
            timerKeep.Enabled = true;

            // init incomes and organize androids
            // begin dock ins after n seconds
            Init();            
        }

        private static void TimerKeep_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("120 seconds iteration reached.....");

            Console.WriteLine(ControlPoint);

            timerKeep.Stop();


            Console.ForegroundColor = ConsoleColor.White;

            scaner.ScanStore(ControlPoint);

            Console.ReadKey();
        }

        /// <summary>
        /// Init
        /// </summary>
        private static void Init()
        {
            InitIncomes();

            setShips();

            OrganizeExoAndroids();

            Thread.Sleep(7000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("BEGIN DOCK INS");

            BeginDockins();
        }

        /// <summary>
        /// Init incomes
        /// </summary>
        private static void InitIncomes()
        {
            int totalShips = ControlPoint.Ships.Count;
            int totalDockers = ControlPoint.Dockers.Count;

            int acceptShips = totalShips / totalDockers;

            int skip = 0;

            ControlPoint.Dockers.ForEach(docker =>
            {
                List<Ship> acceptedShips = ControlPoint.Ships.Skip(skip).Take(acceptShips).ToList();

                acceptedShips.ForEach(ship => ship.DockerId = docker.Id);

                docker.InDockerShips.AddRange(acceptedShips);

                skip += acceptedShips.Count;
            });
        }

        /// <summary>
        /// Search for goods and store
        /// </summary>
        private static void OrganizeExoAndroids()
        {
            Dictionary<string, List<ExoAndroid>> docker_androids = 
                ControlPoint.Dockers
                .Select(d => new { key = d.Name, androids = d.ExoAndroids })
                .ToDictionary(c => c.key, c => c.androids);

            Parallel.ForEach(docker_androids,
                (docker_and_androids) =>
                {
                    SearchForGoods(docker_and_androids.Key, docker_and_androids.Value);

                    Thread.Sleep(1000);

                    SearchForStore(docker_and_androids.Key, docker_and_androids.Value);
                });
        }

        /// <summary>
        /// Reports androids still in search
        /// </summary>
        private static void ReportAndroidsInSearchOfGood()
        {          
            var still_in_search_androids = ControlPoint.Dockers
                .Select(d => new { key = d.Name, androids = d.ExoAndroids })

                .ToDictionary(c => c.key, c => c.androids)
                .Where(android => android.Value.Any(a => a.IsSearchingGoods || a.IsSearchingStore))

                ;

            if (still_in_search_androids.Any())
            {
                Parallel.ForEach(still_in_search_androids,
                    (docker_androids) =>
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"{docker_androids.Key} has {docker_androids.Value.Count} androids still in search");
                    });
            }
        }

        /// <summary>
        /// Builds a 3t tuple
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static Tuple<M, S, R> BuildTuple<M, S, R>(M m, S s, R r)
        {
            return new Tuple<M, S, R>(m, s, r);
        }


        /// <summary>
        /// Begin load the goods
        /// </summary>
        private static void BeginDockins()
        {
            var docker_androids_ships = ControlPoint.Dockers
                .Select(d => new { 
                    docker = d, 
                    androids = d.ExoAndroids
                                    .Where(android => ! android.ISearching)
                                    .ToList(), 
                    ships = d.InDockerShips 
                })
                .Select(d => BuildTuple(d.docker, d.androids, d.ships))
                .ToList();
            ;



            LoadTheGoods(docker_androids_ships);

        }


        /// <summary>
        /// Load the goods
        /// </summary>
        /// <param name="dockers"></param>
        private static void LoadTheGoods(List<Tuple<Docker, List<ExoAndroid> , List<Ship>>> dockers)
        {
            foreach (var docker in dockers)
            {
                DockerLoadsTheGoods(docker.Item1, docker.Item2, docker.Item3);
            }

            Parallel.ForEach(dockers,
                (docker) =>
                {
                    AndroidsBeginUnload(docker.Item2);
                });
        }


        /// <summary>
        /// Unload the goods in parallel
        /// </summary>
        /// <param name="androids"></param>
        private static void AndroidsBeginUnload(List<ExoAndroid> androids)
        {
            Parallel.ForEach(androids,
                (android) =>
                {
                    if (!android.CarryGoods.Any())
                    {
                        return;
                    }

                    if (android.HasDocks)
                    {
                        Console.WriteLine($"{android.Index} begins unload");
                        AndroidUnloads(android);
                        return;
                    }

                    List<Dock> docks_open = ControlPoint.Dockers
                            .First(docker => docker.Id == android.DockerId)
                            .Docks.Where(dock => !dock.IsBusy)
                            .ToList();
                    
                    Random random = new Random();

                    Thread.Sleep(random.Next(100, 300));

                    tryReserveDocks(android, docks_open);

                    if (android.HasDocks)
                    {                        
                        Console.WriteLine($"{android.Index} begins unload");
                        AndroidUnloads(android);
                    }
                });
        }



        /// <summary>
        /// Try reserve docks
        /// </summary>
        /// <param name="android"></param>
        /// <param name="docks_open"></param>
        private static void tryReserveDocks(ExoAndroid android, List<Dock> docks_open)
        {            
            double androidLoad = getLoad(android);

            Dock okDock = null;

            #region ↕

            // Race condition bug

            // ------------- ▲ Androids

            #endregion

            Random sleep = new Random();

            if (android.Index + 8 > docks_open.Count / 2)
            {
                Thread.Sleep(sleep.Next(80, 200));
                okDock = docks_open.Where(dock =>
                    (!dock.IsBusy && !dock.IsUnloading && !dock.IsReserved)
                    && dock.WeightCapacity >= android.WeightCapacity - androidLoad).LastOrDefault();
            }
            else
            {
                Thread.Sleep(sleep.Next(50, 150));
                okDock = docks_open.Where(dock =>
                    (!dock.IsBusy && !dock.IsUnloading && !dock.IsReserved)
                    && dock.WeightCapacity >= android.WeightCapacity - androidLoad).FirstOrDefault();
            }

            if (okDock == null)
            {
                return;
            }

            androidDriver.TryReserveDock(android, okDock);

            if (android.IsInDock && android.Dock.Id == okDock.Id)
            {
                if (android.InDocks.Count == 1)
                {
                    androidToDock(android.Index, android.Dock.Index);
                }

                double docksWeightCapacity = android.InDocks.Sum(dock => dock.WeightCapacity);

                if (android.CarryWeight > docksWeightCapacity)
                {
                    Random random = new Random();

                    Thread.Sleep(random.Next(100, 400));

                    List<Dock> _docks_open = ControlPoint.Dockers
                        .First(docker => docker.Id == android.DockerId)
                        .Docks.Where(dock => !dock.IsBusy)
                        .ToList();

                    tryReserveDocks(android, _docks_open);
                }
                else
                {
                    android.HasDocks = true;

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{android} has docks");
                }
            }
        }


        /// <summary>
        /// Android unload
        /// </summary>
        /// <param name="android"></param>
        private static void AndroidUnloads(ExoAndroid android)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{android} unloads {android.CarryGoods.Count} Goods");

            androidDriver.UnloadGoods(android, android.CarryGoods);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{android} unloaded");

            androidDriver.FreeDocks(android, android.CarryGoods);

            Console.ForegroundColor = ConsoleColor.Yellow;
            string docks = string.Join(',', android.InDocks.Select(dock => dock.Index));
            Console.WriteLine($"{android} frees the docks: {docks} ok");

            #region ↕

            //androidDriver.FreeDocksSync(android, android.CarryGoods);

            #endregion

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($">>>>>>>>>>>>>{android} begin press free button");

            dockDriver.PresFreeButton(android);

            //androidPresFreeButton(android.Index, android.InDocks.First().Index, android.CarryGoods.Count);
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($">>>>>>>>>>>>>{android} press free docks button ok");

            dockDriver.FreeExoAndroid(android);

            androidToSearch(android.Index);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Free the Android {android}");
        }

        /// <summary>
        /// Docker load the goods
        /// </summary>
        /// <param name="docker"></param>
        /// <param name="androids"></param>
        /// <param name="ships"></param>
        private static void DockerLoadsTheGoods(Docker docker, List<ExoAndroid> androids, List<Ship> ships)
        {
            androidsToShips(androids, ships);
        }

        private static void androidsToShips(List<ExoAndroid> androids, List<Ship> ships)
        {
            int totalAndroids = androids.Count;

            int totalShips = ships.Count;

            //int androidsPerShip = totalAndroids / totalShips;

            Parallel.ForEach(ships,
                (ship) =>
                {

                    ExoAndroid android = androids.FirstOrDefault(android => android.Index == ship.Index); 

                    if (android == null)
                    {
                        return;
                    }

                    androidsToShip(new List<ExoAndroid> { android }, ship);

                });

            #region ↕

            //foreach (Ship ship in ships)
            //{
            //    List<ExoAndroid> send_androids = androids.Where(a => !a.IsLoaded && !a.IsWithShip)
            //                .Skip(skip).Take(androidsPerShip).ToList();

            //    send_androids.ForEach(android => android.IsWithShip = true);

            //    androidsToShip(send_androids, ship);
            //}

            #endregion

        }

        private static void androidsToShip(List<ExoAndroid> androids, Ship ship)
        {
            foreach (ExoAndroid android in androids)
            {
                List<Good> take_goods = ship.Goods.Where(g => !g.IsWithAndroid).ToList();

                androidToShip(android.Index, ship.Index);

                Thread.Sleep(2000);

                takeGoodsUntilLoaded(android, take_goods, ship.Index);

                ship.Goods = ship.Goods.Except(android.CarryGoods).ToList();
            }
        }

        static Func<ExoAndroid, double> getLoad = (android) =>
            android.CarryGoods.Any()
            ? android.CarryGoods.Sum(good => good.Weight)
            : 0.0;

        /// <summary>
        /// take goods until loaded
        /// </summary>
        /// <param name="android"></param>
        /// <param name="goods"></param>
        private static void takeGoodsUntilLoaded(ExoAndroid android, List<Good> goods, int indexShip)
        {
            foreach (Good good in goods.Where(g => !g.IsWithAndroid))
            {
                double androidLoad = getLoad(android);

                if (android.WeightCapacity > androidLoad + good.Weight)
                {
                    android.CarryGoods.Add(good);
                    good.IsWithAndroid = true;
                    good.Android = android.Name;
                }

                androidLoad = getLoad(android);

                if (androidLoad + 6000 > android.WeightCapacity)
                {
                    setAndroidLoadGoods(android.Index, android.CarryGoods.Count, indexShip);
                    android.IsLoaded = true;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{android} already loaded with {androidLoad}");
                    break;
                }
            }
        }


        /// <summary>
        /// Search for goods
        /// </summary>
        /// <param name="dockerName"></param>
        /// <param name="androids"></param>
        private static void SearchForGoods(string dockerName, List<ExoAndroid> androids)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"...begin search goods for {dockerName}");

            Parallel.ForEach(androids,
                (android) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"..........{android.Name} search goods for {dockerName}");

                    androidDriver.SearchForGoods(android);
                });
        }


        /// <summary>
        /// Search for store
        /// </summary>
        /// <param name="dockerName"></param>
        /// <param name="androids"></param>
        private static void SearchForStore(string dockerName, List<ExoAndroid> androids)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($".................begin search store for {dockerName}");

            Parallel.ForEach(androids.Where(android => !android.IsSearchingGoods),
                (android) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"........................{android.Name} search store for {dockerName}");

                    androidDriver.SearchForStore(android);
                });
        }

        #endregion control point
    }
}
