using System;
using System.Globalization;

// Token: 0x02000003 RID: 3
internal static class SR
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000206E File Offset: 0x0000026E
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000208F File Offset: 0x0000028F
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000209E File Offset: 0x0000029E
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020A9 File Offset: 0x000002A9
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x0400002A RID: 42
	public const string Argument_BadFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x0400002B RID: 43
	public const string Argument_InvalidNumberStyles = "An undefined NumberStyles value is being used.";

	// Token: 0x0400002C RID: 44
	public const string Argument_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	// Token: 0x0400002D RID: 45
	public const string Argument_MustBeBigInt = "The parameter must be a BigInteger.";

	// Token: 0x0400002E RID: 46
	public const string Format_TooLarge = "The value is too large to be represented by this format specifier.";

	// Token: 0x0400002F RID: 47
	public const string ArgumentOutOfRange_MustBeNonNeg = "The number must be greater than or equal to zero.";

	// Token: 0x04000030 RID: 48
	public const string Overflow_BigIntInfinity = "BigInteger cannot represent infinity.";

	// Token: 0x04000031 RID: 49
	public const string Overflow_NotANumber = "The value is not a number.";

	// Token: 0x04000032 RID: 50
	public const string Overflow_ParseBigInteger = "The value could not be parsed.";

	// Token: 0x04000033 RID: 51
	public const string Overflow_Int32 = "Value was either too large or too small for an Int32.";

	// Token: 0x04000034 RID: 52
	public const string Overflow_Int64 = "Value was either too large or too small for an Int64.";

	// Token: 0x04000035 RID: 53
	public const string Overflow_UInt32 = "Value was either too large or too small for a UInt32.";

	// Token: 0x04000036 RID: 54
	public const string Overflow_UInt64 = "Value was either too large or too small for a UInt64.";

	// Token: 0x04000037 RID: 55
	public const string Overflow_Decimal = "Value was either too large or too small for a Decimal.";

	// Token: 0x04000038 RID: 56
	public const string Arg_ArgumentOutOfRangeException = "Index was out of bounds:";

	// Token: 0x04000039 RID: 57
	public const string Arg_ElementsInSourceIsGreaterThanDestination = "Number of elements in source vector is greater than the destination array";

	// Token: 0x0400003A RID: 58
	public const string Arg_NullArgumentNullRef = "The method was called with a null array argument.";

	// Token: 0x0400003B RID: 59
	public const string Arg_TypeNotSupported = "Specified type is not supported";

	// Token: 0x0400003C RID: 60
	public const string ArgumentException_BufferNotFromPool = "The buffer is not associated with this pool and may not be returned to it.";

	// Token: 0x0400003D RID: 61
	public const string Overflow_Negative_Unsigned = "Negative values do not have an unsigned representation.";
}
