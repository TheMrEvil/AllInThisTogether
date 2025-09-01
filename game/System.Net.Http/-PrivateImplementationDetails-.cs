using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x0200006D RID: 109
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x0000D870 File Offset: 0x0000BA70
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

	// Token: 0x04000158 RID: 344 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=127 1D59178A3E2B293760F6FE72820F96FEC4071964A5B9E4BB13F7EA51510A4729;

	// Token: 0x0200006E RID: 110
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 127)]
	private struct __StaticArrayInitTypeSize=127
	{
	}
}
