using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000026 RID: 38
	internal struct RenderGraphLogIndent : IDisposable
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00009DF2 File Offset: 0x00007FF2
		public RenderGraphLogIndent(RenderGraphLogger logger, int indentation = 1)
		{
			this.m_Disposed = false;
			this.m_Indentation = indentation;
			this.m_Logger = logger;
			this.m_Logger.IncrementIndentation(this.m_Indentation);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009E1A File Offset: 0x0000801A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009E23 File Offset: 0x00008023
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Logger != null)
			{
				this.m_Logger.DecrementIndentation(this.m_Indentation);
			}
			this.m_Disposed = true;
		}

		// Token: 0x0400010E RID: 270
		private int m_Indentation;

		// Token: 0x0400010F RID: 271
		private RenderGraphLogger m_Logger;

		// Token: 0x04000110 RID: 272
		private bool m_Disposed;
	}
}
