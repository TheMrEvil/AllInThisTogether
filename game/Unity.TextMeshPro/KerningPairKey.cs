using System;

namespace TMPro
{
	// Token: 0x02000033 RID: 51
	public struct KerningPairKey
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0001C528 File Offset: 0x0001A728
		public KerningPairKey(uint ascii_left, uint ascii_right)
		{
			this.ascii_Left = ascii_left;
			this.ascii_Right = ascii_right;
			this.key = (ascii_right << 16) + ascii_left;
		}

		// Token: 0x040001DB RID: 475
		public uint ascii_Left;

		// Token: 0x040001DC RID: 476
		public uint ascii_Right;

		// Token: 0x040001DD RID: 477
		public uint key;
	}
}
