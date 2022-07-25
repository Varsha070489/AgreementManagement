using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException(string message)
            : base(message)
        {
        }

        public EntityException(
            string message,
            params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
