using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200018B RID: 395
	internal class JsonWriterDelegator : XmlWriterDelegator
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x0004C185 File Offset: 0x0004A385
		public JsonWriterDelegator(XmlWriter writer) : base(writer)
		{
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004C18E File Offset: 0x0004A38E
		public JsonWriterDelegator(XmlWriter writer, DateTimeFormat dateTimeFormat) : this(writer)
		{
			this.dateTimeFormat = dateTimeFormat;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004C19E File Offset: 0x0004A39E
		internal override void WriteChar(char value)
		{
			base.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004C1AC File Offset: 0x0004A3AC
		internal override void WriteBase64(byte[] bytes)
		{
			if (bytes == null)
			{
				return;
			}
			ByteArrayHelperWithString.Instance.WriteArray(base.Writer, bytes, 0, bytes.Length);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004C1C8 File Offset: 0x0004A3C8
		internal override void WriteQName(XmlQualifiedName value)
		{
			if (value != XmlQualifiedName.Empty)
			{
				this.writer.WriteString(value.Name);
				this.writer.WriteString(":");
				this.writer.WriteString(value.Namespace);
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004C214 File Offset: 0x0004A414
		internal override void WriteUnsignedLong(ulong value)
		{
			this.WriteDecimal(value);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004C222 File Offset: 0x0004A422
		internal override void WriteDecimal(decimal value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteDecimal(value);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004C240 File Offset: 0x0004A440
		internal override void WriteDouble(double value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteDouble(value);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004C25E File Offset: 0x0004A45E
		internal override void WriteFloat(float value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteFloat(value);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004C27C File Offset: 0x0004A47C
		internal override void WriteLong(long value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteLong(value);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004C29A File Offset: 0x0004A49A
		internal override void WriteSignedByte(sbyte value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteSignedByte(value);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
		internal override void WriteUnsignedInt(uint value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteUnsignedInt(value);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004C2D6 File Offset: 0x0004A4D6
		internal override void WriteUnsignedShort(ushort value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteUnsignedShort(value);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004C2F4 File Offset: 0x0004A4F4
		internal override void WriteUnsignedByte(byte value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteUnsignedByte(value);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004C312 File Offset: 0x0004A512
		internal override void WriteShort(short value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteShort(value);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004C330 File Offset: 0x0004A530
		internal override void WriteBoolean(bool value)
		{
			this.writer.WriteAttributeString("type", "boolean");
			base.WriteBoolean(value);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004C34E File Offset: 0x0004A54E
		internal override void WriteInt(int value)
		{
			this.writer.WriteAttributeString("type", "number");
			base.WriteInt(value);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004C36C File Offset: 0x0004A56C
		internal void WriteJsonBooleanArray(bool[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteBoolean(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004C394 File Offset: 0x0004A594
		internal void WriteJsonDateTimeArray(DateTime[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteDateTime(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		internal void WriteJsonDecimalArray(decimal[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteDecimal(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004C3EC File Offset: 0x0004A5EC
		internal void WriteJsonInt32Array(int[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteInt(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004C414 File Offset: 0x0004A614
		internal void WriteJsonInt64Array(long[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteLong(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004C43A File Offset: 0x0004A63A
		internal override void WriteDateTime(DateTime value)
		{
			if (this.dateTimeFormat == null)
			{
				this.WriteDateTimeInDefaultFormat(value);
				return;
			}
			this.writer.WriteString(value.ToString(this.dateTimeFormat.FormatString, this.dateTimeFormat.FormatProvider));
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004C474 File Offset: 0x0004A674
		private void WriteDateTimeInDefaultFormat(DateTime value)
		{
			if (value.Kind != DateTimeKind.Utc)
			{
				long num;
				if (!LocalAppContextSwitches.DoNotUseTimeZoneInfo)
				{
					num = value.Ticks - TimeZoneInfo.Local.GetUtcOffset(value).Ticks;
				}
				else
				{
					num = value.Ticks - TimeZone.CurrentTimeZone.GetUtcOffset(value).Ticks;
				}
				if (num > DateTime.MaxValue.Ticks || num < DateTime.MinValue.Ticks)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("JSON DateTime is out of range."), new ArgumentOutOfRangeException("value")));
				}
			}
			this.writer.WriteString("/Date(");
			this.writer.WriteValue((value.ToUniversalTime().Ticks - JsonGlobals.unixEpochTicks) / 10000L);
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset;
				if (!LocalAppContextSwitches.DoNotUseTimeZoneInfo)
				{
					utcOffset = TimeZoneInfo.Local.GetUtcOffset(value.ToLocalTime());
				}
				else
				{
					utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(value.ToLocalTime());
				}
				if (utcOffset.Ticks < 0L)
				{
					this.writer.WriteString("-");
				}
				else
				{
					this.writer.WriteString("+");
				}
				int num2 = Math.Abs(utcOffset.Hours);
				this.writer.WriteString((num2 < 10) ? ("0" + num2.ToString()) : num2.ToString(CultureInfo.InvariantCulture));
				int num3 = Math.Abs(utcOffset.Minutes);
				this.writer.WriteString((num3 < 10) ? ("0" + num3.ToString()) : num3.ToString(CultureInfo.InvariantCulture));
				break;
			}
			}
			this.writer.WriteString(")/");
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004C644 File Offset: 0x0004A844
		internal void WriteJsonSingleArray(float[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteFloat(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004C66C File Offset: 0x0004A86C
		internal void WriteJsonDoubleArray(double[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
		{
			for (int i = 0; i < value.Length; i++)
			{
				base.WriteDouble(value[i], itemName, itemNamespace);
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004C692 File Offset: 0x0004A892
		internal override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (localName != null && localName.Length == 0)
			{
				base.WriteStartElement("item", "item");
				base.WriteAttributeString(null, "item", null, localName);
				return;
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x040009FC RID: 2556
		private DateTimeFormat dateTimeFormat;
	}
}
