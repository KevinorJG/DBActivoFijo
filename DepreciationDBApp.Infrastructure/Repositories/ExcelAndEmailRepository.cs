using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using EASendMail;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class ExcelAndEmailRepository : IExcelRepository
    {
        public void Create(ActivoEmpleado t)
        {
            
        }

        public void CreateExcel(ActivoEmpleado activoEmpleado)
        {
            try
            {
                string path = Path.GetFullPath("myexcel.xlsx");
                SLDocument document = new SLDocument();
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Columns.Add("Nombre del activo", typeof(string));
                dt.Columns.Add("Nombre del empleado", typeof(string));
                dt.Columns.Add("Codigo del activo", typeof(string));
                dt.Columns.Add("DNi del empleado", typeof(string));

                dt.Rows.Add(activoEmpleado.Activo.Nombre, activoEmpleado.Empleado.Dni, activoEmpleado.Activo.Codigo, activoEmpleado.Empleado.Dni);               

                document.ImportDataTable(2, 2, dt, true);
                document.SaveAs(path);


                SmtpMail mail = new SmtpMail("TryIt");

                mail.From = "kevinjair2003@gmail.com";
                mail.To = "kevinjair2003@gmail.com";
                mail.Subject = "Verificación de adquisición";
                mail.HtmlBody = $"<b>Informe de Activo Adquirido</b>";
                mail.AddAttachment(path);

                SmtpServer server = new SmtpServer("smtp.gmail.com");
                server.User = "kevinjair2003@gmail.com";
                server.Password = "uwpzjfrzrvjrprna";
                server.Port = 587;
                server.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.SendMail(server, mail);

            }
            catch
            {
                throw;
            }
        }

        public bool Delete(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }

        public List<ActivoEmpleado> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }
    }
}
