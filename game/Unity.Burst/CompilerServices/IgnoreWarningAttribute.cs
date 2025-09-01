using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class IgnoreWarningAttribute : Attribute
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000079B4 File Offset: 0x00005BB4
		public IgnoreWarningAttribute(int warning)
		{
		}
	}
}
