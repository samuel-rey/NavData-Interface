using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fix
{
    public class Waypoint : Fix
    {
        public string Area_code { get; }
        public string Icao_code { get; }

        // public WaypointType type { get; }
        // public WaypointUsage usage { get; }

        public Waypoint(string identifier, GeoPoint location, string area_code, string icao_code) : base(identifier, location)
        {
            Area_code = area_code;
        }
    }
}
