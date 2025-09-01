using System;
using System.Runtime.CompilerServices;

// Token: 0x020000A0 RID: 160
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x060005BC RID: 1468 RVA: 0x00012C84 File Offset: 0x00010E84
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619U;
			}
		}
		return num;
	}
}
