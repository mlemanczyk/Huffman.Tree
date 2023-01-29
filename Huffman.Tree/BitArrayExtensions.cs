using System.Buffers;
using System.Collections;
using System.Text;

namespace Huffman.Tree
{
	public static class BitArrayExtensions
	{
		public static BitArray Empty { get; } = new(0);

		public static BitArray Reverse(this BitArray original)
		{
			BitArray reversed = original;
			int length = reversed.Length;
			int mid = length / 2;

			for (int i = 0; i < mid; i++)
			{
				(reversed[length - i - 1], reversed[i]) = (reversed[i], reversed[length - i - 1]);
			}

			return reversed;
		}

		public static string AsString(this BitArray bits)
		{
			StringBuilder sb = new(bits.Count);
			foreach (var bit in Enumerable.Range(0, bits.Count))
			{
				sb = sb.Append(bits[bit] ? '1' : '0');
			}

			return sb.ToString();
		}

		public static BitArray AsBitArray(this DateTime dateTime) 
		{
			long asBinary = dateTime.ToBinary();
			return new BitArray(BitConverter.GetBytes(asBinary));
		}

		public static BitArray AsBitArray(this nint nint) => new(BitConverter.GetBytes(@nint));
		public static BitArray AsBitArray(this bool @bool) => new(BitConverter.GetBytes(@bool));
		public static BitArray AsBitArray(this char @char) => new(BitConverter.GetBytes(@char));
		public static BitArray AsBitArray(this byte @byte) => new(BitConverter.GetBytes((char)@byte));
		public static BitArray AsBitArray(this double @double) => new(BitConverter.GetBytes(@double));
		public static BitArray AsBitArray(this float @float) => new(BitConverter.GetBytes(@float));
		public static BitArray AsBitArray(this short @short) => new(BitConverter.GetBytes(@short));
		public static BitArray AsBitArray(this int @int, int? length = default)
		{
			BitArray bitArray = new(BitConverter.GetBytes(@int));
			if (length != null)
			{
				bitArray.Length = length.Value;
			}

			return bitArray;
		}

		public static BitArray AsBitArray(this long @long, int? length = default)
		{
			BitArray bitArray = new(BitConverter.GetBytes(@long));
			if (length != null)
			{
				bitArray.Length = length.Value;
			}

			return bitArray;
		}

		public static BitArray AsBitArray(this ulong @ulong) => new(BitConverter.GetBytes(@ulong));
		public static BitArray AsBitArray(this ushort @ushort) => new(BitConverter.GetBytes(@ushort));
		public static BitArray AsBitArray(this nuint nuint) => new(BitConverter.GetBytes(nuint));

		public static BitArray AsBitArray(this string value, Encoding? encoding = default)
		{
			encoding ??= Encoding.UTF8;
			var byteCount = encoding.GetByteCount(value);
			var buffer = ArrayPool<byte>.Shared.Rent(byteCount);
			try
			{
				var bufferSpan = buffer.AsSpan();
				byteCount = encoding.GetBytes(value, bufferSpan);
				BitArray result = new(buffer[..byteCount]);
				return result;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer);
			}
		}

		public static BitArray AsBitArray<T>(this T value, Encoding? encoding = default) => value switch
		{
			bool @bool => @bool.AsBitArray(),
			char @char => @char.AsBitArray(),
			byte @byte => @byte.AsBitArray(),
			double @double => @double.AsBitArray(),
			float @float => @float.AsBitArray(),
			short @short => @short.AsBitArray(),
			int @int => @int.AsBitArray(),
			long @long => @long.AsBitArray(),
			ulong @ulong => @ulong.AsBitArray(),
			ushort @ushort => @ushort.AsBitArray(),
			nuint @nuint => @nuint.AsBitArray(),
			nint @nint => @nint.AsBitArray(),
			DateTime @dateTime => @dateTime.AsBitArray(),
			string @string => @string.AsBitArray(encoding),

			_ => throw new ArgumentOutOfRangeException(nameof(value))
		};

		public static BitArray CloneWithLength(this BitArray bitArray, int newLength, bool value)
		{
			BitArray result = new(bitArray)
			{
				Length = newLength
			};
			result[^1] = value;
			return result;
		}

		public static bool[] AddValue(this bool[] array, bool value)
		{
			bool[] result = new bool[array.Length + 1];
			array.CopyTo(result, 0);
			result[array.Length] = value;
			return result;
		}

		public static int OptimalBitCount(this long value) => OptimalBitCount((long?)value);
		public static int OptimalBitCount(this long? value) => value switch
		{
			< 2 or null => 1,
			< 4 => 2,
			< 8 => 3,
			< 16 => 4,
			< 32 => 5,
			< 64 => 6,
			< 128 => 7,
			<= byte.MaxValue => 8,
			<= ushort.MaxValue => 16,
			<= uint.MaxValue => 32,
			_ => 64
		};

		public static BitArray RightShiftAndSet(this BitArray bitArray, bool value)
		{
			bitArray.Length++;
			//bitArray = bitArray.LeftShift(1);
			bitArray[^1] = value;
			return bitArray;
		}

		public static BitArray WithLength(this BitArray bitArray, int length)
		{
			bitArray.Length = length;
			return bitArray;
		}
	}
}
