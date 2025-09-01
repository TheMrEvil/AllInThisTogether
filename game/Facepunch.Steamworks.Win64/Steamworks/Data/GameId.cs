using System;

namespace Steamworks.Data
{
	// Token: 0x020001F6 RID: 502
	public struct GameId
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public static implicit operator GameId(ulong value)
		{
			return new GameId
			{
				Value = value
			};
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00019B14 File Offset: 0x00017D14
		public static implicit operator ulong(GameId value)
		{
			return value.Value;
		}

		// Token: 0x04000BF7 RID: 3063
		public ulong Value;
	}
}
