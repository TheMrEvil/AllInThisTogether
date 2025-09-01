using System;

namespace System.Diagnostics
{
	// Token: 0x020009AF RID: 2479
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class StackTraceHiddenAttribute : Attribute
	{
		// Token: 0x0600598C RID: 22924 RVA: 0x00002050 File Offset: 0x00000250
		public StackTraceHiddenAttribute()
		{
		}
	}
}
