using AviationCalcUtilNet.GeoTools;
using NavData_Interface.DataSources;
using NavData_Interface.Objects.Fix;

namespace NavData_Interface
{
    public class NavDataInterface
    {
        private IDataSource _dataSource;

        public NavDataInterface(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Fix GetClosestFixByIdentifier(GeoPoint point, string identifier)
        {
            var fixes = _dataSource.GetFixesByIdentifier(identifier);
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