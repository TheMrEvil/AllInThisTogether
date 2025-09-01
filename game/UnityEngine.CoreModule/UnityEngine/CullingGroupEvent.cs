using System;

namespace UnityEngine
{
	// Token: 0x02000101 RID: 257
	public struct CullingGroupEvent
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00007FE8 File Offset: 0x000061E8
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00008000 File Offset: 0x00006200
		public bool isVisible
		{
			get
			{
				return (this.m_ThisState & 128) > 0;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00008024 File Offset: 0x00006224
		public bool wasVisible
		{
			get
			{
				return (this.m_PrevState & 128) > 0;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00008048 File Offset: 0x00006248
		public bool hasBecomeVisible
		{
			get
			{
				return this.isVisible && !this.wasVisible;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00008070 File Offset: 0x00006270
		public bool hasBecomeInvisible
		{
			get
			{
				return !this.isVisible && this.wasVisible;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00008094 File Offset: 0x00006294
		public int currentDistance
		{
			get
			{
				return (int)(this.m_ThisState & 127);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000080B0 File Offset: 0x000062B0
		public int previousDistance
		{
			get
			{
				return (int)(this.m_PrevState & 127);
			}
		}

		// Token: 0x0400036C RID: 876
		private int m_Index;

		// Token: 0x0400036D RID: 877
		private byte m_PrevState;

		// Token: 0x0400036E RID: 878
		private byte m_ThisState;

		// Token: 0x0400036F RID: 879
		private const byte kIsVisibleMask = 128;

		// Token: 0x04000370 RID: 880
		private const byte kDistanceMask = 127;
	}
}
