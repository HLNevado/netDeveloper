using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BORME.Data;
namespace BORME.Libraries
{
    public class ClsUsers : CLSBase
    {
        public bool LoginUser(string _userLogin, string _pwd)
        {
            string _tmpPwd = ClsHashing.Compute(_pwd);
            var _exists = (from u in context.AppUsers
                           where u.UserLogin == _userLogin && u.Pwd == _tmpPwd && u.Active == true
                           select u).FirstOrDefault();
            if (_exists != null)
                return true;
            else
                return false;
        }
    }
}
