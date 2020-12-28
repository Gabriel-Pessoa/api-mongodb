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
            this.Name = name;
            this.DateBirth = dateBirth;
            this.Sex = sex.ToUpper();
            this.Location = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public DateTime DateBirth { get; set; }

        [BsonRequired]
        public string Sex { get; set; }
        
        [BsonRequired]
        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}