using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using NHibernate.Engine;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Crowdlearning.Mappings
{
    public class EmpresaMap : ClassMapping<Empresa>
    {
        public EmpresaMap()
        {
            Table("EMPRESAS");
            Id(x => x.Handle,
               a =>
               {
                   a.Column("HANDLE");
                   a.Generator(Generators.Sequence);
               });
            Property(x => x.Nome);
            Property(x => x.NomeFantasia);
            Property(x => x.Ativa,
                m=>m.Type(new YesNoType())
                );
            ManyToOne(x => x.EmpresaMestre,
                 mapping =>
                {
                    mapping.Column("EMPRESA");
                    mapping.Class(typeof(Empresa));
                });
            Filter("Ativa",f =>f.Condition("ATIVA = 'Y'"));
        }
    }
}
