using System;
using System.IO;

namespace System.Xml.Xsl
{
	// Token: 0x02000337 RID: 823
	internal struct XmlQueryCardinality
	{
		// Token: 0x060021C2 RID: 8642 RVA: 0x000D6ACA File Offset: 0x000D4CCA
		private XmlQueryCardinality(int value)
		{
			this.value = value;
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x000D6AD3 File Offset: 0x000D4CD3
		public static XmlQueryCardinality None
		{
			get
			{
				return new XmlQueryCardinality(0);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x000D6ADB File Offset: 0x000D4CDB
		public static XmlQueryCardinality Zero
		{
			get
			{
				return new XmlQueryCardinality(1);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000D6AE3 File Offset: 0x000D4CE3
		public static XmlQueryCardinality One
		{
			get
			{
				return new XmlQueryCardinality(2);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000D6AEB File Offset: 0x000D4CEB
		public static XmlQueryCardinality ZeroOrOne
		{
			get
			{
				return new XmlQueryCardinality(3);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x000D6AF3 File Offset: 0x000D4CF3
		public static XmlQueryCardinality More
		{
			get
			{
				return new XmlQueryCardinality(4);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000D6AFB File Offset: 0x000D4CFB
		public static XmlQueryCardinality NotOne
		{
			get
			{
				return new XmlQueryCardinality(5);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000D6B03 File Offset: 0x000D4D03
		public static XmlQueryCardinality OneOrMore
		{
			get
			{
				return new XmlQueryCardinality(6);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000D6B0B File Offset: 0x000D4D0B
		public static XmlQueryCardinality ZeroOrMore
		{
			get
			{
				return new XmlQueryCardinality(7);
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000D6B13 File Offset: 0x000D4D13
		public bool Equals(XmlQueryCardinality other)
		{
			return this.value == other.value;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000D6B13 File Offset: 0x000D4D13
		public static bool operator ==(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return left.value == right.value;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000D6B23 File Offset: 0x000D4D23
		public static bool operator !=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return left.value != right.value;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000D6B36 File Offset: 0x000D4D36
		public override bool Equals(object other)
		{
			return other is XmlQueryCardinality && this.Equals((XmlQueryCardinality)other);
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000D6B4E File Offset: 0x000D4D4E
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000D6B56 File Offset: 0x000D4D56
		public static XmlQueryCardinality operator |(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return new XmlQueryCardinality(left.value | right.value);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000D6B6A File Offset: 0x000D4D6A
		public static XmlQueryCardinality operator &(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return new XmlQueryCardinality(left.value & right.value);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000D6B7E File Offset: 0x000D4D7E
		public static XmlQueryCardinality operator *(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return XmlQueryCardinality.cardinalityProduct[left.value, right.value];
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000D6B96 File Offset: 0x000D4D96
		public static XmlQueryCardinality operator +(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return XmlQueryCardinality.cardinalitySum[left.value, right.value];
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000D6BAE File Offset: 0x000D4DAE
		public static bool operator <=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return (left.value & ~right.value) == 0;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000D6BC1 File Offset: 0x000D4DC1
		public static bool operator >=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return (right.value & ~left.value) == 0;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000D6BD4 File Offset: 0x000D4DD4
		public XmlQueryCardinality AtMost()
		{
			return new XmlQueryCardinality(this.value | this.value >> 1 | this.value >> 2);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000D6BF3 File Offset: 0x000D4DF3
		public bool NeverSubset(XmlQueryCardinality other)
		{
			return this.value != 0 && (this.value & other.value) == 0;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000D6C0F File Offset: 0x000D4E0F
		public string ToString(string format)
		{
			if (format == "S")
			{
				return XmlQueryCardinality.serialized[this.value];
			}
			return this.ToString();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000D6C37 File Offset: 0x000D4E37
		public override string ToString()
		{
			return XmlQueryCardinality.toString[this.value];
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000D6C48 File Offset: 0x000D4E48
		public XmlQueryCardinality(string s)
		{
			this.value = 0;
			for (int i = 0; i < XmlQueryCardinality.serialized.Length; i++)
			{
				if (s == XmlQueryCardinality.serialized[i])
				{
					this.value = i;
					return;
				}
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000D6C85 File Offset: 0x000D4E85
		public void GetObjectData(BinaryWriter writer)
		{
			writer.Write((byte)this.value);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000D6C94 File Offset: 0x000D4E94
		public XmlQueryCardinality(BinaryReader reader)
		{
			this = new XmlQueryCardinality((int)reader.ReadByte());
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000D6CA4 File Offset: 0x000D4EA4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQueryCardinality()
		{
			XmlQueryCardinality[,] array = new XmlQueryCardinality[8, 8];
			array[0, 0] = XmlQueryCardinality.None;
			array[0, 1] = XmlQueryCardinality.Zero;
			array[0, 2] = XmlQueryCardinality.None;
			array[0, 3] = XmlQueryCardinality.Zero;
			array[0, 4] = XmlQueryCardinality.None;
			array[0, 5] = XmlQueryCardinality.Zero;
			array[0, 6] = XmlQueryCardinality.None;
			array[0, 7] = XmlQueryCardinality.Zero;
			array[1, 0] = XmlQueryCardinality.Zero;
			array[1, 1] = XmlQueryCardinality.Zero;
			array[1, 2] = XmlQueryCardinality.Zero;
			array[1, 3] = XmlQueryCardinality.Zero;
			array[1, 4] = XmlQueryCardinality.Zero;
			array[1, 5] = XmlQueryCardinality.Zero;
			array[1, 6] = XmlQueryCardinality.Zero;
			array[1, 7] = XmlQueryCardinality.Zero;
			array[2, 0] = XmlQueryCardinality.None;
			array[2, 1] = XmlQueryCardinality.Zero;
			array[2, 2] = XmlQueryCardinality.One;
			array[2, 3] = XmlQueryCardinality.ZeroOrOne;
			array[2, 4] = XmlQueryCardinality.More;
			array[2, 5] = XmlQueryCardinality.NotOne;
			array[2, 6] = XmlQueryCardinality.OneOrMore;
			array[2, 7] = XmlQueryCardinality.ZeroOrMore;
			array[3, 0] = XmlQueryCardinality.Zero;
			array[3, 1] = XmlQueryCardinality.Zero;
			array[3, 2] = XmlQueryCardinality.ZeroOrOne;
			array[3, 3] = XmlQueryCardinality.ZeroOrOne;
			array[3, 4] = XmlQueryCardinality.NotOne;
			array[3, 5] = XmlQueryCardinality.NotOne;
			array[3, 6] = XmlQueryCardinality.ZeroOrMore;
			array[3, 7] = XmlQueryCardinality.ZeroOrMore;
			array[4, 0] = XmlQueryCardinality.None;
			array[4, 1] = XmlQueryCardinality.Zero;
			array[4, 2] = XmlQueryCardinality.More;
			array[4, 3] = XmlQueryCardinality.NotOne;
			array[4, 4] = XmlQueryCardinality.More;
			array[4, 5] = XmlQueryCardinality.NotOne;
			array[4, 6] = XmlQueryCardinality.More;
			array[4, 7] = XmlQueryCardinality.NotOne;
			array[5, 0] = XmlQueryCardinality.Zero;
			array[5, 1] = XmlQueryCardinality.Zero;
			array[5, 2] = XmlQueryCardinality.NotOne;
			array[5, 3] = XmlQueryCardinality.NotOne;
			array[5, 4] = XmlQueryCardinality.NotOne;
			array[5, 5] = XmlQueryCardinality.NotOne;
			array[5, 6] = XmlQueryCardinality.NotOne;
			array[5, 7] = XmlQueryCardinality.NotOne;
			array[6, 0] = XmlQueryCardinality.None;
			array[6, 1] = XmlQueryCardinality.Zero;
			array[6, 2] = XmlQueryCardinality.OneOrMore;
			array[6, 3] = XmlQueryCardinality.ZeroOrMore;
			array[6, 4] = XmlQueryCardinality.More;
			array[6, 5] = XmlQueryCardinality.NotOne;
			array[6, 6] = XmlQueryCardinality.OneOrMore;
			array[6, 7] = XmlQueryCardinality.ZeroOrMore;
			array[7, 0] = XmlQueryCardinality.Zero;
			array[7, 1] = XmlQueryCardinality.Zero;
			array[7, 2] = XmlQueryCardinality.ZeroOrMore;
			array[7, 3] = XmlQueryCardinality.ZeroOrMore;
			array[7, 4] = XmlQueryCardinality.NotOne;
			array[7, 5] = XmlQueryCardinality.NotOne;
			array[7, 6] = XmlQueryCardinality.ZeroOrMore;
			array[7, 7] = XmlQueryCardinality.ZeroOrMore;
			XmlQueryCardinality.cardinalityProduct = array;
			XmlQueryCardinality[,] array2 = new XmlQueryCardinality[8, 8];
			array2[0, 0] = XmlQueryCardinality.None;
			array2[0, 1] = XmlQueryCardinality.Zero;
			array2[0, 2] = XmlQueryCardinality.One;
			array2[0, 3] = XmlQueryCardinality.ZeroOrOne;
			array2[0, 4] = XmlQueryCardinality.More;
			array2[0, 5] = XmlQueryCardinality.NotOne;
			array2[0, 6] = XmlQueryCardinality.OneOrMore;
			array2[0, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[1, 0] = XmlQueryCardinality.Zero;
			array2[1, 1] = XmlQueryCardinality.Zero;
			array2[1, 2] = XmlQueryCardinality.One;
			array2[1, 3] = XmlQueryCardinality.ZeroOrOne;
			array2[1, 4] = XmlQueryCardinality.More;
			array2[1, 5] = XmlQueryCardinality.NotOne;
			array2[1, 6] = XmlQueryCardinality.OneOrMore;
			array2[1, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[2, 0] = XmlQueryCardinality.One;
			array2[2, 1] = XmlQueryCardinality.One;
			array2[2, 2] = XmlQueryCardinality.More;
			array2[2, 3] = XmlQueryCardinality.OneOrMore;
			array2[2, 4] = XmlQueryCardinality.More;
			array2[2, 5] = XmlQueryCardinality.OneOrMore;
			array2[2, 6] = XmlQueryCardinality.More;
			array2[2, 7] = XmlQueryCardinality.OneOrMore;
			array2[3, 0] = XmlQueryCardinality.ZeroOrOne;
			array2[3, 1] = XmlQueryCardinality.ZeroOrOne;
			array2[3, 2] = XmlQueryCardinality.OneOrMore;
			array2[3, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[3, 4] = XmlQueryCardinality.More;
			array2[3, 5] = XmlQueryCardinality.ZeroOrMore;
			array2[3, 6] = XmlQueryCardinality.OneOrMore;
			array2[3, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[4, 0] = XmlQueryCardinality.More;
			array2[4, 1] = XmlQueryCardinality.More;
			array2[4, 2] = XmlQueryCardinality.More;
			array2[4, 3] = XmlQueryCardinality.More;
			array2[4, 4] = XmlQueryCardinality.More;
			array2[4, 5] = XmlQueryCardinality.More;
			array2[4, 6] = XmlQueryCardinality.More;
			array2[4, 7] = XmlQueryCardinality.More;
			array2[5, 0] = XmlQueryCardinality.NotOne;
			array2[5, 1] = XmlQueryCardinality.NotOne;
			array2[5, 2] = XmlQueryCardinality.OneOrMore;
			array2[5, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[5, 4] = XmlQueryCardinality.More;
			array2[5, 5] = XmlQueryCardinality.NotOne;
			array2[5, 6] = XmlQueryCardinality.OneOrMore;
			array2[5, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[6, 0] = XmlQueryCardinality.OneOrMore;
			array2[6, 1] = XmlQueryCardinality.OneOrMore;
			array2[6, 2] = XmlQueryCardinality.More;
			array2[6, 3] = XmlQueryCardinality.OneOrMore;
			array2[6, 4] = XmlQueryCardinality.More;
			array2[6, 5] = XmlQueryCardinality.OneOrMore;
			array2[6, 6] = XmlQueryCardinality.More;
			array2[6, 7] = XmlQueryCardinality.OneOrMore;
			array2[7, 0] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 1] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 2] = XmlQueryCardinality.OneOrMore;
			array2[7, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 4] = XmlQueryCardinality.More;
			array2[7, 5] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 6] = XmlQueryCardinality.OneOrMore;
			array2[7, 7] = XmlQueryCardinality.ZeroOrMore;
			XmlQueryCardinality.cardinalitySum = array2;
			XmlQueryCardinality.toString = new string[]
			{
				"",
				"?",
				"",
				"?",
				"+",
				"*",
				"+",
				"*"
			};
			XmlQueryCardinality.serialized = new string[]
			{
				"None",
				"Zero",
				"One",
				"ZeroOrOne",
				"More",
				"NotOne",
				"OneOrMore",
				"ZeroOrMore"
			};
		}

		// Token: 0x04001BC6 RID: 7110
		private int value;

		// Token: 0x04001BC7 RID: 7111
		private static readonly XmlQueryCardinality[,] cardinalityProduct;

		// Token: 0x04001BC8 RID: 7112
		private static readonly XmlQueryCardinality[,] cardinalitySum;

		// Token: 0x04001BC9 RID: 7113
		private static readonly string[] toString;

		// Token: 0x04001BCA RID: 7114
		private static readonly string[] serialized;
	}
}
