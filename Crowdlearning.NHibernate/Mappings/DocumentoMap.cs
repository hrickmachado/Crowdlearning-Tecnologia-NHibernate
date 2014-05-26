using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Crowdlearning.Mappings
{
    public class DocumentoMap : ClassMapping<Documento>
    {
        public DocumentoMap()
        {
            Table("DOCUMENTOS");
            Id(x => x.Handle,
               a =>
               {
                   a.Column("HANDLE");
                   a.Generator(Generators.Sequence);
               });
            Property(x => x.DocumentoDigito);
            Property(x => x.DataEmissao);
            Property(x => x.DataVencimento);
            Property(x => x.Observacoes);
            Property(x => x.Valor);
            Property(x => x.NumeroParcelas);
            
            
            ManyToOne(x => x.Empresa,
                 mapping =>
                {
                    mapping.Column("EMPRESA");
                });

            Filter("Empresa", f => f.Condition("EMPRESA = :EMPRESA"));
           
        }
    }
}
