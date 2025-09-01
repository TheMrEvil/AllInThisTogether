using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000118 RID: 280
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x0600072F RID: 1839 RVA: 0x0001DF6C File Offset: 0x0001C16C
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

	// Token: 0x0400046F RID: 1135 RVA: 0x0001DFEC File Offset: 0x0001C1EC
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 2227CB07D78D81830B0B90D8CD29370C9E8A0D5ADD33A9105598708D5F7A9902;

	// Token: 0x04000470 RID: 1136 RVA: 0x0001DFF6 File Offset: 0x0001C1F6
	internal static readonly long 24B7E3A490F64223F93EC177ED5A641984B68F0783A289AC1F2C94D1D92DA684;

	// Token: 0x04000471 RID: 1137 RVA: 0x0001DFFE File Offset: 0x0001C1FE
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 2EF83B43314F8CD03190EEE30ECCF048DA37791237F27C62A579F23EACE9FD70;

	// Token: 0x04000472 RID: 1138 RVA: 0x0001E01E File Offset: 0x0001C21E
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 678BA95BDBDF562827A4FC9C4DB8ED2C2DA41FEEB1015535877D71C49D0077F2;

	// Token: 0x04000473 RID: 1139 RVA: 0x0001E11E File Offset: 0x0001C31E
	internal static readonly long E0E3CF58E8EBD3158219B64F434304727B1C71307D99BC27D059966A854CB749;

	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 10)]
	private struct __StaticArrayInitTypeSize=10
	{
	}

	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 32)]
	private struct __StaticArrayInitTypeSize=32
	{
	}

	// Token: 0x0200011B RID: 283
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 256)]
	private struct __StaticArrayInitTypeSize=256
	{
	}
}
