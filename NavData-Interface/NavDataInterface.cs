using System.Collections.Generic;
using AviationCalcUtilNet.GeoTools;
using NavData_Interface.DataSources;
using NavData_Interface.Objects.Fix;

namespace NavData_Interface
{
    public class NavDataInterface
    {
        private DataSource _dataSource;

        public NavDataInterface(string filePath)
        {
            _dataSource = new DFDSource(filePath);
        }

        public List<Waypoint> GetWaypoints(string identifier)
        {
            var foundWaypoints = _dataSource.GetEnrouteWaypoints(identifier);
            foreach (var terminalWaypoint in _dataSource.GetTerminalWaypoints(identifier))
            {
                foundWaypoints.Add(terminalWaypoint);
            }
            return foundWaypoints;
            
        }

        public Waypoint GetClosestWaypoint(GeoPoint point, string identifier)
        {
            var waypoints = GetWaypoints(identifier);
            Waypoint closestWaypoint = null;
            double closestDistance = double.MaxValue;

            foreach (var waypoint in waypoints)
            {
                double distance = GeoPoint.DistanceM(point, waypoint.Location);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestWaypoint = waypoint;
                }
            }

            return closestWaypoint;
        }
    }
}