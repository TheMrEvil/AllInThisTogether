using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/UNET/UNETConfiguration.h")]
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true)]
	internal class HostTopologyInternal : IDisposable
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000040C4 File Offset: 0x000022C4
		public HostTopologyInternal(HostTopology topology)
		{
			ConnectionConfigInternal config = new ConnectionConfigInternal(topology.DefaultConfig);
			this.m_Ptr = HostTopologyInternal.InternalCreate(config, topology.MaxDefaultConnections);
			for (int i = 1; i <= topology.SpecialConnectionConfigsCount; i++)
			{
				ConnectionConfig specialConnectionConfig = topology.GetSpecialConnectionConfig(i);
				ConnectionConfigInternal config2 = new ConnectionConfigInternal(specialConnectionConfig);
				this.AddSpecialConnectionConfig(config2);
			}
			this.ReceivedMessagePoolSize = topology.ReceivedMessagePoolSize;
			this.SentMessagePoolSize = topology.SentMessagePoolSize;
			this.MessagePoolSizeGrowthFactor = topology.MessagePoolSizeGrowthFactor;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004154 File Offset: 0x00002354
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				HostTopologyInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004190 File Offset: 0x00002390
		~HostTopologyInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000041C4 File Offset: 0x000023C4
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				HostTopologyInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate(ConnectionConfigInternal config, int maxDefaultConnections);

		// Token: 0x06000117 RID: 279
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort AddSpecialConnectionConfig(ConnectionConfigInternal config);

		// Token: 0x1700004D RID: 77
		// (set) Token: 0x06000119 RID: 281
		[NativeProperty("m_ReceivedMessagePoolSize", TargetType.Field)]
		private extern ushort ReceivedMessagePoolSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004E RID: 78
		// (set) Token: 0x0600011A RID: 282
		[NativeProperty("m_SentMessagePoolSize", TargetType.Field)]
		private extern ushort SentMessagePoolSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004F RID: 79
		// (set) Token: 0x0600011B RID: 283
		[NativeProperty("m_MessagePoolSizeGrowthFactor", TargetType.Field)]
		private extern float MessagePoolSizeGrowthFactor { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x04000066 RID: 102
		public IntPtr m_Ptr;
	}
}
