using System;
using System.Runtime.CompilerServices;

// Token: 0x02000015 RID: 21
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x06000033 RID: 51 RVA: 0x00002C10 File Offset: 0x00000E10
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
