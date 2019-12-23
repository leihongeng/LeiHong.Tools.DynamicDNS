using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeiHong.Tools.DynamicDNS.Core;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace LeiHong.Tools.DynamicDNS.Sample.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DnsController : ControllerBase
    {

        private readonly IDynamicDNS _dynamicDns;
        private static string _clientRealIp = Empty;

        public DnsController(IDynamicDNS dynamicDns)
        {
            _dynamicDns = dynamicDns;
        }

        private string GetIp()
        {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (IsNullOrEmpty(ip))
            {
                ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            Console.WriteLine("获取到的客户端IP地址为：" + ip);
            return ip;
        }


        [HttpGet]
        public IActionResult Update()
        {
            var clientRealIp = GetIp();
            if (_clientRealIp == clientRealIp)
            {
                var message = DateTime.Now + "：公网IP无变化";
                Console.WriteLine(message);
                return Ok(message);
            }
            _clientRealIp = clientRealIp;
            return Ok(_dynamicDns.AddOrUpdateAsync("aiproject.cn", "ddns", "A", clientRealIp).Result);

        }
    }
}
