using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Utility
{
    public class CommonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Dataenum { get; set; }
    }
}
