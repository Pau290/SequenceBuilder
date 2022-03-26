using System;
using System.Collections.Generic;
using System.Text;

namespace Dockers.Library.DTOs
{
    public class ControlPoint
    {
        public ControlPoint()
        {
            this.Dockers = new List<Docker>();
            this.Ships = new List<Ship>();
        }
        public List<Docker> Dockers { get; }

        public List<Ship> Ships { get; set; }

        public override string ToString()
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.AppendLine($"Control Point: {Environment.NewLine}Dockers({this.Dockers.Count}):{Environment.NewLine}");

            this.Dockers.ForEach((docker) =>
            {
                sbuilder.AppendLine($"{docker}");
            });

            sbuilder.AppendLine($"{Environment.NewLine}Total Ships ({this.Ships.Count}):{Environment.NewLine}");

            this.Ships.ForEach((ship) =>
            {
                sbuilder.AppendLine($"{ship}");
            });

            return sbuilder.ToString();
        }
    }
}
