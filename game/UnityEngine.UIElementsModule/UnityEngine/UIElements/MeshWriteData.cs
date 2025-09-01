using System;
using Unity.Collections;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025D RID: 605
	public class MeshWriteData
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x00049058 File Offset: 0x00047258
		internal MeshWriteData()
		{
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00049064 File Offset: 0x00047264
		public int vertexCount
		{
			get
			{
				return this.m_Vertices.Length;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00049084 File Offset: 0x00047284
		public int indexCount
		{
			get
			{
				return this.m_Indices.Length;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000490A4 File Offset: 0x000472A4
		public Rect uvRegion
		{
			get
			{
				return this.m_UVRegion;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000490BC File Offset: 0x000472BC
		public void SetNextVertex(Vertex vertex)
		{
			int num = this.currentVertex;
			this.currentVertex = num + 1;
			this.m_Vertices[num] = vertex;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000490E8 File Offset: 0x000472E8
		public void SetNextIndex(ushort index)
		{
			int num = this.currentIndex;
			this.currentIndex = num + 1;
			this.m_Indices[num] = index;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00049114 File Offset: 0x00047314
		public void SetAllVertices(Vertex[] vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004915C File Offset: 0x0004735C
		public void SetAllVertices(NativeSlice<Vertex> vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000491A4 File Offset: 0x000473A4
		public void SetAllIndices(ushort[] indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000491EC File Offset: 0x000473EC
		public void SetAllIndices(NativeSlice<ushort> indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00049234 File Offset: 0x00047434
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = new Rect(0f, 0f, 1f, 1f);
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00049280 File Offset: 0x00047480
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Rect uvRegion)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = uvRegion;
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x0400085E RID: 2142
		internal NativeSlice<Vertex> m_Vertices;

		// Token: 0x0400085F RID: 2143
		internal NativeSlice<ushort> m_Indices;

		// Token: 0x04000860 RID: 2144
		internal Rect m_UVRegion;

		// Token: 0x04000861 RID: 2145
		internal int currentIndex;

		// Token: 0x04000862 RID: 2146
		internal int currentVertex;
	}
}
