using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F8 RID: 248
	internal struct Asn1Tag : IEquatable<Asn1Tag>
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001738E File Offset: 0x0001558E
		public TagClass TagClass
		{
			get
			{
				return (TagClass)(this._controlFlags & 192);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001739D File Offset: 0x0001559D
		public bool IsConstructed
		{
			get
			{
				return (this._controlFlags & 32) > 0;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000173AB File Offset: 0x000155AB
		public int TagValue
		{
			get
			{
				return this._tagValue;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000173B3 File Offset: 0x000155B3
		private Asn1Tag(byte controlFlags, int tagValue)
		{
			this._controlFlags = (controlFlags & 224);
			this._tagValue = tagValue;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000173CA File Offset: 0x000155CA
		public Asn1Tag(UniversalTagNumber universalTagNumber, bool isConstructed = false)
		{
			this = new Asn1Tag(isConstructed ? 32 : 0, (int)universalTagNumber);
			if (universalTagNumber < UniversalTagNumber.EndOfContents || universalTagNumber > UniversalTagNumber.RelativeObjectIdentifierIRI || universalTagNumber == (UniversalTagNumber)15)
			{
				throw new ArgumentOutOfRangeException("universalTagNumber");
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000173F4 File Offset: 0x000155F4
		public Asn1Tag(TagClass tagClass, int tagValue, bool isConstructed = false)
		{
			this = new Asn1Tag((byte)(tagClass | (isConstructed ? ((TagClass)32) : TagClass.Universal)), tagValue);
			if (tagClass < TagClass.Universal || tagClass > TagClass.Private)
			{
				throw new ArgumentOutOfRangeException("tagClass");
			}
			if (tagValue < 0)
			{
				throw new ArgumentOutOfRangeException("tagValue");
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001742E File Offset: 0x0001562E
		public Asn1Tag AsConstructed()
		{
			return new Asn1Tag(this._controlFlags | 32, this._tagValue);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00017445 File Offset: 0x00015645
		public Asn1Tag AsPrimitive()
		{
			return new Asn1Tag((byte)((int)this._controlFlags & -33), this._tagValue);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001745C File Offset: 0x0001565C
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesRead)
		{
			tag = default(Asn1Tag);
			bytesRead = 0;
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = *source[bytesRead];
			bytesRead++;
			uint num = (uint)(b & 31);
			if (num == 31U)
			{
				num = 0U;
				while (source.Length > bytesRead)
				{
					byte b2 = *source[bytesRead];
					byte b3 = b2 & 127;
					bytesRead++;
					if (num >= 33554432U)
					{
						bytesRead = 0;
						return false;
					}
					num <<= 7;
					num |= (uint)b3;
					if (num == 0U)
					{
						bytesRead = 0;
						return false;
					}
					if ((b2 & 128) != 128)
					{
						if (num <= 30U)
						{
							bytesRead = 0;
							return false;
						}
						if (num > 2147483647U)
						{
							bytesRead = 0;
							return false;
						}
						goto IL_9B;
					}
				}
				bytesRead = 0;
				return false;
			}
			IL_9B:
			tag = new Asn1Tag(b, (int)num);
			return true;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00017514 File Offset: 0x00015714
		public int CalculateEncodedSize()
		{
			if (this.TagValue < 31)
			{
				return 1;
			}
			if (this.TagValue <= 127)
			{
				return 2;
			}
			if (this.TagValue <= 16383)
			{
				return 3;
			}
			if (this.TagValue <= 2097151)
			{
				return 4;
			}
			if (this.TagValue <= 268435455)
			{
				return 5;
			}
			return 6;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00017568 File Offset: 0x00015768
		public unsafe bool TryWrite(Span<byte> destination, out int bytesWritten)
		{
			int num = this.CalculateEncodedSize();
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			if (num == 1)
			{
				byte b = (byte)((int)this._controlFlags | this.TagValue);
				*destination[0] = b;
				bytesWritten = 1;
				return true;
			}
			byte b2 = this._controlFlags | 31;
			*destination[0] = b2;
			int i = this.TagValue;
			int num2 = num - 1;
			while (i > 0)
			{
				int num3 = i & 127;
				if (i != this.TagValue)
				{
					num3 |= 128;
				}
				*destination[num2] = (byte)num3;
				i >>= 7;
				num2--;
			}
			bytesWritten = num;
			return true;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00017606 File Offset: 0x00015806
		public bool Equals(Asn1Tag other)
		{
			return this._controlFlags == other._controlFlags && this._tagValue == other._tagValue;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00017626 File Offset: 0x00015826
		public override bool Equals(object obj)
		{
			return obj != null && obj is Asn1Tag && this.Equals((Asn1Tag)obj);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00017643 File Offset: 0x00015843
		public override int GetHashCode()
		{
			return (int)this._controlFlags << 24 ^ this._tagValue;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00017655 File Offset: 0x00015855
		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001765F File Offset: 0x0001585F
		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001766C File Offset: 0x0001586C
		public override string ToString()
		{
			string text;
			if (this.TagClass == TagClass.Universal)
			{
				text = ((UniversalTagNumber)this.TagValue).ToString();
			}
			else
			{
				text = this.TagClass.ToString() + "-" + this.TagValue.ToString();
			}
			if (this.IsConstructed)
			{
				return "Constructed " + text;
			}
			return text;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000176DC File Offset: 0x000158DC
		// Note: this type is marked as 'beforefieldinit'.
		static Asn1Tag()
		{
		}

		// Token: 0x040003F3 RID: 1011
		private const byte ClassMask = 192;

		// Token: 0x040003F4 RID: 1012
		private const byte ConstructedMask = 32;

		// Token: 0x040003F5 RID: 1013
		private const byte ControlMask = 224;

		// Token: 0x040003F6 RID: 1014
		private const byte TagNumberMask = 31;

		// Token: 0x040003F7 RID: 1015
		internal static readonly Asn1Tag EndOfContents = new Asn1Tag(0, 0);

		// Token: 0x040003F8 RID: 1016
		internal static readonly Asn1Tag Boolean = new Asn1Tag(0, 1);

		// Token: 0x040003F9 RID: 1017
		internal static readonly Asn1Tag Integer = new Asn1Tag(0, 2);

		// Token: 0x040003FA RID: 1018
		internal static readonly Asn1Tag PrimitiveBitString = new Asn1Tag(0, 3);

		// Token: 0x040003FB RID: 1019
		internal static readonly Asn1Tag ConstructedBitString = new Asn1Tag(32, 3);

		// Token: 0x040003FC RID: 1020
		internal static readonly Asn1Tag PrimitiveOctetString = new Asn1Tag(0, 4);

		// Token: 0x040003FD RID: 1021
		internal static readonly Asn1Tag ConstructedOctetString = new Asn1Tag(32, 4);

		// Token: 0x040003FE RID: 1022
		internal static readonly Asn1Tag Null = new Asn1Tag(0, 5);

		// Token: 0x040003FF RID: 1023
		internal static readonly Asn1Tag ObjectIdentifier = new Asn1Tag(0, 6);

		// Token: 0x04000400 RID: 1024
		internal static readonly Asn1Tag Enumerated = new Asn1Tag(0, 10);

		// Token: 0x04000401 RID: 1025
		internal static readonly Asn1Tag Sequence = new Asn1Tag(32, 16);

		// Token: 0x04000402 RID: 1026
		internal static readonly Asn1Tag SetOf = new Asn1Tag(32, 17);

		// Token: 0x04000403 RID: 1027
		internal static readonly Asn1Tag UtcTime = new Asn1Tag(0, 23);

		// Token: 0x04000404 RID: 1028
		internal static readonly Asn1Tag GeneralizedTime = new Asn1Tag(0, 24);

		// Token: 0x04000405 RID: 1029
		private readonly byte _controlFlags;

		// Token: 0x04000406 RID: 1030
		private readonly int _tagValue;
	}
}
