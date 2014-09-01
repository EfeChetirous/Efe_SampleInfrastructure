using System;
using System.Text;

namespace Infrastructure.Service
{
    public abstract class ServiceBase
    {
        protected T ExecuteExceptionHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (AggregateException ex)
            {
                ex = ex.Flatten();
                var exceptionMessages = new StringBuilder();
                for (int index = 0; index < ex.InnerExceptions.Count; index++)
                {
                    var innerException = ex.InnerExceptions[index];
                    exceptionMessages.AppendFormat("{0} : {1}", index + 1, innerException.Message);
                }
                throw new Exception(String.Format("One or more error(s) occured. Error details: {0}", exceptionMessages));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ExecuteExceptionHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AggregateException ex)
            {
                ex = ex.Flatten();
                var exceptionMessages = new StringBuilder();
                for (int index = 0; index < ex.InnerExceptions.Count; index++)
                {
                    var innerException = ex.InnerExceptions[index];
                    exceptionMessages.AppendFormat("{0} : {1}", index + 1, innerException.Message);
                }
                throw new Exception(String.Format("One or more error(s) occured. Error details: {0}", exceptionMessages));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}