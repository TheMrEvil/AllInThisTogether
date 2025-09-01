using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000411 RID: 1041
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0003C980 File Offset: 0x0003AB80
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0003C998 File Offset: 0x0003AB98
		public CullingResults cullingResults
		{
			get
			{
				return this.m_CullingResults;
			}
			set
			{
				this.m_CullingResults = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		public int lightIndex
		{
			get
			{
				return this.m_LightIndex;
			}
			set
			{
				this.m_LightIndex = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x0003C9C8 File Offset: 0x0003ABC8
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x0003C9E3 File Offset: 0x0003ABE3
		public bool useRenderingLayerMaskTest
		{
			get
			{
				return this.m_UseRenderingLayerMaskTest != 0;
			}
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0003C9F4 File Offset: 0x0003ABF4
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		public ShadowSplitData splitData
		{
			get
			{
				return this.m_SplitData;
			}
			set
			{
				this.m_SplitData = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0003CA18 File Offset: 0x0003AC18
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x0003CA30 File Offset: 0x0003AC30
		public ShadowObjectsFilter objectsFilter
		{
			get
			{
				return this.m_ObjectsFilter;
			}
			set
			{
				this.m_ObjectsFilter = value;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x0003CA3A File Offset: 0x0003AC3A
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
			this.m_ObjectsFilter = ShadowObjectsFilter.AllObjects;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0003CA78 File Offset: 0x0003AC78
		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest) && this.m_ObjectsFilter.Equals(other.m_ObjectsFilter);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0003CB28 File Offset: 0x0003AD28
		public override int GetHashCode()
		{
			int num = this.m_CullingResults.GetHashCode();
			num = (num * 397 ^ this.m_LightIndex);
			num = (num * 397 ^ this.m_UseRenderingLayerMaskTest);
			num = (num * 397 ^ this.m_SplitData.GetHashCode());
			return num * 397 ^ (int)this.m_ObjectsFilter;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0003CB98 File Offset: 0x0003AD98
		public static bool operator ==(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
		public static bool operator !=(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D3B RID: 3387
		private CullingResults m_CullingResults;

		// Token: 0x04000D3C RID: 3388
		private int m_LightIndex;

		// Token: 0x04000D3D RID: 3389
		private int m_UseRenderingLayerMaskTest;

		// Token: 0x04000D3E RID: 3390
		private ShadowSplitData m_SplitData;

		// Token: 0x04000D3F RID: 3391
		private ShadowObjectsFilter m_ObjectsFilter;
	}
}
