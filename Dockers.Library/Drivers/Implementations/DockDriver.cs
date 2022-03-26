using Dockers.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class DockDriver : IDockDriver
    {
        IExoAndroidDriver androidDriver;

        public DockDriver()
        {
            androidDriver = new ExoAndroidDriver();
        }

        public Task FreeExoAndroid(ExoAndroid exoAndroid)
        {
            exoAndroid.Dock = null;

            exoAndroid.CarryGoods.Clear();

            return Task.CompletedTask;
        }

        public Task SetDocks(ControlPoint controlPoint)
        {
            controlPoint.Dockers.ForEach((docker) =>
            {
                Thread.Sleep(40);
                docker.Docks.AddRange(Enumerable.Range(1, this.getRandom(28, 38)).Select(getDock));
            });

            return Task.CompletedTask;
        }

        public async Task PresFreeButton(ExoAndroid android)
        {
            await this.androidDriver.PresFreeButton(android).ConfigureAwait(false);
        }

        public Task SetupDocks(ControlPoint controlPoint)
        {
            #region ↕ Bottle neck

            //controlPoint.Dockers.ForEach((docker) =>
            //{
            //    Thread.Sleep(80);

            //    docker.Docks.ForEach((dock) =>
            //    {
            //        Thread.Sleep(80);

            //        dock.IsReady(true);
            //    });
            //});

            #endregion

            #region ♣ Bottle neck out

            controlPoint.Dockers.ForEach((docker) =>
            {
                Thread.Sleep(80);

                Parallel.ForEach(docker.Docks,
                    (dock) =>
                    {
                        Thread.Sleep(80);

                        dock.IsReady(true);
                    });
            });

            #endregion


            return Task.CompletedTask;
        }


        public Task CloseDock(Dock dock)
        {
            Thread.Sleep(40);

            dock.IsReady(false);

            return Task.CompletedTask;
        }

        private Dock getDock(int i)
        {
            return new Dock(Guid.NewGuid(), i);
        }

        private int getRandom(int begin, int end)
        {
            Random random = new Random();

            return random.Next(begin, end);
        }

        private string getRandomName()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append("Android.");

            Enumerable.Range(1, 4).ToList()
                .ForEach(i =>
                {

                    Random random = new Random();

                    int rnd = random.Next(74, 240);

                    sbuilder.Append(char.ConvertFromUtf32(rnd));

                });

            return sbuilder.ToString();
        }
    }
}
