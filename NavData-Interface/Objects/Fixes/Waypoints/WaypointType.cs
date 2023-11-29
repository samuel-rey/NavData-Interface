using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.Objects.Fixes.Waypoints
{
    public enum Kind
    {
        Rnav,
        Vfr,
        Ndb,
        Om,
        Mm,
        RFCenter,
        Other
    }

    public class WaypointType
    {
        public Kind WaypointClass { get; }

        public bool IsIaf { get; }

        public bool IsFaf { get; }
        
        public bool IsIf { get; }

        public bool IsMaf { get; }

        public bool IsFac { get; }

        public bool IsStepdownFix { get; }

        public bool IsOceanicEntryExit { get; }

        public WaypointType(string typeString)
        {
            if (typeString.Length != 3)
            {
                throw new FormatException("The waypoint type is invalid because the type field is too long");
            }

            switch (typeString[0])
            {
                case 'A':
                    WaypointClass = Kind.RFCenter;

                    break;
                case 'C':
                    WaypointClass = Kind.Rnav; 
                    break;
                case 'M':
                    WaypointClass = Kind.Mm;

                    break;
                case 'N':
                    WaypointClass = Kind.Ndb;
                    break;
                case 'O':
                    WaypointClass = Kind.Om;
                    break;
                case 'R':
                    WaypointClass = Kind.Other;
                    break;
                case 'U':
                    WaypointClass = Kind.Other;
                    break;
                case 'V':
                    WaypointClass = Kind.Vfr;
                    break;
                case 'W':
                    WaypointClass = Kind.Rnav;
                    break;

                default:
                    throw new FormatException("The first column of the waypoint type is invalid");


            }
        }
    }
}
