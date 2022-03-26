using Dockers.Library.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface ILaptopDriver
    {
        /// <summary>
        /// Free Dock and set the good - doc id
        /// </summary>
        /// <param name="android"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        Task FreeDocksForAndroid(ExoAndroid android, List<Good> goods);
    }
}
