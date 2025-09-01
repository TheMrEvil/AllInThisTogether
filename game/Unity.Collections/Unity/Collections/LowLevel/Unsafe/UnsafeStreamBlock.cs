using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014A RID: 330
	[BurstCompatible]
	internal struct UnsafeStreamBlock
	{
		// Token: 0x040003D5 RID: 981
		internal unsafe UnsafeStreamBlock* Next;

		// Token: 0x040003D6 RID: 982
		[FixedBuffer(typeof(byte), 1)]
		internal UnsafeStreamBlock.<Data>e__FixedBuffer Data;

		// Token: 0x0200014B RID: 331
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <Data>e__FixedBuffer
		{
			// Token: 0x040003D7 RID: 983
			public byte FixedElementField;
		}
	}
}
