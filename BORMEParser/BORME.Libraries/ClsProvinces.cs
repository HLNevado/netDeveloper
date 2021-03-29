using System.Collections.Generic;
using System.Linq;
using BORME.Data;
namespace BORME.Libraries
{
    public class ClsProvinces : ClsBase
    {
        public List<ProvinceCatalog> GetProvinces()
        {
            List<ProvinceCatalog> _lstProvince = (from p in context.ProvinceCatalog
                                                  select p).OrderBy(o => o.ProvinceCode).ToList();
            return _lstProvince;
        }
    }
}
