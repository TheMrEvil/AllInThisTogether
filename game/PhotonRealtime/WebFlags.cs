using System;

namespace Photon.Realtime
{
	// Token: 0x02000041 RID: 65
	public class WebFlags
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B564 File Offset: 0x00009764
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000B571 File Offset: 0x00009771
		public bool HttpForward
		{
			get
			{
				return (this.WebhookFlags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.WebhookFlags |= 1;
					return;
				}
				this.WebhookFlags = (byte)((int)this.WebhookFlags & -2);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B596 File Offset: 0x00009796
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000B5A3 File Offset: 0x000097A3
		public bool SendAuthCookie
		{
			get
			{
				return (this.WebhookFlags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.WebhookFlags |= 2;
					return;
				}
				this.WebhookFlags = (byte)((int)this.WebhookFlags & -3);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B5C8 File Offset: 0x000097C8
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000B5D5 File Offset: 0x000097D5
		public bool SendSync
		{
			get
			{
				return (this.WebhookFlags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.WebhookFlags |= 4;
					return;
				}
				this.WebhookFlags = (byte)((int)this.WebhookFlags & -5);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B5FA File Offset: 0x000097FA
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000B607 File Offset: 0x00009807
		public bool SendState
		{
			get
			{
				return (this.WebhookFlags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.WebhookFlags |= 8;
					return;
				}
				this.WebhookFlags = (byte)((int)this.WebhookFlags & -9);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B62C File Offset: 0x0000982C
		public WebFlags(byte webhookFlags)
		{
			this.WebhookFlags = webhookFlags;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B63B File Offset: 0x0000983B
		// Note: this type is marked as 'beforefieldinit'.
		static WebFlags()
		{
		}

		// Token: 0x04000215 RID: 533
		public static readonly WebFlags Default = new WebFlags(0);

		// Token: 0x04000216 RID: 534
		public byte WebhookFlags;

		// Token: 0x04000217 RID: 535
		public const byte HttpForwardConst = 1;

		// Token: 0x04000218 RID: 536
		public const byte SendAuthCookieConst = 2;

		// Token: 0x04000219 RID: 537
		public const byte SendSyncConst = 4;

		// Token: 0x0400021A RID: 538
		public const byte SendStateConst = 8;
	}
}
