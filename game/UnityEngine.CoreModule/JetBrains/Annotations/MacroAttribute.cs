using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000CC RID: 204
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class MacroAttribute : Attribute
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00005E35 File Offset: 0x00004035
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00005E3D File Offset: 0x0000403D
		[CanBeNull]
		public string Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expression>k__BackingField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00005E46 File Offset: 0x00004046
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00005E4E File Offset: 0x0000404E
		public int Editable
		{
			[CompilerGenerated]
			get
			{
				return this.<Editable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Editable>k__BackingField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00005E57 File Offset: 0x00004057
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00005E5F File Offset: 0x0000405F
		[CanBeNull]
		public string Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Target>k__BackingField = value;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00002050 File Offset: 0x00000250
		public MacroAttribute()
		{
		}

		// Token: 0x0400025C RID: 604
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <Expression>k__BackingField;

		// Token: 0x0400025D RID: 605
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <Editable>k__BackingField;

		// Token: 0x0400025E RID: 606
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Target>k__BackingField;
	}
}
