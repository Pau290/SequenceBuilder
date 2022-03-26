using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Datasource
{
    public class ControlPointDataSource : IControlPointDataSource
    {
        Task<ControlPoint> IControlPointDataSource.GetControlPoint()
        {
            ControlPoint controlPoint = new ControlPoint();

            return Task.FromResult(controlPoint);
        }
    }
}
