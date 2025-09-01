using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000011 RID: 17
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true)]
	[NativeHeader("Modules/UNET/UNETConfiguration.h")]
	internal class GlobalConfigInternal : IDisposable
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000042E8 File Offset: 0x000024E8
		public GlobalConfigInternal(GlobalConfig config)
		{
			bool flag = config == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.m_Ptr = GlobalConfigInternal.InternalCreate();
			this.ThreadAwakeTimeout = config.ThreadAwakeTimeout;
			this.ReactorModel = (byte)config.ReactorModel;
			this.ReactorMaximumReceivedMessages = config.ReactorMaximumReceivedMessages;
			this.ReactorMaximumSentMessages = config.ReactorMaximumSentMessages;
			this.MaxPacketSize = config.MaxPacketSize;
			this.MaxHosts = config.MaxHosts;
			bool flag2 = config.ThreadPoolSize == 0 || config.ThreadPoolSize > 254;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("Worker thread pool size should be >= 1 && < 254 (for server only)");
			}
			byte threadPoolSize = config.ThreadPoolSize;
			this.ThreadPoolSize = threadPoolSize;
			this.MinTimerTimeout = config.MinTimerTimeout;
			this.MaxTimerTimeout = config.MaxTimerTimeout;
			this.MinNetSimulatorTimeout = config.MinNetSimulatorTimeout;
			this.MaxNetSimulatorTimeout = config.MaxNetSimulatorTimeout;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000043D4 File Offset: 0x000025D4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GlobalConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004410 File Offset: 0x00002610
		~GlobalConfigInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004444 File Offset: 0x00002644
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GlobalConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate();

		// Token: 0x06000127 RID: 295
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x17000050 RID: 80
		// (set) Token: 0x06000128 RID: 296
		[NativeProperty("m_ThreadAwakeTimeout", TargetType.Field)]
		private extern uint ThreadAwakeTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000051 RID: 81
		// (set) Token: 0x06000129 RID: 297
		[NativeProperty("m_ReactorModel", TargetType.Field)]
		private extern byte ReactorModel { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000052 RID: 82
		// (set) Token: 0x0600012A RID: 298
		[NativeProperty("m_ReactorMaximumReceivedMessages", TargetType.Field)]
		private extern ushort ReactorMaximumReceivedMessages { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000053 RID: 83
		// (set) Token: 0x0600012B RID: 299
		[NativeProperty("m_ReactorMaximumSentMessages", TargetType.Field)]
		private extern ushort ReactorMaximumSentMessages { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x0600012C RID: 300
		[NativeProperty("m_MaxPacketSize", TargetType.Field)]
		private extern ushort MaxPacketSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000055 RID: 85
		// (set) Token: 0x0600012D RID: 301
		[NativeProperty("m_MaxHosts", TargetType.Field)]
		private extern ushort MaxHosts { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000056 RID: 86
		// (set) Token: 0x0600012E RID: 302
		[NativeProperty("m_ThreadPoolSize", TargetType.Field)]
		private extern byte ThreadPoolSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (set) Token: 0x0600012F RID: 303
		[NativeProperty("m_MinTimerTimeout", TargetType.Field)]
		private extern uint MinTimerTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x06000130 RID: 304
		[NativeProperty("m_MaxTimerTimeout", TargetType.Field)]
		private extern uint MaxTimerTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000059 RID: 89
		// (set) Token: 0x06000131 RID: 305
		[NativeProperty("m_MinNetSimulatorTimeout", TargetType.Field)]
		private extern uint MinNetSimulatorTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (set) Token: 0x06000132 RID: 306
		[NativeProperty("m_MaxNetSimulatorTimeout", TargetType.Field)]
		private extern uint MaxNetSimulatorTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x04000068 RID: 104
		public IntPtr m_Ptr;
	}
}
