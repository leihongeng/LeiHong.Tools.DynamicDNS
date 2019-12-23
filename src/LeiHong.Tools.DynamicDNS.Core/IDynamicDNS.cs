using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeiHong.Tools.DynamicDNS.Core
{
    public interface IDynamicDNS
    {
        /// <summary>
        /// 获取解析记录列表
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="offset">偏移量，默认为0。</param>
        /// <param name="length">返回数量，默认20，最大值100</param>
        /// <param name="subDomain">（过滤条件）根据子域名进行过滤</param>
        /// <returns></returns>
        Task<DynamicDNSResult> LoadList(string domain, int offset = 0, int length = 20, string subDomain = "");

        /// <summary>
        /// 设置记录状态
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="recordId">解析记录的 ID</param>
        /// <param name="status">可选值为：“disable” 和 “enable”，分别代表 “暂停” 和 “启用”</param>
        /// <returns></returns>
        Task<DynamicDNSResult> SetStatus(string domain, int recordId, RecordStatus status);

        /// <summary>
        /// 添加解析记录
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="subDomain">子域名，例如：www</param>
        /// <param name="recordType">记录类型，可选的记录类型为："A", "CNAME", "MX", "TXT", "NS", "AAAA", "SRV"</param>
        /// <param name="value">记录值</param>
        /// <returns></returns>
        Task<DynamicDNSResult> AddAsync(string domain, string subDomain, string recordType, string value);

        /// <summary>
        /// 修改解析记录
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="recordId">解析记录的 ID</param>
        /// <param name="subDomain">子域名，例如：www</param>
        /// <param name="recordType">记录类型，可选的记录类型为："A"，"CNAME"，"MX"，"TXT"，"NS"，"AAAA"，"SRV"</param>
        /// <param name="value">记录值</param>
        /// <returns></returns>
        Task<DynamicDNSResult> Update(string domain, int recordId, string subDomain, string recordType, string value);

        /// <summary>
        /// 添加或更新解析记录
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="subDomain">子域名，例如：www</param>
        /// <param name="recordType">记录类型，可选的记录类型为："A", "CNAME", "MX", "TXT", "NS", "AAAA", "SRV"</param>
        /// <param name="value">记录值</param>
        /// <returns></returns>
        Task<DynamicDNSResult> AddOrUpdateAsync(string domain, string subDomain, string recordType, string value);

        /// <summary>
        /// 删除解析记录
        /// </summary>
        /// <param name="domain">主域名，不包括 www，例如：xxx.com</param>
        /// <param name="recordId">解析记录的 ID</param>
        /// <returns></returns>
        Task<DynamicDNSResult> DeleteAsync(string domain, int recordId);

    }
}
