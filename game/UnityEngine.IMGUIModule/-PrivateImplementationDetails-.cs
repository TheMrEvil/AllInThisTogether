using System;
using System.Runtime.CompilerServices;

// Token: 0x02000043 RID: 67
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x060004A0 RID: 1184 RVA: 0x00012FAC File Offset: 0x000111AC
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
