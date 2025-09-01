using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Android
{
	// Token: 0x02000013 RID: 19
	public class AndroidAssetPackUseMobileDataRequestResult
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00007DDA File Offset: 0x00005FDA
		internal AndroidAssetPackUseMobileDataRequestResult(bool allowed)
		{
			this.allowed = allowed;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007DEB File Offset: 0x00005FEB
		public bool allowed
		{
			[CompilerGenerated]
			get
			{
				return this.<allowed>k__BackingField;
			}
		}

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <allowed>k__BackingField;
	}
}
