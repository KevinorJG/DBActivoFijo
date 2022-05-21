using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Applications.Services;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepreciationDBApp.Presentation.Forms
{
    public partial class FormAsignar : Form
    {
        private IAssetEmployeeServices assetEmployeeServices;
        private IAssetService assetService;
        private IEmployeeServices employeeServices;
        private IExcelServices excelServices;
        public FormAsignar(IAssetEmployeeServices assetEmployeeServices,IAssetService assetService,IEmployeeServices employeeServices, IExcelServices excelServices)
        {
            this.assetEmployeeServices = assetEmployeeServices;
            this.assetService = assetService;
            this.employeeServices = employeeServices;
            this.excelServices = excelServices;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivoEmpleado activoEmpleado = new ActivoEmpleado()
            {
                ActivoId = int.Parse(textBoxIDAsset.Text),
                EmpleadoId = int.Parse(textBoxEmployee.Text),
                Date = DateTime.Now,
                IsActive = true,
                Activo = assetService.FindById(int.Parse(textBoxIDAsset.Text)),
                Empleado = employeeServices.FindByDni(textBoxEmployee.Text)
                                
            };
            excelServices.CreateExcel(activoEmpleado);
            assetEmployeeServices.Create(activoEmpleado);
            LoadData();

        }
        private void LoadData() => dataGridView1.DataSource = assetEmployeeServices.GetAll();

        private void FormAsignar_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
