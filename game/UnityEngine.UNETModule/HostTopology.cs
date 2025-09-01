using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x0200000A RID: 10
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class HostTopology
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000035D8 File Offset: 0x000017D8
		public HostTopology(ConnectionConfig defaultConfig, int maxDefaultConnections)
		{
			bool flag = defaultConfig == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			bool flag2 = maxDefaultConnections <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be > 0");
			}
			bool flag3 = maxDefaultConnections >= 65535;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be < 65535");
			}
			ConnectionConfig.Validate(defaultConfig);
			this.m_DefConfig = new ConnectionConfig(defaultConfig);
			this.m_MaxDefConnections = maxDefaultConnections;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003690 File Offset: 0x00001890
		private HostTopology()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000036E0 File Offset: 0x000018E0
		public ConnectionConfig DefaultConfig
		{
			get
			{
				return this.m_DefConfig;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000036F8 File Offset: 0x000018F8
		public int MaxDefaultConnections
		{
			get
			{
				return this.m_MaxDefConnections;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003710 File Offset: 0x00001910
		public int SpecialConnectionConfigsCount
		{
			get
			{
				return this.m_SpecialConnections.Count;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003730 File Offset: 0x00001930
		public List<ConnectionConfig> SpecialConnectionConfigs
		{
			get
			{
				return this.m_SpecialConnections;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003748 File Offset: 0x00001948
		public ConnectionConfig GetSpecialConnectionConfig(int i)
		{
			bool flag = i > this.m_SpecialConnections.Count || i == 0;
			if (flag)
			{
				throw new ArgumentException("special configuration index is out of valid range");
			}
			return this.m_SpecialConnections[i - 1];
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000378C File Offset: 0x0000198C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000037A4 File Offset: 0x000019A4
		public ushort ReceivedMessagePoolSize
		{
			get
			{
				return this.m_ReceivedMessagePoolSize;
			}
			set
			{
				this.m_ReceivedMessagePoolSize = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000037C8 File Offset: 0x000019C8
		public ushort SentMessagePoolSize
		{
			get
			{
				return this.m_SentMessagePoolSize;
			}
			set
			{
				this.m_SentMessagePoolSize = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000037D4 File Offset: 0x000019D4
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000037EC File Offset: 0x000019EC
		public float MessagePoolSizeGrowthFactor
		{
			get
			{
				return this.m_MessagePoolSizeGrowthFactor;
			}
			set
			{
				bool flag = (double)value <= 0.5 || (double)value > 1.0;
				if (flag)
				{
					throw new ArgumentException("pool growth factor should be varied between 0.5 and 1.0");
				}
				this.m_MessagePoolSizeGrowthFactor = value;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003830 File Offset: 0x00001A30
		public int AddSpecialConnectionConfig(ConnectionConfig config)
		{
			bool flag = this.m_MaxDefConnections + this.m_SpecialConnections.Count + 1 >= 65535;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be < 65535");
			}
			this.m_SpecialConnections.Add(new ConnectionConfig(config));
			return this.m_SpecialConnections.Count;
		}

		// Token: 0x04000049 RID: 73
		[SerializeField]
		private ConnectionConfig m_DefConfig = null;

		// Token: 0x0400004A RID: 74
		[SerializeField]
		private int m_MaxDefConnections = 0;

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private List<ConnectionConfig> m_SpecialConnections = new List<ConnectionConfig>();

		// Token: 0x0400004C RID: 76
		[SerializeField]
		private ushort m_ReceivedMessagePoolSize = 1024;

		// Token: 0x0400004D RID: 77
		[SerializeField]
		private ushort m_SentMessagePoolSize = 1024;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		private float m_MessagePoolSizeGrowthFactor = 0.75f;
	}
}
