using System;

namespace Mono.Interop
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00002050 File Offset: 0x00000250
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
