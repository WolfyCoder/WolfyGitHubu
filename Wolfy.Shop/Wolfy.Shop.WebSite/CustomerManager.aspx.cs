using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wolfy.Shop.Domain.Entities;

namespace Wolfy.Shop.WebSite
{
    public partial class CustomerManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RepeaterDataBind();
                //List<Guid> lst = new Business.CustomerBusiness().GetAllCustomerID() as List<Guid>;
                //foreach (var item in lst)
                //{
                //    Response.Write(item.ToString());
                //}
                //  Response.Write(new Business.CustomerBusiness().GetCustomerByName("wolfy").FirstOrDefault().CustomerName);
            }
        }
        private void RepeaterDataBind()
        {
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            this.rptCustomerList.DataSource = customerBusiness.GetCustomerList(c => 1 == 1); //customerBusiness.GetCustomers();
            this.rptCustomerList.DataBind();
        }
        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Guid guidCustomerID = Guid.NewGuid();
            var customer = new Customer() { NameAddress = new Name() { CustomerName = "zhangsan3322", CustomerAddress = "北京 海淀" }, CustomerID = guidCustomerID };
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            //使用事务的方式添加客户信息
            if (customerBusiness.SaveOrUpdateByTrans(customer))
            {
                RepeaterDataBind();
            }
            //提供一个名字长度溢出的测试数据
            customer = new Customer() { NameAddress = new Name() { CustomerName = "zhangsan3322", CustomerAddress = "北京 海淀" }, CustomerID = Guid.NewGuid() };
            //使用事务的方式添加客户信息
            if (customerBusiness.SaveOrUpdateByTrans(customer))
            {
                RepeaterDataBind();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            this.rptCustomerList.DataSource = new Business.CustomerBusiness().GetCustomerAddress(this.txtName.Text, this.txtAddress.Text);
            this.rptCustomerList.DataBind();
        }
        /// <summary>
        /// 并发更新操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSameTimeUpdate_Click(object sender, EventArgs e)
        {
            Guid guidCustomerId = new Guid("82724514-682E-4E6F-B759-02E499CDA50F");
            //模拟第一个修改数据
            Customer c1 = new Customer()
            {
                CustomerID = guidCustomerId,
                NameAddress = new Name() { CustomerName = "zhangsan3322", CustomerAddress = "北京 海淀" },
                Version = 1
            };
            //模拟第二个修改数据
            Customer c2 = new Customer()
            {
                CustomerID = guidCustomerId,
                NameAddress = new Name() { CustomerName = "zhangsan3322", CustomerAddress = "北京 海淀" },
                Version = 1
            };

            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            customerBusiness.SaveOrUpdateByTrans(c1);
            customerBusiness.SaveOrUpdateByTrans(c2);
        }
        /// <summary>
        /// 下订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkOrder_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            if (lnkBtn != null)
            {
                Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
                switch (lnkBtn.CommandName)
                {
                    case "Order":
                        Response.Redirect("AddOrder.aspx?cid=" + lnkBtn.CommandArgument);
                        break;
                    case "delete":

                        if (customerBusiness.DeleteCustomer(customerBusiness.GetCustomerList(c => c.CustomerID == new Guid(lnkBtn.CommandArgument)).FirstOrDefault()))
                        {
                            this.RepeaterDataBind();
                        }
                        break;
                    case "OrderProduct":
                        Customer customer = customerBusiness.GetCustomerbyLazyLoad(new Guid(lnkBtn.CommandArgument));
                        Response.Redirect("OrderInfo.aspx?oid=" + customer.Orders.FirstOrDefault().OrderID+"&dt="+custome);
                        break;
                    default:
                        break;
                }

            }
        }
        /// <summary>
        /// 添加客户和订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCsOrder_Click(object sender, EventArgs e)
        {
            var customer = new Customer() { NameAddress = new Name() { CustomerName = "zhangsanOrder", CustomerAddress = "北京 海淀" }, CustomerID = Guid.NewGuid() };
            customer.Orders = new HashSet<Order>();
            customer.Orders.Add(new Order() { OrderID = Guid.NewGuid(), OrderDate = DateTime.Now, Customer = customer });
            customer.Orders.Add(new Order() { OrderID = Guid.NewGuid(), OrderDate = DateTime.Now, Customer = customer });
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            if (customerBusiness.AddCustomer(customer))
            {
                this.RepeaterDataBind();
            }
        }
        /// <summary>
        /// 按客户id查询客户信息及订单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchByID_Click(object sender, EventArgs e)
        {
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            this.rptCustomerList.DataSource = customerBusiness.GetCustomerOrdersByProjection(new Guid("B0720295-9541-40B3-9994-610066224DB8"));
            this.rptCustomerList.DataBind();
        }

        protected void btnOrderProduct_Click(object sender, EventArgs e)
        {
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            this.rptCustomerList.DataSource = customerBusiness.GetCustomerOrderProductsByCriteriaAPI(new Guid("B0720295-9541-40B3-9994-610066224DB8"));
            this.rptCustomerList.DataBind();
        }

        protected void btnLazyLoad_Click(object sender, EventArgs e)
        {
            Business.CustomerBusiness customerBusiness = new Business.CustomerBusiness();
            Customer customer = customerBusiness.GetCustomerbyLazyLoad(new Guid("B0720295-9541-40B3-9994-610066224DB8"));
            this.rptCustomerList.DataSource = customer;
            this.rptCustomerList.DataBind();
        }
    }
}