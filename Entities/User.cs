using Newtonsoft.Json;

namespace Fundamentos.Redis.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class UserExtension
    {
        public static string toJson(this User user)
        {
            return JsonConvert.SerializeObject(user);
        }

        public static User toUser(this string json)
        {
            return JsonConvert.DeserializeObject<User>(json);
        }
    }
}
