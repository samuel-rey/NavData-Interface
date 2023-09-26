using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using NavData_Interface.Objects.Fix;
using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects.Fix.Navaid;
using NavData_Interface.DataSources.DFDUtility;
using NavData_Interface.DataSources.DFDUtility.Factory;
using NavData_Interface.Objects;

namespace NavData_Interface.DataSources
{
    public class DFDSource : DataSource
    {
        private SQLiteConnection _connection;

        public DFDSource(string filePath)
        {
            _connection = new SQLiteConnection($"Data Source={filePath};Version=3;");
            _connection.Open();
        }

        override
        public Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier)
        {
            var foundLocs = GetObjectsWithQuery<Localizer>(LocalizerLookupByAirportRunway(airportIdentifier, runwayIdentifier), reader => LocalizerFactory.Factory(reader));

            return foundLocs[0];
        }

        private SQLiteCommand LocalizerLookupByAirportRunway(string airportIdentifier, string runwayIdentifier)
        {
            runwayIdentifier = "RW" + runwayIdentifier;
            
            var cmd = new SQLiteCommand(_connection)
            {
                CommandText = $"SELECT * from tbl_localizers_glideslopes WHERE airport_identifier = @airportIdentifier AND runway_identifier = @runwayIdentifier"
            };

            cmd.Parameters.AddWithValue("@airportIdentifier", airportIdentifier);
            cmd.Parameters.AddWithValue("@runwayIdentifier", runwayIdentifier);

            return cmd;
        }

        private SQLiteCommand AirportLookupByIdentifier(string identifier)
        {
            var cmd = new SQLiteCommand(_connection)
            {
                CommandText = $"SELECT * FROM tbl_airports WHERE airport_identifier = @identifier"
            };

            cmd.Parameters.AddWithValue("@identifier", identifier);

            return cmd;
        }

        public Airport GetAirportByIdentifier(string identifier)
        {
            var airports = GetObjectsWithQuery<Airport>(AirportLookupByIdentifier(identifier), reader => AirportFactory.Factory(reader));

            if (airports.Count == 1)
            {
                return airports[0];
            } else if (airports.Count > 1)
            {
                Console.Error.WriteLine($"Found two airport results for {identifier}. This should never happen!");
            }

            return null;
        }

        private SQLiteCommand WaypointLookupByIdentifier(bool isTerminal, string identifier)
        {
            var table = isTerminal ? "tbl_terminal_waypoints" : "tbl_enroute_waypoints";

            var cmd = new SQLiteCommand(_connection)
            {
                CommandText = $"SELECT * FROM {table} WHERE waypoint_identifier = @identifier"
            };

            cmd.Parameters.AddWithValue("@identifier", identifier);

            return cmd;
        }

        public List<Waypoint> GetWaypointsByIdentifier(string identifier)
        {
            // We need to combine enroute + terminal waypoints
            
            List<Waypoint> waypoints = GetObjectsWithQuery<Waypoint>(WaypointLookupByIdentifier(true, identifier), reader => WaypointFactory.Factory(reader));
            foreach (var waypoint in GetObjectsWithQuery<Waypoint>(WaypointLookupByIdentifier(false, identifier), reader => WaypointFactory.Factory(reader)))
            {
                waypoints.Add(waypoint);
            }

            return waypoints;
        }

        private SQLiteCommand VhfNavaidLookupByIdentifier(string identifier)
        {
            var cmd = new SQLiteCommand(_connection)
            {
                CommandText = $"SELECT * from tbl_vhfnavaids WHERE vor_identifier = @identifier OR dme_ident = @identifier"
            };
            cmd.Parameters.AddWithValue("@identifier", identifier);

            return cmd;
        }

        public List<VhfNavaid> GetVhfNavaidsByIdentifier(string identifier)
        {
            List<VhfNavaid> navaids = GetObjectsWithQuery<VhfNavaid>(
                VhfNavaidLookupByIdentifier(identifier), 
                reader => VhfNavaidFactory.Factory(reader));

            return navaids;
        }

        public SQLiteCommand NdbLookupByIdentifier(bool isTerminal, string identifier)
        {
            var table = isTerminal ? "tbl_terminal_ndbnavaids" : "tbl_enroute_ndbnavaids";

            var cmd = new SQLiteCommand(_connection)
            {
                CommandText = $"SELECT * FROM {table} WHERE ndb_identifier = @identifier"
            };

            cmd.Parameters.AddWithValue("@identifier", identifier);

            return cmd;
        }

        public List<Ndb> GetNdbsByIdentifier(string identifier)
        {
            // We need to combine enroute + terminal NDBs

            List<Ndb> ndbs = GetObjectsWithQuery<Ndb>(NdbLookupByIdentifier(true, identifier), reader => NdbFactory.Factory(reader));
            foreach (var ndb in GetObjectsWithQuery<Ndb>(NdbLookupByIdentifier(false, identifier), reader => NdbFactory.Factory(reader)))
            {
                ndbs.Add(ndb);
            }

            return ndbs;
        }

        public List<Navaid> GetNavaidsByIdentifier(string identifier)
        {
            // We get all VHF Navaids + all NDBs with this ident

            var navaids = new List<Navaid>();

            foreach (var vhfNavaid in GetVhfNavaidsByIdentifier(identifier))
            {
                navaids.Add(vhfNavaid);
            }

            foreach (var ndbNavaid in GetNdbsByIdentifier(identifier))
            {
                navaids.Add(ndbNavaid);
            }

            return navaids;
        }

        internal List<T> GetObjectsWithQuery<T>(SQLiteCommand cmd, Func<SQLiteDataReader, T> objectFactory)
        {
            var objects = new List<T>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var obj = objectFactory(reader);
                    objects.Add(obj);
                }
            }

            return objects;
        }

        override
        public List<Fix> GetFixesByIdentifier(string identifier)
        {
            List<Fix> foundFixes = new List<Fix>();

            foreach (var waypoint in this.GetWaypointsByIdentifier(identifier))
            {
                foundFixes.Add(waypoint);
            }

            foreach (var navaid in this.GetNavaidsByIdentifier(identifier))
            {
                foundFixes.Add(navaid);
            }

            var airport = GetAirportByIdentifier(identifier);

            if (airport != null)
            {
                foundFixes.Add(airport);
            }

            return foundFixes;
        }
    }
}
