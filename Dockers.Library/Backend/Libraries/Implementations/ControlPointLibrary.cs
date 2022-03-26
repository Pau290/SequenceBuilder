using Dockers.Datasource;
using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Libraries
{
    public class ControlPointLibrary : IControlPointLibrary
    {
        readonly IControlPointDataSource controlPointDataSource;

        readonly IShipDatasource shipDatasource;

        public ControlPointLibrary()
        {
            this.controlPointDataSource = new ControlPointDataSource();
            this.shipDatasource = new ShipDatasource();
        }
        public async Task<ControlPoint> GetControlPoint()
        {
            var controlPoint = await this.controlPointDataSource.GetControlPoint().ConfigureAwait(false);

            return controlPoint;
        }

        public Task GetShips(ControlPoint controlPoint)
        {
            var ships = this.shipDatasource.GetShips().Result;

            controlPoint.Ships.AddRange(ships);

            return Task.CompletedTask;
        }
    }
}
