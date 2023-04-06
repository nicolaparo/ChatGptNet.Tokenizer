# ChatGPT Tokenizer for .NET

[![NuGet](https://img.shields.io/nuget/v/ChatGptNet.Tokenizer.svg?style=flat-square)](https://www.nuget.org/packages/ChatGptNet.Tokenizer)

A ChatGPT tokenizer implementation for .NET.
You can use this library to estimate the cost (amount of tokens) of your request to ChatGPT.

## Installation

The library is available on [NuGet](https://www.nuget.org/packages/ChatGptNet.Tokenizer). Just search for *ChatGptNet.Tokenizer* in the **Package Manager GUI** or run the following command in the **.NET CLI**:

    dotnet add package ChatGptNet.Tokenizer

## About
This is a C# implementation of OpenAI's original python encoder/decoder which can be found [here](https://github.com/openai/gpt-2). This implementation was strongly inspired from the JavaScript version created by latitudegames, that can be found [here](https://github.com/latitudegames/GPT-3-Encoder).

## Setup

The library can be used in any .NET application built with .NET 6.0 or later. Just create an instance of the GptTokenizer and you are ready to go.

You can either create the default tokenizer

```csharp
var tokenizer = await GptTokenizer.CreateTokenizerAsync();
```

Or, optionally, you can create a tokenizer with a custom `vocab.bpe` and `encodings.json` source files with:
```csharp
using var vocabStream = File.OpenRead(@"path/to/vocab.bpe");
using var encodingStream = File.OpenRead(@"path/to/encodings.json");

var tokenizer = await GptTokenizer.CreateTokenizerAsync(vocabStream, encodingsStream);
```
The default `vocab.bpe` and `encodings.json` are already included in this library.

## Usage

Once you obtain an instance of the GptTokenizer, you can perform the tokenization of your text.
```csharp
var tokens = tokenizer.GetTokens(@"Lorem ipsum dolor sit amet")

var tokensCount = tokens.Length;
var tokensAsText = tokenizer.GetTextFromTokens(tokens);
```

## Integration with ChatGptNet
This package extends the [ChatGptNet NuGet package](https://www.nuget.org/packages/ChatGptNet) (source: https://github.com/marcominerva/ChatGptNet) by providing some functionalities for estimating the token amount.
```csharp
public async Task Estimate(IChatGptClient chatGptClient)
{
    var conversationId = Guid.NewGuid();

    chatGptClient.EstimateConversationTokenCountAsync("first message");
    chatGptClient.EstimateConversationTokenCountAsync(conversationId);
    chatGptClient.EstimateConversationTokenCountAsync(conversationId, "next message");
}


```


## Contribute

Contributions are welcome. Feel free to file issues and pull requests on the repo and we'll address them as we can. 