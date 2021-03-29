using System;
using System.Windows.Forms;
using BORMEParser.Utilities;
using BORME.Libraries;

namespace BORMEParser
{
    public partial class FormBORME : Form
    {
        public FormBORME()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ClsUtils().CheckData(dateTimePicker1.Value.Date))
                {
                    DialogResult _oDlgRes;
                    _oDlgRes = MessageBox.Show("El día seleccionado ya ha sido procesado.\nSi continúa se borrarán los datos anteriormente cargados.\n¿Desea continuar ? ", "ATENCION!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (_oDlgRes == DialogResult.Yes)
                    {
                        new ClsUtils().DeleteProcessData(dateTimePicker1.Value.Date);
                        if (!Utils.ProcessBORMEData(dateTimePicker1.Value.Date, LoginInfo.UserId))
                        {
                            MessageBox.Show("No hay datos para la fecha seleccionada", "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Se han procesado los datos de la fecha: " + dateTimePicker1.Value.ToString("dd/MM/yyyy"), "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (!Utils.ProcessBORMEData(dateTimePicker1.Value.Date, LoginInfo.UserId))
                    {
                        MessageBox.Show("No hay datos para la fecha seleccionada", "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Se han procesado los datos de la fecha: " + dateTimePicker1.Value.ToString("dd/MM/yyyy"), "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error al procesar los datos", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
