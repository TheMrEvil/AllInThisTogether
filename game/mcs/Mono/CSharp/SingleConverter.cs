using System;
using System.Runtime.InteropServices;

namespace Mono.CSharp
{
	// Token: 0x02000280 RID: 640
	[StructLayout(LayoutKind.Explicit)]
	internal struct SingleConverter
	{
		// Token: 0x06001F4D RID: 8013 RVA: 0x0009A304 File Offset: 0x00098504
		public static int SingleToInt32Bits(float v)
		{
			return new SingleConverter
			{
				f = v
			}.i;
		}

		// Token: 0x04000B7B RID: 2939
		[FieldOffset(0)]
		private int i;

		// Token: 0x04000B7C RID: 2940
		[FieldOffset(0)]
		private float f;
	}
}
