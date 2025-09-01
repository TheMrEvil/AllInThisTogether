using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000CD RID: 205
	[DataContract(Name = "DateTimeOffset", Namespace = "http://schemas.datacontract.org/2004/07/System")]
	internal struct DateTimeOffsetAdapter
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00031BB2 File Offset: 0x0002FDB2
		public DateTimeOffsetAdapter(DateTime dateTime, short offsetMinutes)
		{
			this.utcDateTime = dateTime;
			this.offsetMinutes = offsetMinutes;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00031BC2 File Offset: 0x0002FDC2
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00031BCA File Offset: 0x0002FDCA
		[DataMember(Name = "DateTime", IsRequired = true)]
		public DateTime UtcDateTime
		{
			get
			{
				return this.utcDateTime;
			}
			set
			{
				this.utcDateTime = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00031BD3 File Offset: 0x0002FDD3
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00031BDB File Offset: 0x0002FDDB
		[DataMember(Name = "OffsetMinutes", IsRequired = true)]
		public short OffsetMinutes
		{
			get
			{
				return this.offsetMinutes;
			}
			set
			{
				this.offsetMinutes = value;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00031BE4 File Offset: 0x0002FDE4
		public static DateTimeOffset GetDateTimeOffset(DateTimeOffsetAdapter value)
		{
			DateTimeOffset result;
			try
			{
				if (value.UtcDateTime.Kind == DateTimeKind.Unspecified)
				{
					result = new DateTimeOffset(value.UtcDateTime, new TimeSpan(0, (int)value.OffsetMinutes, 0));
				}
				else
				{
					DateTimeOffset dateTimeOffset = new DateTimeOffset(value.UtcDateTime);
					result = dateTimeOffset.ToOffset(new TimeSpan(0, (int)value.OffsetMinutes, 0));
				}
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value.ToString(CultureInfo.InvariantCulture), "DateTimeOffset", exception));
			}
			return result;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00031C74 File Offset: 0x0002FE74
		public static DateTimeOffsetAdapter GetDateTimeOffsetAdapter(DateTimeOffset value)
		{
			return new DateTimeOffsetAdapter(value.UtcDateTime, (short)value.Offset.TotalMinutes);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		public string ToString(IFormatProvider provider)
		{
			return "DateTime: " + this.UtcDateTime.ToString() + ", Offset: " + this.OffsetMinutes.ToString();
		}

		// Token: 0x040004C8 RID: 1224
		private DateTime utcDateTime;

		// Token: 0x040004C9 RID: 1225
		private short offsetMinutes;
	}
}
