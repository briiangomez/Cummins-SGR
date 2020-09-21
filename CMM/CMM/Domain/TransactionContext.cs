namespace CMM.Domain
{
    using CMM.IoC;
    using System;
    using System.Runtime.CompilerServices;

    public class TransactionContext : IDisposable
    {
        public TransactionContext()
        {
            ITransactionContext context = ContextContainer.Current.Resolve<ITransactionContext>();
            if (!context.IsStarted)
            {
                this.CurrentTransactionContext = context;
                this.CurrentTransactionContext.Begin();
            }
        }

        public void Commit()
        {
            if (this.CurrentTransactionContext != null)
            {
                this.HasCommitted = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((disposing && this.HasCommitted) && (this.CurrentTransactionContext != null))
            {
                try
                {
                    this.CurrentTransactionContext.Commit();
                }
                catch
                {
                    this.CurrentTransactionContext.Rollback();
                    throw;
                }
                finally
                {
                    this.CurrentTransactionContext = null;
                    this.HasCommitted = false;
                }
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~TransactionContext()
        {
            this.Dispose(false);
        }

        public void Rollback()
        {
            if (this.CurrentTransactionContext != null)
            {
                this.CurrentTransactionContext.Rollback();
            }
        }

        private ITransactionContext CurrentTransactionContext { get; set; }

        private bool HasCommitted { get; set; }
    }
}

