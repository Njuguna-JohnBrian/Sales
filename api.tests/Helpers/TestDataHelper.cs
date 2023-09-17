using api.Database.Entities;

namespace api.tests.Helpers;

public class TestDataHelper
{
    public static List<UserEntity> GetFakeUsersList()
    {
        return new List<UserEntity>()
        {
            new()
            {
                Id = 1,
                FirstName = "testUser",
                LastName = "testUser",
                Email = "test@email.com",
                PasswordHash = "$2a$11$8STxovb1zSpiMqGkxOcJbOkKvvUTYWWEE2dW92mmpZ1JvPhDUJwjO"
            }
        };
    }
}