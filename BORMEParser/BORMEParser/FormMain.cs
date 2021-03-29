using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BORMEParser.Utilities;

namespace BORMEParser
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void OpenForm(object childForm)
        {
            if (this.containPanel.Controls.Count > 0)
                this.containPanel.Controls.RemoveAt(0);
            Form _fh = childForm as Form;
            _fh.TopLevel = false;
            _fh.Dock = DockStyle.Fill;
            this.containPanel.Controls.Add(_fh);
            this.containPanel.Tag = _fh;
            _fh.Show();
        }

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btnlogoInicio_Click(null,e);
            lbluser.Text = LoginInfo.UserLogon;
        }

        private void btnGetBORMEData_Click(object sender, EventArgs e)
        {
            OpenForm(new FormBORME());
        }

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            OpenForm(new FormSearch());
        }
    }
}
