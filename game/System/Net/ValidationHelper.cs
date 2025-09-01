using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005E0 RID: 1504
	internal static class ValidationHelper
	{
		// Token: 0x0600304F RID: 12367 RVA: 0x000A68EA File Offset: 0x000A4AEA
		public static string[] MakeEmptyArrayNull(string[] stringArray)
		{
			if (stringArray == null || stringArray.Length == 0)
			{
				return null;
			}
			return stringArray;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000A68F6 File Offset: 0x000A4AF6
		public static string MakeStringNull(string stringValue)
		{
			if (stringValue == null || stringValue.Length == 0)
			{
				return null;
			}
			return stringValue;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000A6906 File Offset: 0x000A4B06
		public static string ExceptionMessage(Exception exception)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (exception.InnerException == null)
			{
				return exception.Message;
			}
			return exception.Message + " (" + ValidationHelper.ExceptionMessage(exception.InnerException) + ")";
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000A6940 File Offset: 0x000A4B40
		public static string ToString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			if (objectValue is Exception)
			{
				return ValidationHelper.ExceptionMessage(objectValue as Exception);
			}
			if (objectValue is IntPtr)
			{
				return "0x" + ((IntPtr)objectValue).ToString("x");
			}
			return objectValue.ToString();
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000A69B4 File Offset: 0x000A4BB4
		public static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000A69F8 File Offset: 0x000A4BF8
		public static bool IsInvalidHttpString(string stringValue)
		{
			return stringValue.IndexOfAny(ValidationHelper.InvalidParamChars) != -1;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000A6A0B File Offset: 0x000A4C0B
		public static bool IsBlankString(string stringValue)
		{
			return stringValue == null || stringValue.Length == 0;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0009A1C3 File Offset: 0x000983C3
		public static bool ValidateTcpPort(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x00099E50 File Offset: 0x00098050
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000A6A1C File Offset: 0x000A4C1C
		internal static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000A6A76 File Offset: 0x000A4C76
		// Note: this type is marked as 'beforefieldinit'.
		static ValidationHelper()
		{
		}

		// Token: 0x04001B0E RID: 6926
		public static string[] EmptyArray = new string[0];

		// Token: 0x04001B0F RID: 6927
		internal static readonly char[] InvalidMethodChars = new char[]
		{
			' ',
			'\r',
			'\n',
			'\t'
		};

		// Token: 0x04001B10 RID: 6928
		internal static readonly char[] InvalidParamChars = new char[]
		{
			'(',
			')',
			'<',
			'>',
			'@',
			',',
			';',
			':',
			'\\',
			'"',
			'\'',
			'/',
			'[',
			']',
			'?',
			'=',
			'{',
			'}',
			' ',
			'\t',
			'\r',
			'\n'
		};
	}
}
