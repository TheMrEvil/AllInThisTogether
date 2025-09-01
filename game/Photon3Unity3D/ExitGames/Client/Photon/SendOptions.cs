using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000031 RID: 49
	public struct SendOptions
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00013BCC File Offset: 0x00011DCC
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00013BE7 File Offset: 0x00011DE7
		public bool Reliability
		{
			get
			{
				return this.DeliveryMode == DeliveryMode.Reliable;
			}
			set
			{
				this.DeliveryMode = (value ? DeliveryMode.Reliable : DeliveryMode.Unreliable);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00013BF8 File Offset: 0x00011DF8
		// Note: this type is marked as 'beforefieldinit'.
		static SendOptions()
		{
		}

		// Token: 0x04000195 RID: 405
		public static readonly SendOptions SendReliable = new SendOptions
		{
			Reliability = true
		};

		// Token: 0x04000196 RID: 406
		public static readonly SendOptions SendUnreliable = new SendOptions
		{
			Reliability = false
		};

		// Token: 0x04000197 RID: 407
		public DeliveryMode DeliveryMode;

		// Token: 0x04000198 RID: 408
		public bool Encrypt;

		// Token: 0x04000199 RID: 409
		public byte Channel;
	}
}
