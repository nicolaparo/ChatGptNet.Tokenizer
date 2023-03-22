using ChatGptNet.Tokenizer;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatGptTokenizerConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tokenizer = await GptTokenizer.CreateTokenizerAsync();

            while (true)
            {
                Console.Write("Ask me anything: ");
                var message = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(message))
                    return;

                var tokens = tokenizer.GetTokens(message);
                var tokensCount = tokens.Length;

                Console.WriteLine(JsonSerializer.Serialize(tokens));

                ConsoleWriteTokens(tokenizer.GetTextFromTokens(tokens).ToArray());

                Console.WriteLine($"If you ask to ChatGPT this, it will cost {tokensCount} tokens!");
            }
        }

        static void ConsoleWriteTokens(params string[] tokens)
        {
            for(var i = 0; i < tokens.Length; i++) 
            {
                Console.BackgroundColor = (ConsoleColor)(i % 6 + 1);
                Console.Write(tokens[i]);
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}