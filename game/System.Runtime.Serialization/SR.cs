using System;
using System.Globalization;

// Token: 0x02000002 RID: 2
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
}
