using System;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000273 RID: 627
	[VisibleToOtherModules]
	internal struct SpriteChannelInfo
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0002BA04 File Offset: 0x00029C04
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0002BA21 File Offset: 0x00029C21
		public unsafe void* buffer
		{
			get
			{
				return (void*)this.m_Buffer;
			}
			set
			{
				this.m_Buffer = (IntPtr)value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0002BA30 File Offset: 0x00029C30
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0002BA48 File Offset: 0x00029C48
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0002BA54 File Offset: 0x00029C54
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0002BA6C File Offset: 0x00029C6C
		public int offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0002BA78 File Offset: 0x00029C78
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0002BA90 File Offset: 0x00029C90
		public int stride
		{
			get
			{
				return this.m_Stride;
			}
			set
			{
				this.m_Stride = value;
			}
		}

		// Token: 0x040008F7 RID: 2295
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		// Token: 0x040008F8 RID: 2296
		[NativeName("count")]
		private int m_Count;

		// Token: 0x040008F9 RID: 2297
		[NativeName("offset")]
		private int m_Offset;

		// Token: 0x040008FA RID: 2298
		[NativeName("stride")]
		private int m_Stride;
	}
}
