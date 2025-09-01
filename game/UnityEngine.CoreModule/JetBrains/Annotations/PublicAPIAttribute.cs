using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000C5 RID: 197
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	public sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00002059 File Offset: 0x00000259
		public PublicAPIAttribute()
		{
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00005DEA File Offset: 0x00003FEA
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00005DFB File Offset: 0x00003FFB
		[CanBeNull]
		public string Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
		}

		// Token: 0x04000259 RID: 601
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string <Comment>k__BackingField;
	}
}
