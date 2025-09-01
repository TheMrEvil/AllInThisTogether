using System;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
	// Token: 0x02000023 RID: 35
	internal sealed class Match
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005E96 File Offset: 0x00004096
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005E9E File Offset: 0x0000409E
		internal MatchState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005EA7 File Offset: 0x000040A7
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005EAF File Offset: 0x000040AF
		internal int Position
		{
			[CompilerGenerated]
			get
			{
				return this.<Position>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Position>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005EB8 File Offset: 0x000040B8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005EC0 File Offset: 0x000040C0
		internal int Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Length>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005EC9 File Offset: 0x000040C9
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005ED1 File Offset: 0x000040D1
		internal byte Symbol
		{
			[CompilerGenerated]
			get
			{
				return this.<Symbol>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Symbol>k__BackingField = value;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000353B File Offset: 0x0000173B
		public Match()
		{
		}

		// Token: 0x04000155 RID: 341
		[CompilerGenerated]
		private MatchState <State>k__BackingField;

		// Token: 0x04000156 RID: 342
		[CompilerGenerated]
		private int <Position>k__BackingField;

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		private int <Length>k__BackingField;

		// Token: 0x04000158 RID: 344
		[CompilerGenerated]
		private byte <Symbol>k__BackingField;
	}
}
