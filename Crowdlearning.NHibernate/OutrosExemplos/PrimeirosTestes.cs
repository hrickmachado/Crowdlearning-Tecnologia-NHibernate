using System.Linq;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Criterion;

namespace Crowdlearning
{
    [TestClass]
    public class PrimeirosTestes
    {
        [TestMethod]
        public void Consulta()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Estado estado = session.Get<Estado>(1);
                if (estado != null)
                {
                    var nomePais = estado.Pais.Nome;
                    foreach (var municipio in estado.Cidades)
                    {
                        var nomeMunicipio = municipio.Nome;
                    }
                }
            }
        }

        [TestMethod]
        public void SaveDependency()
        {

            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Pais brasil = new Pais();
                brasil.Nome = "Brasil";

                Estado sc = new Estado();
                sc.Nome = "Santa Catarina";

                sc.Pais = brasil;

                brasil.Estados.Add(sc);

                session.SaveOrUpdate(brasil);
            }
        }

        [TestMethod]
        public void MappingByCode()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Cidade cidade = new Cidade();
                cidade.Nome = "Blumenau";

                ICriteria criteria = session.CreateCriteria(typeof(Estado));

                var estado = new Estado { Nome = "Santa Catarina" };
                criteria.Add(Example.Create(estado));

                cidade.Estado = criteria.List<Estado>().First();

                session.Save(cidade);
                session.Flush();
            }
        }

        [TestMethod]
        public void MappingByCodeDependency()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Cidade blumenau = new Cidade();
                blumenau.Nome = "Blumenau";

                Estado sc = new Estado();
                sc.Nome = "Santa Catarina";

                Pais brasil = new Pais();
                brasil.Nome = "Brasil";

                brasil.Estados.Add(sc);
                sc.Pais = brasil;

                sc.Cidades.Add(blumenau);
                blumenau.Estado = sc;

                session.Save(brasil);
                session.Flush();
            }
        }

        [TestMethod]
        public void SaveTest()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                var cidades = session.CreateCriteria<Cidade>().List();
            }
        }
    }
}
