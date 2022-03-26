using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Datasource
{
    public interface IShipDatasource
    {
        Task<List<Ship>> GetShips();
    }
}
