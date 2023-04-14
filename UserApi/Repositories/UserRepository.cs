using MongoDB.Driver;
using UserApi.Repositories.Entities;

namespace UserApi.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<UserEntity> _users;
    
    public UserRepository(string connectionString)
    {
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase("user-api");
        _users = database.GetCollection<UserEntity>("users");
    }

    public async Task<UserEntity?> GetById(string userId)
    {
        return await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<UserEntity> Create(string userId, string refreshToken)
    {
        var userEntity = new UserEntity(userId, refreshToken);
        await _users.InsertOneAsync(userEntity);
        return userEntity;
    }

    public async Task<bool> Delete(string userId)
    {
        var result = await _users.DeleteOneAsync(u => u.Id == userId);
        return result.DeletedCount != 0;
    }
}