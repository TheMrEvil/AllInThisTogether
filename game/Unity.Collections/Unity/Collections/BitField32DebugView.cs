using System;

namespace Unity.Collections
{
	// Token: 0x02000037 RID: 55
	internal sealed class BitField32DebugView
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00004024 File Offset: 0x00002224
		public BitField32DebugView(BitField32 bitfield)
		{
			this.BitField = bitfield;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004034 File Offset: 0x00002234
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[32];
				for (int i = 0; i < 32; i++)
				{
					array[i] = this.BitField.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x0400006F RID: 111
		private BitField32 BitField;
	}
}
