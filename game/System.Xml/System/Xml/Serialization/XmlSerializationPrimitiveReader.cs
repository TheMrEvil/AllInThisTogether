using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002A3 RID: 675
	internal class XmlSerializationPrimitiveReader : XmlSerializationReader
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x00090D24 File Offset: 0x0008EF24
		internal object Read_string()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_string || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.Reader.ReadElementString();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00090D9C File Offset: 0x0008EF9C
		internal object Read_int()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id3_int || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00090E14 File Offset: 0x0008F014
		internal object Read_boolean()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id4_boolean || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToBoolean(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00090E8C File Offset: 0x0008F08C
		internal object Read_short()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id5_short || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00090F04 File Offset: 0x0008F104
		internal object Read_long()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id6_long || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00090F7C File Offset: 0x0008F17C
		internal object Read_float()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id7_float || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToSingle(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00090FF4 File Offset: 0x0008F1F4
		internal object Read_double()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id8_double || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToDouble(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0009106C File Offset: 0x0008F26C
		internal object Read_decimal()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id9_decimal || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToDecimal(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000910E4 File Offset: 0x0008F2E4
		internal object Read_dateTime()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id10_dateTime || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlSerializationReader.ToDateTime(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0009115C File Offset: 0x0008F35C
		internal object Read_unsignedByte()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id11_unsignedByte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000911D4 File Offset: 0x0008F3D4
		internal object Read_byte()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id12_byte || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToSByte(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0009124C File Offset: 0x0008F44C
		internal object Read_unsignedShort()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id13_unsignedShort || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt16(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000912C4 File Offset: 0x0008F4C4
		internal object Read_unsignedInt()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id14_unsignedInt || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt32(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0009133C File Offset: 0x0008F53C
		internal object Read_unsignedLong()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id15_unsignedLong || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToUInt64(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000913B4 File Offset: 0x0008F5B4
		internal object Read_base64Binary()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id16_base64Binary || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.ToByteArrayBase64(false);
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00091428 File Offset: 0x0008F628
		internal object Read_guid()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id17_guid || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlConvert.ToGuid(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000914A0 File Offset: 0x0008F6A0
		internal object Read_TimeSpan()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id19_TimeSpan || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.Reader.IsEmptyElement)
				{
					base.Reader.Skip();
					result = default(TimeSpan);
				}
				else
				{
					result = XmlConvert.ToTimeSpan(base.Reader.ReadElementString());
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00091540 File Offset: 0x0008F740
		internal object Read_char()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id18_char || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = XmlSerializationReader.ToChar(base.Reader.ReadElementString());
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000915B8 File Offset: 0x0008F7B8
		internal object Read_QName()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_QName || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				if (base.ReadNull())
				{
					result = null;
				}
				else
				{
					result = base.ReadElementQualifiedName();
				}
			}
			else
			{
				base.UnknownNode(null);
			}
			return result;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0000B528 File Offset: 0x00009728
		protected override void InitCallbacks()
		{
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0009162C File Offset: 0x0008F82C
		protected override void InitIDs()
		{
			this.id4_boolean = base.Reader.NameTable.Add("boolean");
			this.id14_unsignedInt = base.Reader.NameTable.Add("unsignedInt");
			this.id15_unsignedLong = base.Reader.NameTable.Add("unsignedLong");
			this.id7_float = base.Reader.NameTable.Add("float");
			this.id10_dateTime = base.Reader.NameTable.Add("dateTime");
			this.id6_long = base.Reader.NameTable.Add("long");
			this.id9_decimal = base.Reader.NameTable.Add("decimal");
			this.id8_double = base.Reader.NameTable.Add("double");
			this.id17_guid = base.Reader.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.id19_TimeSpan = base.Reader.NameTable.Add("TimeSpan");
			}
			this.id2_Item = base.Reader.NameTable.Add("");
			this.id13_unsignedShort = base.Reader.NameTable.Add("unsignedShort");
			this.id18_char = base.Reader.NameTable.Add("char");
			this.id3_int = base.Reader.NameTable.Add("int");
			this.id12_byte = base.Reader.NameTable.Add("byte");
			this.id16_base64Binary = base.Reader.NameTable.Add("base64Binary");
			this.id11_unsignedByte = base.Reader.NameTable.Add("unsignedByte");
			this.id5_short = base.Reader.NameTable.Add("short");
			this.id1_string = base.Reader.NameTable.Add("string");
			this.id1_QName = base.Reader.NameTable.Add("QName");
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0009185C File Offset: 0x0008FA5C
		public XmlSerializationPrimitiveReader()
		{
		}

		// Token: 0x0400191D RID: 6429
		private string id4_boolean;

		// Token: 0x0400191E RID: 6430
		private string id14_unsignedInt;

		// Token: 0x0400191F RID: 6431
		private string id15_unsignedLong;

		// Token: 0x04001920 RID: 6432
		private string id7_float;

		// Token: 0x04001921 RID: 6433
		private string id10_dateTime;

		// Token: 0x04001922 RID: 6434
		private string id6_long;

		// Token: 0x04001923 RID: 6435
		private string id9_decimal;

		// Token: 0x04001924 RID: 6436
		private string id8_double;

		// Token: 0x04001925 RID: 6437
		private string id17_guid;

		// Token: 0x04001926 RID: 6438
		private string id19_TimeSpan;

		// Token: 0x04001927 RID: 6439
		private string id2_Item;

		// Token: 0x04001928 RID: 6440
		private string id13_unsignedShort;

		// Token: 0x04001929 RID: 6441
		private string id18_char;

		// Token: 0x0400192A RID: 6442
		private string id3_int;

		// Token: 0x0400192B RID: 6443
		private string id12_byte;

		// Token: 0x0400192C RID: 6444
		private string id16_base64Binary;

		// Token: 0x0400192D RID: 6445
		private string id11_unsignedByte;

		// Token: 0x0400192E RID: 6446
		private string id5_short;

		// Token: 0x0400192F RID: 6447
		private string id1_string;

		// Token: 0x04001930 RID: 6448
		private string id1_QName;
	}
}
