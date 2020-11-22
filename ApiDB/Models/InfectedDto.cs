using System;
using System.ComponentModel.DataAnnotations;

namespace ApiDB.Models
{
    //Representa o infectado no banco de dados
    public class InfectedDto
    {
        [Required(ErrorMessage = "O nome é obrigatório", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O nome deve ter no mínimo 5 e no máximo 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatório", AllowEmptyStrings = false)]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório", AllowEmptyStrings = false)]
        public string Sex { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatório", AllowEmptyStrings = false)]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatório", AllowEmptyStrings = false)]
        public double Longitude { get; set; }
    }
}