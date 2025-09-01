using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse
{
	// Token: 0x02000017 RID: 23
	[ParseClassName("_Session")]
	public class ParseSession : ParseObject
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006E71 File Offset: 0x00005071
		private static HashSet<string> ImmutableKeys
		{
			[CompilerGenerated]
			get
			{
				return ParseSession.<ImmutableKeys>k__BackingField;
			}
		} = new HashSet<string>
		{
			"sessionToken",
			"createdWith",
			"restricted",
			"user",
			"expiresAt",
			"installationId"
		};

		// Token: 0x0600015C RID: 348 RVA: 0x00006E78 File Offset: 0x00005078
		protected override bool CheckKeyMutable(string key)
		{
			return !ParseSession.ImmutableKeys.Contains(key);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006E88 File Offset: 0x00005088
		[ParseFieldName("sessionToken")]
		public string SessionToken
		{
			get
			{
				return base.GetProperty<string>(null, "SessionToken");
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006E96 File Offset: 0x00005096
		public ParseSession() : base(null)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006EA0 File Offset: 0x000050A0
		// Note: this type is marked as 'beforefieldinit'.
		static ParseSession()
		{
		}

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		private static readonly HashSet<string> <ImmutableKeys>k__BackingField;
	}
}
