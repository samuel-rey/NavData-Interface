using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects;
using NavData_Interface.Objects.Fixes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NavData_Interface.DataSources
{
    public abstract class DataSource
    {
        public abstract List<Fix> GetFixesByIdentifier(string identifier);

        public abstract Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier);

        public abstract Airport GetClosestAirportWithinRadius(GeoPoint position, double radiusM);

        public Fix GetClosestFixByIdentifier(GeoPoint point, string identifier)
        {
            var fixes = GetFixesByIdentifier(identifier);
            Fix closestFix = null;
            double closestDistance = double.MaxValue;

            foreach (var fix in fixes)
            {
                double distance = GeoPoint.DistanceM(point, fix.Location);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFix = fix;
                }
            }

            return closestFix;
        }
    }
}
