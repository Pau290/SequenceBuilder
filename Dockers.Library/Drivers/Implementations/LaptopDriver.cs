using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    class LaptopDriver : ILaptopDriver
    {
        public Task FreeDocksForAndroid(ExoAndroid exoAndroid, List<Good> goods)
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

            return Task.CompletedTask;
        }
    }
}
