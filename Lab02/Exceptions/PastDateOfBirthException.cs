using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.Exceptions
{
    internal class PastDateOfBirthException : Exception
    {
        public PastDateOfBirthException() : base("Date of birth is too far in the past.")
        {
        }
    }
}
