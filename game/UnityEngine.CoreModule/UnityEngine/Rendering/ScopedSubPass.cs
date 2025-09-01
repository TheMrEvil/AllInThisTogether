using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040E RID: 1038
	public struct ScopedSubPass : IDisposable
	{
		// Token: 0x0600237C RID: 9084 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		internal ScopedSubPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
		public void Dispose()
		{
			try
			{
				this.m_Context.EndSubPass();
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("The ScopedSubPass instance is not valid. This can happen if it was constructed using the default constructor.", innerException);
			}
		}

		// Token: 0x04000D36 RID: 3382
		private ScriptableRenderContext m_Context;
	}
}
