using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfy.Shop.Domain.Entities;

namespace Wolfy.Shop.Data
{
    /// <summary>
    /// 描述：订单数据层
    /// 创建人：wolfy
    /// 创建时间：2014-11-02
    /// </summary>
    public class OrderData
    {
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrder(Order order)
        {
            try
            {
                var session = NHibernateHelper.GetSession();
                session.SaveOrUpdate(order);
                session.Flush();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
