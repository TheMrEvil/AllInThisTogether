using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200007F RID: 127
	public static class ExtrudeElements
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000301D7 File Offset: 0x0002E3D7
		public static Face[] Extrude(this ProBuilderMesh mesh, IEnumerable<Face> faces, ExtrudeMethod method, float distance)
		{
			if (method == ExtrudeMethod.IndividualFaces)
			{
				return ExtrudeElements.ExtrudePerFace(mesh, faces, distance);
			}
			return ExtrudeElements.ExtrudeAsGroups(mesh, faces, method == ExtrudeMethod.FaceNormal, distance);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000301F4 File Offset: 0x0002E3F4
		public static Edge[] Extrude(this ProBuilderMesh mesh, IEnumerable<Edge> edges, float distance, bool extrudeAsGroup, bool enableManifoldExtrude)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (edges == null)
			{
				throw new ArgumentNullException("edges");
			}
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			List<Edge> list = new List<Edge>();
			List<Face> list2 = new List<Face>();
			foreach (Edge edge in edges)
			{
				int num = 0;
				Face item = null;
				foreach (Face face in mesh.facesInternal)
				{
					if (mesh.IndexOf(face.edgesInternal, edge) > -1)
					{
						item = face;
						if (++num > 1)
						{
							break;
						}
					}
				}
				if (enableManifoldExtrude || num < 2)
				{
					list.Add(edge);
					list2.Add(item);
				}
			}
			if (list.Count < 1)
			{
				return null;
			}
			Vector3[] positionsInternal = mesh.positionsInternal;
			if (!mesh.HasArrays(MeshArrays.Normal))
			{
				mesh.Refresh(RefreshMask.Normals);
			}
			IList<Vector3> normals = mesh.normals;
			int[] array = new int[list.Count * 2];
			int num2 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				array[num2++] = list[j].a;
				array[num2++] = list[j].b;
			}
			List<Edge> list3 = new List<Edge>();
			List<Edge> list4 = new List<Edge>();
			bool flag = mesh.HasArrays(MeshArrays.Color);
			for (int k = 0; k < list.Count; k++)
			{
				Edge edge2 = list[k];
				Face face2 = list2[k];
				Vector3 vector = extrudeAsGroup ? InternalMeshUtility.AverageNormalWithIndexes(sharedVerticesInternal[mesh.GetSharedVertexHandle(edge2.a)], array, normals) : Math.Normal(mesh, face2);
				Vector3 vector2 = extrudeAsGroup ? InternalMeshUtility.AverageNormalWithIndexes(sharedVerticesInternal[mesh.GetSharedVertexHandle(edge2.b)], array, normals) : Math.Normal(mesh, face2);
				int sharedVertexHandle = mesh.GetSharedVertexHandle(edge2.a);
				int sharedVertexHandle2 = mesh.GetSharedVertexHandle(edge2.b);
				Vector3[] positions = new Vector3[]
				{
					positionsInternal[edge2.a],
					positionsInternal[edge2.b],
					positionsInternal[edge2.a] + vector.normalized * distance,
					positionsInternal[edge2.b] + vector2.normalized * distance
				};
				Color[] array2;
				if (!flag)
				{
					array2 = null;
				}
				else
				{
					Color[] array3 = new Color[4];
					array3[0] = mesh.colorsInternal[edge2.a];
					array3[1] = mesh.colorsInternal[edge2.b];
					array3[2] = mesh.colorsInternal[edge2.a];
					array2 = array3;
					array3[3] = mesh.colorsInternal[edge2.b];
				}
				Color[] colors = array2;
				Face face3 = mesh.AppendFace(positions, colors, new Vector2[4], new Vector4[4], new Vector4[4], new Face(new int[]
				{
					2,
					1,
					0,
					2,
					3,
					1
				}, face2.submeshIndex, AutoUnwrapSettings.tile, 0, -1, -1, false), new int[]
				{
					sharedVertexHandle,
					sharedVertexHandle2,
					-1,
					-1
				});
				list4.Add(new Edge(face3.indexesInternal[3], face3.indexesInternal[4]));
				list3.Add(new Edge(sharedVertexHandle, face3.indexesInternal[3]));
				list3.Add(new Edge(sharedVertexHandle2, face3.indexesInternal[4]));
			}
			if (extrudeAsGroup)
			{
				for (int l = 0; l < list3.Count; l++)
				{
					int a = list3[l].a;
					for (int m = 0; m < list3.Count; m++)
					{
						if (m != l && list3[m].a == a)
						{
							mesh.SetVerticesCoincident(new int[]
							{
								list3[m].b,
								list3[l].b
							});
							break;
						}
					}
				}
			}
			Face[] facesInternal = mesh.facesInternal;
			for (int i = 0; i < facesInternal.Length; i++)
			{
				facesInternal[i].InvalidateCache();
			}
			return list4.ToArray();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00030654 File Offset: 0x0002E854
		public static List<Face> DetachFaces(this ProBuilderMesh mesh, IEnumerable<Face> faces)
		{
			return mesh.DetachFaces(faces, true);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00030660 File Offset: 0x0002E860
		public static List<Face> DetachFaces(this ProBuilderMesh mesh, IEnumerable<Face> faces, bool deleteSourceFaces)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			int num = mesh.sharedVerticesInternal.Length;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			List<FaceRebuildData> list2 = new List<FaceRebuildData>();
			foreach (Face face in faces)
			{
				FaceRebuildData faceRebuildData = new FaceRebuildData();
				faceRebuildData.vertices = new List<Vertex>();
				faceRebuildData.sharedIndexes = new List<int>();
				faceRebuildData.face = new Face(face);
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int[] array = new int[face.indexesInternal.Length];
				for (int i = 0; i < face.indexesInternal.Length; i++)
				{
					int count;
					if (dictionary.TryGetValue(face.indexesInternal[i], out count))
					{
						array[i] = count;
					}
					else
					{
						count = faceRebuildData.vertices.Count;
						array[i] = count;
						dictionary.Add(face.indexesInternal[i], count);
						faceRebuildData.vertices.Add(list[face.indexesInternal[i]]);
						faceRebuildData.sharedIndexes.Add(sharedVertexLookup[face.indexesInternal[i]] + num);
					}
				}
				faceRebuildData.face.indexesInternal = array.ToArray<int>();
				list2.Add(faceRebuildData);
			}
			FaceRebuildData.Apply(list2, mesh, list, null);
			if (deleteSourceFaces)
			{
				mesh.DeleteFaces(faces);
			}
			mesh.ToMesh(MeshTopology.Triangles);
			return (from x in list2
			select x.face).ToList<Face>();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00030840 File Offset: 0x0002EA40
		private static Face[] ExtrudePerFace(ProBuilderMesh pb, IEnumerable<Face> faces, float distance)
		{
			Face[] array = (faces as Face[]) ?? faces.ToArray<Face>();
			if (!array.Any<Face>())
			{
				return null;
			}
			List<Vertex> list = new List<Vertex>(pb.GetVertices(null));
			int num = pb.sharedVerticesInternal.Length;
			int num2 = 0;
			int num3 = 0;
			Dictionary<int, int> sharedVertexLookup = pb.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = pb.sharedTextureLookup;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Face[] array2 = new Face[array.Sum((Face x) => x.edges.Count)];
			foreach (Face face in array)
			{
				face.smoothingGroup = 0;
				face.textureGroup = -1;
				Vector3 b = Math.Normal(pb, face) * distance;
				Edge[] edgesInternal = face.edgesInternal;
				dictionary.Clear();
				for (int j = 0; j < edgesInternal.Length; j++)
				{
					int count = list.Count;
					int a = edgesInternal[j].a;
					int b2 = edgesInternal[j].b;
					if (!dictionary.ContainsKey(a))
					{
						dictionary.Add(a, sharedVertexLookup[a]);
						sharedVertexLookup[a] = num + num2++;
					}
					if (!dictionary.ContainsKey(b2))
					{
						dictionary.Add(b2, sharedVertexLookup[b2]);
						sharedVertexLookup[b2] = num + num2++;
					}
					sharedVertexLookup.Add(count, dictionary[a]);
					sharedVertexLookup.Add(count + 1, dictionary[b2]);
					sharedVertexLookup.Add(count + 2, sharedVertexLookup[a]);
					sharedVertexLookup.Add(count + 3, sharedVertexLookup[b2]);
					Vertex vertex = new Vertex(list[a]);
					Vertex vertex2 = new Vertex(list[b2]);
					vertex.position += b;
					vertex2.position += b;
					list.Add(new Vertex(list[a]));
					list.Add(new Vertex(list[b2]));
					list.Add(vertex);
					list.Add(vertex2);
					Face face2 = new Face(new int[]
					{
						count,
						count + 1,
						count + 2,
						count + 1,
						count + 3,
						count + 2
					}, face.submeshIndex, new AutoUnwrapSettings(face.uv), face.smoothingGroup, -1, -1, false);
					array2[num3++] = face2;
				}
				for (int k = 0; k < face.distinctIndexesInternal.Length; k++)
				{
					list[face.distinctIndexesInternal[k]].position += b;
					if (sharedTextureLookup != null && sharedTextureLookup.ContainsKey(face.distinctIndexesInternal[k]))
					{
						sharedTextureLookup.Remove(face.distinctIndexesInternal[k]);
					}
				}
			}
			pb.SetVertices(list, false);
			int faceCount = pb.faceCount;
			int num4 = array2.Length;
			Face[] array4 = new Face[faceCount + num4];
			Array.Copy(pb.facesInternal, 0, array4, 0, faceCount);
			Array.Copy(array2, 0, array4, faceCount, num4);
			pb.faces = array4;
			pb.SetSharedVertices(sharedVertexLookup);
			pb.SetSharedTextures(sharedTextureLookup);
			return array2;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00030B9C File Offset: 0x0002ED9C
		private static Face[] ExtrudeAsGroups(ProBuilderMesh mesh, IEnumerable<Face> faces, bool compensateAngleVertexDistance, float distance)
		{
			if (faces == null || !faces.Any<Face>())
			{
				return null;
			}
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			int num = mesh.sharedVerticesInternal.Length;
			int num2 = 0;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = mesh.sharedTextureLookup;
			List<Face> list2 = new List<Face>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
			Dictionary<int, SimpleTuple<Vector3, Vector3, List<int>>> dictionary4 = new Dictionary<int, SimpleTuple<Vector3, Vector3, List<int>>>();
			foreach (HashSet<Face> hashSet in ExtrudeElements.GetFaceGroups(WingedEdge.GetWingedEdges(mesh, faces, true)))
			{
				Dictionary<EdgeLookup, Face> perimeterEdges = ExtrudeElements.GetPerimeterEdges(hashSet, sharedVertexLookup);
				dictionary2.Clear();
				dictionary.Clear();
				foreach (KeyValuePair<EdgeLookup, Face> keyValuePair in perimeterEdges)
				{
					EdgeLookup key = keyValuePair.Key;
					Face value = keyValuePair.Value;
					int count = list.Count;
					int a = key.local.a;
					int b = key.local.b;
					if (!dictionary.ContainsKey(a))
					{
						dictionary.Add(a, sharedVertexLookup[a]);
						int value2 = -1;
						if (dictionary2.TryGetValue(sharedVertexLookup[a], out value2))
						{
							sharedVertexLookup[a] = value2;
						}
						else
						{
							value2 = num + num2++;
							dictionary2.Add(sharedVertexLookup[a], value2);
							sharedVertexLookup[a] = value2;
						}
					}
					if (!dictionary.ContainsKey(b))
					{
						dictionary.Add(b, sharedVertexLookup[b]);
						int value3 = -1;
						if (dictionary2.TryGetValue(sharedVertexLookup[b], out value3))
						{
							sharedVertexLookup[b] = value3;
						}
						else
						{
							value3 = num + num2++;
							dictionary2.Add(sharedVertexLookup[b], value3);
							sharedVertexLookup[b] = value3;
						}
					}
					sharedVertexLookup.Add(count, dictionary[a]);
					sharedVertexLookup.Add(count + 1, dictionary[b]);
					sharedVertexLookup.Add(count + 2, sharedVertexLookup[a]);
					sharedVertexLookup.Add(count + 3, sharedVertexLookup[b]);
					dictionary3.Add(count + 2, a);
					dictionary3.Add(count + 3, b);
					list.Add(new Vertex(list[a]));
					list.Add(new Vertex(list[b]));
					list.Add(null);
					list.Add(null);
					Face item = new Face(new int[]
					{
						count,
						count + 1,
						count + 2,
						count + 1,
						count + 3,
						count + 2
					}, value.submeshIndex, new AutoUnwrapSettings(value.uv), 0, -1, -1, false);
					list2.Add(item);
				}
				foreach (Face face in hashSet)
				{
					face.textureGroup = -1;
					Vector3 vector = Math.Normal(mesh, face);
					for (int i = 0; i < face.distinctIndexesInternal.Length; i++)
					{
						int num3 = face.distinctIndexesInternal[i];
						if (!dictionary.ContainsKey(num3) && dictionary2.ContainsKey(sharedVertexLookup[num3]))
						{
							sharedVertexLookup[num3] = dictionary2[sharedVertexLookup[num3]];
						}
						int key2 = sharedVertexLookup[num3];
						if (sharedTextureLookup != null && sharedTextureLookup.ContainsKey(face.distinctIndexesInternal[i]))
						{
							sharedTextureLookup.Remove(face.distinctIndexesInternal[i]);
						}
						SimpleTuple<Vector3, Vector3, List<int>> value4;
						if (dictionary4.TryGetValue(key2, out value4))
						{
							value4.item1 += vector;
							value4.item3.Add(num3);
							dictionary4[key2] = value4;
						}
						else
						{
							dictionary4.Add(key2, new SimpleTuple<Vector3, Vector3, List<int>>(vector, vector, new List<int>
							{
								num3
							}));
						}
					}
				}
			}
			foreach (KeyValuePair<int, SimpleTuple<Vector3, Vector3, List<int>>> keyValuePair2 in dictionary4)
			{
				Vector3 vector2 = keyValuePair2.Value.item1 / (float)keyValuePair2.Value.item3.Count;
				vector2.Normalize();
				float num4 = compensateAngleVertexDistance ? Math.Secant(Vector3.Angle(vector2, keyValuePair2.Value.item2) * 0.017453292f) : 1f;
				vector2.x *= distance * num4;
				vector2.y *= distance * num4;
				vector2.z *= distance * num4;
				foreach (int index in keyValuePair2.Value.item3)
				{
					list[index].position += vector2;
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair3 in dictionary3)
			{
				list[keyValuePair3.Key] = new Vertex(list[keyValuePair3.Value]);
			}
			mesh.SetVertices(list, false);
			int faceCount = mesh.faceCount;
			int count2 = list2.Count;
			Face[] array = new Face[faceCount + count2];
			Array.Copy(mesh.facesInternal, 0, array, 0, faceCount);
			int j = faceCount;
			int num5 = faceCount + count2;
			while (j < num5)
			{
				array[j] = list2[j - faceCount];
				j++;
			}
			mesh.faces = array;
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(sharedTextureLookup);
			return list2.ToArray();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00031210 File Offset: 0x0002F410
		private static List<HashSet<Face>> GetFaceGroups(List<WingedEdge> wings)
		{
			HashSet<Face> hashSet = new HashSet<Face>();
			List<HashSet<Face>> list = new List<HashSet<Face>>();
			foreach (WingedEdge wingedEdge in wings)
			{
				if (hashSet.Add(wingedEdge.face))
				{
					HashSet<Face> hashSet2 = new HashSet<Face>
					{
						wingedEdge.face
					};
					ElementSelection.Flood(wingedEdge, hashSet2);
					foreach (Face item in hashSet2)
					{
						hashSet.Add(item);
					}
					list.Add(hashSet2);
				}
			}
			return list;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000312D8 File Offset: 0x0002F4D8
		private static Dictionary<EdgeLookup, Face> GetPerimeterEdges(HashSet<Face> faces, Dictionary<int, int> lookup)
		{
			Dictionary<EdgeLookup, Face> dictionary = new Dictionary<EdgeLookup, Face>();
			HashSet<EdgeLookup> hashSet = new HashSet<EdgeLookup>();
			foreach (Face face in faces)
			{
				foreach (Edge edge in face.edgesInternal)
				{
					EdgeLookup edgeLookup = new EdgeLookup(lookup[edge.a], lookup[edge.b], edge.a, edge.b);
					if (!hashSet.Add(edgeLookup))
					{
						if (dictionary.ContainsKey(edgeLookup))
						{
							dictionary.Remove(edgeLookup);
						}
					}
					else
					{
						dictionary.Add(edgeLookup, face);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005B7 RID: 1463 RVA: 0x0003648F File Offset: 0x0003468F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0003649B File Offset: 0x0003469B
			public <>c()
			{
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x000364A3 File Offset: 0x000346A3
			internal Face <DetachFaces>b__3_0(FaceRebuildData x)
			{
				return x.face;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x000364AB File Offset: 0x000346AB
			internal int <ExtrudePerFace>b__4_0(Face x)
			{
				return x.edges.Count;
			}

			// Token: 0x04000307 RID: 775
			public static readonly ExtrudeElements.<>c <>9 = new ExtrudeElements.<>c();

			// Token: 0x04000308 RID: 776
			public static Func<FaceRebuildData, Face> <>9__3_0;

			// Token: 0x04000309 RID: 777
			public static Func<Face, int> <>9__4_0;
		}
	}
}
