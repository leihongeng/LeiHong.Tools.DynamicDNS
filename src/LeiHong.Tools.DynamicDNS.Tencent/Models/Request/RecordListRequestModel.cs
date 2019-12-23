using LeiHong.Tools.DynamicDNS.TencentCloud.Models.Base;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Models
{
    public class RecordListRequestModel : IRequestModel
    {
        public RecordListRequestModel(string domain)
        {
            Domain = domain;
            Action = "RecordList";
        }


        public string Domain { get; set; }

        public string Action { get; }
    }
}