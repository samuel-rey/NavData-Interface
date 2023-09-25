using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace NavData_Interface.DataSources.DFDUtility
{
    internal static class SQLHelper
    {
        internal static GeoPoint locationFromColumns(SQLiteDataReader reader, string latColumn, string lonColumn)
        {
            return new GeoPoint(
                Double.Parse(reader[latColumn].ToString()),
                Double.Parse(reader[lonColumn].ToString())
                );
        }
    }
}
