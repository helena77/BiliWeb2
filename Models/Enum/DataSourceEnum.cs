using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Models.Enum
{
    public enum DataSourceEnum
    {
        // Not specified
        Unknown = 0,

        // Mock Dataset
        Mock = 1,

        //TODO, remove
        SQL = 2,

        // Empty for unit tests
        UnitTest = 3,

        // Data Storage
        Local = 10,
        ServerTest = 11,
        ServerLive = 12
    }
}
