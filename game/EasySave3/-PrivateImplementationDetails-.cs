using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x020000EB RID: 235
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x0600054E RID: 1358 RVA: 0x0001F468 File Offset: 0x0001D668
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

	// Token: 0x04000187 RID: 391 RVA: 0x00047024 File Offset: 0x00045224
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=6 E4ADC6F67998BC92BA402D8A1450BAEDFCAE3F99F870FE71B1DDCFA4C625D41C;

	// Token: 0x02000113 RID: 275
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 6)]
	private struct __StaticArrayInitTypeSize=6
	{
	}
}
