using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Linq;

namespace Crowdlearning
{
    [TestClass]
    public class LinkToSQLTest
    {
        [TestMethod]
        public void PesquisaEstadoPorNome()
        {
            ISession session = Context.SessionFactory.OpenSession();
            Estado estado = (from e in session.Query<Estado>() where e.Nome == "Santa Catarina" select e).FirstOrDefault();
            Assert.AreEqual(estado.Nome, "Santa Catarina");
        }

        [TestMethod]
        public void PesquisaEstadoPeloNomeDoPais()
        {
            ISession session = Context.SessionFactory.OpenSession();
            Estado estado = (session.Query<Estado>().Where(e => e.Pais.Nome == "Brasil")).FirstOrDefault();
            Assert.AreEqual(estado.Pais.Nome, "Brasil");
        }


        [TestMethod]
        public void PesquisaListaDeEstadoPeloNomeDoPais()
        {
            ISession session = Context.SessionFactory.OpenSession();
            IQueryable<Estado> estados = (session.Query<Estado>().Where(e => e.Pais.Nome == "Brasil"));
            
            foreach (var estado in estados)
            {
                Assert.AreEqual(estado.Pais.Nome, "Brasil");   
            }
        }
    }
}
