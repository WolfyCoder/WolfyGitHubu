using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfy.Shop.Data;
using Wolfy.Shop.Domain.Entities;

namespace Wolfy.Shop.Business
{
    /// <summary>
    /// 描述：订单业务逻辑层
    /// 创建人：wolfy
    /// 创建时间：2014-11-02
    /// </summary>
    public class OrderBusiness
    {
        private OrderData _orderData;
        public OrderBusiness()
        {
            _orderData = new OrderData();
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrder(Order order)
        {
            return _orderData.AddOrder(order);
        }
    }
}
