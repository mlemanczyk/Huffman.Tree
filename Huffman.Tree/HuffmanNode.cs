using System.Collections;

namespace Huffman.Tree
{
	public record HuffmanNode
	{
		protected virtual long RecalculateTotalSize() => Frequency;

		private long _frequency;
		public long Frequency
		{
			get => _frequency;

			set
			{
				if (_frequency != value)
				{
					_frequency = value;
					_totalSize = default;
				}
			}
		}

		protected long? _totalSize;
		public long TotalSize => _totalSize ??= RecalculateTotalSize();

		private BitArray? _code;
		public BitArray Code => _code ??= BuildCode(this);

		private static BitArray BuildCode(HuffmanNode root)
		{
			var parent = root.Top;
			if (parent == null)
			{
				return BitArrayExtensions.Empty;
			}

			BitArray bits = new(parent.Code);
			var oldLength = bits.Length++;
			bits[oldLength] = (bool)root.Bit!;
			return bits;
		}

		public HuffmanNode? Top { get; set; }
		public bool? Bit { get; set; }
	}
}