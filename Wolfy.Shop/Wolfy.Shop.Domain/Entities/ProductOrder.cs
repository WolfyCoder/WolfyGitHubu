using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfy.Shop.Domain.Entities
{
    /// <summary>
    /// 描述：订单商品实体，数据库持久化类
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class ProductOrder
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual Guid OrderID { set; get; }
        /// <summary>
        /// 订单下商品id
        /// </summary>
        public virtual Guid ProductID { set; get; }
    }
}
