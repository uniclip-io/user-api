using UserApi.Models;

namespace UserApi.Dtos;

public record UpdateUser(
    string UserId, 
    Settings Settings
);
