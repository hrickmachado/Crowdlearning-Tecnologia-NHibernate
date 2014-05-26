using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Crowdlearning.Model
{
    public class Estado
    {
        public virtual int Handle { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Sigla { get; set; }
        public virtual Pais Pais { get; set; }

        private IList<Cidade> _cidades = new List<Cidade>();
        public virtual IList<Cidade> Cidades
        {
            get { return _cidades; }
            set { _cidades = value; }
        }
    }
}
