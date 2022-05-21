using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Infrastructure.Repositories;
using DepreciationDBApp.Presentation.Forms;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DepreciationDBApp.Forms
{
    public partial class Form1 : Form
    {
        private IAssetService assetService;
        private IEmployeeServices employeeServices;
        private IAssetEmployeeServices assetemployeeService;
        private IExcelServices excelServices;
        public static int idActivo = 0;
        private string code = String.Empty;
        private bool borrado = false;

        public Form1(IAssetService assetService, IEmployeeServices employeeServices,IAssetEmployeeServices assetEmployeeServices, IExcelServices excelServices)
        {
            this.assetService = assetService;
            this.employeeServices = employeeServices;
            this.assetemployeeService = assetEmployeeServices;
            this.excelServices = excelServices;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            labelCodigo.Visible = false;
            labelCode.Visible = false;
            buttonClear.Enabled = false;
            buttonUpdate.Enabled = false;
        }

        private void btnAddAsset_Click(object sender, EventArgs e)
        {
            IEnumerable<String> value = from item in assetService.GetAll()
                                        select item.Codigo;

            if (value.Contains(labelCodigo.Text))
            {
                MessageBox.Show("Este registro ya existe", "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ActivoFijo asset = new ActivoFijo()
                {
                    Nombre = textBoxNombre.Text,
                    Descripcion = textBoxDescripcion.Text,
                    ValorActivo = decimal.Parse(textBoxValorActivo.Text),
                    ValorResidual = decimal.Parse(textBoxValorRe.Text),
                    VidaUtil = int.Parse(textBoxVida.Text),
                    Codigo = GeneradorCodigo(),
                    Estado = comboBoxEstado.SelectedItem.ToString(),
                    EsActivo = true
                };
                if (decimal.Parse(textBoxValorRe.Text) > decimal.Parse(textBoxValorActivo.Text))
                {
                    MessageBox.Show("No debe de ser mayor que el valor del activo");
                }
                else
                {
                    assetService.Create(asset);                  
                    Clean();
                    LoadData();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Value = Interaction.InputBox("Ingrese el ID a buscar");
            if (!String.IsNullOrEmpty(Value))
            {
                int id = Convert.ToInt32(Value);
                ViewActivo(id);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea realizar el cambio?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var activo = assetService.FindById(idActivo);

                activo.Nombre = textBoxNombre.Text;
                activo.Descripcion = textBoxDescripcion.Text;
                activo.ValorResidual = decimal.Parse(textBoxValorRe.Text);
                activo.Estado = comboBoxEstado.SelectedItem.ToString();
                activo.EsActivo = true;

                assetService.Update(activo);
            }
            buttonClear.Enabled = false;
            buttonUpdate.Enabled = false;
            Clean();
            LoadData();
        }
        private void textBoxValorRe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                errorProviderValorR.SetError(this.textBoxValorRe, "Caracter no válido");
                e.Handled = true;

            }
            else
            {
                errorProviderValorR.Clear();
            }

        }
        #region Metodos
        public void ViewActivo(int id)
        {

            IEnumerable<int> num = from item in assetService.GetAll()
                                   select item.Id;

            if (!num.Contains(id))
            {
                MessageBox.Show($"No se encuentra el ID {id} en la base de datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var activo = assetService.FindById(id);
                idActivo = activo.Id;
                textBoxNombre.Text = activo.Nombre;
                textBoxValorActivo.Text = Convert.ToString(activo.ValorActivo);
                textBoxValorRe.Text = Convert.ToString(activo.ValorResidual);
                textBoxVida.Text = Convert.ToString(activo.VidaUtil);
                textBoxDescripcion.Text = activo.Descripcion;
                comboBoxEstado.Text = activo.Estado;

                textBoxVida.Enabled = false;
                textBoxValorActivo.Enabled = false;
                labelCodigo.Visible = true;
                labelCodigo.Text = activo.Codigo;
                labelCode.Visible = true;
                buttonClear.Enabled = true;
                buttonUpdate.Enabled = true;
                buttonCancel.Enabled = true;

                LoadID(borrado);

            }

        }

        private void LoadData() => dgvAsset.DataSource = assetService.GetAll();

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Desea eliminar este registro?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var activo = assetService.FindById(idActivo);
                assetService.Delete(activo);
                buttonClear.Enabled = false;
                buttonUpdate.Enabled = false;
                Clean();
                LoadData();
                borrado = true;
                LoadID(borrado);
            }

        }
        public string GeneradorCodigo()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            return new String(Charsarr);
        }

        public void Clean()
        {
            textBoxVida.Clear();
            textBoxValorRe.Clear();
            textBoxValorActivo.Clear();
            textBoxNombre.Clear();
            textBoxDescripcion.Clear();
            comboBoxEstado.SelectedIndex = -1;
            labelCodigo.Visible = false;
            labelCode.Visible = false;
            textBoxVida.Enabled = true;
            textBoxValorActivo.Enabled = true;
        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            labelCodigo.ResetText();
            LoadID(true);
            Clean();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ThreadStart start = new ThreadStart(FormWindow);
            Thread thread = new Thread(start);
            thread.Start();          

        }

        public void FormWindow() => new Form2(assetService, employeeServices).ShowDialog();
        public void FormWindow2() => new FormAsignar(assetemployeeService,assetService,employeeServices,excelServices).ShowDialog();

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ThreadStart start = new ThreadStart(FormWindow2);
            Thread thread = new Thread(start);
            thread.Start();

        }
       
    }
}
