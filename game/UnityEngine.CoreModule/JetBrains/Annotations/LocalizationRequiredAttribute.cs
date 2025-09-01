using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00005D15 File Offset: 0x00003F15
		public LocalizationRequiredAttribute() : this(true)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00005D20 File Offset: 0x00003F20
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00005D31 File Offset: 0x00003F31
		public bool Required
		{
			[CompilerGenerated]
			get
			{
				return this.<Required>k__BackingField;
			}
		}

		// Token: 0x04000248 RID: 584
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <Required>k__BackingField;
	}
}
