using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace ChatGptNet.Tokenizer
{
    public static class ChatGptClientExtensions
    {
        private static async Task<int> EstimateTokenCountAsync(IEnumerable<(string role, string content)> entries)
        {
            var tokenizer = await GptTokenizer.GetGpt3TokenizerAsync();

            var num_tokens = 0;

            foreach (var (role, content) in entries)
            {
                num_tokens += 4;
                num_tokens += tokenizer.GetTokens(role).Length;
                num_tokens += tokenizer.GetTokens(content).Length;
                num_tokens += 1;
            }

            num_tokens += 2;

            return num_tokens;
        }

        /// <summary>
        /// Estimates the amount of tokens required for the specified conversation.
        /// </summary>
        /// <param name="client">the <see cref="IChatGptClient"/></param>
        /// <param name="conversationId">the conversationId to estimate</param>
        /// <remarks>Please, note that the value is just an estimation and the actual billed value might be different.</remarks>
        /// <returns>An esteem of the amount of tokens required.</returns>
        public static async Task<int> EstimateConversationTokenCountAsync(this IChatGptClient client, Guid conversationId)
        {
            var conversation = await client.GetConversationAsync(conversationId);
            var result = await EstimateTokenCountAsync(conversation.Select(e => (e.Role, e.Content)));
            return result;
        }

        /// <summary>
        /// Estimates the amount of tokens required for starting a conversation with a specified message.
        /// </summary>
        /// <param name="client">the <see cref="IChatGptClient"/></param>
        /// <param name="message">the initial message of the conversation</param>
        /// <remarks>Please, note that the value is just an estimation and the actual billed value might be different.</remarks>
        /// <returns>An esteem of the amount of tokens required.</returns>
        public static async Task<int> EstimateConversationTokenCountAsync(this IChatGptClient client, string message)
        {
            var result = await EstimateTokenCountAsync(new[] { ("user", message) });
            return result;
        }

        /// <summary>
        /// Estimates the amount of tokens required for replying to a conversation with a specified message.
        /// </summary>
        /// <param name="client">the <see cref="IChatGptClient"/></param>
        /// <param name="conversationId">the conversationId to reply</param>
        /// <param name="message">the message that will reply the conversation</param>
        /// <remarks>Please, note that the value is just an estimation and the actual billed value might be different.</remarks>
        /// <returns>An esteem of the amount of tokens required.</returns>
        public static async Task<int> EstimateConversationTokenCountAsync(this IChatGptClient client, Guid conversationId, string message)
        {
            var conversation = await client.GetConversationAsync(conversationId);
            var result = await EstimateTokenCountAsync(conversation.Select(e => (e.Role, e.Content)).Append(("user", message)));
            return result;
        }

    }
}