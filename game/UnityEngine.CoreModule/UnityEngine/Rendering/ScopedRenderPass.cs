using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040D RID: 1037
	public struct ScopedRenderPass : IDisposable
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x0003BF95 File Offset: 0x0003A195
		internal ScopedRenderPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		public void Dispose()
		{
			try
			{
				this.m_Context.EndRenderPass();
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("The ScopedRenderPass instance is not valid. This can happen if it was constructed using the default constructor.", innerException);
			}
		}

		// Token: 0x04000D35 RID: 3381
		private ScriptableRenderContext m_Context;
	}
}
