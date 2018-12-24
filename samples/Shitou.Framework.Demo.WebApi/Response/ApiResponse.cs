using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shitou.Framework.Demo.WebApi
{
    public class ApiResponse
    {
        public string MsgCode { get; set; }
        public string MsgContent { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T ResponseData { get; set; }
    }
}
