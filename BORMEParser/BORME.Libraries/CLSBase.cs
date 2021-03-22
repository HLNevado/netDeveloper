using BORME.Data;
using System.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORME.Libraries
{
    public class CLSBase
    {
        protected BORMEDataBaseEntities context = null;

        public CLSBase()
        {
            context = new BORMEDataBaseEntities();
            context.Configuration.LazyLoadingEnabled = false; // para no traernos todo el árbol de entidades relacionadas

        }
    }
}
