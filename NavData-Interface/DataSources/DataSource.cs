using AviationCalcUtilNet.GeoTools;
using NavData_Interface.Objects;
using NavData_Interface.Objects.Fixes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NavData_Interface.DataSources
{
    public interface DataSource
    {
        List<Fix> GetFixesByIdentifier(string identifier);

        Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier);

        Airport GetClosestAirportWithinRadius(GeoPoint position, double radiusM);

        Airport GetAirportByIdentifier(string identifier);
    }
}
