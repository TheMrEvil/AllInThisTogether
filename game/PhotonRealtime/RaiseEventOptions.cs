using System;

namespace Photon.Realtime
{
	// Token: 0x0200002E RID: 46
	public class RaiseEventOptions
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000848F File Offset: 0x0000668F
		public RaiseEventOptions()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000084A2 File Offset: 0x000066A2
		// Note: this type is marked as 'beforefieldinit'.
		static RaiseEventOptions()
		{
		}

		// Token: 0x04000191 RID: 401
		public static readonly RaiseEventOptions Default = new RaiseEventOptions();

		// Token: 0x04000192 RID: 402
		public EventCaching CachingOption;

		// Token: 0x04000193 RID: 403
		public byte InterestGroup;

		// Token: 0x04000194 RID: 404
		public int[] TargetActors;

		// Token: 0x04000195 RID: 405
		public ReceiverGroup Receivers;

		// Token: 0x04000196 RID: 406
		[Obsolete("Not used where SendOptions are a parameter too. Use SendOptions.Channel instead.")]
		public byte SequenceChannel;

		// Token: 0x04000197 RID: 407
		public WebFlags Flags = WebFlags.Default;
	}
}
