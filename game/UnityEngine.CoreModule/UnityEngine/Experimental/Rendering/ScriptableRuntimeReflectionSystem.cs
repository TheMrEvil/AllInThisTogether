using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000471 RID: 1137
	public abstract class ScriptableRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x00042F14 File Offset: 0x00041114
		public virtual bool TickRealtimeProbes()
		{
			return false;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00004563 File Offset: 0x00002763
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00042F27 File Offset: 0x00041127
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00002072 File Offset: 0x00000272
		protected ScriptableRuntimeReflectionSystem()
		{
		}
	}
}
