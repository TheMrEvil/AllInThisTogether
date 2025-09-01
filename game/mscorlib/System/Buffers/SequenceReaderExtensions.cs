using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000AF0 RID: 2800
	public static class SequenceReaderExtensions
	{
		// Token: 0x060063BB RID: 25531 RVA: 0x0014E190 File Offset: 0x0014C390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryRead<[IsUnmanaged] T>(this SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			ReadOnlySpan<byte> unreadSpan = reader.UnreadSpan;
			if (unreadSpan.Length < sizeof(T))
			{
				return SequenceReaderExtensions.TryReadMultisegment<T>(ref reader, out value);
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(unreadSpan));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x0014E1DC File Offset: 0x0014C3DC
		private unsafe static bool TryReadMultisegment<[IsUnmanaged] T>(ref SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			T t = default(T);
			Span<byte> span = new Span<byte>((void*)(&t), sizeof(T));
			if (!reader.TryCopyTo(span))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(span));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x0014E232 File Offset: 0x0014C432
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0014E24A File Offset: 0x0014C44A
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0014E262 File Offset: 0x0014C462
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out short value)
		{
			if (ref reader.TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0014E279 File Offset: 0x0014C479
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0014E291 File Offset: 0x0014C491
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0014E2A9 File Offset: 0x0014C4A9
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out int value)
		{
			if (ref reader.TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0014E2C0 File Offset: 0x0014C4C0
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0014E2D8 File Offset: 0x0014C4D8
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return ref reader.TryRead(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0014E2F0 File Offset: 0x0014C4F0
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out long value)
		{
			if (ref reader.TryRead(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}
	}
}
