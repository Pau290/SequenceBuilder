using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Libraries
{
    public interface IControlPointLibrary
    {
        Task<ControlPoint> GetControlPoint();

        Task GetShips(ControlPoint controlPoint);
    }
}
