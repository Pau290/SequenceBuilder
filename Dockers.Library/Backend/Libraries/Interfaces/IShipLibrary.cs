using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Libraries
{
    public interface IShipLibrary
    {
        public Task<List<Ship>> GetShips();
    }
}
