using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Crowdlearning.Demo3
{
    [TestClass]
    public class CriteriaTest
    {
        [TestMethod]
        public void Exemplo()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.Add(Expression.Ge("Handle", 100));
                criteria.Add(Expression.Like("Nome", "A%"));

                IList cridades = criteria.List();
            }
        }

        [TestMethod]
        public void ExemploUniqueResult()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Cidade>();
                var blumenau = criteria.Add(Expression.Eq("Nome","Blumenau")).UniqueResult<Cidade>();
            }

        }

        // Ordenação
        [TestMethod]
        public void Exemplo1()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.Add(Expression.Ge("Handle", 100));
                criteria.Add(Expression.Like("Nome", "C%"));
                criteria.AddOrder(new Order("Estado", true));

                var cidades = criteria.List<Cidade>();
            }
        }

        // Join
        [TestMethod]
        public void Exemplo2()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                var cidades = session.CreateCriteria<Cidade>()
                    .CreateAlias("Estado", "e")
                    .Add(Expression.Like("Nome", "B%"))
                    .Add(Expression.Eq("e.Nome", "Santa Catarina"))
                    .AddOrder(new Order("e.Nome", true)).List<Cidade>();
            }
        }

        // Inner Join / Left Join / RightOuterJoin
        [TestMethod]
        public void Exemplo3()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.CreateCriteria("Estado", "e", 
                    JoinType.InnerJoin, new LikeExpression("e.Nome", "Sant%"));

                var cidades = criteria.List<Cidade>();
            }
        }

        // Sub Querys - DetachedCriteria
        [TestMethod]
        public void Exmplo4()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.Add(
                    Subqueries.PropertyIn("Estado",
                        DetachedCriteria.For<Estado>().Add(Expression.In("Sigla", new[] { "SC", "PR" }))
                        .SetProjection(Projections.Property("Handle"))
                    ));

                var cidades = criteria.List<Cidade>();
            }
        }


        // By sample class
        [TestMethod]
        public void Test5()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                // Buscar a cidade com os filtros
                Cidade cidade = new Cidade();
                cidade.Nome = "Blume%";

                var exampleCidade = Example.Create(cidade)
                    .EnableLike()
                    .ExcludeZeroes();

                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.Add(exampleCidade);

                var cidades = criteria.List<Cidade>();
            }
        }

        // By sample class exemplo join
        [TestMethod]
        public void Test6()
        {
            using (ISession session = Context.SessionFactory.OpenSession())
            {
                Cidade cidade = new Cidade();
                cidade.Estado = new Estado();
                cidade.Estado.Sigla = "SC"; // where SIGLA = 'SC'
                cidade.Nome = "B%"; // where NOME like 'B%'

                ICriteria criteria = session.CreateCriteria<Cidade>();
                criteria.Add(Example.Create(cidade).EnableLike());
                criteria.CreateCriteria("Estado").Add(Example.Create(cidade.Estado));

                var cidades = criteria.List<Cidade>();
            }
        }
    }
}
