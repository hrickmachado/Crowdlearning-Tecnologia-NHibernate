using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NHibernate.Event;

namespace Crowdlearning.Model
{
    public class Pais
    {
        private IList<Estado> _estados = new List<Estado>();
        
        public virtual int Handle { get; set; }
        public virtual string Nome { get; set; }

        public virtual IList<Estado> Estados
        {
            get { return _estados; }
            set { _estados = value; }
        }

        
    }

    public class ReprosityPais
    {
        public void Save(Pais pais)
        {
            if (pais.Nome.Length > 10)
                throw new Exception();
            Context.SessionFactory.OpenSession().SaveOrUpdate(pais);
        }
    }
}