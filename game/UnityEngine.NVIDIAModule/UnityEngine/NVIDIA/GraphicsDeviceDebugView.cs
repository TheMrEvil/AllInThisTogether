using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000013 RID: 19
	public class GraphicsDeviceDebugView
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000293C File Offset: 0x00000B3C
		internal GraphicsDeviceDebugView(uint viewId)
		{
			this.m_ViewId = viewId;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000296C File Offset: 0x00000B6C
		public uint deviceVersion
		{
			get
			{
				return this.m_DeviceVersion;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002984 File Offset: 0x00000B84
		public uint ngxVersion
		{
			get
			{
				return this.m_NgxVersion;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000299C File Offset: 0x00000B9C
		public IEnumerable<DLSSDebugFeatureInfos> dlssFeatureInfos
		{
			get
			{
				IEnumerable<DLSSDebugFeatureInfos> result;
				if (this.m_DlssDebugFeatures != null)
				{
					IEnumerable<DLSSDebugFeatureInfos> dlssDebugFeatures = this.m_DlssDebugFeatures;
					result = dlssDebugFeatures;
				}
				else
				{
					result = Enumerable.Empty<DLSSDebugFeatureInfos>();
				}
				return result;
			}
		}

		// Token: 0x0400004D RID: 77
		internal uint m_ViewId = 0U;

		// Token: 0x0400004E RID: 78
		internal uint m_DeviceVersion = 0U;

		// Token: 0x0400004F RID: 79
		internal uint m_NgxVersion = 0U;

		// Token: 0x04000050 RID: 80
		internal DLSSDebugFeatureInfos[] m_DlssDebugFeatures = null;
	}
}
