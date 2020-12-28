using System;
using ApiDB.Data.Collections;
using ApiDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;


namespace ApiDB.Controllers
{
    [ApiController]
    [Route("[controller]")] // Irá considerar o nome: Infected || infected
    public class InfectedController : ControllerBase
    {
        // Base de dados
        Data.MongoDB _mongoDB;

        //Coleção
        IMongoCollection<Infected> _infectedsCollection;

        //Construtor, injetando dependência mongo.
        public InfectedController(Data.MongoDB mongoDB)
        {
            // Inicializando a dependência injetada
            _mongoDB = mongoDB;
            _infectedsCollection = _mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        // Inserir infectado
        //Post: https://localhost:<port>/infected
        // Recebe objeto com estrutura idêntica ao InfectedDto
        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedDto dto)
        {
            var infected = new Infected(dto.Name, dto.DateBirth, dto.Sex, dto.Latitude, dto.Longitude);

            _infectedsCollection.InsertOne(infected);

            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        // Obter lista de infectados
        //Get: https://localhost:<port>/infected
        [HttpGet]
        public ActionResult GetInfecteds()
        {
            var infecteds = _infectedsCollection.Find(Builders<Infected>.Filter.Empty).ToList();

            return Ok(infecteds);
        }

        // Alterar infectado, alterando todo o seu documento
        //Put: https://localhost:<port>/infected/<id>
        [HttpPut("{id}")]
        public ActionResult UpdateInfected(string id, [FromBody] InfectedDto dto)
        {   
             // Faz o filtro baseado no Id
            var filter = Builders<Infected>.Filter.Where(_ => _.Id == id); 

            //Atualiza também a location para não ter inconsistência de dados
            var location = new GeoJson2DGeographicCoordinates(dto.Longitude, dto.Latitude);

            var update = Builders<Infected>.Update.Set("name", dto.Name).Set("dateBirth", dto.DateBirth).Set("sex", dto.Sex.ToUpper())
                .Set("location", location).Set("latitude", dto.Latitude).Set("longitude", dto.Longitude).CurrentDate("lastModified");

            _infectedsCollection.UpdateOne(filter, update); // Atualiza um documento

            return Ok("Atualizado com sucesso!"); // Resposta
        }

        // Deletar infectado. 
        //Delete: https://localhost:<port>/infected/<id>
        [HttpDelete("{id}")]
        public ActionResult DeleteInfected(string id)
        {
            // Faz o filtro baseado no Id
            var filter = Builders<Infected>.Filter.Where(_ => _.Id == id);

            _infectedsCollection.DeleteOne(filter); // Deleta um documento

            return Ok("Deletado com sucesso"); // Resposta
        }
    }
}