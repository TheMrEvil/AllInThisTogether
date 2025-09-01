using System;

// Token: 0x0200001B RID: 27
internal sealed class Locale
{
	// Token: 0x060000C7 RID: 199 RVA: 0x00003D93 File Offset: 0x00001F93
	private Locale()
	{
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00005696 File Offset: 0x00003896
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005699 File Offset: 0x00003899
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
