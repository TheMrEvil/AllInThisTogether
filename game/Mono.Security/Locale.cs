using System;

// Token: 0x02000004 RID: 4
internal sealed class Locale
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private Locale()
	{
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000205B File Offset: 0x0000025B
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
