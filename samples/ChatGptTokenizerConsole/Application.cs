using ChatGptNet;
using ChatGptNet.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptTokenizerConsole
{
    internal class Application
    {
        private readonly IChatGptClient chatGptClient;

        public Application(IChatGptClient chatGptClient)
        {
            this.chatGptClient = chatGptClient;
        }

        public async Task ExecuteAsync()
        {
            var conversationId = Guid.NewGuid();

            while(true)
            {
                Console.Write("Ask me something: ");
                var prompt = Console.ReadLine().Trim();

                var estimatedTokenCount = await chatGptClient.EstimateConversationTokenCountAsync(conversationId, prompt);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Estimated token count: {estimatedTokenCount}");
                Console.ResetColor();

                var response = await chatGptClient.AskAsync(conversationId, prompt);

                Console.WriteLine(response.GetMessage());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Actual token count: {response.Usage.PromptTokens}");
                Console.ResetColor();
            }
        }
    }
}
