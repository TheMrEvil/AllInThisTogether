using System;

namespace QFSW.QC
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public sealed class CommandDescriptionAttribute : Attribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002219 File Offset: 0x00000419
		public CommandDescriptionAttribute(string description)
		{
			this.Description = description;
			this.Valid = !string.IsNullOrWhiteSpace(description);
		}

		// Token: 0x0400000C RID: 12
		public readonly string Description;

		// Token: 0x0400000D RID: 13
		public readonly bool Valid;
	}
}
