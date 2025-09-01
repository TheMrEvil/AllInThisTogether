using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200000B RID: 11
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class GlobalConfig
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003894 File Offset: 0x00001A94
		public GlobalConfig()
		{
			this.m_ThreadAwakeTimeout = 1U;
			this.m_ReactorModel = ReactorModel.SelectReactor;
			this.m_ReactorMaximumReceivedMessages = 1024;
			this.m_ReactorMaximumSentMessages = 1024;
			this.m_MaxPacketSize = 2000;
			this.m_MaxHosts = 16;
			this.m_ThreadPoolSize = 1;
			this.m_MinTimerTimeout = 1U;
			this.m_MaxTimerTimeout = 12000U;
			this.m_MinNetSimulatorTimeout = 1U;
			this.m_MaxNetSimulatorTimeout = 12000U;
			this.m_ConnectionReadyForSend = null;
			this.m_NetworkEventAvailable = null;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000391C File Offset: 0x00001B1C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003934 File Offset: 0x00001B34
		public uint ThreadAwakeTimeout
		{
			get
			{
				return this.m_ThreadAwakeTimeout;
			}
			set
			{
				bool flag = value == 0U;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Minimal thread awake timeout should be > 0");
				}
				this.m_ThreadAwakeTimeout = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000395C File Offset: 0x00001B5C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003974 File Offset: 0x00001B74
		public ReactorModel ReactorModel
		{
			get
			{
				return this.m_ReactorModel;
			}
			set
			{
				this.m_ReactorModel = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003980 File Offset: 0x00001B80
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003998 File Offset: 0x00001B98
		public ushort ReactorMaximumReceivedMessages
		{
			get
			{
				return this.m_ReactorMaximumReceivedMessages;
			}
			set
			{
				this.m_ReactorMaximumReceivedMessages = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000039A4 File Offset: 0x00001BA4
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000039BC File Offset: 0x00001BBC
		public ushort ReactorMaximumSentMessages
		{
			get
			{
				return this.m_ReactorMaximumSentMessages;
			}
			set
			{
				this.m_ReactorMaximumSentMessages = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000039C8 File Offset: 0x00001BC8
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000039E0 File Offset: 0x00001BE0
		public ushort MaxPacketSize
		{
			get
			{
				return this.m_MaxPacketSize;
			}
			set
			{
				this.m_MaxPacketSize = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000039EC File Offset: 0x00001BEC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003A04 File Offset: 0x00001C04
		public ushort MaxHosts
		{
			get
			{
				return this.m_MaxHosts;
			}
			set
			{
				bool flag = value == 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxHosts", "Maximum hosts number should be > 0");
				}
				bool flag2 = value > 128;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxHosts", "Maximum hosts number should be <= " + 128.ToString());
				}
				this.m_MaxHosts = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003A60 File Offset: 0x00001C60
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003A78 File Offset: 0x00001C78
		public byte ThreadPoolSize
		{
			get
			{
				return this.m_ThreadPoolSize;
			}
			set
			{
				this.m_ThreadPoolSize = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003A84 File Offset: 0x00001C84
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003A9C File Offset: 0x00001C9C
		public uint MinTimerTimeout
		{
			get
			{
				return this.m_MinTimerTimeout;
			}
			set
			{
				bool flag = value > this.MaxTimerTimeout;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MinTimerTimeout should be < MaxTimerTimeout");
				}
				bool flag2 = value == 0U;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MinTimerTimeout should be > 0");
				}
				this.m_MinTimerTimeout = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003ADC File Offset: 0x00001CDC
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public uint MaxTimerTimeout
		{
			get
			{
				return this.m_MaxTimerTimeout;
			}
			set
			{
				bool flag = value == 0U;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxTimerTimeout should be > 0");
				}
				bool flag2 = value > 12000U;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxTimerTimeout should be <=" + 12000U.ToString());
				}
				this.m_MaxTimerTimeout = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003B48 File Offset: 0x00001D48
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003B60 File Offset: 0x00001D60
		public uint MinNetSimulatorTimeout
		{
			get
			{
				return this.m_MinNetSimulatorTimeout;
			}
			set
			{
				bool flag = value > this.MaxNetSimulatorTimeout;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MinNetSimulatorTimeout should be < MaxTimerTimeout");
				}
				bool flag2 = value == 0U;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MinNetSimulatorTimeout should be > 0");
				}
				this.m_MinNetSimulatorTimeout = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003BA0 File Offset: 0x00001DA0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public uint MaxNetSimulatorTimeout
		{
			get
			{
				return this.m_MaxNetSimulatorTimeout;
			}
			set
			{
				bool flag = value == 0U;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxNetSimulatorTimeout should be > 0");
				}
				bool flag2 = value > 12000U;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxNetSimulatorTimeout should be <=" + 12000U.ToString());
				}
				this.m_MaxNetSimulatorTimeout = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003C0C File Offset: 0x00001E0C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003C24 File Offset: 0x00001E24
		public Action<int> NetworkEventAvailable
		{
			get
			{
				return this.m_NetworkEventAvailable;
			}
			set
			{
				this.m_NetworkEventAvailable = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003C48 File Offset: 0x00001E48
		public Action<int, int> ConnectionReadyForSend
		{
			get
			{
				return this.m_ConnectionReadyForSend;
			}
			set
			{
				this.m_ConnectionReadyForSend = value;
			}
		}

		// Token: 0x0400004F RID: 79
		private const uint g_MaxTimerTimeout = 12000U;

		// Token: 0x04000050 RID: 80
		private const uint g_MaxNetSimulatorTimeout = 12000U;

		// Token: 0x04000051 RID: 81
		private const ushort g_MaxHosts = 128;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		private uint m_ThreadAwakeTimeout;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private ReactorModel m_ReactorModel;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private ushort m_ReactorMaximumReceivedMessages;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private ushort m_ReactorMaximumSentMessages;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private ushort m_MaxPacketSize;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		private ushort m_MaxHosts;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private byte m_ThreadPoolSize;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private uint m_MinTimerTimeout;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private uint m_MaxTimerTimeout;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		private uint m_MinNetSimulatorTimeout;

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private uint m_MaxNetSimulatorTimeout;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private Action<int, int> m_ConnectionReadyForSend;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private Action<int> m_NetworkEventAvailable;
	}
}
