using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fix
{
    public abstract class Fix
    {
        public string Identifier { get; }
        public GeoPoint Location { get; }

        protected Fix(string identifier, GeoPoint location)
        {
            Identifier = identifier;
            Location = location;
        }
    }
}