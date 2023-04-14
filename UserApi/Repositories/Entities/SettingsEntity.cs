namespace UserApi.Repositories.Entities;

public record SettingsEntity(
    string UserId,
    int MaxConnectedDevices,
    string Theme
)
{
    public static SettingsEntity Create(string userId)
    {
        return new SettingsEntity(userId, 2, "light");
    }
}
