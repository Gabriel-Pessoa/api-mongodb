using System;
using System.ComponentModel.DataAnnotations;

namespace ApiDB.Models
{
    //Representa o infectado no banco de dados
    public class InfectedDto
    {
        [Required(ErrorMessage = "O nome é obrigatório", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O nome deve ter no mínimo 5 e no máximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z ,.'-]+$", ErrorMessage = "Nome inválido. Evite acentos e caracteres especiais")]
        public string Name { get; set; }


        public DateTime DateBirth { get; set; }


        [Required(ErrorMessage = "O sexo é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[FM]{1}", ErrorMessage = "Sexo inválido, tente 'F' ou 'M'")]
        public string Sex { get; set; }


        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}