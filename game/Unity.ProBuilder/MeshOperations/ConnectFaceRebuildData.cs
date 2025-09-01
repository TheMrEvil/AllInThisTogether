using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200007B RID: 123
	internal sealed class ConnectFaceRebuildData
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		public ConnectFaceRebuildData(FaceRebuildData faceRebuildData, List<int> newVertexIndexes)
		{
			this.faceRebuildData = faceRebuildData;
			this.newVertexIndexes = newVertexIndexes;
		}

		// Token: 0x04000263 RID: 611
		public FaceRebuildData faceRebuildData;

		// Token: 0x04000264 RID: 612
		public List<int> newVertexIndexes;
	}
}
