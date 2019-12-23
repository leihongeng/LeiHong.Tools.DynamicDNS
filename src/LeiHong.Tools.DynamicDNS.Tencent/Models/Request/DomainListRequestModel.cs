using LeiHong.Tools.DynamicDNS.TencentCloud.Models.Base;

namespace LeiHong.Tools.DynamicDNS.TencentCloud.Models
{
    public class DomainListRequestModel:IRequestModel
    {
        public DomainListRequestModel()
        {
            Action = "DomainList";
        }


        /// <summary>
        /// 偏移量，默认为0。
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// 返回数量，默认20，最大值100
        /// </summary>
        public int Length { get; set; } = 20;

        /// <summary>
        /// （过滤条件）根据关键字搜索域名
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 过滤条件）项目 ID
        /// </summary>
        public int QProjectId { get; set; }

        public string Action { get; }
    }
}