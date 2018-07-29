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

        public static string GetDateValidatedToShortDateString(DateTime? date)
        {
            var formatedDate = string.Empty;

            if (date != null)
            {
                var validDate = (DateTime)date;
                if (validDate > new DateTime(0001, 1, 1))
                {
                    formatedDate = validDate.ToString("MM/dd/yyyy");
                }
            }

            return formatedDate;
        }
    }
}
