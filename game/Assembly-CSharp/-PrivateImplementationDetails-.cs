using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x020003DE RID: 990
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x06002034 RID: 8244 RVA: 0x000BFB20 File Offset: 0x000BDD20
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

	// Token: 0x040020A6 RID: 8358 RVA: 0x001C3C28 File Offset: 0x001C1E28
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 0FAD06BD37AEC59D857758AA276036320DD60C02A301BC5A37A49C9C75088860;

	// Token: 0x040020A7 RID: 8359 RVA: 0x001C3C38 File Offset: 0x001C1E38
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 2A7F4E0D38F8D73405F2D2745935534748CEBE51C1D4C0F68A2C71F06666C3E1;

	// Token: 0x040020A8 RID: 8360 RVA: 0x001C3C50 File Offset: 0x001C1E50
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 BAED642339816AFFB3FE8719792D0E4CE82F12DB72B7373D244EAA65445800FE;

	// Token: 0x040020A9 RID: 8361 RVA: 0x001C3C60 File Offset: 0x001C1E60
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=28 C0229AC64E837266486BE6F7DA406C51AF5B363E2ED607D3558DE586C8F2357B;

	// Token: 0x040020AA RID: 8362 RVA: 0x001C3C80 File Offset: 0x001C1E80
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 CD82F8CBBAE0E08AD97DD079F1608973F971CABDB23C2762E7366147665A9CFD;

	// Token: 0x040020AB RID: 8363 RVA: 0x001C3C98 File Offset: 0x001C1E98
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 CE18F5C9B62E24ECE371F92F5BBDB067A5A59A86E5D0F3ECFFF02E17DA6446D2;

	// Token: 0x040020AC RID: 8364 RVA: 0x001C3CA8 File Offset: 0x001C1EA8
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 FF1F6EE5D67458CFAC950F62E93042E21FCB867E2234DCC8721801231064AD40;

	// Token: 0x020006AA RID: 1706
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 16)]
	private struct __StaticArrayInitTypeSize=16
	{
	}

	// Token: 0x020006AB RID: 1707
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	private struct __StaticArrayInitTypeSize=20
	{
	}

	// Token: 0x020006AC RID: 1708
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
	private struct __StaticArrayInitTypeSize=28
	{
	}

	// Token: 0x020006AD RID: 1709
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 32)]
	private struct __StaticArrayInitTypeSize=32
	{
	}
}
