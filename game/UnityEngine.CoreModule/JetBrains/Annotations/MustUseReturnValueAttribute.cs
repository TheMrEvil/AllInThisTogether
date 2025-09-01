using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000C8 RID: 200
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00002059 File Offset: 0x00000259
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00005E03 File Offset: 0x00004003
		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.Justification = justification;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00005E14 File Offset: 0x00004014
		[CanBeNull]
		public string Justification
		{
			[CompilerGenerated]
			get
			{
				return this.<Justification>k__BackingField;
			}
		}

		// Token: 0x0400025A RID: 602
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <Justification>k__BackingField;
	}
}
