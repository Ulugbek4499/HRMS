using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Exceptions
{
    public class ValidationException:Exception
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public ValidationException()
            :base("One more Validation Exception occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string name, string message)
        {
            Errors = new Dictionary<string, string[]>
            {
                {name, new string[] {message} }
            };
        }

        public ValidationException(IEnumerable<Exception> failures)
        {
           /* Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key,
                              failureGroup => failureGroup.ToArray());*/
        }
    }
}
