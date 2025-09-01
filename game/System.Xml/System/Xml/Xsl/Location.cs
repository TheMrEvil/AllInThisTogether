using System;
using System.Diagnostics;

namespace System.Xml.Xsl
{
	// Token: 0x0200032C RID: 812
	[DebuggerDisplay("({Line},{Pos})")]
	internal struct Location
	{
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000D2D39 File Offset: 0x000D0F39
		public int Line
		{
			get
			{
				return (int)(this.value >> 32);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x000D2D45 File Offset: 0x000D0F45
		public int Pos
		{
			get
			{
				return (int)this.value;
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000D2D4E File Offset: 0x000D0F4E
		public Location(int line, int pos)
		{
			this.value = (ulong)((long)line << 32 | (long)((ulong)pos));
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000D2D5E File Offset: 0x000D0F5E
		public Location(Location that)
		{
			this.value = that.value;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000D2D6C File Offset: 0x000D0F6C
		public bool LessOrEqual(Location that)
		{
			return this.value <= that.value;
		}

		// Token: 0x04001B98 RID: 7064
		private ulong value;
	}
}
