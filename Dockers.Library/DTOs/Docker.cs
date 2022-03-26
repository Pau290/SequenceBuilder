using Dockers.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dockers.Library.DTOs
{
    public class Docker
    {
        Docker()
        {
            this.InDockerShips = new List<Ship>();
            this.Docks = new List<Dock>();
            this.ExoAndroids = new List<ExoAndroid>();
        }

        public Docker(Guid id, string name, bool isAvailable) : this()
        {
            this.Id = id;
            this.Name = name;
            this.IsAvailable = isAvailable;
        }

        public Guid Id { get; }

        public string Name { get;  }

        public bool IsAvailable { get; private set; }

        public List<Ship> InDockerShips { get; }

        public List<ExoAndroid> ExoAndroids { get; }

        public List<Dock> Docks { get; }

        public void OpenDocker(object source)
        {
            if (object.ReferenceEquals(source, null))
            {
                throw new Exception("Unauthorized");
            }

            if (!source.GetType().Equals(typeof(DockerDriver)))
            {
                throw new Exception("Unauthorized");
            }

            this.IsAvailable = true;
        }

        public override string ToString()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.AppendLine($"Docker -> {this.Name}{Environment.NewLine}");
            sbuilder.AppendLine(this.IsAvailable ? "---> Available" : "<-- Not Available");
            sbuilder.AppendLine($"{Environment.NewLine} ---> Docks ({this.Docks.Count}) :{Environment.NewLine}");
            this.Docks.ForEach((dock) =>
            {
                sbuilder.AppendLine($"{dock}");
            });

            sbuilder.AppendLine($"---> Exo Androids ({this.ExoAndroids.Count}) :{Environment.NewLine}");

            this.ExoAndroids.ForEach((android) =>
            {
                sbuilder.AppendLine($"{android}");
            });

            sbuilder.AppendLine($"♣ {this.InDockerShips.Count} docked Ships");

            this.InDockerShips.ForEach((ship) =>
            {
                sbuilder.AppendLine($"{ship}");
            });
           
            return sbuilder.ToString();
        }
    }
}
