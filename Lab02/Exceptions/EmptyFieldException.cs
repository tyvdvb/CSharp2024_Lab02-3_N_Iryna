using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.Exceptions
{
    internal class EmptyFieldException : Exception
    {
        public EmptyFieldException(string message) : base(message)
        {
        }
    }
}
