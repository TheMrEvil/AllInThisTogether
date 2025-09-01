using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000025 RID: 37
	internal class CreateMatchRequest : Request
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000057E7 File Offset: 0x000039E7
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000057EF File Offset: 0x000039EF
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005800 File Offset: 0x00003A00
		public uint size
		{
			[CompilerGenerated]
			get
			{
				return this.<size>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<size>k__BackingField = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005809 File Offset: 0x00003A09
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005811 File Offset: 0x00003A11
		public string publicAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<publicAddress>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<publicAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000581A File Offset: 0x00003A1A
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005822 File Offset: 0x00003A22
		public string privateAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<privateAddress>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<privateAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000582B File Offset: 0x00003A2B
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005833 File Offset: 0x00003A33
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005844 File Offset: 0x00003A44
		public bool advertise
		{
			[CompilerGenerated]
			get
			{
				return this.<advertise>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<advertise>k__BackingField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000584D File Offset: 0x00003A4D
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005855 File Offset: 0x00003A55
		public string password
		{
			[CompilerGenerated]
			get
			{
				return this.<password>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<password>k__BackingField = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000585E File Offset: 0x00003A5E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00005866 File Offset: 0x00003A66
		public Dictionary<string, long> matchAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAttributes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<matchAttributes>k__BackingField = value;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005870 File Offset: 0x00003A70
		public override string ToString()
		{
			return UnityString.Format("[{0}]-name:{1},size:{2},publicAddress:{3},privateAddress:{4},eloScore:{5},advertise:{6},HasPassword:{7},matchAttributes.Count:{8}", new object[]
			{
				base.ToString(),
				this.name,
				this.size,
				this.publicAddress,
				this.privateAddress,
				this.eloScore,
				this.advertise,
				string.IsNullOrEmpty(this.password) ? "NO" : "YES",
				(this.matchAttributes == null) ? 0 : this.matchAttributes.Count
			});
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000591C File Offset: 0x00003B1C
		public override bool IsValid()
		{
			return base.IsValid() && this.size >= 2U && (this.matchAttributes == null || this.matchAttributes.Count <= 10);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000595F File Offset: 0x00003B5F
		public CreateMatchRequest()
		{
		}

		// Token: 0x040000A0 RID: 160
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <name>k__BackingField;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <size>k__BackingField;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <publicAddress>k__BackingField;

		// Token: 0x040000A3 RID: 163
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <privateAddress>k__BackingField;

		// Token: 0x040000A4 RID: 164
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <eloScore>k__BackingField;

		// Token: 0x040000A5 RID: 165
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <advertise>k__BackingField;

		// Token: 0x040000A6 RID: 166
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <password>k__BackingField;

		// Token: 0x040000A7 RID: 167
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Dictionary<string, long> <matchAttributes>k__BackingField;
	}
}
