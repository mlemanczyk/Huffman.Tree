using System.Collections;
using System.Text;

namespace Huffman.Tree
{
	public readonly record struct HuffmanCode<T>
	{
		public HuffmanCode(T? token, BitArray code)
		{
			Token = token;
			Code = code;
		}

		public readonly T? Token;
		public readonly BitArray Code;

		public override int GetHashCode() => Token?.GetHashCode() ?? 0;
		public override string ToString()
		{
			string token = Token?.ToString() ?? "<null>";
			StringBuilder sb = new(token.Length + 12 + Code.Count);
			sb = sb.Append("{{").Append(token).Append("}} => {{");
			sb = AddBits(sb, Code);
			sb = sb.Append("}}");

			return sb.ToString();
		}

		private static StringBuilder AddBits(StringBuilder sb, BitArray bits)
		{
			for (var bitIdx = 0; bitIdx < bits.Count; bitIdx++)
			{
				_ = sb.Append(bits[bitIdx] ? '1' : '0');
			}

			return sb;
		}
	}
}
