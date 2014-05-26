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
    public class FiltroAutomatico
    {
        [TestMethod]
        public void FiltroEmpresaCorrente()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                // Setar Empresa Currente.
                session.EnableFilter("Empresa").SetParameter("EMPRESA", 5);


                ICriteria criteria = session.CreateCriteria(typeof(Documento));
                var documentos = criteria.List<Documento>();
                foreach (var documento in documentos)
                {
                    Assert.AreEqual(documento.Empresa.Handle, 5);
                }
            }

        }

        [TestMethod]
        public void FiltroEmpresasAtivas()
        {
            ISession session = Context.SessionFactory.OpenSession();
            session.EnableFilter("Ativa");

            var criteria = session.CreateCriteria(typeof(Empresa));
            var empresas = criteria.List();
        }
    }
}
