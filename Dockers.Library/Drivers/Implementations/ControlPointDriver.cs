using Dockers.Library.DTOs;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class ControlPointDriver : IControlPointDriver
    {
        IDockerDriver dockerDriver;

        IRadarDriver radarDriver;

        public ControlPointDriver()
        {
            this.dockerDriver = new DockerDriver();
            this.radarDriver = new RadarDriver();
        }


        public void SetContext(ControlPoint controlPoint)
        {
            Context.SetContext(controlPoint);
        }

        public void BeginAutoOrganizationContext()
        {
            Context.Start();
        }

        public async Task SetDockers(ControlPoint controlPoint)
        {
            await this.dockerDriver.SetDockersToControlPoint(controlPoint).ConfigureAwait(false);
        }

        public async Task FreeDockers(ControlPoint controlPoint)
        {
            await this.dockerDriver.OpenDockers(controlPoint).ConfigureAwait(false);
        }

        public async Task LoadDocks(ControlPoint controlPoint)
        {
            await this.dockerDriver.SetDocks(controlPoint);
        }

        public async Task SetupDocks(ControlPoint controlPoint)
        {
            await this.dockerDriver.SetupDocks(controlPoint).ConfigureAwait(false);
        }


        public async Task InitRadar(ControlPoint controlPoint)
        {
            await radarDriver.Init(controlPoint).ConfigureAwait(false);
        }

        public async Task InformShipsToControlPoint(ControlPoint controlPoint)
        {
            await radarDriver.InformShipsToControlPoint(controlPoint).ConfigureAwait(false);
        }

        public async Task InitExoAndroids(ControlPoint controlPoint)
        {
            await this.dockerDriver.InitExoAndroids(controlPoint).ConfigureAwait(false);
        }
    }
}
