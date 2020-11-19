using System;
using ApiDB.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;


namespace ApiDB.Data
{
    public class MongoDB
    {
        // Ao refenciar essa propriedade fora da classe, ela já estará pronta para uso e acesso ao Banco.
        public IMongoDatabase DB { get; }

        // Construtor recebe uma injeção de dependência chamada IConfiguration.
        // IConfiguration: Classe .net onde já estão prontas as configurações padrões de host, ambiente, conexões.
        public MongoDB(IConfiguration configuration)
        {
            try
            { 
                // Cria as configurações Mongo baseada na minha ConnectionString
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings); // Cria uma nova instância MongoClient, baseado na variável settings.
                DB = client.GetDatabase(configuration["NameDB"]); // Seta numa propriedade pública, a variável client fazendo um get baseado no nome do banco.
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }

        // Forma mais organizada de realizar os Data Notations    
        private void MapClasses()
        {
            //Cria uma convenção e regista, detalhando o formato  
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            // Se não tiver nenhuma classe mapeada do tipo Infected.  
            if (!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i =>
                {
                    // Evita rescrever todas as propriedades para exibição.
                    i.AutoMap();
                    // Evita bugs, caso aja acréscimo nas propriedades da classe Infected.
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}