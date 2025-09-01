using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000742 RID: 1858
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x040022C6 RID: 8902
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x040022C7 RID: 8903
		[FieldOffset(0)]
		public int Length;

		// Token: 0x040022C8 RID: 8904
		[FieldOffset(4)]
		public int IfIndex;
	}
}
