using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NHibernate.Classic;
using NHibernate.Event;

namespace Crowdlearning.Model
{
    public class Pais : IValidatable
    {
        private IList<Estado> _estados = new List<Estado>();
        
        public virtual int Handle { get; set; }
        public virtual string Nome { get; set; }

        public virtual IList<Estado> Estados
        {
            get { return _estados; }
            set { _estados = value; }
        }


        public virtual void Validate()
        {
            if (Nome.Length <= 2)
                throw new ValidationFailure("Nome não pode ser melhor que 2.");
        }
    }

   
}