namespace ChatGptNet.Tokenizer
{
    internal static class GPTTokenizerEnumerableExtensions
    {
        public static Dictionary<TFirst, TSecond> ToDictionary<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> collection) where TFirst : notnull
        {
            return collection.ToDictionary(c => c.Item1, c => c.Item2);
        }
        public static string JoinStrings<T>(this IEnumerable<T> values, string glue)
        {
            return string.Join(glue, values.ToArray());
        }
        public static string JoinStrings<T>(this IEnumerable<T> values)
        {
            return values.JoinStrings(string.Empty);
        }
    }

}