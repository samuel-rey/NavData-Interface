using NavData_Interface.Objects.Fix;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavData_Interface.DataSources
{
    public interface IDataSource
    {
        List<Fix> GetFixesByIdentifier(string identifier);
    }
}
