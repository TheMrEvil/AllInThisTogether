using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class BMeshOperators
{
	// Token: 0x06000027 RID: 39 RVA: 0x00002E18 File Offset: 0x00001018
	public static void AttributeLerp(BMesh mesh, BMesh.Vertex destination, BMesh.Vertex v1, BMesh.Vertex v2, float t)
	{
		foreach (BMesh.AttributeDefinition attributeDefinition in mesh.vertexAttributes)
		{
			if (v1.attributes.ContainsKey(attributeDefinition.name) && v2.attributes.ContainsKey(attributeDefinition.name))
			{
				BMesh.AttributeBaseType baseType = attributeDefinition.type.baseType;
				if (baseType != BMesh.AttributeBaseType.Int)
				{
					if (baseType == BMesh.AttributeBaseType.Float)
					{
						BMesh.FloatAttributeValue floatAttributeValue = v1.attributes[attributeDefinition.name] as BMesh.FloatAttributeValue;
						BMesh.FloatAttributeValue floatAttributeValue2 = v2.attributes[attributeDefinition.name] as BMesh.FloatAttributeValue;
						int num = floatAttributeValue.data.Length;
						BMesh.FloatAttributeValue floatAttributeValue3 = new BMesh.FloatAttributeValue
						{
							data = new float[num]
						};
						for (int i = 0; i < num; i++)
						{
							floatAttributeValue3.data[i] = Mathf.Lerp(floatAttributeValue.data[i], floatAttributeValue2.data[i], t);
						}
						destination.attributes[attributeDefinition.name] = floatAttributeValue3;
					}
				}
				else
				{
					BMesh.IntAttributeValue intAttributeValue = v1.attributes[attributeDefinition.name] as BMesh.IntAttributeValue;
					BMesh.IntAttributeValue intAttributeValue2 = v2.attributes[attributeDefinition.name] as BMesh.IntAttributeValue;
					int num2 = intAttributeValue.data.Length;
					BMesh.IntAttributeValue intAttributeValue3 = new BMesh.IntAttributeValue
					{
						data = new int[num2]
					};
					for (int j = 0; j < num2; j++)
					{
						intAttributeValue3.data[j] = (int)Mathf.Round(Mathf.Lerp((float)intAttributeValue.data[j], (float)intAttributeValue2.data[j], t));
					}
					destination.attributes[attributeDefinition.name] = intAttributeValue3;
				}
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002FF8 File Offset: 0x000011F8
	public static void Subdivide(BMesh mesh)
	{
		int num = 0;
		BMesh.Vertex[] array = new BMesh.Vertex[mesh.edges.Count];
		BMesh.Edge[] array2 = new BMesh.Edge[mesh.edges.Count];
		foreach (BMesh.Edge edge in mesh.edges)
		{
			array[num] = mesh.AddVertex(edge.Center());
			BMeshOperators.AttributeLerp(mesh, array[num], edge.vert1, edge.vert2, 0.5f);
			array2[num] = edge;
			edge.id = num++;
		}
		foreach (BMesh.Face face in new List<BMesh.Face>(mesh.faces))
		{
			BMesh.Vertex vertex = mesh.AddVertex(face.Center());
			float num2 = 0f;
			BMesh.Loop loop = face.loop;
			do
			{
				num2 += 1f;
				BMeshOperators.AttributeLerp(mesh, vertex, vertex, loop.vert, 1f / num2);
				BMesh.Vertex[] fVerts = new BMesh.Vertex[]
				{
					loop.vert,
					array[loop.edge.id],
					vertex,
					array[loop.prev.edge.id]
				};
				mesh.AddFace(fVerts);
				loop = loop.next;
			}
			while (loop != face.loop);
			mesh.RemoveFace(face);
		}
		foreach (BMesh.Edge e in array2)
		{
			mesh.RemoveEdge(e);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000031B8 File Offset: 0x000013B8
	private static Matrix4x4 ComputeLocalAxis(Vector3 r0, Vector3 r1, Vector3 r2, Vector3 r3)
	{
		Vector3 normalized = (Vector3.Cross(r0, r1).normalized + Vector3.Cross(r1, r2).normalized + Vector3.Cross(r2, r3).normalized + Vector3.Cross(r3, r0).normalized).normalized;
		Vector3 normalized2 = r0.normalized;
		Vector3 v = Vector3.Cross(normalized, normalized2);
		return new Matrix4x4(normalized2, v, normalized, Vector4.zero);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003248 File Offset: 0x00001448
	private static float AverageRadiusLength(BMesh mesh)
	{
		float num = 0f;
		float num2 = 0f;
		foreach (BMesh.Face face in mesh.faces)
		{
			Vector3 b = face.Center();
			List<BMesh.Vertex> list = face.NeighborVertices();
			if (list.Count == 4)
			{
				Vector3 vector = list[0].point - b;
				Vector3 vector2 = list[1].point - b;
				Vector3 vector3 = list[2].point - b;
				Vector3 vector4 = list[3].point - b;
				Matrix4x4 transpose = BMeshOperators.ComputeLocalAxis(vector, vector2, vector3, vector4).transpose;
				Vector3 vector5 = transpose * vector;
				Vector3 vector6 = transpose * vector2;
				Vector3 vector7 = transpose * vector3;
				Vector3 vector8 = transpose * vector4;
				Vector3 a = vector5;
				Vector3 b2 = new Vector3(vector6.y, -vector6.x, vector6.z);
				Vector3 b3 = new Vector3(-vector7.x, -vector7.y, vector7.z);
				Vector3 b4 = new Vector3(-vector8.y, vector8.x, vector8.z);
				num += ((a + b2 + b3 + b4) / 4f).magnitude;
				num2 += 1f;
			}
		}
		return num / num2;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003418 File Offset: 0x00001618
	public static void SquarifyQuads(BMesh mesh, float rate = 1f, bool uniformLength = false)
	{
		float d = 0f;
		if (uniformLength)
		{
			d = BMeshOperators.AverageRadiusLength(mesh);
		}
		Vector3[] array = new Vector3[mesh.vertices.Count];
		float[] array2 = new float[mesh.vertices.Count];
		int num = 0;
		foreach (BMesh.Vertex vertex in mesh.vertices)
		{
			if (mesh.HasVertexAttribute("restpos"))
			{
				if (mesh.HasVertexAttribute("weight"))
				{
					array2[num] = (vertex.attributes["weight"] as BMesh.FloatAttributeValue).data[0];
				}
				else
				{
					array2[num] = 1f;
				}
				Vector3 a = (vertex.attributes["restpos"] as BMesh.FloatAttributeValue).AsVector3();
				array[num] = (a - vertex.point) * array2[num];
			}
			else
			{
				array[num] = Vector3.zero;
				array2[num] = 0f;
			}
			vertex.id = num++;
		}
		foreach (BMesh.Face face in mesh.faces)
		{
			Vector3 b = face.Center();
			List<BMesh.Vertex> list = face.NeighborVertices();
			if (list.Count == 4)
			{
				Vector3 vector = list[0].point - b;
				Vector3 vector2 = list[1].point - b;
				Vector3 vector3 = list[2].point - b;
				Vector3 vector4 = list[3].point - b;
				Matrix4x4 lhs = BMeshOperators.ComputeLocalAxis(vector, vector2, vector3, vector4);
				Matrix4x4 transpose = lhs.transpose;
				Vector3 vector5 = transpose * vector;
				Vector3 vector6 = transpose * vector2;
				Vector3 vector7 = transpose * vector3;
				Vector3 vector8 = transpose * vector4;
				bool flag = false;
				if (vector6.normalized.y < vector8.normalized.y)
				{
					flag = true;
					Vector3 vector9 = vector8;
					vector8 = vector6;
					vector6 = vector9;
				}
				Vector3 a2 = vector5;
				Vector3 b2 = new Vector3(vector6.y, -vector6.x, vector6.z);
				Vector3 b3 = new Vector3(-vector7.x, -vector7.y, vector7.z);
				Vector3 b4 = new Vector3(-vector8.y, vector8.x, vector8.z);
				Vector3 vector10 = (a2 + b2 + b3 + b4) / 4f;
				if (uniformLength)
				{
					vector10 = vector10.normalized * d;
				}
				Vector3 v = vector10;
				Vector3 vector11 = new Vector3(-vector10.y, vector10.x, vector10.z);
				Vector3 v2 = new Vector3(-vector10.x, -vector10.y, vector10.z);
				Vector3 vector12 = new Vector3(vector10.y, -vector10.x, vector10.z);
				if (flag)
				{
					Vector3 vector13 = vector12;
					vector12 = vector11;
					vector11 = vector13;
				}
				Vector3 a3 = lhs * v;
				Vector3 a4 = lhs * vector11;
				Vector3 a5 = lhs * v2;
				Vector3 a6 = lhs * vector12;
				array[list[0].id] += a3 - vector;
				array[list[1].id] += a4 - vector2;
				array[list[2].id] += a5 - vector3;
				array[list[3].id] += a6 - vector4;
				array2[list[0].id] += 1f;
				array2[list[1].id] += 1f;
				array2[list[2].id] += 1f;
				array2[list[3].id] += 1f;
			}
		}
		num = 0;
		foreach (BMesh.Vertex vertex2 in mesh.vertices)
		{
			if (array2[num] > 0f)
			{
				vertex2.point += array[num] * (rate / array2[num]);
			}
			num++;
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003980 File Offset: 0x00001B80
	public static void Merge(BMesh mesh, BMesh other)
	{
		BMesh.Vertex[] array = new BMesh.Vertex[other.vertices.Count];
		int num = 0;
		foreach (BMesh.Vertex vertex in other.vertices)
		{
			array[num] = mesh.AddVertex(vertex.point);
			BMeshOperators.AttributeLerp(mesh, array[num], vertex, vertex, 1f);
			vertex.id = num;
			num++;
		}
		foreach (BMesh.Edge edge in other.edges)
		{
			mesh.AddEdge(array[edge.vert1.id], array[edge.vert2.id]);
		}
		foreach (BMesh.Face face in other.faces)
		{
			List<BMesh.Vertex> list = face.NeighborVertices();
			BMesh.Vertex[] array2 = new BMesh.Vertex[list.Count];
			int num2 = 0;
			foreach (BMesh.Vertex vertex2 in list)
			{
				array2[num2] = array[vertex2.id];
				num2++;
			}
			mesh.AddFace(array2);
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003B0C File Offset: 0x00001D0C
	public static BMesh.Vertex Nearpoint(BMesh mesh, BMesh.AttributeValue value, string attrName)
	{
		if (!mesh.HasVertexAttribute(attrName))
		{
			return null;
		}
		BMesh.Vertex vertex = null;
		float num = 0f;
		foreach (BMesh.Vertex vertex2 in mesh.vertices)
		{
			float num2 = BMesh.AttributeValue.Distance(vertex2.attributes[attrName], value);
			if (vertex == null || num2 < num)
			{
				vertex = vertex2;
				num = num2;
			}
		}
		return vertex;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003B90 File Offset: 0x00001D90
	public BMeshOperators()
	{
	}
}
