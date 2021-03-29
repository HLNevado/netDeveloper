using BORME.Data;
using System;
using System.Data;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace BORME.Libraries
{
    public class ClsUtils : ClsBase
    {
        public bool CheckData(DateTime _BORMEDate)
        {
            var _isProcessed = (from d in context.ProcessDates
                           where d.ProcessDate == _BORMEDate
                           select d).FirstOrDefault();
            if (_isProcessed != null)
                return true;
            else
                return false;
        }
        public void DeleteProcessData(DateTime _BORMEDate)
        {
            try
            {
                using (context)
                {
                    TransactionOptions transOp = new TransactionOptions();
                    transOp.IsolationLevel = IsolationLevel.Serializable;
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOp))
                    {
                        try
                        {
                            ProcessDates _data = (from p in context.ProcessDates
                                                  where p.ProcessDate == _BORMEDate
                                                  select p).FirstOrDefault();
                            if (_data != null)
                            {
                                context.Companies.RemoveRange(context.Companies.Where(d => d.ProcessDateId == _data.IdDate));
                                context.ProcessDates.Remove(_data);
                                context.SaveChanges();
                            }
                            transaction.Complete();
                        }
                        catch (Exception)
                        {
                            transaction.Dispose();
                            throw new Exception("Ha ocurrido un error al procesar los datos");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error al procesar los datos");
            }
            
        }
    }
}
