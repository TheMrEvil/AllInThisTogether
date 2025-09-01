using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002C RID: 44
	internal class ListMatchRequest : Request
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005D12 File Offset: 0x00003F12
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00005D1A File Offset: 0x00003F1A
		public int pageSize
		{
			[CompilerGenerated]
			get
			{
				return this.<pageSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pageSize>k__BackingField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005D23 File Offset: 0x00003F23
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005D2B File Offset: 0x00003F2B
		public int pageNum
		{
			[CompilerGenerated]
			get
			{
				return this.<pageNum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<pageNum>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005D34 File Offset: 0x00003F34
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00005D3C File Offset: 0x00003F3C
		public string nameFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<nameFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<nameFilter>k__BackingField = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005D45 File Offset: 0x00003F45
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005D4D File Offset: 0x00003F4D
		public bool filterOutPrivateMatches
		{
			[CompilerGenerated]
			get
			{
				return this.<filterOutPrivateMatches>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<filterOutPrivateMatches>k__BackingField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005D56 File Offset: 0x00003F56
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00005D5E File Offset: 0x00003F5E
		public int eloScore
		{
			[CompilerGenerated]
			get
			{
				return this.<eloScore>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<eloScore>k__BackingField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00005D67 File Offset: 0x00003F67
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00005D6F File Offset: 0x00003F6F
		public Dictionary<string, long> matchAttributeFilterLessThan
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAttributeFilterLessThan>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<matchAttributeFilterLessThan>k__BackingField = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005D78 File Offset: 0x00003F78
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005D80 File Offset: 0x00003F80
		public Dictionary<string, long> matchAttributeFilterEqualTo
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAttributeFilterEqualTo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<matchAttributeFilterEqualTo>k__BackingField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005D89 File Offset: 0x00003F89
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00005D91 File Offset: 0x00003F91
		public Dictionary<string, long> matchAttributeFilterGreaterThan
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAttributeFilterGreaterThan>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<matchAttributeFilterGreaterThan>k__BackingField = value;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005D9C File Offset: 0x00003F9C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-pageSize:{1},pageNum:{2},nameFilter:{3}, filterOutPrivateMatches:{4}, eloScore:{5}, matchAttributeFilterLessThan.Count:{6}, matchAttributeFilterEqualTo.Count:{7}, matchAttributeFilterGreaterThan.Count:{8}", new object[]
			{
				base.ToString(),
				this.pageSize,
				this.pageNum,
				this.nameFilter,
				this.filterOutPrivateMatches,
				this.eloScore,
				(this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count,
				(this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count,
				(this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005E64 File Offset: 0x00004064
		public override bool IsValid()
		{
			int num = (this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count;
			num += ((this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count);
			num += ((this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count);
			return base.IsValid() && this.pageSize >= 1 && this.pageSize <= 1000 && num <= 10;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000595F File Offset: 0x00003B5F
		public ListMatchRequest()
		{
		}

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pageSize>k__BackingField;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pageNum>k__BackingField;

		// Token: 0x040000C2 RID: 194
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <nameFilter>k__BackingField;

		// Token: 0x040000C3 RID: 195
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <filterOutPrivateMatches>k__BackingField;

		// Token: 0x040000C4 RID: 196
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <eloScore>k__BackingField;

		// Token: 0x040000C5 RID: 197
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Dictionary<string, long> <matchAttributeFilterLessThan>k__BackingField;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, long> <matchAttributeFilterEqualTo>k__BackingField;

		// Token: 0x040000C7 RID: 199
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Dictionary<string, long> <matchAttributeFilterGreaterThan>k__BackingField;

		// Token: 0x040000C8 RID: 200
		[Obsolete("This bool is deprecated in favor of filterOutPrivateMatches")]
		public bool includePasswordMatches;
	}
}
