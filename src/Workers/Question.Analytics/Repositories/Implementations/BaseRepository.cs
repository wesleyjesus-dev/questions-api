using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Question.Analytics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Question.Analytics.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> entity;

        public BaseRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("analytics");

            entity = database.GetCollection<T>(typeof(T).ToString());
        }

        public async Task<List<T>> Get()
        {
            var result = await entity.FindAsync(book => true);
            return result.ToList();
                
        }

        public async Task<T> Get(int id)
        {
            var result = await entity.FindAsync<T>(book => book.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<T> Create(T book)
        {
            await entity.InsertOneAsync(book);
            return book;
        }

        public async Task Update(int id, T bookIn) => await entity.ReplaceOneAsync(book => book.Id == id, bookIn);

        public async Task Remove(T bookIn) => await entity.DeleteOneAsync(book => book.Id == bookIn.Id);

        public async Task Remove(int id) => await entity.DeleteOneAsync(book => book.Id == id);
    }
}
