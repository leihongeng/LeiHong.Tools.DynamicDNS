using System;
using LeiHong.Tools.DynamicDNS.Core;
using Newtonsoft.Json.Linq;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Http
{
    public class ResponseUtil
    {
        public static DynamicDNSResult Validate(string resp)
        {
            try
            {
                Console.WriteLine(resp);
                var json = JObject.Parse(resp);
                var code = json["code"].Value<int>();
                if (code==0)
                {
                    return new DynamicDNSResult()
                    {
                        IsSuccess =true,
                        Data = resp
                    };
                }
                return new DynamicDNSResult()
                {
                    IsSuccess = false,
                    ErrorMessage = json["message"].Value<string>()
                };
            }
            catch (Exception e)
            {
               return  new DynamicDNSResult()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}