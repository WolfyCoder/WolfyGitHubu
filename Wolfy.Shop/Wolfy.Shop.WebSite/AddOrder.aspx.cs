using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wolfy.Shop.Domain.Entities;

namespace Wolfy.Shop.WebSite
{
    public partial class AddOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCid = Request.QueryString["cid"];
                if (!string.IsNullOrEmpty(strCid))
                {
                    Business.OrderBusiness orderBusiness = new Business.OrderBusiness();
                    Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
                    
                    Order order = new Order() { Customer = customerBusiness.GetCustomerList(c => c.CustomerID == new Guid(strCid)).FirstOrDefault(), OrderDate = DateTime.Now, OrderID = Guid.NewGuid() };
                    if (orderBusiness.AddOrder(order))
                    {
                        Response.Write("添加成功");
                    }
                }
            }
        }
    }
}