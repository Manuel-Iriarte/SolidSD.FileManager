using FileManager.Contratos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Core.Helpers
{
    public static class Validador<T> where T : class, IFileModel
    {
        public static T Validar(T instancia)
        {
            var contextoValidacion = new ValidationContext(instancia, null, null);
            var resultados = new List<ValidationResult>();

            if (!Validator.TryValidateObject(instancia, contextoValidacion, resultados, true))
            {
                var MensajesValidacion = new List<string>();

                resultados.ForEach(r => MensajesValidacion.Add(r.ErrorMessage));

                instancia.MensajesValidacion = MensajesValidacion;
            }


            return instancia;
        }
    }
}
