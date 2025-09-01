using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CF RID: 1743
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NativeCallableAttribute : Attribute
	{
		// Token: 0x0600400F RID: 16399 RVA: 0x00002050 File Offset: 0x00000250
		public NativeCallableAttribute()
		{
		}

		// Token: 0x04002A13 RID: 10771
		public string EntryPoint;

		// Token: 0x04002A14 RID: 10772
		public CallingConvention CallingConvention;
	}
}
