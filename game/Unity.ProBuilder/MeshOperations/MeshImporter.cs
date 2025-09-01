using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000083 RID: 131
	public sealed class MeshImporter
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x0003207C File Offset: 0x0003027C
		public MeshImporter(GameObject gameObject)
		{
			MeshFilter component = gameObject.GetComponent<MeshFilter>();
			this.m_SourceMesh = component.sharedMesh;
			if (this.m_SourceMesh == null)
			{
				throw new ArgumentNullException("gameObject", "GameObject does not contain a valid MeshFilter.sharedMesh.");
			}
			this.m_Destination = gameObject.DemandComponent<ProBuilderMesh>();
			MeshRenderer component2 = gameObject.GetComponent<MeshRenderer>();
			this.m_SourceMaterials = ((component2 != null) ? component2.sharedMaterials : null);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000320E4 File Offset: 0x000302E4
		public MeshImporter(Mesh sourceMesh, Material[] sourceMaterials, ProBuilderMesh destination)
		{
			if (sourceMesh == null)
			{
				throw new ArgumentNullException("sourceMesh");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.m_SourceMesh = sourceMesh;
			this.m_SourceMaterials = sourceMaterials;
			this.m_Destination = destination;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00032134 File Offset: 0x00030334
		[Obsolete]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MeshImporter(ProBuilderMesh destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00032144 File Offset: 0x00030344
		[Obsolete]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Import(GameObject go, MeshImportSettings importSettings = null)
		{
			try
			{
				this.m_SourceMesh = go.GetComponent<MeshFilter>().sharedMesh;
				MeshRenderer component = go.GetComponent<MeshRenderer>();
				this.m_SourceMaterials = ((component != null) ? component.sharedMaterials : null);
				this.Import(importSettings);
			}
			catch (Exception ex)
			{
				Log.Error(ex.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000321A4 File Offset: 0x000303A4
		public void Import(MeshImportSettings importSettings = null)
		{
			if (importSettings == null)
			{
				importSettings = MeshImporter.k_DefaultImportSettings;
			}
			Vertex[] vertices = this.m_SourceMesh.GetVertices();
			List<Vertex> list = new List<Vertex>();
			List<Face> list2 = new List<Face>();
			int num = 0;
			int num2 = (this.m_SourceMaterials != null) ? this.m_SourceMaterials.Length : 0;
			for (int i = 0; i < this.m_SourceMesh.subMeshCount; i++)
			{
				MeshTopology topology = this.m_SourceMesh.GetTopology(i);
				if (topology != MeshTopology.Triangles)
				{
					if (topology != MeshTopology.Quads)
					{
						throw new NotSupportedException("ProBuilder only supports importing triangle and quad meshes.");
					}
					int[] indices = this.m_SourceMesh.GetIndices(i);
					for (int j = 0; j < indices.Length; j += 4)
					{
						list2.Add(new Face(new int[]
						{
							num,
							num + 1,
							num + 2,
							num + 2,
							num + 3,
							num
						}, Math.Clamp(i, 0, num2 - 1), AutoUnwrapSettings.tile, 0, -1, -1, true));
						list.Add(vertices[indices[j]]);
						list.Add(vertices[indices[j + 1]]);
						list.Add(vertices[indices[j + 2]]);
						list.Add(vertices[indices[j + 3]]);
						num += 4;
					}
				}
				else
				{
					int[] indices2 = this.m_SourceMesh.GetIndices(i);
					for (int k = 0; k < indices2.Length; k += 3)
					{
						list2.Add(new Face(new int[]
						{
							num,
							num + 1,
							num + 2
						}, Math.Clamp(i, 0, num2 - 1), AutoUnwrapSettings.tile, 0, -1, -1, true));
						list.Add(vertices[indices2[k]]);
						list.Add(vertices[indices2[k + 1]]);
						list.Add(vertices[indices2[k + 2]]);
						num += 3;
					}
				}
			}
			this.m_Vertices = list.ToArray();
			this.m_Destination.Clear();
			this.m_Destination.SetVertices(this.m_Vertices, false);
			this.m_Destination.faces = list2;
			this.m_Destination.sharedVertices = SharedVertex.GetSharedVerticesWithPositions(this.m_Destination.positionsInternal);
			this.m_Destination.sharedTextures = new SharedVertex[0];
			if (importSettings.quads)
			{
				this.m_Destination.ToQuads(this.m_Destination.facesInternal, !importSettings.smoothing);
			}
			if (importSettings.smoothing)
			{
				Smoothing.ApplySmoothingGroups(this.m_Destination, this.m_Destination.facesInternal, importSettings.smoothingAngle, (from x in this.m_Vertices
				select x.normal).ToArray<Vector3>());
				MergeElements.CollapseCoincidentVertices(this.m_Destination, this.m_Destination.facesInternal);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00032461 File Offset: 0x00030661
		// Note: this type is marked as 'beforefieldinit'.
		static MeshImporter()
		{
		}

		// Token: 0x0400026A RID: 618
		private static readonly MeshImportSettings k_DefaultImportSettings = new MeshImportSettings
		{
			quads = true,
			smoothing = true,
			smoothingAngle = 1f
		};

		// Token: 0x0400026B RID: 619
		private Mesh m_SourceMesh;

		// Token: 0x0400026C RID: 620
		private Material[] m_SourceMaterials;

		// Token: 0x0400026D RID: 621
		private ProBuilderMesh m_Destination;

		// Token: 0x0400026E RID: 622
		private Vertex[] m_Vertices;

		// Token: 0x020000BF RID: 191
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C9 RID: 1481 RVA: 0x00036537 File Offset: 0x00034737
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x00036543 File Offset: 0x00034743
			public <>c()
			{
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x0003654B File Offset: 0x0003474B
			internal Vector3 <Import>b__9_0(Vertex x)
			{
				return x.normal;
			}

			// Token: 0x04000315 RID: 789
			public static readonly MeshImporter.<>c <>9 = new MeshImporter.<>c();

			// Token: 0x04000316 RID: 790
			public static Func<Vertex, Vector3> <>9__9_0;
		}
	}
}
