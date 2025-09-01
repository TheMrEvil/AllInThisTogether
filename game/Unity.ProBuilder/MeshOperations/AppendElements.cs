using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000078 RID: 120
	public static class AppendElements
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00029280 File Offset: 0x00027480
		internal static Face AppendFace(this ProBuilderMesh mesh, Vector3[] positions, Color[] colors, Vector2[] uv0s, Vector4[] uv2s, Vector4[] uv3s, Face face, int[] common)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			if (face == null)
			{
				throw new ArgumentNullException("face");
			}
			int num = positions.Length;
			if (common == null)
			{
				common = new int[num];
				for (int i = 0; i < num; i++)
				{
					common[i] = -1;
				}
			}
			int vertexCount = mesh.vertexCount;
			bool flag = mesh.HasArrays(MeshArrays.Color);
			bool flag2 = colors != null;
			bool flag3 = mesh.HasArrays(MeshArrays.Texture0);
			bool flag4 = uv0s != null;
			bool flag5 = mesh.HasArrays(MeshArrays.Texture2);
			bool flag6 = uv2s != null;
			bool flag7 = mesh.HasArrays(MeshArrays.Texture3);
			bool flag8 = uv3s != null;
			Vector3[] array = new Vector3[vertexCount + num];
			Color[] array2 = (flag || flag2) ? new Color[vertexCount + num] : null;
			Vector2[] array3 = (flag3 || flag4) ? new Vector2[vertexCount + num] : null;
			List<Vector4> list = (flag5 || flag6) ? new List<Vector4>() : null;
			List<Vector4> list2 = (flag7 || flag8) ? new List<Vector4>() : null;
			List<Face> list3 = new List<Face>(mesh.facesInternal);
			Array.Copy(mesh.positionsInternal, 0, array, 0, vertexCount);
			Array.Copy(positions, 0, array, vertexCount, num);
			if (flag || flag2)
			{
				Array.Copy(flag ? mesh.colorsInternal : ArrayUtility.Fill<Color>(Color.white, vertexCount), 0, array2, 0, vertexCount);
				Array.Copy(flag2 ? colors : ArrayUtility.Fill<Color>(Color.white, num), 0, array2, vertexCount, colors.Length);
			}
			if (flag3 || flag4)
			{
				Array.Copy(flag3 ? mesh.texturesInternal : ArrayUtility.Fill<Vector2>(Vector2.zero, vertexCount), 0, array3, 0, vertexCount);
				Array.Copy(flag4 ? uv0s : ArrayUtility.Fill<Vector2>(Vector2.zero, num), 0, array3, mesh.texturesInternal.Length, num);
			}
			if (flag5 || flag6)
			{
				list.AddRange(flag5 ? mesh.textures2Internal : new Vector4[vertexCount].ToList<Vector4>());
				list.AddRange(flag6 ? uv2s : new Vector4[num]);
			}
			if (flag7 || flag8)
			{
				list2.AddRange(flag7 ? mesh.textures3Internal : new Vector4[vertexCount].ToList<Vector4>());
				list2.AddRange(flag8 ? uv3s : new Vector4[num]);
			}
			face.ShiftIndexesToZero();
			face.ShiftIndexes(vertexCount);
			list3.Add(face);
			for (int j = 0; j < common.Length; j++)
			{
				if (common[j] < 0)
				{
					mesh.AddSharedVertex(new SharedVertex(new int[]
					{
						j + vertexCount
					}));
				}
				else
				{
					mesh.AddToSharedVertex(common[j], j + vertexCount);
				}
			}
			mesh.positions = array;
			mesh.colors = array2;
			mesh.textures = array3;
			mesh.faces = list3;
			mesh.textures2Internal = list;
			mesh.textures3Internal = list2;
			return face;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00029534 File Offset: 0x00027734
		public static Face[] AppendFaces(this ProBuilderMesh mesh, Vector3[][] positions, Color[][] colors, Vector2[][] uvs, Face[] faces, int[][] shared)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			if (colors == null)
			{
				throw new ArgumentNullException("colors");
			}
			if (uvs == null)
			{
				throw new ArgumentNullException("uvs");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			List<Vector3> list = new List<Vector3>(mesh.positionsInternal);
			List<Color> list2 = new List<Color>(mesh.colorsInternal);
			List<Vector2> list3 = new List<Vector2>(mesh.texturesInternal);
			List<Face> list4 = new List<Face>(mesh.facesInternal);
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			int num = mesh.vertexCount;
			for (int i = 0; i < faces.Length; i++)
			{
				list.AddRange(positions[i]);
				list2.AddRange(colors[i]);
				list3.AddRange(uvs[i]);
				faces[i].ShiftIndexesToZero();
				faces[i].ShiftIndexes(num);
				list4.Add(faces[i]);
				if (shared != null && positions[i].Length != shared[i].Length)
				{
					Debug.LogError("Append Face failed because shared array does not match new vertex array.");
					return null;
				}
				bool flag = shared != null;
				for (int j = 0; j < shared[i].Length; j++)
				{
					sharedVertexLookup.Add(j + num, flag ? shared[i][j] : -1);
				}
				num = list.Count;
			}
			mesh.positions = list;
			mesh.colors = list2;
			mesh.textures = list3;
			mesh.faces = list4;
			mesh.SetSharedVertices(sharedVertexLookup);
			return faces;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000296AC File Offset: 0x000278AC
		public static Face CreatePolygon(this ProBuilderMesh mesh, IList<int> indexes, bool unordered)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			HashSet<int> sharedVertexHandles = mesh.GetSharedVertexHandles(indexes);
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			List<Vertex> list2 = new List<Vertex>();
			foreach (int num in sharedVertexHandles)
			{
				int index = sharedVerticesInternal[num][0];
				list2.Add(new Vertex(list[index]));
			}
			FaceRebuildData faceRebuildData = AppendElements.FaceWithVertices(list2, unordered);
			if (faceRebuildData != null)
			{
				faceRebuildData.sharedIndexes = sharedVertexHandles.ToList<int>();
				List<Face> faces = new List<Face>(mesh.facesInternal);
				FaceRebuildData.Apply(new FaceRebuildData[]
				{
					faceRebuildData
				}, list, faces, sharedVertexLookup, null);
				mesh.SetVertices(list, false);
				mesh.faces = faces;
				mesh.SetSharedVertices(sharedVertexLookup);
				return faceRebuildData.face;
			}
			Log.Info(unordered ? "Too Few Unique Points Selected" : "Points not ordered correctly");
			return null;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000297C4 File Offset: 0x000279C4
		public static Face CreatePolygonWithHole(this ProBuilderMesh mesh, IList<int> indexes, IList<IList<int>> holes)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			HashSet<int> sharedVertexHandles = mesh.GetSharedVertexHandles(indexes);
			List<Vertex> list2 = new List<Vertex>();
			foreach (int num in sharedVertexHandles)
			{
				int index = sharedVerticesInternal[num][0];
				list2.Add(new Vertex(list[index]));
			}
			HashSet<int> hashSet = sharedVertexHandles;
			List<HashSet<int>> list3 = new List<HashSet<int>>();
			List<List<Vertex>> list4 = new List<List<Vertex>>();
			for (int i = 0; i < holes.Count; i++)
			{
				list3.Add(mesh.GetSharedVertexHandles(holes[i]));
				List<Vertex> list5 = new List<Vertex>();
				list4.Add(list5);
				foreach (int num2 in list3[i])
				{
					hashSet.Add(num2);
					int index2 = sharedVerticesInternal[num2][0];
					list5.Add(new Vertex(list[index2]));
				}
			}
			FaceRebuildData faceRebuildData = AppendElements.FaceWithVerticesAndHole(list2, list4);
			if (faceRebuildData != null)
			{
				faceRebuildData.sharedIndexes = hashSet.ToList<int>();
				List<Face> faces = new List<Face>(mesh.facesInternal);
				FaceRebuildData.Apply(new FaceRebuildData[]
				{
					faceRebuildData
				}, list, faces, sharedVertexLookup, null);
				mesh.SetVertices(list, false);
				mesh.faces = faces;
				mesh.SetSharedVertices(sharedVertexLookup);
				return faceRebuildData.face;
			}
			return null;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00029988 File Offset: 0x00027B88
		public static ActionResult CreateShapeFromPolygon(this PolyShape poly)
		{
			return poly.mesh.CreateShapeFromPolygon(poly.m_Points, poly.extrude, poly.flipNormals);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000299A7 File Offset: 0x00027BA7
		internal static void ClearAndRefreshMesh(this ProBuilderMesh mesh)
		{
			mesh.Clear();
			mesh.ToMesh(MeshTopology.Triangles);
			mesh.Refresh(RefreshMask.All);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000299BE File Offset: 0x00027BBE
		public static ActionResult CreateShapeFromPolygon(this ProBuilderMesh mesh, IList<Vector3> points, float extrude, bool flipNormals)
		{
			return mesh.CreateShapeFromPolygon(points, extrude, flipNormals, null);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000299CA File Offset: 0x00027BCA
		[Obsolete("Face.CreateShapeFromPolygon is deprecated as it no longer relies on camera look at.")]
		public static ActionResult CreateShapeFromPolygon(this ProBuilderMesh mesh, IList<Vector3> points, float extrude, bool flipNormals, Vector3 cameraLookAt, IList<IList<Vector3>> holePoints = null)
		{
			return mesh.CreateShapeFromPolygon(points, extrude, flipNormals, null);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000299D8 File Offset: 0x00027BD8
		public static ActionResult CreateShapeFromPolygon(this ProBuilderMesh mesh, IList<Vector3> points, float extrude, bool flipNormals, IList<IList<Vector3>> holePoints)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (points == null || points.Count < 3)
			{
				mesh.ClearAndRefreshMesh();
				return new ActionResult(ActionResult.Status.NoChange, "Too Few Points");
			}
			Vector3[] array = points.ToArray<Vector3>();
			Vector3[][] array2 = null;
			if (holePoints != null && holePoints.Count > 0)
			{
				array2 = new Vector3[holePoints.Count][];
				for (int i = 0; i < holePoints.Count; i++)
				{
					if (holePoints[i] == null || holePoints[i].Count < 3)
					{
						mesh.ClearAndRefreshMesh();
						return new ActionResult(ActionResult.Status.NoChange, "Too Few Points in hole " + i.ToString());
					}
					array2[i] = holePoints[i].ToArray<Vector3>();
				}
			}
			Log.PushLogLevel(LogLevel.Error);
			List<int> list;
			if (!Triangulation.TriangulateVertices(array, out list, array2))
			{
				mesh.ClearAndRefreshMesh();
				Log.PopLogLevel();
				return new ActionResult(ActionResult.Status.Failure, "Failed Triangulating Points");
			}
			Vector3[] array3;
			if (array2 != null)
			{
				array3 = new Vector3[array.Length + array2.Sum((Vector3[] arr) => arr.Length)];
				Array.Copy(array, array3, array.Length);
				int num = array.Length;
				foreach (Vector3[] array5 in array2)
				{
					Array.ConstrainedCopy(array5, 0, array3, num, array5.Length);
					num += array5.Length;
				}
			}
			else
			{
				array3 = array;
			}
			int[] array6 = list.ToArray();
			if (Math.PolygonArea(array3, array6) < Mathf.Epsilon)
			{
				mesh.ClearAndRefreshMesh();
				Log.PopLogLevel();
				return new ActionResult(ActionResult.Status.Failure, "Polygon Area < Epsilon");
			}
			mesh.Clear();
			mesh.positionsInternal = array3;
			Face face = new Face(array6);
			mesh.facesInternal = new Face[]
			{
				face
			};
			mesh.sharedVerticesInternal = SharedVertex.GetSharedVerticesWithPositions(array3);
			mesh.InvalidateCaches();
			if (face.distinctIndexesInternal.Length != array3.Length)
			{
				mesh.ClearAndRefreshMesh();
				Log.PopLogLevel();
				return new ActionResult(ActionResult.Status.Failure, "Triangulation missing points");
			}
			Vector3 vector = Math.Normal(mesh, mesh.facesInternal[0]);
			vector = mesh.gameObject.transform.TransformDirection(vector);
			if (flipNormals ? (Vector3.Dot(mesh.gameObject.transform.up, vector) > 0f) : (Vector3.Dot(mesh.gameObject.transform.up, vector) < 0f))
			{
				mesh.facesInternal[0].Reverse();
			}
			if (extrude != 0f)
			{
				mesh.DuplicateAndFlip(mesh.facesInternal);
				mesh.Extrude(new Face[]
				{
					flipNormals ? mesh.facesInternal[1] : mesh.facesInternal[0]
				}, ExtrudeMethod.IndividualFaces, extrude);
				if ((extrude < 0f && !flipNormals) || (extrude > 0f && flipNormals))
				{
					Face[] facesInternal = mesh.facesInternal;
					for (int j = 0; j < facesInternal.Length; j++)
					{
						facesInternal[j].Reverse();
					}
				}
			}
			mesh.ToMesh(MeshTopology.Triangles);
			mesh.Refresh(RefreshMask.All);
			Log.PopLogLevel();
			return new ActionResult(ActionResult.Status.Success, "Create Polygon Shape");
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00029CE0 File Offset: 0x00027EE0
		internal static FaceRebuildData FaceWithVertices(List<Vertex> vertices, bool unordered = true)
		{
			List<int> indices;
			if (Triangulation.TriangulateVertices(vertices, out indices, unordered, false))
			{
				return new FaceRebuildData
				{
					vertices = vertices,
					face = new Face(indices)
				};
			}
			return null;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00029D14 File Offset: 0x00027F14
		internal static FaceRebuildData FaceWithVerticesAndHole(List<Vertex> borderVertices, List<List<Vertex>> holes)
		{
			Vector3[] vertices = (from v in borderVertices
			select v.position).ToArray<Vector3>();
			Vector3[][] array = new Vector3[holes.Count][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (from v in holes[i]
				select v.position).ToArray<Vector3>();
			}
			List<int> indices;
			if (Triangulation.TriangulateVertices(vertices, out indices, array))
			{
				List<Vertex> list = new List<Vertex>();
				list.AddRange(borderVertices);
				foreach (List<Vertex> collection in holes)
				{
					list.AddRange(collection);
				}
				return new FaceRebuildData
				{
					vertices = list,
					face = new Face(indices)
				};
			}
			return null;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00029E14 File Offset: 0x00028014
		internal static List<FaceRebuildData> TentCapWithVertices(List<Vertex> path)
		{
			int count = path.Count;
			Vertex item = Vertex.Average(path, null);
			List<FaceRebuildData> list = new List<FaceRebuildData>();
			for (int i = 0; i < count; i++)
			{
				List<Vertex> vertices = new List<Vertex>
				{
					path[i],
					item,
					path[(i + 1) % count]
				};
				list.Add(new FaceRebuildData
				{
					vertices = vertices,
					face = new Face(new int[]
					{
						0,
						1,
						2
					})
				});
			}
			return list;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00029EA4 File Offset: 0x000280A4
		public static void DuplicateAndFlip(this ProBuilderMesh mesh, Face[] faces)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			List<FaceRebuildData> list = new List<FaceRebuildData>();
			List<Vertex> list2 = new List<Vertex>(mesh.GetVertices(null));
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			foreach (Face face in faces)
			{
				FaceRebuildData faceRebuildData = new FaceRebuildData();
				faceRebuildData.vertices = new List<Vertex>();
				faceRebuildData.face = new Face(face);
				faceRebuildData.sharedIndexes = new List<int>();
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int num = faceRebuildData.face.indexesInternal.Length;
				for (int j = 0; j < num; j++)
				{
					if (!dictionary.ContainsKey(face.indexesInternal[j]))
					{
						dictionary.Add(face.indexesInternal[j], dictionary.Count);
						faceRebuildData.vertices.Add(list2[face.indexesInternal[j]]);
						faceRebuildData.sharedIndexes.Add(sharedVertexLookup[face.indexesInternal[j]]);
					}
				}
				int[] array = new int[num];
				for (int k = 0; k < num; k++)
				{
					array[num - (k + 1)] = dictionary[faceRebuildData.face[k]];
				}
				faceRebuildData.face.SetIndexes(array);
				list.Add(faceRebuildData);
			}
			FaceRebuildData.Apply(list, mesh, list2, null);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002A020 File Offset: 0x00028220
		public static Face Bridge(this ProBuilderMesh mesh, Edge a, Edge b, bool allowNonManifoldGeometry = false)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			if (!allowNonManifoldGeometry && (ElementSelection.GetNeighborFaces(mesh, a).Count > 1 || ElementSelection.GetNeighborFaces(mesh, b).Count > 1))
			{
				return null;
			}
			foreach (Face face in mesh.facesInternal)
			{
				if (mesh.IndexOf(face.edgesInternal, a) >= 0 && mesh.IndexOf(face.edgesInternal, b) >= 0)
				{
					Log.Warning("Face already exists between these two edges!");
					return null;
				}
			}
			Vector3[] positionsInternal = mesh.positionsInternal;
			bool flag = mesh.HasArrays(MeshArrays.Color);
			Color[] array = flag ? mesh.colorsInternal : null;
			AutoUnwrapSettings tile = AutoUnwrapSettings.tile;
			int submeshIndex = 0;
			SimpleTuple<Face, Edge> simpleTuple;
			if (EdgeUtility.ValidateEdge(mesh, a, out simpleTuple) || EdgeUtility.ValidateEdge(mesh, b, out simpleTuple))
			{
				tile = new AutoUnwrapSettings(simpleTuple.item1.uv);
				submeshIndex = simpleTuple.item1.submeshIndex;
			}
			Vector3[] array2;
			Color[] array3;
			int[] array4;
			if (a.Contains(b.a, sharedVertexLookup) || a.Contains(b.b, sharedVertexLookup))
			{
				array2 = new Vector3[3];
				array3 = new Color[3];
				array4 = new int[3];
				bool flag2 = Array.IndexOf<int>(sharedVerticesInternal[mesh.GetSharedVertexHandle(a.a)].arrayInternal, b.a) > -1;
				bool flag3 = Array.IndexOf<int>(sharedVerticesInternal[mesh.GetSharedVertexHandle(a.a)].arrayInternal, b.b) > -1;
				bool flag4 = Array.IndexOf<int>(sharedVerticesInternal[mesh.GetSharedVertexHandle(a.b)].arrayInternal, b.a) > -1;
				bool flag5 = Array.IndexOf<int>(sharedVerticesInternal[mesh.GetSharedVertexHandle(a.b)].arrayInternal, b.b) > -1;
				if (flag2)
				{
					array2[0] = positionsInternal[a.a];
					if (flag)
					{
						array3[0] = array[a.a];
					}
					array4[0] = mesh.GetSharedVertexHandle(a.a);
					array2[1] = positionsInternal[a.b];
					if (flag)
					{
						array3[1] = array[a.b];
					}
					array4[1] = mesh.GetSharedVertexHandle(a.b);
					array2[2] = positionsInternal[b.b];
					if (flag)
					{
						array3[2] = array[b.b];
					}
					array4[2] = mesh.GetSharedVertexHandle(b.b);
				}
				else if (flag3)
				{
					array2[0] = positionsInternal[a.a];
					if (flag)
					{
						array3[0] = array[a.a];
					}
					array4[0] = mesh.GetSharedVertexHandle(a.a);
					array2[1] = positionsInternal[a.b];
					if (flag)
					{
						array3[1] = array[a.b];
					}
					array4[1] = mesh.GetSharedVertexHandle(a.b);
					array2[2] = positionsInternal[b.a];
					if (flag)
					{
						array3[2] = array[b.a];
					}
					array4[2] = mesh.GetSharedVertexHandle(b.a);
				}
				else if (flag4)
				{
					array2[0] = positionsInternal[a.b];
					if (flag)
					{
						array3[0] = array[a.b];
					}
					array4[0] = mesh.GetSharedVertexHandle(a.b);
					array2[1] = positionsInternal[a.a];
					if (flag)
					{
						array3[1] = array[a.a];
					}
					array4[1] = mesh.GetSharedVertexHandle(a.a);
					array2[2] = positionsInternal[b.b];
					if (flag)
					{
						array3[2] = array[b.b];
					}
					array4[2] = mesh.GetSharedVertexHandle(b.b);
				}
				else if (flag5)
				{
					array2[0] = positionsInternal[a.b];
					if (flag)
					{
						array3[0] = array[a.b];
					}
					array4[0] = mesh.GetSharedVertexHandle(a.b);
					array2[1] = positionsInternal[a.a];
					if (flag)
					{
						array3[1] = array[a.a];
					}
					array4[1] = mesh.GetSharedVertexHandle(a.a);
					array2[2] = positionsInternal[b.a];
					if (flag)
					{
						array3[2] = array[b.a];
					}
					array4[2] = mesh.GetSharedVertexHandle(b.a);
				}
				Vector3[] positions = array2;
				Color[] colors = flag ? array3 : null;
				Vector2[] uv0s = new Vector2[array2.Length];
				Vector4[] uv2s = new Vector4[array2.Length];
				Vector4[] uv3s = new Vector4[array2.Length];
				IEnumerable<int> triangles;
				if (!flag2 && !flag3)
				{
					int[] array5 = new int[3];
					array5[1] = 1;
					triangles = array5;
					array5[2] = 2;
				}
				else
				{
					int[] array6 = new int[3];
					array6[0] = 2;
					triangles = array6;
					array6[1] = 1;
				}
				return mesh.AppendFace(positions, colors, uv0s, uv2s, uv3s, new Face(triangles, submeshIndex, tile, 0, -1, -1, false), array4);
			}
			array2 = new Vector3[4];
			array3 = new Color[4];
			array4 = new int[4];
			array2[0] = positionsInternal[a.a];
			if (flag)
			{
				array3[0] = mesh.colorsInternal[a.a];
			}
			array4[0] = mesh.GetSharedVertexHandle(a.a);
			array2[1] = positionsInternal[a.b];
			if (flag)
			{
				array3[1] = mesh.colorsInternal[a.b];
			}
			array4[1] = mesh.GetSharedVertexHandle(a.b);
			Vector3 normalized = Vector3.Cross(positionsInternal[b.a] - positionsInternal[a.a], positionsInternal[a.b] - positionsInternal[a.a]).normalized;
			Vector2[] array7 = Projection.PlanarProject(new Vector3[]
			{
				positionsInternal[a.a],
				positionsInternal[a.b],
				positionsInternal[b.a],
				positionsInternal[b.b]
			}, null, normalized);
			Vector2 zero = Vector2.zero;
			if (!Math.GetLineSegmentIntersect(array7[0], array7[2], array7[1], array7[3], ref zero))
			{
				array2[2] = positionsInternal[b.a];
				if (flag)
				{
					array3[2] = mesh.colorsInternal[b.a];
				}
				array4[2] = mesh.GetSharedVertexHandle(b.a);
				array2[3] = positionsInternal[b.b];
				if (flag)
				{
					array3[3] = mesh.colorsInternal[b.b];
				}
				array4[3] = mesh.GetSharedVertexHandle(b.b);
			}
			else
			{
				array2[2] = positionsInternal[b.b];
				if (flag)
				{
					array3[2] = mesh.colorsInternal[b.b];
				}
				array4[2] = mesh.GetSharedVertexHandle(b.b);
				array2[3] = positionsInternal[b.a];
				if (flag)
				{
					array3[3] = mesh.colorsInternal[b.a];
				}
				array4[3] = mesh.GetSharedVertexHandle(b.a);
			}
			return mesh.AppendFace(array2, flag ? array3 : null, new Vector2[array2.Length], new Vector4[array2.Length], new Vector4[array2.Length], new Face(new int[]
			{
				2,
				1,
				0,
				2,
				3,
				1
			}, submeshIndex, tile, 0, -1, -1, false), array4);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002A804 File Offset: 0x00028A04
		public static Face AppendVerticesToFace(this ProBuilderMesh mesh, Face face, Vector3[] points)
		{
			return mesh.AppendVerticesToFace(face, points, true);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002A810 File Offset: 0x00028A10
		public static Face AppendVerticesToFace(this ProBuilderMesh mesh, Face face, Vector3[] points, bool insertOnEdge)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (face == null)
			{
				throw new ArgumentNullException("face");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			List<Vertex> list = mesh.GetVertices(null).ToList<Vertex>();
			List<Face> faces = new List<Face>(mesh.facesInternal);
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> dictionary = null;
			if (mesh.sharedTextures != null)
			{
				dictionary = new Dictionary<int, int>();
				SharedVertex.GetSharedVertexLookup(mesh.sharedTextures, dictionary);
			}
			List<Edge> list2 = WingedEdge.SortEdgesByAdjacency(face);
			List<Vertex> list3 = new List<Vertex>();
			List<int> list4 = new List<int>();
			List<int> list5 = (dictionary != null) ? new List<int>() : null;
			for (int i = 0; i < list2.Count; i++)
			{
				list3.Add(list[list2[i].a]);
				list4.Add(sharedVertexLookup[list2[i].a]);
				if (dictionary != null)
				{
					int item;
					if (dictionary.TryGetValue(list2[i].a, out item))
					{
						list5.Add(item);
					}
					else
					{
						list5.Add(-1);
					}
				}
			}
			if (insertOnEdge)
			{
				for (int j = 0; j < points.Length; j++)
				{
					int num = -1;
					float num2 = float.PositiveInfinity;
					Vector3 vector = points[j];
					int count = list3.Count;
					for (int k = 0; k < count; k++)
					{
						Vector3 position = list3[k].position;
						Vector3 position2 = list3[(k + 1) % count].position;
						float num3 = Math.DistancePointLineSegment(vector, position, position2);
						if (num3 < num2)
						{
							num2 = num3;
							num = k;
						}
					}
					Vertex vertex = list3[num];
					Vertex vertex2 = list3[(num + 1) % count];
					float sqrMagnitude = (vector - vertex.position).sqrMagnitude;
					float sqrMagnitude2 = (vector - vertex2.position).sqrMagnitude;
					Vertex item2 = Vertex.Mix(vertex, vertex2, sqrMagnitude / (sqrMagnitude + sqrMagnitude2));
					list3.Insert((num + 1) % count, item2);
					list4.Insert((num + 1) % count, -1);
					if (list5 != null)
					{
						list5.Insert((num + 1) % count, -1);
					}
				}
			}
			else
			{
				for (int l = 0; l < points.Length; l++)
				{
					int num4 = -1;
					Vector3 position3 = points[l];
					int count2 = list3.Count;
					Vertex vertex3 = new Vertex();
					vertex3.position = position3;
					list3.Insert((num4 + 1) % count2, vertex3);
					list4.Insert((num4 + 1) % count2, -1);
					if (list5 != null)
					{
						list5.Insert((num4 + 1) % count2, -1);
					}
				}
			}
			List<int> list6;
			try
			{
				Triangulation.TriangulateVertices(list3, out list6, true, false);
			}
			catch
			{
				Debug.Log("Failed triangulating face after appending vertices.");
				return null;
			}
			FaceRebuildData faceRebuildData = new FaceRebuildData();
			faceRebuildData.face = new Face(list6.ToArray(), face.submeshIndex, new AutoUnwrapSettings(face.uv), face.smoothingGroup, face.textureGroup, -1, face.manualUV);
			faceRebuildData.vertices = list3;
			faceRebuildData.sharedIndexes = list4;
			faceRebuildData.sharedIndexesUV = list5;
			FaceRebuildData.Apply(new List<FaceRebuildData>
			{
				faceRebuildData
			}, list, faces, sharedVertexLookup, dictionary);
			Face face2 = faceRebuildData.face;
			mesh.SetVertices(list, false);
			mesh.faces = faces;
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(dictionary);
			Vector3 lhs = Math.Normal(mesh, face);
			Vector3 rhs = Math.Normal(mesh, face2);
			if (Vector3.Dot(lhs, rhs) < 0f)
			{
				face2.Reverse();
			}
			mesh.DeleteFace(face);
			return face2;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002ABAC File Offset: 0x00028DAC
		public static List<Edge> AppendVerticesToEdge(this ProBuilderMesh mesh, Edge edge, int count)
		{
			return mesh.AppendVerticesToEdge(new Edge[]
			{
				edge
			}, count);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0002ABC4 File Offset: 0x00028DC4
		public static List<Edge> AppendVerticesToEdge(this ProBuilderMesh mesh, IList<Edge> edges, int count)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (edges == null)
			{
				throw new ArgumentNullException("edges");
			}
			if (count < 1 || count > 512)
			{
				Log.Error("New edge vertex count is less than 1 or greater than 512.");
				return null;
			}
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = mesh.sharedTextureLookup;
			List<int> list2 = new List<int>();
			List<Edge> list3 = mesh.GetSharedVertexHandleEdges(edges).Distinct<Edge>().ToList<Edge>();
			Dictionary<Face, FaceRebuildData> dictionary = new Dictionary<Face, FaceRebuildData>();
			int count2 = sharedVertexLookup.Count;
			int num = count2;
			foreach (Edge edge in list3)
			{
				Edge edgeWithSharedVertexHandles = mesh.GetEdgeWithSharedVertexHandles(edge);
				List<Vertex> list4 = new List<Vertex>(count);
				for (int i = 0; i < count; i++)
				{
					list4.Add(Vertex.Mix(list[edgeWithSharedVertexHandles.a], list[edgeWithSharedVertexHandles.b], (float)(i + 1) / ((float)count + 1f)));
				}
				List<SimpleTuple<Face, Edge>> neighborFaces = ElementSelection.GetNeighborFaces(mesh, edgeWithSharedVertexHandles);
				Edge edge2 = new Edge(sharedVertexLookup[edgeWithSharedVertexHandles.a], sharedVertexLookup[edgeWithSharedVertexHandles.b]);
				Edge edge3 = default(Edge);
				foreach (SimpleTuple<Face, Edge> simpleTuple in neighborFaces)
				{
					Face item = simpleTuple.item1;
					FaceRebuildData faceRebuildData;
					if (!dictionary.TryGetValue(item, out faceRebuildData))
					{
						faceRebuildData = new FaceRebuildData();
						faceRebuildData.face = new Face(new int[0], item.submeshIndex, new AutoUnwrapSettings(item.uv), item.smoothingGroup, item.textureGroup, -1, item.manualUV);
						faceRebuildData.vertices = new List<Vertex>(list.ValuesWithIndexes(item.distinctIndexesInternal));
						faceRebuildData.sharedIndexes = new List<int>();
						faceRebuildData.sharedIndexesUV = new List<int>();
						foreach (int key in item.distinctIndexesInternal)
						{
							int item2;
							if (sharedVertexLookup.TryGetValue(key, out item2))
							{
								faceRebuildData.sharedIndexes.Add(item2);
							}
							if (sharedTextureLookup.TryGetValue(key, out item2))
							{
								faceRebuildData.sharedIndexesUV.Add(item2);
							}
						}
						list2.AddRange(item.distinctIndexesInternal);
						dictionary.Add(item, faceRebuildData);
						List<Vertex> list5 = new List<Vertex>();
						List<int> list6 = new List<int>();
						List<int> list7 = new List<int>();
						List<Edge> list8 = WingedEdge.SortEdgesByAdjacency(item);
						for (int k = 0; k < list8.Count; k++)
						{
							edge3.a = list8[k].a;
							edge3.b = list8[k].b;
							list5.Add(list[edge3.a]);
							int item3;
							if (sharedVertexLookup.TryGetValue(edge3.a, out item3))
							{
								list6.Add(item3);
							}
							if (sharedTextureLookup.TryGetValue(k, out item3))
							{
								faceRebuildData.sharedIndexesUV.Add(item3);
							}
							if (edge2.a == sharedVertexLookup[edge3.a] && edge2.b == sharedVertexLookup[edge3.b])
							{
								for (int l = 0; l < count; l++)
								{
									list5.Add(list4[l]);
									list6.Add(num + l);
									list7.Add(-1);
								}
							}
							else if (edge2.a == sharedVertexLookup[edge3.b] && edge2.b == sharedVertexLookup[edge3.a])
							{
								for (int m = count - 1; m >= 0; m--)
								{
									list5.Add(list4[m]);
									list6.Add(num + m);
									list7.Add(-1);
								}
							}
						}
						faceRebuildData.vertices = list5;
						faceRebuildData.sharedIndexes = list6;
						faceRebuildData.sharedIndexesUV = list7;
					}
					else
					{
						List<Vertex> vertices = faceRebuildData.vertices;
						List<int> sharedIndexes = faceRebuildData.sharedIndexes;
						List<int> sharedIndexesUV = faceRebuildData.sharedIndexesUV;
						for (int n = 0; n < vertices.Count; n++)
						{
							Vertex item4 = vertices[n];
							int num2 = list.IndexOf(item4);
							Vertex item5 = vertices[(n + 1) % vertices.Count];
							int num3 = list.IndexOf(item5);
							if (num2 != -1 && num3 != -1)
							{
								if (sharedVertexLookup[num2] == sharedVertexLookup[edgeWithSharedVertexHandles.a] && sharedVertexLookup[num3] == sharedVertexLookup[edgeWithSharedVertexHandles.b])
								{
									vertices.InsertRange(n + 1, list4);
									for (int num4 = 0; num4 < count; num4++)
									{
										sharedIndexes.Insert(n + num4 + 1, num + num4);
										sharedIndexesUV.Add(-1);
									}
								}
								else if (sharedVertexLookup[num2] == sharedVertexLookup[edgeWithSharedVertexHandles.b] && sharedVertexLookup[num3] == sharedVertexLookup[edgeWithSharedVertexHandles.a])
								{
									list4.Reverse();
									vertices.InsertRange(n + 1, list4);
									for (int num5 = count - 1; num5 >= 0; num5--)
									{
										sharedIndexes.Insert(n + 1, num + num5);
										sharedIndexesUV.Add(-1);
									}
								}
							}
						}
						faceRebuildData.vertices = vertices;
						faceRebuildData.sharedIndexes = sharedIndexes;
						faceRebuildData.sharedIndexesUV = sharedIndexesUV;
					}
				}
				num += count;
			}
			List<Face> list9 = dictionary.Keys.ToList<Face>();
			List<FaceRebuildData> list10 = dictionary.Values.ToList<FaceRebuildData>();
			List<EdgeLookup> list11 = new List<EdgeLookup>();
			for (int num6 = 0; num6 < list9.Count; num6++)
			{
				Face face = list9[num6];
				FaceRebuildData faceRebuildData2 = list10[num6];
				int count3 = list.Count;
				List<int> indices;
				if (Triangulation.TriangulateVertices(faceRebuildData2.vertices, out indices, false, false))
				{
					faceRebuildData2.face = new Face(indices);
					faceRebuildData2.face.submeshIndex = face.submeshIndex;
					faceRebuildData2.face.ShiftIndexes(count3);
					face.CopyFrom(faceRebuildData2.face);
					for (int num7 = 0; num7 < faceRebuildData2.vertices.Count; num7++)
					{
						sharedVertexLookup.Add(count3 + num7, faceRebuildData2.sharedIndexes[num7]);
					}
					if (faceRebuildData2.sharedIndexesUV.Count == faceRebuildData2.vertices.Count)
					{
						for (int num8 = 0; num8 < faceRebuildData2.vertices.Count; num8++)
						{
							sharedTextureLookup.Add(count3 + num8, faceRebuildData2.sharedIndexesUV[num8]);
						}
					}
					list.AddRange(faceRebuildData2.vertices);
					foreach (Edge edge4 in face.edgesInternal)
					{
						EdgeLookup item6 = new EdgeLookup(new Edge(sharedVertexLookup[edge4.a], sharedVertexLookup[edge4.b]), edge4);
						if (item6.common.a >= count2 || item6.common.b >= count2)
						{
							list11.Add(item6);
						}
					}
				}
			}
			list2 = list2.Distinct<int>().ToList<int>();
			int delCount = list2.Count;
			List<Edge> result = (from x in list11.Distinct<EdgeLookup>()
			select x.local - delCount).ToList<Edge>();
			mesh.SetVertices(list, false);
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(sharedTextureLookup);
			mesh.DeleteVertices(list2);
			return result;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0002B37C File Offset: 0x0002957C
		public static Face[] InsertVertexInFace(this ProBuilderMesh mesh, Face face, Vector3 point)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			if (face == null)
			{
				throw new ArgumentNullException("face");
			}
			List<Vertex> list = mesh.GetVertices(null).ToList<Vertex>();
			List<Face> faces = new List<Face>(mesh.facesInternal);
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> dictionary = null;
			if (mesh.sharedTextures != null)
			{
				dictionary = new Dictionary<int, int>();
				SharedVertex.GetSharedVertexLookup(mesh.sharedTextures, dictionary);
			}
			List<Edge> list2 = WingedEdge.SortEdgesByAdjacency(face);
			List<FaceRebuildData> list3 = new List<FaceRebuildData>();
			Vertex vertex = new Vertex();
			vertex.position = point;
			for (int i = 0; i < list2.Count; i++)
			{
				List<Vertex> list4 = new List<Vertex>();
				List<int> list5 = new List<int>();
				List<int> list6 = (dictionary != null) ? new List<int>() : null;
				list4.Add(list[list2[i].a]);
				list4.Add(list[list2[i].b]);
				list4.Add(vertex);
				list5.Add(sharedVertexLookup[list2[i].a]);
				list5.Add(sharedVertexLookup[list2[i].b]);
				list5.Add(list.Count);
				if (dictionary != null)
				{
					dictionary.Clear();
					int item;
					if (dictionary.TryGetValue(list2[i].a, out item))
					{
						list6.Add(item);
					}
					else
					{
						list6.Add(-1);
					}
					if (dictionary.TryGetValue(list2[i].b, out item))
					{
						list6.Add(item);
					}
					else
					{
						list6.Add(-1);
					}
					list6.Add(list.Count);
				}
				List<int> list7;
				try
				{
					Triangulation.TriangulateVertices(list4, out list7, true, false);
				}
				catch
				{
					Debug.Log("Failed triangulating face after appending vertices.");
					return null;
				}
				list3.Add(new FaceRebuildData
				{
					face = new Face(list7.ToArray(), face.submeshIndex, new AutoUnwrapSettings(face.uv), face.smoothingGroup, face.textureGroup, -1, face.manualUV),
					vertices = list4,
					sharedIndexes = list5,
					sharedIndexesUV = list6
				});
			}
			FaceRebuildData.Apply(list3, list, faces, sharedVertexLookup, dictionary);
			mesh.SetVertices(list, false);
			mesh.faces = faces;
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(dictionary);
			Face[] result = (from f in list3
			select f.face).ToArray<Face>();
			foreach (FaceRebuildData faceRebuildData in list3)
			{
				Face face2 = faceRebuildData.face;
				Vector3 lhs = Math.Normal(mesh, face);
				Vector3 rhs = Math.Normal(mesh, face2);
				if (Vector3.Dot(lhs, rhs) < 0f)
				{
					face2.Reverse();
				}
			}
			mesh.DeleteFace(face);
			return result;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0002B684 File Offset: 0x00029884
		public static Vertex InsertVertexOnEdge(this ProBuilderMesh mesh, Edge originalEdge, Vector3 point)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			List<Vertex> list = new List<Vertex>(mesh.GetVertices(null));
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> sharedTextureLookup = mesh.sharedTextureLookup;
			List<int> list2 = new List<int>();
			Dictionary<Face, FaceRebuildData> dictionary = new Dictionary<Face, FaceRebuildData>();
			int count = sharedVertexLookup.Count;
			Vector3 vector = point - list[originalEdge.a].position;
			Vector3 vector2 = list[originalEdge.b].position - list[originalEdge.a].position;
			float weight = Vector3.Magnitude(vector) * Mathf.Cos(Vector3.Angle(vector2, vector) * 0.017453292f) / Vector3.Magnitude(vector2);
			Vertex vertex = Vertex.Mix(list[originalEdge.a], list[originalEdge.b], weight);
			List<SimpleTuple<Face, Edge>> neighborFaces = ElementSelection.GetNeighborFaces(mesh, originalEdge);
			Edge edge = new Edge(sharedVertexLookup[originalEdge.a], sharedVertexLookup[originalEdge.b]);
			Edge edge2 = default(Edge);
			foreach (SimpleTuple<Face, Edge> simpleTuple in neighborFaces)
			{
				Face item = simpleTuple.item1;
				FaceRebuildData faceRebuildData;
				if (!dictionary.TryGetValue(item, out faceRebuildData))
				{
					faceRebuildData = new FaceRebuildData();
					faceRebuildData.face = new Face(new int[0], item.submeshIndex, new AutoUnwrapSettings(item.uv), item.smoothingGroup, item.textureGroup, -1, item.manualUV);
					faceRebuildData.vertices = new List<Vertex>(list.ValuesWithIndexes(item.distinctIndexesInternal));
					faceRebuildData.sharedIndexes = new List<int>();
					faceRebuildData.sharedIndexesUV = new List<int>();
					foreach (int key in item.distinctIndexesInternal)
					{
						int item2;
						if (sharedVertexLookup.TryGetValue(key, out item2))
						{
							faceRebuildData.sharedIndexes.Add(item2);
						}
						if (sharedTextureLookup.TryGetValue(key, out item2))
						{
							faceRebuildData.sharedIndexesUV.Add(item2);
						}
					}
					list2.AddRange(item.distinctIndexesInternal);
					dictionary.Add(item, faceRebuildData);
				}
				faceRebuildData.vertices.Add(vertex);
				faceRebuildData.sharedIndexes.Add(count);
				faceRebuildData.sharedIndexesUV.Add(-1);
				List<Vertex> list3 = new List<Vertex>();
				List<int> list4 = new List<int>();
				List<Edge> list5 = WingedEdge.SortEdgesByAdjacency(item);
				bool flag = true;
				for (int j = 0; j < list5.Count; j++)
				{
					edge2.a = list5[j].a;
					edge2.b = list5[j].b;
					list3.Add(list[edge2.a]);
					int item3;
					if (sharedVertexLookup.TryGetValue(edge2.a, out item3))
					{
						list4.Add(item3);
					}
					if ((flag && edge.a == sharedVertexLookup[edge2.a] && edge.b == sharedVertexLookup[edge2.b]) || (edge.a == sharedVertexLookup[edge2.b] && edge.b == sharedVertexLookup[edge2.a]))
					{
						flag = false;
						list3.Add(faceRebuildData.vertices[faceRebuildData.vertices.Count - 1]);
						list4.Add(count);
					}
				}
				faceRebuildData.vertices = list3;
				faceRebuildData.sharedIndexes = list4;
			}
			List<Face> list6 = dictionary.Keys.ToList<Face>();
			List<FaceRebuildData> list7 = dictionary.Values.ToList<FaceRebuildData>();
			for (int k = 0; k < list6.Count; k++)
			{
				Face face = list6[k];
				FaceRebuildData faceRebuildData2 = list7[k];
				int count2 = list.Count;
				List<int> indices;
				if (Triangulation.TriangulateVertices(faceRebuildData2.vertices, out indices, false, false))
				{
					faceRebuildData2.face = new Face(indices);
					faceRebuildData2.face.submeshIndex = face.submeshIndex;
					faceRebuildData2.face.ShiftIndexes(count2);
					face.CopyFrom(faceRebuildData2.face);
					for (int l = 0; l < faceRebuildData2.vertices.Count; l++)
					{
						sharedVertexLookup.Add(count2 + l, faceRebuildData2.sharedIndexes[l]);
					}
					if (faceRebuildData2.sharedIndexesUV.Count == faceRebuildData2.vertices.Count)
					{
						for (int m = 0; m < faceRebuildData2.vertices.Count; m++)
						{
							sharedTextureLookup.Add(count2 + m, faceRebuildData2.sharedIndexesUV[m]);
						}
					}
					list.AddRange(faceRebuildData2.vertices);
				}
			}
			list2 = list2.Distinct<int>().ToList<int>();
			mesh.SetVertices(list, false);
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(sharedTextureLookup);
			mesh.DeleteVertices(list2);
			return vertex;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0002BB88 File Offset: 0x00029D88
		public static Vertex InsertVertexInMesh(this ProBuilderMesh mesh, Vector3 point, Vector3 normal)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			List<Vertex> list = mesh.GetVertices(null).ToList<Vertex>();
			Dictionary<int, int> sharedVertexLookup = mesh.sharedVertexLookup;
			Dictionary<int, int> dictionary = null;
			int count = sharedVertexLookup.Count;
			if (mesh.sharedTextures != null)
			{
				dictionary = new Dictionary<int, int>();
				SharedVertex.GetSharedVertexLookup(mesh.sharedTextures, dictionary);
			}
			Vertex vertex = new Vertex();
			vertex.position = point;
			vertex.normal = normal.normalized;
			list.Add(vertex);
			sharedVertexLookup.Add(count, count);
			dictionary.Add(count, -1);
			mesh.SetVertices(list, false);
			mesh.SetSharedVertices(sharedVertexLookup);
			mesh.SetSharedTextures(dictionary);
			return vertex;
		}

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600056A RID: 1386 RVA: 0x00035FCF File Offset: 0x000341CF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00035FDB File Offset: 0x000341DB
			public <>c()
			{
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x00035FE3 File Offset: 0x000341E3
			internal int <CreateShapeFromPolygon>b__8_0(Vector3[] arr)
			{
				return arr.Length;
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00035FE8 File Offset: 0x000341E8
			internal Vector3 <FaceWithVerticesAndHole>b__10_0(Vertex v)
			{
				return v.position;
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x00035FF0 File Offset: 0x000341F0
			internal Vector3 <FaceWithVerticesAndHole>b__10_1(Vertex v)
			{
				return v.position;
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x00035FF8 File Offset: 0x000341F8
			internal Face <InsertVertexInFace>b__18_0(FaceRebuildData f)
			{
				return f.face;
			}

			// Token: 0x040002C8 RID: 712
			public static readonly AppendElements.<>c <>9 = new AppendElements.<>c();

			// Token: 0x040002C9 RID: 713
			public static Func<Vector3[], int> <>9__8_0;

			// Token: 0x040002CA RID: 714
			public static Func<Vertex, Vector3> <>9__10_0;

			// Token: 0x040002CB RID: 715
			public static Func<Vertex, Vector3> <>9__10_1;

			// Token: 0x040002CC RID: 716
			public static Func<FaceRebuildData, Face> <>9__18_0;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x06000570 RID: 1392 RVA: 0x00036000 File Offset: 0x00034200
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x00036008 File Offset: 0x00034208
			internal Edge <AppendVerticesToEdge>b__0(EdgeLookup x)
			{
				return x.local - this.delCount;
			}

			// Token: 0x040002CD RID: 717
			public int delCount;
		}
	}
}
