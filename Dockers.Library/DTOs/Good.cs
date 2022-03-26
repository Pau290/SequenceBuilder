using System;

namespace Dockers.Library.DTOs
{
    public class Good
    {
        public Good(Guid id, Guid shipId, double weight)
        {
            this.Id = id;
            this.ShipId = shipId;
            this.Weight = weight;
        }

        public Guid Id { get; }

        public Guid ShipId { get; }

        public double Weight { get; }

        public double Volume { get; set; }

        public bool IsWithAndroid { get; set; }

        public bool IsInDock { get; set; }

        public Guid DockId { get; set; }

        public string Android { get; set; }
    }
}
