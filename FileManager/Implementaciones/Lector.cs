using FileManager.Contratos;
using FileManager.Core.Dtos;
using FileManager.Core.Enums;
using FileManager.Implementaciones.EstrategiasDeLectura;
using System.Collections.Generic;

namespace FileManager.Implementaciones
{
    public class Lector<T> where T : IFileModel
    {
        Archivo _archivo;

        public Lector(Archivo archivo)
        {
            _archivo = archivo;
        }

        public IEnumerable<T> Leer(TipoArchivo tipoArchivo)
        {
            ILecturaStrategy<T> lector = null;

            switch (tipoArchivo)
            {
                case TipoArchivo.Excel:
                    lector = new LecturaExcel<T>();
                    break;
                case TipoArchivo.Txt:
                    lector = new LecturaTxt<T>();
                    break;
                default:
                    break;
            }

            return lector.LeerArchivo(_archivo);
        }
    }
}
