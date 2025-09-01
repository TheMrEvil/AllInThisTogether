using System;

namespace Steamworks
{
	// Token: 0x0200017F RID: 383
	public abstract class Callback
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060008A6 RID: 2214
		public abstract bool IsGameServer { get; }

		// Token: 0x060008A7 RID: 2215
		internal abstract Type GetCallbackType();

		// Token: 0x060008A8 RID: 2216
		internal abstract void OnRunCallback(IntPtr pvParam);

		// Token: 0x060008A9 RID: 2217
		internal abstract void SetUnregistered();

		// Token: 0x060008AA RID: 2218 RVA: 0x0000C9E6 File Offset: 0x0000ABE6
		protected Callback()
		{
		}
	}
}
