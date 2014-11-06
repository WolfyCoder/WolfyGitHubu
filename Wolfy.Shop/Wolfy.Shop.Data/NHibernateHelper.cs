using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfy.Shop.Data
{
    /// <summary>
    /// 描述：nhibernate辅助类
    /// 创建人：wolfy
    /// 创建时间：2014-10-16
    /// </summary>
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static object _objLock = new object();
        private NHibernateHelper()
        {

        }
        /// <summary>
        /// 创建ISessionFactory
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (_objLock)
                {
                    if (_sessionFactory == null)
                    {
                        //配置ISessionFactory
                        _sessionFactory = (new Configuration()).Configure().BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;

        }
        /// <summary>
        /// 打开ISession
        /// </summary>
        /// <returns></returns>
        public static ISession GetSession()
        {
            _sessionFactory = GetSessionFactory();
            if (_session == null)
            {
                lock (_objLock)
                {
                    if (_session == null)
                    {
                        _session = _sessionFactory.OpenSession();
                    }
                }
            }
            return _session;
        }
    }

}
