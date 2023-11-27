using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects.Fixes;

namespace NavData_Interface.DataSources
{
    public class CombinedSource
    {
        public DataSource Data_source { get; }

        public CombinedSource(DataSource dataSource)
        {
            Data_source = dataSource;
        }

        public Fix GetClosestFixByIdentifier(GeoPoint point, string identifier)
        {
            var fixes = Data_source.GetFixesByIdentifier(identifier);
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