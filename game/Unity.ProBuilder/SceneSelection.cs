using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200004D RID: 77
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SceneSelection : IEquatable<SceneSelection>
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0001BABB File Offset: 0x00019CBB
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001BAC3 File Offset: 0x00019CC3
		public List<int> vertexes
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0001BACC File Offset: 0x00019CCC
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public List<Edge> edges
		{
			get
			{
				return this.m_Edges;
			}
			set
			{
				this.m_Edges = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0001BADD File Offset: 0x00019CDD
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0001BAE5 File Offset: 0x00019CE5
		public List<Face> faces
		{
			get
			{
				return this.m_Faces;
			}
			set
			{
				this.m_Faces = value;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001BAEE File Offset: 0x00019CEE
		public SceneSelection(GameObject gameObject = null)
		{
			this.gameObject = gameObject;
			this.m_Vertices = new List<int>();
			this.m_Edges = new List<Edge>();
			this.m_Faces = new List<Face>();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001BB1E File Offset: 0x00019D1E
		public SceneSelection(ProBuilderMesh mesh, int vertex) : this(mesh, new List<int>
		{
			vertex
		})
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001BB33 File Offset: 0x00019D33
		public SceneSelection(ProBuilderMesh mesh, Edge edge) : this(mesh, new List<Edge>
		{
			edge
		})
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001BB48 File Offset: 0x00019D48
		public SceneSelection(ProBuilderMesh mesh, Face face) : this(mesh, new List<Face>
		{
			face
		})
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001BB5D File Offset: 0x00019D5D
		internal SceneSelection(ProBuilderMesh mesh, List<int> vertexes) : this((mesh != null) ? mesh.gameObject : null)
		{
			this.mesh = mesh;
			this.m_Vertices = vertexes;
			this.m_Edges = new List<Edge>();
			this.m_Faces = new List<Face>();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001BB9B File Offset: 0x00019D9B
		internal SceneSelection(ProBuilderMesh mesh, List<Edge> edges) : this((mesh != null) ? mesh.gameObject : null)
		{
			this.mesh = mesh;
			this.vertexes = new List<int>();
			this.edges = edges;
			this.faces = new List<Face>();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001BBD9 File Offset: 0x00019DD9
		internal SceneSelection(ProBuilderMesh mesh, List<Face> faces) : this((mesh != null) ? mesh.gameObject : null)
		{
			this.mesh = mesh;
			this.vertexes = new List<int>();
			this.edges = new List<Edge>();
			this.faces = faces;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001BC17 File Offset: 0x00019E17
		public void SetSingleFace(Face face)
		{
			this.faces.Clear();
			this.faces.Add(face);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001BC30 File Offset: 0x00019E30
		public void SetSingleVertex(int vertex)
		{
			this.vertexes.Clear();
			this.vertexes.Add(vertex);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001BC49 File Offset: 0x00019E49
		public void SetSingleEdge(Edge edge)
		{
			this.edges.Clear();
			this.edges.Add(edge);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001BC62 File Offset: 0x00019E62
		public void Clear()
		{
			this.gameObject = null;
			this.mesh = null;
			this.faces.Clear();
			this.edges.Clear();
			this.vertexes.Clear();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001BC94 File Offset: 0x00019E94
		public void CopyTo(SceneSelection dst)
		{
			dst.gameObject = this.gameObject;
			dst.mesh = this.mesh;
			dst.faces.Clear();
			dst.edges.Clear();
			dst.vertexes.Clear();
			dst.faces.AddRange(this.faces);
			dst.edges.AddRange(this.edges);
			dst.vertexes.AddRange(this.vertexes);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001BD10 File Offset: 0x00019F10
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("GameObject: " + ((this.gameObject != null) ? this.gameObject.name : null));
			stringBuilder.AppendLine("ProBuilderMesh: " + ((this.mesh != null) ? this.mesh.name : null));
			stringBuilder.AppendLine("Face: " + ((this.faces != null) ? this.faces.ToString() : null));
			stringBuilder.AppendLine("Edge: " + this.edges.ToString());
			string str = "Vertex: ";
			List<int> vertexes = this.vertexes;
			stringBuilder.AppendLine(str + ((vertexes != null) ? vertexes.ToString() : null));
			return stringBuilder.ToString();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		public bool Equals(SceneSelection other)
		{
			return other != null && (this == other || (object.Equals(this.gameObject, other.gameObject) && object.Equals(this.mesh, other.mesh) && this.vertexes.SequenceEqual(other.vertexes) && this.edges.SequenceEqual(other.edges) && this.faces.SequenceEqual(other.faces)));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001BE5F File Offset: 0x0001A05F
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SceneSelection)obj)));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001BE90 File Offset: 0x0001A090
		public override int GetHashCode()
		{
			return (((((this.gameObject != null) ? this.gameObject.GetHashCode() : 0) * 397 ^ ((this.mesh != null) ? this.mesh.GetHashCode() : 0)) * 397 ^ ((this.vertexes != null) ? this.vertexes.GetHashCode() : 0)) * 397 ^ ((this.edges != null) ? this.edges.GetHashCode() : 0)) * 397 ^ ((this.faces != null) ? this.faces.GetHashCode() : 0);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001BF33 File Offset: 0x0001A133
		public static bool operator ==(SceneSelection left, SceneSelection right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001BF3C File Offset: 0x0001A13C
		public static bool operator !=(SceneSelection left, SceneSelection right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x040001C3 RID: 451
		public GameObject gameObject;

		// Token: 0x040001C4 RID: 452
		public ProBuilderMesh mesh;

		// Token: 0x040001C5 RID: 453
		private List<int> m_Vertices;

		// Token: 0x040001C6 RID: 454
		private List<Edge> m_Edges;

		// Token: 0x040001C7 RID: 455
		private List<Face> m_Faces;

		// Token: 0x040001C8 RID: 456
		[Obsolete("Use SetSingleVertex")]
		public int vertex;

		// Token: 0x040001C9 RID: 457
		[Obsolete("Use SetSingleEdge")]
		public Edge edge;

		// Token: 0x040001CA RID: 458
		[Obsolete("Use SetSingleFace")]
		public Face face;
	}
}
