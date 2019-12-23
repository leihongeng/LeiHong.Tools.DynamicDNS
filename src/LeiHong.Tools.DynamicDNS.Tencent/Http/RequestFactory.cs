using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Flurl.Http;
using Flurl.Http.Configuration;
using LeiHong.Tools.DynamicDNS.TencentCloud.Encrypt;
using LeiHong.Tools.DynamicDNS.TencentCloud.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Http
{
    public static class RequestFactory
    {
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public static string CreatePost(IRequestModel dataModel)
        {
            if (dataModel == null)
            {
                throw new ArgumentNullException(nameof(dataModel));
            }

            var originData =
                $"Action={dataModel.Action}&Nonce={new Random(DateTime.Now.Millisecond).Next(10000, 99999)}&SecretId={AppConsts.SecretId}&SignatureMethod={AppConsts.SignatureMethod.ToString()}&Timestamp={DateTimeOffset.Now.ToUnixTimeSeconds()}";
            var data = $"{AppConsts.Gateway}?{originData}";

            var serializeData = JsonConvert.SerializeObject(dataModel,new JsonSerializerSettings(){ContractResolver = new CamelCasePropertyNamesContractResolver()});
            var json = JObject.Parse(serializeData);
            var dic = new SortedDictionary<string, string>();
            foreach (var item in json)
            {
                dic.Add(item.Key, item.Value.Value<string>());
            }

            dic.Remove("action");
            StringBuilder sb=new StringBuilder();
            foreach (var dicKey in dic.Keys)
            {
                sb.Append($"{dicKey}={dic[dicKey]}&");
            }
            serializeData = sb.ToString().TrimEnd(new[] {'&'});

            if (!string.IsNullOrEmpty(serializeData))
            {
                data = $"{data}&{serializeData}";
                originData = $"{originData}&{serializeData}";
            }
            var encryptData = $"POST{data}";
            var signature = AppConsts.SignatureMethod == HmacType.HmacSHA1
                ? HmacUtil.EncryptWithSha1(encryptData, AppConsts.SecretKey)
                : HmacUtil.EncryptWithSha256(encryptData, AppConsts.SecretKey);

            var resultData = $"{originData}&Signature={WebUtility.UrlEncode(signature)}";
            return resultData;
        }

        public static string CreateGet(IRequestModel dataModel)
        {
            if (dataModel == null)
            {
                throw new ArgumentNullException(nameof(dataModel));
            }
            var data = $"{AppConsts.Gateway}?Action={dataModel.Action}&Nonce={new Random(DateTime.Now.Millisecond).Next(10000, 99999)}&SecretId={AppConsts.SecretId}&SignatureMethod={AppConsts.SignatureMethod.ToString()}&Timestamp={DateTimeOffset.Now.ToUnixTimeSeconds()}";

            var serializeData = JsonConvert.SerializeObject(dataModel, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var json = JObject.Parse(serializeData);
            var dic = new SortedDictionary<string, string>();
            foreach (var item in json)
            {
                dic.Add(item.Key, item.Value.Value<string>());
            }

            dic.Remove("action");
            StringBuilder sb = new StringBuilder();
            foreach (var dicKey in dic.Keys)
            {
                sb.Append($"{dicKey}={dic[dicKey]}&");
            }
            serializeData = sb.ToString().TrimEnd(new[] { '&' });
            data = $"{data}&{serializeData}";
            var encryptData = $"GET{data}";
            var signature = AppConsts.SignatureMethod == HmacType.HmacSHA1
                ? HmacUtil.EncryptWithSha1(encryptData, AppConsts.SecretKey)
                : HmacUtil.EncryptWithSha256(encryptData, AppConsts.SecretKey);

            return $"{AppConsts.Protocol}://{data}&Signature={HttpUtility.UrlEncode(signature)}"; ;
        }

        public static async Task<string> RequestGetAsync(IRequestModel model)
        {
            var getUrl = RequestFactory.CreateGet(model);
            var resp = await getUrl.GetAsync();
            resp.EnsureSuccessStatusCode();
            var resultData = await resp.Content.ReadAsStringAsync();
            return resultData;
        }

        public static async Task<T> RequestGetAsync<T>(IRequestModel model)
        {
            var getUrl = RequestFactory.CreateGet(model);
            return await getUrl.GetJsonAsync<T>();
        }

        public static async Task<string> RequestPostAsync(IRequestModel model)
        {
            var postData = RequestFactory.CreatePost(model);
            var resp = await $"{AppConsts.Protocol}://{AppConsts.Gateway}".PostUrlEncodedAsync(postData);
            resp.EnsureSuccessStatusCode();
            var resultData = await resp.Content.ReadAsStringAsync();
            return resultData;
        }

        public static async Task<T> RequestPostAsync<T>(IRequestModel model)
        {
            var postData = RequestFactory.CreatePost(model);
            return await $"{AppConsts.Protocol}://{AppConsts.Gateway}".PostUrlEncodedAsync(postData).ReceiveJson<T>();
        }

        public static async Task<string> Request(IRequestModel model)
        {
            switch (AppConsts.DefaultRequestMethod)
            {
                case RequestMethod.GET: return await RequestGetAsync(model);
                case RequestMethod.POST: return await RequestPostAsync(model);
                default:throw new InvalidOperationException("不支持的请求方式");
            }
        }

        public static async Task<T> Request<T>(IRequestModel model)
        {
            switch (AppConsts.DefaultRequestMethod)
            {
                case RequestMethod.GET: return await RequestGetAsync<T>(model);
                case RequestMethod.POST: return await RequestPostAsync<T>(model);
                default: throw new InvalidOperationException("不支持的请求方式");
            }
        }

    }
}