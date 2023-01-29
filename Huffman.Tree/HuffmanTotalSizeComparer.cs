namespace Huffman.Tree
{
	public static partial class HuffmanTreeBuilder
	{
		internal class HuffmanTotalSizeComparer : IComparer<HuffmanNode?>
		{
			public int Compare(HuffmanNode? x, HuffmanNode? y)
			{
				return (x?.TotalSize - y?.TotalSize) switch
				{
					> 0L => 1,
					< 0L => -1,
					_ => 0
				};
			}
		}
	}
}
