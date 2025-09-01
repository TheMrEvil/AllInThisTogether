using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000018 RID: 24
	internal struct MeshInfo
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000068F8 File Offset: 0x00004AF8
		public MeshInfo(int size)
		{
			this.material = null;
			size = Mathf.Min(size, 16383);
			int num = size * 4;
			int num2 = size * 6;
			this.vertexCount = 0;
			this.vertices = new Vector3[num];
			this.uvs0 = new Vector2[num];
			this.uvs2 = new Vector2[num];
			this.colors32 = new Color32[num];
			this.triangles = new int[num2];
			int num3 = 0;
			int num4 = 0;
			while (num4 / 4 < size)
			{
				for (int i = 0; i < 4; i++)
				{
					this.vertices[num4 + i] = Vector3.zero;
					this.uvs0[num4 + i] = Vector2.zero;
					this.uvs2[num4 + i] = Vector2.zero;
					this.colors32[num4 + i] = MeshInfo.k_DefaultColor;
				}
				this.triangles[num3] = num4;
				this.triangles[num3 + 1] = num4 + 1;
				this.triangles[num3 + 2] = num4 + 2;
				this.triangles[num3 + 3] = num4 + 2;
				this.triangles[num3 + 4] = num4 + 3;
				this.triangles[num3 + 5] = num4;
				num4 += 4;
				num3 += 6;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006A38 File Offset: 0x00004C38
		internal void ResizeMeshInfo(int size)
		{
			size = Mathf.Min(size, 16383);
			int newSize = size * 4;
			int newSize2 = size * 6;
			int num = this.vertices.Length / 4;
			Array.Resize<Vector3>(ref this.vertices, newSize);
			Array.Resize<Vector2>(ref this.uvs0, newSize);
			Array.Resize<Vector2>(ref this.uvs2, newSize);
			Array.Resize<Color32>(ref this.colors32, newSize);
			Array.Resize<int>(ref this.triangles, newSize2);
			for (int i = num; i < size; i++)
			{
				int num2 = i * 4;
				int num3 = i * 6;
				this.triangles[num3] = num2;
				this.triangles[1 + num3] = 1 + num2;
				this.triangles[2 + num3] = 2 + num2;
				this.triangles[3 + num3] = 2 + num2;
				this.triangles[4 + num3] = 3 + num2;
				this.triangles[5 + num3] = num2;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006B1C File Offset: 0x00004D1C
		internal void Clear(bool uploadChanges)
		{
			bool flag = this.vertices == null;
			if (!flag)
			{
				Array.Clear(this.vertices, 0, this.vertices.Length);
				this.vertexCount = 0;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006B58 File Offset: 0x00004D58
		internal void ClearUnusedVertices()
		{
			int num = this.vertices.Length - this.vertexCount;
			bool flag = num > 0;
			if (flag)
			{
				Array.Clear(this.vertices, this.vertexCount, num);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006B94 File Offset: 0x00004D94
		internal void ClearUnusedVertices(int startIndex)
		{
			int num = this.vertices.Length - startIndex;
			bool flag = num > 0;
			if (flag)
			{
				Array.Clear(this.vertices, startIndex, num);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal void SortGeometry(VertexSortingOrder order)
		{
			if (order != VertexSortingOrder.Normal)
			{
				if (order == VertexSortingOrder.Reverse)
				{
					int num = this.vertexCount / 4;
					for (int i = 0; i < num; i++)
					{
						int num2 = i * 4;
						int num3 = (num - i - 1) * 4;
						bool flag = num2 < num3;
						if (flag)
						{
							this.SwapVertexData(num2, num3);
						}
					}
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006C28 File Offset: 0x00004E28
		internal void SwapVertexData(int src, int dst)
		{
			Vector3 vector = this.vertices[dst];
			this.vertices[dst] = this.vertices[src];
			this.vertices[src] = vector;
			vector = this.vertices[dst + 1];
			this.vertices[dst + 1] = this.vertices[src + 1];
			this.vertices[src + 1] = vector;
			vector = this.vertices[dst + 2];
			this.vertices[dst + 2] = this.vertices[src + 2];
			this.vertices[src + 2] = vector;
			vector = this.vertices[dst + 3];
			this.vertices[dst + 3] = this.vertices[src + 3];
			this.vertices[src + 3] = vector;
			Vector2 vector2 = this.uvs0[dst];
			this.uvs0[dst] = this.uvs0[src];
			this.uvs0[src] = vector2;
			vector2 = this.uvs0[dst + 1];
			this.uvs0[dst + 1] = this.uvs0[src + 1];
			this.uvs0[src + 1] = vector2;
			vector2 = this.uvs0[dst + 2];
			this.uvs0[dst + 2] = this.uvs0[src + 2];
			this.uvs0[src + 2] = vector2;
			vector2 = this.uvs0[dst + 3];
			this.uvs0[dst + 3] = this.uvs0[src + 3];
			this.uvs0[src + 3] = vector2;
			vector2 = this.uvs2[dst];
			this.uvs2[dst] = this.uvs2[src];
			this.uvs2[src] = vector2;
			vector2 = this.uvs2[dst + 1];
			this.uvs2[dst + 1] = this.uvs2[src + 1];
			this.uvs2[src + 1] = vector2;
			vector2 = this.uvs2[dst + 2];
			this.uvs2[dst + 2] = this.uvs2[src + 2];
			this.uvs2[src + 2] = vector2;
			vector2 = this.uvs2[dst + 3];
			this.uvs2[dst + 3] = this.uvs2[src + 3];
			this.uvs2[src + 3] = vector2;
			Color32 color = this.colors32[dst];
			this.colors32[dst] = this.colors32[src];
			this.colors32[src] = color;
			color = this.colors32[dst + 1];
			this.colors32[dst + 1] = this.colors32[src + 1];
			this.colors32[src + 1] = color;
			color = this.colors32[dst + 2];
			this.colors32[dst + 2] = this.colors32[src + 2];
			this.colors32[src + 2] = color;
			color = this.colors32[dst + 3];
			this.colors32[dst + 3] = this.colors32[src + 3];
			this.colors32[src + 3] = color;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006FC2 File Offset: 0x000051C2
		// Note: this type is marked as 'beforefieldinit'.
		static MeshInfo()
		{
		}

		// Token: 0x0400009A RID: 154
		private static readonly Color32 k_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400009B RID: 155
		public int vertexCount;

		// Token: 0x0400009C RID: 156
		public Vector3[] vertices;

		// Token: 0x0400009D RID: 157
		public Vector2[] uvs0;

		// Token: 0x0400009E RID: 158
		public Vector2[] uvs2;

		// Token: 0x0400009F RID: 159
		public Color32[] colors32;

		// Token: 0x040000A0 RID: 160
		public int[] triangles;

		// Token: 0x040000A1 RID: 161
		public Material material;
	}
}
