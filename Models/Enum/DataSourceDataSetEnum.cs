using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models.Enum
{
    public enum DataSourceDataSetEnum
    {
        // Not specified
        Default = 0,

        // Mock Dataset
        Demo = 1,

        // SQL Dataset
        UnitTest = 2,

        // Empty Dataset for Unit testing purposes
        EmptyUnitTest = 3
    }
}
