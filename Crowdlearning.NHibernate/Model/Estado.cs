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
        public virtual Pais Pais { get; set; }

        private IList<Municipio> _municipios = new List<Municipio>();
        public virtual IList<Municipio> Municipios
        {
            get { return _municipios; }
            set { _municipios = value; }
        }
    }
}
