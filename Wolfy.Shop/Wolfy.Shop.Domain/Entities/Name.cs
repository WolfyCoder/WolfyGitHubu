using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfy.Shop.Domain.Entities
{
    /// <summary>
    /// 描述：客户实体，数据库持久化类
    /// 创建人：wolfy
    /// 创建时间：2014-11-01
    /// </summary>
    public class Name
    {
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 客户名字
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 名字和地址
        /// </summary>
        public string NameAddress
        {
            get { return this.CustomerName + this.CustomerAddress; }
        }
    }
}
