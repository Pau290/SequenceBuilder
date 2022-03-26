using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    /// <summary>
    /// Arrezife Software 2020
    /// </summary>
    public interface IControlPointDriver
    {

        /// <summary>
        /// Set dockers
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task SetDockers(ControlPoint controlPoint);

        /// <summary>
        /// Free dockers
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task FreeDockers(ControlPoint controlPoint);

        #region ↕ Docks

        /// <summary>
        /// Load docks
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task LoadDocks(ControlPoint controlPoint);

        /// <summary>
        /// Set up docks
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

        #region ↕ Radar

        /// <summary>
        /// Init radar
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task InitRadar(ControlPoint controlPoint);

        /// <summary>
        /// Set the result <see cref="Ship"/> to the control point
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        Task InformShipsToControlPoint(ControlPoint controlPoint);

        #endregion Radar    

        /// <summary>
        /// Set the Static Context
        /// </summary>
        /// <param name="controlPoint"></param>
        void SetContext(ControlPoint controlPoint);

        /// <summary>
        /// Begin auto organize
        /// </summary>
        /// <param name="controlPoint"></param>
        void BeginAutoOrganizationContext();

    }
}
