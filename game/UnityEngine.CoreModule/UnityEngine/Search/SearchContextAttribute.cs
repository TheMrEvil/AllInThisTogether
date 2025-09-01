using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Search
{
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.Field)]
	public class SearchContextAttribute : PropertyAttribute
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00030B42 File Offset: 0x0002ED42
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00030B4A File Offset: 0x0002ED4A
		public string query
		{
			[CompilerGenerated]
			get
			{
				return this.<query>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<query>k__BackingField = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00030B53 File Offset: 0x0002ED53
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00030B5B File Offset: 0x0002ED5B
		public string[] providerIds
		{
			[CompilerGenerated]
			get
			{
				return this.<providerIds>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<providerIds>k__BackingField = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00030B64 File Offset: 0x0002ED64
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00030B6C File Offset: 0x0002ED6C
		public Type[] instantiableProviders
		{
			[CompilerGenerated]
			get
			{
				return this.<instantiableProviders>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<instantiableProviders>k__BackingField = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00030B75 File Offset: 0x0002ED75
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x00030B7D File Offset: 0x0002ED7D
		public SearchViewFlags flags
		{
			[CompilerGenerated]
			get
			{
				return this.<flags>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<flags>k__BackingField = value;
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00030B86 File Offset: 0x0002ED86
		public SearchContextAttribute(string query) : this(query, null, SearchViewFlags.None)
		{
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00030B93 File Offset: 0x0002ED93
		public SearchContextAttribute(string query, SearchViewFlags flags) : this(query, null, flags)
		{
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00030BA0 File Offset: 0x0002EDA0
		public SearchContextAttribute(string query, string providerIdsCommaSeparated) : this(query, providerIdsCommaSeparated, SearchViewFlags.None)
		{
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00030BAD File Offset: 0x0002EDAD
		public SearchContextAttribute(string query, string providerIdsCommaSeparated, SearchViewFlags flags) : this(query, flags, providerIdsCommaSeparated, null)
		{
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00030BBB File Offset: 0x0002EDBB
		public SearchContextAttribute(string query, params Type[] instantiableProviders) : this(query, SearchViewFlags.None, null, instantiableProviders)
		{
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00030BC9 File Offset: 0x0002EDC9
		public SearchContextAttribute(string query, SearchViewFlags flags, params Type[] instantiableProviders) : this(query, flags, null, instantiableProviders)
		{
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00030BD8 File Offset: 0x0002EDD8
		public SearchContextAttribute(string query, SearchViewFlags flags, string providerIdsCommaSeparated, params Type[] instantiableProviders)
		{
			this.query = ((string.IsNullOrEmpty(query) || query.EndsWith(" ")) ? query : (query + " "));
			this.providerIds = (((providerIdsCommaSeparated != null) ? providerIdsCommaSeparated.Split(new char[]
			{
				',',
				';'
			}) : null) ?? new string[0]);
			this.instantiableProviders = (instantiableProviders ?? new Type[0]);
			this.flags = flags;
		}

		// Token: 0x040009CB RID: 2507
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <query>k__BackingField;

		// Token: 0x040009CC RID: 2508
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string[] <providerIds>k__BackingField;

		// Token: 0x040009CD RID: 2509
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Type[] <instantiableProviders>k__BackingField;

		// Token: 0x040009CE RID: 2510
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SearchViewFlags <flags>k__BackingField;
	}
}
