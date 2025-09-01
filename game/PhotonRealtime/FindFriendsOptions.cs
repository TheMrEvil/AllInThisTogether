using System;

namespace Photon.Realtime
{
	// Token: 0x0200001F RID: 31
	public class FindFriendsOptions
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00008354 File Offset: 0x00006554
		internal int ToIntFlags()
		{
			int num = 0;
			if (this.CreatedOnGs)
			{
				num |= 1;
			}
			if (this.Visible)
			{
				num |= 2;
			}
			if (this.Open)
			{
				num |= 4;
			}
			return num;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008388 File Offset: 0x00006588
		public FindFriendsOptions()
		{
		}

		// Token: 0x040000BA RID: 186
		public bool CreatedOnGs;

		// Token: 0x040000BB RID: 187
		public bool Visible;

		// Token: 0x040000BC RID: 188
		public bool Open;
	}
}
