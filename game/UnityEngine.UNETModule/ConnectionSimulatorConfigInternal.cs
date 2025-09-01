using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000010 RID: 16
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true)]
	[NativeHeader("Modules/UNET/UNETConfiguration.h")]
	internal class ConnectionSimulatorConfigInternal : IDisposable
	{
		// Token: 0x0600011C RID: 284 RVA: 0x000041FF File Offset: 0x000023FF
		public ConnectionSimulatorConfigInternal(ConnectionSimulatorConfig config)
		{
			this.m_Ptr = ConnectionSimulatorConfigInternal.InternalCreate(config.m_OutMinDelay, config.m_OutAvgDelay, config.m_InMinDelay, config.m_InAvgDelay, config.m_PacketLossPercentage);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004234 File Offset: 0x00002434
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionSimulatorConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004278 File Offset: 0x00002478
		~ConnectionSimulatorConfigInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000042AC File Offset: 0x000024AC
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionSimulatorConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate(int outMinDelay, int outAvgDelay, int inMinDelay, int inAvgDelay, float packetLossPercentage);

		// Token: 0x06000121 RID: 289
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x04000067 RID: 103
		public IntPtr m_Ptr;
	}
}
