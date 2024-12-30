using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Application.Common.Exeptions
{
    public class ValidationEventAppException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }
        public ValidationEventAppException(IReadOnlyDictionary<string, string[]> errors)
            : base("One or more validation errors occured")
            => Errors = errors;
    }
}
