using System;

namespace Steamworks
{
	// Token: 0x020000B5 RID: 181
	public struct SteamId
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00012018 File Offset: 0x00010218
		public static implicit operator SteamId(ulong value)
		{
			return new SteamId
			{
				Value = value
			};
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001203C File Offset: 0x0001023C
		public static implicit operator ulong(SteamId value)
		{
			return value.Value;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00012054 File Offset: 0x00010254
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00012061 File Offset: 0x00010261
		public uint AccountId
		{
			get
			{
				return (uint)(this.Value & (ulong)-1);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001206D File Offset: 0x0001026D
		public bool IsValid
		{
			get
			{
				return this.Value > 0UL;
			}
		}

		// Token: 0x0400076C RID: 1900
		public ulong Value;
	}
}
