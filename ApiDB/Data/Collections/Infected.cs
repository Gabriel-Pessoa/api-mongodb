using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ApiDB.Data.Collections
{

    //Classe representação da collection no Banco
    public class Infected
    {
        public Infected(string name, DateTime dateBirth, string sex, double latitude, double longitude)
        {
            //this.Id = Id; // Sem necessidade de instanciar atributo
            this.Name = name;
            this.DateBirth = dateBirth;
            this.Sex = sex;
            this.Location = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public string Sex { get; set; }

        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}