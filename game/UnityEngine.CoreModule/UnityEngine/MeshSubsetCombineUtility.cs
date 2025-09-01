using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000245 RID: 581
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x060018C6 RID: 6342 RVA: 0x00002072 File Offset: 0x00000272
		public MeshSubsetCombineUtility()
		{
		}

		// Token: 0x02000246 RID: 582
		public struct MeshInstance
		{
			// Token: 0x04000857 RID: 2135
			public int meshInstanceID;

			// Token: 0x04000858 RID: 2136
			public int rendererInstanceID;

			// Token: 0x04000859 RID: 2137
			public int additionalVertexStreamsMeshInstanceID;

			// Token: 0x0400085A RID: 2138
			public int enlightenVertexStreamMeshInstanceID;

			// Token: 0x0400085B RID: 2139
			public Matrix4x4 transform;

			// Token: 0x0400085C RID: 2140
			public Vector4 lightmapScaleOffset;

			// Token: 0x0400085D RID: 2141
			public Vector4 realtimeLightmapScaleOffset;
		}

		// Token: 0x02000247 RID: 583
		public struct SubMeshInstance
		{
			// Token: 0x0400085E RID: 2142
			public int meshInstanceID;

			// Token: 0x0400085F RID: 2143
			public int vertexOffset;

			// Token: 0x04000860 RID: 2144
			public int gameObjectInstanceID;

			// Token: 0x04000861 RID: 2145
			public int subMeshIndex;

			// Token: 0x04000862 RID: 2146
			public Matrix4x4 transform;
		}

		// Token: 0x02000248 RID: 584
		public struct MeshContainer
		{
			// Token: 0x04000863 RID: 2147
			public GameObject gameObject;

			// Token: 0x04000864 RID: 2148
			public MeshSubsetCombineUtility.MeshInstance instance;

			// Token: 0x04000865 RID: 2149
			public List<MeshSubsetCombineUtility.SubMeshInstance> subMeshInstances;
		}
	}
}
