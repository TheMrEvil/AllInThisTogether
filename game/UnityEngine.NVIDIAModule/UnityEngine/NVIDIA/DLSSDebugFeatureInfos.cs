using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000D RID: 13
	public readonly struct DLSSDebugFeatureInfos
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000252C File Offset: 0x0000072C
		public bool validFeature
		{
			get
			{
				return this.m_ValidFeature;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002544 File Offset: 0x00000744
		public uint featureSlot
		{
			get
			{
				return this.m_FeatureSlot;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000255C File Offset: 0x0000075C
		public DLSSCommandExecutionData execData
		{
			get
			{
				return this.m_ExecData;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002574 File Offset: 0x00000774
		public DLSSCommandInitializationData initData
		{
			get
			{
				return this.m_InitData;
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly bool m_ValidFeature;

		// Token: 0x0400003C RID: 60
		private readonly uint m_FeatureSlot;

		// Token: 0x0400003D RID: 61
		private readonly DLSSCommandExecutionData m_ExecData;

		// Token: 0x0400003E RID: 62
		private readonly DLSSCommandInitializationData m_InitData;
	}
}
