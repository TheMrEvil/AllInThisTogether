using System;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Xsl
{
	// Token: 0x02000338 RID: 824
	internal abstract class XmlQueryType : ListBase<XmlQueryType>
	{
		// Token: 0x060021DE RID: 8670 RVA: 0x000D73E0 File Offset: 0x000D55E0
		static XmlQueryType()
		{
			XmlQueryType.TypeCodeDerivation = new XmlQueryType.BitMatrix(XmlQueryType.BaseTypeCodes.Length);
			for (int i = 0; i < XmlQueryType.BaseTypeCodes.Length; i++)
			{
				int num = i;
				for (;;)
				{
					XmlQueryType.TypeCodeDerivation[i, num] = true;
					if (XmlQueryType.BaseTypeCodes[num] == (XmlTypeCode)num)
					{
						break;
					}
					num = (int)XmlQueryType.BaseTypeCodes[num];
				}
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060021DF RID: 8671
		public abstract XmlTypeCode TypeCode { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060021E0 RID: 8672
		public abstract XmlQualifiedNameTest NameTest { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060021E1 RID: 8673
		public abstract XmlSchemaType SchemaType { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060021E2 RID: 8674
		public abstract bool IsNillable { get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060021E3 RID: 8675
		public abstract XmlNodeKindFlags NodeKinds { get; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060021E4 RID: 8676
		public abstract bool IsStrict { get; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060021E5 RID: 8677
		public abstract XmlQueryCardinality Cardinality { get; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060021E6 RID: 8678
		public abstract XmlQueryType Prime { get; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060021E7 RID: 8679
		public abstract bool IsNotRtf { get; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060021E8 RID: 8680
		public abstract bool IsDod { get; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060021E9 RID: 8681
		public abstract XmlValueConverter ClrMapping { get; }

		// Token: 0x060021EA RID: 8682 RVA: 0x000D7654 File Offset: 0x000D5854
		public bool IsSubtypeOf(XmlQueryType baseType)
		{
			if (!(this.Cardinality <= baseType.Cardinality) || (!this.IsDod && baseType.IsDod))
			{
				return false;
			}
			if (!this.IsDod && baseType.IsDod)
			{
				return false;
			}
			XmlQueryType prime = this.Prime;
			XmlQueryType prime2 = baseType.Prime;
			if (prime == prime2)
			{
				return true;
			}
			if (prime.Count == 1 && prime2.Count == 1)
			{
				return prime.IsSubtypeOfItemType(prime2);
			}
			foreach (XmlQueryType xmlQueryType in prime)
			{
				bool flag = false;
				foreach (XmlQueryType baseType2 in prime2)
				{
					if (xmlQueryType.IsSubtypeOfItemType(baseType2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000D7758 File Offset: 0x000D5958
		public bool NeverSubtypeOf(XmlQueryType baseType)
		{
			if (this.Cardinality.NeverSubset(baseType.Cardinality))
			{
				return true;
			}
			if (this.MaybeEmpty && baseType.MaybeEmpty)
			{
				return false;
			}
			if (this.Count == 0)
			{
				return false;
			}
			foreach (XmlQueryType xmlQueryType in this)
			{
				foreach (XmlQueryType other in baseType)
				{
					if (xmlQueryType.HasIntersectionItemType(other))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000D7820 File Offset: 0x000D5A20
		public bool Equals(XmlQueryType that)
		{
			if (that == null)
			{
				return false;
			}
			if (this.Cardinality != that.Cardinality || this.IsDod != that.IsDod)
			{
				return false;
			}
			XmlQueryType prime = this.Prime;
			XmlQueryType prime2 = that.Prime;
			if (prime == prime2)
			{
				return true;
			}
			if (prime.Count != prime2.Count)
			{
				return false;
			}
			if (prime.Count == 1)
			{
				return prime.TypeCode == prime2.TypeCode && prime.NameTest == prime2.NameTest && prime.SchemaType == prime2.SchemaType && prime.IsStrict == prime2.IsStrict && prime.IsNotRtf == prime2.IsNotRtf;
			}
			foreach (XmlQueryType xmlQueryType in this)
			{
				bool flag = false;
				foreach (XmlQueryType xmlQueryType2 in that)
				{
					if (xmlQueryType.TypeCode == xmlQueryType2.TypeCode && xmlQueryType.NameTest == xmlQueryType2.NameTest && xmlQueryType.SchemaType == xmlQueryType2.SchemaType && xmlQueryType.IsStrict == xmlQueryType2.IsStrict && xmlQueryType.IsNotRtf == xmlQueryType2.IsNotRtf)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000D79B4 File Offset: 0x000D5BB4
		public static bool operator ==(XmlQueryType left, XmlQueryType right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000D79C5 File Offset: 0x000D5BC5
		public static bool operator !=(XmlQueryType left, XmlQueryType right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000D79D9 File Offset: 0x000D5BD9
		public bool IsEmpty
		{
			get
			{
				return this.Cardinality <= XmlQueryCardinality.Zero;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000D79EB File Offset: 0x000D5BEB
		public bool IsSingleton
		{
			get
			{
				return this.Cardinality <= XmlQueryCardinality.One;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000D79FD File Offset: 0x000D5BFD
		public bool MaybeEmpty
		{
			get
			{
				return XmlQueryCardinality.Zero <= this.Cardinality;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000D7A0F File Offset: 0x000D5C0F
		public bool MaybeMany
		{
			get
			{
				return XmlQueryCardinality.More <= this.Cardinality;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000D7A21 File Offset: 0x000D5C21
		public bool IsNode
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsNode) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000D7A34 File Offset: 0x000D5C34
		public bool IsAtomicValue
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsAtomicValue) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000D7A47 File Offset: 0x000D5C47
		public bool IsNumeric
		{
			get
			{
				return (XmlQueryType.TypeCodeToFlags[(int)this.TypeCode] & XmlQueryType.TypeFlags.IsNumeric) > XmlQueryType.TypeFlags.None;
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000D7A5C File Offset: 0x000D5C5C
		public override bool Equals(object obj)
		{
			XmlQueryType xmlQueryType = obj as XmlQueryType;
			return !(xmlQueryType == null) && this.Equals(xmlQueryType);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000D7A84 File Offset: 0x000D5C84
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				int num = (int)this.TypeCode;
				XmlSchemaType schemaType = this.SchemaType;
				if (schemaType != null)
				{
					num += (num << 7 ^ schemaType.GetHashCode());
				}
				num += (num << 7 ^ (int)this.NodeKinds);
				num += (num << 7 ^ this.Cardinality.GetHashCode());
				num += (num << 7 ^ (this.IsStrict ? 1 : 0));
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				this.hashCode = ((num == 0) ? 1 : num);
			}
			return this.hashCode;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000D7B19 File Offset: 0x000D5D19
		public override string ToString()
		{
			return this.ToString("G");
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000D7B28 File Offset: 0x000D5D28
		public string ToString(string format)
		{
			StringBuilder stringBuilder;
			if (format == "S")
			{
				stringBuilder = new StringBuilder();
				stringBuilder.Append(this.Cardinality.ToString(format));
				stringBuilder.Append(';');
				for (int i = 0; i < this.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append("|");
					}
					stringBuilder.Append(this[i].TypeCode.ToString());
				}
				stringBuilder.Append(';');
				stringBuilder.Append(this.IsStrict);
				return stringBuilder.ToString();
			}
			bool flag = format == "X";
			if (this.Cardinality == XmlQueryCardinality.None)
			{
				return "none";
			}
			if (this.Cardinality == XmlQueryCardinality.Zero)
			{
				return "empty";
			}
			stringBuilder = new StringBuilder();
			int count = this.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					string[] array = new string[this.Count];
					for (int j = 0; j < this.Count; j++)
					{
						array[j] = this[j].ItemTypeToString(flag);
					}
					Array.Sort<string>(array);
					stringBuilder = new StringBuilder();
					stringBuilder.Append('(');
					stringBuilder.Append(array[0]);
					for (int k = 1; k < array.Length; k++)
					{
						stringBuilder.Append(" | ");
						stringBuilder.Append(array[k]);
					}
					stringBuilder.Append(')');
				}
				else
				{
					stringBuilder.Append(this[0].ItemTypeToString(flag));
				}
			}
			else
			{
				stringBuilder.Append("none");
			}
			stringBuilder.Append(this.Cardinality.ToString());
			if (!flag && this.IsDod)
			{
				stringBuilder.Append('#');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060021FA RID: 8698
		public abstract void GetObjectData(BinaryWriter writer);

		// Token: 0x060021FB RID: 8699 RVA: 0x000D7D08 File Offset: 0x000D5F08
		private bool IsSubtypeOfItemType(XmlQueryType baseType)
		{
			XmlSchemaType schemaType = baseType.SchemaType;
			if (this.TypeCode != baseType.TypeCode)
			{
				if (baseType.IsStrict)
				{
					return false;
				}
				XmlSchemaType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(baseType.TypeCode);
				return (builtInSimpleType == null || schemaType == builtInSimpleType) && XmlQueryType.TypeCodeDerivation[this.TypeCode, baseType.TypeCode];
			}
			else
			{
				if (baseType.IsStrict)
				{
					return this.IsStrict && this.SchemaType == schemaType;
				}
				return (this.IsNotRtf || !baseType.IsNotRtf) && this.NameTest.IsSubsetOf(baseType.NameTest) && (schemaType == XmlSchemaComplexType.AnyType || XmlSchemaType.IsDerivedFrom(this.SchemaType, schemaType, XmlSchemaDerivationMethod.Empty)) && (!this.IsNillable || baseType.IsNillable);
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000D7DC8 File Offset: 0x000D5FC8
		private bool HasIntersectionItemType(XmlQueryType other)
		{
			if (this.TypeCode == other.TypeCode && (this.NodeKinds & (XmlNodeKindFlags.Document | XmlNodeKindFlags.Element | XmlNodeKindFlags.Attribute)) != XmlNodeKindFlags.None)
			{
				return this.TypeCode == XmlTypeCode.Node || (this.NameTest.HasIntersection(other.NameTest) && (XmlSchemaType.IsDerivedFrom(this.SchemaType, other.SchemaType, XmlSchemaDerivationMethod.Empty) || XmlSchemaType.IsDerivedFrom(other.SchemaType, this.SchemaType, XmlSchemaDerivationMethod.Empty)));
			}
			return this.IsSubtypeOf(other) || other.IsSubtypeOf(this);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000D7E50 File Offset: 0x000D6050
		private string ItemTypeToString(bool isXQ)
		{
			string text;
			if (this.IsNode)
			{
				text = XmlQueryType.TypeNames[(int)this.TypeCode];
				XmlTypeCode typeCode = this.TypeCode;
				if (typeCode != XmlTypeCode.Document)
				{
					if (typeCode - XmlTypeCode.Element > 1)
					{
						goto IL_B0;
					}
				}
				else if (isXQ)
				{
					text = text + "{(element" + this.NameAndType(true) + "?&text?&comment?&processing-instruction?)*}";
					goto IL_B0;
				}
				text += this.NameAndType(isXQ);
			}
			else if (this.SchemaType != XmlSchemaComplexType.AnyType)
			{
				if (this.SchemaType.QualifiedName.IsEmpty)
				{
					text = "<:" + XmlQueryType.TypeNames[(int)this.TypeCode];
				}
				else
				{
					text = XmlQueryType.QNameToString(this.SchemaType.QualifiedName);
				}
			}
			else
			{
				text = XmlQueryType.TypeNames[(int)this.TypeCode];
			}
			IL_B0:
			if (!isXQ && this.IsStrict)
			{
				text += "=";
			}
			return text;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000D7F28 File Offset: 0x000D6128
		private string NameAndType(bool isXQ)
		{
			string text = this.NameTest.ToString();
			string text2 = "*";
			if (this.SchemaType.QualifiedName.IsEmpty)
			{
				text2 = "typeof(" + text + ")";
			}
			else if (isXQ || (this.SchemaType != XmlSchemaComplexType.AnyType && this.SchemaType != DatatypeImplementation.AnySimpleType))
			{
				text2 = XmlQueryType.QNameToString(this.SchemaType.QualifiedName);
			}
			if (this.IsNillable)
			{
				text2 += " nillable";
			}
			if (text == "*" && text2 == "*")
			{
				return "";
			}
			return string.Concat(new string[]
			{
				"(",
				text,
				", ",
				text2,
				")"
			});
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000D7FFC File Offset: 0x000D61FC
		private static string QNameToString(XmlQualifiedName name)
		{
			if (name.IsEmpty)
			{
				return "*";
			}
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000D808C File Offset: 0x000D628C
		protected XmlQueryType()
		{
		}

		// Token: 0x04001BCB RID: 7115
		private static readonly XmlQueryType.BitMatrix TypeCodeDerivation;

		// Token: 0x04001BCC RID: 7116
		private int hashCode;

		// Token: 0x04001BCD RID: 7117
		private static readonly XmlQueryType.TypeFlags[] TypeCodeToFlags = new XmlQueryType.TypeFlags[]
		{
			(XmlQueryType.TypeFlags)7,
			XmlQueryType.TypeFlags.None,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsNode,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			(XmlQueryType.TypeFlags)6,
			XmlQueryType.TypeFlags.IsAtomicValue,
			XmlQueryType.TypeFlags.IsAtomicValue
		};

		// Token: 0x04001BCE RID: 7118
		private static readonly XmlTypeCode[] BaseTypeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.None,
			XmlTypeCode.Item,
			XmlTypeCode.Item,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Node,
			XmlTypeCode.Item,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.AnyAtomicType,
			XmlTypeCode.String,
			XmlTypeCode.NormalizedString,
			XmlTypeCode.Token,
			XmlTypeCode.Token,
			XmlTypeCode.Token,
			XmlTypeCode.Name,
			XmlTypeCode.NCName,
			XmlTypeCode.NCName,
			XmlTypeCode.NCName,
			XmlTypeCode.Decimal,
			XmlTypeCode.Integer,
			XmlTypeCode.NonPositiveInteger,
			XmlTypeCode.Integer,
			XmlTypeCode.Long,
			XmlTypeCode.Int,
			XmlTypeCode.Short,
			XmlTypeCode.Integer,
			XmlTypeCode.NonNegativeInteger,
			XmlTypeCode.UnsignedLong,
			XmlTypeCode.UnsignedInt,
			XmlTypeCode.UnsignedShort,
			XmlTypeCode.NonNegativeInteger,
			XmlTypeCode.Duration,
			XmlTypeCode.Duration
		};

		// Token: 0x04001BCF RID: 7119
		private static readonly string[] TypeNames = new string[]
		{
			"none",
			"item",
			"node",
			"document",
			"element",
			"attribute",
			"namespace",
			"processing-instruction",
			"comment",
			"text",
			"xdt:anyAtomicType",
			"xdt:untypedAtomic",
			"xs:string",
			"xs:boolean",
			"xs:decimal",
			"xs:float",
			"xs:double",
			"xs:duration",
			"xs:dateTime",
			"xs:time",
			"xs:date",
			"xs:gYearMonth",
			"xs:gYear",
			"xs:gMonthDay",
			"xs:gDay",
			"xs:gMonth",
			"xs:hexBinary",
			"xs:base64Binary",
			"xs:anyUri",
			"xs:QName",
			"xs:NOTATION",
			"xs:normalizedString",
			"xs:token",
			"xs:language",
			"xs:NMTOKEN",
			"xs:Name",
			"xs:NCName",
			"xs:ID",
			"xs:IDREF",
			"xs:ENTITY",
			"xs:integer",
			"xs:nonPositiveInteger",
			"xs:negativeInteger",
			"xs:long",
			"xs:int",
			"xs:short",
			"xs:byte",
			"xs:nonNegativeInteger",
			"xs:unsignedLong",
			"xs:unsignedInt",
			"xs:unsignedShort",
			"xs:unsignedByte",
			"xs:positiveInteger",
			"xdt:yearMonthDuration",
			"xdt:dayTimeDuration"
		};

		// Token: 0x02000339 RID: 825
		private enum TypeFlags
		{
			// Token: 0x04001BD1 RID: 7121
			None,
			// Token: 0x04001BD2 RID: 7122
			IsNode,
			// Token: 0x04001BD3 RID: 7123
			IsAtomicValue,
			// Token: 0x04001BD4 RID: 7124
			IsNumeric = 4
		}

		// Token: 0x0200033A RID: 826
		private sealed class BitMatrix
		{
			// Token: 0x06002201 RID: 8705 RVA: 0x000D8094 File Offset: 0x000D6294
			public BitMatrix(int count)
			{
				this.bits = new ulong[count];
			}

			// Token: 0x170006AF RID: 1711
			public bool this[int index1, int index2]
			{
				get
				{
					return (this.bits[index1] & 1UL << index2) > 0UL;
				}
				set
				{
					if (value)
					{
						this.bits[index1] |= 1UL << index2;
						return;
					}
					this.bits[index1] &= ~(1UL << index2);
				}
			}

			// Token: 0x170006B0 RID: 1712
			public bool this[XmlTypeCode index1, XmlTypeCode index2]
			{
				get
				{
					return this[(int)index1, (int)index2];
				}
			}

			// Token: 0x04001BD5 RID: 7125
			private ulong[] bits;
		}
	}
}
