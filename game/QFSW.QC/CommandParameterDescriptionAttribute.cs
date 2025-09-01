using System;

namespace QFSW.QC
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public sealed class CommandParameterDescriptionAttribute : Attribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002237 File Offset: 0x00000437
		public CommandParameterDescriptionAttribute(string description)
		{
			this.Description = description;
			this.Valid = !string.IsNullOrWhiteSpace(description);
		}

		// Token: 0x0400000E RID: 14
		public readonly string Description;

		// Token: 0x0400000F RID: 15
		public readonly bool Valid;
	}
}
