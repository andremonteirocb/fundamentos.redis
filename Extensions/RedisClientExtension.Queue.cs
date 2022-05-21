using ServiceStack.Redis;

namespace Fundamentos.Redis.Extensions
{
    public static partial class RedisClientExtension
    {
        /// <summary>
        /// Obtém o primeiro elemento adicionado na lista
        /// </summary>
        /// <param name="client">client do redis</param>
        /// <param name="listId">identificador da lista</param>
        public static string GetAndRemoveFirstElementFromList(this IRedisClient client, string listId)
        {
            var result = client.Custom("LPOP", listId);
            return result?.Text;
        }

        /// <summary>
        /// Obtém e remove o primeiro elemento adicionado na lista
        /// </summary>
        /// <param name="client">client do redis</param>
        /// <param name="listId">identificador da lista</param>
        /// <param name="waitTime">Caso seja zero o programa irá esperar até um elemento ser adicionado na lista</param>
        /// <returns></returns>
        public static string GetAndRemoveFirstElementFromListWithBlockWait(this IRedisClient client, string listId, int waitTime = 0)
        {
            var result = client.Custom("BLPOP", listId, waitTime);
            return result?.Children[1]?.Text;
        }

        /// <summary>
        /// Adiciona um elemento na primeira posição da lista
        /// </summary>
        /// <param name="client">client do redis</param>
        /// <param name="listId">identificador da lista</param>
        /// <param name="value">valor a ser adicionado na lista</param>
        public static void AddElementLeftPositionList(this IRedisClient client, string listId, string value)
        {
            client.Custom("LPUSH", listId, value); ;
        }

        /// <summary>
        /// Adiciona um elemento na última posição da lista
        /// </summary>
        /// <param name="client">client do redis</param>
        /// <param name="listId">identificador da lista</param>
        /// <param name="value">valor a ser adicionado na lista</param>
        public static void AddElementRightPositionList(this IRedisClient client, string listId, string value)
        {
            client.Custom("RPUSH", listId, value); ;
        }
    }
}
