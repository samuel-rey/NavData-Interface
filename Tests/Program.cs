using System;
using AviationCalcUtilNet.GeoTools;
using NavData_Interface;

namespace NavData_Interface_Tests
{
    public class Tests
    {
        public static void Main()
        {
            TestGetClosestWaypoint();
        }

        public static void TestGetWaypointLocation()
        {
            var navDataInterface = new NavDataInterface("C:\\Users\\Samuel\\Desktop\\NavData\\e_dfd_2301.s3db");
            var waypoints = navDataInterface.GetWaypoints("WILLO");
            foreach (var waypoint in waypoints)
            {
                Console.WriteLine($"Waypoint {waypoint.Identifier} is located at ({waypoint.Location.Lat}, {waypoint.Location.Lon})");
            }
        }

        public static void TestGetClosestWaypoint()
        {
            var navDataInterface = new NavDataInterface("C:\\Users\\Samuel\\Desktop\\NavData\\e_dfd_2301.s3db");
            GeoPoint point = new GeoPoint(51.5074, -0.1278); // Example location (London)
            string identifier = "WILLO";
            var closestWaypoint = navDataInterface.GetClosestWaypoint(point, identifier);
            Console.WriteLine($"The closest waypoint to ({point.Lat}, {point.Lon}) with identifier {identifier} is located at ({closestWaypoint.Location.Lat}, {closestWaypoint.Location.Lon})");
        }
    }
}