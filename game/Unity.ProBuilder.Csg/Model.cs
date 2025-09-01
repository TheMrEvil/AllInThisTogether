using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000002 RID: 2
	internal sealed class Model
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public List<Material> materials
		{
			get
			{
				return this.m_Materials;
			}
			set
			{
				this.m_Materials = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public List<Vertex> vertices
		{
			get
			{
				return this.m_Vertices;
			}
			set
			{
				this.m_Vertices = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public List<List<int>> indices
		{
			get
			{
				return this.m_Indices;
			}
			set
			{
				this.m_Indices = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		public Mesh mesh
		{
			get
			{
				return (Mesh)this;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public Model(GameObject gameObject)
		{
			MeshFilter component = gameObject.GetComponent<MeshFilter>();
			Mesh mesh = (component != null) ? component.sharedMesh : null;
			MeshRenderer component2 = gameObject.GetComponent<MeshRenderer>();
			this..ctor(mesh, (component2 != null) ? component2.sharedMaterials : null, gameObject.GetComponent<Transform>());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020C0 File Offset: 0x000002C0
		public Model(Mesh mesh, Material[] materials, Transform transform)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			this.m_Vertices = (from x in mesh.GetVertices()
			select transform.TransformVertex(x)).ToList<Vertex>();
			this.m_Materials = new List<Material>(materials);
			this.m_Indices = new List<List<int>>();
			int i = 0;
			int subMeshCount = mesh.subMeshCount;
			while (i < subMeshCount)
			{
				if (mesh.GetTopology(i) == MeshTopology.Triangles)
				{
					List<int> list = new List<int>();
					mesh.GetIndices(list, i);
					this.m_Indices.Add(list);
				}
				i++;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217C File Offset: 0x0000037C
		internal Model(List<Polygon> polygons)
		{
			this.m_Vertices = new List<Vertex>();
			Dictionary<Material, List<int>> dictionary = new Dictionary<Material, List<int>>();
			int num = 0;
			for (int i = 0; i < polygons.Count; i++)
			{
				Polygon polygon = polygons[i];
				List<int> list;
				if (!dictionary.TryGetValue(polygon.material, out list))
				{
					dictionary.Add(polygon.material, list = new List<int>());
				}
				for (int j = 2; j < polygon.vertices.Count; j++)
				{
					this.m_Vertices.Add(polygon.vertices[0]);
					list.Add(num++);
					this.m_Vertices.Add(polygon.vertices[j - 1]);
					list.Add(num++);
					this.m_Vertices.Add(polygon.vertices[j]);
					list.Add(num++);
				}
			}
			this.m_Materials = dictionary.Keys.ToList<Material>();
			this.m_Indices = dictionary.Values.ToList<List<int>>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002294 File Offset: 0x00000494
		internal List<Polygon> ToPolygons()
		{
			List<Polygon> list = new List<Polygon>();
			int i = 0;
			int count = this.m_Indices.Count;
			while (i < count)
			{
				List<int> list2 = this.m_Indices[i];
				int j = 0;
				int count2 = list2.Count;
				while (j < list2.Count)
				{
					List<Vertex> list3 = new List<Vertex>
					{
						this.m_Vertices[list2[j]],
						this.m_Vertices[list2[j + 1]],
						this.m_Vertices[list2[j + 2]]
					};
					list.Add(new Polygon(list3, this.m_Materials[i]));
					j += 3;
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002360 File Offset: 0x00000560
		public static explicit operator Mesh(Model model)
		{
			Mesh mesh = new Mesh();
			VertexUtility.SetMesh(mesh, model.m_Vertices);
			mesh.subMeshCount = model.m_Indices.Count;
			int i = 0;
			int subMeshCount = mesh.subMeshCount;
			while (i < subMeshCount)
			{
				mesh.SetIndices(model.m_Indices[i], MeshTopology.Triangles, i, true, 0);
				i++;
			}
			return mesh;
		}

		// Token: 0x04000001 RID: 1
		private List<Vertex> m_Vertices;

		// Token: 0x04000002 RID: 2
		private List<Material> m_Materials;

		// Token: 0x04000003 RID: 3
		private List<List<int>> m_Indices;

		// Token: 0x0200000A RID: 10
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00003900 File Offset: 0x00001B00
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003908 File Offset: 0x00001B08
			internal Vertex <.ctor>b__0(Vertex x)
			{
				return this.transform.TransformVertex(x);
			}

			// Token: 0x04000023 RID: 35
			public Transform transform;
		}
	}
}
