using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks.Data
{
	// Token: 0x020001FF RID: 511
	internal struct NumericalFilter
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0001A954 File Offset: 0x00018B54
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x0001A95C File Offset: 0x00018B5C
		public string Key
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Key>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Key>k__BackingField = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0001A965 File Offset: 0x00018B65
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0001A96D File Offset: 0x00018B6D
		public int Value
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0001A976 File Offset: 0x00018B76
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0001A97E File Offset: 0x00018B7E
		public LobbyComparison Comparer
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Comparer>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Comparer>k__BackingField = value;
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0001A987 File Offset: 0x00018B87
		internal NumericalFilter(string k, int v, LobbyComparison c)
		{
			this.Key = k;
			this.Value = v;
			this.Comparer = c;
		}

		// Token: 0x04000C14 RID: 3092
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Key>k__BackingField;

		// Token: 0x04000C15 RID: 3093
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Value>k__BackingField;

		// Token: 0x04000C16 RID: 3094
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LobbyComparison <Comparer>k__BackingField;
	}
}
