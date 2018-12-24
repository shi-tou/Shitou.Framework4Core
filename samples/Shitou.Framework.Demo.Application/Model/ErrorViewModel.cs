using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Demo.Application.Model
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
