using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200001A RID: 26
	internal sealed class FaceRebuildData
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000100BA File Offset: 0x0000E2BA
		public int Offset()
		{
			return this._appliedOffset;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000100C2 File Offset: 0x0000E2C2
		public override string ToString()
		{
			return string.Format("{0}\n{1}", this.vertices.ToString(", "), this.sharedIndexes.ToString(", "));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public static void Apply(IEnumerable<FaceRebuildData> newFaces, ProBuilderMesh mesh, List<Vertex> vertices = null, List<Face> faces = null)
		{
			if (faces == null)
			{
				faces = new List<Face>(mesh.facesInternal);
			}
			if (vertices == null)
			{
				vertices = new List<Vertex>(mesh.GetVertices(null));
			}
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = mesh.sharedTextureLookup;
			FaceRebuildData.Apply(newFaces, vertices, faces, sharedVertexLookup, sharedTextureLookup);
			mesh.SetVertices(vertices, false);
			mesh.faces = faces;
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(sharedTextureLookup);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00010154 File Offset: 0x0000E354
		public static void Apply(IEnumerable<FaceRebuildData> newFaces, List<Vertex> vertices, List<Face> faces, Dictionary<int, int> sharedVertexLookup, Dictionary<int, int> sharedTextureLookup = null)
		{
			int num = vertices.Count;
			foreach (FaceRebuildData faceRebuildData in newFaces)
			{
				Face face = faceRebuildData.face;
				int count = faceRebuildData.vertices.Count;
				bool flag = sharedVertexLookup != null && faceRebuildData.sharedIndexes != null && faceRebuildData.sharedIndexes.Count == count;
				bool flag2 = sharedTextureLookup != null && faceRebuildData.sharedIndexesUV != null && faceRebuildData.sharedIndexesUV.Count == count;
				for (int i = 0; i < count; i++)
				{
					int num2 = i;
					if (sharedVertexLookup != null)
					{
						sharedVertexLookup.Add(num2 + num, flag ? faceRebuildData.sharedIndexes[num2] : -1);
					}
					if (sharedTextureLookup != null && flag2)
					{
						sharedTextureLookup.Add(num2 + num, faceRebuildData.sharedIndexesUV[num2]);
					}
				}
				faceRebuildData._appliedOffset = num;
				int[] indexesInternal = face.indexesInternal;
				int j = 0;
				int num3 = indexesInternal.Length;
				while (j < num3)
				{
					indexesInternal[j] += num;
					j++;
				}
				num += faceRebuildData.vertices.Count;
				face.indexesInternal = indexesInternal;
				faces.Add(face);
				vertices.AddRange(faceRebuildData.vertices);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public FaceRebuildData()
		{
		}

		// Token: 0x0400005C RID: 92
		public Face face;

		// Token: 0x0400005D RID: 93
		public List<Vertex> vertices;

		// Token: 0x0400005E RID: 94
		public List<int> sharedIndexes;

		// Token: 0x0400005F RID: 95
		public List<int> sharedIndexesUV;

		// Token: 0x04000060 RID: 96
		private int _appliedOffset;
	}
}
