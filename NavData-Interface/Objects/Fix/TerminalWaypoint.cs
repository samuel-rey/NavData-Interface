using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fix
{
    public class TerminalWaypoint : Waypoint
    {
        public string RegionCode { get; }

        public TerminalWaypoint(string identifier, GeoPoint location, string area_code, string icao_code,
            string regionCode) : base(identifier, location, area_code, icao_code)
        {
            RegionCode = regionCode;
        }
    }
}
