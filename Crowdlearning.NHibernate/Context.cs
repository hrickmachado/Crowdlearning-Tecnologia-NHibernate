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
using NHibernate.Mapping.ByCode;


namespace Crowdlearning
{
    [TestClass]
    public class Context
    {
        public static ISessionFactory SessionFactory;

        [AssemblyInitialize]
        public static void NHibernateConfiguration(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();

            Configuration cfg = new Configuration();
            // lendo o arquivo hibernate.cfg.xml
            cfg.Configure(); 

            // Mapeamento por código
            var mapper = new ModelMapper(); 
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            
            var mappingXMl = mapping.AsString();

            // Mapeamento por arquivo, in resource.
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            // Gerando o SessionFactory
            SessionFactory = cfg.BuildSessionFactory();
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
