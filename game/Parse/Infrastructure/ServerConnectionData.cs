using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x0200004C RID: 76
	public struct ServerConnectionData : IServerConnectionData
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000C503 File Offset: 0x0000A703
		internal bool Test
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Test>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Test>k__BackingField = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000C50C File Offset: 0x0000A70C
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000C514 File Offset: 0x0000A714
		public string ApplicationID
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<ApplicationID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ApplicationID>k__BackingField = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000C51D File Offset: 0x0000A71D
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000C525 File Offset: 0x0000A725
		public string ServerURI
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<ServerURI>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerURI>k__BackingField = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000C52E File Offset: 0x0000A72E
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000C536 File Offset: 0x0000A736
		public string Key
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Key>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Key>k__BackingField = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000C53F File Offset: 0x0000A73F
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000C547 File Offset: 0x0000A747
		public string MasterKey
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<MasterKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MasterKey>k__BackingField = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000C550 File Offset: 0x0000A750
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000C558 File Offset: 0x0000A758
		public IDictionary<string, string> Headers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Headers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Headers>k__BackingField = value;
			}
		}

		// Token: 0x040000BE RID: 190
		[CompilerGenerated]
		private bool <Test>k__BackingField;

		// Token: 0x040000BF RID: 191
		[CompilerGenerated]
		private string <ApplicationID>k__BackingField;

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		private string <ServerURI>k__BackingField;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		private string <Key>k__BackingField;

		// Token: 0x040000C2 RID: 194
		[CompilerGenerated]
		private string <MasterKey>k__BackingField;

		// Token: 0x040000C3 RID: 195
		[CompilerGenerated]
		private IDictionary<string, string> <Headers>k__BackingField;
	}
}
