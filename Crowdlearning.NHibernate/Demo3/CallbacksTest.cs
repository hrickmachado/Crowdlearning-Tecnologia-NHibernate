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
    public class CallbacksTest
    {
        [TestMethod]
        public void DesativarEmpresaNoDelete()
        {
            ISession session = Context.SessionFactory.OpenSession();
            Empresa empresa = new Empresa();
            empresa.Nome = "Empresa Ativa Teste";
            empresa.NomeFantasia = empresa.Nome;
            empresa.Ativa = true;
            session.Save(empresa);
            session.Flush();

            empresa = session.Get<Empresa>(empresa.Handle);
            session.Delete(empresa);
            session.Flush();

            empresa = session.Get<Empresa>(empresa.Handle);
            Assert.IsFalse(empresa.Ativa);

        }
    }
}
