using ServiceStack.Redis;

namespace Fundamentos.Redis.Extensions
{
    public static partial class RedisClientExtension
    {
        public static string GetAndRemoveFirstElementFromList(this IRedisClient client, string listId)
        {
            var result = client.Custom("LPOP", listId);
            return result?.Text;
        }

        public static string GetAndRemoveFirstElementFromListWithBlockWait(this IRedisClient client, string listId, int waitTime)
        {
            var result = client.Custom("BLPOP", listId, waitTime);
            return result?.Children[1]?.Text;
        }

        public static void AddElementLeftPositionList(this IRedisClient client, string listId, string value)
        {
            client.Custom("LPUSH", listId, value); ;
        }

        public static void AddElementRightPositionList(this IRedisClient client, string listId, string value)
        {
            client.Custom("RPUSH", listId, value); ;
        }
    }
}
