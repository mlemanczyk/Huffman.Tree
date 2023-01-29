namespace Huffman.Tree
{
	public record HuffmanValueToken<TToken, TValue> : HuffmanToken<TToken>
	{
		public TValue? Value { get; init; }
	}
}
