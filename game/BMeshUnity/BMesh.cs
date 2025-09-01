using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class BMesh
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public BMesh()
	{
		this.vertices = new List<BMesh.Vertex>();
		this.loops = new List<BMesh.Loop>();
		this.edges = new List<BMesh.Edge>();
		this.faces = new List<BMesh.Face>();
		this.vertexAttributes = new List<BMesh.AttributeDefinition>();
		this.edgeAttributes = new List<BMesh.AttributeDefinition>();
		this.loopAttributes = new List<BMesh.AttributeDefinition>();
		this.faceAttributes = new List<BMesh.AttributeDefinition>();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020BB File Offset: 0x000002BB
	public BMesh.Vertex AddVertex(BMesh.Vertex vert)
	{
		this.EnsureVertexAttributes(vert);
		this.vertices.Add(vert);
		return vert;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020D1 File Offset: 0x000002D1
	public BMesh.Vertex AddVertex(Vector3 point)
	{
		return this.AddVertex(new BMesh.Vertex(point));
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020DF File Offset: 0x000002DF
	public BMesh.Vertex AddVertex(float x, float y, float z)
	{
		return this.AddVertex(new Vector3(x, y, z));
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
	public BMesh.Edge AddEdge(BMesh.Vertex vert1, BMesh.Vertex vert2)
	{
		BMesh.Edge edge = this.FindEdge(vert1, vert2);
		if (edge != null)
		{
			return edge;
		}
		edge = new BMesh.Edge
		{
			vert1 = vert1,
			vert2 = vert2
		};
		this.EnsureEdgeAttributes(edge);
		this.edges.Add(edge);
		if (vert1.edge == null)
		{
			vert1.edge = edge;
			edge.next1 = (edge.prev1 = edge);
		}
		else
		{
			edge.next1 = vert1.edge.Next(vert1);
			edge.prev1 = vert1.edge;
			edge.next1.SetPrev(vert1, edge);
			edge.prev1.SetNext(vert1, edge);
		}
		if (vert2.edge == null)
		{
			vert2.edge = edge;
			edge.next2 = (edge.prev2 = edge);
		}
		else
		{
			edge.next2 = vert2.edge.Next(vert2);
			edge.prev2 = vert2.edge;
			edge.next2.SetPrev(vert2, edge);
			edge.prev2.SetNext(vert2, edge);
		}
		return edge;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021E5 File Offset: 0x000003E5
	public BMesh.Edge AddEdge(int v1, int v2)
	{
		return this.AddEdge(this.vertices[v1], this.vertices[v2]);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002208 File Offset: 0x00000408
	public BMesh.Face AddFace(BMesh.Vertex[] fVerts)
	{
		if (fVerts.Length == 0)
		{
			return null;
		}
		foreach (BMesh.Vertex vertex in fVerts)
		{
		}
		BMesh.Edge[] array = new BMesh.Edge[fVerts.Length];
		int num = fVerts.Length - 1;
		for (int j = 0; j < fVerts.Length; j++)
		{
			array[num] = this.AddEdge(fVerts[num], fVerts[j]);
			num = j;
		}
		BMesh.Face face = new BMesh.Face();
		this.EnsureFaceAttributes(face);
		this.faces.Add(face);
		for (int j = 0; j < fVerts.Length; j++)
		{
			BMesh.Loop loop = new BMesh.Loop(fVerts[j], array[j], face);
			this.EnsureLoopAttributes(loop);
			this.loops.Add(loop);
		}
		face.vertcount = fVerts.Length;
		return face;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000022B9 File Offset: 0x000004B9
	public BMesh.Face AddFace(BMesh.Vertex v0, BMesh.Vertex v1)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			v0,
			v1
		});
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000022CF File Offset: 0x000004CF
	public BMesh.Face AddFace(BMesh.Vertex v0, BMesh.Vertex v1, BMesh.Vertex v2)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			v0,
			v1,
			v2
		});
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000022E9 File Offset: 0x000004E9
	public BMesh.Face AddFace(BMesh.Vertex v0, BMesh.Vertex v1, BMesh.Vertex v2, BMesh.Vertex v3)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			v0,
			v1,
			v2,
			v3
		});
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002308 File Offset: 0x00000508
	public BMesh.Face AddFace(int i0, int i1)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			this.vertices[i0],
			this.vertices[i1]
		});
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002334 File Offset: 0x00000534
	public BMesh.Face AddFace(int i0, int i1, int i2)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			this.vertices[i0],
			this.vertices[i1],
			this.vertices[i2]
		});
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002370 File Offset: 0x00000570
	public BMesh.Face AddFace(int i0, int i1, int i2, int i3)
	{
		return this.AddFace(new BMesh.Vertex[]
		{
			this.vertices[i0],
			this.vertices[i1],
			this.vertices[i2],
			this.vertices[i3]
		});
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000023C8 File Offset: 0x000005C8
	public BMesh.Edge FindEdge(BMesh.Vertex vert1, BMesh.Vertex vert2)
	{
		if (vert1.edge == null || vert2.edge == null)
		{
			return null;
		}
		BMesh.Edge edge = vert1.edge;
		BMesh.Edge edge2 = vert2.edge;
		while (!edge.ContainsVertex(vert2))
		{
			if (edge2.ContainsVertex(vert1))
			{
				return edge2;
			}
			edge = edge.Next(vert1);
			edge2 = edge2.Next(vert2);
			if (edge == vert1.edge || edge2 == vert2.edge)
			{
				return null;
			}
		}
		return edge;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000242E File Offset: 0x0000062E
	public void RemoveVertex(BMesh.Vertex v)
	{
		while (v.edge != null)
		{
			this.RemoveEdge(v.edge);
		}
		this.vertices.Remove(v);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002454 File Offset: 0x00000654
	public void RemoveEdge(BMesh.Edge e)
	{
		while (e.loop != null)
		{
			this.RemoveLoop(e.loop);
		}
		if (e == e.vert1.edge)
		{
			e.vert1.edge = ((e.next1 != e) ? e.next1 : null);
		}
		if (e == e.vert2.edge)
		{
			e.vert2.edge = ((e.next2 != e) ? e.next2 : null);
		}
		e.prev1.SetNext(e.vert1, e.next1);
		e.next1.SetPrev(e.vert1, e.prev1);
		e.prev2.SetNext(e.vert2, e.next2);
		e.next2.SetPrev(e.vert2, e.prev2);
		this.edges.Remove(e);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002538 File Offset: 0x00000738
	private void RemoveLoop(BMesh.Loop l)
	{
		if (l.face != null)
		{
			this.RemoveFace(l.face);
			return;
		}
		if (l.radial_next == l)
		{
			l.edge.loop = null;
		}
		else
		{
			l.radial_prev.radial_next = l.radial_next;
			l.radial_next.radial_prev = l.radial_prev;
			if (l.edge.loop == l)
			{
				l.edge.loop = l.radial_next;
			}
		}
		l.next = null;
		l.prev = null;
		this.loops.Remove(l);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000025D0 File Offset: 0x000007D0
	public void RemoveFace(BMesh.Face f)
	{
		BMesh.Loop loop = f.loop;
		BMesh.Loop loop2 = null;
		while (loop2 != f.loop)
		{
			loop2 = loop.next;
			loop.face = null;
			this.RemoveLoop(loop);
			loop = loop2;
		}
		this.faces.Remove(f);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002618 File Offset: 0x00000818
	public bool HasVertexAttribute(string attribName)
	{
		using (List<BMesh.AttributeDefinition>.Enumerator enumerator = this.vertexAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == attribName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002678 File Offset: 0x00000878
	public bool HasVertexAttribute(BMesh.AttributeDefinition attrib)
	{
		return this.HasVertexAttribute(attrib.name);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002688 File Offset: 0x00000888
	public BMesh.AttributeDefinition AddVertexAttribute(BMesh.AttributeDefinition attrib)
	{
		if (this.HasVertexAttribute(attrib))
		{
			return attrib;
		}
		this.vertexAttributes.Add(attrib);
		foreach (BMesh.Vertex vertex in this.vertices)
		{
			if (vertex.attributes == null)
			{
				vertex.attributes = new Dictionary<string, BMesh.AttributeValue>();
			}
			vertex.attributes[attrib.name] = BMesh.AttributeValue.Copy(attrib.defaultValue);
		}
		return attrib;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000271C File Offset: 0x0000091C
	public BMesh.AttributeDefinition AddVertexAttribute(string name, BMesh.AttributeBaseType baseType, int dimensions)
	{
		return this.AddVertexAttribute(new BMesh.AttributeDefinition(name, baseType, dimensions));
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000272C File Offset: 0x0000092C
	private void EnsureVertexAttributes(BMesh.Vertex v)
	{
		if (v.attributes == null)
		{
			v.attributes = new Dictionary<string, BMesh.AttributeValue>();
		}
		foreach (BMesh.AttributeDefinition attributeDefinition in this.vertexAttributes)
		{
			if (!v.attributes.ContainsKey(attributeDefinition.name))
			{
				v.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
			else if (!attributeDefinition.type.CheckValue(v.attributes[attributeDefinition.name]))
			{
				Debug.LogWarning("Vertex attribute '" + attributeDefinition.name + "' is not compatible with mesh attribute definition, ignoring.");
				v.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002818 File Offset: 0x00000A18
	public bool HasEdgeAttribute(string attribName)
	{
		using (List<BMesh.AttributeDefinition>.Enumerator enumerator = this.edgeAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == attribName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002878 File Offset: 0x00000A78
	public bool HasEdgeAttribute(BMesh.AttributeDefinition attrib)
	{
		return this.HasEdgeAttribute(attrib.name);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002888 File Offset: 0x00000A88
	public BMesh.AttributeDefinition AddEdgeAttribute(BMesh.AttributeDefinition attrib)
	{
		if (this.HasEdgeAttribute(attrib))
		{
			return attrib;
		}
		this.edgeAttributes.Add(attrib);
		foreach (BMesh.Edge edge in this.edges)
		{
			if (edge.attributes == null)
			{
				edge.attributes = new Dictionary<string, BMesh.AttributeValue>();
			}
			edge.attributes[attrib.name] = BMesh.AttributeValue.Copy(attrib.defaultValue);
		}
		return attrib;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000291C File Offset: 0x00000B1C
	public BMesh.AttributeDefinition AddEdgeAttribute(string name, BMesh.AttributeBaseType baseType, int dimensions)
	{
		return this.AddEdgeAttribute(new BMesh.AttributeDefinition(name, baseType, dimensions));
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000292C File Offset: 0x00000B2C
	private void EnsureEdgeAttributes(BMesh.Edge e)
	{
		if (e.attributes == null)
		{
			e.attributes = new Dictionary<string, BMesh.AttributeValue>();
		}
		foreach (BMesh.AttributeDefinition attributeDefinition in this.edgeAttributes)
		{
			if (!e.attributes.ContainsKey(attributeDefinition.name))
			{
				e.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
			else if (!attributeDefinition.type.CheckValue(e.attributes[attributeDefinition.name]))
			{
				Debug.LogWarning("Edge attribute '" + attributeDefinition.name + "' is not compatible with mesh attribute definition, ignoring.");
				e.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002A18 File Offset: 0x00000C18
	public bool HasLoopAttribute(string attribName)
	{
		using (List<BMesh.AttributeDefinition>.Enumerator enumerator = this.loopAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == attribName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002A78 File Offset: 0x00000C78
	public bool HasLoopAttribute(BMesh.AttributeDefinition attrib)
	{
		return this.HasLoopAttribute(attrib.name);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002A88 File Offset: 0x00000C88
	public BMesh.AttributeDefinition AddLoopAttribute(BMesh.AttributeDefinition attrib)
	{
		if (this.HasLoopAttribute(attrib))
		{
			return attrib;
		}
		this.loopAttributes.Add(attrib);
		foreach (BMesh.Loop loop in this.loops)
		{
			if (loop.attributes == null)
			{
				loop.attributes = new Dictionary<string, BMesh.AttributeValue>();
			}
			loop.attributes[attrib.name] = BMesh.AttributeValue.Copy(attrib.defaultValue);
		}
		return attrib;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002B1C File Offset: 0x00000D1C
	public BMesh.AttributeDefinition AddLoopAttribute(string name, BMesh.AttributeBaseType baseType, int dimensions)
	{
		return this.AddLoopAttribute(new BMesh.AttributeDefinition(name, baseType, dimensions));
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002B2C File Offset: 0x00000D2C
	private void EnsureLoopAttributes(BMesh.Loop l)
	{
		if (l.attributes == null)
		{
			l.attributes = new Dictionary<string, BMesh.AttributeValue>();
		}
		foreach (BMesh.AttributeDefinition attributeDefinition in this.loopAttributes)
		{
			if (!l.attributes.ContainsKey(attributeDefinition.name))
			{
				l.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
			else if (!attributeDefinition.type.CheckValue(l.attributes[attributeDefinition.name]))
			{
				Debug.LogWarning("Loop attribute '" + attributeDefinition.name + "' is not compatible with mesh attribute definition, ignoring.");
				l.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002C18 File Offset: 0x00000E18
	public bool HasFaceAttribute(string attribName)
	{
		using (List<BMesh.AttributeDefinition>.Enumerator enumerator = this.faceAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == attribName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002C78 File Offset: 0x00000E78
	public bool HasFaceAttribute(BMesh.AttributeDefinition attrib)
	{
		return this.HasFaceAttribute(attrib.name);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002C88 File Offset: 0x00000E88
	public BMesh.AttributeDefinition AddFaceAttribute(BMesh.AttributeDefinition attrib)
	{
		if (this.HasFaceAttribute(attrib))
		{
			return attrib;
		}
		this.faceAttributes.Add(attrib);
		foreach (BMesh.Face face in this.faces)
		{
			if (face.attributes == null)
			{
				face.attributes = new Dictionary<string, BMesh.AttributeValue>();
			}
			face.attributes[attrib.name] = BMesh.AttributeValue.Copy(attrib.defaultValue);
		}
		return attrib;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002D1C File Offset: 0x00000F1C
	public BMesh.AttributeDefinition AddFaceAttribute(string name, BMesh.AttributeBaseType baseType, int dimensions)
	{
		return this.AddFaceAttribute(new BMesh.AttributeDefinition(name, baseType, dimensions));
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002D2C File Offset: 0x00000F2C
	private void EnsureFaceAttributes(BMesh.Face f)
	{
		if (f.attributes == null)
		{
			f.attributes = new Dictionary<string, BMesh.AttributeValue>();
		}
		foreach (BMesh.AttributeDefinition attributeDefinition in this.faceAttributes)
		{
			if (!f.attributes.ContainsKey(attributeDefinition.name))
			{
				f.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
			else if (!attributeDefinition.type.CheckValue(f.attributes[attributeDefinition.name]))
			{
				Debug.LogWarning("Face attribute '" + attributeDefinition.name + "' is not compatible with mesh attribute definition, ignoring.");
				f.attributes[attributeDefinition.name] = BMesh.AttributeValue.Copy(attributeDefinition.defaultValue);
			}
		}
	}

	// Token: 0x04000001 RID: 1
	public List<BMesh.Vertex> vertices;

	// Token: 0x04000002 RID: 2
	public List<BMesh.Edge> edges;

	// Token: 0x04000003 RID: 3
	public List<BMesh.Loop> loops;

	// Token: 0x04000004 RID: 4
	public List<BMesh.Face> faces;

	// Token: 0x04000005 RID: 5
	public List<BMesh.AttributeDefinition> vertexAttributes;

	// Token: 0x04000006 RID: 6
	public List<BMesh.AttributeDefinition> edgeAttributes;

	// Token: 0x04000007 RID: 7
	public List<BMesh.AttributeDefinition> loopAttributes;

	// Token: 0x04000008 RID: 8
	public List<BMesh.AttributeDefinition> faceAttributes;

	// Token: 0x02000005 RID: 5
	public class Vertex
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00004540 File Offset: 0x00002740
		public Vertex(Vector3 _point)
		{
			this.point = _point;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004550 File Offset: 0x00002750
		public List<BMesh.Edge> NeighborEdges()
		{
			List<BMesh.Edge> list = new List<BMesh.Edge>();
			if (this.edge != null)
			{
				BMesh.Edge edge = this.edge;
				do
				{
					list.Add(edge);
					edge = edge.Next(this);
				}
				while (edge != this.edge);
			}
			return list;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000458C File Offset: 0x0000278C
		public List<BMesh.Face> NeighborFaces()
		{
			HashSet<BMesh.Face> hashSet = new HashSet<BMesh.Face>();
			if (this.edge != null)
			{
				BMesh.Edge edge = this.edge;
				do
				{
					foreach (BMesh.Face item in edge.NeighborFaces())
					{
						hashSet.Add(item);
					}
					edge = edge.Next(this);
				}
				while (edge != this.edge);
			}
			return hashSet.ToList<BMesh.Face>();
		}

		// Token: 0x04000009 RID: 9
		public int id;

		// Token: 0x0400000A RID: 10
		public Vector3 point;

		// Token: 0x0400000B RID: 11
		public Dictionary<string, BMesh.AttributeValue> attributes;

		// Token: 0x0400000C RID: 12
		public BMesh.Edge edge;
	}

	// Token: 0x02000006 RID: 6
	public class Edge
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000460C File Offset: 0x0000280C
		public bool ContainsVertex(BMesh.Vertex v)
		{
			return v == this.vert1 || v == this.vert2;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004622 File Offset: 0x00002822
		public BMesh.Vertex OtherVertex(BMesh.Vertex v)
		{
			if (v != this.vert1)
			{
				return this.vert1;
			}
			return this.vert2;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000463A File Offset: 0x0000283A
		public BMesh.Edge Next(BMesh.Vertex v)
		{
			if (v != this.vert1)
			{
				return this.next2;
			}
			return this.next1;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004652 File Offset: 0x00002852
		public void SetNext(BMesh.Vertex v, BMesh.Edge other)
		{
			if (v == this.vert1)
			{
				this.next1 = other;
				return;
			}
			this.next2 = other;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000466C File Offset: 0x0000286C
		public BMesh.Edge Prev(BMesh.Vertex v)
		{
			if (v != this.vert1)
			{
				return this.prev2;
			}
			return this.prev1;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004684 File Offset: 0x00002884
		public void SetPrev(BMesh.Vertex v, BMesh.Edge other)
		{
			if (v == this.vert1)
			{
				this.prev1 = other;
				return;
			}
			this.prev2 = other;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000046A0 File Offset: 0x000028A0
		public List<BMesh.Face> NeighborFaces()
		{
			List<BMesh.Face> list = new List<BMesh.Face>();
			if (this.loop != null)
			{
				BMesh.Loop radial_next = this.loop;
				do
				{
					list.Add(radial_next.face);
					radial_next = radial_next.radial_next;
				}
				while (radial_next != this.loop);
			}
			return list;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000046DF File Offset: 0x000028DF
		public Vector3 Center()
		{
			return (this.vert1.point + this.vert2.point) * 0.5f;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004706 File Offset: 0x00002906
		public Edge()
		{
		}

		// Token: 0x0400000D RID: 13
		public int id;

		// Token: 0x0400000E RID: 14
		public Dictionary<string, BMesh.AttributeValue> attributes;

		// Token: 0x0400000F RID: 15
		public BMesh.Vertex vert1;

		// Token: 0x04000010 RID: 16
		public BMesh.Vertex vert2;

		// Token: 0x04000011 RID: 17
		public BMesh.Edge next1;

		// Token: 0x04000012 RID: 18
		public BMesh.Edge next2;

		// Token: 0x04000013 RID: 19
		public BMesh.Edge prev1;

		// Token: 0x04000014 RID: 20
		public BMesh.Edge prev2;

		// Token: 0x04000015 RID: 21
		public BMesh.Loop loop;
	}

	// Token: 0x02000007 RID: 7
	public class Loop
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000470E File Offset: 0x0000290E
		public Loop(BMesh.Vertex v, BMesh.Edge e, BMesh.Face f)
		{
			this.vert = v;
			this.SetEdge(e);
			this.SetFace(f);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000472C File Offset: 0x0000292C
		public void SetFace(BMesh.Face f)
		{
			if (f.loop == null)
			{
				f.loop = this;
				this.prev = this;
				this.next = this;
			}
			else
			{
				this.prev = f.loop;
				this.next = f.loop.next;
				f.loop.next.prev = this;
				f.loop.next = this;
				f.loop = this;
			}
			this.face = f;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000047A4 File Offset: 0x000029A4
		public void SetEdge(BMesh.Edge e)
		{
			if (e.loop == null)
			{
				e.loop = this;
				this.radial_prev = this;
				this.radial_next = this;
			}
			else
			{
				this.radial_prev = e.loop;
				this.radial_next = e.loop.radial_next;
				e.loop.radial_next.radial_prev = this;
				e.loop.radial_next = this;
				e.loop = this;
			}
			this.edge = e;
		}

		// Token: 0x04000016 RID: 22
		public Dictionary<string, BMesh.AttributeValue> attributes;

		// Token: 0x04000017 RID: 23
		public BMesh.Vertex vert;

		// Token: 0x04000018 RID: 24
		public BMesh.Edge edge;

		// Token: 0x04000019 RID: 25
		public BMesh.Face face;

		// Token: 0x0400001A RID: 26
		public BMesh.Loop radial_prev;

		// Token: 0x0400001B RID: 27
		public BMesh.Loop radial_next;

		// Token: 0x0400001C RID: 28
		public BMesh.Loop prev;

		// Token: 0x0400001D RID: 29
		public BMesh.Loop next;
	}

	// Token: 0x02000008 RID: 8
	public class Face
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000481C File Offset: 0x00002A1C
		public List<BMesh.Vertex> NeighborVertices()
		{
			List<BMesh.Vertex> list = new List<BMesh.Vertex>();
			if (this.loop != null)
			{
				BMesh.Loop next = this.loop;
				do
				{
					list.Add(next.vert);
					next = next.next;
				}
				while (next != this.loop);
			}
			return list;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000485C File Offset: 0x00002A5C
		public BMesh.Loop Loop(BMesh.Vertex v)
		{
			if (this.loop != null)
			{
				BMesh.Loop next = this.loop;
				while (next.vert != v)
				{
					next = next.next;
					if (next == this.loop)
					{
						goto IL_2A;
					}
				}
				return next;
			}
			IL_2A:
			return null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004894 File Offset: 0x00002A94
		public List<BMesh.Edge> NeighborEdges()
		{
			List<BMesh.Edge> list = new List<BMesh.Edge>();
			if (this.loop != null)
			{
				BMesh.Loop next = this.loop;
				do
				{
					list.Add(next.edge);
					next = next.next;
				}
				while (next != this.loop);
			}
			return list;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000048D4 File Offset: 0x00002AD4
		public Vector3 Center()
		{
			Vector3 a = Vector3.zero;
			float num = 0f;
			foreach (BMesh.Vertex vertex in this.NeighborVertices())
			{
				a += vertex.point;
				num += 1f;
			}
			return a / num;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004948 File Offset: 0x00002B48
		public Face()
		{
		}

		// Token: 0x0400001E RID: 30
		public int id;

		// Token: 0x0400001F RID: 31
		public Dictionary<string, BMesh.AttributeValue> attributes;

		// Token: 0x04000020 RID: 32
		public int vertcount;

		// Token: 0x04000021 RID: 33
		public BMesh.Loop loop;
	}

	// Token: 0x02000009 RID: 9
	public enum AttributeBaseType
	{
		// Token: 0x04000023 RID: 35
		Int,
		// Token: 0x04000024 RID: 36
		Float
	}

	// Token: 0x0200000A RID: 10
	public class AttributeType
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004950 File Offset: 0x00002B50
		public bool CheckValue(BMesh.AttributeValue value)
		{
			BMesh.AttributeBaseType attributeBaseType = this.baseType;
			if (attributeBaseType == BMesh.AttributeBaseType.Int)
			{
				BMesh.IntAttributeValue intAttributeValue = value as BMesh.IntAttributeValue;
				return intAttributeValue != null && intAttributeValue.data.Length == this.dimensions;
			}
			if (attributeBaseType != BMesh.AttributeBaseType.Float)
			{
				return false;
			}
			BMesh.FloatAttributeValue floatAttributeValue = value as BMesh.FloatAttributeValue;
			return floatAttributeValue != null && floatAttributeValue.data.Length == this.dimensions;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000049A8 File Offset: 0x00002BA8
		public AttributeType()
		{
		}

		// Token: 0x04000025 RID: 37
		public BMesh.AttributeBaseType baseType;

		// Token: 0x04000026 RID: 38
		public int dimensions;
	}

	// Token: 0x0200000B RID: 11
	public class AttributeValue
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000049B0 File Offset: 0x00002BB0
		public static BMesh.AttributeValue Copy(BMesh.AttributeValue value)
		{
			BMesh.IntAttributeValue intAttributeValue = value as BMesh.IntAttributeValue;
			if (intAttributeValue != null)
			{
				int[] array = new int[intAttributeValue.data.Length];
				intAttributeValue.data.CopyTo(array, 0);
				return new BMesh.IntAttributeValue
				{
					data = array
				};
			}
			BMesh.FloatAttributeValue floatAttributeValue = value as BMesh.FloatAttributeValue;
			if (floatAttributeValue != null)
			{
				float[] array2 = new float[floatAttributeValue.data.Length];
				floatAttributeValue.data.CopyTo(array2, 0);
				return new BMesh.FloatAttributeValue
				{
					data = array2
				};
			}
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004A24 File Offset: 0x00002C24
		public static float Distance(BMesh.AttributeValue value1, BMesh.AttributeValue value2)
		{
			BMesh.IntAttributeValue intAttributeValue = value1 as BMesh.IntAttributeValue;
			if (intAttributeValue != null)
			{
				BMesh.IntAttributeValue intAttributeValue2 = value2 as BMesh.IntAttributeValue;
				if (intAttributeValue2 != null)
				{
					return BMesh.IntAttributeValue.Distance(intAttributeValue, intAttributeValue2);
				}
			}
			BMesh.FloatAttributeValue floatAttributeValue = value1 as BMesh.FloatAttributeValue;
			if (floatAttributeValue != null)
			{
				BMesh.FloatAttributeValue floatAttributeValue2 = value2 as BMesh.FloatAttributeValue;
				if (floatAttributeValue2 != null)
				{
					return BMesh.FloatAttributeValue.Distance(floatAttributeValue, floatAttributeValue2);
				}
			}
			return float.PositiveInfinity;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004A6E File Offset: 0x00002C6E
		public BMesh.FloatAttributeValue asFloat()
		{
			return this as BMesh.FloatAttributeValue;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004A76 File Offset: 0x00002C76
		public BMesh.IntAttributeValue asInt()
		{
			return this as BMesh.IntAttributeValue;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004A7E File Offset: 0x00002C7E
		public AttributeValue()
		{
		}
	}

	// Token: 0x0200000C RID: 12
	public class IntAttributeValue : BMesh.AttributeValue
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00004A86 File Offset: 0x00002C86
		public IntAttributeValue()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004A8E File Offset: 0x00002C8E
		public IntAttributeValue(int i)
		{
			this.data = new int[]
			{
				i
			};
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public IntAttributeValue(int i0, int i1)
		{
			this.data = new int[]
			{
				i0,
				i1
			};
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public static float Distance(BMesh.IntAttributeValue value1, BMesh.IntAttributeValue value2)
		{
			int num = value1.data.Length;
			if (num != value2.data.Length)
			{
				return float.PositiveInfinity;
			}
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				float num3 = (float)(value1.data[i] - value2.data[i]);
				num2 += num3 * num3;
			}
			return Mathf.Sqrt(num2);
		}

		// Token: 0x04000027 RID: 39
		public int[] data;
	}

	// Token: 0x0200000D RID: 13
	public class FloatAttributeValue : BMesh.AttributeValue
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004B1C File Offset: 0x00002D1C
		public FloatAttributeValue()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004B24 File Offset: 0x00002D24
		public FloatAttributeValue(float f)
		{
			this.data = new float[]
			{
				f
			};
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004B3C File Offset: 0x00002D3C
		public FloatAttributeValue(float f0, float f1)
		{
			this.data = new float[]
			{
				f0,
				f1
			};
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004B58 File Offset: 0x00002D58
		public FloatAttributeValue(Vector3 v)
		{
			this.data = new float[]
			{
				v.x,
				v.y,
				v.z
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004B87 File Offset: 0x00002D87
		public void FromVector2(Vector2 v)
		{
			this.data[0] = v.x;
			this.data[1] = v.y;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004BA5 File Offset: 0x00002DA5
		public void FromVector3(Vector3 v)
		{
			this.data[0] = v.x;
			this.data[1] = v.y;
			this.data[2] = v.z;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004BD1 File Offset: 0x00002DD1
		public void FromColor(Color c)
		{
			this.data[0] = c.r;
			this.data[1] = c.g;
			this.data[2] = c.b;
			this.data[3] = c.a;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004C0C File Offset: 0x00002E0C
		public Vector3 AsVector3()
		{
			return new Vector3((this.data.Length != 0) ? this.data[0] : 0f, (this.data.Length > 1) ? this.data[1] : 0f, (this.data.Length > 2) ? this.data[2] : 0f);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004C6C File Offset: 0x00002E6C
		public Color AsColor()
		{
			return new Color((this.data.Length != 0) ? this.data[0] : 0f, (this.data.Length > 1) ? this.data[1] : 0f, (this.data.Length > 2) ? this.data[2] : 0f, (this.data.Length > 3) ? this.data[3] : 1f);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public static float Distance(BMesh.FloatAttributeValue value1, BMesh.FloatAttributeValue value2)
		{
			int num = value1.data.Length;
			if (num != value2.data.Length)
			{
				return float.PositiveInfinity;
			}
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				float num3 = value1.data[i] - value2.data[i];
				num2 += num3 * num3;
			}
			return Mathf.Sqrt(num2);
		}

		// Token: 0x04000028 RID: 40
		public float[] data;
	}

	// Token: 0x0200000E RID: 14
	public class AttributeDefinition
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00004D3B File Offset: 0x00002F3B
		public AttributeDefinition(string name, BMesh.AttributeBaseType baseType, int dimensions)
		{
			this.name = name;
			this.type = new BMesh.AttributeType
			{
				baseType = baseType,
				dimensions = dimensions
			};
			this.defaultValue = this.NullValue();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004D70 File Offset: 0x00002F70
		public BMesh.AttributeValue NullValue()
		{
			BMesh.AttributeBaseType baseType = this.type.baseType;
			if (baseType == BMesh.AttributeBaseType.Int)
			{
				return new BMesh.IntAttributeValue
				{
					data = new int[this.type.dimensions]
				};
			}
			if (baseType != BMesh.AttributeBaseType.Float)
			{
				return new BMesh.AttributeValue();
			}
			return new BMesh.FloatAttributeValue
			{
				data = new float[this.type.dimensions]
			};
		}

		// Token: 0x04000029 RID: 41
		public string name;

		// Token: 0x0400002A RID: 42
		public BMesh.AttributeType type;

		// Token: 0x0400002B RID: 43
		public BMesh.AttributeValue defaultValue;
	}
}
