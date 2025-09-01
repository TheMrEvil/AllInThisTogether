using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001EB RID: 491
	internal struct NetErrorMessage
	{
		// Token: 0x04000BE3 RID: 3043
		[FixedBuffer(typeof(char), 1024)]
		public NetErrorMessage.<Value>e__FixedBuffer Value;

		// Token: 0x02000284 RID: 644
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 2048)]
		public struct <Value>e__FixedBuffer
		{
			// Token: 0x04000F0A RID: 3850
			public char FixedElementField;
		}
	}
}
