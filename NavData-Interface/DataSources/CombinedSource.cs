using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects;
using NavData_Interface.Objects.Fixes;
using System.Collections.Generic;

namespace NavData_Interface.DataSources
{
    public class CombinedSource : DataSource
    {
        public DataSource Data_source { get; }

        public CombinedSource(DataSource dataSource)
        {
            Data_source = dataSource;
        }

        

        public override List<Fix> GetFixesByIdentifier(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public override Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier)
        {
            throw new System.NotImplementedException();
        }

        public override Airport GetClosestAirportWithinRadius(GeoPoint position, double radiusM)
        {
            throw new System.NotImplementedException();
        }
    }
}