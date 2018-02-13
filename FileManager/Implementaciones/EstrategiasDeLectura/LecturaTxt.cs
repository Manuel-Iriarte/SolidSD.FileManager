using FileManager.Contratos;
using FileManager.Core.Dtos;
using System.Collections.Generic;

namespace FileManager.Implementaciones.EstrategiasDeLectura
{
    public class LecturaTxt<T> : ILecturaStrategy<T> where T : IFileModel
    {
        public IEnumerable<T> LeerArchivo(Archivo archivo)
        {
            throw new System.NotImplementedException();
        }
    }
}
