using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Crowdlearning.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Type;


namespace Crowdlearning
{
    [TestClass]
    public class Context
    {
        public static ISessionFactory SessionFactory;
        public static Configuration Configuration;

        [AssemblyInitialize]
        public static void NHibernateConfiguration(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();
            

            Configuration = new Configuration();
            // lendo o arquivo hibernate.cfg.xml
            Configuration.Configure();

            FilterDefinition filterDef = new FilterDefinition(
                "Empresa","EMPRESA = :EMPRESA",
                new Dictionary<string, IType>() {{"EMPRESA", NHibernateUtil.Int32}}, false);
            Configuration.AddFilterDefinition(filterDef);
            filterDef = new FilterDefinition(
                "Ativa", "ATIVO = 'Y'",
                new Dictionary<string, IType>(), false);
            Configuration.AddFilterDefinition(filterDef);


            // Mapeamento por código
            var mapper = new ModelMapper(); 
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            Configuration.AddMapping(mapping);
            
            // Gerar o XML a partir do mapeamento de codigo.
            //var mappingXMl = mapping.AsString();

            // Mapeamento por arquivo, in resource.
            Configuration.AddAssembly(Assembly.GetExecutingAssembly());

            
            // Gerando o SessionFactory
            SessionFactory = Configuration.BuildSessionFactory();
        }

        /// <summary>
        /// Faz a criação de tabelas na base de dados.
        /// Atenção: Os dados de tabelas já existentes podem ser perdidos.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CreateTablesForModelMapping()
        {
            new SchemaExport(Configuration).Create(false, true);
        }


        #region Configuração por Código
        public static void NHibernateConfigurationInBuild(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();
            
            Configuration cfg = new Configuration();
            
            cfg.SessionFactoryName("BuildIt");
            
            cfg.DataBaseIntegration(db =>
            {
                db.LogSqlInConsole = true;
                db.ConnectionString = @"Server=localhost;initial catalog=nhibernate;User Id=sa;Password=masterkey;";
                db.Driver<SqlClientDriver>();
                db.Dialect<MsSql2012Dialect>();
            });


            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            

            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            SessionFactory = cfg.BuildSessionFactory();
        }
        #endregion
    }
}
