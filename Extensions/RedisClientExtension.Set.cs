using ServiceStack.Redis;

namespace Fundamentos.Redis.Extensions
{
    public static partial class RedisClientExtension
    {
        /// <summary>
        /// Adiciona elementos no conjunto
        /// </summary>
        /// <param name="elementos">elementos para ser adicionados</param>
        public static void AddElementInSet(this IRedisClient client, params string[] elements)
        {
            client.Custom("SADD", elements);
        }

        /// <summary>
        /// Remove elementos no conjunto
        /// </summary>
        /// <param name="elementos">elementos para ser adicionados</param>
        public static void AddElementInSet(this IRedisClient client, string setId, string element)
        {
            client.Custom("SREM", setId, element);
        }
    }
}
