namespace Huffman.Tree
{
	internal static class HuffmanTreeQueueExtensions
	{
		public static bool EnqueueIfNotNull<T>(this Queue<HuffmanNode> queue, in HuffmanNode? item)
		{
			if (item != default)
			{
				queue.Enqueue(item);
				return true;
			}

			return false;
		}
	}
}