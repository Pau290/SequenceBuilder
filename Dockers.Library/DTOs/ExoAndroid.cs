using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Dockers.Library.DTOs
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public class ExoAndroid
    {
        ExoAndroid()
        {
            this.Id = Guid.NewGuid();
            this.IsSearchingGoods = true;
            this.IsSearchingStore = true;
            this.InDocks = new List<Dock>();
            this.LoadedGoods = new List<Good>();
            this.DockedGoods = new List<Good>();
            this.CarryGoods = new List<Good>();
            this.Name = this.getRandomName();
            this.WeightCapacity = this.getRandomWeightCapacity();
        }

        public ExoAndroid(Guid dockerId, int index) : this()
        {
            this.DockerId = dockerId;
            this.Index = index;
        }

        public int Index { get; set; }

        public bool IsLoaded { get; set; }

        public bool IsWithShip { get; set; }
        
        public Guid Id { get; }

        public string Name { get;  }

        public Guid DockerId { get; }

        public bool IsInDock => this.Dock != null;

        public bool IsSearchingStore { get; set; }

        public bool IsSearchingGoods { get; set; }

        public bool ISearching => this.IsSearchingGoods || this.IsSearchingStore;

        public Dock Dock { get; set; }

        public List<Dock> InDocks { get; }

        public List<Good> DockedGoods { get; set; }

        public List<Good> LoadedGoods { get; set; }

        public double LoadWeight => this.LoadedGoods.Sum(good => good.Weight);

        public List<Good> CarryGoods { get; set; }

        public double CarryWeight => this.CarryGoods.Sum(good => good.Weight);

        public double LoadedWeight => this.LoadedGoods.Sum(good => good.Weight);

        public double DockedWeight => this.DockedGoods.Sum(good => good.Weight);


        public Func<Guid, double> WeightInDock => (guid) =>
        {
            double weight = DockedGoods.Where(good => good.DockId == guid).Sum(good => good.Weight);

            return weight;
        };

        public bool HasDocks { get; set; }

        public double WeightCapacity { get; }

        public int Velocity { get; set; }

        public override string ToString()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.AppendLine($":[{this.Index}] Android {this.Name}");

            if (this.CarryGoods.Any())
            {
                sbuilder.AppendLine($"---> Is carryng {this.CarryGoods.Count} Goods with a load of {this.CarryWeight}");
            }

            if (this.LoadedGoods.Any())
            {
                sbuilder.AppendLine($"---> Has loaded {this.LoadedGoods.Count} Goods with a load of {this.LoadedWeight}");
            }

            if (this.DockedGoods.Any())
            {
                sbuilder.AppendLine($"---> Has docked {this.DockedGoods.Count} Goods with a load of {this.DockedWeight}");
            }

            sbuilder.AppendLine($">> Weight capacity: {this.WeightCapacity}");

            return sbuilder.ToString();
        }

        private string getRandomName()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append("Android.");

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
                return 20000.0;
            }
            else if (60 < rnd)
            {
                return 25000.0;
            }
            else if (40 < rnd)
            {
                return 40000.0;
            }

            return 20000.0;
        }
    }
}
