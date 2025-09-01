using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x0200033B RID: 827
	internal static class XmlQueryTypeFactory
	{
		// Token: 0x06002205 RID: 8709 RVA: 0x000D80FD File Offset: 0x000D62FD
		public static XmlQueryType Type(XmlTypeCode code, bool isStrict)
		{
			return XmlQueryTypeFactory.ItemType.Create(code, isStrict);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000D8108 File Offset: 0x000D6308
		public static XmlQueryType Type(XmlSchemaSimpleType schemaType, bool isStrict)
		{
			if (schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				if (schemaType == DatatypeImplementation.AnySimpleType)
				{
					return XmlQueryTypeFactory.AnyAtomicTypeS;
				}
				return XmlQueryTypeFactory.ItemType.Create(schemaType, isStrict);
			}
			else
			{
				while (schemaType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
				{
					schemaType = (XmlSchemaSimpleType)schemaType.BaseXmlSchemaType;
				}
				if (schemaType.DerivedBy == XmlSchemaDerivationMethod.List)
				{
					return XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Type(((XmlSchemaSimpleTypeList)schemaType.Content).BaseItemType, isStrict), XmlQueryCardinality.ZeroOrMore);
				}
				XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)schemaType.Content).BaseMemberTypes;
				XmlQueryType[] array = new XmlQueryType[baseMemberTypes.Length];
				for (int i = 0; i < baseMemberTypes.Length; i++)
				{
					array[i] = XmlQueryTypeFactory.Type(baseMemberTypes[i], isStrict);
				}
				return XmlQueryTypeFactory.Choice(array);
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000D81B2 File Offset: 0x000D63B2
		public static XmlQueryType Choice(XmlQueryType left, XmlQueryType right)
		{
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(XmlQueryTypeFactory.PrimeChoice(new List<XmlQueryType>(left), right)), left.Cardinality | right.Cardinality);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000D81DC File Offset: 0x000D63DC
		public static XmlQueryType Choice(params XmlQueryType[] types)
		{
			if (types.Length == 0)
			{
				return XmlQueryTypeFactory.None;
			}
			if (types.Length == 1)
			{
				return types[0];
			}
			List<XmlQueryType> list = new List<XmlQueryType>(types[0]);
			XmlQueryCardinality xmlQueryCardinality = types[0].Cardinality;
			for (int i = 1; i < types.Length; i++)
			{
				XmlQueryTypeFactory.PrimeChoice(list, types[i]);
				xmlQueryCardinality |= types[i].Cardinality;
			}
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(list), xmlQueryCardinality);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000D8242 File Offset: 0x000D6442
		public static XmlQueryType NodeChoice(XmlNodeKindFlags kinds)
		{
			return XmlQueryTypeFactory.ChoiceType.Create(kinds);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000D824A File Offset: 0x000D644A
		public static XmlQueryType Sequence(XmlQueryType left, XmlQueryType right)
		{
			return XmlQueryTypeFactory.SequenceType.Create(XmlQueryTypeFactory.ChoiceType.Create(XmlQueryTypeFactory.PrimeChoice(new List<XmlQueryType>(left), right)), left.Cardinality + right.Cardinality);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000D8273 File Offset: 0x000D6473
		public static XmlQueryType PrimeProduct(XmlQueryType t, XmlQueryCardinality c)
		{
			if (t.Cardinality == c && !t.IsDod)
			{
				return t;
			}
			return XmlQueryTypeFactory.SequenceType.Create(t.Prime, c);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000D8299 File Offset: 0x000D6499
		public static XmlQueryType Product(XmlQueryType t, XmlQueryCardinality c)
		{
			return XmlQueryTypeFactory.PrimeProduct(t, t.Cardinality * c);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000D82AD File Offset: 0x000D64AD
		public static XmlQueryType AtMost(XmlQueryType t, XmlQueryCardinality c)
		{
			return XmlQueryTypeFactory.PrimeProduct(t, c.AtMost());
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000D82BC File Offset: 0x000D64BC
		private static List<XmlQueryType> PrimeChoice(List<XmlQueryType> accumulator, IList<XmlQueryType> types)
		{
			foreach (XmlQueryType itemType in types)
			{
				XmlQueryTypeFactory.AddItemToChoice(accumulator, itemType);
			}
			return accumulator;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000D8308 File Offset: 0x000D6508
		private static void AddItemToChoice(List<XmlQueryType> accumulator, XmlQueryType itemType)
		{
			bool flag = true;
			for (int i = 0; i < accumulator.Count; i++)
			{
				if (itemType.IsSubtypeOf(accumulator[i]))
				{
					return;
				}
				if (accumulator[i].IsSubtypeOf(itemType))
				{
					if (flag)
					{
						flag = false;
						accumulator[i] = itemType;
					}
					else
					{
						accumulator.RemoveAt(i);
						i--;
					}
				}
			}
			if (flag)
			{
				accumulator.Add(itemType);
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000D836B File Offset: 0x000D656B
		public static XmlQueryType Type(XPathNodeType kind, XmlQualifiedNameTest nameTest, XmlSchemaType contentType, bool isNillable)
		{
			return XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.NodeKindToTypeCode[(int)kind], nameTest, contentType, isNillable);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000D837C File Offset: 0x000D657C
		[Conditional("DEBUG")]
		public static void CheckSerializability(XmlQueryType type)
		{
			type.GetObjectData(new BinaryWriter(Stream.Null));
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000D8390 File Offset: 0x000D6590
		public static void Serialize(BinaryWriter writer, XmlQueryType type)
		{
			sbyte value;
			if (type.GetType() == typeof(XmlQueryTypeFactory.ItemType))
			{
				value = 0;
			}
			else if (type.GetType() == typeof(XmlQueryTypeFactory.ChoiceType))
			{
				value = 1;
			}
			else if (type.GetType() == typeof(XmlQueryTypeFactory.SequenceType))
			{
				value = 2;
			}
			else
			{
				value = -1;
			}
			writer.Write(value);
			type.GetObjectData(writer);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000D8400 File Offset: 0x000D6600
		public static XmlQueryType Deserialize(BinaryReader reader)
		{
			switch (reader.ReadByte())
			{
			case 0:
				return XmlQueryTypeFactory.ItemType.Create(reader);
			case 1:
				return XmlQueryTypeFactory.ChoiceType.Create(reader);
			case 2:
				return XmlQueryTypeFactory.SequenceType.Create(reader);
			default:
				return null;
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000D8440 File Offset: 0x000D6640
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQueryTypeFactory()
		{
		}

		// Token: 0x04001BD6 RID: 7126
		public static readonly XmlQueryType None = XmlQueryTypeFactory.ChoiceType.None;

		// Token: 0x04001BD7 RID: 7127
		public static readonly XmlQueryType Empty = XmlQueryTypeFactory.SequenceType.Zero;

		// Token: 0x04001BD8 RID: 7128
		public static readonly XmlQueryType Item = XmlQueryTypeFactory.Type(XmlTypeCode.Item, false);

		// Token: 0x04001BD9 RID: 7129
		public static readonly XmlQueryType ItemS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Item, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BDA RID: 7130
		public static readonly XmlQueryType Node = XmlQueryTypeFactory.Type(XmlTypeCode.Node, false);

		// Token: 0x04001BDB RID: 7131
		public static readonly XmlQueryType NodeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Node, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BDC RID: 7132
		public static readonly XmlQueryType Element = XmlQueryTypeFactory.Type(XmlTypeCode.Element, false);

		// Token: 0x04001BDD RID: 7133
		public static readonly XmlQueryType ElementS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Element, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BDE RID: 7134
		public static readonly XmlQueryType Document = XmlQueryTypeFactory.Type(XmlTypeCode.Document, false);

		// Token: 0x04001BDF RID: 7135
		public static readonly XmlQueryType DocumentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Document, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BE0 RID: 7136
		public static readonly XmlQueryType Attribute = XmlQueryTypeFactory.Type(XmlTypeCode.Attribute, false);

		// Token: 0x04001BE1 RID: 7137
		public static readonly XmlQueryType AttributeQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Attribute, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x04001BE2 RID: 7138
		public static readonly XmlQueryType AttributeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Attribute, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BE3 RID: 7139
		public static readonly XmlQueryType Namespace = XmlQueryTypeFactory.Type(XmlTypeCode.Namespace, false);

		// Token: 0x04001BE4 RID: 7140
		public static readonly XmlQueryType NamespaceS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Namespace, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BE5 RID: 7141
		public static readonly XmlQueryType Text = XmlQueryTypeFactory.Type(XmlTypeCode.Text, false);

		// Token: 0x04001BE6 RID: 7142
		public static readonly XmlQueryType TextS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Text, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BE7 RID: 7143
		public static readonly XmlQueryType Comment = XmlQueryTypeFactory.Type(XmlTypeCode.Comment, false);

		// Token: 0x04001BE8 RID: 7144
		public static readonly XmlQueryType CommentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Comment, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BE9 RID: 7145
		public static readonly XmlQueryType PI = XmlQueryTypeFactory.Type(XmlTypeCode.ProcessingInstruction, false);

		// Token: 0x04001BEA RID: 7146
		public static readonly XmlQueryType PIS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.PI, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BEB RID: 7147
		public static readonly XmlQueryType DocumentOrElement = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Document, XmlQueryTypeFactory.Element);

		// Token: 0x04001BEC RID: 7148
		public static readonly XmlQueryType DocumentOrElementQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrElement, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x04001BED RID: 7149
		public static readonly XmlQueryType DocumentOrElementS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrElement, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BEE RID: 7150
		public static readonly XmlQueryType Content = XmlQueryTypeFactory.Choice(new XmlQueryType[]
		{
			XmlQueryTypeFactory.Element,
			XmlQueryTypeFactory.Comment,
			XmlQueryTypeFactory.PI,
			XmlQueryTypeFactory.Text
		});

		// Token: 0x04001BEF RID: 7151
		public static readonly XmlQueryType ContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.Content, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BF0 RID: 7152
		public static readonly XmlQueryType DocumentOrContent = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Document, XmlQueryTypeFactory.Content);

		// Token: 0x04001BF1 RID: 7153
		public static readonly XmlQueryType DocumentOrContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.DocumentOrContent, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BF2 RID: 7154
		public static readonly XmlQueryType AttributeOrContent = XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.Attribute, XmlQueryTypeFactory.Content);

		// Token: 0x04001BF3 RID: 7155
		public static readonly XmlQueryType AttributeOrContentS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.AttributeOrContent, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BF4 RID: 7156
		public static readonly XmlQueryType AnyAtomicType = XmlQueryTypeFactory.Type(XmlTypeCode.AnyAtomicType, false);

		// Token: 0x04001BF5 RID: 7157
		public static readonly XmlQueryType AnyAtomicTypeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.AnyAtomicType, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BF6 RID: 7158
		public static readonly XmlQueryType String = XmlQueryTypeFactory.Type(XmlTypeCode.String, false);

		// Token: 0x04001BF7 RID: 7159
		public static readonly XmlQueryType StringX = XmlQueryTypeFactory.Type(XmlTypeCode.String, true);

		// Token: 0x04001BF8 RID: 7160
		public static readonly XmlQueryType StringXS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.StringX, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BF9 RID: 7161
		public static readonly XmlQueryType Boolean = XmlQueryTypeFactory.Type(XmlTypeCode.Boolean, false);

		// Token: 0x04001BFA RID: 7162
		public static readonly XmlQueryType BooleanX = XmlQueryTypeFactory.Type(XmlTypeCode.Boolean, true);

		// Token: 0x04001BFB RID: 7163
		public static readonly XmlQueryType Int = XmlQueryTypeFactory.Type(XmlTypeCode.Int, false);

		// Token: 0x04001BFC RID: 7164
		public static readonly XmlQueryType IntX = XmlQueryTypeFactory.Type(XmlTypeCode.Int, true);

		// Token: 0x04001BFD RID: 7165
		public static readonly XmlQueryType IntXS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.IntX, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001BFE RID: 7166
		public static readonly XmlQueryType IntegerX = XmlQueryTypeFactory.Type(XmlTypeCode.Integer, true);

		// Token: 0x04001BFF RID: 7167
		public static readonly XmlQueryType LongX = XmlQueryTypeFactory.Type(XmlTypeCode.Long, true);

		// Token: 0x04001C00 RID: 7168
		public static readonly XmlQueryType DecimalX = XmlQueryTypeFactory.Type(XmlTypeCode.Decimal, true);

		// Token: 0x04001C01 RID: 7169
		public static readonly XmlQueryType FloatX = XmlQueryTypeFactory.Type(XmlTypeCode.Float, true);

		// Token: 0x04001C02 RID: 7170
		public static readonly XmlQueryType Double = XmlQueryTypeFactory.Type(XmlTypeCode.Double, false);

		// Token: 0x04001C03 RID: 7171
		public static readonly XmlQueryType DoubleX = XmlQueryTypeFactory.Type(XmlTypeCode.Double, true);

		// Token: 0x04001C04 RID: 7172
		public static readonly XmlQueryType DateTimeX = XmlQueryTypeFactory.Type(XmlTypeCode.DateTime, true);

		// Token: 0x04001C05 RID: 7173
		public static readonly XmlQueryType QNameX = XmlQueryTypeFactory.Type(XmlTypeCode.QName, true);

		// Token: 0x04001C06 RID: 7174
		public static readonly XmlQueryType UntypedDocument = XmlQueryTypeFactory.ItemType.UntypedDocument;

		// Token: 0x04001C07 RID: 7175
		public static readonly XmlQueryType UntypedElement = XmlQueryTypeFactory.ItemType.UntypedElement;

		// Token: 0x04001C08 RID: 7176
		public static readonly XmlQueryType UntypedAttribute = XmlQueryTypeFactory.ItemType.UntypedAttribute;

		// Token: 0x04001C09 RID: 7177
		public static readonly XmlQueryType UntypedNode = XmlQueryTypeFactory.Choice(new XmlQueryType[]
		{
			XmlQueryTypeFactory.UntypedDocument,
			XmlQueryTypeFactory.UntypedElement,
			XmlQueryTypeFactory.UntypedAttribute,
			XmlQueryTypeFactory.Namespace,
			XmlQueryTypeFactory.Text,
			XmlQueryTypeFactory.Comment,
			XmlQueryTypeFactory.PI
		});

		// Token: 0x04001C0A RID: 7178
		public static readonly XmlQueryType UntypedNodeS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.UntypedNode, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001C0B RID: 7179
		public static readonly XmlQueryType NodeNotRtf = XmlQueryTypeFactory.ItemType.NodeNotRtf;

		// Token: 0x04001C0C RID: 7180
		public static readonly XmlQueryType NodeNotRtfQ = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrOne);

		// Token: 0x04001C0D RID: 7181
		public static readonly XmlQueryType NodeNotRtfS = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001C0E RID: 7182
		public static readonly XmlQueryType NodeSDod = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeNotRtf, XmlQueryCardinality.ZeroOrMore);

		// Token: 0x04001C0F RID: 7183
		private static readonly XmlTypeCode[] NodeKindToTypeCode = new XmlTypeCode[]
		{
			XmlTypeCode.Document,
			XmlTypeCode.Element,
			XmlTypeCode.Attribute,
			XmlTypeCode.Namespace,
			XmlTypeCode.Text,
			XmlTypeCode.Text,
			XmlTypeCode.Text,
			XmlTypeCode.ProcessingInstruction,
			XmlTypeCode.Comment,
			XmlTypeCode.Node
		};

		// Token: 0x0200033C RID: 828
		private sealed class ItemType : XmlQueryType
		{
			// Token: 0x06002215 RID: 8725 RVA: 0x000D883C File Offset: 0x000D6A3C
			static ItemType()
			{
				int num = 55;
				XmlQueryTypeFactory.ItemType.BuiltInItemTypes = new XmlQueryType[num];
				XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict = new XmlQueryType[num];
				for (int i = 0; i < num; i++)
				{
					XmlTypeCode typeCode = (XmlTypeCode)i;
					switch (i)
					{
					case 0:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = XmlQueryTypeFactory.ChoiceType.None;
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ChoiceType.None;
						break;
					case 1:
					case 2:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, false);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 3:
					case 4:
					case 6:
					case 7:
					case 8:
					case 9:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 5:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.AnySimpleType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 10:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.AnyAtomicType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					case 11:
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.UntypedAtomicType, false, true, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i];
						break;
					default:
					{
						XmlSchemaType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(typeCode);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypes[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, builtInSimpleType, false, false, true);
						XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[i] = new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, builtInSimpleType, false, true, true);
						break;
					}
					}
				}
				XmlQueryTypeFactory.ItemType.UntypedDocument = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Document, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.UntypedAnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.UntypedElement = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Element, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.UntypedAnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.UntypedAttribute = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Attribute, XmlQualifiedNameTest.Wildcard, DatatypeImplementation.UntypedAtomicType, false, false, true);
				XmlQueryTypeFactory.ItemType.NodeNotRtf = new XmlQueryTypeFactory.ItemType(XmlTypeCode.Node, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false, false, true);
				XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes = new XmlQueryType[]
				{
					XmlQueryTypeFactory.ItemType.UntypedDocument,
					XmlQueryTypeFactory.ItemType.UntypedElement,
					XmlQueryTypeFactory.ItemType.UntypedAttribute,
					XmlQueryTypeFactory.ItemType.NodeNotRtf
				};
			}

			// Token: 0x06002216 RID: 8726 RVA: 0x000D8A6B File Offset: 0x000D6C6B
			public static XmlQueryType Create(XmlTypeCode code, bool isStrict)
			{
				if (isStrict)
				{
					return XmlQueryTypeFactory.ItemType.BuiltInItemTypesStrict[(int)code];
				}
				return XmlQueryTypeFactory.ItemType.BuiltInItemTypes[(int)code];
			}

			// Token: 0x06002217 RID: 8727 RVA: 0x000D8A80 File Offset: 0x000D6C80
			public static XmlQueryType Create(XmlSchemaSimpleType schemaType, bool isStrict)
			{
				XmlTypeCode typeCode = schemaType.Datatype.TypeCode;
				if (schemaType == XmlSchemaType.GetBuiltInSimpleType(typeCode))
				{
					return XmlQueryTypeFactory.ItemType.Create(typeCode, isStrict);
				}
				return new XmlQueryTypeFactory.ItemType(typeCode, XmlQualifiedNameTest.Wildcard, schemaType, false, isStrict, true);
			}

			// Token: 0x06002218 RID: 8728 RVA: 0x000D8ABC File Offset: 0x000D6CBC
			public static XmlQueryType Create(XmlTypeCode code, XmlQualifiedNameTest nameTest, XmlSchemaType contentType, bool isNillable)
			{
				if (code - XmlTypeCode.Document <= 1)
				{
					if (nameTest.IsWildcard)
					{
						if (contentType == XmlSchemaComplexType.AnyType)
						{
							return XmlQueryTypeFactory.ItemType.Create(code, false);
						}
						if (contentType == XmlSchemaComplexType.UntypedAnyType)
						{
							if (code == XmlTypeCode.Element)
							{
								return XmlQueryTypeFactory.ItemType.UntypedElement;
							}
							if (code == XmlTypeCode.Document)
							{
								return XmlQueryTypeFactory.ItemType.UntypedDocument;
							}
						}
					}
					return new XmlQueryTypeFactory.ItemType(code, nameTest, contentType, isNillable, false, true);
				}
				if (code != XmlTypeCode.Attribute)
				{
					return XmlQueryTypeFactory.ItemType.Create(code, false);
				}
				if (nameTest.IsWildcard)
				{
					if (contentType == DatatypeImplementation.AnySimpleType)
					{
						return XmlQueryTypeFactory.ItemType.Create(code, false);
					}
					if (contentType == DatatypeImplementation.UntypedAtomicType)
					{
						return XmlQueryTypeFactory.ItemType.UntypedAttribute;
					}
				}
				return new XmlQueryTypeFactory.ItemType(code, nameTest, contentType, isNillable, false, true);
			}

			// Token: 0x06002219 RID: 8729 RVA: 0x000D8B50 File Offset: 0x000D6D50
			private ItemType(XmlTypeCode code, XmlQualifiedNameTest nameTest, XmlSchemaType schemaType, bool isNillable, bool isStrict, bool isNotRtf)
			{
				this.code = code;
				this.nameTest = nameTest;
				this.schemaType = schemaType;
				this.isNillable = isNillable;
				this.isStrict = isStrict;
				this.isNotRtf = isNotRtf;
				switch (code)
				{
				case XmlTypeCode.Item:
					this.nodeKinds = XmlNodeKindFlags.Any;
					return;
				case XmlTypeCode.Node:
					this.nodeKinds = XmlNodeKindFlags.Any;
					return;
				case XmlTypeCode.Document:
					this.nodeKinds = XmlNodeKindFlags.Document;
					return;
				case XmlTypeCode.Element:
					this.nodeKinds = XmlNodeKindFlags.Element;
					return;
				case XmlTypeCode.Attribute:
					this.nodeKinds = XmlNodeKindFlags.Attribute;
					return;
				case XmlTypeCode.Namespace:
					this.nodeKinds = XmlNodeKindFlags.Namespace;
					return;
				case XmlTypeCode.ProcessingInstruction:
					this.nodeKinds = XmlNodeKindFlags.PI;
					return;
				case XmlTypeCode.Comment:
					this.nodeKinds = XmlNodeKindFlags.Comment;
					return;
				case XmlTypeCode.Text:
					this.nodeKinds = XmlNodeKindFlags.Text;
					return;
				default:
					this.nodeKinds = XmlNodeKindFlags.None;
					return;
				}
			}

			// Token: 0x0600221A RID: 8730 RVA: 0x000D8C14 File Offset: 0x000D6E14
			public override void GetObjectData(BinaryWriter writer)
			{
				sbyte b = (sbyte)this.code;
				for (int i = 0; i < XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes.Length; i++)
				{
					if (this == XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes[i])
					{
						b = (sbyte)(~(sbyte)i);
						break;
					}
				}
				writer.Write(b);
				if (0 <= b)
				{
					writer.Write(this.isStrict);
				}
			}

			// Token: 0x0600221B RID: 8731 RVA: 0x000D8C64 File Offset: 0x000D6E64
			public static XmlQueryType Create(BinaryReader reader)
			{
				sbyte b = reader.ReadSByte();
				if (0 <= b)
				{
					return XmlQueryTypeFactory.ItemType.Create((XmlTypeCode)b, reader.ReadBoolean());
				}
				return XmlQueryTypeFactory.ItemType.SpecialBuiltInItemTypes[(int)(~(int)b)];
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x0600221C RID: 8732 RVA: 0x000D8C91 File Offset: 0x000D6E91
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x0600221D RID: 8733 RVA: 0x000D8C99 File Offset: 0x000D6E99
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return this.nameTest;
				}
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x0600221E RID: 8734 RVA: 0x000D8CA1 File Offset: 0x000D6EA1
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.schemaType;
				}
			}

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x0600221F RID: 8735 RVA: 0x000D8CA9 File Offset: 0x000D6EA9
			public override bool IsNillable
			{
				get
				{
					return this.isNillable;
				}
			}

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06002220 RID: 8736 RVA: 0x000D8CB1 File Offset: 0x000D6EB1
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.nodeKinds;
				}
			}

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x06002221 RID: 8737 RVA: 0x000D8CB9 File Offset: 0x000D6EB9
			public override bool IsStrict
			{
				get
				{
					return this.isStrict;
				}
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x06002222 RID: 8738 RVA: 0x000D8CC1 File Offset: 0x000D6EC1
			public override bool IsNotRtf
			{
				get
				{
					return this.isNotRtf;
				}
			}

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x06002223 RID: 8739 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool IsDod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x06002224 RID: 8740 RVA: 0x000D8CC9 File Offset: 0x000D6EC9
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					return XmlQueryCardinality.One;
				}
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x06002225 RID: 8741 RVA: 0x00002068 File Offset: 0x00000268
			public override XmlQueryType Prime
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x06002226 RID: 8742 RVA: 0x000D8CD0 File Offset: 0x000D6ED0
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (base.IsAtomicValue)
					{
						return this.SchemaType.ValueConverter;
					}
					if (base.IsNode)
					{
						return XmlNodeConverter.Node;
					}
					return XmlAnyConverter.Item;
				}
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x06002227 RID: 8743 RVA: 0x0001222F File Offset: 0x0001042F
			public override int Count
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x170006BD RID: 1725
			public override XmlQueryType this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x04001C10 RID: 7184
			public static readonly XmlQueryType UntypedDocument;

			// Token: 0x04001C11 RID: 7185
			public static readonly XmlQueryType UntypedElement;

			// Token: 0x04001C12 RID: 7186
			public static readonly XmlQueryType UntypedAttribute;

			// Token: 0x04001C13 RID: 7187
			public static readonly XmlQueryType NodeNotRtf;

			// Token: 0x04001C14 RID: 7188
			private static XmlQueryType[] BuiltInItemTypes;

			// Token: 0x04001C15 RID: 7189
			private static XmlQueryType[] BuiltInItemTypesStrict;

			// Token: 0x04001C16 RID: 7190
			private static XmlQueryType[] SpecialBuiltInItemTypes;

			// Token: 0x04001C17 RID: 7191
			private XmlTypeCode code;

			// Token: 0x04001C18 RID: 7192
			private XmlQualifiedNameTest nameTest;

			// Token: 0x04001C19 RID: 7193
			private XmlSchemaType schemaType;

			// Token: 0x04001C1A RID: 7194
			private bool isNillable;

			// Token: 0x04001C1B RID: 7195
			private XmlNodeKindFlags nodeKinds;

			// Token: 0x04001C1C RID: 7196
			private bool isStrict;

			// Token: 0x04001C1D RID: 7197
			private bool isNotRtf;
		}

		// Token: 0x0200033D RID: 829
		private sealed class ChoiceType : XmlQueryType
		{
			// Token: 0x0600222A RID: 8746 RVA: 0x000D8D08 File Offset: 0x000D6F08
			public static XmlQueryType Create(XmlNodeKindFlags nodeKinds)
			{
				if (Bits.ExactlyOne((uint)nodeKinds))
				{
					return XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.ChoiceType.NodeKindToTypeCode[Bits.LeastPosition((uint)nodeKinds)], false);
				}
				List<XmlQueryType> list = new List<XmlQueryType>();
				while (nodeKinds != XmlNodeKindFlags.None)
				{
					list.Add(XmlQueryTypeFactory.ItemType.Create(XmlQueryTypeFactory.ChoiceType.NodeKindToTypeCode[Bits.LeastPosition((uint)nodeKinds)], false));
					nodeKinds = (XmlNodeKindFlags)Bits.ClearLeast((uint)nodeKinds);
				}
				return XmlQueryTypeFactory.ChoiceType.Create(list);
			}

			// Token: 0x0600222B RID: 8747 RVA: 0x000D8D61 File Offset: 0x000D6F61
			public static XmlQueryType Create(List<XmlQueryType> members)
			{
				if (members.Count == 0)
				{
					return XmlQueryTypeFactory.ChoiceType.None;
				}
				if (members.Count == 1)
				{
					return members[0];
				}
				return new XmlQueryTypeFactory.ChoiceType(members);
			}

			// Token: 0x0600222C RID: 8748 RVA: 0x000D8D88 File Offset: 0x000D6F88
			private ChoiceType(List<XmlQueryType> members)
			{
				this.members = members;
				for (int i = 0; i < members.Count; i++)
				{
					XmlQueryType xmlQueryType = members[i];
					if (this.code == XmlTypeCode.None)
					{
						this.code = xmlQueryType.TypeCode;
						this.schemaType = xmlQueryType.SchemaType;
					}
					else if (base.IsNode && xmlQueryType.IsNode)
					{
						if (this.code == xmlQueryType.TypeCode)
						{
							if (this.code == XmlTypeCode.Element)
							{
								this.schemaType = XmlSchemaComplexType.AnyType;
							}
							else if (this.code == XmlTypeCode.Attribute)
							{
								this.schemaType = DatatypeImplementation.AnySimpleType;
							}
						}
						else
						{
							this.code = XmlTypeCode.Node;
							this.schemaType = null;
						}
					}
					else if (base.IsAtomicValue && xmlQueryType.IsAtomicValue)
					{
						this.code = XmlTypeCode.AnyAtomicType;
						this.schemaType = DatatypeImplementation.AnyAtomicType;
					}
					else
					{
						this.code = XmlTypeCode.Item;
						this.schemaType = null;
					}
					this.nodeKinds |= xmlQueryType.NodeKinds;
				}
			}

			// Token: 0x0600222D RID: 8749 RVA: 0x000D8E88 File Offset: 0x000D7088
			public override void GetObjectData(BinaryWriter writer)
			{
				writer.Write(this.members.Count);
				for (int i = 0; i < this.members.Count; i++)
				{
					XmlQueryTypeFactory.Serialize(writer, this.members[i]);
				}
			}

			// Token: 0x0600222E RID: 8750 RVA: 0x000D8ED0 File Offset: 0x000D70D0
			public static XmlQueryType Create(BinaryReader reader)
			{
				int num = reader.ReadInt32();
				List<XmlQueryType> list = new List<XmlQueryType>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(XmlQueryTypeFactory.Deserialize(reader));
				}
				return XmlQueryTypeFactory.ChoiceType.Create(list);
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x0600222F RID: 8751 RVA: 0x000D8F09 File Offset: 0x000D7109
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06002230 RID: 8752 RVA: 0x000D8F11 File Offset: 0x000D7111
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return XmlQualifiedNameTest.Wildcard;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06002231 RID: 8753 RVA: 0x000D8F18 File Offset: 0x000D7118
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.schemaType;
				}
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x06002232 RID: 8754 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool IsNillable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x06002233 RID: 8755 RVA: 0x000D8F20 File Offset: 0x000D7120
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.nodeKinds;
				}
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x06002234 RID: 8756 RVA: 0x000D8F28 File Offset: 0x000D7128
			public override bool IsStrict
			{
				get
				{
					return this.members.Count == 0;
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06002235 RID: 8757 RVA: 0x000D8F38 File Offset: 0x000D7138
			public override bool IsNotRtf
			{
				get
				{
					for (int i = 0; i < this.members.Count; i++)
					{
						if (!this.members[i].IsNotRtf)
						{
							return false;
						}
					}
					return true;
				}
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06002236 RID: 8758 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool IsDod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x06002237 RID: 8759 RVA: 0x000D8F71 File Offset: 0x000D7171
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					if (this.TypeCode != XmlTypeCode.None)
					{
						return XmlQueryCardinality.One;
					}
					return XmlQueryCardinality.None;
				}
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06002238 RID: 8760 RVA: 0x00002068 File Offset: 0x00000268
			public override XmlQueryType Prime
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x06002239 RID: 8761 RVA: 0x000D8F86 File Offset: 0x000D7186
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (this.code == XmlTypeCode.None || this.code == XmlTypeCode.Item)
					{
						return XmlAnyConverter.Item;
					}
					if (base.IsAtomicValue)
					{
						return this.SchemaType.ValueConverter;
					}
					return XmlNodeConverter.Node;
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x0600223A RID: 8762 RVA: 0x000D8FB8 File Offset: 0x000D71B8
			public override int Count
			{
				get
				{
					return this.members.Count;
				}
			}

			// Token: 0x170006CA RID: 1738
			public override XmlQueryType this[int index]
			{
				get
				{
					return this.members[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600223D RID: 8765 RVA: 0x000D8FD3 File Offset: 0x000D71D3
			// Note: this type is marked as 'beforefieldinit'.
			static ChoiceType()
			{
			}

			// Token: 0x04001C1E RID: 7198
			public static readonly XmlQueryType None = new XmlQueryTypeFactory.ChoiceType(new List<XmlQueryType>());

			// Token: 0x04001C1F RID: 7199
			private XmlTypeCode code;

			// Token: 0x04001C20 RID: 7200
			private XmlSchemaType schemaType;

			// Token: 0x04001C21 RID: 7201
			private XmlNodeKindFlags nodeKinds;

			// Token: 0x04001C22 RID: 7202
			private List<XmlQueryType> members;

			// Token: 0x04001C23 RID: 7203
			private static readonly XmlTypeCode[] NodeKindToTypeCode = new XmlTypeCode[]
			{
				XmlTypeCode.None,
				XmlTypeCode.Document,
				XmlTypeCode.Element,
				XmlTypeCode.Attribute,
				XmlTypeCode.Text,
				XmlTypeCode.Comment,
				XmlTypeCode.ProcessingInstruction,
				XmlTypeCode.Namespace
			};
		}

		// Token: 0x0200033E RID: 830
		private sealed class SequenceType : XmlQueryType
		{
			// Token: 0x0600223E RID: 8766 RVA: 0x000D8FFC File Offset: 0x000D71FC
			public static XmlQueryType Create(XmlQueryType prime, XmlQueryCardinality card)
			{
				if (prime.TypeCode == XmlTypeCode.None)
				{
					if (!(XmlQueryCardinality.Zero <= card))
					{
						return XmlQueryTypeFactory.None;
					}
					return XmlQueryTypeFactory.SequenceType.Zero;
				}
				else
				{
					if (card == XmlQueryCardinality.None)
					{
						return XmlQueryTypeFactory.None;
					}
					if (card == XmlQueryCardinality.Zero)
					{
						return XmlQueryTypeFactory.SequenceType.Zero;
					}
					if (card == XmlQueryCardinality.One)
					{
						return prime;
					}
					return new XmlQueryTypeFactory.SequenceType(prime, card);
				}
			}

			// Token: 0x0600223F RID: 8767 RVA: 0x000D9066 File Offset: 0x000D7266
			private SequenceType(XmlQueryType prime, XmlQueryCardinality card)
			{
				this.prime = prime;
				this.card = card;
			}

			// Token: 0x06002240 RID: 8768 RVA: 0x000D907C File Offset: 0x000D727C
			public override void GetObjectData(BinaryWriter writer)
			{
				writer.Write(this.IsDod);
				if (this.IsDod)
				{
					return;
				}
				XmlQueryTypeFactory.Serialize(writer, this.prime);
				this.card.GetObjectData(writer);
			}

			// Token: 0x06002241 RID: 8769 RVA: 0x000D90AC File Offset: 0x000D72AC
			public static XmlQueryType Create(BinaryReader reader)
			{
				if (reader.ReadBoolean())
				{
					return XmlQueryTypeFactory.NodeSDod;
				}
				XmlQueryType xmlQueryType = XmlQueryTypeFactory.Deserialize(reader);
				XmlQueryCardinality xmlQueryCardinality = new XmlQueryCardinality(reader);
				return XmlQueryTypeFactory.SequenceType.Create(xmlQueryType, xmlQueryCardinality);
			}

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06002242 RID: 8770 RVA: 0x000D90DB File Offset: 0x000D72DB
			public override XmlTypeCode TypeCode
			{
				get
				{
					return this.prime.TypeCode;
				}
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06002243 RID: 8771 RVA: 0x000D90E8 File Offset: 0x000D72E8
			public override XmlQualifiedNameTest NameTest
			{
				get
				{
					return this.prime.NameTest;
				}
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06002244 RID: 8772 RVA: 0x000D90F5 File Offset: 0x000D72F5
			public override XmlSchemaType SchemaType
			{
				get
				{
					return this.prime.SchemaType;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06002245 RID: 8773 RVA: 0x000D9102 File Offset: 0x000D7302
			public override bool IsNillable
			{
				get
				{
					return this.prime.IsNillable;
				}
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06002246 RID: 8774 RVA: 0x000D910F File Offset: 0x000D730F
			public override XmlNodeKindFlags NodeKinds
			{
				get
				{
					return this.prime.NodeKinds;
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06002247 RID: 8775 RVA: 0x000D911C File Offset: 0x000D731C
			public override bool IsStrict
			{
				get
				{
					return this.prime.IsStrict;
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06002248 RID: 8776 RVA: 0x000D9129 File Offset: 0x000D7329
			public override bool IsNotRtf
			{
				get
				{
					return this.prime.IsNotRtf;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06002249 RID: 8777 RVA: 0x000D9136 File Offset: 0x000D7336
			public override bool IsDod
			{
				get
				{
					return this == XmlQueryTypeFactory.NodeSDod;
				}
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x0600224A RID: 8778 RVA: 0x000D9140 File Offset: 0x000D7340
			public override XmlQueryCardinality Cardinality
			{
				get
				{
					return this.card;
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x0600224B RID: 8779 RVA: 0x000D9148 File Offset: 0x000D7348
			public override XmlQueryType Prime
			{
				get
				{
					return this.prime;
				}
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x0600224C RID: 8780 RVA: 0x000D9150 File Offset: 0x000D7350
			public override XmlValueConverter ClrMapping
			{
				get
				{
					if (this.converter == null)
					{
						this.converter = XmlListConverter.Create(this.prime.ClrMapping);
					}
					return this.converter;
				}
			}

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x0600224D RID: 8781 RVA: 0x000D9176 File Offset: 0x000D7376
			public override int Count
			{
				get
				{
					return this.prime.Count;
				}
			}

			// Token: 0x170006D7 RID: 1751
			public override XmlQueryType this[int index]
			{
				get
				{
					return this.prime[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06002250 RID: 8784 RVA: 0x000D9191 File Offset: 0x000D7391
			// Note: this type is marked as 'beforefieldinit'.
			static SequenceType()
			{
			}

			// Token: 0x04001C24 RID: 7204
			public static readonly XmlQueryType Zero = new XmlQueryTypeFactory.SequenceType(XmlQueryTypeFactory.ChoiceType.None, XmlQueryCardinality.Zero);

			// Token: 0x04001C25 RID: 7205
			private XmlQueryType prime;

			// Token: 0x04001C26 RID: 7206
			private XmlQueryCardinality card;

			// Token: 0x04001C27 RID: 7207
			private XmlValueConverter converter;
		}
	}
}
