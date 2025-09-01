using System;

namespace Unity.Collections
{
	// Token: 0x02000039 RID: 57
	internal sealed class BitField64DebugView
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000418D File Offset: 0x0000238D
		public BitField64DebugView(BitField64 data)
		{
			this.Data = data;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000419C File Offset: 0x0000239C
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[64];
				for (int i = 0; i < 64; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000071 RID: 113
		private BitField64 Data;
	}
}
