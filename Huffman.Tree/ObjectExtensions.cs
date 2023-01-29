using System.Text;

namespace Huffman.Tree
{
	public static class ObjectExtensions
	{
		private static readonly Dictionary<Type, int> _dataTypeSizes = new()
		{
			{ typeof(bool), 1 },
			{ typeof(sbyte), 8 },
			{ typeof(byte), 8 },
			{ typeof(short), 16 },
			{ typeof(ushort), 16 },
			{ typeof(int), 32 },
			{ typeof(uint), 32 },
			{ typeof(long), 32 },
			{ typeof(ulong), 32 },
			{ typeof(float), 32 },
			{ typeof(double), 64 },
			{ typeof(decimal), 128 },
		};

		internal static long GetSize<T>(T? value, Encoding? encoding = default)
		{
			encoding ??= Encoding.UTF8;
			return value is string @string
				? encoding.GetByteCount(@string)
				: value != null
				? _dataTypeSizes[typeof(T)]
				: 0;
		}

		public static long SizeOf(this long value) => GetSize(value);
		public static long SizeOf(this int value) => GetSize(value);
		public static long SizeOf(this string value, Encoding? encoding = default) => GetSize(value, encoding);
		public static long SizeOf<T>(this T value, Encoding? encoding = default) => GetSize(value, encoding);
	}
}
