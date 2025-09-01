using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	internal sealed class PerformanceCounterNameAttribute : Attribute
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000FAA1 File Offset: 0x0000DCA1
		public PerformanceCounterNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x040001E4 RID: 484
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
