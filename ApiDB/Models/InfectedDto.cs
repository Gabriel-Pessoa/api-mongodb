using System;

namespace ApiDB.Models
{
    //Representa o infectado no banco de dados
    public class InfectedDto
    {   
        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public string Sex { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
    }
}