using MongoDB.Driver;
using UserApi.Models;
using UserApi.Repositories.Entities;

namespace UserApi.Repositories;

public class SettingsRepository
{
    private readonly IMongoCollection<SettingsEntity> _settings;
    
    public SettingsRepository(string connectionString)
    {
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase("user-api");
        _settings = database.GetCollection<SettingsEntity>("settings");
    }

    public async Task<Settings?> GetSettingsByUserId(string userId)
    {
        var settingsEntity = await _settings.Find(s => s.UserId == userId).FirstOrDefaultAsync();

        return settingsEntity == null ? null : Settings.From(settingsEntity);
    }

    public async Task<Settings> CreateForUser(string userId)
    {
        var settingsEntity = SettingsEntity.Create(userId);
        await _settings.InsertOneAsync(settingsEntity);
        return Settings.From(settingsEntity);
    }

    public async Task<Settings?> UpdateForUser(string userId, int? maxConnectedDevices, string? theme)
    {
        var settingsEntity = await GetSettingsByUserId(userId);

        if (settingsEntity == null)
        {
            return null;
        }

        var updateMaxConnectedDevices = maxConnectedDevices ?? settingsEntity.MaxConnectedDevices;
        var updatedTheme = theme ?? settingsEntity.Theme;
        var updatedSettingsEntity = new SettingsEntity(userId, updateMaxConnectedDevices, updatedTheme);

        await _settings.ReplaceOneAsync(s => s.UserId == userId, updatedSettingsEntity);
        
        return Settings.From(updatedSettingsEntity);
    }

    public async Task<bool> DeleteSettingsByUserId(string userId)
    {
        var result = await _settings.DeleteOneAsync(s => s.UserId == userId);
        return result.DeletedCount != 0;
    }
}
