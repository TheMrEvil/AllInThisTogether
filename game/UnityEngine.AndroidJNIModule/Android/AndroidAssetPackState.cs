using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Android
{
	// Token: 0x02000012 RID: 18
	public class AndroidAssetPackState
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007DA3 File Offset: 0x00005FA3
		internal AndroidAssetPackState(string name, AndroidAssetPackStatus status, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.error = error;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007DC2 File Offset: 0x00005FC2
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007DCA File Offset: 0x00005FCA
		public AndroidAssetPackStatus status
		{
			[CompilerGenerated]
			get
			{
				return this.<status>k__BackingField;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007DD2 File Offset: 0x00005FD2
		public AndroidAssetPackError error
		{
			[CompilerGenerated]
			get
			{
				return this.<error>k__BackingField;
			}
		}

		// Token: 0x0400003C RID: 60
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <name>k__BackingField;

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly AndroidAssetPackStatus <status>k__BackingField;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly AndroidAssetPackError <error>k__BackingField;
	}
}
