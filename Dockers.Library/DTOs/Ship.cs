using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dockers.Library.DTOs
{
    public class Ship
    {
        public Ship(Guid id, string name, int index) 
        {
            this.Id = id;
            this.Name = name;
            this.Index = index;
            this.Goods = this.getGoods(this.Id);
        }

        public Guid Id { get; }

        public int Index { get; set; }

        public string Name { get; }

        public bool IsInDocker => this.DockerId.HasValue;

        public Guid? DockerId { get; set; }

        public List<Good> Goods { get; set; }

        public double GoodsWeight => (this.Goods.Any()) ? this.Goods.Sum(good => good.Weight) : 0.0;

        public bool HasGoodsForAndroids => this.Goods.Any() && this.Goods.Any(good => !good.IsWithAndroid);

        public override string ToString()
        {
            return $" ↓ Ship:{this.Name}..{this.Goods.Count} Goods";
        }

        private List<Good> getGoods(Guid shipId)
        {
            List<Good> goods = new List<Good>();

            Random random = new Random();

            int numsOfGoods = random.Next(8, 20);

            int weight = random.Next(4000, 6000);

            goods.AddRange(Enumerable
                .Range(1, numsOfGoods).Select((index) => getGood(shipId, weight)));

            return goods;
        }

        private Good getGood(Guid shipId, double weight)
        {
            return new Good(Guid.NewGuid(), shipId, weight);
        }
    }
}
