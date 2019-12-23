using System.Collections.Generic;
using LeiHong.Tools.DynamicDNS.TencentCloud.Models.Base;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Models.Response
{
    public class RecordListResponseModel : ResponseModel
    {
        public RecordData Data { get; set; }
    }

    public class RecordData
    {
        public RecordData()
        {
            Records = new List<RecordDataItem>();
        }

        public List<RecordDataItem> Records { get; set; }
    }

    public class RecordDataItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}