using FileManager.Contratos;
using FileManager.Core.Dtos;
using FileManager.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FileManager.Implementaciones.EstrategiasDeLectura
{
    /// <summary>
    /// Lee la primera hoja de un excel que contenga los campos del objeto T entregado
    /// </summary>
    /// <typeparam name="T">T es el modelo que se desea recuperar del excel cargado</typeparam>
    public class LecturaExcel<T> : ILecturaStrategy<T> where T : IFileModel
    {
        public IEnumerable<T> LeerArchivo(Archivo archivo)
        {
            List<T> resultado = new List<T>();
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel._Worksheet xlWorksheet = null;
            Excel.Range xlRange = null;

            try
            {
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(archivo.Ruta);
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                int filas = xlRange.Rows.Count;
                int columnas = xlRange.Columns.Count;

                var tipo = typeof(T);
                var propiedades = tipo.GetProperties();

                var titulos = new Dictionary<int, string>();

                for (int i = 1; i <= columnas; i++)
                {
                    if (xlRange.Cells[1, i].Value2 != null && xlRange.Cells[1, i] != null)
                    {
                        string textoLimpio = StringHelpers.LimpiarTexto(xlRange.Cells[1, i].Value2);
                        titulos.Add(i, textoLimpio);
                    }

                }

                for (int indexFila = 2; indexFila <= filas; indexFila++)
                {
                    if (xlRange.Cells[indexFila, 1].Value2 != null && xlRange.Cells[indexFila, 1] != null)
                    {
                        var modelo = Activator.CreateInstance(tipo);

                        propiedades.ToList().ForEach(p =>
                        {
                            var indexColumna = titulos.FirstOrDefault(t => t.Value == p.Name).Key;

                            if (indexColumna != 0)
                            {
                                var valor = xlRange.Cells[indexFila, indexColumna].Value2;

                                //if (p.PropertyType == typeof(string) && String.IsNullOrEmpty(valor)) valor = string.Empty;

                                p.SetValue(modelo, Convert.ChangeType(valor, p.PropertyType));
                            }
                        });

                        modelo = Validador<IFileModel>.Validar((T)modelo);

                        resultado.Add((T)modelo);
                    }
                }
            }
            catch (Exception ex)
            {
                // todo:manejar con un objeto de resultado
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }

            return resultado;
        }

        private void Limpiar()
        {

        }
    }
}
