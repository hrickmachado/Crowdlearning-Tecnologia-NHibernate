using NHibernate;
using NHibernate.Classic;

namespace Crowdlearning.Model
{
    public class Empresa : ILifecycle
    {
        #region Propriedades
        public virtual int Handle { get; set; }
        public virtual string Nome { get; set; }

        public virtual string NomeFantasia { get; set; }

        public virtual Empresa EmpresaMestre { get; set; }
        public virtual bool Ativa { get; set; }
        #endregion

        public virtual LifecycleVeto OnSave(ISession s)
        {
            return LifecycleVeto.NoVeto;
        }

        public virtual LifecycleVeto OnUpdate(ISession s)
        {
            return LifecycleVeto.NoVeto;
        }

        public virtual LifecycleVeto OnDelete(ISession s)
        {
            this.Ativa = false;
            s.SaveOrUpdate(this);
            return LifecycleVeto.Veto;
        }

        public virtual void OnLoad(ISession s, object id)
        {
            return;
        }
    }
}
