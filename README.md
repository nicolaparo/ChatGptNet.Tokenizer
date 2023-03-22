# ChatGPT Tokenizer for .NET

A ChatGPT tokenizer implementation for .NET.
You can use this library to estimate the cost (in terms of tokens) of your request to ChatGPT.

## Usage

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
The default `vocab.bpe` and `encodings.json` are already included in this library as `gpt_tokenizer_vocab.bpe` and `gpt_tokenizer_encodings.json`.

## Contribute

Contributions are welcome. Feel free to file issues and pull requests on the repo and we'll address them as we can. 