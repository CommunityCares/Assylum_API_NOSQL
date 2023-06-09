using Api_ComminityCares.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api_ComminityCares.Servicios
{
    public class DonorService
    {
        private readonly IMongoCollection<Donor> _donorCollection;

        public DonorService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _donorCollection = mongoDatabase.GetCollection<Donor>(
                bookStoreDatabaseSettings.Value.DonorCollectionName);
        }

        public async Task<List<Donor>> GetAsync() =>
            await _donorCollection.Find(_ => true).ToListAsync();

        public async Task<Donor?> GetAsync(int id) =>
            await _donorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Donor newDonor) =>
            await _donorCollection.InsertOneAsync(newDonor);

        public async Task UpdateAsync(int id, Donor updatedDonor) =>
            await _donorCollection.ReplaceOneAsync(x => x.Id == id, updatedDonor);

        public async Task RemoveAsync(int id) =>
            await _donorCollection.DeleteOneAsync(x => x.Id == id);
    }
}
