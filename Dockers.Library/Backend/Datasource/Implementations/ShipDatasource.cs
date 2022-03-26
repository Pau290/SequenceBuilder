using Dockers.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dockers.Datasource
{
    public class ShipDatasource : IShipDatasource
    {
        public Task<List<Ship>> GetShips()
        {
            Thread.Sleep(2000);

            List<Ship> ships = new List<Ship>();

            ships.AddRange(Enumerable.Range(1, 24).Select(getShip));

            return Task.FromResult(ships);
        }

        private Ship getShip(int index)
        {
            return new Ship(Guid.NewGuid(), $"  ↓ Ship.{index}", 0);
        }
    }
}
