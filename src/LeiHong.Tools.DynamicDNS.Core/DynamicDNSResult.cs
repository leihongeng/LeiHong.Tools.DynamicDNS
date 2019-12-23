using System;
using System.Collections.Generic;
using System.Text;

namespace LeiHong.Tools.DynamicDNS.Core
{
    public class DynamicDNSResult
    {

        public DynamicDNSResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            ErrorMessage = message;
        }

        public DynamicDNSResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public DynamicDNSResult()
        {

        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string Data { get; set; }
    }
}
