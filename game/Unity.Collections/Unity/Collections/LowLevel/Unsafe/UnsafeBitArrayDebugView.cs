using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000120 RID: 288
	internal sealed class UnsafeBitArrayDebugView
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0001F9F1 File Offset: 0x0001DBF1
		public UnsafeBitArrayDebugView(UnsafeBitArray data)
		{
			this.Data = data;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001FA00 File Offset: 0x0001DC00
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[this.Data.Length];
				for (int i = 0; i < this.Data.Length; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x04000377 RID: 887
		private UnsafeBitArray Data;
	}
}
