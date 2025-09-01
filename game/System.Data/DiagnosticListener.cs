using System;

// Token: 0x0200001D RID: 29
internal class DiagnosticListener
{
	// Token: 0x060000D5 RID: 213 RVA: 0x00003D93 File Offset: 0x00001F93
	internal DiagnosticListener(string s)
	{
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000573D File Offset: 0x0000393D
	internal bool IsEnabled(string s)
	{
		return DiagnosticListener.DiagnosticListenerEnabled;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00005744 File Offset: 0x00003944
	internal void Write(string s1, object s2)
	{
		Console.WriteLine(string.Format("|| {0},  {1}", s1, s2));
	}

	// Token: 0x040003FF RID: 1023
	internal static bool DiagnosticListenerEnabled;
}
