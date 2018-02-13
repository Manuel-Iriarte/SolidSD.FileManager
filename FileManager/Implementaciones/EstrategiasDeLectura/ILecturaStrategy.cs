using FileManager.Core.Dtos;
using System.Collections.Generic;

namespace FileManager.Implementaciones.EstrategiasDeLectura
{
    public interface ILecturaStrategy<T>
    {
        IEnumerable<T> LeerArchivo(Archivo archivo);
    }
}
