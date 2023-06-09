using Api_ComminityCares.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api_ComminityCares.Servicios
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _personCollection;

        public PersonService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _personCollection = mongoDatabase.GetCollection<Person>(
                bookStoreDatabaseSettings.Value.PersonCollectionName);
        }

        public async Task<List<Person>> GetAsync() =>
            await _personCollection.Find(_ => true).ToListAsync();

        public async Task<Person?> GetAsync(int id) =>
            await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Person newPerson) =>
            await _personCollection.InsertOneAsync(newPerson);

        public async Task UpdateAsync(int id, Person updatedPerson) =>
            await _personCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);

        public async Task RemoveAsync(int id) =>
            await _personCollection.DeleteOneAsync(x => x.Id == id);
    }
}
