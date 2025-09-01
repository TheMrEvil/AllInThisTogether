using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000008 RID: 8
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class ChannelQOS
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002A48 File Offset: 0x00000C48
		public ChannelQOS(QosType value)
		{
			this.m_Type = value;
			this.m_BelongsSharedOrderChannel = false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002A60 File Offset: 0x00000C60
		public ChannelQOS()
		{
			this.m_Type = QosType.Unreliable;
			this.m_BelongsSharedOrderChannel = false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002A78 File Offset: 0x00000C78
		public ChannelQOS(ChannelQOS channel)
		{
			bool flag = channel == null;
			if (flag)
			{
				throw new NullReferenceException("channel is not defined");
			}
			this.m_Type = channel.m_Type;
			this.m_BelongsSharedOrderChannel = channel.m_BelongsSharedOrderChannel;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public QosType QOS
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public bool BelongsToSharedOrderChannel
		{
			get
			{
				return this.m_BelongsSharedOrderChannel;
			}
		}

		// Token: 0x0400002A RID: 42
		[SerializeField]
		internal QosType m_Type;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		internal bool m_BelongsSharedOrderChannel;
	}
}
