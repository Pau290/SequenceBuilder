using Dockers.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class RadarDriver : IRadarDriver
    {
        static System.Timers.Timer timer;

        List<Ship> Ships;

        public Task Init(ControlPoint controlPoint)
        {
            timer = new System.Timers.Timer(1000);

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            return Task.CompletedTask;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.ScanForShips();

            Console.WriteLine($"Ships found: {Ships.Count}");
        }

        private void ScanForShips()
        {
            if (Ships == null)
            {
                Ships = new List<Ship>();
            }

            Random random = new Random();

            int rnd = random.Next(8, 15);

            Ships.AddRange(Enumerable.Range(1, rnd).Select(i =>

               new Ship(Guid.NewGuid(), this.getRandomName(), this.Ships.Count + 1)
            ));
        }

        public Task InformShipsToControlPoint(ControlPoint controlPoint)
        {
            controlPoint.Ships.AddRange(Ships);

            timer.Stop();

            return Task.CompletedTask;
        }


        private string getRandomName()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append("Ship.");

            Enumerable.Range(1, 7).ToList()
                .ForEach(i =>
            {

                Random random = new Random();

                int rnd = random.Next(1, 280);

                sbuilder.Append(char.ConvertFromUtf32(rnd));

            });

            return sbuilder.ToString();
        }

    }
}
