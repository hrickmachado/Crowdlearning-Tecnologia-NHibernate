using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Crowdlearning.Demo2
{
    [TestClass]
    public class DemostracaoConsultas
    {
        [TestMethod]
        public void QueryTestHQL1()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQuery q = session.CreateQuery("from Pais");
            q.SetMaxResults(10);
            IList<Pais> paises = q.List<Pais>();
        }

        [TestMethod]
        public void QueryTestHQL2()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQuery q = session.CreateQuery("from Estado as estado join estado.Pais pais");
            var listObjects = q.List();
            foreach (Object[] objects in listObjects)
            {
                Estado estado = (Estado)objects[0];
                Pais pais = (Pais)objects[1];
            }
        }

        [TestMethod]
        public void QueryFiltroComHQL()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQuery q = session.CreateQuery("from Estado estado where estado.Nome = :nome");
            q.SetParameter("nome", "Santa Catarina");
            q.SetMaxResults(1);
            Estado estado = q.UniqueResult<Estado>();
        }


        [TestMethod]
        public void QueryPaginacao()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQuery q = session.CreateQuery("from Pais");
            q.SetFirstResult(10);
            q.SetMaxResults(10);
            IList<Pais> paises = q.List<Pais>();

            q.SetFirstResult(20);
            paises = q.List<Pais>();
        }

        [TestMethod]
        public void CriteriaTest()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IList municipiosList = session.CreateCriteria(typeof(Municipio))
                .Add(Expression.Or(
                    Expression.Eq("Nome", "Blumenau"),
                    Expression.Like("Nome", "Blu%")
                ))
                .List();
            Assert.IsNotNull(municipiosList);
        }

        [TestMethod]
        public void LambdaQuery()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQueryable<Estado> query = session.Query<Estado>();
            Estado estado = query.Where(e => e.Pais.Nome == "Brasil").FirstOrDefault();
            Assert.AreEqual(estado.Pais.Nome, "Brasil");
        }

        [TestMethod]
        public void LinqToSQL()
        {
            ISession session = Context.SessionFactory.OpenSession();
            var estados = from e in session.Query<Estado>() 
                          where e.Nome == "Santa Catarina" 
                          select e;

            foreach (var estado in estados)
            {
                Assert.AreEqual(estado.Nome, "Santa Catarina");   
            }
        }
    }
}
