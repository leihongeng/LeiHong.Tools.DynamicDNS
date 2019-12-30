using LeiHong.Tools.DynamicDNS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeiHong.Tools.DynamicDNS.TencentCloud.Http;
using LeiHong.Tools.DynamicDNS.TencentCloud.Models;
using LeiHong.Tools.DynamicDNS.TencentCloud.Models.Response;

namespace LeiHong.Tools.DynamicDNS.TencentCloud
{
    public class TencentCloudDynamicDns : IDynamicDNS
    {
        public Task<DynamicDNSResult> LoadList(string domain, int offset = 0, int length = 20, string subDomain = "")
        {
            throw new NotImplementedException();
        }

        public Task<DynamicDNSResult> SetStatus(string domain, int recordId, RecordStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicDNSResult> AddAsync(string domain, string subDomain, string recordType, string value)
        {
            var model = new CreateRecordRequestModel(domain, subDomain, recordType, value);
            var resp = await RequestFactory.Request(model);
            return ResponseUtil.Validate(resp);
        }

        public Task<DynamicDNSResult> Update(string domain, int recordId, string subDomain, string recordType, string value)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicDNSResult> AddOrUpdateAsync(string domain, string subDomain, string recordType, string value)
        {
            var recordList = await RequestFactory.Request<RecordListResponseModel>(new RecordListRequestModel(domain));
            if (recordList.Code != 0)
            {
                return new DynamicDNSResult(false, recordList.Message);
            }

            if (!recordList.Data.Records.Any())
            {
                return new DynamicDNSResult(false, "没有任何解析记录！");
            }

            var updateRecordList = subDomain.Split(',').ToList();

            var result = string.Empty;
            foreach (var item in updateRecordList)
            {
                if(string.IsNullOrEmpty(item)) continue;
                var oldRecord = recordList.Data.Records.FirstOrDefault(x => x.Name == item)?.Id;
                if (oldRecord.HasValue)
                {
                    await RequestFactory.Request(new RemoveRecordRequestModel(domain, oldRecord.Value));
                }
                var model = new CreateRecordRequestModel(domain, item, recordType, value);
                result = await RequestFactory.Request(model);
            }
           
            return ResponseUtil.Validate(result);
        }

        public Task<DynamicDNSResult> DeleteAsync(string domain, int recordId)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicDNSResult> DeleteAsync(string domain, string subDomain)
        {
            var recordList = await RequestFactory.Request<RecordListResponseModel>(new RecordListRequestModel(domain));
            if (recordList.Code != 0)
            {
                return new DynamicDNSResult(false, recordList.Message);
            }

            var recordIds = recordList.Data.Records.Where(a => string.Equals(a.Name, subDomain, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Id);
            foreach (var id in recordIds)
            {
                var resp = await RequestFactory.Request(new RemoveRecordRequestModel(domain, id));
                var res = ResponseUtil.Validate(resp);
                if (!res.IsSuccess)
                {
                    return res;
                }
            }

            return new DynamicDNSResult()
            {
                IsSuccess = false,
                ErrorMessage = ""
            };
        }
    }
}
