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
                SLStyle style = new SLStyle();
                SLStyle rowHeaderStyle = new SLStyle();
                rowHeaderStyle.SetVerticalAlignment(DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center);
                rowHeaderStyle.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left);

                System.Data.DataTable dt = new System.Data.DataTable();
                
                //Columnas
                document.SetCellValue("B2", "Nombre del Activo");
                document.SetCellValue("C2", "Nombre del Empleado");
                document.SetCellValue("D2", "Código del activo");
                document.SetCellValue("E2", "DNI del empleado");
                //Filas
                document.SetCellValue("B3",activoEmpleado.Activo.Nombre);
                document.SetCellValue("C3",activoEmpleado.Empleado.Nombre);
                document.SetCellValue("D3",activoEmpleado.Activo.Codigo);
                document.SetCellValue("E3",activoEmpleado.Empleado.Dni);

                document.SetRowStyle(2, rowHeaderStyle);
                //Estilo de las celdas 
                style.Font.Bold = true;
                style.Font.FontSize = 16;
                style.Border.Outline = true;
                //tamaño de ancho de columnas
                document.SetColumnWidth(2, 24.14);
                document.SetColumnWidth(3, 29.00);
                document.SetColumnWidth(4, 22.71);
                document.SetColumnWidth(5, 23.0);
                //

                document.SetCellStyle("B2", style);
                document.SetCellStyle("C2", style);
                document.SetCellStyle("D2", style);
                document.SetCellStyle("E2", style);
                document.SaveAs(path);


                SmtpMail mail = new SmtpMail("TryIt");

                mail.From = "kevinjair2003@gmail.com";
                mail.To = activoEmpleado.Empleado.Email;
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
