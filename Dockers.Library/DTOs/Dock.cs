using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dockers.Library.DTOs
{
    public class Dock
    {
        public Dock(Guid id, int index)
        {
            this.Id = id;
            this.Index = index;
            this.Description = this.getRandomName();

            this.WeightCapacity = this.getRandomWeightCapacity();

            this.GoodsHistorial = new List<Good>();
            this.InJobAndroids = new List<ExoAndroid>();
        }

        public int Index { get; set; }

        public string Description { get; set; }

        public ExoAndroid ExoAndroid { get; set; }

        public List<ExoAndroid> InJobAndroids { get; set; }

        public bool IsUnloading { get; set; }

        public bool IsBusy => this.ExoAndroid != null;

        public bool IsReserved { get; set; }

        public Guid Id { get; }

        public bool Ready { get; private set; }

        public void IsReady(bool ready)
        {
            this.Ready = ready;
        }

        public double WeightCapacity { get; set; }

        public List<Good> GoodsHistorial { get; set; }

        public double GoodsHistorialWeight => this.GoodsHistorial.Any() ? this.GoodsHistorial.Sum(good => good.Weight) : 0.0;

        public int OpenTimes => this.GoodsHistorial.Count;

        public override string ToString()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append($"Dock {this.Description}");

            if (this.IsBusy)
            {
                sbuilder.Append($" with Android {this.ExoAndroid.Name}");
            }

            if (this.IsUnloading)
            {
                sbuilder.AppendLine("Still unloading....");
            }

            if (this.Ready)
            {
                sbuilder.AppendLine("Ready");
            }

            this.InJobAndroids.ForEach(android =>
            {
                sbuilder.AppendLine($"----> {android.Name} was here");
            });

            sbuilder.AppendLine($"Collected for {this.InJobAndroids.Count} Androids");

            sbuilder.AppendLine($"-> Capacity {this.WeightCapacity}");            

            return sbuilder.ToString();
        }

        private string getRandomName()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append("dock.");

            Enumerable.Range(1, 7).ToList()
                .ForEach(i =>
                {

                    Random random = new Random();

                    int rnd = random.Next(44, 188);

                    sbuilder.Append(char.ConvertFromUtf32(rnd));

                });

            return sbuilder.ToString();
        }

        private double getRandomWeightCapacity()
        {
            Random random = new Random();

            int rnd = random.Next(1, 100);

            if (80 < rnd)
            {
                return 150000.0;
            }
            else if (60 < rnd)
            {
                return 200000.0;
            }
            else if (40 < rnd)
            {
                return 440000.0;
            }

            return 200000.0;
        }
    }
}
