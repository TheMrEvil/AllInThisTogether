using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000C RID: 12
	public readonly struct OptimalDLSSSettingsData
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002484 File Offset: 0x00000684
		public uint outRenderWidth
		{
			get
			{
				return this.m_OutRenderWidth;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000249C File Offset: 0x0000069C
		public uint outRenderHeight
		{
			get
			{
				return this.m_OutRenderHeight;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024B4 File Offset: 0x000006B4
		public float sharpness
		{
			get
			{
				return this.m_Sharpness;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000024CC File Offset: 0x000006CC
		public uint maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024E4 File Offset: 0x000006E4
		public uint maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000024FC File Offset: 0x000006FC
		public uint minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002514 File Offset: 0x00000714
		public uint minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly uint m_OutRenderWidth;

		// Token: 0x04000035 RID: 53
		private readonly uint m_OutRenderHeight;

		// Token: 0x04000036 RID: 54
		private readonly float m_Sharpness;

		// Token: 0x04000037 RID: 55
		private readonly uint m_MaxWidth;

		// Token: 0x04000038 RID: 56
		private readonly uint m_MaxHeight;

		// Token: 0x04000039 RID: 57
		private readonly uint m_MinWidth;

		// Token: 0x0400003A RID: 58
		private readonly uint m_MinHeight;
	}
}
