using System;

namespace System.Data.SqlTypes
{
	// Token: 0x02000303 RID: 771
	internal static class SQLResource
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0009ED73 File Offset: 0x0009CF73
		internal static string NullString
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x0009ED7A File Offset: 0x0009CF7A
		internal static string MessageString
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x0009ED81 File Offset: 0x0009CF81
		internal static string ArithOverflowMessage
		{
			get
			{
				return "Arithmetic Overflow.";
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0009ED88 File Offset: 0x0009CF88
		internal static string DivideByZeroMessage
		{
			get
			{
				return "Divide by zero error encountered.";
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0009ED8F File Offset: 0x0009CF8F
		internal static string NullValueMessage
		{
			get
			{
				return "Data is Null. This method or property cannot be called on Null values.";
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x0009ED96 File Offset: 0x0009CF96
		internal static string TruncationMessage
		{
			get
			{
				return "Numeric arithmetic causes truncation.";
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x0009ED9D File Offset: 0x0009CF9D
		internal static string DateTimeOverflowMessage
		{
			get
			{
				return "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x0009EDA4 File Offset: 0x0009CFA4
		internal static string ConcatDiffCollationMessage
		{
			get
			{
				return "Two strings to be concatenated have different collation.";
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0009EDAB File Offset: 0x0009CFAB
		internal static string CompareDiffCollationMessage
		{
			get
			{
				return "Two strings to be compared have different collation.";
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x0009EDB2 File Offset: 0x0009CFB2
		internal static string InvalidFlagMessage
		{
			get
			{
				return "Invalid flag value.";
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0009EDB9 File Offset: 0x0009CFB9
		internal static string NumeToDecOverflowMessage
		{
			get
			{
				return "Conversion from SqlDecimal to Decimal overflows.";
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0009EDC0 File Offset: 0x0009CFC0
		internal static string ConversionOverflowMessage
		{
			get
			{
				return "Conversion overflows.";
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x0009EDC7 File Offset: 0x0009CFC7
		internal static string InvalidDateTimeMessage
		{
			get
			{
				return "Invalid SqlDateTime.";
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0009EDCE File Offset: 0x0009CFCE
		internal static string TimeZoneSpecifiedMessage
		{
			get
			{
				return "A time zone was specified. SqlDateTime does not support time zones.";
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0009EDD5 File Offset: 0x0009CFD5
		internal static string InvalidArraySizeMessage
		{
			get
			{
				return "Invalid array size.";
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0009EDDC File Offset: 0x0009CFDC
		internal static string InvalidPrecScaleMessage
		{
			get
			{
				return "Invalid numeric precision/scale.";
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0009EDE3 File Offset: 0x0009CFE3
		internal static string FormatMessage
		{
			get
			{
				return "The input wasn't in a correct format.";
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0009EDEA File Offset: 0x0009CFEA
		internal static string NotFilledMessage
		{
			get
			{
				return "SQL Type has not been loaded with data.";
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0009EDF1 File Offset: 0x0009CFF1
		internal static string AlreadyFilledMessage
		{
			get
			{
				return "SQL Type has already been loaded with data.";
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0009EDF8 File Offset: 0x0009CFF8
		internal static string ClosedXmlReaderMessage
		{
			get
			{
				return "Invalid attempt to access a closed XmlReader.";
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0009EDFF File Offset: 0x0009CFFF
		internal static string InvalidOpStreamClosed(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is closed.", method);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0009EE0C File Offset: 0x0009D00C
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-writable.", method);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0009EE19 File Offset: 0x0009D019
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream non-readable.", method);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0009EE26 File Offset: 0x0009D026
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return SR.Format("Invalid attempt to call {0} when the stream is non-seekable.", method);
		}
	}
}
