using Api_ComminityCares.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace Api_ComminityCares.Servicios
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                bookStoreDatabaseSettings.Value.UserCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(int id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newPerson) =>
            await _userCollection.InsertOneAsync(newPerson);

        public async Task UpdateAsync(int id, User updatedPerson) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);

        public async Task RemoveAsync(int id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _userCollection
            .Find(x => x.Email == email && x.Password == password)
            .FirstOrDefaultAsync();

            if (user == null || password == null)
            {
                return false; // El usuario no existe
            }


            return true; // Inicio de sesión exitoso
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Aquí puedes usar un algoritmo de hashing para comparar la contraseña proporcionada
            // con la contraseña almacenada en la base de datos.
            // Por razones de seguridad, no se debe almacenar la contraseña en texto plano.
            // En su lugar, se almacena su hash y se compara con el hash de la contraseña proporcionada.
            // Implementar esta función depende de la biblioteca o algoritmo de hashing que estés utilizando.
            // A continuación se muestra un ejemplo simple utilizando el algoritmo de hashing SHA256:

            using (var sha256 = SHA256.Create())
            {
                var hashedInputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedInput = BitConverter.ToString(hashedInputBytes).Replace("-", "").ToLower();

                return hashedInput == hashedPassword;
            }
        }
    }
}
