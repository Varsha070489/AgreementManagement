using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Helpers
{
    public class ResourceHelper
    {
        public static string GetResourceValue(string key)
        {
            ResourceManager rm = new ResourceManager("SoftwareManagement.Web.Messages", Assembly.GetExecutingAssembly());
            String value = rm.GetString(key, CultureInfo.CurrentCulture);
            return value;
        }
    }
}
