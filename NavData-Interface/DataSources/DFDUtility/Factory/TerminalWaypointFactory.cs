﻿using NavData_Interface.Objects.Fixes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace NavData_Interface.DataSources.DFDUtility.Factory
{
    internal static class TerminalWaypointFactory
    {
        public static Waypoint Factory(SQLiteDataReader reader)
        {
            var waypoint = new Waypoint(
                    reader["waypoint_identifier"].ToString(),
                    SQLHelper.locationFromColumns(reader, "waypoint_latitude", "waypoint_longitude"),
                    reader["area_code"].ToString(),
                    reader["icao_code"].ToString(),
                    reader["region_code"].ToString()
                );
            return waypoint;
        }
    }
}
