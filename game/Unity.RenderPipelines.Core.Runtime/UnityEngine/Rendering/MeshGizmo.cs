using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AE RID: 174
	internal class MeshGizmo : IDisposable
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001BC80 File Offset: 0x00019E80
		public MeshGizmo(int capacity = 0)
		{
			this.vertices = new List<Vector3>(capacity);
			this.indices = new List<int>(capacity);
			this.colors = new List<Color>(capacity);
			this.mesh = new Mesh
			{
				indexFormat = IndexFormat.UInt32,
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001BCD1 File Offset: 0x00019ED1
		public void Clear()
		{
			this.vertices.Clear();
			this.indices.Clear();
			this.colors.Clear();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		public void AddWireCube(Vector3 center, Vector3 size, Color color)
		{
			MeshGizmo.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.color = color;
			Vector3 vector = size / 2f;
			Vector3 b = new Vector3(vector.x, vector.y, vector.z);
			Vector3 b2 = new Vector3(-vector.x, vector.y, vector.z);
			Vector3 b3 = new Vector3(-vector.x, -vector.y, vector.z);
			Vector3 b4 = new Vector3(vector.x, -vector.y, vector.z);
			Vector3 b5 = new Vector3(vector.x, vector.y, -vector.z);
			Vector3 b6 = new Vector3(-vector.x, vector.y, -vector.z);
			Vector3 b7 = new Vector3(-vector.x, -vector.y, -vector.z);
			Vector3 b8 = new Vector3(vector.x, -vector.y, -vector.z);
			this.<AddWireCube>g__AddEdge|10_0(center + b, center + b2, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b2, center + b3, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b3, center + b4, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b4, center + b, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b5, center + b6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b6, center + b7, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b7, center + b8, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b8, center + b5, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b, center + b5, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b2, center + b6, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b3, center + b7, ref CS$<>8__locals1);
			this.<AddWireCube>g__AddEdge|10_0(center + b4, center + b8, ref CS$<>8__locals1);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001BF0C File Offset: 0x0001A10C
		private void DrawMesh(Matrix4x4 trs, Material mat, MeshTopology topology, CompareFunction depthTest, string gizmoName)
		{
			this.mesh.Clear();
			this.mesh.SetVertices(this.vertices);
			this.mesh.SetColors(this.colors);
			this.mesh.SetIndices(this.indices, topology, 0, true, 0);
			mat.SetFloat("_HandleZTest", (float)depthTest);
			CommandBuffer commandBuffer = CommandBufferPool.Get(gizmoName ?? "Mesh Gizmo Rendering");
			commandBuffer.DrawMesh(this.mesh, trs, mat, 0, 0);
			Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001BF8E File Offset: 0x0001A18E
		public void RenderWireframe(Matrix4x4 trs, CompareFunction depthTest = CompareFunction.LessEqual, string gizmoName = null)
		{
			this.DrawMesh(trs, this.wireMaterial, MeshTopology.Lines, depthTest, gizmoName);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public void Dispose()
		{
			CoreUtils.Destroy(this.mesh);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001BFAD File Offset: 0x0001A1AD
		// Note: this type is marked as 'beforefieldinit'.
		static MeshGizmo()
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		[CompilerGenerated]
		private void <AddWireCube>g__AddEdge|10_0(Vector3 p1, Vector3 p2, ref MeshGizmo.<>c__DisplayClass10_0 A_3)
		{
			this.vertices.Add(p1);
			this.vertices.Add(p2);
			this.indices.Add(this.indices.Count);
			this.indices.Add(this.indices.Count);
			this.colors.Add(A_3.color);
			this.colors.Add(A_3.color);
		}

		// Token: 0x0400037D RID: 893
		public static readonly int vertexCountPerCube = 24;

		// Token: 0x0400037E RID: 894
		public Mesh mesh;

		// Token: 0x0400037F RID: 895
		private List<Vector3> vertices;

		// Token: 0x04000380 RID: 896
		private List<int> indices;

		// Token: 0x04000381 RID: 897
		private List<Color> colors;

		// Token: 0x04000382 RID: 898
		private Material wireMaterial;

		// Token: 0x04000383 RID: 899
		private Material dottedWireMaterial;

		// Token: 0x04000384 RID: 900
		private Material solidMaterial;

		// Token: 0x0200017C RID: 380
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass10_0
		{
			// Token: 0x040005BC RID: 1468
			public MeshGizmo <>4__this;

			// Token: 0x040005BD RID: 1469
			public Color color;
		}
	}
}
