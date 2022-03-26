using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface IRadarDriver
    {
        /// <summary>
        /// Inits the control point radar
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task Init(ControlPoint controlPoint);

        /// <summary>
        /// Sets the results <see cref="Ship"/> to the control point
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task InformShipsToControlPoint(ControlPoint controlPoint);
    }
}
