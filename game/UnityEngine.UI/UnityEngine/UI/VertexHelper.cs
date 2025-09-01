using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x0200003D RID: 61
	public class VertexHelper : IDisposable
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00015F1E File Offset: 0x0001411E
		public VertexHelper()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00015F28 File Offset: 0x00014128
		public VertexHelper(Mesh m)
		{
			this.InitializeListIfRequired();
			this.m_Positions.AddRange(m.vertices);
			this.m_Colors.AddRange(m.colors32);
			List<Vector4> list = new List<Vector4>();
			m.GetUVs(0, list);
			this.m_Uv0S.AddRange(list);
			m.GetUVs(1, list);
			this.m_Uv1S.AddRange(list);
			m.GetUVs(2, list);
			this.m_Uv2S.AddRange(list);
			m.GetUVs(3, list);
			this.m_Uv3S.AddRange(list);
			this.m_Normals.AddRange(m.normals);
			this.m_Tangents.AddRange(m.tangents);
			this.m_Indices.AddRange(m.GetIndices(0));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015FF0 File Offset: 0x000141F0
		private void InitializeListIfRequired()
		{
			if (!this.m_ListsInitalized)
			{
				this.m_Positions = CollectionPool<List<Vector3>, Vector3>.Get();
				this.m_Colors = CollectionPool<List<Color32>, Color32>.Get();
				this.m_Uv0S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv1S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv2S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Uv3S = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Normals = CollectionPool<List<Vector3>, Vector3>.Get();
				this.m_Tangents = CollectionPool<List<Vector4>, Vector4>.Get();
				this.m_Indices = CollectionPool<List<int>, int>.Get();
				this.m_ListsInitalized = true;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016070 File Offset: 0x00014270
		public void Dispose()
		{
			if (this.m_ListsInitalized)
			{
				CollectionPool<List<Vector3>, Vector3>.Release(this.m_Positions);
				CollectionPool<List<Color32>, Color32>.Release(this.m_Colors);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv0S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv1S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv2S);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Uv3S);
				CollectionPool<List<Vector3>, Vector3>.Release(this.m_Normals);
				CollectionPool<List<Vector4>, Vector4>.Release(this.m_Tangents);
				CollectionPool<List<int>, int>.Release(this.m_Indices);
				this.m_Positions = null;
				this.m_Colors = null;
				this.m_Uv0S = null;
				this.m_Uv1S = null;
				this.m_Uv2S = null;
				this.m_Uv3S = null;
				this.m_Normals = null;
				this.m_Tangents = null;
				this.m_Indices = null;
				this.m_ListsInitalized = false;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016134 File Offset: 0x00014334
		public void Clear()
		{
			if (this.m_ListsInitalized)
			{
				this.m_Positions.Clear();
				this.m_Colors.Clear();
				this.m_Uv0S.Clear();
				this.m_Uv1S.Clear();
				this.m_Uv2S.Clear();
				this.m_Uv3S.Clear();
				this.m_Normals.Clear();
				this.m_Tangents.Clear();
				this.m_Indices.Clear();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000161AC File Offset: 0x000143AC
		public int currentVertCount
		{
			get
			{
				if (this.m_Positions == null)
				{
					return 0;
				}
				return this.m_Positions.Count;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000161C3 File Offset: 0x000143C3
		public int currentIndexCount
		{
			get
			{
				if (this.m_Indices == null)
				{
					return 0;
				}
				return this.m_Indices.Count;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000161DC File Offset: 0x000143DC
		public void PopulateUIVertex(ref UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			vertex.position = this.m_Positions[i];
			vertex.color = this.m_Colors[i];
			vertex.uv0 = this.m_Uv0S[i];
			vertex.uv1 = this.m_Uv1S[i];
			vertex.uv2 = this.m_Uv2S[i];
			vertex.uv3 = this.m_Uv3S[i];
			vertex.normal = this.m_Normals[i];
			vertex.tangent = this.m_Tangents[i];
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016280 File Offset: 0x00014480
		public void SetUIVertex(UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			this.m_Positions[i] = vertex.position;
			this.m_Colors[i] = vertex.color;
			this.m_Uv0S[i] = vertex.uv0;
			this.m_Uv1S[i] = vertex.uv1;
			this.m_Uv2S[i] = vertex.uv2;
			this.m_Uv3S[i] = vertex.uv3;
			this.m_Normals[i] = vertex.normal;
			this.m_Tangents[i] = vertex.tangent;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016324 File Offset: 0x00014524
		public void FillMesh(Mesh mesh)
		{
			this.InitializeListIfRequired();
			mesh.Clear();
			if (this.m_Positions.Count >= 65000)
			{
				throw new ArgumentException("Mesh can not have more than 65000 vertices");
			}
			mesh.SetVertices(this.m_Positions);
			mesh.SetColors(this.m_Colors);
			mesh.SetUVs(0, this.m_Uv0S);
			mesh.SetUVs(1, this.m_Uv1S);
			mesh.SetUVs(2, this.m_Uv2S);
			mesh.SetUVs(3, this.m_Uv3S);
			mesh.SetNormals(this.m_Normals);
			mesh.SetTangents(this.m_Tangents);
			mesh.SetTriangles(this.m_Indices, 0);
			mesh.RecalculateBounds();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000163D4 File Offset: 0x000145D4
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0, Vector4 uv1, Vector4 uv2, Vector4 uv3, Vector3 normal, Vector4 tangent)
		{
			this.InitializeListIfRequired();
			this.m_Positions.Add(position);
			this.m_Colors.Add(color);
			this.m_Uv0S.Add(uv0);
			this.m_Uv1S.Add(uv1);
			this.m_Uv2S.Add(uv2);
			this.m_Uv3S.Add(uv3);
			this.m_Normals.Add(normal);
			this.m_Tangents.Add(tangent);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001644C File Offset: 0x0001464C
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0, Vector4 uv1, Vector3 normal, Vector4 tangent)
		{
			this.AddVert(position, color, uv0, uv1, Vector4.zero, Vector4.zero, normal, tangent);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016472 File Offset: 0x00014672
		public void AddVert(Vector3 position, Color32 color, Vector4 uv0)
		{
			this.AddVert(position, color, uv0, Vector4.zero, VertexHelper.s_DefaultNormal, VertexHelper.s_DefaultTangent);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001648C File Offset: 0x0001468C
		public void AddVert(UIVertex v)
		{
			this.AddVert(v.position, v.color, v.uv0, v.uv1, v.uv2, v.uv3, v.normal, v.tangent);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000164CF File Offset: 0x000146CF
		public void AddTriangle(int idx0, int idx1, int idx2)
		{
			this.InitializeListIfRequired();
			this.m_Indices.Add(idx0);
			this.m_Indices.Add(idx1);
			this.m_Indices.Add(idx2);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000164FC File Offset: 0x000146FC
		public void AddUIVertexQuad(UIVertex[] verts)
		{
			int currentVertCount = this.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				this.AddVert(verts[i].position, verts[i].color, verts[i].uv0, verts[i].uv1, verts[i].normal, verts[i].tangent);
			}
			this.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			this.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016584 File Offset: 0x00014784
		public void AddUIVertexStream(List<UIVertex> verts, List<int> indices)
		{
			this.InitializeListIfRequired();
			if (verts != null)
			{
				CanvasRenderer.AddUIVertexStream(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents);
			}
			if (indices != null)
			{
				this.m_Indices.AddRange(indices);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000165E0 File Offset: 0x000147E0
		public void AddUIVertexTriangleStream(List<UIVertex> verts)
		{
			if (verts == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.SplitUIVertexStreams(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016634 File Offset: 0x00014834
		public void GetUIVertexStream(List<UIVertex> stream)
		{
			if (stream == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.CreateUIVertexStream(stream, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016687 File Offset: 0x00014887
		// Note: this type is marked as 'beforefieldinit'.
		static VertexHelper()
		{
		}

		// Token: 0x04000184 RID: 388
		private List<Vector3> m_Positions;

		// Token: 0x04000185 RID: 389
		private List<Color32> m_Colors;

		// Token: 0x04000186 RID: 390
		private List<Vector4> m_Uv0S;

		// Token: 0x04000187 RID: 391
		private List<Vector4> m_Uv1S;

		// Token: 0x04000188 RID: 392
		private List<Vector4> m_Uv2S;

		// Token: 0x04000189 RID: 393
		private List<Vector4> m_Uv3S;

		// Token: 0x0400018A RID: 394
		private List<Vector3> m_Normals;

		// Token: 0x0400018B RID: 395
		private List<Vector4> m_Tangents;

		// Token: 0x0400018C RID: 396
		private List<int> m_Indices;

		// Token: 0x0400018D RID: 397
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x0400018E RID: 398
		private static readonly Vector3 s_DefaultNormal = Vector3.back;

		// Token: 0x0400018F RID: 399
		private bool m_ListsInitalized;
	}
}
