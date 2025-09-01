using System;

// Token: 0x0200003B RID: 59
internal sealed class Locale
{
	// Token: 0x06000074 RID: 116 RVA: 0x0000259F File Offset: 0x0000079F
	private Locale()
	{
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000270D File Offset: 0x0000090D
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00002710 File Offset: 0x00000910
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
