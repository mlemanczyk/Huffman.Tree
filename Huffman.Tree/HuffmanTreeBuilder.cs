namespace Huffman.Tree
{
	public static partial class HuffmanTreeBuilder
	{
		public static HuffmanTree<string> AsHuffmanTree(this IEnumerable<(string token, long frequency)> inputTokens)
		{
			return inputTokens.Select(x => new HuffmanToken<string>()
			{
				Token = x.token,
				Frequency = x.frequency
			})
			.AsHuffmanTree();
		}

		public static HuffmanTree<T> AsHuffmanTree<T>(this IEnumerable<HuffmanToken<T>> inputTokens)
		{
			var tokensQueue = inputTokens.OrderByTotalSize();
			// TODO: Find a way to avoid .ToArray() & be able to dynamically calculate HuffmanNode.Code before knowing all parents.
			var treeNodes = tokensQueue.BuildTreeNodes().ToArray();
			return new HuffmanTree<T>(treeNodes);
		}

		private static IEnumerable<HuffmanNode> BuildTreeNodes(this PriorityQueue<HuffmanNode, long> tokensQueue)
		{
			var leftNode = tokensQueue.Dequeue();
			HuffmanNode headNode = leftNode;
			while (tokensQueue.Count > 0)
			{
				var rightNode = tokensQueue.Dequeue();
				headNode = new HuffmanLeaf(leftNode, rightNode);
				// Don't move yielding before topNode creation. Otherwise the yielded nodes
				// will have Top = null. We shall only yield nodes that have assigned nodes.
				yield return leftNode;
				yield return rightNode;
				leftNode = tokensQueue.EnqueueDequeue(headNode, headNode.TotalSize);
			}

			yield return headNode;
		}

		public static PriorityQueue<HuffmanNode, long> OrderByTotalSize<T>(this IEnumerable<HuffmanToken<T>> tokens)
			=> new(tokens.Select(x => ((HuffmanNode)x, x.TotalSize)));
	}
}
