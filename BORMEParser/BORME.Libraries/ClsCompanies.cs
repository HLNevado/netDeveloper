using BORME.Data;
using System;
using System.Data;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Linq;
using System.Collections.Generic;

namespace BORME.Libraries
{
    public class ClsCompanies : ClsBase
    {
        public void ProcessDataDwn(DataTable _BORMEData, DateTime _BORMEDate, string _urlBase, int _userId)
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
                            ProcessDates _newProcessDate = new ProcessDates();
                            _newProcessDate.ProcessDate = _BORMEDate;
                            context.ProcessDates.Add(_newProcessDate);
                            context.SaveChanges();
                            foreach (DataRow _row in _BORMEData.Rows)
                            {
                                string _provinceCode = _row.ItemArray[2].ToString().Substring((_row.ItemArray[2].ToString().Length - 2), 2);
                                var _province = (from p in context.ProvinceCatalog
                                                 where p.ProvinceCode == _provinceCode
                                                 select p).FirstOrDefault();
                                if (_province != null)
                                {
                                    int _ProvinceId = _province.IdProvince;
                                    string _fileName = _row.ItemArray[2].ToString() + ".pdf";
                                    string _urlPDF = _urlBase + _BORMEDate.ToString("yyyy") + "/" + _BORMEDate.ToString("MM") + "/" + _BORMEDate.ToString("dd") + "/" + "pdfs/" + _fileName;
                                    
                                    // TO DO:
                                    // Descargar el archivo
                                    // Extraer el texto
                                    // Añadir registros a DB
                                    //Companies _newCompany = new Companies();
                                    //_newCompany.CompanyName = "";
                                    //_newCompany.ProvinceCatalogId = _ProvinceId;
                                    //_newCompany.ProcessDateId = _newProcessDate.IdDate;
                                    //_newCompany.CreationDate = DateTime.Now;
                                    //_newCompany.CreationUserId = _userId;

                                }
                                else
                                    throw new Exception("Ha ocurrido un error al procesar los datos");
                            }
                            context.SaveChanges();
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
        public List<vw_Companies> SearchCompanies(string _provinceCode, string _companyName, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                var _data = (from v in context.vw_Companies
                             select v);
                if (!string.IsNullOrEmpty(_companyName.Trim()))
                    _data = _data.Where(s => s.CompanyName.Contains(_companyName.Trim()));
                _data = _data.Where(s => s.ProcessDate >= _dateFrom && s.ProcessDate <= _dateTo);
                if (_provinceCode != "99")
                {
                    int _provinceId = (from p in context.ProvinceCatalog
                                       where p.ProvinceCode == _provinceCode
                                       select p.IdProvince).First();
                    _data = _data.Where(s => s.IdProvince == _provinceId);
                }
                return _data.OrderBy(o => o.ProvinceName).ThenBy(o => o.CompanyName).ThenByDescending(o => o.ProcessDate).ToList<vw_Companies>();
            }
            catch (Exception)
            {
                throw new Exception("Error al acceder a los datos");
            }
        }
    }
}
