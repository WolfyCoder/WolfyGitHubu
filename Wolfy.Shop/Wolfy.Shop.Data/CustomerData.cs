using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfy.Shop.Domain.Entities;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Cfg;
using System.Linq.Expressions;
using NHibernate.Criterion;
namespace Wolfy.Shop.Data
{
    /// <summary>
    /// 描述：客户数据层类，操作数据库
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class CustomerData
    {
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns>是否添加成功 </returns>
        public bool AddCustomer(Customer customer)
        {

            try
            {

                var session = NHibernateHelper.GetSession();
                //将customer对象写入内存
                session.Save(customer);
                //更新到数据库
                session.Flush();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="customer">客户对象</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {

                var session = NHibernateHelper.GetSession();
                //Update the persistent instance with the identifier of the given transient instance.
                session.Update(customer);
                session.Flush();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 添加或者修改客户信息
        /// </summary>
        /// <param name="customer">客户对象</param>
        /// <returns>是否修改或添加成功成功</returns>
        public bool SaveOrUpdateCustomer(Customer customer)
        {
            try
            {

                var session = NHibernateHelper.GetSession();
                //Either Save() or Update() the given instance, depending upon the value of
                //its identifier property.
                session.SaveOrUpdate(customer);
                session.Flush();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 通过事务的方式添加或者修改
        /// </summary>
        /// <param name="customer">添加的对象</param>
        /// <returns>是否成功</returns>
        public bool SaveOrUpdateByTrans(Customer customer)
        {

            var session = NHibernateHelper.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(customer);
                    session.Flush();
                    //成功则提交
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    //出现异常，则回滚
                    transaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据条件得到客户信息集合
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>客户信息集合</returns>
        public IList<Customer> GetCustomerList(Expression<Func<Customer, bool>> where)
        {
            try
            {

                ISession session = NHibernateHelper.GetSession();
                return session.Query<Customer>().Where(where).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据客户姓名进行模糊查询
        /// </summary>
        /// <param name="strName">查询条件</param>
        /// <returns>满足条件的客户信息</returns>
        public IList<Customer> SearchByName(string strName)
        {

            ISession session = NHibernateHelper.GetSession();

            //from后面跟的是持久化类Customer而不是数据表名TB_Customer
            return session.CreateQuery("from Customer as customer where customer.CustomerName like '%" + strName + "%'").List<Customer>();

        }
        /// <summary>
        /// 根据姓名查询客户信息
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerByName(string strName)
        {

            ISession session = NHibernateHelper.GetSession();
            //方式一
            // return session.CreateQuery("from Customer c where c.CustomerName='" + strName + "'").List<Customer>();
            //方式二:位置型参数
            //return session.CreateQuery("from Customer c where c.CustomerName=?")
            //    .SetString(0, strName)
            //     .List<Customer>();
            //写法3:命名型参数(推荐)
            return session.CreateQuery("from Customer c where c.CustomerName=:cn")
                .SetString("cn", strName)
                .List<Customer>();

        }
        /// <summary>
        /// 获得所有客户的id
        /// </summary>
        /// <returns></returns>
        public IList<Guid> GetAllCustomerID()
        {

            ISession session = NHibernateHelper.GetSession();
            return session.CreateQuery("select customer.CustomerID from Customer as customer").List<Guid>();
        }
        /// <summary>
        /// 通过条件查询Criteria查询顾客信息
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetCustomers()
        {

            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            //通过ISession创建ICriteria实例
            ICriteria criteria = session.CreateCriteria(typeof(Customer));
            //查询名字以w开头的客户信息。
            criteria.Add(Restrictions.Like("CustomerName", "w%"));
            //另一种方式
            criteria.Add(Restrictions.Like("CustomerName", "w", MatchMode.Start));
            //查询名字在wolfy和zhangsan内的客户信息
            criteria.Add(Restrictions.In("CustomerName", new string[] { "zhangsan", "wolfy" }));
            //查询名称不为null的客户信息
            criteria.Add(Restrictions.IsNotNull("CustomerName"));
            //查询名称等于wolfy的客户
            criteria.Add(Restrictions.Eq("CustomerName", "wolfy"));
            //查地址是北京海淀区 并且名字为wolfy的客户
            criteria.Add(Restrictions.And(Restrictions.Like("CustomerAddress", "北京%"), Restrictions.Eq("CustomerName", "wolfy")));
            //按照名字升序排列
            criteria.AddOrder(NHibernate.Criterion.Order.Asc("CustomerName"));
            return criteria.List<Customer>();
        }
        /// <summary>
        /// 根据客户姓名和地址查找客户信息
        /// </summary>
        /// <param name="strCustomerName">姓名</param>
        /// <param name="strAddress">地址</param>
        /// <returns>客户信息集合</returns>
        public IList<Customer> GetCustomerAddress(string strCustomerName, string strAddress)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            return session.CreateQuery("from Customer as c where c.NameAddress.CustomerName=:cn and c.NameAddress.CustomerAddress=:ca")
                .SetString("cn", strCustomerName)
                .SetString("ca", strAddress)
                .List<Customer>();
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool DeleteCustomer(Customer customer)
        {

            var session = NHibernateHelper.GetSession();
            using (ITransaction trans = session.BeginTransaction())
            {
                try
                {
                    session.Delete(customer);
                    session.Flush();
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrders(Guid customerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            //实例化IQuery接口；使用ISession.CreateSQLQuery()方法，传递的参数是SQL查询语句
            return session.CreateSQLQuery("select distinct tb_customer.*,tb_order.* from tb_customer "
                + "inner join tb_order on tb_customer.customerid=tb_order.customerid where tb_customer.customerid=:id")
                .AddEntity("Customer", typeof(Customer))
                .SetGuid("id", customerID)
                .List<Customer>();
        }
        /// <summary>
        /// HQL查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByHQL(Guid customerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            return session.CreateQuery("select c from Customer c inner join c.Orders  where c.CustomerID=:id")
                .SetGuid("id", customerID)
                .List<Customer>();
        }
        /// <summary>
        /// Criteria API查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByCriteria(Guid customerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            return session.CreateCriteria(typeof(Customer))
                .CreateCriteria("Orders")
                .Add(Restrictions.Eq("Customer.CustomerID", customerID))
                //预过滤重复的结果
                .SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                //或者.SetResultTransformer(NHibernate.CriteriaUtil.DistinctRootEntity)
                .List<Customer>();
        }
        /// <summary>
        /// 投影查询某客户信息与该客户的订单信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrdersByProjection(Guid customerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            IList<Guid> ids = session.CreateCriteria(typeof(Customer))
                .SetProjection(Projections.Distinct(
                Projections.ProjectionList()
                .Add(Projections.Property("CustomerID"))
                )
                )
                .CreateCriteria("Orders")
        .Add(Restrictions.Eq("Customer.CustomerID", customerID))
        .List<Guid>();
            return session.CreateCriteria(typeof(Customer))
       .Add(Restrictions.In("CustomerID", ids.ToArray<Guid>()))
       .List<Customer>();
        }
        /// <summary>
        /// 通过sql查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsBySQL(Guid cutomerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            //使用inner join关联多个表进行查询
            return session.CreateSQLQuery("select distinct c.* from TB_Customer c" +
                                        " inner join TB_Order o on o.CustomerID=c.CustomerID" +
                                        " inner join TB_OrderProduct op on o.OrderID=op.OrderID" +
                                        " inner join TB_Product p on op.ProductID=p.ProductID where c.CustomerID=:CustomerID")
                //使用AddEntity设置返回的实体。
                                       .AddEntity("Customer", typeof(Customer))
                                       .SetGuid("CustomerID", cutomerID)
                                       .List<Customer>();
        }
        /// <summary>
        /// 通过HQL查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsByHQL(Guid cutomerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            //使用HQL基于面向对象的查询方式
            return session.CreateQuery("select distinct c from Customer c inner join c.Orders o inner join o.Products where c.CustomerID=:CustomerID")
                .SetGuid("CustomerID", cutomerID)
                .List<Customer>();
        }
        /// <summary>
        /// 通过HQL查询客户下的订单和产品信息
        /// </summary>
        /// <param name="cutomerID"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerOrderProductsByCriteriaAPI(Guid cutomerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            return session.CreateCriteria(typeof(Customer))
                .Add(Restrictions.Eq("CustomerID", cutomerID))
                .CreateCriteria("Orders")
                .CreateCriteria("Products")
                .List<Customer>();
        }
        /// <summary>
        /// 采用懒加载的方式根据客户id得到客户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer GetCustomerbyLazyLoad(Guid customerID)
        {
            //获得ISession实例
            ISession session = NHibernateHelper.GetSession();
            Customer customer = session.Get<Customer>(customerID);
            return customer;
        }
    }
}
