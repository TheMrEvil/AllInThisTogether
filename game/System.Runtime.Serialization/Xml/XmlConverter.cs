using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200005C RID: 92
	internal static class XmlConverter
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000154AC File Offset: 0x000136AC
		public static Base64Encoding Base64Encoding
		{
			get
			{
				if (XmlConverter.base64Encoding == null)
				{
					XmlConverter.base64Encoding = new Base64Encoding();
				}
				return XmlConverter.base64Encoding;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000154C4 File Offset: 0x000136C4
		private static UTF8Encoding UTF8Encoding
		{
			get
			{
				if (XmlConverter.utf8Encoding == null)
				{
					XmlConverter.utf8Encoding = new UTF8Encoding(false, true);
				}
				return XmlConverter.utf8Encoding;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000154DE File Offset: 0x000136DE
		private static UnicodeEncoding UnicodeEncoding
		{
			get
			{
				if (XmlConverter.unicodeEncoding == null)
				{
					XmlConverter.unicodeEncoding = new UnicodeEncoding(false, false, true);
				}
				return XmlConverter.unicodeEncoding;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000154FC File Offset: 0x000136FC
		public static bool ToBoolean(string value)
		{
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Boolean", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Boolean", exception2));
			}
			return result;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015558 File Offset: 0x00013758
		public static bool ToBoolean(byte[] buffer, int offset, int count)
		{
			if (count == 1)
			{
				byte b = buffer[offset];
				if (b == 49)
				{
					return true;
				}
				if (b == 48)
				{
					return false;
				}
			}
			return XmlConverter.ToBoolean(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00015588 File Offset: 0x00013788
		public static int ToInt32(string value)
		{
			int result;
			try
			{
				result = XmlConvert.ToInt32(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int32", exception3));
			}
			return result;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00015600 File Offset: 0x00013800
		public static int ToInt32(byte[] buffer, int offset, int count)
		{
			int result;
			if (XmlConverter.TryParseInt32(buffer, offset, count, out result))
			{
				return result;
			}
			return XmlConverter.ToInt32(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00015628 File Offset: 0x00013828
		public static long ToInt64(string value)
		{
			long result;
			try
			{
				result = XmlConvert.ToInt64(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Int64", exception3));
			}
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000156A0 File Offset: 0x000138A0
		public static long ToInt64(byte[] buffer, int offset, int count)
		{
			long result;
			if (XmlConverter.TryParseInt64(buffer, offset, count, out result))
			{
				return result;
			}
			return XmlConverter.ToInt64(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000156C8 File Offset: 0x000138C8
		public static float ToSingle(string value)
		{
			float result;
			try
			{
				result = XmlConvert.ToSingle(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "float", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "float", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "float", exception3));
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00015740 File Offset: 0x00013940
		public static float ToSingle(byte[] buffer, int offset, int count)
		{
			float result;
			if (XmlConverter.TryParseSingle(buffer, offset, count, out result))
			{
				return result;
			}
			return XmlConverter.ToSingle(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015768 File Offset: 0x00013968
		public static double ToDouble(string value)
		{
			double result;
			try
			{
				result = XmlConvert.ToDouble(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "double", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "double", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "double", exception3));
			}
			return result;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000157E0 File Offset: 0x000139E0
		public static double ToDouble(byte[] buffer, int offset, int count)
		{
			double result;
			if (XmlConverter.TryParseDouble(buffer, offset, count, out result))
			{
				return result;
			}
			return XmlConverter.ToDouble(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00015808 File Offset: 0x00013A08
		public static decimal ToDecimal(string value)
		{
			decimal result;
			try
			{
				result = XmlConvert.ToDecimal(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "decimal", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "decimal", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "decimal", exception3));
			}
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00015880 File Offset: 0x00013A80
		public static decimal ToDecimal(byte[] buffer, int offset, int count)
		{
			return XmlConverter.ToDecimal(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00015890 File Offset: 0x00013A90
		public static DateTime ToDateTime(long value)
		{
			DateTime result;
			try
			{
				result = DateTime.FromBinary(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(XmlConverter.ToString(value), "DateTime", exception));
			}
			return result;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000158D0 File Offset: 0x00013AD0
		public static DateTime ToDateTime(string value)
		{
			DateTime result;
			try
			{
				result = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "DateTime", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "DateTime", exception2));
			}
			return result;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001592C File Offset: 0x00013B2C
		public static DateTime ToDateTime(byte[] buffer, int offset, int count)
		{
			DateTime result;
			if (XmlConverter.TryParseDateTime(buffer, offset, count, out result))
			{
				return result;
			}
			return XmlConverter.ToDateTime(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015954 File Offset: 0x00013B54
		public static UniqueId ToUniqueId(string value)
		{
			UniqueId result;
			try
			{
				result = new UniqueId(XmlConverter.Trim(value));
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "UniqueId", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "UniqueId", exception2));
			}
			return result;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000159B4 File Offset: 0x00013BB4
		public static UniqueId ToUniqueId(byte[] buffer, int offset, int count)
		{
			return XmlConverter.ToUniqueId(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000159C4 File Offset: 0x00013BC4
		public static TimeSpan ToTimeSpan(string value)
		{
			TimeSpan result;
			try
			{
				result = XmlConvert.ToTimeSpan(value);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "TimeSpan", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "TimeSpan", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "TimeSpan", exception3));
			}
			return result;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00015A3C File Offset: 0x00013C3C
		public static TimeSpan ToTimeSpan(byte[] buffer, int offset, int count)
		{
			return XmlConverter.ToTimeSpan(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015A4C File Offset: 0x00013C4C
		public static Guid ToGuid(string value)
		{
			Guid result;
			try
			{
				result = Guid.Parse(XmlConverter.Trim(value));
			}
			catch (FormatException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Guid", exception));
			}
			catch (ArgumentException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Guid", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "Guid", exception3));
			}
			return result;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00015ACC File Offset: 0x00013CCC
		public static Guid ToGuid(byte[] buffer, int offset, int count)
		{
			return XmlConverter.ToGuid(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00015ADC File Offset: 0x00013CDC
		public static ulong ToUInt64(string value)
		{
			ulong result;
			try
			{
				result = ulong.Parse(value, NumberFormatInfo.InvariantInfo);
			}
			catch (ArgumentException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "UInt64", exception));
			}
			catch (FormatException exception2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "UInt64", exception2));
			}
			catch (OverflowException exception3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value, "UInt64", exception3));
			}
			return result;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00015B5C File Offset: 0x00013D5C
		public static ulong ToUInt64(byte[] buffer, int offset, int count)
		{
			return XmlConverter.ToUInt64(XmlConverter.ToString(buffer, offset, count));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00015B6C File Offset: 0x00013D6C
		public static string ToString(byte[] buffer, int offset, int count)
		{
			string @string;
			try
			{
				@string = XmlConverter.UTF8Encoding.GetString(buffer, offset, count);
			}
			catch (DecoderFallbackException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateEncodingException(buffer, offset, count, exception));
			}
			return @string;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00015BAC File Offset: 0x00013DAC
		public static string ToStringUnicode(byte[] buffer, int offset, int count)
		{
			string @string;
			try
			{
				@string = XmlConverter.UnicodeEncoding.GetString(buffer, offset, count);
			}
			catch (DecoderFallbackException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateEncodingException(buffer, offset, count, exception));
			}
			return @string;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00015BEC File Offset: 0x00013DEC
		public static byte[] ToBytes(string value)
		{
			byte[] bytes;
			try
			{
				bytes = XmlConverter.UTF8Encoding.GetBytes(value);
			}
			catch (DecoderFallbackException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateEncodingException(value, exception));
			}
			return bytes;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00015C28 File Offset: 0x00013E28
		public static int ToChars(byte[] buffer, int offset, int count, char[] chars, int charOffset)
		{
			int chars2;
			try
			{
				chars2 = XmlConverter.UTF8Encoding.GetChars(buffer, offset, count, chars, charOffset);
			}
			catch (DecoderFallbackException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateEncodingException(buffer, offset, count, exception));
			}
			return chars2;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00015C6C File Offset: 0x00013E6C
		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00015C7C File Offset: 0x00013E7C
		public static string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00015C84 File Offset: 0x00013E84
		public static string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00015C8C File Offset: 0x00013E8C
		public static string ToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00015C94 File Offset: 0x00013E94
		public static string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00015C9C File Offset: 0x00013E9C
		public static string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public static string ToString(TimeSpan value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00015CAC File Offset: 0x00013EAC
		public static string ToString(UniqueId value)
		{
			return value.ToString();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00015CB4 File Offset: 0x00013EB4
		public static string ToString(Guid value)
		{
			return value.ToString();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00015CC3 File Offset: 0x00013EC3
		public static string ToString(ulong value)
		{
			return value.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00015CD4 File Offset: 0x00013ED4
		public static string ToString(DateTime value)
		{
			byte[] array = new byte[64];
			int count = XmlConverter.ToChars(value, array, 0);
			return XmlConverter.ToString(array, 0, count);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00015CFC File Offset: 0x00013EFC
		private static string ToString(object value)
		{
			if (value is int)
			{
				return XmlConverter.ToString((int)value);
			}
			if (value is long)
			{
				return XmlConverter.ToString((long)value);
			}
			if (value is float)
			{
				return XmlConverter.ToString((float)value);
			}
			if (value is double)
			{
				return XmlConverter.ToString((double)value);
			}
			if (value is decimal)
			{
				return XmlConverter.ToString((decimal)value);
			}
			if (value is TimeSpan)
			{
				return XmlConverter.ToString((TimeSpan)value);
			}
			if (value is UniqueId)
			{
				return XmlConverter.ToString((UniqueId)value);
			}
			if (value is Guid)
			{
				return XmlConverter.ToString((Guid)value);
			}
			if (value is ulong)
			{
				return XmlConverter.ToString((ulong)value);
			}
			if (value is DateTime)
			{
				return XmlConverter.ToString((DateTime)value);
			}
			if (value is bool)
			{
				return XmlConverter.ToString((bool)value);
			}
			return value.ToString();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00015DEC File Offset: 0x00013FEC
		public static string ToString(object[] objects)
		{
			if (objects.Length == 0)
			{
				return string.Empty;
			}
			string text = XmlConverter.ToString(objects[0]);
			if (objects.Length > 1)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				for (int i = 1; i < objects.Length; i++)
				{
					stringBuilder.Append(' ');
					stringBuilder.Append(XmlConverter.ToString(objects[i]));
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015E48 File Offset: 0x00014048
		public static void ToQualifiedName(string qname, out string prefix, out string localName)
		{
			int num = qname.IndexOf(':');
			if (num < 0)
			{
				prefix = string.Empty;
				localName = XmlConverter.Trim(qname);
				return;
			}
			if (num == qname.Length - 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Expected XML qualified name. Found '{0}'.", new object[]
				{
					qname
				})));
			}
			prefix = XmlConverter.Trim(qname.Substring(0, num));
			localName = XmlConverter.Trim(qname.Substring(num + 1));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015EBC File Offset: 0x000140BC
		private static bool TryParseInt32(byte[] chars, int offset, int count, out int result)
		{
			result = 0;
			if (count == 0)
			{
				return false;
			}
			int num = 0;
			int num2 = offset + count;
			if (chars[offset] == 45)
			{
				if (count == 1)
				{
					return false;
				}
				for (int i = offset + 1; i < num2; i++)
				{
					int num3 = (int)(chars[i] - 48);
					if (num3 > 9)
					{
						return false;
					}
					if (num < -214748364)
					{
						return false;
					}
					num *= 10;
					if (num < -2147483648 + num3)
					{
						return false;
					}
					num -= num3;
				}
			}
			else
			{
				for (int j = offset; j < num2; j++)
				{
					int num4 = (int)(chars[j] - 48);
					if (num4 > 9)
					{
						return false;
					}
					if (num > 214748364)
					{
						return false;
					}
					num *= 10;
					if (num > 2147483647 - num4)
					{
						return false;
					}
					num += num4;
				}
			}
			result = num;
			return true;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015F68 File Offset: 0x00014168
		private static bool TryParseInt64(byte[] chars, int offset, int count, out long result)
		{
			result = 0L;
			if (count >= 11)
			{
				long num = 0L;
				int num2 = offset + count;
				if (chars[offset] == 45)
				{
					if (count == 1)
					{
						return false;
					}
					for (int i = offset + 1; i < num2; i++)
					{
						int num3 = (int)(chars[i] - 48);
						if (num3 > 9)
						{
							return false;
						}
						if (num < -922337203685477580L)
						{
							return false;
						}
						num *= 10L;
						if (num < -9223372036854775808L + (long)num3)
						{
							return false;
						}
						num -= (long)num3;
					}
				}
				else
				{
					for (int j = offset; j < num2; j++)
					{
						int num4 = (int)(chars[j] - 48);
						if (num4 > 9)
						{
							return false;
						}
						if (num > 922337203685477580L)
						{
							return false;
						}
						num *= 10L;
						if (num > 9223372036854775807L - (long)num4)
						{
							return false;
						}
						num += (long)num4;
					}
				}
				result = num;
				return true;
			}
			int num5;
			if (!XmlConverter.TryParseInt32(chars, offset, count, out num5))
			{
				return false;
			}
			result = (long)num5;
			return true;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00016044 File Offset: 0x00014244
		private static bool TryParseSingle(byte[] chars, int offset, int count, out float result)
		{
			result = 0f;
			int num = offset + count;
			bool flag = false;
			if (offset < num && chars[offset] == 45)
			{
				flag = true;
				offset++;
				count--;
			}
			if (count < 1 || count > 10)
			{
				return false;
			}
			int num2 = 0;
			while (offset < num)
			{
				int num3 = (int)(chars[offset] - 48);
				if (num3 == -2)
				{
					offset++;
					int num4 = 1;
					while (offset < num)
					{
						num3 = (int)(chars[offset] - 48);
						if (num3 >= 10)
						{
							return false;
						}
						num4 *= 10;
						num2 = num2 * 10 + num3;
						offset++;
					}
					if (count > 8)
					{
						result = (float)((double)num2 / (double)num4);
					}
					else
					{
						result = (float)num2 / (float)num4;
					}
					if (flag)
					{
						result = -result;
					}
					return true;
				}
				if (num3 >= 10)
				{
					return false;
				}
				num2 = num2 * 10 + num3;
				offset++;
			}
			if (count == 10)
			{
				return false;
			}
			if (flag)
			{
				result = (float)(-(float)num2);
			}
			else
			{
				result = (float)num2;
			}
			return true;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00016110 File Offset: 0x00014310
		private static bool TryParseDouble(byte[] chars, int offset, int count, out double result)
		{
			result = 0.0;
			int num = offset + count;
			bool flag = false;
			if (offset < num && chars[offset] == 45)
			{
				flag = true;
				offset++;
				count--;
			}
			if (count < 1 || count > 10)
			{
				return false;
			}
			int num2 = 0;
			while (offset < num)
			{
				int num3 = (int)(chars[offset] - 48);
				if (num3 == -2)
				{
					offset++;
					int num4 = 1;
					while (offset < num)
					{
						num3 = (int)(chars[offset] - 48);
						if (num3 >= 10)
						{
							return false;
						}
						num4 *= 10;
						num2 = num2 * 10 + num3;
						offset++;
					}
					if (flag)
					{
						result = -(double)num2 / (double)num4;
					}
					else
					{
						result = (double)num2 / (double)num4;
					}
					return true;
				}
				if (num3 >= 10)
				{
					return false;
				}
				num2 = num2 * 10 + num3;
				offset++;
			}
			if (count == 10)
			{
				return false;
			}
			if (flag)
			{
				result = (double)(-(double)num2);
			}
			else
			{
				result = (double)num2;
			}
			return true;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000161D4 File Offset: 0x000143D4
		private static int ToInt32D2(byte[] chars, int offset)
		{
			byte b = chars[offset] - 48;
			byte b2 = chars[offset + 1] - 48;
			if (b > 9 || b2 > 9)
			{
				return -1;
			}
			return (int)(10 * b + b2);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00016205 File Offset: 0x00014405
		private static int ToInt32D4(byte[] chars, int offset, int count)
		{
			return XmlConverter.ToInt32D7(chars, offset, count);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00016210 File Offset: 0x00014410
		private static int ToInt32D7(byte[] chars, int offset, int count)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				byte b = chars[offset + i] - 48;
				if (b > 9)
				{
					return -1;
				}
				num = num * 10 + (int)b;
			}
			return num;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00016244 File Offset: 0x00014444
		private static bool TryParseDateTime(byte[] chars, int offset, int count, out DateTime result)
		{
			int num = offset + count;
			result = DateTime.MaxValue;
			if (count < 19)
			{
				return false;
			}
			if (chars[offset + 4] != 45 || chars[offset + 7] != 45 || chars[offset + 10] != 84 || chars[offset + 13] != 58 || chars[offset + 16] != 58)
			{
				return false;
			}
			int num2 = XmlConverter.ToInt32D4(chars, offset, 4);
			int num3 = XmlConverter.ToInt32D2(chars, offset + 5);
			int num4 = XmlConverter.ToInt32D2(chars, offset + 8);
			int num5 = XmlConverter.ToInt32D2(chars, offset + 11);
			int num6 = XmlConverter.ToInt32D2(chars, offset + 14);
			int num7 = XmlConverter.ToInt32D2(chars, offset + 17);
			if ((num2 | num3 | num4 | num5 | num6 | num7) < 0)
			{
				return false;
			}
			DateTimeKind kind = DateTimeKind.Unspecified;
			offset += 19;
			int num8 = 0;
			if (offset < num && chars[offset] == 46)
			{
				offset++;
				int num9 = offset;
				while (offset < num)
				{
					byte b = chars[offset];
					if (b < 48 || b > 57)
					{
						break;
					}
					offset++;
				}
				int num10 = offset - num9;
				if (num10 < 1 || num10 > 7)
				{
					return false;
				}
				num8 = XmlConverter.ToInt32D7(chars, num9, num10);
				if (num8 < 0)
				{
					return false;
				}
				for (int i = num10; i < 7; i++)
				{
					num8 *= 10;
				}
			}
			bool flag = false;
			int num11 = 0;
			int num12 = 0;
			if (offset < num)
			{
				byte b2 = chars[offset];
				if (b2 == 90)
				{
					offset++;
					kind = DateTimeKind.Utc;
				}
				else if (b2 == 43 || b2 == 45)
				{
					offset++;
					if (offset + 5 > num || chars[offset + 2] != 58)
					{
						return false;
					}
					kind = DateTimeKind.Utc;
					flag = true;
					num11 = XmlConverter.ToInt32D2(chars, offset);
					num12 = XmlConverter.ToInt32D2(chars, offset + 3);
					if ((num11 | num12) < 0)
					{
						return false;
					}
					if (b2 == 43)
					{
						num11 = -num11;
						num12 = -num12;
					}
					offset += 5;
				}
			}
			if (offset < num)
			{
				return false;
			}
			DateTime dateTime;
			try
			{
				dateTime = new DateTime(num2, num3, num4, num5, num6, num7, kind);
			}
			catch (ArgumentException)
			{
				return false;
			}
			if (num8 > 0)
			{
				dateTime = dateTime.AddTicks((long)num8);
			}
			if (flag)
			{
				try
				{
					TimeSpan timeSpan = new TimeSpan(num11, num12, 0);
					if ((num11 >= 0 && dateTime < DateTime.MaxValue - timeSpan) || (num11 < 0 && dateTime > DateTime.MinValue - timeSpan))
					{
						dateTime = dateTime.Add(timeSpan).ToLocalTime();
					}
					else
					{
						dateTime = dateTime.ToLocalTime().Add(timeSpan);
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					return false;
				}
			}
			result = dateTime;
			return true;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000164B8 File Offset: 0x000146B8
		public static int ToChars(bool value, byte[] buffer, int offset)
		{
			if (value)
			{
				buffer[offset] = 116;
				buffer[offset + 1] = 114;
				buffer[offset + 2] = 117;
				buffer[offset + 3] = 101;
				return 4;
			}
			buffer[offset] = 102;
			buffer[offset + 1] = 97;
			buffer[offset + 2] = 108;
			buffer[offset + 3] = 115;
			buffer[offset + 4] = 101;
			return 5;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00016508 File Offset: 0x00014708
		public static int ToCharsR(int value, byte[] chars, int offset)
		{
			int num = 0;
			if (value >= 0)
			{
				while (value >= 10)
				{
					int num2 = value / 10;
					num++;
					chars[--offset] = (byte)(48 + (value - num2 * 10));
					value = num2;
				}
				chars[--offset] = (byte)(48 + value);
				num++;
			}
			else
			{
				while (value <= -10)
				{
					int num3 = value / 10;
					num++;
					chars[--offset] = (byte)(48 - (value - num3 * 10));
					value = num3;
				}
				chars[--offset] = (byte)(48 - value);
				chars[--offset] = 45;
				num += 2;
			}
			return num;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00016594 File Offset: 0x00014794
		public static int ToChars(int value, byte[] chars, int offset)
		{
			int num = XmlConverter.ToCharsR(value, chars, offset + 16);
			Buffer.BlockCopy(chars, offset + 16 - num, chars, offset, num);
			return num;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000165C0 File Offset: 0x000147C0
		public static int ToCharsR(long value, byte[] chars, int offset)
		{
			int num = 0;
			if (value >= 0L)
			{
				while (value > 2147483647L)
				{
					long num2 = value / 10L;
					num++;
					chars[--offset] = (byte)(48 + (int)(value - num2 * 10L));
					value = num2;
				}
			}
			else
			{
				while (value < -2147483648L)
				{
					long num3 = value / 10L;
					num++;
					chars[--offset] = (byte)(48 - (int)(value - num3 * 10L));
					value = num3;
				}
			}
			return num + XmlConverter.ToCharsR((int)value, chars, offset);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00016638 File Offset: 0x00014838
		public static int ToChars(long value, byte[] chars, int offset)
		{
			int num = XmlConverter.ToCharsR(value, chars, offset + 32);
			Buffer.BlockCopy(chars, offset + 32 - num, chars, offset, num);
			return num;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016664 File Offset: 0x00014864
		[SecuritySafeCritical]
		private unsafe static bool IsNegativeZero(float value)
		{
			float num = --0f;
			return *(int*)(&value) == *(int*)(&num);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016684 File Offset: 0x00014884
		[SecuritySafeCritical]
		private unsafe static bool IsNegativeZero(double value)
		{
			double num = --0.0;
			return *(long*)(&value) == *(long*)(&num);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000166A5 File Offset: 0x000148A5
		private static int ToInfinity(bool isNegative, byte[] buffer, int offset)
		{
			if (isNegative)
			{
				buffer[offset] = 45;
				buffer[offset + 1] = 73;
				buffer[offset + 2] = 78;
				buffer[offset + 3] = 70;
				return 4;
			}
			buffer[offset] = 73;
			buffer[offset + 1] = 78;
			buffer[offset + 2] = 70;
			return 3;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000166DA File Offset: 0x000148DA
		private static int ToZero(bool isNegative, byte[] buffer, int offset)
		{
			if (isNegative)
			{
				buffer[offset] = 45;
				buffer[offset + 1] = 48;
				return 2;
			}
			buffer[offset] = 48;
			return 1;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000166F4 File Offset: 0x000148F4
		public static int ToChars(double value, byte[] buffer, int offset)
		{
			if (double.IsInfinity(value))
			{
				return XmlConverter.ToInfinity(double.IsNegativeInfinity(value), buffer, offset);
			}
			if (value == 0.0)
			{
				return XmlConverter.ToZero(XmlConverter.IsNegativeZero(value), buffer, offset);
			}
			return XmlConverter.ToAsciiChars(value.ToString("R", NumberFormatInfo.InvariantInfo), buffer, offset);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001674C File Offset: 0x0001494C
		public static int ToChars(float value, byte[] buffer, int offset)
		{
			if (float.IsInfinity(value))
			{
				return XmlConverter.ToInfinity(float.IsNegativeInfinity(value), buffer, offset);
			}
			if ((double)value == 0.0)
			{
				return XmlConverter.ToZero(XmlConverter.IsNegativeZero(value), buffer, offset);
			}
			return XmlConverter.ToAsciiChars(value.ToString("R", NumberFormatInfo.InvariantInfo), buffer, offset);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000167A2 File Offset: 0x000149A2
		public static int ToChars(decimal value, byte[] buffer, int offset)
		{
			return XmlConverter.ToAsciiChars(value.ToString(null, NumberFormatInfo.InvariantInfo), buffer, offset);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000167B8 File Offset: 0x000149B8
		public static int ToChars(ulong value, byte[] buffer, int offset)
		{
			return XmlConverter.ToAsciiChars(value.ToString(null, NumberFormatInfo.InvariantInfo), buffer, offset);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000167D0 File Offset: 0x000149D0
		private static int ToAsciiChars(string s, byte[] buffer, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				buffer[offset++] = (byte)s[i];
			}
			return s.Length;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016804 File Offset: 0x00014A04
		private static int ToCharsD2(int value, byte[] chars, int offset)
		{
			if (value < 10)
			{
				chars[offset] = 48;
				chars[offset + 1] = (byte)(48 + value);
			}
			else
			{
				int num = value / 10;
				chars[offset] = (byte)(48 + num);
				chars[offset + 1] = (byte)(48 + value - num * 10);
			}
			return 2;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00016844 File Offset: 0x00014A44
		private static int ToCharsD4(int value, byte[] chars, int offset)
		{
			XmlConverter.ToCharsD2(value / 100, chars, offset);
			XmlConverter.ToCharsD2(value % 100, chars, offset + 2);
			return 4;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016864 File Offset: 0x00014A64
		private static int ToCharsD7(int value, byte[] chars, int offset)
		{
			int num = 7 - XmlConverter.ToCharsR(value, chars, offset + 7);
			for (int i = 0; i < num; i++)
			{
				chars[offset + i] = 48;
			}
			int num2 = 7;
			while (num2 > 0 && chars[offset + num2 - 1] == 48)
			{
				num2--;
			}
			return num2;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000168AC File Offset: 0x00014AAC
		public static int ToChars(DateTime value, byte[] chars, int offset)
		{
			int num = offset;
			offset += XmlConverter.ToCharsD4(value.Year, chars, offset);
			chars[offset++] = 45;
			offset += XmlConverter.ToCharsD2(value.Month, chars, offset);
			chars[offset++] = 45;
			offset += XmlConverter.ToCharsD2(value.Day, chars, offset);
			chars[offset++] = 84;
			offset += XmlConverter.ToCharsD2(value.Hour, chars, offset);
			chars[offset++] = 58;
			offset += XmlConverter.ToCharsD2(value.Minute, chars, offset);
			chars[offset++] = 58;
			offset += XmlConverter.ToCharsD2(value.Second, chars, offset);
			int num2 = (int)(value.Ticks % 10000000L);
			if (num2 != 0)
			{
				chars[offset++] = 46;
				offset += XmlConverter.ToCharsD7(num2, chars, offset);
			}
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				break;
			case DateTimeKind.Utc:
				chars[offset++] = 90;
				break;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				if (utcOffset.Ticks < 0L)
				{
					chars[offset++] = 45;
				}
				else
				{
					chars[offset++] = 43;
				}
				offset += XmlConverter.ToCharsD2(Math.Abs(utcOffset.Hours), chars, offset);
				chars[offset++] = 58;
				offset += XmlConverter.ToCharsD2(Math.Abs(utcOffset.Minutes), chars, offset);
				break;
			}
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
			return offset - num;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00016A20 File Offset: 0x00014C20
		public static bool IsWhitespace(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!XmlConverter.IsWhitespace(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016A4F File Offset: 0x00014C4F
		public static bool IsWhitespace(char ch)
		{
			return ch <= ' ' && (ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n');
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016A70 File Offset: 0x00014C70
		public static string StripWhitespace(string s)
		{
			int num = s.Length;
			for (int i = 0; i < s.Length; i++)
			{
				if (XmlConverter.IsWhitespace(s[i]))
				{
					num--;
				}
			}
			if (num == s.Length)
			{
				return s;
			}
			char[] array = new char[num];
			num = 0;
			foreach (char c in s)
			{
				if (!XmlConverter.IsWhitespace(c))
				{
					array[num++] = c;
				}
			}
			return new string(array);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016AF0 File Offset: 0x00014CF0
		private static string Trim(string s)
		{
			int num = 0;
			while (num < s.Length && XmlConverter.IsWhitespace(s[num]))
			{
				num++;
			}
			int num2 = s.Length;
			while (num2 > 0 && XmlConverter.IsWhitespace(s[num2 - 1]))
			{
				num2--;
			}
			if (num == 0 && num2 == s.Length)
			{
				return s;
			}
			if (num2 == 0)
			{
				return string.Empty;
			}
			return s.Substring(num, num2 - num);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016B5E File Offset: 0x00014D5E
		// Note: this type is marked as 'beforefieldinit'.
		static XmlConverter()
		{
		}

		// Token: 0x04000269 RID: 617
		public const int MaxDateTimeChars = 64;

		// Token: 0x0400026A RID: 618
		public const int MaxInt32Chars = 16;

		// Token: 0x0400026B RID: 619
		public const int MaxInt64Chars = 32;

		// Token: 0x0400026C RID: 620
		public const int MaxBoolChars = 5;

		// Token: 0x0400026D RID: 621
		public const int MaxFloatChars = 16;

		// Token: 0x0400026E RID: 622
		public const int MaxDoubleChars = 32;

		// Token: 0x0400026F RID: 623
		public const int MaxDecimalChars = 40;

		// Token: 0x04000270 RID: 624
		public const int MaxUInt64Chars = 32;

		// Token: 0x04000271 RID: 625
		public const int MaxPrimitiveChars = 64;

		// Token: 0x04000272 RID: 626
		private static char[] whiteSpaceChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};

		// Token: 0x04000273 RID: 627
		private static UTF8Encoding utf8Encoding;

		// Token: 0x04000274 RID: 628
		private static UnicodeEncoding unicodeEncoding;

		// Token: 0x04000275 RID: 629
		private static Base64Encoding base64Encoding;
	}
}
