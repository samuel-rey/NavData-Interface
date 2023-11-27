﻿using NavData_Interface.Objects.Fixes.Navaids;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace NavData_Interface.DataSources.DFDUtility.Factory
{
    internal static class TerminalNdbFactory
    {
        internal static Ndb Factory(SQLiteDataReader reader)
        {
            return new Ndb
                (
                reader["ndb_identifier"].ToString(),
                SQLHelper.locationFromColumns(reader, "ndb_latitude", "ndb_longitude"),
                reader["area_code"].ToString(),
                reader["icao_code"].ToString(),
                reader["ndb_name"].ToString(),
                Double.Parse(reader["ndb_frequency"].ToString()),
                Int32.Parse(reader["range"].ToString()),
                reader["airport_identifier"].ToString()
                );
        }
    }
}
