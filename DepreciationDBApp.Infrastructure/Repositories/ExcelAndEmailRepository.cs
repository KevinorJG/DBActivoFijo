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
using DocumentFormat.OpenXml.Spreadsheet;

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
                SLStyle ColumnsAsset = new SLStyle();
                SLStyle RowAsset = new SLStyle();
                

                System.Data.DataTable dt = new System.Data.DataTable();
                
                //Columnas y estilos
                document.SetCellValue("B2", "Nombre del Activo");
                document.SetCellValue("C2", "Nombre del Empleado");
                document.SetCellValue("D2", "Código del activo");
                document.SetCellValue("E2", "DNI del empleado");

                ColumnsAsset.Font.Bold = true;
                ColumnsAsset.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Yellow, System.Drawing.Color.Black);//Color de las columnas en B
                document.SetCellStyle("B6", ColumnsAsset);
                document.SetCellStyle("B8", ColumnsAsset);
                document.SetCellStyle("B9", ColumnsAsset);
                document.SetCellStyle("B10", ColumnsAsset);

                document.SetCellValue("B6", "Descripción del Activo");
                document.SetCellValue("B8", "Valor del activo");
                document.SetCellValue("B9", "Vida util");
                document.SetCellValue("B10", "Descripción");

                //Filas
                RowAsset.Alignment.Horizontal = HorizontalAlignmentValues.Center; //Centrado de las filas
                document.SetCellValue("C8", activoEmpleado.Activo.ValorActivo);
                document.SetCellValue("C9", activoEmpleado.Activo.VidaUtil);
                document.SetCellValue("C10", activoEmpleado.Activo.Descripcion);
               

                document.SetCellValue("B3",activoEmpleado.Activo.Nombre);
                document.SetCellValue("C3",activoEmpleado.Empleado.Nombre);
                document.SetCellValue("D3",activoEmpleado.Activo.Codigo);
                document.SetCellValue("E3",activoEmpleado.Empleado.Dni);

                //tamaño de ancho de columnas
                document.SetColumnWidth(2, 24.14);
                document.SetColumnWidth(3, 29.00);
                document.SetColumnWidth(4, 22.71);
                document.SetColumnWidth(5, 23.0);
                //tamaño de ancho de filas
                document.SetRowHeight(6, 15.00);
                //Estilo de columnas principales(Celdas)
                style.Font.Bold = true;
                style.Font.FontSize = 16;
                style.Border.Outline = true;
                style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Yellow, System.Drawing.Color.Black);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                document.SetCellStyle("B2", style);
                document.SetCellStyle("C2", style);
                document.SetCellStyle("D2", style);
                document.SetCellStyle("E2", style);

                //Centrado de filas Columna C
                document.SetCellStyle("C8", RowAsset);
                document.SetCellStyle("C9", RowAsset);
                document.SetCellStyle("C10", RowAsset);
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
