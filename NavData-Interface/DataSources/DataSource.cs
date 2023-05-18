using NavData_Interface.Objects.Fix;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.DataSources
{
    internal abstract class DataSource
    {

        public abstract List<TerminalWaypoint> GetTerminalWaypoints(string identifier);

        public abstract List<Waypoint> GetEnrouteWaypoints(string identifier);
    }
}
