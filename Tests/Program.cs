using System;
using AviationCalcUtilNet.GeoTools;
using NavData_Interface;
using NavData_Interface.DataSources;

namespace NavData_Interface_Tests
{
    public class Tests
    {
        public static void Main()
        {
            TestGetClosestWaypoint();
            TestGetNavaidByIdentifier();
        }

        public static void TestGetWaypointLocation()
        {
            var dataSource = new DFDSource("e_dfd_2301.s3db");
            var waypoints = dataSource.GetEnrouteWaypoints("WILLO");

            foreach (var waypoint in dataSource.GetTerminalWaypoints("WILLO"))
            {
                waypoints.Add(waypoint);
            }

            foreach (var waypoint in waypoints)
            {
                Console.WriteLine($"Waypoint {waypoint.Identifier} is located at ({waypoint.Location.Lat}, {waypoint.Location.Lon})");
            }
        }

        public static void TestGetClosestWaypoint()
        {
            var navDataInterface = new NavDataInterface(new DFDSource("e_dfd_2301.s3db"));
            GeoPoint point = new GeoPoint(51.5074, -0.1278); // Example location (London)
            string identifier = "WILLO";
            var closestWaypoint = navDataInterface.GetClosestFixByIdentifier(point, identifier);
                Console.WriteLine($"The closest waypoint to ({point.Lat}, {point.Lon}) with identifier {identifier} is located at ({closestWaypoint.Location.Lat}, {closestWaypoint.Location.Lon})");
        }
        public static void TestGetNavaidByIdentifier()
        {
            // Assuming you have a NavaidDataSource class that provides access to Navaid data
            var navaidDataSource = new DFDSource("e_dfd_2301.s3db");

            // Replace "ICAO_CODE" with the ICAO code of the Navaid you want to test
            var navaidIdentifier = "LAM";

            var navaids = navaidDataSource.GetNavaidsByIdentifier(navaidIdentifier);

            if (navaids.Count > 0)
            {
                foreach (var navaid in navaids)
                {
                    Console.WriteLine($"Navaid {navaid.Identifier} with ICAO code {navaid.IcaoCode}, named {navaid.Name} is located at ({navaid.Location.Lat}, {navaid.Location.Lon})");
                }
             }
            else
            {
                Console.WriteLine($"Navaid with ICAO code {navaidIdentifier} not found.");
            }
        }
    }
}