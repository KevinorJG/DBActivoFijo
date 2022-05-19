using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Domain.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

    }
}
