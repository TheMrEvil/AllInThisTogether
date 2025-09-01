using System;

namespace UnityEngine
{
	// Token: 0x020001A1 RID: 417
	public struct CombineInstance
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0001607C File Offset: 0x0001427C
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00016099 File Offset: 0x00014299
		public Mesh mesh
		{
			get
			{
				return Mesh.FromInstanceID(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x000160B4 File Offset: 0x000142B4
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x000160CC File Offset: 0x000142CC
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x000160D8 File Offset: 0x000142D8
		// (set) Token: 0x060010B6 RID: 4278 RVA: 0x000160F0 File Offset: 0x000142F0
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x000160FC File Offset: 0x000142FC
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00016114 File Offset: 0x00014314
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.m_LightmapScaleOffset;
			}
			set
			{
				this.m_LightmapScaleOffset = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00016120 File Offset: 0x00014320
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00016138 File Offset: 0x00014338
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.m_RealtimeLightmapScaleOffset;
			}
			set
			{
				this.m_RealtimeLightmapScaleOffset = value;
			}
		}

		// Token: 0x040005B8 RID: 1464
		private int m_MeshInstanceID;

		// Token: 0x040005B9 RID: 1465
		private int m_SubMeshIndex;

		// Token: 0x040005BA RID: 1466
		private Matrix4x4 m_Transform;

		// Token: 0x040005BB RID: 1467
		private Vector4 m_LightmapScaleOffset;

		// Token: 0x040005BC RID: 1468
		private Vector4 m_RealtimeLightmapScaleOffset;
	}
}
