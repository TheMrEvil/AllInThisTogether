using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200023E RID: 574
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class MonoCQItem
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x0000259F File Offset: 0x0000079F
		public MonoCQItem()
		{
		}

		// Token: 0x04001727 RID: 5927
		private object[] array;

		// Token: 0x04001728 RID: 5928
		private byte[] array_state;

		// Token: 0x04001729 RID: 5929
		private int head;

		// Token: 0x0400172A RID: 5930
		private int tail;
	}
}
