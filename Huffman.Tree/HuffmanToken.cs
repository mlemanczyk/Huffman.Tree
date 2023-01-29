using System.Text;

namespace Huffman.Tree
{
	public record HuffmanToken<T> : HuffmanNode
	{
		protected override long RecalculateTotalSize() 
			=> Token.SizeOf(Encoding) * Frequency;

		public Encoding Encoding { get; init; }

		private T? _token;

		public HuffmanToken(Encoding? encoding = default)
		{
			Encoding = encoding ?? Encoding.UTF8;
		}

		protected HuffmanToken(HuffmanNode original, Encoding? encoding = default) : base(original)
		{
			Encoding = encoding ?? Encoding.UTF8;
		}

		public T? Token
		{
			get => _token;

			init
			{
				_token = value;
				_totalSize = default;
			}
		}
	}
}
