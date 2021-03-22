using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BORME.Libraries
{
    public static class ClsHashing
    {		public static string Compute(string strSource)
		{
			byte[] arrBytTarget;
			MD5 objMD5 = new MD5CryptoServiceProvider();
			// Codifica la cadena
			arrBytTarget = objMD5.ComputeHash(ASCIIEncoding.Default.GetBytes(strSource));
			// Convierte los bytes codificados en una cadena legible
			return BitConverter.ToString(arrBytTarget).Replace("-", "");
		}
	}
}
