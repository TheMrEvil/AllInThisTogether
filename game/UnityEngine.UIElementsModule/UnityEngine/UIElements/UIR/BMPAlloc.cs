using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000330 RID: 816
	internal struct BMPAlloc
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x00072E0C File Offset: 0x0007100C
		public bool Equals(BMPAlloc other)
		{
			return this.page == other.page && this.pageLine == other.pageLine && this.bitIndex == other.bitIndex;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00072E4C File Offset: 0x0007104C
		public bool IsValid()
		{
			return this.page >= 0;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00072E6C File Offset: 0x0007106C
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.page, this.pageLine, this.bitIndex);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00072EAC File Offset: 0x000710AC
		// Note: this type is marked as 'beforefieldinit'.
		static BMPAlloc()
		{
		}

		// Token: 0x04000C34 RID: 3124
		public static readonly BMPAlloc Invalid = new BMPAlloc
		{
			page = -1
		};

		// Token: 0x04000C35 RID: 3125
		public int page;

		// Token: 0x04000C36 RID: 3126
		public ushort pageLine;

		// Token: 0x04000C37 RID: 3127
		public byte bitIndex;

		// Token: 0x04000C38 RID: 3128
		public OwnedState ownedState;
	}
}
