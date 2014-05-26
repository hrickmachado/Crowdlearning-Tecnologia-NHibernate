using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Linq;

namespace Crowdlearning.Demo3
{
    [TestClass]
    public class LambdaLinkToSQL
    {
        // Lista de estado.
        [TestMethod]
        public void Test1()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQueryable<Estado> query = session.Query<Estado>();
                var estados = query.Where(e => e.Pais.Nome == "Brasil").ToList();
                Assert.AreEqual(estados[0].Pais.Nome, "Brasil");
            }
        }
        
        // Select Top 1 (SQL SERVER)
        [TestMethod]
        public void Test2()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQueryable<Estado> query = session.Query<Estado>();
                Estado estado = query.Where(e => e.Pais.Nome == "Brasil").FirstOrDefault();
                Assert.AreEqual(estado.Pais.Nome, "Brasil");
            }
        }

        // Link to SQL
        [TestMethod]
        public void Test3()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                var estados = from e in session.Query<Estado>()
                    where e.Nome == "Santa Catarina"
                    select new {e.Nome, NomePais = e.Pais.Nome };

                foreach (var estado in estados)
                {
                    Assert.AreEqual(estado.Nome, "Santa Catarina");
                }
            }
        }
    }
}
