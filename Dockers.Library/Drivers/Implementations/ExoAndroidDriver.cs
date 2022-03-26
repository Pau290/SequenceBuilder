using Dockers.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class ExoAndroidDriver : IExoAndroidDriver
    {
        readonly ILaptopDriver laptopDriver = new LaptopDriver();

        public Task TryReserveDock(ExoAndroid exoAndroid, Dock dock)
        {
            if (dock.IsBusy) 
            {
                dock.IsReserved = false;
                return Task.CompletedTask;
            }

            dock.IsReserved = true;
            exoAndroid.Dock = dock;
            exoAndroid.InDocks.Add(dock);
            dock.ExoAndroid = exoAndroid;
           
            return Task.CompletedTask;
        }


        public Task UnloadGoods(ExoAndroid exoAndroid, List<Good> goods)
        {
            exoAndroid.InDocks.ForEach(dock => {                                
                dock.IsUnloading = true;                
            });

            Thread.Sleep(600);
            
            return Task.CompletedTask;
        }

        public async Task FreeDocks(ExoAndroid exoAndroid, List<Good> goods)
        {
            await this.laptopDriver.FreeDocksForAndroid(exoAndroid, goods);
        }

        public void FreeDocksSync(ExoAndroid exoAndroid, List<Good> goods)
        {
            int numOfBusyDocks = exoAndroid.InDocks.Count;

            int numOfGoods = goods.Count;

            int numOfGoodsByDock = numOfGoods / numOfBusyDocks;

            
            exoAndroid.InDocks.ForEach(dock =>
            {
                dock.ExoAndroid = null;

                List<Good> dock_goods = new List<Good>();

                int skip = 0;

                foreach (Good good in goods.Where(good => !good.IsInDock))
                {
                    if (skip > numOfGoodsByDock)
                    {
                        continue;
                    }

                    good.DockId = dock.Id;
                    good.IsInDock = true;

                    dock_goods.Add(good);

                    skip++;
                }

                exoAndroid.DockedGoods.AddRange(dock_goods);
                exoAndroid.LoadedGoods.AddRange(dock_goods);
                dock.InJobAndroids.Add(exoAndroid);
                dock.GoodsHistorial.AddRange(dock_goods);
            });
        }

        public Task PresFreeButton(ExoAndroid exoAndroid)
        {
            Thread.Sleep(5000);

            exoAndroid.InDocks.ForEach(dock =>
            {
                dock.IsUnloading = false;

                dock.IsReserved = false;
                
            });

            return Task.CompletedTask;
        }

        public Task SearchForGoods(ExoAndroid android)
        {
            Random random = new Random();

            android.IsSearchingGoods = true;

            int rnd = random.Next(100, 200);

            Thread.Sleep(rnd);

            android.IsSearchingGoods = false;

            return Task.FromResult(true);
        }

        public Task<bool> SearchForStore(ExoAndroid android)
        {
            Random random = new Random();

            android.IsSearchingStore = true;

            int rnd = random.Next(400, 600);

            Thread.Sleep(rnd);

            android.IsSearchingStore = false;

            return Task.FromResult(true);
        }
    }
}
