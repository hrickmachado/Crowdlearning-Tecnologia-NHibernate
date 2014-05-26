using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Criterion;

namespace Crowdlearning.Demo3
{
    [TestClass]
    public class MuiltCommandTest
    {
        [TestMethod]
        public void MultQueryTest()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IMultiQuery multiQuery = session.CreateMultiQuery()
                    .Add(session.CreateQuery("from Cidade c where c.Estado.Sigla = :sgla")
                        .SetFirstResult(10))
                    .Add(session.CreateQuery("select count(*) from Cidade c where c.Estado.Sigla = :sgla"))
                    .SetString("sgla", "SC");
                IList results = multiQuery.List();
                IList items = (IList) results[0];
                long count = (long) ((IList) results[1])[0];
            }
        }



        [TestMethod]
        public void MultCriteriaTest()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IMultiCriteria multiCrit = session.CreateMultiCriteria()
                    .Add(session.CreateCriteria(typeof (Cidade))
                        .Add(Expression.Like("Nome", "B%"))
                        .SetMaxResults(20))
                    .Add(session.CreateCriteria(typeof(Cidade))
                        .Add(Expression.Like("Nome", "B%"))
                        .SetProjection(Projections.RowCount()));
                IList results = multiCrit.List();
                IList items = (IList) results[0];
                int count = (int) ((IList) results[1])[0];
            }
        }
    }
}
