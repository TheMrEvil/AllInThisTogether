using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000008 RID: 8
	public struct DLSSCommandInitializationData
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020C4 File Offset: 0x000002C4
		public uint inputRTWidth
		{
			get
			{
				return this.m_InputRTWidth;
			}
			set
			{
				this.m_InputRTWidth = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020E8 File Offset: 0x000002E8
		public uint inputRTHeight
		{
			get
			{
				return this.m_InputRTHeight;
			}
			set
			{
				this.m_InputRTHeight = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000210C File Offset: 0x0000030C
		public uint outputRTWidth
		{
			get
			{
				return this.m_OutputRTWidth;
			}
			set
			{
				this.m_OutputRTWidth = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002130 File Offset: 0x00000330
		public uint outputRTHeight
		{
			get
			{
				return this.m_OutputRTHeight;
			}
			set
			{
				this.m_OutputRTHeight = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002160 File Offset: 0x00000360
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002154 File Offset: 0x00000354
		public DLSSQuality quality
		{
			get
			{
				return this.m_Quality;
			}
			set
			{
				this.m_Quality = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002184 File Offset: 0x00000384
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002178 File Offset: 0x00000378
		public DLSSFeatureFlags featureFlags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021A8 File Offset: 0x000003A8
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000219C File Offset: 0x0000039C
		internal uint featureSlot
		{
			get
			{
				return this.m_FeatureSlot;
			}
			set
			{
				this.m_FeatureSlot = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021C0 File Offset: 0x000003C0
		public void SetFlag(DLSSFeatureFlags flag, bool value)
		{
			if (value)
			{
				this.m_Flags |= flag;
			}
			else
			{
				this.m_Flags &= ~flag;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021F8 File Offset: 0x000003F8
		public bool GetFlag(DLSSFeatureFlags flag)
		{
			return (this.m_Flags & flag) > DLSSFeatureFlags.None;
		}

		// Token: 0x04000010 RID: 16
		private uint m_InputRTWidth;

		// Token: 0x04000011 RID: 17
		private uint m_InputRTHeight;

		// Token: 0x04000012 RID: 18
		private uint m_OutputRTWidth;

		// Token: 0x04000013 RID: 19
		private uint m_OutputRTHeight;

		// Token: 0x04000014 RID: 20
		private DLSSQuality m_Quality;

		// Token: 0x04000015 RID: 21
		private DLSSFeatureFlags m_Flags;

		// Token: 0x04000016 RID: 22
		private uint m_FeatureSlot;
	}
}
