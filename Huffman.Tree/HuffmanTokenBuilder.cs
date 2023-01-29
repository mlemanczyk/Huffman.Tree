namespace Huffman.Tree
{
	public static class HuffmanTokenBuilder
	{
		public static HuffmanToken<TToken> AsHuffmanToken<TToken>(this TToken value, long frequency = 0)
			=> new() { Token = value, Frequency = frequency };

		public static HuffmanToken<TToken> WithFrequency<TToken>(this HuffmanToken<TToken> token, long frequency)
			=> token with { Frequency = frequency };
	}
}
