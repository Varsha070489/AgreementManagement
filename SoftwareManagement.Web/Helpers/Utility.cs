using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Helpers
{
    public class Utility
    {
        public static StringContent GetContent<T>(T model)
        {
            var Content = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(Content, Encoding.Default, "application/json");
            return stringContent;
        }
    }
}
