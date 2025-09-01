using System;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x020002A5 RID: 677
	internal class SspiClientContextStatus
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00092FD6 File Offset: 0x000911D6
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00092FDE File Offset: 0x000911DE
		public SafeFreeCredentials CredentialsHandle
		{
			[CompilerGenerated]
			get
			{
				return this.<CredentialsHandle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CredentialsHandle>k__BackingField = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00092FE7 File Offset: 0x000911E7
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00092FEF File Offset: 0x000911EF
		public SafeDeleteContext SecurityContext
		{
			[CompilerGenerated]
			get
			{
				return this.<SecurityContext>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SecurityContext>k__BackingField = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x00092FF8 File Offset: 0x000911F8
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x00093000 File Offset: 0x00091200
		public ContextFlagsPal ContextFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<ContextFlags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContextFlags>k__BackingField = value;
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00003D93 File Offset: 0x00001F93
		public SspiClientContextStatus()
		{
		}

		// Token: 0x0400159B RID: 5531
		[CompilerGenerated]
		private SafeFreeCredentials <CredentialsHandle>k__BackingField;

		// Token: 0x0400159C RID: 5532
		[CompilerGenerated]
		private SafeDeleteContext <SecurityContext>k__BackingField;

		// Token: 0x0400159D RID: 5533
		[CompilerGenerated]
		private ContextFlagsPal <ContextFlags>k__BackingField;
	}
}
