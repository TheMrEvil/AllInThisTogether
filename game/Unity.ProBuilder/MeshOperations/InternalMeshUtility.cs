using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000080 RID: 128
	internal static class InternalMeshUtility
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x000313B0 File Offset: 0x0002F5B0
		internal static Vector3 AverageNormalWithIndexes(SharedVertex shared, int[] all, IList<Vector3> norm)
		{
			Vector3 a = Vector3.zero;
			int num = 0;
			for (int i = 0; i < all.Length; i++)
			{
				if (shared.Contains(all[i]))
				{
					a += norm[all[i]];
					num++;
				}
			}
			return a / (float)num;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000313FC File Offset: 0x0002F5FC
		public static ProBuilderMesh CreateMeshWithTransform(Transform t, bool preserveFaces)
		{
			Mesh sharedMesh = t.GetComponent<MeshFilter>().sharedMesh;
			Vector3[] meshChannel = MeshUtility.GetMeshChannel<Vector3[]>(t.gameObject, (Mesh x) => x.vertices);
			Color[] meshChannel2 = MeshUtility.GetMeshChannel<Color[]>(t.gameObject, (Mesh x) => x.colors);
			Vector2[] meshChannel3 = MeshUtility.GetMeshChannel<Vector2[]>(t.gameObject, (Mesh x) => x.uv);
			List<Vector3> list = preserveFaces ? new List<Vector3>(sharedMesh.vertices) : new List<Vector3>();
			List<Color> list2 = preserveFaces ? new List<Color>(sharedMesh.colors) : new List<Color>();
			List<Vector2> list3 = preserveFaces ? new List<Vector2>(sharedMesh.uv) : new List<Vector2>();
			List<Face> list4 = new List<Face>();
			for (int i = 0; i < sharedMesh.subMeshCount; i++)
			{
				int[] triangles = sharedMesh.GetTriangles(i);
				for (int j = 0; j < triangles.Length; j += 3)
				{
					int num = -1;
					if (preserveFaces)
					{
						for (int k = 0; k < list4.Count; k++)
						{
							if (list4[k].distinctIndexesInternal.Contains(triangles[j]) || list4[k].distinctIndexesInternal.Contains(triangles[j + 1]) || list4[k].distinctIndexesInternal.Contains(triangles[j + 2]))
							{
								num = k;
								break;
							}
						}
					}
					if (num > -1 && preserveFaces)
					{
						int num2 = list4[num].indexesInternal.Length;
						int[] array = new int[num2 + 3];
						Array.Copy(list4[num].indexesInternal, 0, array, 0, num2);
						array[num2] = triangles[j];
						array[num2 + 1] = triangles[j + 1];
						array[num2 + 2] = triangles[j + 2];
						list4[num].indexesInternal = array;
					}
					else
					{
						int[] triangles2;
						if (preserveFaces)
						{
							triangles2 = new int[]
							{
								triangles[j],
								triangles[j + 1],
								triangles[j + 2]
							};
						}
						else
						{
							list.Add(meshChannel[triangles[j]]);
							list.Add(meshChannel[triangles[j + 1]]);
							list.Add(meshChannel[triangles[j + 2]]);
							list2.Add((meshChannel2 != null) ? meshChannel2[triangles[j]] : Color.white);
							list2.Add((meshChannel2 != null) ? meshChannel2[triangles[j + 1]] : Color.white);
							list2.Add((meshChannel2 != null) ? meshChannel2[triangles[j + 2]] : Color.white);
							list3.Add(meshChannel3[triangles[j]]);
							list3.Add(meshChannel3[triangles[j + 1]]);
							list3.Add(meshChannel3[triangles[j + 2]]);
							triangles2 = new int[]
							{
								j,
								j + 1,
								j + 2
							};
						}
						list4.Add(new Face(triangles2, i, AutoUnwrapSettings.tile, 0, -1, -1, true));
					}
				}
			}
			GameObject gameObject = Object.Instantiate<GameObject>(t.gameObject);
			gameObject.GetComponent<MeshFilter>().sharedMesh = null;
			ProBuilderMesh proBuilderMesh = gameObject.AddComponent<ProBuilderMesh>();
			proBuilderMesh.RebuildWithPositionsAndFaces(list.ToArray(), list4.ToArray());
			proBuilderMesh.colorsInternal = list2.ToArray();
			proBuilderMesh.textures = list3;
			proBuilderMesh.gameObject.name = t.name;
			gameObject.transform.position = t.position;
			gameObject.transform.localRotation = t.localRotation;
			gameObject.transform.localScale = t.localScale;
			proBuilderMesh.CenterPivot(null);
			return proBuilderMesh;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000317D4 File Offset: 0x0002F9D4
		public static bool ResetPbObjectWithMeshFilter(ProBuilderMesh pb, bool preserveFaces)
		{
			MeshFilter component = pb.gameObject.GetComponent<MeshFilter>();
			if (component == null || component.sharedMesh == null)
			{
				Log.Error(pb.name + " does not have a mesh or Mesh Filter component.");
				return false;
			}
			Mesh sharedMesh = component.sharedMesh;
			int vertexCount = sharedMesh.vertexCount;
			Vector3[] meshChannel = MeshUtility.GetMeshChannel<Vector3[]>(pb.gameObject, (Mesh x) => x.vertices);
			Color[] meshChannel2 = MeshUtility.GetMeshChannel<Color[]>(pb.gameObject, (Mesh x) => x.colors);
			Vector2[] meshChannel3 = MeshUtility.GetMeshChannel<Vector2[]>(pb.gameObject, (Mesh x) => x.uv);
			List<Vector3> list = preserveFaces ? new List<Vector3>(sharedMesh.vertices) : new List<Vector3>();
			List<Color> list2 = preserveFaces ? new List<Color>(sharedMesh.colors) : new List<Color>();
			List<Vector2> list3 = preserveFaces ? new List<Vector2>(sharedMesh.uv) : new List<Vector2>();
			List<Face> list4 = new List<Face>();
			MeshRenderer meshRenderer = pb.gameObject.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = pb.gameObject.AddComponent<MeshRenderer>();
			}
			int num = meshRenderer.sharedMaterials.Length;
			for (int i = 0; i < sharedMesh.subMeshCount; i++)
			{
				int[] triangles = sharedMesh.GetTriangles(i);
				for (int j = 0; j < triangles.Length; j += 3)
				{
					int num2 = -1;
					if (preserveFaces)
					{
						for (int k = 0; k < list4.Count; k++)
						{
							if (list4[k].distinctIndexesInternal.Contains(triangles[j]) || list4[k].distinctIndexesInternal.Contains(triangles[j + 1]) || list4[k].distinctIndexesInternal.Contains(triangles[j + 2]))
							{
								num2 = k;
								break;
							}
						}
					}
					if (num2 > -1 && preserveFaces)
					{
						int num3 = list4[num2].indexesInternal.Length;
						int[] array = new int[num3 + 3];
						Array.Copy(list4[num2].indexesInternal, 0, array, 0, num3);
						array[num3] = triangles[j];
						array[num3 + 1] = triangles[j + 1];
						array[num3 + 2] = triangles[j + 2];
						list4[num2].indexesInternal = array;
					}
					else
					{
						int[] triangles2;
						if (preserveFaces)
						{
							triangles2 = new int[]
							{
								triangles[j],
								triangles[j + 1],
								triangles[j + 2]
							};
						}
						else
						{
							list.Add(meshChannel[triangles[j]]);
							list.Add(meshChannel[triangles[j + 1]]);
							list.Add(meshChannel[triangles[j + 2]]);
							list2.Add((meshChannel2 != null && meshChannel2.Length == vertexCount) ? meshChannel2[triangles[j]] : Color.white);
							list2.Add((meshChannel2 != null && meshChannel2.Length == vertexCount) ? meshChannel2[triangles[j + 1]] : Color.white);
							list2.Add((meshChannel2 != null && meshChannel2.Length == vertexCount) ? meshChannel2[triangles[j + 2]] : Color.white);
							list3.Add(meshChannel3[triangles[j]]);
							list3.Add(meshChannel3[triangles[j + 1]]);
							list3.Add(meshChannel3[triangles[j + 2]]);
							triangles2 = new int[]
							{
								j,
								j + 1,
								j + 2
							};
						}
						list4.Add(new Face(triangles2, Math.Clamp(i, 0, num - 1), AutoUnwrapSettings.tile, 0, -1, -1, true));
					}
				}
			}
			pb.positionsInternal = list.ToArray();
			pb.texturesInternal = list3.ToArray();
			pb.facesInternal = list4.ToArray();
			pb.sharedVerticesInternal = SharedVertex.GetSharedVerticesWithPositions(list.ToArray());
			pb.colorsInternal = list2.ToArray();
			return true;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		internal static void FilterUnusedSubmeshIndexes(ProBuilderMesh mesh)
		{
			Material[] sharedMaterials = mesh.renderer.sharedMaterials;
			int num = sharedMaterials.Length;
			bool[] array = new bool[num];
			foreach (Face face in mesh.facesInternal)
			{
				array[Math.Clamp(face.submeshIndex, 0, num - 1)] = true;
			}
			IEnumerable<int> enumerable = array.AllIndexesOf((bool x) => !x);
			if (enumerable.Any<int>())
			{
				foreach (Face face2 in mesh.facesInternal)
				{
					int submeshIndex = face2.submeshIndex;
					foreach (int num2 in enumerable)
					{
						if (submeshIndex > num2)
						{
							Face face3 = face2;
							int submeshIndex2 = face3.submeshIndex;
							face3.submeshIndex = submeshIndex2 - 1;
						}
					}
				}
				mesh.renderer.sharedMaterials = sharedMaterials.RemoveAt(enumerable);
			}
		}

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005BB RID: 1467 RVA: 0x000364B8 File Offset: 0x000346B8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x000364C4 File Offset: 0x000346C4
			public <>c()
			{
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x000364CC File Offset: 0x000346CC
			internal Vector3[] <CreateMeshWithTransform>b__1_0(Mesh x)
			{
				return x.vertices;
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x000364D4 File Offset: 0x000346D4
			internal Color[] <CreateMeshWithTransform>b__1_1(Mesh x)
			{
				return x.colors;
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x000364DC File Offset: 0x000346DC
			internal Vector2[] <CreateMeshWithTransform>b__1_2(Mesh x)
			{
				return x.uv;
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x000364E4 File Offset: 0x000346E4
			internal Vector3[] <ResetPbObjectWithMeshFilter>b__2_0(Mesh x)
			{
				return x.vertices;
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x000364EC File Offset: 0x000346EC
			internal Color[] <ResetPbObjectWithMeshFilter>b__2_1(Mesh x)
			{
				return x.colors;
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x000364F4 File Offset: 0x000346F4
			internal Vector2[] <ResetPbObjectWithMeshFilter>b__2_2(Mesh x)
			{
				return x.uv;
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x000364FC File Offset: 0x000346FC
			internal bool <FilterUnusedSubmeshIndexes>b__3_0(bool x)
			{
				return !x;
			}

			// Token: 0x0400030A RID: 778
			public static readonly InternalMeshUtility.<>c <>9 = new InternalMeshUtility.<>c();

			// Token: 0x0400030B RID: 779
			public static Func<Mesh, Vector3[]> <>9__1_0;

			// Token: 0x0400030C RID: 780
			public static Func<Mesh, Color[]> <>9__1_1;

			// Token: 0x0400030D RID: 781
			public static Func<Mesh, Vector2[]> <>9__1_2;

			// Token: 0x0400030E RID: 782
			public static Func<Mesh, Vector3[]> <>9__2_0;

			// Token: 0x0400030F RID: 783
			public static Func<Mesh, Color[]> <>9__2_1;

			// Token: 0x04000310 RID: 784
			public static Func<Mesh, Vector2[]> <>9__2_2;

			// Token: 0x04000311 RID: 785
			public static Func<bool, bool> <>9__3_0;
		}
	}
}
