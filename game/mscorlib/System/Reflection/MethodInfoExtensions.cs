using System;

namespace System.Reflection
{
	// Token: 0x020008D8 RID: 2264
	public static class MethodInfoExtensions
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x000F0838 File Offset: 0x000EEA38
		public static MethodInfo GetBaseDefinition(MethodInfo method)
		{
			Requires.NotNull(method, "method");
			return method.GetBaseDefinition();
		}
	}
}
