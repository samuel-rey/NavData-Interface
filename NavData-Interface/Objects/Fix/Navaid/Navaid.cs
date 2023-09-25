using AviationCalcUtilNet.GeoTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fix.Navaid
{
    public abstract class Navaid : Fix
    {
        public string AreaCode { get; }
        public string IcaoCode { get; }
        public string Name { get; }
        public double Frequency { get; }

        public int Range { get; }
        protected Navaid(string areaCode, string identifier, string icaoCode, GeoPoint location, string name, double frequency, int range) : base(identifier, location)
        {
            IcaoCode = icaoCode;
            AreaCode = areaCode;
            Name = name;
            Frequency = frequency;
            Range = range;
        }
    }
}
