using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Utilities
{
    public class DataTypeUtility
    {
        public static int GetIntegerFromString(string stringValue)
        {
            int integerValue = 0;
            int.TryParse(stringValue, out integerValue);
            return integerValue;
        }
    }
}
