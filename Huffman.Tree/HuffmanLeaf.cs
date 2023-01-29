namespace Huffman.Tree
{
	public record HuffmanLeaf : HuffmanNode
	{
		public HuffmanLeaf(HuffmanNode? left, HuffmanNode? right)
		{
			long leftFrequency;
			long rightFrequency;

			if (left != default)
			{
				left.Top = this;
				left.Bit = false;
				leftFrequency = left.Frequency;
			}
			else
			{
				leftFrequency = 0;
			}

			if (right != null)
			{
				right.Top = this;
				right.Bit = true;
				rightFrequency = right.Frequency; 
			}
			else
			{
				rightFrequency= 0;
			}

			Frequency = leftFrequency + rightFrequency;
		}
	}
}
