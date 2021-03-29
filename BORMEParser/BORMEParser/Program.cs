using BORMEParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatLoginWatermark
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormLogin _login = new FormLogin();
            _login.ShowDialog();
            if (_login.DialogResult == DialogResult.OK)
                Application.Run(new FormMain());
        }
    }
}
