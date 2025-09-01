using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001C4 RID: 452
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x0000259F File Offset: 0x0000079F
		public Pinnable()
		{
		}

		// Token: 0x04001443 RID: 5187
		public T Data;
	}
}
