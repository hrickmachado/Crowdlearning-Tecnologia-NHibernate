using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Classic;

namespace Crowdlearning.Demo1
{
    [TestClass]
    public class Demostracao
    {
        [TestMethod]
        [ExpectedException(typeof (ValidationFailure))]
        public void ValidacaoPais()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Pais pais = new Pais();
                pais.Nome = "Br";

                session.Save(pais);

                session.Flush();
            }
        }
        [TestMethod]
        public void CrudTestPais()
        {
            Int32 paisID = 0;
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Pais pais = new Pais();
                pais.Nome = "Azerbaijão";

                session.Save(pais);

                session.Flush();

                paisID = pais.Handle;
            }

            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Pais alteracaoPais = session.Get<Pais>(paisID);
                alteracaoPais.Nome = "Tailândia";
                session.Update(alteracaoPais);
                session.Flush();
            }

            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Pais alteracaoPais = session.Get<Pais>(paisID);
                session.Delete(alteracaoPais);
                session.Flush();
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ObjectNotFoundException))]
        public void ExemploLoad()
        {
            ISession session = Context.SessionFactory.OpenSession();
            var paisInexistente = session.Load<Pais>(9876);

            if (paisInexistente != null) // always true
            {
                var nomePais = paisInexistente.Nome; // ObjectNotFoundException 
            }
        }

        [TestMethod]
        public void ExemploGet()
        {
            ISession session = Context.SessionFactory.OpenSession();
            var paisInexistente = session.Get<Pais>(9876);

            if (paisInexistente != null) // null if not exists
            {
                var nomePais = paisInexistente.Nome; // ObjectNotFoundException 
            }
        }

        [TestMethod]
        public void SalvandoDependencia()
        {
            Estado estado = new Estado();
            estado.Nome = "Santa Catarina";
            estado.Pais = new Pais();
            estado.Pais.Nome = "Brasil";

            using (ISession session = Context.SessionFactory.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    session.SaveOrUpdate(estado.Pais);
                    session.Save(estado);
                    session.Flush();
                    session.Transaction.Commit();
                }
                catch (Exception)
                {
                    session.Transaction.Rollback();
                    throw;
                }
            }
        }

        [TestMethod]
        public void RecuperandoAssociacoes()
        {
            ISession session = Context.SessionFactory.OpenSession();
            Estado estado = session.Get<Estado>(1);

            if (estado.Pais != null)
            {
                var nomePais = estado.Pais.Nome;
            }

            int qtdMunicipios = estado.Cidades.Count;
            Assert.IsTrue(qtdMunicipios > 0);

            foreach (var municipio in estado.Cidades)
            {
                var nomeMunicipios = municipio.Nome;
            }
        }
    }
}
