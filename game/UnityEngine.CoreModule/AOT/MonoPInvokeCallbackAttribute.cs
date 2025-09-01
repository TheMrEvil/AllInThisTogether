using System;

namespace AOT
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002059 File Offset: 0x00000259
		public MonoPInvokeCallbackAttribute(Type type)
		{
		}
	}
}
