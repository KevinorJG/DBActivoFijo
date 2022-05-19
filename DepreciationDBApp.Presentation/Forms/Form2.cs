using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using System.Threading;
using System.Windows.Forms;

namespace DepreciationDBApp.Presentation.Forms
{
    public partial class Form2 : Form
    {

        private IAssetService AssetService;
        private IEmployeeServices EmployeeServices;

        public Form2(IAssetService assetService, IEmployeeServices employeeServices)
        {
            AssetService = assetService;
            EmployeeServices = employeeServices;
            InitializeComponent();
        }

        private void Form2_Load(object sender, System.EventArgs e)
        {
            LoadEmployee();
        }

        private void LoadEmployee()
        {
            dgvEmployee.DataSource = EmployeeServices.GetAll();
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            FormClose();

        }
        public bool FormClose()
        {
            this.Dispose();
            return true;
        }

        private void btnCreate_Click(object sender, System.EventArgs e)
        {
            Empleado empleado = new Empleado()
            {
                Nombre = txtNames.Text,
                Apellido = txtLastNames.Text,
                Direccion = txtAddress.Text,
                Email = txtEmail.Text,
                Dni = txtDNI.Text,
                Telefono = txtPhone.Text,
                Status = "Activo"
            };

            EmployeeServices.Create(empleado);
            LoadEmployee();
        }
    }
}
