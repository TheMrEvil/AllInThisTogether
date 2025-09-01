using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000207 RID: 519
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x06001067 RID: 4199 RVA: 0x0001B2F4 File Offset: 0x000194F4
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

	// Token: 0x04000C39 RID: 3129 RVA: 0x00086E18 File Offset: 0x00085018
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=5 B1A9AA820F353E1BEF1F7D40CD3F58447AA91D123BC2539918BC70F8A66E75B9;

	// Token: 0x02000296 RID: 662
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 5)]
	private struct __StaticArrayInitTypeSize=5
	{
	}
}
