using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x0200030B RID: 779
	internal class XmlCustomFormatter
	{
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x000D0C8C File Offset: 0x000CEE8C
		private static DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				if (XmlCustomFormatter.mode == DateTimeSerializationSection.DateTimeSerializationMode.Default)
				{
					DateTimeSerializationSection dateTimeSerializationSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.DateTimeSerializationSectionPath) as DateTimeSerializationSection;
					if (dateTimeSerializationSection != null)
					{
						XmlCustomFormatter.mode = dateTimeSerializationSection.Mode;
					}
					else
					{
						XmlCustomFormatter.mode = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip;
					}
				}
				return XmlCustomFormatter.mode;
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0000216B File Offset: 0x0000036B
		private XmlCustomFormatter()
		{
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000D0CCC File Offset: 0x000CEECC
		internal static string FromDefaultValue(object value, string formatter)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == typeof(DateTime))
			{
				if (formatter == "DateTime")
				{
					return XmlCustomFormatter.FromDateTime((DateTime)value);
				}
				if (formatter == "Date")
				{
					return XmlCustomFormatter.FromDate((DateTime)value);
				}
				if (formatter == "Time")
				{
					return XmlCustomFormatter.FromTime((DateTime)value);
				}
			}
			else if (type == typeof(string))
			{
				if (formatter == "XmlName")
				{
					return XmlCustomFormatter.FromXmlName((string)value);
				}
				if (formatter == "XmlNCName")
				{
					return XmlCustomFormatter.FromXmlNCName((string)value);
				}
				if (formatter == "XmlNmToken")
				{
					return XmlCustomFormatter.FromXmlNmToken((string)value);
				}
				if (formatter == "XmlNmTokens")
				{
					return XmlCustomFormatter.FromXmlNmTokens((string)value);
				}
			}
			throw new Exception(Res.GetString("The default value type, {0}, is unsupported.", new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000D0DD9 File Offset: 0x000CEFD9
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
		internal static string FromTime(DateTime value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization && value.Kind == DateTimeKind.Utc)
			{
				return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffZ");
			}
			return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000D0E3D File Offset: 0x000CF03D
		internal static string FromDateTime(DateTime value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
			}
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000D0E5A File Offset: 0x000CF05A
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000D0E62 File Offset: 0x000CF062
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000D0E6A File Offset: 0x000CF06A
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000D0E72 File Offset: 0x000CF072
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000D0E7C File Offset: 0x000CF07C
		internal static string FromXmlNmTokens(string nmTokens)
		{
			if (nmTokens == null)
			{
				return null;
			}
			if (nmTokens.IndexOf(' ') < 0)
			{
				return XmlCustomFormatter.FromXmlNmToken(nmTokens);
			}
			string[] array = nmTokens.Split(new char[]
			{
				' '
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(XmlCustomFormatter.FromXmlNmToken(array[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000D0EE8 File Offset: 0x000CF0E8
		internal static void WriteArrayBase64(XmlWriter writer, byte[] inData, int start, int count)
		{
			if (inData == null || count == 0)
			{
				return;
			}
			writer.WriteBase64(inData, start, count);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000D0EFA File Offset: 0x000CF0FA
		internal static string FromByteArrayHex(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			return XmlConvert.ToBinHexString(value);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000D0F14 File Offset: 0x000CF114
		internal static string FromEnum(long val, string[] vals, long[] ids, string typeName)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0L)
				{
					num2 = i;
				}
				else
				{
					if (val == 0L)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0L)
			{
				throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
				{
					num,
					(typeName == null) ? "enum" : typeName
				}));
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000D0FC4 File Offset: 0x000CF1C4
		internal static object ToDefaultValue(string value, string formatter)
		{
			if (formatter == "DateTime")
			{
				return XmlCustomFormatter.ToDateTime(value);
			}
			if (formatter == "Date")
			{
				return XmlCustomFormatter.ToDate(value);
			}
			if (formatter == "Time")
			{
				return XmlCustomFormatter.ToTime(value);
			}
			if (formatter == "XmlName")
			{
				return XmlCustomFormatter.ToXmlName(value);
			}
			if (formatter == "XmlNCName")
			{
				return XmlCustomFormatter.ToXmlNCName(value);
			}
			if (formatter == "XmlNmToken")
			{
				return XmlCustomFormatter.ToXmlNmToken(value);
			}
			if (formatter == "XmlNmTokens")
			{
				return XmlCustomFormatter.ToXmlNmTokens(value);
			}
			throw new Exception(Res.GetString("The formatter {0} cannot be used for default values.", new object[]
			{
				formatter
			}));
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000D1085 File Offset: 0x000CF285
		internal static DateTime ToDateTime(string value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
			}
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000D10A2 File Offset: 0x000CF2A2
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000D10AB File Offset: 0x000CF2AB
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000D10B8 File Offset: 0x000CF2B8
		internal static DateTime ToTime(string value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization)
			{
				return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.RoundtripKind);
			}
			return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000D10E9 File Offset: 0x000CF2E9
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000D10F1 File Offset: 0x000CF2F1
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000D10F1 File Offset: 0x000CF2F1
		internal static string ToXmlNCName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000D10F1 File Offset: 0x000CF2F1
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000D10F1 File Offset: 0x000CF2F1
		internal static string ToXmlNmTokens(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000D10FE File Offset: 0x000CF2FE
		internal static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000D1122 File Offset: 0x000CF322
		internal static byte[] ToByteArrayHex(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000D1138 File Offset: 0x000CF338
		internal static long ToEnum(string val, Hashtable vals, string typeName, bool validate)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj != null)
				{
					num |= (long)obj;
				}
				else if (validate && array[i].Length > 0)
				{
					throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
					{
						array[i],
						typeName
					}));
				}
			}
			return num;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000D11A5 File Offset: 0x000CF3A5
		private static string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000D11B4 File Offset: 0x000CF3B4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlCustomFormatter()
		{
		}

		// Token: 0x04001B2B RID: 6955
		private static DateTimeSerializationSection.DateTimeSerializationMode mode;

		// Token: 0x04001B2C RID: 6956
		private static string[] allDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz",
			"yyyy",
			"---dd",
			"---ddZ",
			"---ddzzzzzz",
			"--MM-dd",
			"--MM-ddZ",
			"--MM-ddzzzzzz",
			"--MM--",
			"--MM--Z",
			"--MM--zzzzzz",
			"yyyy-MM",
			"yyyy-MMZ",
			"yyyy-MMzzzzzz",
			"yyyyzzzzzz",
			"yyyy-MM-dd",
			"yyyy-MM-ddZ",
			"yyyy-MM-ddzzzzzz",
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss",
			"yyyy-MM-ddTHH:mm:ss.f",
			"yyyy-MM-ddTHH:mm:ss.ff",
			"yyyy-MM-ddTHH:mm:ss.fff",
			"yyyy-MM-ddTHH:mm:ss.ffff",
			"yyyy-MM-ddTHH:mm:ss.fffff",
			"yyyy-MM-ddTHH:mm:ss.ffffff",
			"yyyy-MM-ddTHH:mm:ss.fffffff",
			"yyyy-MM-ddTHH:mm:ssZ",
			"yyyy-MM-ddTHH:mm:ss.fZ",
			"yyyy-MM-ddTHH:mm:ss.ffZ",
			"yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffffZ",
			"yyyy-MM-ddTHH:mm:sszzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz"
		};

		// Token: 0x04001B2D RID: 6957
		private static string[] allDateFormats = new string[]
		{
			"yyyy-MM-ddzzzzzz",
			"yyyy-MM-dd",
			"yyyy-MM-ddZ",
			"yyyy",
			"---dd",
			"---ddZ",
			"---ddzzzzzz",
			"--MM-dd",
			"--MM-ddZ",
			"--MM-ddzzzzzz",
			"--MM--",
			"--MM--Z",
			"--MM--zzzzzz",
			"yyyy-MM",
			"yyyy-MMZ",
			"yyyy-MMzzzzzz",
			"yyyyzzzzzz"
		};

		// Token: 0x04001B2E RID: 6958
		private static string[] allTimeFormats = new string[]
		{
			"HH:mm:ss.fffffffzzzzzz",
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz"
		};
	}
}
