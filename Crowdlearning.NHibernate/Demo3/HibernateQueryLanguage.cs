using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Crowdlearning.Demo3
{
    [TestClass]
    public class HibernateQueryLanguage 
    {
        [TestMethod]
        public void Test1()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQuery q = session.CreateQuery("from Estado estado where estado.Nome = :nome");
                q.SetParameter("nome", "Santa Catarina");
                q.SetMaxResults(1);
                Estado estado = q.UniqueResult<Estado>();
            }
        }


        // Join
        [TestMethod]
        public void Test2()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQuery q = session.CreateQuery(
                    "from Estado estado join estado.Pais p " +
                    "where estado.Nome is not null and p.Nome = :nomePais");
                q.SetString("nomePais", "Brasil");
                var estadoPais = q.List();
                foreach (Object[] objects in estadoPais)
                {
                    Estado estado = (Estado)objects[0];
                    Pais pais = (Pais)objects[1];
                }
            }
        }

        // UniqueResult - count(*)
        [TestMethod]
        public void Test3()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQuery q = session.CreateQuery("select count(*) from Estado");
                var quantidadeEstado = q.UniqueResult<long>();
                Console.WriteLine("Quantidade estados: {0}", quantidadeEstado);
            }
        }


        // Retornando valores parciais. 
        [TestMethod]
        public void Test4()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQuery q = session.CreateQuery("select e.Nome from Estado e where e.Sigla = :sigla");
                q.SetParameter("sigla", "SC");
                var nomeEstado = q.UniqueResult<string>();
                Console.WriteLine("Estado com sigla SC = {0}", nomeEstado);
            }
        }
    }
}
