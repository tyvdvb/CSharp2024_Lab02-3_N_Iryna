using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.Exceptions
{
    internal class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException() : base("Invalid email format")
        {
        }
    }
}
