using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x02000104 RID: 260
	internal sealed class AsnWriter : IDisposable
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001A725 File Offset: 0x00018925
		public AsnEncodingRules RuleSet
		{
			[CompilerGenerated]
			get
			{
				return this.<RuleSet>k__BackingField;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001A72D File Offset: 0x0001892D
		public AsnWriter(AsnEncodingRules ruleSet)
		{
			if (ruleSet != AsnEncodingRules.BER && ruleSet != AsnEncodingRules.CER && ruleSet != AsnEncodingRules.DER)
			{
				throw new ArgumentOutOfRangeException("ruleSet");
			}
			this.RuleSet = ruleSet;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001A754 File Offset: 0x00018954
		public void Dispose()
		{
			this._nestingStack = null;
			if (this._buffer != null)
			{
				Array.Clear(this._buffer, 0, this._offset);
				ArrayPool<byte>.Shared.Return(this._buffer, false);
				this._buffer = null;
				this._offset = 0;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A7A4 File Offset: 0x000189A4
		private void EnsureWriteCapacity(int pendingCount)
		{
			if (pendingCount < 0)
			{
				throw new OverflowException();
			}
			if (this._buffer == null || this._buffer.Length - this._offset < pendingCount)
			{
				int num = checked(this._offset + pendingCount + 1023) / 1024;
				byte[] array = ArrayPool<byte>.Shared.Rent(1024 * num);
				if (this._buffer != null)
				{
					Buffer.BlockCopy(this._buffer, 0, array, 0, this._offset);
					Array.Clear(this._buffer, 0, this._offset);
					ArrayPool<byte>.Shared.Return(this._buffer, false);
				}
				this._buffer = array;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001A844 File Offset: 0x00018A44
		private void WriteTag(Asn1Tag tag)
		{
			int num = tag.CalculateEncodedSize();
			this.EnsureWriteCapacity(num);
			int num2;
			if (!tag.TryWrite(this._buffer.AsSpan(this._offset, num), out num2) || num2 != num)
			{
				throw new CryptographicException();
			}
			this._offset += num;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001A898 File Offset: 0x00018A98
		private void WriteLength(int length)
		{
			if (length == -1)
			{
				this.EnsureWriteCapacity(1);
				this._buffer[this._offset] = 128;
				this._offset++;
				return;
			}
			if (length < 128)
			{
				this.EnsureWriteCapacity(1 + length);
				this._buffer[this._offset] = (byte)length;
				this._offset++;
				return;
			}
			int encodedLengthSubsequentByteCount = AsnWriter.GetEncodedLengthSubsequentByteCount(length);
			this.EnsureWriteCapacity(encodedLengthSubsequentByteCount + 1 + length);
			this._buffer[this._offset] = (byte)(128 | encodedLengthSubsequentByteCount);
			int num = this._offset + encodedLengthSubsequentByteCount;
			int num2 = length;
			do
			{
				this._buffer[num] = (byte)num2;
				num2 >>= 8;
				num--;
			}
			while (num2 > 0);
			this._offset += encodedLengthSubsequentByteCount + 1;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001A958 File Offset: 0x00018B58
		private static int GetEncodedLengthSubsequentByteCount(int length)
		{
			if (length <= 127)
			{
				return 0;
			}
			if (length <= 255)
			{
				return 1;
			}
			if (length <= 65535)
			{
				return 2;
			}
			if (length <= 16777215)
			{
				return 3;
			}
			return 4;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A980 File Offset: 0x00018B80
		public void WriteEncodedValue(ReadOnlyMemory<byte> preEncodedValue)
		{
			AsnReader asnReader = new AsnReader(preEncodedValue, this.RuleSet);
			asnReader.GetEncodedValue();
			if (asnReader.HasData)
			{
				throw new ArgumentException("The input to WriteEncodedValue must represent a single encoded value with no trailing data.", "preEncodedValue");
			}
			this.EnsureWriteCapacity(preEncodedValue.Length);
			preEncodedValue.Span.CopyTo(this._buffer.AsSpan(this._offset));
			this._offset += preEncodedValue.Length;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		private void WriteEndOfContents()
		{
			this.EnsureWriteCapacity(2);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			this._offset = offset + 1;
			buffer[offset] = 0;
			byte[] buffer2 = this._buffer;
			offset = this._offset;
			this._offset = offset + 1;
			buffer2[offset] = 0;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001AA3E File Offset: 0x00018C3E
		public void WriteBoolean(bool value)
		{
			this.WriteBooleanCore(Asn1Tag.Boolean, value);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public void WriteBoolean(Asn1Tag tag, bool value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Boolean);
			this.WriteBooleanCore(tag.AsPrimitive(), value);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001AA63 File Offset: 0x00018C63
		private void WriteBooleanCore(Asn1Tag tag, bool value)
		{
			this.WriteTag(tag);
			this.WriteLength(1);
			this._buffer[this._offset] = (value ? byte.MaxValue : 0);
			this._offset++;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001AA9A File Offset: 0x00018C9A
		public void WriteInteger(long value)
		{
			this.WriteIntegerCore(Asn1Tag.Integer, value);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		public void WriteInteger(ulong value)
		{
			this.WriteNonNegativeIntegerCore(Asn1Tag.Integer, value);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001AAB6 File Offset: 0x00018CB6
		public void WriteInteger(BigInteger value)
		{
			this.WriteIntegerCore(Asn1Tag.Integer, value);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public void WriteInteger(ReadOnlySpan<byte> value)
		{
			this.WriteIntegerCore(Asn1Tag.Integer, value);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001AAD2 File Offset: 0x00018CD2
		public void WriteInteger(Asn1Tag tag, long value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Integer);
			this.WriteIntegerCore(tag.AsPrimitive(), value);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001AAEC File Offset: 0x00018CEC
		private void WriteIntegerCore(Asn1Tag tag, long value)
		{
			if (value >= 0L)
			{
				this.WriteNonNegativeIntegerCore(tag, (ulong)value);
				return;
			}
			int num;
			if (value >= -128L)
			{
				num = 1;
			}
			else if (value >= -32768L)
			{
				num = 2;
			}
			else if (value >= -8388608L)
			{
				num = 3;
			}
			else if (value >= -2147483648L)
			{
				num = 4;
			}
			else if (value >= -549755813888L)
			{
				num = 5;
			}
			else if (value >= -140737488355328L)
			{
				num = 6;
			}
			else if (value >= -36028797018963968L)
			{
				num = 7;
			}
			else
			{
				num = 8;
			}
			this.WriteTag(tag);
			this.WriteLength(num);
			long num2 = value;
			int num3 = this._offset + num - 1;
			do
			{
				this._buffer[num3] = (byte)num2;
				num2 >>= 8;
				num3--;
			}
			while (num3 >= this._offset);
			this._offset += num;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001ABAE File Offset: 0x00018DAE
		public void WriteInteger(Asn1Tag tag, ulong value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Integer);
			this.WriteNonNegativeIntegerCore(tag.AsPrimitive(), value);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		private void WriteNonNegativeIntegerCore(Asn1Tag tag, ulong value)
		{
			int num;
			if (value < 128UL)
			{
				num = 1;
			}
			else if (value < 32768UL)
			{
				num = 2;
			}
			else if (value < 8388608UL)
			{
				num = 3;
			}
			else if (value < (ulong)-2147483648)
			{
				num = 4;
			}
			else if (value < 549755813888UL)
			{
				num = 5;
			}
			else if (value < 140737488355328UL)
			{
				num = 6;
			}
			else if (value < 36028797018963968UL)
			{
				num = 7;
			}
			else if (value < 9223372036854775808UL)
			{
				num = 8;
			}
			else
			{
				num = 9;
			}
			this.WriteTag(tag);
			this.WriteLength(num);
			ulong num2 = value;
			int num3 = this._offset + num - 1;
			do
			{
				this._buffer[num3] = (byte)num2;
				num2 >>= 8;
				num3--;
			}
			while (num3 >= this._offset);
			this._offset += num;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001AC90 File Offset: 0x00018E90
		public void WriteInteger(Asn1Tag tag, BigInteger value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Integer);
			this.WriteIntegerCore(tag.AsPrimitive(), value);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001ACA7 File Offset: 0x00018EA7
		public void WriteInteger(Asn1Tag tag, ReadOnlySpan<byte> value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Integer);
			this.WriteIntegerCore(tag.AsPrimitive(), value);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		private unsafe void WriteIntegerCore(Asn1Tag tag, ReadOnlySpan<byte> value)
		{
			if (value.IsEmpty)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (value.Length > 1)
			{
				ushort num = (ushort)((int)(*value[0]) << 8 | (int)(*value[1])) & 65408;
				if (num == 0 || num == 65408)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			this.WriteTag(tag);
			this.WriteLength(value.Length);
			value.CopyTo(this._buffer.AsSpan(this._offset));
			this._offset += value.Length;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001AD60 File Offset: 0x00018F60
		private void WriteIntegerCore(Asn1Tag tag, BigInteger value)
		{
			byte[] array = value.ToByteArray();
			Array.Reverse<byte>(array);
			this.WriteTag(tag);
			this.WriteLength(array.Length);
			Buffer.BlockCopy(array, 0, this._buffer, this._offset, array.Length);
			this._offset += array.Length;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001ADB1 File Offset: 0x00018FB1
		public void WriteBitString(ReadOnlySpan<byte> bitString, int unusedBitCount = 0)
		{
			this.WriteBitStringCore(Asn1Tag.PrimitiveBitString, bitString, unusedBitCount);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		public void WriteBitString(Asn1Tag tag, ReadOnlySpan<byte> bitString, int unusedBitCount = 0)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.BitString);
			this.WriteBitStringCore(tag, bitString, unusedBitCount);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		private unsafe void WriteBitStringCore(Asn1Tag tag, ReadOnlySpan<byte> bitString, int unusedBitCount)
		{
			if (unusedBitCount < 0 || unusedBitCount > 7)
			{
				throw new ArgumentOutOfRangeException("unusedBitCount", unusedBitCount, "Unused bit count must be between 0 and 7, inclusive.");
			}
			if (bitString.Length == 0 && unusedBitCount != 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = (1 << unusedBitCount) - 1;
			if ((((!bitString.IsEmpty && *bitString[bitString.Length - 1] != 0) ? 1 : 0) & num) != 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (this.RuleSet == AsnEncodingRules.CER && bitString.Length >= 1000)
			{
				this.WriteConstructedCerBitString(tag, bitString, unusedBitCount);
				return;
			}
			this.WriteTag(tag.AsPrimitive());
			this.WriteLength(bitString.Length + 1);
			this._buffer[this._offset] = (byte)unusedBitCount;
			this._offset++;
			bitString.CopyTo(this._buffer.AsSpan(this._offset));
			this._offset += bitString.Length;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001AED4 File Offset: 0x000190D4
		private void WriteConstructedCerBitString(Asn1Tag tag, ReadOnlySpan<byte> payload, int unusedBitCount)
		{
			this.WriteTag(tag.AsConstructed());
			this.WriteLength(-1);
			int num2;
			int num = Math.DivRem(payload.Length, 999, out num2);
			int num3;
			if (num2 == 0)
			{
				num3 = 0;
			}
			else
			{
				num3 = 3 + num2 + AsnWriter.GetEncodedLengthSubsequentByteCount(num2);
			}
			int pendingCount = num * 1004 + num3 + 2;
			this.EnsureWriteCapacity(pendingCount);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			ReadOnlySpan<byte> readOnlySpan = payload;
			Asn1Tag primitiveBitString = Asn1Tag.PrimitiveBitString;
			Span<byte> destination;
			while (readOnlySpan.Length > 999)
			{
				this.WriteTag(primitiveBitString);
				this.WriteLength(1000);
				this._buffer[this._offset] = 0;
				this._offset++;
				destination = this._buffer.AsSpan(this._offset);
				readOnlySpan.Slice(0, 999).CopyTo(destination);
				readOnlySpan = readOnlySpan.Slice(999);
				this._offset += 999;
			}
			this.WriteTag(primitiveBitString);
			this.WriteLength(readOnlySpan.Length + 1);
			this._buffer[this._offset] = (byte)unusedBitCount;
			this._offset++;
			destination = this._buffer.AsSpan(this._offset);
			readOnlySpan.CopyTo(destination);
			this._offset += readOnlySpan.Length;
			this.WriteEndOfContents();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B039 File Offset: 0x00019239
		public void WriteNamedBitList(object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			this.WriteNamedBitList(Asn1Tag.PrimitiveBitString, enumValue);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B055 File Offset: 0x00019255
		public void WriteNamedBitList<TEnum>(TEnum enumValue) where TEnum : struct
		{
			this.WriteNamedBitList<TEnum>(Asn1Tag.PrimitiveBitString, enumValue);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001B063 File Offset: 0x00019263
		public void WriteNamedBitList(Asn1Tag tag, object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			this.WriteNamedBitList(tag, enumValue.GetType(), enumValue);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B081 File Offset: 0x00019281
		public void WriteNamedBitList<TEnum>(Asn1Tag tag, TEnum enumValue) where TEnum : struct
		{
			this.WriteNamedBitList(tag, typeof(TEnum), enumValue);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B09C File Offset: 0x0001929C
		private void WriteNamedBitList(Asn1Tag tag, Type tEnum, object enumValue)
		{
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (!tEnum.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("Named bit list operations require an enum with the [Flags] attribute.", "tEnum");
			}
			ulong integralValue;
			if (enumUnderlyingType == typeof(ulong))
			{
				integralValue = Convert.ToUInt64(enumValue);
			}
			else
			{
				integralValue = (ulong)Convert.ToInt64(enumValue);
			}
			this.WriteNamedBitList(tag, integralValue);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B0FC File Offset: 0x000192FC
		private unsafe void WriteNamedBitList(Asn1Tag tag, ulong integralValue)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			span.Clear();
			int num = -1;
			int num2 = 0;
			while (integralValue != 0UL)
			{
				if ((integralValue & 1UL) != 0UL)
				{
					ref byte ptr = ref span[num2 / 8];
					ptr |= (byte)(128 >> num2 % 8);
					num = num2;
				}
				integralValue >>= 1;
				num2++;
			}
			if (num < 0)
			{
				this.WriteBitString(tag, ReadOnlySpan<byte>.Empty, 0);
				return;
			}
			int length = num / 8 + 1;
			int unusedBitCount = 7 - num % 8;
			this.WriteBitString(tag, span.Slice(0, length), unusedBitCount);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B187 File Offset: 0x00019387
		public void WriteOctetString(ReadOnlySpan<byte> octetString)
		{
			this.WriteOctetString(Asn1Tag.PrimitiveOctetString, octetString);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B195 File Offset: 0x00019395
		public void WriteOctetString(Asn1Tag tag, ReadOnlySpan<byte> octetString)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.OctetString);
			this.WriteOctetStringCore(tag, octetString);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001B1A8 File Offset: 0x000193A8
		private void WriteOctetStringCore(Asn1Tag tag, ReadOnlySpan<byte> octetString)
		{
			if (this.RuleSet == AsnEncodingRules.CER && octetString.Length > 1000)
			{
				this.WriteConstructedCerOctetString(tag, octetString);
				return;
			}
			this.WriteTag(tag.AsPrimitive());
			this.WriteLength(octetString.Length);
			octetString.CopyTo(this._buffer.AsSpan(this._offset));
			this._offset += octetString.Length;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001B21C File Offset: 0x0001941C
		private void WriteConstructedCerOctetString(Asn1Tag tag, ReadOnlySpan<byte> payload)
		{
			this.WriteTag(tag.AsConstructed());
			this.WriteLength(-1);
			int num2;
			int num = Math.DivRem(payload.Length, 1000, out num2);
			int num3;
			if (num2 == 0)
			{
				num3 = 0;
			}
			else
			{
				num3 = 2 + num2 + AsnWriter.GetEncodedLengthSubsequentByteCount(num2);
			}
			int pendingCount = num * 1004 + num3 + 2;
			this.EnsureWriteCapacity(pendingCount);
			byte[] buffer = this._buffer;
			int offset = this._offset;
			ReadOnlySpan<byte> readOnlySpan = payload;
			Asn1Tag primitiveOctetString = Asn1Tag.PrimitiveOctetString;
			Span<byte> destination;
			while (readOnlySpan.Length > 1000)
			{
				this.WriteTag(primitiveOctetString);
				this.WriteLength(1000);
				destination = this._buffer.AsSpan(this._offset);
				readOnlySpan.Slice(0, 1000).CopyTo(destination);
				this._offset += 1000;
				readOnlySpan = readOnlySpan.Slice(1000);
			}
			this.WriteTag(primitiveOctetString);
			this.WriteLength(readOnlySpan.Length);
			destination = this._buffer.AsSpan(this._offset);
			readOnlySpan.CopyTo(destination);
			this._offset += readOnlySpan.Length;
			this.WriteEndOfContents();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001B343 File Offset: 0x00019543
		public void WriteNull()
		{
			this.WriteNullCore(Asn1Tag.Null);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001B350 File Offset: 0x00019550
		public void WriteNull(Asn1Tag tag)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Null);
			this.WriteNullCore(tag.AsPrimitive());
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001B366 File Offset: 0x00019566
		private void WriteNullCore(Asn1Tag tag)
		{
			this.WriteTag(tag);
			this.WriteLength(0);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001B376 File Offset: 0x00019576
		public void WriteObjectIdentifier(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this.WriteObjectIdentifier(oid.Value);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001B392 File Offset: 0x00019592
		public void WriteObjectIdentifier(string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			this.WriteObjectIdentifier(oidValue.AsSpan());
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001B3AE File Offset: 0x000195AE
		public void WriteObjectIdentifier(ReadOnlySpan<char> oidValue)
		{
			this.WriteObjectIdentifierCore(Asn1Tag.ObjectIdentifier, oidValue);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001B3BC File Offset: 0x000195BC
		public void WriteObjectIdentifier(Asn1Tag tag, Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this.WriteObjectIdentifier(tag, oid.Value);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B3D9 File Offset: 0x000195D9
		public void WriteObjectIdentifier(Asn1Tag tag, string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			this.WriteObjectIdentifier(tag, oidValue.AsSpan());
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001B3F6 File Offset: 0x000195F6
		public void WriteObjectIdentifier(Asn1Tag tag, ReadOnlySpan<char> oidValue)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.ObjectIdentifier);
			this.WriteObjectIdentifierCore(tag.AsPrimitive(), oidValue);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B410 File Offset: 0x00019610
		private unsafe void WriteObjectIdentifierCore(Asn1Tag tag, ReadOnlySpan<char> oidValue)
		{
			if (oidValue.Length < 3)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			if (*oidValue[1] != 46)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(oidValue.Length / 2);
			int num = 0;
			try
			{
				int num2;
				switch (*oidValue[0])
				{
				case 48:
					num2 = 0;
					break;
				case 49:
					num2 = 1;
					break;
				case 50:
					num2 = 2;
					break;
				default:
					throw new CryptographicException("The OID value was invalid.");
				}
				ReadOnlySpan<char> readOnlySpan = oidValue.Slice(2);
				BigInteger left = AsnWriter.ParseSubIdentifier(ref readOnlySpan);
				left += 40 * num2;
				int num3 = AsnWriter.EncodeSubIdentifier(array.AsSpan(num), ref left);
				num += num3;
				while (!readOnlySpan.IsEmpty)
				{
					left = AsnWriter.ParseSubIdentifier(ref readOnlySpan);
					num3 = AsnWriter.EncodeSubIdentifier(array.AsSpan(num), ref left);
					num += num3;
				}
				this.WriteTag(tag);
				this.WriteLength(num);
				Buffer.BlockCopy(array, 0, this._buffer, this._offset, num);
				this._offset += num;
			}
			finally
			{
				Array.Clear(array, 0, num);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B554 File Offset: 0x00019754
		private unsafe static BigInteger ParseSubIdentifier(ref ReadOnlySpan<char> oidValue)
		{
			int num = oidValue.IndexOf('.');
			if (num == -1)
			{
				num = oidValue.Length;
			}
			else if (num == oidValue.Length - 1)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			BigInteger bigInteger = BigInteger.Zero;
			for (int i = 0; i < num; i++)
			{
				if (i > 0 && bigInteger == 0L)
				{
					throw new CryptographicException("The OID value was invalid.");
				}
				bigInteger *= 10;
				bigInteger += AsnWriter.AtoI((char)(*oidValue[i]));
			}
			oidValue = oidValue.Slice(Math.Min(oidValue.Length, num + 1));
			return bigInteger;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B5FF File Offset: 0x000197FF
		private static int AtoI(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			throw new CryptographicException("The OID value was invalid.");
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B61C File Offset: 0x0001981C
		private unsafe static int EncodeSubIdentifier(Span<byte> dest, ref BigInteger subIdentifier)
		{
			if (subIdentifier.IsZero)
			{
				*dest[0] = 0;
				return 1;
			}
			BigInteger bigInteger = subIdentifier;
			int num = 0;
			do
			{
				byte b = (byte)(bigInteger & 127);
				if (subIdentifier != bigInteger)
				{
					b |= 128;
				}
				bigInteger >>= 7;
				*dest[num] = b;
				num++;
			}
			while (bigInteger != BigInteger.Zero);
			AsnWriter.Reverse(dest.Slice(0, num));
			return num;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B6A2 File Offset: 0x000198A2
		public void WriteEnumeratedValue(object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			this.WriteEnumeratedValue(Asn1Tag.Enumerated, enumValue);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001B6BE File Offset: 0x000198BE
		public void WriteEnumeratedValue<TEnum>(TEnum value) where TEnum : struct
		{
			this.WriteEnumeratedValue<TEnum>(Asn1Tag.Enumerated, value);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001B6CC File Offset: 0x000198CC
		public void WriteEnumeratedValue(Asn1Tag tag, object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			this.WriteEnumeratedValue(tag.AsPrimitive(), enumValue.GetType(), enumValue);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public void WriteEnumeratedValue<TEnum>(Asn1Tag tag, TEnum value) where TEnum : struct
		{
			this.WriteEnumeratedValue(tag.AsPrimitive(), typeof(TEnum), value);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B710 File Offset: 0x00019910
		private void WriteEnumeratedValue(Asn1Tag tag, Type tEnum, object enumValue)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Enumerated);
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (tEnum.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.", "tEnum");
			}
			if (enumUnderlyingType == typeof(ulong))
			{
				ulong value = Convert.ToUInt64(enumValue);
				this.WriteNonNegativeIntegerCore(tag, value);
				return;
			}
			long value2 = Convert.ToInt64(enumValue);
			this.WriteIntegerCore(tag, value2);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B77E File Offset: 0x0001997E
		public void PushSequence()
		{
			this.PushSequenceCore(Asn1Tag.Sequence);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B78B File Offset: 0x0001998B
		public void PushSequence(Asn1Tag tag)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Sequence);
			this.PushSequenceCore(tag.AsConstructed());
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B7A2 File Offset: 0x000199A2
		private void PushSequenceCore(Asn1Tag tag)
		{
			this.PushTag(tag.AsConstructed());
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001B7B1 File Offset: 0x000199B1
		public void PopSequence()
		{
			this.PopSequence(Asn1Tag.Sequence);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001B7BE File Offset: 0x000199BE
		public void PopSequence(Asn1Tag tag)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Sequence);
			this.PopSequenceCore(tag.AsConstructed());
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001B7D5 File Offset: 0x000199D5
		private void PopSequenceCore(Asn1Tag tag)
		{
			this.PopTag(tag, false);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001B7DF File Offset: 0x000199DF
		public void PushSetOf()
		{
			this.PushSetOf(Asn1Tag.SetOf);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001B7EC File Offset: 0x000199EC
		public void PushSetOf(Asn1Tag tag)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Set);
			this.PushSetOfCore(tag.AsConstructed());
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001B803 File Offset: 0x00019A03
		private void PushSetOfCore(Asn1Tag tag)
		{
			this.PushTag(tag);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001B80C File Offset: 0x00019A0C
		public void PopSetOf()
		{
			this.PopSetOfCore(Asn1Tag.SetOf);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001B819 File Offset: 0x00019A19
		public void PopSetOf(Asn1Tag tag)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.Set);
			this.PopSetOfCore(tag.AsConstructed());
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001B830 File Offset: 0x00019A30
		private void PopSetOfCore(Asn1Tag tag)
		{
			bool sortContents = this.RuleSet == AsnEncodingRules.CER || this.RuleSet == AsnEncodingRules.DER;
			this.PopTag(tag, sortContents);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001B85B File Offset: 0x00019A5B
		public void WriteUtcTime(DateTimeOffset value)
		{
			this.WriteUtcTimeCore(Asn1Tag.UtcTime, value);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001B869 File Offset: 0x00019A69
		public void WriteUtcTime(Asn1Tag tag, DateTimeOffset value)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.UtcTime);
			this.WriteUtcTimeCore(tag.AsPrimitive(), value);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001B881 File Offset: 0x00019A81
		public void WriteUtcTime(DateTimeOffset value, int minLegalYear)
		{
			if (minLegalYear <= value.Year && value.Year < minLegalYear + 100)
			{
				this.WriteUtcTime(value);
				return;
			}
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001B8AC File Offset: 0x00019AAC
		private void WriteUtcTimeCore(Asn1Tag tag, DateTimeOffset value)
		{
			this.WriteTag(tag);
			this.WriteLength(13);
			DateTimeOffset dateTimeOffset = value.ToUniversalTime();
			int year = dateTimeOffset.Year;
			int month = dateTimeOffset.Month;
			int day = dateTimeOffset.Day;
			int hour = dateTimeOffset.Hour;
			int minute = dateTimeOffset.Minute;
			int second = dateTimeOffset.Second;
			Span<byte> span = this._buffer.AsSpan(this._offset);
			StandardFormat format = new StandardFormat('D', 2);
			int num;
			if (!Utf8Formatter.TryFormat(year % 100, span.Slice(0, 2), out num, format) || !Utf8Formatter.TryFormat(month, span.Slice(2, 2), out num, format) || !Utf8Formatter.TryFormat(day, span.Slice(4, 2), out num, format) || !Utf8Formatter.TryFormat(hour, span.Slice(6, 2), out num, format) || !Utf8Formatter.TryFormat(minute, span.Slice(8, 2), out num, format) || !Utf8Formatter.TryFormat(second, span.Slice(10, 2), out num, format))
			{
				throw new CryptographicException();
			}
			this._buffer[this._offset + 12] = 90;
			this._offset += 13;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public void WriteGeneralizedTime(DateTimeOffset value, bool omitFractionalSeconds = false)
		{
			this.WriteGeneralizedTimeCore(Asn1Tag.GeneralizedTime, value, omitFractionalSeconds);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001B9D7 File Offset: 0x00019BD7
		public void WriteGeneralizedTime(Asn1Tag tag, DateTimeOffset value, bool omitFractionalSeconds = false)
		{
			AsnWriter.CheckUniversalTag(tag, UniversalTagNumber.GeneralizedTime);
			this.WriteGeneralizedTimeCore(tag.AsPrimitive(), value, omitFractionalSeconds);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		private unsafe void WriteGeneralizedTimeCore(Asn1Tag tag, DateTimeOffset value, bool omitFractionalSeconds)
		{
			DateTimeOffset dateTimeOffset = value.ToUniversalTime();
			if (dateTimeOffset.Year > 9999)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			Span<byte> destination = default(Span<byte>);
			if (!omitFractionalSeconds)
			{
				long num = dateTimeOffset.Ticks % 10000000L;
				if (num != 0L)
				{
					destination = new Span<byte>(stackalloc byte[(UIntPtr)9], 9);
					int num2;
					if (!Utf8Formatter.TryFormat(num / 10000000m, destination, out num2, new StandardFormat('G', 255)))
					{
						throw new CryptographicException();
					}
					destination = destination.Slice(1, num2 - 1);
				}
			}
			int length = 15 + destination.Length;
			this.WriteTag(tag);
			this.WriteLength(length);
			int year = dateTimeOffset.Year;
			int month = dateTimeOffset.Month;
			int day = dateTimeOffset.Day;
			int hour = dateTimeOffset.Hour;
			int minute = dateTimeOffset.Minute;
			int second = dateTimeOffset.Second;
			Span<byte> span = this._buffer.AsSpan(this._offset);
			StandardFormat format = new StandardFormat('D', 4);
			StandardFormat format2 = new StandardFormat('D', 2);
			int num3;
			if (!Utf8Formatter.TryFormat(year, span.Slice(0, 4), out num3, format) || !Utf8Formatter.TryFormat(month, span.Slice(4, 2), out num3, format2) || !Utf8Formatter.TryFormat(day, span.Slice(6, 2), out num3, format2) || !Utf8Formatter.TryFormat(hour, span.Slice(8, 2), out num3, format2) || !Utf8Formatter.TryFormat(minute, span.Slice(10, 2), out num3, format2) || !Utf8Formatter.TryFormat(second, span.Slice(12, 2), out num3, format2))
			{
				throw new CryptographicException();
			}
			this._offset += 14;
			destination.CopyTo(span.Slice(14));
			this._offset += destination.Length;
			this._buffer[this._offset] = 90;
			this._offset++;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public bool TryEncode(Span<byte> dest, out int bytesWritten)
		{
			Stack<ValueTuple<Asn1Tag, int>> nestingStack = this._nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (dest.Length < this._offset)
			{
				bytesWritten = 0;
				return false;
			}
			if (this._offset == 0)
			{
				bytesWritten = 0;
				return true;
			}
			bytesWritten = this._offset;
			this._buffer.AsSpan(0, this._offset).CopyTo(dest);
			return true;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001BC4C File Offset: 0x00019E4C
		public byte[] Encode()
		{
			Stack<ValueTuple<Asn1Tag, int>> nestingStack = this._nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (this._offset == 0)
			{
				return Array.Empty<byte>();
			}
			return this._buffer.AsSpan(0, this._offset).ToArray();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public ReadOnlySpan<byte> EncodeAsSpan()
		{
			Stack<ValueTuple<Asn1Tag, int>> nestingStack = this._nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (this._offset == 0)
			{
				return ReadOnlySpan<byte>.Empty;
			}
			return new ReadOnlySpan<byte>(this._buffer, 0, this._offset);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001BCEC File Offset: 0x00019EEC
		private void PushTag(Asn1Tag tag)
		{
			if (this._nestingStack == null)
			{
				this._nestingStack = new Stack<ValueTuple<Asn1Tag, int>>();
			}
			this.WriteTag(tag);
			this._nestingStack.Push(new ValueTuple<Asn1Tag, int>(tag, this._offset));
			this.WriteLength(-1);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001BD28 File Offset: 0x00019F28
		private void PopTag(Asn1Tag tag, bool sortContents = false)
		{
			if (this._nestingStack == null || this._nestingStack.Count == 0)
			{
				throw new ArgumentException("Cannot pop the requested tag as it is not currently in progress.", "tag");
			}
			ValueTuple<Asn1Tag, int> valueTuple = this._nestingStack.Peek();
			Asn1Tag item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			if (item != tag)
			{
				throw new ArgumentException("Cannot pop the requested tag as it is not currently in progress.", "tag");
			}
			this._nestingStack.Pop();
			if (sortContents)
			{
				AsnWriter.SortContents(this._buffer, item2 + 1, this._offset);
			}
			if (this.RuleSet == AsnEncodingRules.CER)
			{
				this.WriteEndOfContents();
				return;
			}
			int num = this._offset - 1 - item2;
			int encodedLengthSubsequentByteCount = AsnWriter.GetEncodedLengthSubsequentByteCount(num);
			if (encodedLengthSubsequentByteCount == 0)
			{
				this._buffer[item2] = (byte)num;
				return;
			}
			this.EnsureWriteCapacity(encodedLengthSubsequentByteCount);
			int num2 = item2 + 1;
			Buffer.BlockCopy(this._buffer, num2, this._buffer, num2 + encodedLengthSubsequentByteCount, num);
			int offset = this._offset;
			this._offset = item2;
			this.WriteLength(num);
			this._offset = offset + encodedLengthSubsequentByteCount;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001BE22 File Offset: 0x0001A022
		public void WriteCharacterString(UniversalTagNumber encodingType, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.WriteCharacterString(encodingType, str.AsSpan());
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001BE40 File Offset: 0x0001A040
		public void WriteCharacterString(UniversalTagNumber encodingType, ReadOnlySpan<char> str)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			this.WriteCharacterStringCore(new Asn1Tag(encodingType, false), encoding, str);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001BE63 File Offset: 0x0001A063
		public void WriteCharacterString(Asn1Tag tag, UniversalTagNumber encodingType, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.WriteCharacterString(tag, encodingType, str.AsSpan());
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001BE84 File Offset: 0x0001A084
		public void WriteCharacterString(Asn1Tag tag, UniversalTagNumber encodingType, ReadOnlySpan<char> str)
		{
			AsnWriter.CheckUniversalTag(tag, encodingType);
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			this.WriteCharacterStringCore(tag, encoding, str);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		private unsafe void WriteCharacterStringCore(Asn1Tag tag, Encoding encoding, ReadOnlySpan<char> str)
		{
			int num = -1;
			if (this.RuleSet == AsnEncodingRules.CER)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(str))
				{
					char* chars = reference;
					num = encoding.GetByteCount(chars, str.Length);
					if (num > 1000)
					{
						this.WriteConstructedCerCharacterString(tag, encoding, str, num);
						return;
					}
				}
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(str))
			{
				char* chars2 = reference;
				if (num < 0)
				{
					num = encoding.GetByteCount(chars2, str.Length);
				}
				this.WriteTag(tag.AsPrimitive());
				this.WriteLength(num);
				Span<byte> span = this._buffer.AsSpan(this._offset, num);
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(span))
				{
					byte* bytes = reference2;
					if (encoding.GetBytes(chars2, str.Length, bytes, span.Length) != num)
					{
						throw new InvalidOperationException();
					}
				}
				this._offset += num;
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001BF78 File Offset: 0x0001A178
		private unsafe void WriteConstructedCerCharacterString(Asn1Tag tag, Encoding encoding, ReadOnlySpan<char> str, int size)
		{
			byte[] array;
			fixed (char* reference = MemoryMarshal.GetReference<char>(str))
			{
				char* chars = reference;
				array = ArrayPool<byte>.Shared.Rent(size);
				byte[] array2;
				byte* bytes;
				if ((array2 = array) == null || array2.Length == 0)
				{
					bytes = null;
				}
				else
				{
					bytes = &array2[0];
				}
				if (encoding.GetBytes(chars, str.Length, bytes, array.Length) != size)
				{
					throw new InvalidOperationException();
				}
				array2 = null;
			}
			this.WriteConstructedCerOctetString(tag, array.AsSpan(0, size));
			Array.Clear(array, 0, size);
			ArrayPool<byte>.Shared.Return(array, false);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001C004 File Offset: 0x0001A204
		private static void SortContents(byte[] buffer, int start, int end)
		{
			int num = end - start;
			if (num == 0)
			{
				return;
			}
			AsnReader asnReader = new AsnReader(new ReadOnlyMemory<byte>(buffer, start, num), AsnEncodingRules.BER);
			List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
			int num2 = start;
			while (asnReader.HasData)
			{
				ReadOnlyMemory<byte> encodedValue = asnReader.GetEncodedValue();
				list.Add(new ValueTuple<int, int>(num2, encodedValue.Length));
				num2 += encodedValue.Length;
			}
			AsnWriter.ArrayIndexSetOfValueComparer comparer = new AsnWriter.ArrayIndexSetOfValueComparer(buffer);
			list.Sort(comparer);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			num2 = 0;
			foreach (ValueTuple<int, int> valueTuple in list)
			{
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				Buffer.BlockCopy(buffer, item, array, num2, item2);
				num2 += item2;
			}
			Buffer.BlockCopy(array, 0, buffer, start, num);
			Array.Clear(array, 0, num);
			ArrayPool<byte>.Shared.Return(array, false);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001C0FC File Offset: 0x0001A2FC
		internal unsafe static void Reverse(Span<byte> span)
		{
			int i = 0;
			int num = span.Length - 1;
			while (i < num)
			{
				byte b = *span[i];
				*span[i] = *span[num];
				*span[num] = b;
				i++;
				num--;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001C149 File Offset: 0x0001A349
		private static void CheckUniversalTag(Asn1Tag tag, UniversalTagNumber universalTagNumber)
		{
			if (tag.TagClass == TagClass.Universal && tag.TagValue != (int)universalTagNumber)
			{
				throw new ArgumentException("Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.", "tag");
			}
		}

		// Token: 0x04000417 RID: 1047
		private byte[] _buffer;

		// Token: 0x04000418 RID: 1048
		private int _offset;

		// Token: 0x04000419 RID: 1049
		private Stack<ValueTuple<Asn1Tag, int>> _nestingStack;

		// Token: 0x0400041A RID: 1050
		[CompilerGenerated]
		private readonly AsnEncodingRules <RuleSet>k__BackingField;

		// Token: 0x02000105 RID: 261
		private class ArrayIndexSetOfValueComparer : IComparer<ValueTuple<int, int>>
		{
			// Token: 0x060006BF RID: 1727 RVA: 0x0001C16E File Offset: 0x0001A36E
			public ArrayIndexSetOfValueComparer(byte[] data)
			{
				this._data = data;
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0001C180 File Offset: 0x0001A380
			public int Compare(ValueTuple<int, int> x, ValueTuple<int, int> y)
			{
				int item = x.Item1;
				int item2 = x.Item2;
				int item3 = y.Item1;
				int item4 = y.Item2;
				int num = SetOfValueComparer.Instance.Compare(new ReadOnlyMemory<byte>(this._data, item, item2), new ReadOnlyMemory<byte>(this._data, item3, item4));
				if (num == 0)
				{
					return item - item3;
				}
				return num;
			}

			// Token: 0x0400041B RID: 1051
			private readonly byte[] _data;
		}
	}
}
