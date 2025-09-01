using System;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x02000018 RID: 24
	public struct PhotonMessageInfo
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000885F File Offset: 0x00006A5F
		public PhotonMessageInfo(Player player, int timestamp, PhotonView view)
		{
			this.Sender = player;
			this.timeInt = timestamp;
			this.photonView = view;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00008876 File Offset: 0x00006A76
		[Obsolete("Use SentServerTime instead.")]
		public double timestamp
		{
			get
			{
				return this.timeInt / 1000.0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000888A File Offset: 0x00006A8A
		public double SentServerTime
		{
			get
			{
				return this.timeInt / 1000.0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000889E File Offset: 0x00006A9E
		public int SentServerTimestamp
		{
			get
			{
				return this.timeInt;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000088A6 File Offset: 0x00006AA6
		public override string ToString()
		{
			return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", this.SentServerTime, this.Sender);
		}

		// Token: 0x040000A5 RID: 165
		private readonly int timeInt;

		// Token: 0x040000A6 RID: 166
		public readonly Player Sender;

		// Token: 0x040000A7 RID: 167
		public readonly PhotonView photonView;
	}
}
