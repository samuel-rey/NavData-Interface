using NavData_Interface.Objects;
using NavData_Interface.Objects.Fix;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.DataSources
{
    public abstract class DataSource
    {
        public abstract List<Fix> GetFixesByIdentifier(string identifier);

        public abstract Localizer GetLocalizerFromAirportRunway(string airportIdentifier, string runwayIdentifier);
    }
}
