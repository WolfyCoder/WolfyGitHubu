using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Wolfy.Shop.Domain.Entities
{
    /// <summary>
    /// 描述：客户实体，数据库持久化类
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 客户id
        /// </summary>
        public virtual Guid CustomerID { get; set; }
        /// <summary>
        /// 客户名字
        /// </summary>
        public virtual Name NameAddress { get; set; }
        /// <summary>
        /// 版本控制
        /// </summary>
        public virtual int Version { get; set; }
        /// <summary>
        /// 一对多关系：一个Customer有一个或者多个Order
        /// </summary>
        public virtual System.Collections.Generic.ISet<Order> Orders { set; get; }
    }
}
