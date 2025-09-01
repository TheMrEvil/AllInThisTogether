using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class BMeshUnity
{
	// Token: 0x0600002F RID: 47 RVA: 0x00003B98 File Offset: 0x00001D98
	public static void SetInMeshFilter(BMesh mesh, MeshFilter mf)
	{
		Vector2[] array = null;
		Vector2[] array2 = null;
		Vector3[] array3 = null;
		Color[] array4 = null;
		Vector3[] array5 = new Vector3[mesh.vertices.Count];
		if (mesh.HasVertexAttribute("uv"))
		{
			array = new Vector2[mesh.vertices.Count];
		}
		if (mesh.HasVertexAttribute("uv2"))
		{
			array2 = new Vector2[mesh.vertices.Count];
		}
		if (mesh.HasVertexAttribute("normal"))
		{
			array3 = new Vector3[mesh.vertices.Count];
		}
		if (mesh.HasVertexAttribute("color"))
		{
			array4 = new Color[mesh.vertices.Count];
		}
		int num = 0;
		foreach (BMesh.Vertex vertex in mesh.vertices)
		{
			vertex.id = num;
			array5[num] = vertex.point;
			if (array != null)
			{
				BMesh.FloatAttributeValue floatAttributeValue = vertex.attributes["uv"] as BMesh.FloatAttributeValue;
				array[num] = new Vector2(floatAttributeValue.data[0], floatAttributeValue.data[1]);
			}
			if (array2 != null)
			{
				BMesh.FloatAttributeValue floatAttributeValue2 = vertex.attributes["uv2"] as BMesh.FloatAttributeValue;
				array2[num] = new Vector2(floatAttributeValue2.data[0], floatAttributeValue2.data[1]);
			}
			if (array3 != null)
			{
				BMesh.FloatAttributeValue floatAttributeValue3 = vertex.attributes["normal"] as BMesh.FloatAttributeValue;
				array3[num] = floatAttributeValue3.AsVector3();
			}
			if (array4 != null)
			{
				BMesh.FloatAttributeValue floatAttributeValue4 = vertex.attributes["color"] as BMesh.FloatAttributeValue;
				array4[num] = floatAttributeValue4.AsColor();
			}
			num++;
		}
		int num2 = 0;
		bool flag = mesh.HasFaceAttribute("materialId");
		if (flag)
		{
			foreach (BMesh.Face face in mesh.faces)
			{
				num2 = Mathf.Max(num2, face.attributes["materialId"].asInt().data[0]);
			}
		}
		int[] array6 = new int[num2 + 1];
		foreach (BMesh.Face face2 in mesh.faces)
		{
			int num3 = flag ? face2.attributes["materialId"].asInt().data[0] : 0;
			array6[num3] += face2.vertcount - 2;
		}
		int[][] array7 = new int[num2 + 1][];
		for (int i = 0; i < array7.Length; i++)
		{
			array7[i] = new int[3 * array6[i]];
			array6[i] = 0;
		}
		foreach (BMesh.Face face3 in mesh.faces)
		{
			int num4 = flag ? face3.attributes["materialId"].asInt().data[0] : 0;
			BMesh.Loop loop = face3.loop;
			array7[num4][3 * array6[num4]] = loop.vert.id;
			loop = loop.next;
			array7[num4][3 * array6[num4] + 2] = loop.vert.id;
			loop = loop.next;
			array7[num4][3 * array6[num4] + 1] = loop.vert.id;
			loop = loop.next;
			array6[num4]++;
			if (face3.vertcount == 4)
			{
				BMesh.Loop next = face3.loop.next.next;
				array7[num4][3 * array6[num4]] = next.vert.id;
				next = next.next;
				array7[num4][3 * array6[num4] + 2] = next.vert.id;
				next = next.next;
				array7[num4][3 * array6[num4] + 1] = next.vert.id;
				next = next.next;
				array6[num4]++;
			}
		}
		Mesh mesh2 = new Mesh();
		mf.mesh = mesh2;
		mesh2.vertices = array5;
		if (array != null)
		{
			mesh2.uv = array;
		}
		if (array2 != null)
		{
			mesh2.uv2 = array2;
		}
		if (array3 != null)
		{
			mesh2.normals = array3;
		}
		if (array4 != null)
		{
			mesh2.colors = array4;
		}
		mesh2.subMeshCount = array7.Length;
		MeshRenderer component = mf.GetComponent<MeshRenderer>();
		if (component)
		{
			mesh2.subMeshCount = Mathf.Max(mesh2.subMeshCount, component.sharedMaterials.Length);
		}
		for (int j = 0; j < array7.Length; j++)
		{
			mesh2.SetTriangles(array7[j], j);
		}
		if (array3 == null)
		{
			mesh2.RecalculateNormals();
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000040F4 File Offset: 0x000022F4
	public static void Merge(BMesh mesh, Mesh unityMesh, bool flipFaces = false)
	{
		Vector3[] vertices = unityMesh.vertices;
		Vector2[] uv = unityMesh.uv;
		Vector2[] uv2 = unityMesh.uv2;
		Vector3[] normals = unityMesh.normals;
		Color[] colors = unityMesh.colors;
		bool flag = uv != null && uv.Length != 0;
		bool flag2 = uv2 != null && uv2.Length != 0;
		bool flag3 = normals != null && normals.Length != 0;
		bool flag4 = colors != null && colors.Length != 0;
		int[] triangles = unityMesh.triangles;
		BMesh.Vertex[] array = new BMesh.Vertex[vertices.Length];
		if (flag)
		{
			mesh.AddVertexAttribute(new BMesh.AttributeDefinition("uv", BMesh.AttributeBaseType.Float, 2));
		}
		if (flag2)
		{
			mesh.AddVertexAttribute(new BMesh.AttributeDefinition("uv2", BMesh.AttributeBaseType.Float, 2));
		}
		if (flag3)
		{
			mesh.AddVertexAttribute(new BMesh.AttributeDefinition("normal", BMesh.AttributeBaseType.Float, 3));
		}
		if (flag4)
		{
			mesh.AddVertexAttribute(new BMesh.AttributeDefinition("color", BMesh.AttributeBaseType.Float, 4));
		}
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 point = vertices[i];
			array[i] = mesh.AddVertex(point);
			if (flag)
			{
				array[i].attributes["uv"].asFloat().FromVector2(uv[i]);
			}
			if (flag2)
			{
				array[i].attributes["uv2"].asFloat().FromVector2(uv2[i]);
			}
			if (flag3)
			{
				array[i].attributes["normal"].asFloat().FromVector3(normals[i]);
			}
			if (flag4)
			{
				array[i].attributes["color"].asFloat().FromColor(colors[i]);
			}
		}
		for (int j = 0; j < triangles.Length / 3; j++)
		{
			mesh.AddFace(array[triangles[3 * j + (flipFaces ? 1 : 0)]], array[triangles[3 * j + (flipFaces ? 0 : 1)]], array[triangles[3 * j + 2]]);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000042F0 File Offset: 0x000024F0
	public static void DrawGizmos(BMesh mesh)
	{
		Gizmos.color = Color.yellow;
		foreach (BMesh.Edge edge in mesh.edges)
		{
			Gizmos.DrawLine(edge.vert1.point, edge.vert2.point);
		}
		Gizmos.color = Color.red;
		foreach (BMesh.Loop loop in mesh.loops)
		{
			BMesh.Vertex vert = loop.vert;
			BMesh.Vertex vertex = loop.edge.OtherVertex(vert);
			Gizmos.DrawRay(vert.point, (vertex.point - vert.point) * 0.1f);
			BMesh.Loop next = loop.next;
			BMesh.Vertex vertex2 = next.edge.ContainsVertex(vert) ? next.edge.OtherVertex(vert) : next.edge.OtherVertex(vertex);
			Vector3 vector = vert.point + (vertex.point - vert.point) * 0.1f;
			Gizmos.DrawRay(vector, (vertex2.point - vector) * 0.1f);
		}
		Gizmos.color = Color.green;
		foreach (BMesh.Face face in mesh.faces)
		{
			Vector3 vector2 = face.Center();
			Gizmos.DrawLine(vector2, face.loop.vert.point);
			Gizmos.DrawRay(vector2, (face.loop.next.vert.point - vector2) * 0.2f);
		}
		foreach (BMesh.Vertex vertex3 in mesh.vertices)
		{
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00004538 File Offset: 0x00002738
	public BMeshUnity()
	{
	}
}
