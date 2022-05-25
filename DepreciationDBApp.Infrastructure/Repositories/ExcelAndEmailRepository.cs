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
using System.Drawing;
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
                string path = Path.GetFullPath("Reporte.xlsx");
                SLDocument document = new SLDocument();
                SLStyle style = new SLStyle();
                SLStyle ColumnsAsset = new SLStyle();
                SLStyle RowAsset = new SLStyle();

                #region Bordes/Estilos/Centrado/Color
                //Bordes de celdas aplicadas a cada lado                
                style.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                style.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                style.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                style.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);

                //Estilo, tamaño, fuente,color, alineación
                //Columna B2:E2
                style.Font.Bold = true;
                style.Font.FontSize = 16;
                style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Yellow, System.Drawing.Color.Black);
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;

                //Columna B6:B10
                ColumnsAsset.Font.Bold = true;
                ColumnsAsset.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Yellow, System.Drawing.Color.Black);//Color de las columnas en B
                ColumnsAsset.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                ColumnsAsset.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                ColumnsAsset.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);
                ColumnsAsset.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Gray);

                //Centrado de texto
                RowAsset.Alignment.Horizontal = HorizontalAlignmentValues.Center; //Centrado de las filas

                //Tamaño de columnas B2:E2
                document.SetColumnWidth(2, 24.14);
                document.SetColumnWidth(3, 29.00);
                document.SetColumnWidth(4, 22.71);
                document.SetColumnWidth(5, 23.0);
                #endregion

                //Columnas
                document.SetCellValue("B2", "Nombre del Activo");
                document.SetCellValue("C2", "Nombre del Empleado");
                document.SetCellValue("D2", "Código del activo");
                document.SetCellValue("E2", "DNI del empleado");


                document.SetCellStyle("B6", ColumnsAsset);
                document.SetCellStyle("B8", ColumnsAsset);
                document.SetCellStyle("B9", ColumnsAsset);
                document.SetCellStyle("B10", ColumnsAsset);

                document.SetCellValue("B6", "Descripción del Activo");
                document.SetCellValue("B8", "Valor del activo");
                document.SetCellValue("B9", "Vida util");
                document.SetCellValue("B10", "Descripción");

                //Filas

                document.SetCellValue("C8", activoEmpleado.Activo.ValorActivo);
                document.SetCellValue("C9", activoEmpleado.Activo.VidaUtil);
                document.SetCellValue("C10", activoEmpleado.Activo.Descripcion);

                document.SetCellValue("B3", activoEmpleado.Activo.Nombre);
                document.SetCellValue("C3", activoEmpleado.Empleado.Nombre);
                document.SetCellValue("D3", activoEmpleado.Activo.Codigo);
                document.SetCellValue("E3", activoEmpleado.Empleado.Dni);

                document.SetCellStyle("B2", style);
                document.SetCellStyle("C2", style);
                document.SetCellStyle("D2", style);
                document.SetCellStyle("E2", style);

                //Centrado de filas Columna C
                document.SetCellStyle("C8", RowAsset);
                document.SetCellStyle("C9", RowAsset);
                document.SetCellStyle("C10", RowAsset);
                document.SaveAs(path);


                SmtpMail mail = new SmtpMail("Tryit");

                mail.From = "teamwork.nicaragua@gmail.com";
                mail.To = activoEmpleado.Empleado.Email;
                mail.Subject = "Verificación de adquisición";
                mail.HtmlBody = "<!DOCTYPE html>"+
                                    "<html>"+                                 
                                        "<body style= \"background-color:white;text-align:center\">" +
                                        "<font color=\"black\">"+
                                        "<h3>Informe de Activo Adquirido</h3>" +
                                        "<div>Mensaje de confirmación</div>"+
                                        "<div> Usted podrá ver mediante esta hoja de excel el activo que adquirió y su breve información </div>"+
                                        "</font>"+
                                        "<div>-------------------------------------------------------------------------------------------</div>"+
                                        "<img src=\"https://cdn-icons-png.flaticon.com/512/3094/3094929.png\" width=\"100\" height=\"100\">" +                                                                 
                                        "</body>" +
                                    "</html>";                
                 mail.AddAttachment(path);

                SmtpServer server = new SmtpServer("smtp.gmail.com");
                server.User = "teamwork.nicaragua@gmail.com";
                server.Password = "admin2022.123";
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
