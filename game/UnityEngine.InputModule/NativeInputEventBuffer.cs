using System;
using System.Runtime.InteropServices;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000004 RID: 4
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	internal struct NativeInputEventBuffer
	{
		// Token: 0x04000007 RID: 7
		public const int structSize = 20;

		// Token: 0x04000008 RID: 8
		[FieldOffset(0)]
		public unsafe void* eventBuffer;

		// Token: 0x04000009 RID: 9
		[FieldOffset(8)]
		public int eventCount;

		// Token: 0x0400000A RID: 10
		[FieldOffset(12)]
		public int sizeInBytes;

		// Token: 0x0400000B RID: 11
		[FieldOffset(16)]
		public int capacityInBytes;
	}
}
