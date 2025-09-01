using System;
using System.Runtime.InteropServices;

namespace IKVM.Reflection
{
	// Token: 0x02000076 RID: 118
	[StructLayout(LayoutKind.Explicit)]
	internal struct SingleConverter
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x00013C74 File Offset: 0x00011E74
		internal static int SingleToInt32Bits(float v)
		{
			return new SingleConverter
			{
				f = v
			}.i;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00013C98 File Offset: 0x00011E98
		internal static float Int32BitsToSingle(int v)
		{
			return new SingleConverter
			{
				i = v
			}.f;
		}

		// Token: 0x04000277 RID: 631
		[FieldOffset(0)]
		private int i;

		// Token: 0x04000278 RID: 632
		[FieldOffset(0)]
		private float f;
	}
}
