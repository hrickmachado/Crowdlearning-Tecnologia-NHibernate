using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdlearning.Model
{
    public class Documento
    {
        public virtual int Handle { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual String DocumentoDigito { get; set; }
        public virtual DateTime DataEmissao { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual Int32 NumeroParcelas { get; set; }
        public virtual Decimal Valor { get; set; }
        public virtual string Observacoes { get; set; }
    }
}
