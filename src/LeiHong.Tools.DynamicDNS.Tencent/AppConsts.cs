using LeiHong.Tools.DynamicDNS.TencentCloud.Encrypt;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeiHong.Tools.DynamicDNS.TencentCloud
{
    public class AppConsts
    {
        public const string Gateway = "cns.api.qcloud.com/v2/index.php";
        public const string Protocol = "https";

        public static string SecretId { get; set; }
        public static string SecretKey { get; set; }
        public static HmacType SignatureMethod { get; set; } = HmacType.HmacSHA256;
        public static RequestMethod DefaultRequestMethod { get; set; } = RequestMethod.GET;
    }
}
