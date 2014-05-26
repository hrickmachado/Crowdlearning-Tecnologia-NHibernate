using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdlearning.Model;
using NHibernate.Cfg.Loquacious;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Crowdlearning.Mappings
{
    public class EstadoMap : ClassMapping<Estado>
    {
        public EstadoMap()
        {
            // :TODO BuscandoEmLote
            BatchSize(10);
            Table("ESTADOS");
            Id(x => x.Handle,
                a =>
                {
                    a.Column("HANDLE");
                    a.Generator(Generators.Sequence, g => g.Params(new
                    {
                        sequence = "hibernate_sequence"
                    }));
                });

            Property(x => x.Nome,
                m =>
                {
                    m.Column("NOME");
                    m.NotNullable(true);
                });
            Property(x => x.Sigla,
                m =>
                {
                    m.Column("SIGLA");
                    m.Length(2);
                });
            ManyToOne(x => x.Pais,
                mapping =>
                {
                    mapping.Column("PAIS");
                    mapping.Class(typeof(Pais));
                    mapping.NotNullable(true);
                });
             
            Bag(estado => estado.Cidades,
                mapping =>
                {
                    mapping.Cascade(Cascade.All);
                    mapping.Inverse(true);
                    mapping.Key(k => k.Column("ESTADO"));
                    // :TODO BuscandoEmLote
                    mapping.BatchSize(5);
                }, relation => relation.OneToMany()
                );
        }
    }
}