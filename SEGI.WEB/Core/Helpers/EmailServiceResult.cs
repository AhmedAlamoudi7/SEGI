using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.Helpers
{
    public class EmailServiceResult
    {
        // Indicates whether the operation was successful or not
        public bool Success { get; set; }

        // Message containing additional information about the result (e.g., error message or success message)
        public string Message { get; set; }
    }
}
