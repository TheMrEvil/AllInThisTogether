using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001EC RID: 492
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct NetKeyValue
	{
		// Token: 0x04000BE4 RID: 3044
		[FieldOffset(0)]
		internal NetConfig Value;

		// Token: 0x04000BE5 RID: 3045
		[FieldOffset(4)]
		internal NetConfigType DataType;

		// Token: 0x04000BE6 RID: 3046
		[FieldOffset(8)]
		internal long Int64Value;

		// Token: 0x04000BE7 RID: 3047
		[FieldOffset(8)]
		internal int Int32Value;

		// Token: 0x04000BE8 RID: 3048
		[FieldOffset(8)]
		internal float FloatValue;

		// Token: 0x04000BE9 RID: 3049
		[FieldOffset(8)]
		internal IntPtr PointerValue;
	}
}
