using NavData_Interface.Objects.Fix.Navaid;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace NavData_Interface.DataSources.DFDUtility.Factory
{
    internal class VhfNavaidFactory
    {
        internal static VhfNavaid Factory(SQLiteDataReader reader)
        {
            var navaid = new VhfNavaid
                (
                SQLHelper.locationFromColumns(reader, "vor_latitude", "vor_longitude"),
                reader["area_code"].ToString(),
                reader["airport_identifier"].ToString(),
                reader["icao_code"].ToString(),
                reader["vor_identifier"].ToString(),
                reader["vor_name"].ToString(),
                Double.Parse(reader["vor_frequency"].ToString()),
                reader["dme_ident"].ToString(),
                SQLHelper.locationFromColumns(reader, "dme_latitude", "dme_longitude"),
                Int32.Parse(reader["dme_elevation"].ToString()),
                Double.Parse(reader["ilsdme_bias"].ToString()),
                Int32.Parse(reader["range"].ToString()),
                Double.Parse(reader["station_declination"].ToString())
                );

            return navaid;
        }
    }
}
