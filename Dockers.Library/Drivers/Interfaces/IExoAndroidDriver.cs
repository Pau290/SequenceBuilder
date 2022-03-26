using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface IExoAndroidDriver
    {
        /// <summary>
        /// Search for store
        /// </summary>
        /// <param name="android"></param>
        /// <returns></returns>
        Task<bool> SearchForStore(ExoAndroid android);

        /// <summary>
        /// Search for goods
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <returns></returns>
        Task SearchForGoods(ExoAndroid exoAndroid);

        /// <summary>
        /// Try reserve dock for exo android
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <param name="dock"></param>
        /// <returns></returns>
        Task TryReserveDock(ExoAndroid exoAndroid, Dock dock);


        /// <summary>
        /// Unloads the goods
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        Task UnloadGoods(ExoAndroid exoAndroid, List<Good> goods);

        /// <summary>
        /// Frees the docks
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        Task FreeDocks(ExoAndroid exoAndroid, List<Good> goods);

        /// <summary>
        /// Frees the docks sync
        /// </summary>
        /// <param name="exoAndroid"></param>
        /// <param name="goods"></param>
        void FreeDocksSync(ExoAndroid exoAndroid, List<Good> goods);

        /// <summary>
        /// Pres free button
        /// </summary>
        /// <param name="expAndroid"></param>
        /// <returns></returns>
        Task PresFreeButton(ExoAndroid expAndroid);
    }
}
