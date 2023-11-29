﻿using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fixes.Waypoints
{
    public class Waypoint : Fix
    {
        public string Area_code { get; }
        public string Icao_code { get; }

        public bool IsTerminal { get; }

        public string Region_code { get; }

        // public WaypointType type { get; }

        public Waypoint(string identifier, string name, GeoPoint location, string area_code, string icao_code) : this(identifier, name, location, area_code, icao_code, "") { }

        public Waypoint(string identifier, string name, GeoPoint location, string area_code, string icao_code, string region_code) : base(identifier, name, location)
        {
            Area_code = area_code;
            Icao_code = icao_code;

            if (region_code == "")
            {
                IsTerminal = false;
            }
            else
            {
                IsTerminal = true;
                Region_code = region_code;
            }
        }

        public Waypoint(string identifier, string name, GeoPoint location) : this(identifier, name, location, "", "")
        {

        }
    }
}
