using System;
using System.Runtime.CompilerServices;

// Token: 0x02000089 RID: 137
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x0600048C RID: 1164 RVA: 0x0000B414 File Offset: 0x00009614
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
