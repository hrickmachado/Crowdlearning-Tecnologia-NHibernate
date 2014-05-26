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
    public class BuscandoEmLoteTest
    {
        [TestMethod]
        public void BuscaClasse()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IList<Cidade> cidades = new List<Cidade>();
                cidades.Add(session.Get<Cidade>(4355));
                cidades.Add(session.Get<Cidade>(1120));
                cidades.Add(session.Get<Cidade>(139));
                cidades.Add(session.Get<Cidade>(5368));
                cidades.Add(session.Get<Cidade>(800));
                cidades.Add(session.Get<Cidade>(5507));

                foreach (var cidade in cidades)
                {
                    string nomeEstado = cidade.Estado.Nome;
                    Console.WriteLine("Estado: {0} - Cidade: {1}", nomeEstado, cidade.Nome);
                }
            }
        }
        [TestMethod]
        public void BuscaColecoes()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                IQuery query = session.CreateQuery("from Estado");
                query.SetMaxResults(10);
                IList<Estado> estados = query.List<Estado>();

                foreach (var estado in estados)
                {
                    foreach (var municipio in estado.Cidades)
                    {
                        string nomeMunicipio = municipio.Nome;
                    }
                    Console.WriteLine("Estado: {0} - Cidade: {1}", estado.Nome, estado.Cidades.Count());
                    Console.WriteLine("");
                }
            }
        }
    }
}
