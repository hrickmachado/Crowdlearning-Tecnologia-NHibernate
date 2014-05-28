using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Classic;

namespace Crowdlearning.Demo3
{
    [TestClass]
    public class ValidatableTest
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationFailure))]
        public void ValidandoCriacaoPais()
        {
            try
            {
                using (ISession session = Context.SessionFactory.OpenSession())
                {
                    Pais pais = new Pais();
                    pais.Nome = "A";
                    session.Save(pais);
                    session.Flush();
                }
            }
            catch (ValidationFailure validationFailure)
            {
                Assert.AreEqual(validationFailure.Message, "Nome não pode ser melhor que 2.");
                throw;
            }
        }
    }
}
