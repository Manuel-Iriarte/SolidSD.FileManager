using System.Text;

namespace FileManager.Core.Helpers
{
    public static class StringHelpers
    {
        public static string LimpiarTexto(string texto)
        {
            string resultado = string.Empty;

            // Quitar espacios
            resultado = texto.Replace(" ", "");

            // Quitar acentos
            byte[] tempBytes;
            tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(resultado);
            resultado = Encoding.UTF8.GetString(tempBytes);

            return resultado;
        }
    }
}
