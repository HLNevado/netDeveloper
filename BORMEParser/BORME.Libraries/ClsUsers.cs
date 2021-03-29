using System.Linq;
namespace BORME.Libraries
{
    public class ClsUsers : ClsBase
    {
        public int LoginUser(string _userLogin, string _pwd)
        {
            string _tmpPwd = ClsHashing.Compute(_pwd);
            var _exists = (from u in context.AppUsers
                           where u.UserLogin == _userLogin && u.Pwd == _tmpPwd && u.Active == true
                           select u).FirstOrDefault();
            if (_exists != null)
                return _exists.IdUser;
            else
                return 0;
        }
    }
}
