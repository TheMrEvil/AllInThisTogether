using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001F1 RID: 497
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00023F04 File Offset: 0x00022104
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x00023F0C File Offset: 0x0002210C
		public string menuName
		{
			[CompilerGenerated]
			get
			{
				return this.<menuName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuName>k__BackingField = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00023F15 File Offset: 0x00022115
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00023F1D File Offset: 0x0002211D
		public string fileName
		{
			[CompilerGenerated]
			get
			{
				return this.<fileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<fileName>k__BackingField = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00023F26 File Offset: 0x00022126
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x00023F2E File Offset: 0x0002212E
		public int order
		{
			[CompilerGenerated]
			get
			{
				return this.<order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<order>k__BackingField = value;
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00002050 File Offset: 0x00000250
		public CreateAssetMenuAttribute()
		{
		}

		// Token: 0x040007D4 RID: 2004
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <menuName>k__BackingField;

		// Token: 0x040007D5 RID: 2005
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <fileName>k__BackingField;

		// Token: 0x040007D6 RID: 2006
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <order>k__BackingField;
	}
}
