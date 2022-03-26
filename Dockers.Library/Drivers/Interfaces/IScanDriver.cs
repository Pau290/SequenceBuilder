using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public interface IScanDriver
    {
        Task ScanStore(ControlPoint controlPoint);
    }
}
