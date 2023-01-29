using System.Collections;

namespace Huffman.Tree
{
	public class HuffmanTree<T> : IEnumerable<HuffmanNode>
	{
		private IEnumerable<HuffmanToken<T>> EnumerateCodes() => EnumerateNodes().OfType<HuffmanToken<T>>();
		private IEnumerable<HuffmanNode> EnumerateNodes() => Nodes;

		public HuffmanTree(IEnumerable<HuffmanNode> nodes)
		{
			Nodes = nodes;
		}

		public void Dispose()
		{
			Nodes = Enumerable.Empty<HuffmanNode>();
		}

		public IEnumerator<HuffmanNode> GetEnumerator() => Nodes.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private IEnumerable<HuffmanToken<T>>? _codes;
		public IEnumerable<HuffmanToken<T>> Codes => _codes ??= EnumerateCodes();
		public IEnumerable<HuffmanNode> Nodes { get; protected set; }
	}
}
