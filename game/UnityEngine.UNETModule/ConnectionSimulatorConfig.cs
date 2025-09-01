using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200000C RID: 12
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	public class ConnectionSimulatorConfig : IDisposable
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003C52 File Offset: 0x00001E52
		public ConnectionSimulatorConfig(int outMinDelay, int outAvgDelay, int inMinDelay, int inAvgDelay, float packetLossPercentage)
		{
			this.m_OutMinDelay = outMinDelay;
			this.m_OutAvgDelay = outAvgDelay;
			this.m_InMinDelay = inMinDelay;
			this.m_InAvgDelay = inAvgDelay;
			this.m_PacketLossPercentage = packetLossPercentage;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003C81 File Offset: 0x00001E81
		[ThreadAndSerializationSafe]
		public void Dispose()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003C84 File Offset: 0x00001E84
		~ConnectionSimulatorConfig()
		{
			this.Dispose();
		}

		// Token: 0x0400005F RID: 95
		internal int m_OutMinDelay;

		// Token: 0x04000060 RID: 96
		internal int m_OutAvgDelay;

		// Token: 0x04000061 RID: 97
		internal int m_InMinDelay;

		// Token: 0x04000062 RID: 98
		internal int m_InAvgDelay;

		// Token: 0x04000063 RID: 99
		internal float m_PacketLossPercentage;
	}
}
