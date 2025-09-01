using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000186 RID: 390
	internal class JsonReaderDelegator : XmlReaderDelegator
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x0004BA5B File Offset: 0x00049C5B
		public JsonReaderDelegator(XmlReader reader) : base(reader)
		{
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004BA64 File Offset: 0x00049C64
		public JsonReaderDelegator(XmlReader reader, DateTimeFormat dateTimeFormat) : this(reader)
		{
			this.dateTimeFormat = dateTimeFormat;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0004BA74 File Offset: 0x00049C74
		internal XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				if (this.dictionaryReader == null)
				{
					return null;
				}
				return this.dictionaryReader.Quotas;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0004BA8B File Offset: 0x00049C8B
		private JsonReaderDelegator.DateTimeArrayJsonHelperWithString DateTimeArrayHelper
		{
			get
			{
				if (this.dateTimeArrayHelper == null)
				{
					this.dateTimeArrayHelper = new JsonReaderDelegator.DateTimeArrayJsonHelperWithString(this.dateTimeFormat);
				}
				return this.dateTimeArrayHelper;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004BAAC File Offset: 0x00049CAC
		internal static XmlQualifiedName ParseQualifiedName(string qname)
		{
			string name;
			string ns;
			if (string.IsNullOrEmpty(qname))
			{
				ns = (name = string.Empty);
			}
			else
			{
				qname = qname.Trim();
				int num = qname.IndexOf(':');
				if (num >= 0)
				{
					name = qname.Substring(0, num);
					ns = qname.Substring(num + 1);
				}
				else
				{
					name = qname;
					ns = string.Empty;
				}
			}
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004BB04 File Offset: 0x00049D04
		internal override char ReadContentAsChar()
		{
			return XmlConvert.ToChar(base.ReadContentAsString());
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004BB11 File Offset: 0x00049D11
		internal override XmlQualifiedName ReadContentAsQName()
		{
			return JsonReaderDelegator.ParseQualifiedName(base.ReadContentAsString());
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004BB1E File Offset: 0x00049D1E
		internal override char ReadElementContentAsChar()
		{
			return XmlConvert.ToChar(base.ReadElementContentAsString());
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004BB2C File Offset: 0x00049D2C
		internal override byte[] ReadContentAsBase64()
		{
			if (this.isEndOfEmptyElement)
			{
				return new byte[0];
			}
			byte[] result;
			if (this.dictionaryReader == null)
			{
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(this.reader);
				result = ByteArrayHelperWithString.Instance.ReadArray(xmlDictionaryReader, "item", string.Empty, xmlDictionaryReader.Quotas.MaxArrayLength);
			}
			else
			{
				result = ByteArrayHelperWithString.Instance.ReadArray(this.dictionaryReader, "item", string.Empty, this.dictionaryReader.Quotas.MaxArrayLength);
			}
			return result;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004BBAC File Offset: 0x00049DAC
		internal override byte[] ReadElementContentAsBase64()
		{
			if (this.isEndOfEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Start element expected. Found {0}.", new object[]
				{
					"EndElement"
				})));
			}
			byte[] result;
			if (this.reader.IsStartElement() && this.reader.IsEmptyElement)
			{
				this.reader.Read();
				result = new byte[0];
			}
			else
			{
				this.reader.ReadStartElement();
				result = this.ReadContentAsBase64();
				this.reader.ReadEndElement();
			}
			return result;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004BC34 File Offset: 0x00049E34
		internal override DateTime ReadContentAsDateTime()
		{
			return JsonReaderDelegator.ParseJsonDate(base.ReadContentAsString(), this.dateTimeFormat);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004BC47 File Offset: 0x00049E47
		internal static DateTime ParseJsonDate(string originalDateTimeValue, DateTimeFormat dateTimeFormat)
		{
			if (dateTimeFormat == null)
			{
				return JsonReaderDelegator.ParseJsonDateInDefaultFormat(originalDateTimeValue);
			}
			return DateTime.ParseExact(originalDateTimeValue, dateTimeFormat.FormatString, dateTimeFormat.FormatProvider, dateTimeFormat.DateTimeStyles);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004BC6C File Offset: 0x00049E6C
		internal static DateTime ParseJsonDateInDefaultFormat(string originalDateTimeValue)
		{
			string text;
			if (!string.IsNullOrEmpty(originalDateTimeValue))
			{
				text = originalDateTimeValue.Trim();
			}
			else
			{
				text = originalDateTimeValue;
			}
			if (string.IsNullOrEmpty(text) || !text.StartsWith("/Date(", StringComparison.Ordinal) || !text.EndsWith(")/", StringComparison.Ordinal))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("Invalid JSON dateTime string is specified: original value '{0}', start guide writer: {1}, end guard writer: {2}.", new object[]
				{
					originalDateTimeValue,
					"\\/Date(",
					")\\/"
				})));
			}
			string text2 = text.Substring(6, text.Length - 8);
			DateTimeKind dateTimeKind = DateTimeKind.Utc;
			int num = text2.IndexOf('+', 1);
			if (num == -1)
			{
				num = text2.IndexOf('-', 1);
			}
			if (num != -1)
			{
				dateTimeKind = DateTimeKind.Local;
				text2 = text2.Substring(0, num);
			}
			long num2;
			try
			{
				num2 = long.Parse(text2, CultureInfo.InvariantCulture);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text2, "Int64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text2, "Int64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text2, "Int64", exception3));
			}
			long ticks = num2 * 10000L + JsonGlobals.unixEpochTicks;
			DateTime result;
			try
			{
				DateTime dateTime = new DateTime(ticks, DateTimeKind.Utc);
				switch (dateTimeKind)
				{
				case DateTimeKind.Unspecified:
					return DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
				case DateTimeKind.Local:
					return dateTime.ToLocalTime();
				}
				result = dateTime;
			}
			catch (ArgumentException exception4)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text2, "DateTime", exception4));
			}
			return result;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004BE08 File Offset: 0x0004A008
		internal override DateTime ReadElementContentAsDateTime()
		{
			return JsonReaderDelegator.ParseJsonDate(base.ReadElementContentAsString(), this.dateTimeFormat);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004BE1C File Offset: 0x0004A01C
		internal bool TryReadJsonDateTimeArray(XmlObjectSerializerReadContext context, XmlDictionaryString itemName, XmlDictionaryString itemNamespace, int arrayLength, out DateTime[] array)
		{
			if (this.dictionaryReader == null || arrayLength != -1)
			{
				array = null;
				return false;
			}
			array = this.DateTimeArrayHelper.ReadArray(this.dictionaryReader, XmlDictionaryString.GetString(itemName), XmlDictionaryString.GetString(itemNamespace), base.GetArrayLengthQuota(context));
			context.IncrementItemCount(array.Length);
			return true;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004BE70 File Offset: 0x0004A070
		internal override ulong ReadContentAsUnsignedLong()
		{
			string text = this.reader.ReadContentAsString();
			if (text == null || text.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(XmlObjectSerializer.TryAddLineInfo(this, SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
				{
					text,
					"UInt64"
				}))));
			}
			ulong result;
			try
			{
				result = ulong.Parse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception3));
			}
			return result;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004BF3C File Offset: 0x0004A13C
		internal override ulong ReadElementContentAsUnsignedLong()
		{
			if (this.isEndOfEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Start element expected. Found {0}.", new object[]
				{
					"EndElement"
				})));
			}
			string text = this.reader.ReadElementContentAsString();
			if (text == null || text.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(XmlObjectSerializer.TryAddLineInfo(this, SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
				{
					text,
					"UInt64"
				}))));
			}
			ulong result;
			try
			{
				result = ulong.Parse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(text, "UInt64", exception3));
			}
			return result;
		}

		// Token: 0x040009F8 RID: 2552
		private DateTimeFormat dateTimeFormat;

		// Token: 0x040009F9 RID: 2553
		private JsonReaderDelegator.DateTimeArrayJsonHelperWithString dateTimeArrayHelper;

		// Token: 0x02000187 RID: 391
		private class DateTimeArrayJsonHelperWithString : ArrayHelper<string, DateTime>
		{
			// Token: 0x060013A8 RID: 5032 RVA: 0x0004C030 File Offset: 0x0004A230
			public DateTimeArrayJsonHelperWithString(DateTimeFormat dateTimeFormat)
			{
				this.dateTimeFormat = dateTimeFormat;
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x0004C040 File Offset: 0x0004A240
			protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, DateTime[] array, int offset, int count)
			{
				XmlJsonReader.CheckArray(array, offset, count);
				int num = 0;
				while (num < count && reader.IsStartElement("item", string.Empty))
				{
					array[offset + num] = JsonReaderDelegator.ParseJsonDate(reader.ReadElementContentAsString(), this.dateTimeFormat);
					num++;
				}
				return num;
			}

			// Token: 0x060013AA RID: 5034 RVA: 0x0004C094 File Offset: 0x0004A294
			protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x040009FA RID: 2554
			private DateTimeFormat dateTimeFormat;
		}
	}
}
