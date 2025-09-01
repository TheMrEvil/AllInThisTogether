using System;
using System.Runtime.InteropServices;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000053 RID: 83
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct ProfilerMarkerData
	{
		// Token: 0x04000157 RID: 343
		[FieldOffset(0)]
		public byte Type;

		// Token: 0x04000158 RID: 344
		[FieldOffset(1)]
		private readonly byte reserved0;

		// Token: 0x04000159 RID: 345
		[FieldOffset(2)]
		private readonly ushort reserved1;

		// Token: 0x0400015A RID: 346
		[FieldOffset(4)]
		public uint Size;

		// Token: 0x0400015B RID: 347
		[FieldOffset(8)]
		public unsafe void* Ptr;
	}
}
