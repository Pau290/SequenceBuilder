using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Datasource
{
    public interface IControlPointDataSource
    {
        Task<ControlPoint> GetControlPoint();
    }
}
