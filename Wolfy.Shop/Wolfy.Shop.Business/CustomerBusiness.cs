using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wolfy.Shop.Data;
using Wolfy.Shop.Domain.Entities;

namespace Wolfy.Shop.Business
{

    /// <summary>
    /// 描述：客户信息业务逻辑层
    /// 创建人：wolfy
    /// 创建时间：2014-10-17
    /// </summary>
    public class CustomerBusiness
    {
        private CustomerData _customerData;
        public CustomerBusiness()
        {
            _customerData = new CustomerData();
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns>是否添加成功 </returns>
        public bool AddCustomer(Customer customer)
        {
            return _customerData.AddCustomer(customer);
        }
        /// <summary>
        /// 根据条件得到客户信息集合
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>客户信息集合</returns>
        public IList<Customer> GetCustomerList(Expression<Func<Customer, bool>> where)
        {
            return _customerData.GetCustomerList(where);
        }
        /// <summary>
        /// 根据客户姓名进行模糊查询
        /// </summary>
        /// <param name="strName">查询条件</param>
        /// <returns>满足条件的客户信息</returns>
        public IList<Customer> SearchByName(string strName)
        {
            return _customerData.SearchByName(strName);
        }
        /// <summary>
        /// 根据姓名查询客户信息
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerByName(string strName)
        {
            return _customerData.GetCustomerByName(strName);
        }
        /// <summary>
        /// 获得所有客户的id
        /// </summary>
        /// <returns></returns>
        public IList<Guid> GetAllCustomerID()
        {
            return _customerData.GetAllCustomerID();
        }
        /// <summary>
        /// 通过条件查询Criteria查询顾客信息
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetCustomers()
        {
            return _customerData.GetCustomers();
        }
        /// <summary>
        /// 添加或者修改客户信息
        /// </summary>
        /// <param name="customer">客户对象</param>
        /// <returns>是否修改或添加成功成功</returns>
        public bool SaveOrUpdateCustomer(Customer customer)
        {
            return _customerData.SaveOrUpdateCustomer(customer);
        }
        /// <summary>
        /// 通过事务的方式添加或者修改
        /// </summary>
        /// <param name="customer">添加的对象</param>
        /// <returns>是否成功</returns>
        public bool SaveOrUpdateByTrans(Customer customer)
        {
            return _customerData.SaveOrUpdateByTrans(customer);
        }
        /// <summary>
        /// 根据客户姓名和地址查找客户信息
        /// </summary>
        /// <param name="strCustomerName">姓名</param>
        /// <param name="strAddress">地址</param>
        /// <returns>客户信息集合</returns>
        public IList<Customer> GetCustomerAddress(string strCustomerName, string strAddress)
        {
            return _customerData.GetCustomerAddress(strCustomerName, strAddress);
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool DeleteCustomer(Customer customer)
        {
            return _customerData.DeleteCustomer(customer);
        }
        /// <summary>
        /// 查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrders(Guid customerID)
        {
            return _customerData.GetCustomerOrders(customerID);
        }
        /// <summary>
        /// HQL查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByHQL(Guid customerID)
        {
            return _customerData.GetCustomerOrdersByHQL(customerID);
        }
        /// <summary>
        /// Criteria API查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByCriteria(Guid customerID)
        {
            return _customerData.GetCustomerOrdersByCriteria(customerID);
        }
        /// <summary>
        /// 投影查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByProjection(Guid customerID)
        {
            return _customerData.GetCustomerOrdersByProjection(customerID);
        }
        /// <summary>
        /// 通过sql查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsBySQL(Guid cutomerID)
        {
            return _customerData.GetCustomerOrderProductsBySQL(cutomerID);
        }
        /// <summary>
        /// 通过HQL查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsByHQL(Guid cutomerID)
        {
            return _customerData.GetCustomerOrderProductsByHQL(cutomerID);
        }
        /// <summary>
        /// 通过HQL查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsByCriteriaAPI(Guid cutomerID)
        {
            return _customerData.GetCustomerOrderProductsByCriteriaAPI(cutomerID);
        }
               /// <summary>
        /// 采用懒加载的方式根据客户id得到客户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer GetCustomerbyLazyLoad(Guid customerID)
        {
            return _customerData.GetCustomerbyLazyLoad(customerID);
        }
    }
}
