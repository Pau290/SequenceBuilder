using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface IDockDriver
    {
        /// <summary>
        /// Free the exo android
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <returns></returns>
        Task FreeExoAndroid(ExoAndroid exoAndroid);        

        /// <summary>
        /// Set docks
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetDocks(ControlPoint controlPoint);

        /// <summary>
        /// Set up docks
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetupDocks(ControlPoint controlPoint);

        /// <summary>
        /// Pres free button
        /// </summary>
        /// <param name="android"></param>
        /// <returns></returns>
        Task PresFreeButton(ExoAndroid android);

        /// <summary>
        /// Close dock
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        Task CloseDock(Dock dock);
    }
}
