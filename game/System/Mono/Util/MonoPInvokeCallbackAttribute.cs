using System;
using System.Diagnostics;

namespace Mono.Util
{
	// Token: 0x0200003D RID: 61
	[Conditional("FULL_AOT_RUNTIME")]
	[Conditional("UNITY")]
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("MONOTOUCH")]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00003D9F File Offset: 0x00001F9F
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
