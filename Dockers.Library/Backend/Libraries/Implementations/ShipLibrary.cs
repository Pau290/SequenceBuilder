using Dockers.Datasource;
using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Libraries
{
    public class ShipLibrary : IShipLibrary
    {
        readonly IShipDatasource shipDataSource;

        public ShipLibrary()
        {
            this.shipDataSource = new ShipDatasource();
        }

        public async Task<List<Ship>> GetShips()
        {
            var ships = await this.shipDataSource.GetShips();

            return ships;
        }
    }
}
