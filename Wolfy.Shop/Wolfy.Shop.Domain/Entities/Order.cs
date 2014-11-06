using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfy.Shop.Domain.Entities
{
    /// <summary>
    /// 描述：订单实体，数据库持久化类
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual Guid OrderID { set; get; }
        /// <summary>
        /// 下订单时间
        /// </summary>
        public virtual DateTime OrderDate { set; get; }
        /// <summary>
        /// 下订单的客户,多对一的关系：orders对应一个客户
        /// </summary>
        public virtual Customer Customer { set; get; }
        /// <summary>
        /// 多对多关系，一个订单下可以有多个产品
        /// </summary>
        public virtual IList<Product> Products { set; get; }
    }
}
