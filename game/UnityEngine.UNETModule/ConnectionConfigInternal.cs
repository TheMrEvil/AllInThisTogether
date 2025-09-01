using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000E RID: 14
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true)]
	[NativeHeader("Modules/UNET/UNETConfiguration.h")]
	[NativeHeader("Modules/UNET/UNetTypes.h")]
	[NativeHeader("Modules/UNET/UNETManager.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class ConnectionConfigInternal : IDisposable
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00003D70 File Offset: 0x00001F70
		public ConnectionConfigInternal(ConnectionConfig config)
		{
			bool flag = config == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.m_Ptr = ConnectionConfigInternal.InternalCreate();
			bool flag2 = !this.SetPacketSize(config.PacketSize);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("PacketSize is too small");
			}
			this.FragmentSize = config.FragmentSize;
			this.ResendTimeout = config.ResendTimeout;
			this.DisconnectTimeout = config.DisconnectTimeout;
			this.ConnectTimeout = config.ConnectTimeout;
			this.MinUpdateTimeout = config.MinUpdateTimeout;
			this.PingTimeout = config.PingTimeout;
			this.ReducedPingTimeout = config.ReducedPingTimeout;
			this.AllCostTimeout = config.AllCostTimeout;
			this.NetworkDropThreshold = config.NetworkDropThreshold;
			this.OverflowDropThreshold = config.OverflowDropThreshold;
			this.MaxConnectionAttempt = config.MaxConnectionAttempt;
			this.AckDelay = config.AckDelay;
			this.SendDelay = config.SendDelay;
			this.MaxCombinedReliableMessageSize = config.MaxCombinedReliableMessageSize;
			this.MaxCombinedReliableMessageCount = config.MaxCombinedReliableMessageCount;
			this.MaxSentMessageQueueSize = config.MaxSentMessageQueueSize;
			this.AcksType = (byte)config.AcksType;
			this.UsePlatformSpecificProtocols = config.UsePlatformSpecificProtocols;
			this.InitialBandwidth = config.InitialBandwidth;
			this.BandwidthPeakFactor = config.BandwidthPeakFactor;
			this.WebSocketReceiveBufferMaxSize = config.WebSocketReceiveBufferMaxSize;
			this.UdpSocketReceiveBufferMaxSize = config.UdpSocketReceiveBufferMaxSize;
			bool flag3 = config.SSLCertFilePath != null;
			if (flag3)
			{
				int num = this.SetSSLCertFilePath(config.SSLCertFilePath);
				bool flag4 = num != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("SSLCertFilePath cannot be > than " + num.ToString());
				}
			}
			bool flag5 = config.SSLPrivateKeyFilePath != null;
			if (flag5)
			{
				int num2 = this.SetSSLPrivateKeyFilePath(config.SSLPrivateKeyFilePath);
				bool flag6 = num2 != 0;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("SSLPrivateKeyFilePath cannot be > than " + num2.ToString());
				}
			}
			bool flag7 = config.SSLCAFilePath != null;
			if (flag7)
			{
				int num3 = this.SetSSLCAFilePath(config.SSLCAFilePath);
				bool flag8 = num3 != 0;
				if (flag8)
				{
					throw new ArgumentOutOfRangeException("SSLCAFilePath cannot be > than " + num3.ToString());
				}
			}
			byte b = 0;
			while ((int)b < config.ChannelCount)
			{
				this.AddChannel((int)((byte)config.GetChannel(b)));
				b += 1;
			}
			byte b2 = 0;
			while ((int)b2 < config.SharedOrderChannelCount)
			{
				IList<byte> sharedOrderChannels = config.GetSharedOrderChannels(b2);
				byte[] array = new byte[sharedOrderChannels.Count];
				sharedOrderChannels.CopyTo(array, 0);
				this.MakeChannelsSharedOrder(array);
				b2 += 1;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004018 File Offset: 0x00002218
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004054 File Offset: 0x00002254
		~ConnectionConfigInternal()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate();

		// Token: 0x060000F5 RID: 245
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte AddChannel(int value);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPacketSize(ushort value);

		// Token: 0x17000037 RID: 55
		// (set) Token: 0x060000F8 RID: 248
		[NativeProperty("m_ProtocolRequired.m_FragmentSize", TargetType.Field)]
		private extern ushort FragmentSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000038 RID: 56
		// (set) Token: 0x060000F9 RID: 249
		[NativeProperty("m_ProtocolRequired.m_ResendTimeout", TargetType.Field)]
		private extern uint ResendTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000039 RID: 57
		// (set) Token: 0x060000FA RID: 250
		[NativeProperty("m_ProtocolRequired.m_DisconnectTimeout", TargetType.Field)]
		private extern uint DisconnectTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003A RID: 58
		// (set) Token: 0x060000FB RID: 251
		[NativeProperty("m_ProtocolRequired.m_ConnectTimeout", TargetType.Field)]
		private extern uint ConnectTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003B RID: 59
		// (set) Token: 0x060000FC RID: 252
		[NativeProperty("m_ProtocolOptional.m_MinUpdateTimeout", TargetType.Field)]
		private extern uint MinUpdateTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003C RID: 60
		// (set) Token: 0x060000FD RID: 253
		[NativeProperty("m_ProtocolRequired.m_PingTimeout", TargetType.Field)]
		private extern uint PingTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003D RID: 61
		// (set) Token: 0x060000FE RID: 254
		[NativeProperty("m_ProtocolRequired.m_ReducedPingTimeout", TargetType.Field)]
		private extern uint ReducedPingTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003E RID: 62
		// (set) Token: 0x060000FF RID: 255
		[NativeProperty("m_ProtocolRequired.m_AllCostTimeout", TargetType.Field)]
		private extern uint AllCostTimeout { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003F RID: 63
		// (set) Token: 0x06000100 RID: 256
		[NativeProperty("m_ProtocolOptional.m_NetworkDropThreshold", TargetType.Field)]
		private extern byte NetworkDropThreshold { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000040 RID: 64
		// (set) Token: 0x06000101 RID: 257
		[NativeProperty("m_ProtocolOptional.m_OverflowDropThreshold", TargetType.Field)]
		private extern byte OverflowDropThreshold { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000102 RID: 258
		[NativeProperty("m_ProtocolOptional.m_MaxConnectionAttempt", TargetType.Field)]
		private extern byte MaxConnectionAttempt { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000042 RID: 66
		// (set) Token: 0x06000103 RID: 259
		[NativeProperty("m_ProtocolOptional.m_AckDelay", TargetType.Field)]
		private extern uint AckDelay { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000043 RID: 67
		// (set) Token: 0x06000104 RID: 260
		[NativeProperty("m_ProtocolOptional.m_SendDelay", TargetType.Field)]
		private extern uint SendDelay { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000044 RID: 68
		// (set) Token: 0x06000105 RID: 261
		[NativeProperty("m_ProtocolOptional.m_MaxCombinedReliableMessageSize", TargetType.Field)]
		private extern ushort MaxCombinedReliableMessageSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000045 RID: 69
		// (set) Token: 0x06000106 RID: 262
		[NativeProperty("m_ProtocolOptional.m_MaxCombinedReliableMessageAmount", TargetType.Field)]
		private extern ushort MaxCombinedReliableMessageCount { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000046 RID: 70
		// (set) Token: 0x06000107 RID: 263
		[NativeProperty("m_ProtocolOptional.m_MaxSentMessageQueueSize", TargetType.Field)]
		private extern ushort MaxSentMessageQueueSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000047 RID: 71
		// (set) Token: 0x06000108 RID: 264
		[NativeProperty("m_ProtocolRequired.m_AcksType", TargetType.Field)]
		private extern byte AcksType { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000048 RID: 72
		// (set) Token: 0x06000109 RID: 265
		[NativeProperty("m_ProtocolRequired.m_UsePlatformSpecificProtocols", TargetType.Field)]
		private extern bool UsePlatformSpecificProtocols { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000049 RID: 73
		// (set) Token: 0x0600010A RID: 266
		[NativeProperty("m_ProtocolOptional.m_InitialBandwidth", TargetType.Field)]
		private extern uint InitialBandwidth { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x0600010B RID: 267
		[NativeProperty("m_ProtocolOptional.m_BandwidthPeakFactor", TargetType.Field)]
		private extern float BandwidthPeakFactor { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004B RID: 75
		// (set) Token: 0x0600010C RID: 268
		[NativeProperty("m_ProtocolOptional.m_WebSocketReceiveBufferMaxSize", TargetType.Field)]
		private extern ushort WebSocketReceiveBufferMaxSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004C RID: 76
		// (set) Token: 0x0600010D RID: 269
		[NativeProperty("m_ProtocolOptional.m_UdpSocketReceiveBufferMaxSize", TargetType.Field)]
		private extern uint UdpSocketReceiveBufferMaxSize { [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600010E RID: 270
		[NativeMethod("SetSSLCertFilePath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int SetSSLCertFilePath(string value);

		// Token: 0x0600010F RID: 271
		[NativeMethod("SetSSLPrivateKeyFilePath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int SetSSLPrivateKeyFilePath(string value);

		// Token: 0x06000110 RID: 272
		[NativeMethod("SetSSLCAFilePath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int SetSSLCAFilePath(string value);

		// Token: 0x06000111 RID: 273
		[NativeMethod("MakeChannelsSharedOrder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool MakeChannelsSharedOrder(byte[] values);

		// Token: 0x04000065 RID: 101
		public IntPtr m_Ptr;
	}
}
