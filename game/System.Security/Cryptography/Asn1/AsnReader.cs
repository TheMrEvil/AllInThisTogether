using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x02000101 RID: 257
	internal class AsnReader
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00017CC6 File Offset: 0x00015EC6
		public bool HasData
		{
			get
			{
				return !this._data.IsEmpty;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00017CD6 File Offset: 0x00015ED6
		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet)
		{
			AsnReader.CheckEncodingRules(ruleSet);
			this._data = data;
			this._ruleSet = ruleSet;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00017CF2 File Offset: 0x00015EF2
		public void ThrowIfNotEmpty()
		{
			if (this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00017D07 File Offset: 0x00015F07
		public static bool TryPeekTag(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesRead)
		{
			return Asn1Tag.TryParse(source, out tag, out bytesRead);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00017D14 File Offset: 0x00015F14
		public Asn1Tag PeekTag()
		{
			Asn1Tag result;
			int num;
			if (AsnReader.TryPeekTag(this._data.Span, out result, out num))
			{
				return result;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00017D44 File Offset: 0x00015F44
		private unsafe static bool TryReadLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int? length, out int bytesRead)
		{
			length = null;
			bytesRead = 0;
			AsnReader.CheckEncodingRules(ruleSet);
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = *source[bytesRead];
			bytesRead++;
			if (b == 128)
			{
				if (ruleSet == AsnEncodingRules.DER)
				{
					bytesRead = 0;
					return false;
				}
				return true;
			}
			else
			{
				if (b < 128)
				{
					length = new int?((int)b);
					return true;
				}
				if (b == 255)
				{
					bytesRead = 0;
					return false;
				}
				byte b2 = (byte)((int)b & -129);
				if ((int)(b2 + 1) > source.Length)
				{
					bytesRead = 0;
					return false;
				}
				bool flag = ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER;
				if (flag && b2 > 4)
				{
					bytesRead = 0;
					return false;
				}
				uint num = 0U;
				for (int i = 0; i < (int)b2; i++)
				{
					byte b3 = *source[bytesRead];
					bytesRead++;
					if (num == 0U)
					{
						if (flag && b3 == 0)
						{
							bytesRead = 0;
							return false;
						}
						if (!flag && b3 != 0 && (int)b2 - i > 4)
						{
							bytesRead = 0;
							return false;
						}
					}
					num <<= 8;
					num |= (uint)b3;
				}
				if (num > 2147483647U)
				{
					bytesRead = 0;
					return false;
				}
				if (flag && num < 128U)
				{
					bytesRead = 0;
					return false;
				}
				length = new int?((int)num);
				return true;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00017E60 File Offset: 0x00016060
		internal Asn1Tag ReadTagAndLength(out int? contentsLength, out int bytesRead)
		{
			Asn1Tag result;
			int num;
			int? num2;
			int num3;
			if (AsnReader.TryPeekTag(this._data.Span, out result, out num) && AsnReader.TryReadLength(this._data.Slice(num).Span, this._ruleSet, out num2, out num3))
			{
				int num4 = num + num3;
				if (result.IsConstructed)
				{
					if (this._ruleSet == AsnEncodingRules.CER && num2 != null)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				else if (num2 == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bytesRead = num4;
				contentsLength = num2;
				return result;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00017F00 File Offset: 0x00016100
		private static void ValidateEndOfContents(Asn1Tag tag, int? length, int headerLength)
		{
			if (!tag.IsConstructed)
			{
				int? num = length;
				int num2 = 0;
				if ((num.GetValueOrDefault() == num2 & num != null) && headerLength == 2)
				{
					return;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00017F40 File Offset: 0x00016140
		private int SeekEndOfContents(ReadOnlyMemory<byte> source)
		{
			int num = 0;
			AsnReader asnReader = new AsnReader(source, this._ruleSet);
			int num2 = 1;
			while (asnReader.HasData)
			{
				int? length;
				int num3;
				Asn1Tag asn1Tag = asnReader.ReadTagAndLength(out length, out num3);
				if (asn1Tag == Asn1Tag.EndOfContents)
				{
					AsnReader.ValidateEndOfContents(asn1Tag, length, num3);
					num2--;
					if (num2 == 0)
					{
						return num;
					}
				}
				if (length == null)
				{
					num2++;
					asnReader._data = asnReader._data.Slice(num3);
					num += num3;
				}
				else
				{
					ReadOnlyMemory<byte> readOnlyMemory = AsnReader.Slice(asnReader._data, 0, new int?(num3 + length.Value));
					asnReader._data = asnReader._data.Slice(readOnlyMemory.Length);
					num += readOnlyMemory.Length;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001800C File Offset: 0x0001620C
		public ReadOnlyMemory<byte> PeekEncodedValue()
		{
			int? num;
			int num2;
			this.ReadTagAndLength(out num, out num2);
			if (num == null)
			{
				int num3 = this.SeekEndOfContents(this._data.Slice(num2));
				return AsnReader.Slice(this._data, 0, new int?(num2 + num3 + 2));
			}
			return AsnReader.Slice(this._data, 0, new int?(num2 + num.Value));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00018074 File Offset: 0x00016274
		public ReadOnlyMemory<byte> PeekContentBytes()
		{
			int? num;
			int num2;
			this.ReadTagAndLength(out num, out num2);
			if (num == null)
			{
				return AsnReader.Slice(this._data, num2, new int?(this.SeekEndOfContents(this._data.Slice(num2))));
			}
			return AsnReader.Slice(this._data, num2, new int?(num.Value));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000180D4 File Offset: 0x000162D4
		public ReadOnlyMemory<byte> GetEncodedValue()
		{
			ReadOnlyMemory<byte> result = this.PeekEncodedValue();
			this._data = this._data.Slice(result.Length);
			return result;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00018104 File Offset: 0x00016304
		private unsafe static bool ReadBooleanValue(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet)
		{
			if (source.Length != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = *source[0];
			if (b == 0)
			{
				return false;
			}
			if (b != 255 && (ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return true;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018151 File Offset: 0x00016351
		public bool ReadBoolean()
		{
			return this.ReadBoolean(Asn1Tag.Boolean);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00018160 File Offset: 0x00016360
		public bool ReadBoolean(Asn1Tag expectedTag)
		{
			int? num;
			int num2;
			Asn1Tag tag = this.ReadTagAndLength(out num, out num2);
			AsnReader.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Boolean);
			if (tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bool result = AsnReader.ReadBooleanValue(AsnReader.Slice(this._data, num2, new int?(num.Value)).Span, this._ruleSet);
			this._data = this._data.Slice(num2 + num.Value);
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000181DC File Offset: 0x000163DC
		private unsafe ReadOnlyMemory<byte> GetIntegerContents(Asn1Tag expectedTag, UniversalTagNumber tagNumber, out int headerLength)
		{
			int? num;
			Asn1Tag tag = this.ReadTagAndLength(out num, out headerLength);
			AsnReader.CheckExpectedTag(tag, expectedTag, tagNumber);
			if (!tag.IsConstructed)
			{
				int? num2 = num;
				int num3 = 1;
				if (!(num2.GetValueOrDefault() < num3 & num2 != null))
				{
					ReadOnlyMemory<byte> result = AsnReader.Slice(this._data, headerLength, new int?(num.Value));
					ReadOnlySpan<byte> span = result.Span;
					if (result.Length > 1)
					{
						ushort num4 = (ushort)((int)(*span[0]) << 8 | (int)(*span[1])) & 65408;
						if (num4 == 0 || num4 == 65408)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
					}
					return result;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001828E File Offset: 0x0001648E
		public ReadOnlyMemory<byte> GetIntegerBytes()
		{
			return this.GetIntegerBytes(Asn1Tag.Integer);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001829C File Offset: 0x0001649C
		public ReadOnlyMemory<byte> GetIntegerBytes(Asn1Tag expectedTag)
		{
			int num;
			ReadOnlyMemory<byte> integerContents = this.GetIntegerContents(expectedTag, UniversalTagNumber.Integer, out num);
			this._data = this._data.Slice(num + integerContents.Length);
			return integerContents;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000182CF File Offset: 0x000164CF
		public BigInteger GetInteger()
		{
			return this.GetInteger(Asn1Tag.Integer);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000182DC File Offset: 0x000164DC
		public unsafe BigInteger GetInteger(Asn1Tag expectedTag)
		{
			int num;
			ReadOnlyMemory<byte> integerContents = this.GetIntegerContents(expectedTag, UniversalTagNumber.Integer, out num);
			byte[] array = ArrayPool<byte>.Shared.Rent(integerContents.Length);
			BigInteger result;
			try
			{
				byte value = ((*integerContents.Span[0] & 128) == 0) ? 0 : byte.MaxValue;
				new Span<byte>(array, integerContents.Length, array.Length - integerContents.Length).Fill(value);
				integerContents.CopyTo(array);
				AsnWriter.Reverse(new Span<byte>(array, 0, integerContents.Length));
				result = new BigInteger(array);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			this._data = this._data.Slice(num + integerContents.Length);
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000183B8 File Offset: 0x000165B8
		private unsafe bool TryReadSignedInteger(int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out long value)
		{
			int num;
			ReadOnlyMemory<byte> integerContents = this.GetIntegerContents(expectedTag, tagNumber, out num);
			if (integerContents.Length > sizeLimit)
			{
				value = 0L;
				return false;
			}
			ReadOnlySpan<byte> span = integerContents.Span;
			long num2 = ((*span[0] & 128) > 0) ? -1L : 0L;
			for (int i = 0; i < integerContents.Length; i++)
			{
				num2 <<= 8;
				num2 |= (long)((ulong)(*span[i]));
			}
			this._data = this._data.Slice(num + integerContents.Length);
			value = num2;
			return true;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001844C File Offset: 0x0001664C
		private unsafe bool TryReadUnsignedInteger(int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out ulong value)
		{
			int num;
			ReadOnlyMemory<byte> integerContents = this.GetIntegerContents(expectedTag, tagNumber, out num);
			ReadOnlySpan<byte> readOnlySpan = integerContents.Span;
			int length = integerContents.Length;
			if ((*readOnlySpan[0] & 128) > 0)
			{
				value = 0UL;
				return false;
			}
			if (readOnlySpan.Length > 1 && *readOnlySpan[0] == 0)
			{
				readOnlySpan = readOnlySpan.Slice(1);
			}
			if (readOnlySpan.Length > sizeLimit)
			{
				value = 0UL;
				return false;
			}
			ulong num2 = 0UL;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				num2 <<= 8;
				num2 |= (ulong)(*readOnlySpan[i]);
			}
			this._data = this._data.Slice(num + length);
			value = num2;
			return true;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018508 File Offset: 0x00016708
		public bool TryReadInt32(out int value)
		{
			return this.TryReadInt32(Asn1Tag.Integer, out value);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018518 File Offset: 0x00016718
		public bool TryReadInt32(Asn1Tag expectedTag, out int value)
		{
			long num;
			if (this.TryReadSignedInteger(4, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (int)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001853C File Offset: 0x0001673C
		public bool TryReadUInt32(out uint value)
		{
			return this.TryReadUInt32(Asn1Tag.Integer, out value);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001854C File Offset: 0x0001674C
		public bool TryReadUInt32(Asn1Tag expectedTag, out uint value)
		{
			ulong num;
			if (this.TryReadUnsignedInteger(4, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (uint)num;
				return true;
			}
			value = 0U;
			return false;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018570 File Offset: 0x00016770
		public bool TryReadInt64(out long value)
		{
			return this.TryReadInt64(Asn1Tag.Integer, out value);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001857E File Offset: 0x0001677E
		public bool TryReadInt64(Asn1Tag expectedTag, out long value)
		{
			return this.TryReadSignedInteger(8, expectedTag, UniversalTagNumber.Integer, out value);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001858A File Offset: 0x0001678A
		public bool TryReadUInt64(out ulong value)
		{
			return this.TryReadUInt64(Asn1Tag.Integer, out value);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00018598 File Offset: 0x00016798
		public bool TryReadUInt64(Asn1Tag expectedTag, out ulong value)
		{
			return this.TryReadUnsignedInteger(8, expectedTag, UniversalTagNumber.Integer, out value);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000185A4 File Offset: 0x000167A4
		public bool TryReadInt16(out short value)
		{
			return this.TryReadInt16(Asn1Tag.Integer, out value);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000185B4 File Offset: 0x000167B4
		public bool TryReadInt16(Asn1Tag expectedTag, out short value)
		{
			long num;
			if (this.TryReadSignedInteger(2, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (short)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000185D8 File Offset: 0x000167D8
		public bool TryReadUInt16(out ushort value)
		{
			return this.TryReadUInt16(Asn1Tag.Integer, out value);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000185E8 File Offset: 0x000167E8
		public bool TryReadUInt16(Asn1Tag expectedTag, out ushort value)
		{
			ulong num;
			if (this.TryReadUnsignedInteger(2, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (ushort)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001860C File Offset: 0x0001680C
		public bool TryReadInt8(out sbyte value)
		{
			return this.TryReadInt8(Asn1Tag.Integer, out value);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001861C File Offset: 0x0001681C
		public bool TryReadInt8(Asn1Tag expectedTag, out sbyte value)
		{
			long num;
			if (this.TryReadSignedInteger(1, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (sbyte)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00018640 File Offset: 0x00016840
		public bool TryReadUInt8(out byte value)
		{
			return this.TryReadUInt8(Asn1Tag.Integer, out value);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00018650 File Offset: 0x00016850
		public bool TryReadUInt8(Asn1Tag expectedTag, out byte value)
		{
			ulong num;
			if (this.TryReadUnsignedInteger(1, expectedTag, UniversalTagNumber.Integer, out num))
			{
				value = (byte)num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00018674 File Offset: 0x00016874
		private unsafe void ParsePrimitiveBitStringContents(ReadOnlyMemory<byte> source, out int unusedBitCount, out ReadOnlyMemory<byte> value, out byte normalizedLastByte)
		{
			if (this._ruleSet == AsnEncodingRules.CER && source.Length > 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (source.Length == 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> span = source.Span;
			unusedBitCount = (int)(*span[0]);
			if (unusedBitCount > 7)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (source.Length == 1)
			{
				if (unusedBitCount > 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				value = ReadOnlyMemory<byte>.Empty;
				normalizedLastByte = 0;
				return;
			}
			else
			{
				int num = -1 << unusedBitCount;
				byte b = *span[span.Length - 1];
				byte b2 = (byte)((int)b & num);
				if (b2 != b && (this._ruleSet == AsnEncodingRules.DER || this._ruleSet == AsnEncodingRules.CER))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				normalizedLastByte = b2;
				value = source.Slice(1);
				return;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00018758 File Offset: 0x00016958
		private unsafe static void CopyBitStringValue(ReadOnlyMemory<byte> value, byte normalizedLastByte, Span<byte> destination)
		{
			if (value.Length == 0)
			{
				return;
			}
			value.Span.CopyTo(destination);
			*destination[value.Length - 1] = normalizedLastByte;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00018794 File Offset: 0x00016994
		private int CountConstructedBitString(ReadOnlyMemory<byte> source, bool isIndefinite)
		{
			Span<byte> empty = Span<byte>.Empty;
			int num;
			int num2;
			return this.ProcessConstructedBitString(source, empty, null, isIndefinite, out num, out num2);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000187B8 File Offset: 0x000169B8
		private void CopyConstructedBitString(ReadOnlyMemory<byte> source, Span<byte> destination, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = this.ProcessConstructedBitString(source, destination, delegate(ReadOnlyMemory<byte> value, byte lastByte, Span<byte> dest)
			{
				AsnReader.CopyBitStringValue(value, lastByte, dest);
			}, isIndefinite, out unusedBitCount, out bytesRead);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000187F8 File Offset: 0x000169F8
		private int ProcessConstructedBitString(ReadOnlyMemory<byte> source, Span<byte> destination, AsnReader.BitStringCopyAction copyAction, bool isIndefinite, out int lastUnusedBitCount, out int bytesRead)
		{
			lastUnusedBitCount = 0;
			bytesRead = 0;
			int num = 1000;
			AsnReader asnReader = new AsnReader(source, this._ruleSet);
			Stack<ValueTuple<AsnReader, bool, int>> stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			for (;;)
			{
				if (asnReader.HasData)
				{
					int? length;
					int num3;
					asn1Tag = asnReader.ReadTagAndLength(out length, out num3);
					if (asn1Tag == Asn1Tag.PrimitiveBitString)
					{
						if (lastUnusedBitCount != 0)
						{
							break;
						}
						if (this._ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							goto Block_4;
						}
						ReadOnlyMemory<byte> source2 = AsnReader.Slice(asnReader._data, num3, new int?(length.Value));
						ReadOnlyMemory<byte> value;
						byte normalizedLastByte;
						this.ParsePrimitiveBitStringContents(source2, out lastUnusedBitCount, out value, out normalizedLastByte);
						int num4 = num3 + source2.Length;
						asnReader._data = asnReader._data.Slice(num4);
						bytesRead += num4;
						num2 += value.Length;
						num = source2.Length;
						if (copyAction != null)
						{
							copyAction(value, normalizedLastByte, destination2);
							destination2 = destination2.Slice(value.Length);
							continue;
						}
						continue;
					}
					else if (asn1Tag == Asn1Tag.EndOfContents && isIndefinite)
					{
						AsnReader.ValidateEndOfContents(asn1Tag, length, num3);
						bytesRead += num3;
						if (stack != null && stack.Count > 0)
						{
							ValueTuple<AsnReader, bool, int> valueTuple = stack.Pop();
							AsnReader item = valueTuple.Item1;
							bool item2 = valueTuple.Item2;
							int item3 = valueTuple.Item3;
							item._data = item._data.Slice(bytesRead);
							bytesRead += item3;
							isIndefinite = item2;
							asnReader = item;
							continue;
						}
					}
					else
					{
						if (!(asn1Tag == Asn1Tag.ConstructedBitString))
						{
							goto IL_1E7;
						}
						if (this._ruleSet == AsnEncodingRules.CER)
						{
							goto Block_10;
						}
						if (stack == null)
						{
							stack = new Stack<ValueTuple<AsnReader, bool, int>>();
						}
						stack.Push(new ValueTuple<AsnReader, bool, int>(asnReader, isIndefinite, bytesRead));
						asnReader = new AsnReader(AsnReader.Slice(asnReader._data, num3, length), this._ruleSet);
						bytesRead = num3;
						isIndefinite = (length == null);
						continue;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					goto Block_13;
				}
				if (stack != null && stack.Count > 0)
				{
					ValueTuple<AsnReader, bool, int> valueTuple2 = stack.Pop();
					AsnReader item4 = valueTuple2.Item1;
					bool item5 = valueTuple2.Item2;
					int item6 = valueTuple2.Item3;
					asnReader = item4;
					asnReader._data = asnReader._data.Slice(bytesRead);
					isIndefinite = item5;
					bytesRead += item6;
				}
				else
				{
					asnReader = null;
				}
				if (asnReader == null)
				{
					return num2;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
			Block_4:
			throw new CryptographicException("ASN1 corrupted data.");
			Block_10:
			throw new CryptographicException("ASN1 corrupted data.");
			IL_1E7:
			throw new CryptographicException("ASN1 corrupted data.");
			Block_13:
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00018A74 File Offset: 0x00016C74
		private bool TryCopyConstructedBitStringValue(ReadOnlyMemory<byte> source, Span<byte> dest, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			int num = this.CountConstructedBitString(source, isIndefinite);
			if (this._ruleSet == AsnEncodingRules.CER && num < 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (dest.Length < num)
			{
				unusedBitCount = 0;
				bytesRead = 0;
				bytesWritten = 0;
				return false;
			}
			this.CopyConstructedBitString(source, dest, isIndefinite, out unusedBitCount, out bytesRead, out bytesWritten);
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00018AD0 File Offset: 0x00016CD0
		private bool TryGetPrimitiveBitStringValue(Asn1Tag expectedTag, out Asn1Tag actualTag, out int? contentsLength, out int headerLength, out int unusedBitCount, out ReadOnlyMemory<byte> value, out byte normalizedLastByte)
		{
			actualTag = this.ReadTagAndLength(out contentsLength, out headerLength);
			AsnReader.CheckExpectedTag(actualTag, expectedTag, UniversalTagNumber.BitString);
			if (!actualTag.IsConstructed)
			{
				ReadOnlyMemory<byte> source = AsnReader.Slice(this._data, headerLength, new int?(contentsLength.Value));
				this.ParsePrimitiveBitStringContents(source, out unusedBitCount, out value, out normalizedLastByte);
				return true;
			}
			if (this._ruleSet == AsnEncodingRules.DER)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			unusedBitCount = 0;
			value = default(ReadOnlyMemory<byte>);
			normalizedLastByte = 0;
			return false;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00018B4F File Offset: 0x00016D4F
		public bool TryGetPrimitiveBitStringValue(out int unusedBitCount, out ReadOnlyMemory<byte> contents)
		{
			return this.TryGetPrimitiveBitStringValue(Asn1Tag.PrimitiveBitString, out unusedBitCount, out contents);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00018B60 File Offset: 0x00016D60
		public unsafe bool TryGetPrimitiveBitStringValue(Asn1Tag expectedTag, out int unusedBitCount, out ReadOnlyMemory<byte> value)
		{
			Asn1Tag asn1Tag;
			int? num;
			int num2;
			byte b;
			bool flag = this.TryGetPrimitiveBitStringValue(expectedTag, out asn1Tag, out num, out num2, out unusedBitCount, out value, out b);
			if (flag)
			{
				if (value.Length != 0 && b != *value.Span[value.Length - 1])
				{
					unusedBitCount = 0;
					value = default(ReadOnlyMemory<byte>);
					return false;
				}
				this._data = this._data.Slice(num2 + value.Length + 1);
			}
			return flag;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018BCE File Offset: 0x00016DCE
		public bool TryCopyBitStringBytes(Span<byte> destination, out int unusedBitCount, out int bytesWritten)
		{
			return this.TryCopyBitStringBytes(Asn1Tag.PrimitiveBitString, destination, out unusedBitCount, out bytesWritten);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00018BE0 File Offset: 0x00016DE0
		public bool TryCopyBitStringBytes(Asn1Tag expectedTag, Span<byte> destination, out int unusedBitCount, out int bytesWritten)
		{
			Asn1Tag asn1Tag;
			int? length;
			int num;
			ReadOnlyMemory<byte> value;
			byte normalizedLastByte;
			if (!this.TryGetPrimitiveBitStringValue(expectedTag, out asn1Tag, out length, out num, out unusedBitCount, out value, out normalizedLastByte))
			{
				int num2;
				bool flag = this.TryCopyConstructedBitStringValue(AsnReader.Slice(this._data, num, length), destination, length == null, out unusedBitCount, out num2, out bytesWritten);
				if (flag)
				{
					this._data = this._data.Slice(num + num2);
				}
				return flag;
			}
			if (value.Length > destination.Length)
			{
				bytesWritten = 0;
				unusedBitCount = 0;
				return false;
			}
			AsnReader.CopyBitStringValue(value, normalizedLastByte, destination);
			bytesWritten = value.Length;
			this._data = this._data.Slice(num + value.Length + 1);
			return true;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018C86 File Offset: 0x00016E86
		public TFlagsEnum GetNamedBitListValue<TFlagsEnum>() where TFlagsEnum : struct
		{
			return this.GetNamedBitListValue<TFlagsEnum>(Asn1Tag.PrimitiveBitString);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00018C94 File Offset: 0x00016E94
		public TFlagsEnum GetNamedBitListValue<TFlagsEnum>(Asn1Tag expectedTag) where TFlagsEnum : struct
		{
			Type typeFromHandle = typeof(TFlagsEnum);
			return (TFlagsEnum)((object)Enum.ToObject(typeFromHandle, this.GetNamedBitListValue(expectedTag, typeFromHandle)));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00018CBF File Offset: 0x00016EBF
		public Enum GetNamedBitListValue(Type tFlagsEnum)
		{
			return this.GetNamedBitListValue(Asn1Tag.PrimitiveBitString, tFlagsEnum);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public unsafe Enum GetNamedBitListValue(Asn1Tag expectedTag, Type tFlagsEnum)
		{
			Type enumUnderlyingType = tFlagsEnum.GetEnumUnderlyingType();
			if (!tFlagsEnum.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("Named bit list operations require an enum with the [Flags] attribute.", "tFlagsEnum");
			}
			int num = Marshal.SizeOf(enumUnderlyingType);
			Span<byte> destination = new Span<byte>(stackalloc byte[(UIntPtr)num], num);
			ReadOnlyMemory<byte> data = this._data;
			Enum result;
			try
			{
				int num2;
				int num3;
				if (!this.TryCopyBitStringBytes(expectedTag, destination, out num2, out num3))
				{
					throw new CryptographicException(SR.Format("The encoded named bit list value is larger than the value size of the '{0}' enum.", tFlagsEnum.Name));
				}
				if (num3 == 0)
				{
					result = (Enum)Enum.ToObject(tFlagsEnum, 0);
				}
				else
				{
					ReadOnlySpan<byte> valueSpan = destination.Slice(0, num3);
					if (this._ruleSet == AsnEncodingRules.DER || this._ruleSet == AsnEncodingRules.CER)
					{
						bool flag = *valueSpan[num3 - 1] != 0;
						byte b = (byte)(1 << num2);
						if (((flag ? 1 : 0) & b) == 0)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
					}
					result = (Enum)Enum.ToObject(tFlagsEnum, AsnReader.InterpretNamedBitListReversed(valueSpan));
				}
			}
			catch
			{
				this._data = data;
				throw;
			}
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00018DD0 File Offset: 0x00016FD0
		private unsafe static long InterpretNamedBitListReversed(ReadOnlySpan<byte> valueSpan)
		{
			long num = 0L;
			long num2 = 1L;
			for (int i = 0; i < valueSpan.Length; i++)
			{
				byte b = *valueSpan[i];
				for (int j = 7; j >= 0; j--)
				{
					int num3 = 1 << j;
					if (((int)b & num3) != 0)
					{
						num |= num2;
					}
					num2 <<= 1;
				}
			}
			return num;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00018E27 File Offset: 0x00017027
		public ReadOnlyMemory<byte> GetEnumeratedBytes()
		{
			return this.GetEnumeratedBytes(Asn1Tag.Enumerated);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00018E34 File Offset: 0x00017034
		public ReadOnlyMemory<byte> GetEnumeratedBytes(Asn1Tag expectedTag)
		{
			int num;
			ReadOnlyMemory<byte> integerContents = this.GetIntegerContents(expectedTag, UniversalTagNumber.Enumerated, out num);
			this._data = this._data.Slice(num + integerContents.Length);
			return integerContents;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018E68 File Offset: 0x00017068
		public TEnum GetEnumeratedValue<TEnum>() where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			return (TEnum)((object)Enum.ToObject(typeFromHandle, this.GetEnumeratedValue(typeFromHandle)));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00018E94 File Offset: 0x00017094
		public TEnum GetEnumeratedValue<TEnum>(Asn1Tag expectedTag) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			return (TEnum)((object)Enum.ToObject(typeFromHandle, this.GetEnumeratedValue(expectedTag, typeFromHandle)));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00018EBF File Offset: 0x000170BF
		public Enum GetEnumeratedValue(Type tEnum)
		{
			return this.GetEnumeratedValue(Asn1Tag.Enumerated, tEnum);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00018ED0 File Offset: 0x000170D0
		public Enum GetEnumeratedValue(Asn1Tag expectedTag, Type tEnum)
		{
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (tEnum.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.", "tEnum");
			}
			int sizeLimit = Marshal.SizeOf(enumUnderlyingType);
			if (enumUnderlyingType == typeof(int) || enumUnderlyingType == typeof(long) || enumUnderlyingType == typeof(short) || enumUnderlyingType == typeof(sbyte))
			{
				long value;
				if (!this.TryReadSignedInteger(sizeLimit, expectedTag, UniversalTagNumber.Enumerated, out value))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (Enum)Enum.ToObject(tEnum, value);
			}
			else
			{
				if (!(enumUnderlyingType == typeof(uint)) && !(enumUnderlyingType == typeof(ulong)) && !(enumUnderlyingType == typeof(ushort)) && !(enumUnderlyingType == typeof(byte)))
				{
					throw new CryptographicException();
				}
				ulong value2;
				if (!this.TryReadUnsignedInteger(sizeLimit, expectedTag, UniversalTagNumber.Enumerated, out value2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (Enum)Enum.ToObject(tEnum, value2);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00018FF0 File Offset: 0x000171F0
		private bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, out Asn1Tag actualTag, out int? contentLength, out int headerLength, out ReadOnlyMemory<byte> contents, UniversalTagNumber universalTagNumber = UniversalTagNumber.OctetString)
		{
			actualTag = this.ReadTagAndLength(out contentLength, out headerLength);
			AsnReader.CheckExpectedTag(actualTag, expectedTag, universalTagNumber);
			if (actualTag.IsConstructed)
			{
				if (this._ruleSet == AsnEncodingRules.DER)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				contents = default(ReadOnlyMemory<byte>);
				return false;
			}
			else
			{
				ReadOnlyMemory<byte> readOnlyMemory = AsnReader.Slice(this._data, headerLength, new int?(contentLength.Value));
				if (this._ruleSet == AsnEncodingRules.CER && readOnlyMemory.Length > 1000)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				contents = readOnlyMemory;
				return true;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019088 File Offset: 0x00017288
		private bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out ReadOnlyMemory<byte> contents)
		{
			Asn1Tag asn1Tag;
			int? num;
			int num2;
			if (this.TryGetPrimitiveOctetStringBytes(expectedTag, out asn1Tag, out num, out num2, out contents, universalTagNumber))
			{
				this._data = this._data.Slice(num2 + contents.Length);
				return true;
			}
			return false;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000190C2 File Offset: 0x000172C2
		public bool TryGetPrimitiveOctetStringBytes(out ReadOnlyMemory<byte> contents)
		{
			return this.TryGetPrimitiveOctetStringBytes(Asn1Tag.PrimitiveOctetString, out contents);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000190D0 File Offset: 0x000172D0
		public bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, out ReadOnlyMemory<byte> contents)
		{
			return this.TryGetPrimitiveOctetStringBytes(expectedTag, UniversalTagNumber.OctetString, out contents);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000190DC File Offset: 0x000172DC
		private int CountConstructedOctetString(ReadOnlyMemory<byte> source, bool isIndefinite)
		{
			int num2;
			int num = this.CopyConstructedOctetString(source, Span<byte>.Empty, false, isIndefinite, out num2);
			if (this._ruleSet == AsnEncodingRules.CER && num <= 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return num;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00019117 File Offset: 0x00017317
		private void CopyConstructedOctetString(ReadOnlyMemory<byte> source, Span<byte> destination, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = this.CopyConstructedOctetString(source, destination, true, isIndefinite, out bytesRead);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00019128 File Offset: 0x00017328
		private int CopyConstructedOctetString(ReadOnlyMemory<byte> source, Span<byte> destination, bool write, bool isIndefinite, out int bytesRead)
		{
			bytesRead = 0;
			int num = 1000;
			AsnReader asnReader = new AsnReader(source, this._ruleSet);
			Stack<ValueTuple<AsnReader, bool, int>> stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			for (;;)
			{
				if (asnReader.HasData)
				{
					int? length;
					int num3;
					asn1Tag = asnReader.ReadTagAndLength(out length, out num3);
					if (asn1Tag == Asn1Tag.PrimitiveOctetString)
					{
						if (this._ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							break;
						}
						ReadOnlyMemory<byte> readOnlyMemory = AsnReader.Slice(asnReader._data, num3, new int?(length.Value));
						int num4 = num3 + readOnlyMemory.Length;
						asnReader._data = asnReader._data.Slice(num4);
						bytesRead += num4;
						num2 += readOnlyMemory.Length;
						num = readOnlyMemory.Length;
						if (this._ruleSet == AsnEncodingRules.CER && num > 1000)
						{
							goto Block_5;
						}
						if (write)
						{
							readOnlyMemory.Span.CopyTo(destination2);
							destination2 = destination2.Slice(readOnlyMemory.Length);
							continue;
						}
						continue;
					}
					else if (asn1Tag == Asn1Tag.EndOfContents && isIndefinite)
					{
						AsnReader.ValidateEndOfContents(asn1Tag, length, num3);
						bytesRead += num3;
						if (stack != null && stack.Count > 0)
						{
							ValueTuple<AsnReader, bool, int> valueTuple = stack.Pop();
							AsnReader item = valueTuple.Item1;
							bool item2 = valueTuple.Item2;
							int item3 = valueTuple.Item3;
							item._data = item._data.Slice(bytesRead);
							bytesRead += item3;
							isIndefinite = item2;
							asnReader = item;
							continue;
						}
					}
					else
					{
						if (!(asn1Tag == Asn1Tag.ConstructedOctetString))
						{
							goto IL_1E7;
						}
						if (this._ruleSet == AsnEncodingRules.CER)
						{
							goto Block_11;
						}
						if (stack == null)
						{
							stack = new Stack<ValueTuple<AsnReader, bool, int>>();
						}
						stack.Push(new ValueTuple<AsnReader, bool, int>(asnReader, isIndefinite, bytesRead));
						asnReader = new AsnReader(AsnReader.Slice(asnReader._data, num3, length), this._ruleSet);
						bytesRead = num3;
						isIndefinite = (length == null);
						continue;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					goto Block_14;
				}
				if (stack != null && stack.Count > 0)
				{
					ValueTuple<AsnReader, bool, int> valueTuple2 = stack.Pop();
					AsnReader item4 = valueTuple2.Item1;
					bool item5 = valueTuple2.Item2;
					int item6 = valueTuple2.Item3;
					asnReader = item4;
					asnReader._data = asnReader._data.Slice(bytesRead);
					isIndefinite = item5;
					bytesRead += item6;
				}
				else
				{
					asnReader = null;
				}
				if (asnReader == null)
				{
					return num2;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
			Block_5:
			throw new CryptographicException("ASN1 corrupted data.");
			Block_11:
			throw new CryptographicException("ASN1 corrupted data.");
			IL_1E7:
			throw new CryptographicException("ASN1 corrupted data.");
			Block_14:
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000193A4 File Offset: 0x000175A4
		private bool TryCopyConstructedOctetStringContents(ReadOnlyMemory<byte> source, Span<byte> dest, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesRead = 0;
			int num = this.CountConstructedOctetString(source, isIndefinite);
			if (dest.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			this.CopyConstructedOctetString(source, dest, isIndefinite, out bytesRead, out bytesWritten);
			return true;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000193DC File Offset: 0x000175DC
		public bool TryCopyOctetStringBytes(Span<byte> destination, out int bytesWritten)
		{
			return this.TryCopyOctetStringBytes(Asn1Tag.PrimitiveOctetString, destination, out bytesWritten);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000193EC File Offset: 0x000175EC
		public bool TryCopyOctetStringBytes(Asn1Tag expectedTag, Span<byte> destination, out int bytesWritten)
		{
			Asn1Tag asn1Tag;
			int? length;
			int num;
			ReadOnlyMemory<byte> readOnlyMemory;
			if (!this.TryGetPrimitiveOctetStringBytes(expectedTag, out asn1Tag, out length, out num, out readOnlyMemory, UniversalTagNumber.OctetString))
			{
				int num2;
				bool flag = this.TryCopyConstructedOctetStringContents(AsnReader.Slice(this._data, num, length), destination, length == null, out num2, out bytesWritten);
				if (flag)
				{
					this._data = this._data.Slice(num + num2);
				}
				return flag;
			}
			if (readOnlyMemory.Length > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			readOnlyMemory.Span.CopyTo(destination);
			bytesWritten = readOnlyMemory.Length;
			this._data = this._data.Slice(num + readOnlyMemory.Length);
			return true;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001948F File Offset: 0x0001768F
		public void ReadNull()
		{
			this.ReadNull(Asn1Tag.Null);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001949C File Offset: 0x0001769C
		public void ReadNull(Asn1Tag expectedTag)
		{
			int? num;
			int start;
			Asn1Tag tag = this.ReadTagAndLength(out num, out start);
			AsnReader.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Null);
			if (!tag.IsConstructed)
			{
				int? num2 = num;
				int num3 = 0;
				if (num2.GetValueOrDefault() == num3 & num2 != null)
				{
					this._data = this._data.Slice(start);
					return;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000194FC File Offset: 0x000176FC
		private unsafe static void ReadSubIdentifier(ReadOnlySpan<byte> source, out int bytesRead, out long? smallValue, out BigInteger? largeValue)
		{
			if (*source[0] == 128)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = -1;
			int i;
			for (i = 0; i < source.Length; i++)
			{
				if ((*source[i] & 128) == 0)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bytesRead = num + 1;
			long num2 = 0L;
			if (bytesRead <= 9)
			{
				for (i = 0; i < bytesRead; i++)
				{
					byte b = *source[i];
					num2 <<= 7;
					num2 |= (long)((ulong)(b & 127));
				}
				largeValue = null;
				smallValue = new long?(num2);
				return;
			}
			int minimumLength = (bytesRead / 8 + 1) * 7;
			byte[] array = ArrayPool<byte>.Shared.Rent(minimumLength);
			Array.Clear(array, 0, array.Length);
			Span<byte> destination = array;
			Span<byte> destination2 = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			int j = bytesRead;
			i = bytesRead - 8;
			while (j > 0)
			{
				byte b2 = *source[i];
				num2 <<= 7;
				num2 |= (long)((ulong)(b2 & 127));
				i++;
				if (i >= j)
				{
					BinaryPrimitives.WriteInt64LittleEndian(destination2, num2);
					destination2.Slice(0, 7).CopyTo(destination);
					destination = destination.Slice(7);
					num2 = 0L;
					j -= 8;
					i = Math.Max(0, j - 8);
				}
			}
			int length = array.Length - destination.Length;
			largeValue = new BigInteger?(new BigInteger(array));
			smallValue = null;
			Array.Clear(array, 0, length);
			ArrayPool<byte>.Shared.Return(array, false);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00019688 File Offset: 0x00017888
		private string ReadObjectIdentifierAsString(Asn1Tag expectedTag, out int totalBytesRead)
		{
			int? num;
			int num2;
			Asn1Tag tag = this.ReadTagAndLength(out num, out num2);
			AsnReader.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.ObjectIdentifier);
			if (!tag.IsConstructed)
			{
				int? num3 = num;
				int num4 = 1;
				if (!(num3.GetValueOrDefault() < num4 & num3 != null))
				{
					ReadOnlySpan<byte> source = AsnReader.Slice(this._data, num2, new int?(num.Value)).Span;
					StringBuilder stringBuilder = new StringBuilder((int)((byte)source.Length * 4));
					int start;
					long? num5;
					BigInteger? bigInteger;
					AsnReader.ReadSubIdentifier(source, out start, out num5, out bigInteger);
					if (num5 != null)
					{
						long num6 = num5.Value;
						byte value;
						if (num6 < 40L)
						{
							value = 0;
						}
						else if (num6 < 80L)
						{
							value = 1;
							num6 -= 40L;
						}
						else
						{
							value = 2;
							num6 -= 80L;
						}
						stringBuilder.Append(value);
						stringBuilder.Append('.');
						stringBuilder.Append(num6);
					}
					else
					{
						BigInteger left = bigInteger.Value;
						byte value = 2;
						left -= 80;
						stringBuilder.Append(value);
						stringBuilder.Append('.');
						stringBuilder.Append(left.ToString());
					}
					source = source.Slice(start);
					while (!source.IsEmpty)
					{
						AsnReader.ReadSubIdentifier(source, out start, out num5, out bigInteger);
						stringBuilder.Append('.');
						if (num5 != null)
						{
							stringBuilder.Append(num5.Value);
						}
						else
						{
							stringBuilder.Append(bigInteger.Value.ToString());
						}
						source = source.Slice(start);
					}
					totalBytesRead = num2 + num.Value;
					return stringBuilder.ToString();
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001983B File Offset: 0x00017A3B
		public string ReadObjectIdentifierAsString()
		{
			return this.ReadObjectIdentifierAsString(Asn1Tag.ObjectIdentifier);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019848 File Offset: 0x00017A48
		public string ReadObjectIdentifierAsString(Asn1Tag expectedTag)
		{
			int start;
			string result = this.ReadObjectIdentifierAsString(expectedTag, out start);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019870 File Offset: 0x00017A70
		public Oid ReadObjectIdentifier(bool skipFriendlyName = false)
		{
			return this.ReadObjectIdentifier(Asn1Tag.ObjectIdentifier, skipFriendlyName);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00019880 File Offset: 0x00017A80
		public Oid ReadObjectIdentifier(Asn1Tag expectedTag, bool skipFriendlyName = false)
		{
			int start;
			string text = this.ReadObjectIdentifierAsString(expectedTag, out start);
			Oid result = skipFriendlyName ? new Oid(text, text) : new Oid(text);
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000198BC File Offset: 0x00017ABC
		private bool TryCopyCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Span<byte> destination, out int bytesRead, out int bytesWritten)
		{
			Asn1Tag asn1Tag;
			int? length;
			int num;
			ReadOnlyMemory<byte> readOnlyMemory;
			if (this.TryGetPrimitiveOctetStringBytes(expectedTag, out asn1Tag, out length, out num, out readOnlyMemory, universalTagNumber))
			{
				bytesWritten = readOnlyMemory.Length;
				if (destination.Length < bytesWritten)
				{
					bytesWritten = 0;
					bytesRead = 0;
					return false;
				}
				readOnlyMemory.Span.CopyTo(destination);
				bytesRead = num + bytesWritten;
				return true;
			}
			else
			{
				int num2;
				bool flag = this.TryCopyConstructedOctetStringContents(AsnReader.Slice(this._data, num, length), destination, length == null, out num2, out bytesWritten);
				if (flag)
				{
					bytesRead = num + num2;
					return flag;
				}
				bytesRead = 0;
				return flag;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00019948 File Offset: 0x00017B48
		private unsafe static bool TryCopyCharacterString(ReadOnlySpan<byte> source, Span<char> destination, Encoding encoding, out int charsWritten)
		{
			if (source.Length == 0)
			{
				charsWritten = 0;
				return true;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* bytes = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(destination))
				{
					char* chars = reference2;
					try
					{
						if (encoding.GetCharCount(bytes, source.Length) > destination.Length)
						{
							charsWritten = 0;
							return false;
						}
						charsWritten = encoding.GetChars(bytes, source.Length, chars, destination.Length);
					}
					catch (DecoderFallbackException inner)
					{
						throw new CryptographicException("ASN1 corrupted data.", inner);
					}
					return true;
				}
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000199D4 File Offset: 0x00017BD4
		private unsafe string GetCharacterString(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Encoding encoding)
		{
			byte[] array = null;
			int start;
			ReadOnlySpan<byte> octetStringContents = this.GetOctetStringContents(expectedTag, universalTagNumber, out start, ref array, default(Span<byte>));
			string result;
			try
			{
				string text;
				if (octetStringContents.Length == 0)
				{
					text = string.Empty;
				}
				else
				{
					try
					{
						fixed (byte* ptr = MemoryMarshal.GetReference<byte>(octetStringContents))
						{
							byte* bytes = ptr;
							try
							{
								text = encoding.GetString(bytes, octetStringContents.Length);
							}
							catch (DecoderFallbackException inner)
							{
								throw new CryptographicException("ASN1 corrupted data.", inner);
							}
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				this._data = this._data.Slice(start);
				result = text;
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, octetStringContents.Length);
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00019AA0 File Offset: 0x00017CA0
		private bool TryCopyCharacterString(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Encoding encoding, Span<char> destination, out int charsWritten)
		{
			byte[] array = null;
			int start;
			ReadOnlySpan<byte> octetStringContents = this.GetOctetStringContents(expectedTag, universalTagNumber, out start, ref array, default(Span<byte>));
			bool result;
			try
			{
				bool flag = AsnReader.TryCopyCharacterString(octetStringContents, destination, encoding, out charsWritten);
				if (flag)
				{
					this._data = this._data.Slice(start);
				}
				result = flag;
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, octetStringContents.Length);
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return result;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00019B1C File Offset: 0x00017D1C
		public bool TryGetPrimitiveCharacterStringBytes(UniversalTagNumber encodingType, out ReadOnlyMemory<byte> contents)
		{
			return this.TryGetPrimitiveCharacterStringBytes(new Asn1Tag(encodingType, false), encodingType, out contents);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00019B2D File Offset: 0x00017D2D
		public bool TryGetPrimitiveCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber encodingType, out ReadOnlyMemory<byte> contents)
		{
			AsnReader.CheckCharacterStringEncodingType(encodingType);
			return this.TryGetPrimitiveOctetStringBytes(expectedTag, encodingType, out contents);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00019B3E File Offset: 0x00017D3E
		public bool TryCopyCharacterStringBytes(UniversalTagNumber encodingType, Span<byte> destination, out int bytesWritten)
		{
			return this.TryCopyCharacterStringBytes(new Asn1Tag(encodingType, false), encodingType, destination, out bytesWritten);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00019B50 File Offset: 0x00017D50
		public bool TryCopyCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber encodingType, Span<byte> destination, out int bytesWritten)
		{
			AsnReader.CheckCharacterStringEncodingType(encodingType);
			int start;
			bool flag = this.TryCopyCharacterStringBytes(expectedTag, encodingType, destination, out start, out bytesWritten);
			if (flag)
			{
				this._data = this._data.Slice(start);
			}
			return flag;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00019B85 File Offset: 0x00017D85
		public bool TryCopyCharacterString(UniversalTagNumber encodingType, Span<char> destination, out int charsWritten)
		{
			return this.TryCopyCharacterString(new Asn1Tag(encodingType, false), encodingType, destination, out charsWritten);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019B98 File Offset: 0x00017D98
		public bool TryCopyCharacterString(Asn1Tag expectedTag, UniversalTagNumber encodingType, Span<char> destination, out int charsWritten)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			return this.TryCopyCharacterString(expectedTag, encodingType, encoding, destination, out charsWritten);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019BB8 File Offset: 0x00017DB8
		public string GetCharacterString(UniversalTagNumber encodingType)
		{
			return this.GetCharacterString(new Asn1Tag(encodingType, false), encodingType);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019BC8 File Offset: 0x00017DC8
		public string GetCharacterString(Asn1Tag expectedTag, UniversalTagNumber encodingType)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			return this.GetCharacterString(expectedTag, encodingType, encoding);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00019BE5 File Offset: 0x00017DE5
		public AsnReader ReadSequence()
		{
			return this.ReadSequence(Asn1Tag.Sequence);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00019BF4 File Offset: 0x00017DF4
		public AsnReader ReadSequence(Asn1Tag expectedTag)
		{
			int? num;
			int num2;
			Asn1Tag tag = this.ReadTagAndLength(out num, out num2);
			AsnReader.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Sequence);
			if (!tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num3 = 0;
			if (num == null)
			{
				num = new int?(this.SeekEndOfContents(this._data.Slice(num2)));
				num3 = 2;
			}
			ReadOnlyMemory<byte> data = AsnReader.Slice(this._data, num2, new int?(num.Value));
			this._data = this._data.Slice(num2 + data.Length + num3);
			return new AsnReader(data, this._ruleSet);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00019C92 File Offset: 0x00017E92
		public AsnReader ReadSetOf(bool skipSortOrderValidation = false)
		{
			return this.ReadSetOf(Asn1Tag.SetOf, skipSortOrderValidation);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019CA0 File Offset: 0x00017EA0
		public AsnReader ReadSetOf(Asn1Tag expectedTag, bool skipSortOrderValidation = false)
		{
			int? num;
			int num2;
			Asn1Tag tag = this.ReadTagAndLength(out num, out num2);
			AsnReader.CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Set);
			if (!tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num3 = 0;
			if (num == null)
			{
				num = new int?(this.SeekEndOfContents(this._data.Slice(num2)));
				num3 = 2;
			}
			ReadOnlyMemory<byte> data = AsnReader.Slice(this._data, num2, new int?(num.Value));
			if (!skipSortOrderValidation && (this._ruleSet == AsnEncodingRules.DER || this._ruleSet == AsnEncodingRules.CER))
			{
				AsnReader asnReader = new AsnReader(data, this._ruleSet);
				ReadOnlyMemory<byte> readOnlyMemory = ReadOnlyMemory<byte>.Empty;
				SetOfValueComparer instance = SetOfValueComparer.Instance;
				while (asnReader.HasData)
				{
					ReadOnlyMemory<byte> y = readOnlyMemory;
					readOnlyMemory = asnReader.GetEncodedValue();
					if (instance.Compare(readOnlyMemory, y) < 0)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
			}
			this._data = this._data.Slice(num2 + data.Length + num3);
			return new AsnReader(data, this._ruleSet);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019DA1 File Offset: 0x00017FA1
		private static int ParseNonNegativeIntAndSlice(ref ReadOnlySpan<byte> data, int bytesToRead)
		{
			int result = AsnReader.ParseNonNegativeInt(AsnReader.Slice(data, 0, bytesToRead));
			data = data.Slice(bytesToRead);
			return result;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019DC4 File Offset: 0x00017FC4
		private static int ParseNonNegativeInt(ReadOnlySpan<byte> data)
		{
			uint num;
			int num2;
			if (Utf8Parser.TryParse(data, out num, out num2, '\0') && num <= 2147483647U && num2 == data.Length)
			{
				return (int)num;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019DFC File Offset: 0x00017FFC
		private unsafe DateTimeOffset ParseUtcTime(ReadOnlySpan<byte> contentOctets, int twoDigitYearMax)
		{
			if ((this._ruleSet == AsnEncodingRules.DER || this._ruleSet == AsnEncodingRules.CER) && contentOctets.Length != 13)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (contentOctets.Length < 11 || contentOctets.Length > 17 || (contentOctets.Length & 1) != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> readOnlySpan = contentOctets;
			int num = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			int month = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			int day = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			int hour = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			int minute = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			int second = 0;
			int hours = 0;
			int num2 = 0;
			bool flag = false;
			if (contentOctets.Length == 17 || contentOctets.Length == 13)
			{
				second = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			}
			if (contentOctets.Length == 11 || contentOctets.Length == 13)
			{
				if (*readOnlySpan[0] != 90)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			else
			{
				if (*readOnlySpan[0] == 45)
				{
					flag = true;
				}
				else if (*readOnlySpan[0] != 43)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				readOnlySpan = readOnlySpan.Slice(1);
				hours = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
				num2 = AsnReader.ParseNonNegativeIntAndSlice(ref readOnlySpan, 2);
			}
			if (num2 > 59)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			TimeSpan timeSpan = new TimeSpan(hours, num2, 0);
			if (flag)
			{
				timeSpan = -timeSpan;
			}
			int num3 = twoDigitYearMax / 100;
			if (num > twoDigitYearMax % 100)
			{
				num3--;
			}
			int year = num3 * 100 + num;
			DateTimeOffset result;
			try
			{
				result = new DateTimeOffset(year, month, day, hour, minute, second, timeSpan);
			}
			catch (Exception inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00019FAC File Offset: 0x000181AC
		public DateTimeOffset GetUtcTime(int twoDigitYearMax = 2049)
		{
			return this.GetUtcTime(Asn1Tag.UtcTime, twoDigitYearMax);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00019FBC File Offset: 0x000181BC
		public unsafe DateTimeOffset GetUtcTime(Asn1Tag expectedTag, int twoDigitYearMax = 2049)
		{
			byte[] array = null;
			Span<byte> tmpSpace = new Span<byte>(stackalloc byte[(UIntPtr)17], 17);
			int start;
			ReadOnlySpan<byte> octetStringContents = this.GetOctetStringContents(expectedTag, UniversalTagNumber.UtcTime, out start, ref array, tmpSpace);
			DateTimeOffset result = this.ParseUtcTime(octetStringContents, twoDigitYearMax);
			if (array != null)
			{
				Array.Clear(array, 0, octetStringContents.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A020 File Offset: 0x00018220
		private static byte? ParseGeneralizedTime_GetNextState(byte octet)
		{
			if (octet == 90 || octet == 45 || octet == 43)
			{
				return new byte?(2);
			}
			if (octet == 46 || octet == 44)
			{
				return new byte?(1);
			}
			return null;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001A060 File Offset: 0x00018260
		private unsafe static DateTimeOffset ParseGeneralizedTime(AsnEncodingRules ruleSet, ReadOnlySpan<byte> contentOctets, bool disallowFractions)
		{
			bool flag = ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER;
			if (flag && contentOctets.Length < 15)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (contentOctets.Length < 10)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> source = contentOctets;
			int year = AsnReader.ParseNonNegativeIntAndSlice(ref source, 4);
			int month = AsnReader.ParseNonNegativeIntAndSlice(ref source, 2);
			int day = AsnReader.ParseNonNegativeIntAndSlice(ref source, 2);
			int hour = AsnReader.ParseNonNegativeIntAndSlice(ref source, 2);
			int? num = null;
			int? num2 = null;
			ulong num3 = 0UL;
			ulong num4 = 1UL;
			byte b = byte.MaxValue;
			TimeSpan? timeSpan = null;
			bool flag2 = false;
			byte b2 = 0;
			while (b2 == 0 && source.Length != 0)
			{
				byte? b3 = AsnReader.ParseGeneralizedTime_GetNextState(*source[0]);
				if (b3 == null)
				{
					if (num == null)
					{
						num = new int?(AsnReader.ParseNonNegativeIntAndSlice(ref source, 2));
					}
					else
					{
						if (num2 != null)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						num2 = new int?(AsnReader.ParseNonNegativeIntAndSlice(ref source, 2));
					}
				}
				else
				{
					b2 = b3.Value;
				}
			}
			if (b2 == 1)
			{
				if (disallowFractions)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				byte b4 = *source[0];
				if (b4 != 46)
				{
					if (b4 != 44)
					{
						throw new CryptographicException();
					}
					if (flag)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				source = source.Slice(1);
				if (source.IsEmpty)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				int num5;
				if (!Utf8Parser.TryParse(AsnReader.SliceAtMost(source, 12), out num3, out num5, '\0') || num5 == 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				b = (byte)(num3 % 10UL);
				for (int i = 0; i < num5; i++)
				{
					num4 *= 10UL;
				}
				source = source.Slice(num5);
				uint num6;
				while (Utf8Parser.TryParse(AsnReader.SliceAtMost(source, 9), out num6, out num5, '\0'))
				{
					source = source.Slice(num5);
					b = (byte)(num6 % 10U);
				}
				if (source.Length != 0)
				{
					byte? b5 = AsnReader.ParseGeneralizedTime_GetNextState(*source[0]);
					if (b5 == null)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					b2 = b5.Value;
				}
			}
			if (b2 == 2)
			{
				byte b6 = *source[0];
				source = source.Slice(1);
				if (b6 == 90)
				{
					timeSpan = new TimeSpan?(TimeSpan.Zero);
					flag2 = true;
				}
				else
				{
					bool flag3;
					if (b6 == 43)
					{
						flag3 = false;
					}
					else
					{
						if (b6 != 45)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						flag3 = true;
					}
					if (source.IsEmpty)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					int hours = AsnReader.ParseNonNegativeIntAndSlice(ref source, 2);
					int num7 = 0;
					if (source.Length != 0)
					{
						num7 = AsnReader.ParseNonNegativeIntAndSlice(ref source, 2);
					}
					if (num7 > 59)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					TimeSpan timeSpan2 = new TimeSpan(hours, num7, 0);
					if (flag3)
					{
						timeSpan2 = -timeSpan2;
					}
					timeSpan = new TimeSpan?(timeSpan2);
				}
			}
			if (!source.IsEmpty)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (flag)
			{
				if (!flag2 || num2 == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (b == 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			double num8 = num3 / num4;
			TimeSpan zero = TimeSpan.Zero;
			if (num == null)
			{
				num = new int?(0);
				num2 = new int?(0);
				if (num3 != 0UL)
				{
					zero = new TimeSpan((long)(num8 * 36000000000.0));
				}
			}
			else if (num2 == null)
			{
				num2 = new int?(0);
				if (num3 != 0UL)
				{
					zero = new TimeSpan((long)(num8 * 600000000.0));
				}
			}
			else if (num3 != 0UL)
			{
				zero = new TimeSpan((long)(num8 * 10000000.0));
			}
			DateTimeOffset result;
			try
			{
				DateTimeOffset dateTimeOffset;
				if (timeSpan == null)
				{
					dateTimeOffset = new DateTimeOffset(new DateTime(year, month, day, hour, num.Value, num2.Value));
				}
				else
				{
					dateTimeOffset = new DateTimeOffset(year, month, day, hour, num.Value, num2.Value, timeSpan.Value);
				}
				dateTimeOffset += zero;
				result = dateTimeOffset;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
			return result;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001A47C File Offset: 0x0001867C
		public DateTimeOffset GetGeneralizedTime(bool disallowFractions = false)
		{
			return this.GetGeneralizedTime(Asn1Tag.GeneralizedTime, disallowFractions);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001A48C File Offset: 0x0001868C
		public DateTimeOffset GetGeneralizedTime(Asn1Tag expectedTag, bool disallowFractions = false)
		{
			byte[] array = null;
			int start;
			ReadOnlySpan<byte> octetStringContents = this.GetOctetStringContents(expectedTag, UniversalTagNumber.GeneralizedTime, out start, ref array, default(Span<byte>));
			DateTimeOffset result = AsnReader.ParseGeneralizedTime(this._ruleSet, octetStringContents, disallowFractions);
			if (array != null)
			{
				Array.Clear(array, 0, octetStringContents.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			this._data = this._data.Slice(start);
			return result;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001A4F0 File Offset: 0x000186F0
		private ReadOnlySpan<byte> GetOctetStringContents(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out int bytesRead, ref byte[] rented, Span<byte> tmpSpace = default(Span<byte>))
		{
			Asn1Tag asn1Tag;
			int? length;
			int num;
			ReadOnlyMemory<byte> readOnlyMemory;
			if (this.TryGetPrimitiveOctetStringBytes(expectedTag, out asn1Tag, out length, out num, out readOnlyMemory, universalTagNumber))
			{
				bytesRead = num + readOnlyMemory.Length;
				return readOnlyMemory.Span;
			}
			ReadOnlyMemory<byte> source = AsnReader.Slice(this._data, num, length);
			bool isIndefinite = length == null;
			int num2 = this.CountConstructedOctetString(source, isIndefinite);
			if (tmpSpace.Length < num2)
			{
				rented = ArrayPool<byte>.Shared.Rent(num2);
				tmpSpace = rented;
			}
			int num3;
			int length2;
			this.CopyConstructedOctetString(source, tmpSpace, isIndefinite, out num3, out length2);
			bytesRead = num + num3;
			return tmpSpace.Slice(0, length2);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001A594 File Offset: 0x00018794
		private static ReadOnlySpan<byte> SliceAtMost(ReadOnlySpan<byte> source, int longestPermitted)
		{
			int length = Math.Min(longestPermitted, source.Length);
			return source.Slice(0, length);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001A5B8 File Offset: 0x000187B8
		private static ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> source, int offset, int length)
		{
			if (length < 0 || source.Length - offset < length)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return source.Slice(offset, length);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001A5E0 File Offset: 0x000187E0
		private static ReadOnlyMemory<byte> Slice(ReadOnlyMemory<byte> source, int offset, int? length)
		{
			if (length == null)
			{
				return source.Slice(offset);
			}
			int value = length.Value;
			if (value < 0 || source.Length - offset < value)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return source.Slice(offset, value);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001A62B File Offset: 0x0001882B
		private static void CheckEncodingRules(AsnEncodingRules ruleSet)
		{
			if (ruleSet != AsnEncodingRules.BER && ruleSet != AsnEncodingRules.CER && ruleSet != AsnEncodingRules.DER)
			{
				throw new ArgumentOutOfRangeException("ruleSet");
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A644 File Offset: 0x00018844
		private static void CheckExpectedTag(Asn1Tag tag, Asn1Tag expectedTag, UniversalTagNumber tagNumber)
		{
			if (expectedTag.TagClass == TagClass.Universal && expectedTag.TagValue != (int)tagNumber)
			{
				throw new ArgumentException("Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.", "expectedTag");
			}
			if (expectedTag.TagClass != tag.TagClass || expectedTag.TagValue != tag.TagValue)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001A6A0 File Offset: 0x000188A0
		private static void CheckCharacterStringEncodingType(UniversalTagNumber encodingType)
		{
			switch (encodingType)
			{
			case UniversalTagNumber.UTF8String:
			case UniversalTagNumber.NumericString:
			case UniversalTagNumber.PrintableString:
			case UniversalTagNumber.TeletexString:
			case UniversalTagNumber.VideotexString:
			case UniversalTagNumber.IA5String:
			case UniversalTagNumber.GraphicString:
			case UniversalTagNumber.VisibleString:
			case UniversalTagNumber.GeneralString:
			case UniversalTagNumber.UniversalString:
			case UniversalTagNumber.BMPString:
				return;
			}
			throw new ArgumentOutOfRangeException("encodingType");
		}

		// Token: 0x0400040E RID: 1038
		internal const int MaxCERSegmentSize = 1000;

		// Token: 0x0400040F RID: 1039
		private const int EndOfContentsEncodedLength = 2;

		// Token: 0x04000410 RID: 1040
		private ReadOnlyMemory<byte> _data;

		// Token: 0x04000411 RID: 1041
		private readonly AsnEncodingRules _ruleSet;

		// Token: 0x04000412 RID: 1042
		private const byte HmsState = 0;

		// Token: 0x04000413 RID: 1043
		private const byte FracState = 1;

		// Token: 0x04000414 RID: 1044
		private const byte SuffixState = 2;

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x06000660 RID: 1632
		private delegate void BitStringCopyAction(ReadOnlyMemory<byte> value, byte normalizedLastByte, Span<byte> destination);

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000663 RID: 1635 RVA: 0x0001A70F File Offset: 0x0001890F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00002145 File Offset: 0x00000345
			public <>c()
			{
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0001A71B File Offset: 0x0001891B
			internal void <CopyConstructedBitString>b__47_0(ReadOnlyMemory<byte> value, byte lastByte, Span<byte> dest)
			{
				AsnReader.CopyBitStringValue(value, lastByte, dest);
			}

			// Token: 0x04000415 RID: 1045
			public static readonly AsnReader.<>c <>9 = new AsnReader.<>c();

			// Token: 0x04000416 RID: 1046
			public static AsnReader.BitStringCopyAction <>9__47_0;
		}
	}
}
