using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002A2 RID: 674
	internal class XmlSerializationPrimitiveWriter : XmlSerializationWriter
	{
		// Token: 0x0600193B RID: 6459 RVA: 0x000908F1 File Offset: 0x0008EAF1
		internal void Write_string(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("string", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteral("string", "", (string)o);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00090929 File Offset: 0x0008EB29
		internal void Write_int(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("int", "");
				return;
			}
			base.WriteElementStringRaw("int", "", XmlConvert.ToString((int)o));
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00090960 File Offset: 0x0008EB60
		internal void Write_boolean(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("boolean", "");
				return;
			}
			base.WriteElementStringRaw("boolean", "", XmlConvert.ToString((bool)o));
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00090997 File Offset: 0x0008EB97
		internal void Write_short(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("short", "");
				return;
			}
			base.WriteElementStringRaw("short", "", XmlConvert.ToString((short)o));
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000909CE File Offset: 0x0008EBCE
		internal void Write_long(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("long", "");
				return;
			}
			base.WriteElementStringRaw("long", "", XmlConvert.ToString((long)o));
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00090A05 File Offset: 0x0008EC05
		internal void Write_float(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("float", "");
				return;
			}
			base.WriteElementStringRaw("float", "", XmlConvert.ToString((float)o));
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00090A3C File Offset: 0x0008EC3C
		internal void Write_double(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("double", "");
				return;
			}
			base.WriteElementStringRaw("double", "", XmlConvert.ToString((double)o));
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00090A73 File Offset: 0x0008EC73
		internal void Write_decimal(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("decimal", "");
				return;
			}
			base.WriteElementStringRaw("decimal", "", XmlConvert.ToString((decimal)o));
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00090AAA File Offset: 0x0008ECAA
		internal void Write_dateTime(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("dateTime", "");
				return;
			}
			base.WriteElementStringRaw("dateTime", "", XmlSerializationWriter.FromDateTime((DateTime)o));
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00090AE1 File Offset: 0x0008ECE1
		internal void Write_unsignedByte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedByte", "");
				return;
			}
			base.WriteElementStringRaw("unsignedByte", "", XmlConvert.ToString((byte)o));
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00090B18 File Offset: 0x0008ED18
		internal void Write_byte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("byte", "");
				return;
			}
			base.WriteElementStringRaw("byte", "", XmlConvert.ToString((sbyte)o));
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00090B4F File Offset: 0x0008ED4F
		internal void Write_unsignedShort(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedShort", "");
				return;
			}
			base.WriteElementStringRaw("unsignedShort", "", XmlConvert.ToString((ushort)o));
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00090B86 File Offset: 0x0008ED86
		internal void Write_unsignedInt(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedInt", "");
				return;
			}
			base.WriteElementStringRaw("unsignedInt", "", XmlConvert.ToString((uint)o));
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00090BBD File Offset: 0x0008EDBD
		internal void Write_unsignedLong(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedLong", "");
				return;
			}
			base.WriteElementStringRaw("unsignedLong", "", XmlConvert.ToString((ulong)o));
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00090BF4 File Offset: 0x0008EDF4
		internal void Write_base64Binary(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("base64Binary", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteralRaw("base64Binary", "", XmlSerializationWriter.FromByteArrayBase64((byte[])o));
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00090C31 File Offset: 0x0008EE31
		internal void Write_guid(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("guid", "");
				return;
			}
			base.WriteElementStringRaw("guid", "", XmlConvert.ToString((Guid)o));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00090C68 File Offset: 0x0008EE68
		internal void Write_TimeSpan(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("TimeSpan", "");
				return;
			}
			TimeSpan value = (TimeSpan)o;
			base.WriteElementStringRaw("TimeSpan", "", XmlConvert.ToString(value));
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00090CAC File Offset: 0x0008EEAC
		internal void Write_char(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("char", "");
				return;
			}
			base.WriteElementString("char", "", XmlSerializationWriter.FromChar((char)o));
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00090CE3 File Offset: 0x0008EEE3
		internal void Write_QName(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("QName", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableQualifiedNameLiteral("QName", "", (XmlQualifiedName)o);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0000B528 File Offset: 0x00009728
		protected override void InitCallbacks()
		{
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00090D1B File Offset: 0x0008EF1B
		public XmlSerializationPrimitiveWriter()
		{
		}
	}
}
