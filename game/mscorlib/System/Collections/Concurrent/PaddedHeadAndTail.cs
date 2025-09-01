using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A56 RID: 2646
	[DebuggerDisplay("Head = {Head}, Tail = {Tail}")]
	[StructLayout(LayoutKind.Explicit, Size = 384)]
	internal struct PaddedHeadAndTail
	{
		// Token: 0x04003934 RID: 14644
		[FieldOffset(128)]
		public int Head;

		// Token: 0x04003935 RID: 14645
		[FieldOffset(256)]
		public int Tail;
	}
}
