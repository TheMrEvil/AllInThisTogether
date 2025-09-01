using System;

namespace Steamworks
{
	// Token: 0x02000006 RID: 6
	public class AuthTicket : IDisposable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		public void Cancel()
		{
			bool flag = this.Handle > 0U;
			if (flag)
			{
				SteamUser.Internal.CancelAuthTicket(this.Handle);
			}
			this.Handle = 0U;
			this.Data = null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002291 File Offset: 0x00000491
		public void Dispose()
		{
			this.Cancel();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229B File Offset: 0x0000049B
		public AuthTicket()
		{
		}

		// Token: 0x04000004 RID: 4
		public byte[] Data;

		// Token: 0x04000005 RID: 5
		public uint Handle;
	}
}
