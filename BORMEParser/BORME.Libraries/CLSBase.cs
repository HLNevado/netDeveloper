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
    public class ClsBase
    {
        protected BORMEDataBaseEntities context = null;

        public ClsBase()
        {
            context = new BORMEDataBaseEntities();
            context.Configuration.LazyLoadingEnabled = false; // Para no traernos todo el árbol de entidades relacionadas
        }
    }
}
