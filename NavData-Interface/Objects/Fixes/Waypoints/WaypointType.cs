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

        public WaypointType(Kind waypointClass) : this(waypointClass, false, false, false, false, false, false, false) { }

        public WaypointType(Kind waypointClass, bool isIaf, bool isFaf, bool isIf, bool isMaf, bool isFac, bool isStepdownFix, bool isOceanicEntryExit)
        {
            WaypointClass = waypointClass;
            IsIaf = isIaf;
            IsFaf = isFaf;
            IsIf = isIf;
            IsMaf = isMaf;
            IsFac = isFac;
            IsStepdownFix = isStepdownFix;
            IsOceanicEntryExit = isOceanicEntryExit;
        }

        public WaypointType()
        {
            WaypointClass = Kind.Rnav;
            IsIaf = false;
            IsFaf = false;
            IsIf = false;
            IsMaf = false;
            IsFac = false;
            IsStepdownFix = false;
            IsOceanicEntryExit = false;
        }
    }
}
