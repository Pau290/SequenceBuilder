using Dockers.Library.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dockers.Drivers
{
    public class DockerDriver : IDockerDriver
    {
        readonly IDockDriver dockDriver;

        public DockerDriver()
        {
            this.dockDriver = new DockDriver();
        }

        public Task OpenDockers(ControlPoint controlPoint)
        {
            controlPoint.Dockers.ForEach((docker) =>
            {
                Thread.Sleep(20);
                docker.OpenDocker(this);
            });


            return Task.CompletedTask;
        }

        public Task SetDockersToControlPoint(ControlPoint controlPoint)
        {
            Thread.Sleep(400);

            controlPoint.Dockers.AddRange(Enumerable.Range(1, 1 /*this.getRandom(4, 8)*/).Select(getDocker));

            return Task.CompletedTask;
        }

        #region ↕ Docks

        public async Task SetDocks(ControlPoint controlPoint)
        {
            await this.dockDriver.SetDocks(controlPoint).ConfigureAwait(false);
        }


        public async Task SetupDocks(ControlPoint controlPoint)
        {
            await this.dockDriver.SetupDocks(controlPoint).ConfigureAwait(false);
        }

        #endregion Docks

        #region ↕ ExoAndroids

        public Task InitExoAndroids(ControlPoint controlPoint)
        {
            Thread.Sleep(600);

            controlPoint.Dockers.ForEach((docker) =>
            {
                docker.ExoAndroids.AddRange(Enumerable.Range(1, this.getRandom(68, 88))
                    .Select((android) => this.getExoAndroid(docker.Id, android)));
                 
            });

            return Task.CompletedTask;
        }

        #endregion ExoAndroids

        private Docker getDocker(int i)
        {
            return new Docker(Guid.NewGuid(), $"Docker.{i}", false);
        }


        private ExoAndroid getExoAndroid(Guid dockerId, int index)
        {
            return new ExoAndroid(dockerId, index);
        }


        private int getRandom(int begin, int end)
        {
            Random random = new Random();

            return random.Next(begin, end);
        }
    }
}
