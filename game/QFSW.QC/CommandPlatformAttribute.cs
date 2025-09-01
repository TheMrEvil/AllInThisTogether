using System;

namespace QFSW.QC
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public sealed class CommandPlatformAttribute : Attribute
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002255 File Offset: 0x00000455
		public CommandPlatformAttribute(Platform supportedPlatforms)
		{
			this.SupportedPlatforms = supportedPlatforms;
		}

		// Token: 0x04000010 RID: 16
		public readonly Platform SupportedPlatforms;
	}
}
