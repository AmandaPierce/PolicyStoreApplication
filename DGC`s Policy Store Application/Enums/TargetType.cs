using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Enums
{
    [Flags]
    public enum TargetType
    {
        None = 0,
        FullTimeEmployees = 1,
        PartTimeEmployees = 2,
        SeasonalEmployees = 4,
        TemporaryEmployees = 8,
        LeasedEmployees = 16
    }
}
