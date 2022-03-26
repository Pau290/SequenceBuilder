using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface IDockerDriver
    {
        /// <summary>
        /// Open dockers
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task OpenDockers(ControlPoint controlPoint);        

        /// <summary>
        /// Set dockers to control point
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetDockersToControlPoint(ControlPoint controlPoint);

        #region ↕ Docks

        /// <summary>
        /// Set docks to control point
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetDocks(ControlPoint controlPoint);


        /// <summary>
        /// Set up the docks
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetupDocks(ControlPoint controlPoint);

        #endregion Docks

        #region ↕ ExoAndroids

        /// <summary>
        /// Init exo androids
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task InitExoAndroids(ControlPoint controlPoint);

        #endregion ExoAndroids
    }
}
