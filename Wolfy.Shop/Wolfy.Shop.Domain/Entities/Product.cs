using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfy.Shop.Domain.Entities
{
    /// <summary>
    /// 描述：商品实体，数据库持久化类
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public virtual Guid ProductID { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string Name { set; get; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public virtual decimal Price { set; get; }
        /// <summary>
        /// 多对多关系：Product属于多个Orders
        /// </summary>
        public virtual IList<Order> Orders { get; set; }

    }
}
