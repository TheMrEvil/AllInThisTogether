using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x02000073 RID: 115
	[Obsolete("Please use ProfilingScope")]
	public struct ProfilingSample : IDisposable
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00011551 File Offset: 0x0000F751
		public ProfilingSample(CommandBuffer cmd, string name, CustomSampler sampler = null)
		{
			this.m_Cmd = cmd;
			this.m_Name = name;
			this.m_Disposed = false;
			if (cmd != null && name != "")
			{
				cmd.BeginSample(name);
			}
			this.m_Sampler = sampler;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011586 File Offset: 0x0000F786
		public ProfilingSample(CommandBuffer cmd, string format, object arg)
		{
			this = new ProfilingSample(cmd, string.Format(format, arg), null);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011597 File Offset: 0x0000F797
		public ProfilingSample(CommandBuffer cmd, string format, params object[] args)
		{
			this = new ProfilingSample(cmd, string.Format(format, args), null);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000115B1 File Offset: 0x0000F7B1
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Cmd != null && this.m_Name != "")
			{
				this.m_Cmd.EndSample(this.m_Name);
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000252 RID: 594
		private readonly CommandBuffer m_Cmd;

		// Token: 0x04000253 RID: 595
		private readonly string m_Name;

		// Token: 0x04000254 RID: 596
		private bool m_Disposed;

		// Token: 0x04000255 RID: 597
		private CustomSampler m_Sampler;
	}
}
