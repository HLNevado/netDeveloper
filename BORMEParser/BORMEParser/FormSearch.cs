using System;
using System.Windows.Forms;
using BORME.Libraries;
using System.Linq;
using BORME.Data;
using System.Collections.Generic;
using System.Data;
using OfficeOpenXml;

namespace BORMEParser
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }
        private void FormSearch_Load(object sender, EventArgs e)
        {
            fillProvince();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var _data = new ClsCompanies().SearchCompanies(cmbProvinces.SelectedValue.ToString(), txtCompany.Text, dateTimeFrom.Value.Date, dateTimeTo.Value.Date);
                var _columns = from t in _data
                               select new
                               {
                                   Province = t.ProvinceName,
                                   Company = t.CompanyName,
                                   PubDate = t.ProcessDate
                               };
                dataGridView1.DataSource = _columns.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error al obtener los datos", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<vw_Companies> _data = new ClsCompanies().SearchCompanies(cmbProvinces.SelectedValue.ToString(), txtCompany.Text, dateTimeFrom.Value.Date, dateTimeTo.Value.Date);
            //var _export = (from d in _data
            //               select new
            //               {
            //                   ProvinceName = d.ProvinceName,
            //                   CompanyName = d.CompanyName,
            //                   ProcessDate = d.ProcessDate
            //               });
            //ExcelPackage _excel = new ExcelPackage();
            //var workSheet = _excel.Workbook.Worksheets.Add("Datos");
            //workSheet.Column(3).Style.Numberformat.Format = "dd/mm/yyyy";
            //workSheet.Cells[1, 1].LoadFromCollection(_export, true);
            //workSheet.Cells[1, 1].Value = "Provincia";
            //workSheet.Cells[1, 2].Value = "Nombre Empresa";
            //workSheet.Cells[1, 12].Value = "Fecha Publicación BORME";
            //workSheet.Row(1).Style.Font.Bold = true;
            //workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            //workSheet.Cells[workSheet.Dimension.Address].AutoFilter = true;
            //_excel.Save();
        }
        private void fillProvince()
        {
            List<ProvinceCatalog> _provinces = new ClsProvinces().GetProvinces();
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = _provinces;
            cmbProvinces.DataSource = bindingSource1.DataSource;
            cmbProvinces.DisplayMember = "ProvinceName";
            cmbProvinces.ValueMember = "ProvinceCode";
            cmbProvinces.SelectedIndex = cmbProvinces.FindString("Todas");
        }
    }
}
