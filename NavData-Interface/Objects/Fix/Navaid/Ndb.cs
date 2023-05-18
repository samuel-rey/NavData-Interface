using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fix.Navaid
{
    public class Ndb : Navaid
    {

        public Ndb(string identifier, GeoPoint location, string areaCode, string icaoCode, string name, double frequency) : base(areaCode, identifier, icaoCode, location, name, frequency)
        {
        }
    }

    public class TerminalNdb : Ndb
    {
        public string AirportIdentifier { get; }

        public TerminalNdb(string identifier, GeoPoint location, string areaCode, string airportIdentifier, string icaoCode, string name, double frequency) : base(identifier, location, areaCode, icaoCode, name, frequency)
        {
            AirportIdentifier = airportIdentifier;
        }
    }
}
