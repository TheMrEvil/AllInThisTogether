using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x0200007A RID: 122
	public static class CombineMeshes
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0002CA73 File Offset: 0x0002AC73
		[Obsolete("Combine(IEnumerable<ProBuilderMesh> meshes) is deprecated. Plase use Combine(IEnumerable<ProBuilderMesh> meshes, ProBuilderMesh meshTarget).")]
		public static List<ProBuilderMesh> Combine(IEnumerable<ProBuilderMesh> meshes)
		{
			return CombineMeshes.CombineToNewMeshes(meshes);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002CA7C File Offset: 0x0002AC7C
		public static List<ProBuilderMesh> Combine(IEnumerable<ProBuilderMesh> meshes, ProBuilderMesh meshTarget)
		{
			if (meshes == null)
			{
				throw new ArgumentNullException("meshes");
			}
			if (meshTarget == null)
			{
				throw new ArgumentNullException("meshTarget");
			}
			if (!meshes.Any<ProBuilderMesh>() || meshes.Count<ProBuilderMesh>() < 2)
			{
				return null;
			}
			if (!meshes.Contains(meshTarget))
			{
				return null;
			}
			List<Vertex> vertices = new List<Vertex>(meshTarget.GetVertices(null));
			List<Face> faces = new List<Face>(meshTarget.facesInternal);
			List<SharedVertex> sharedVertices = new List<SharedVertex>(meshTarget.sharedVertices);
			List<SharedVertex> list = new List<SharedVertex>(meshTarget.sharedTextures);
			int vertexCount = meshTarget.vertexCount;
			List<Material> list2 = new List<Material>(meshTarget.renderer.sharedMaterials);
			Transform transform = meshTarget.transform;
			List<ProBuilderMesh> list3 = new List<ProBuilderMesh>();
			List<ProBuilderMesh> list4 = new List<ProBuilderMesh>();
			int num = vertexCount;
			foreach (ProBuilderMesh proBuilderMesh in meshes)
			{
				if (proBuilderMesh != meshTarget)
				{
					if ((long)(num + proBuilderMesh.vertexCount) < 65535L)
					{
						num += proBuilderMesh.vertexCount;
						list3.Add(proBuilderMesh);
					}
					else
					{
						list4.Add(proBuilderMesh);
					}
				}
			}
			List<Face> facesToConvert = new List<Face>();
			CombineMeshes.AccumulateMeshesInfo(list3, vertexCount, ref vertices, ref faces, ref facesToConvert, ref sharedVertices, ref list, ref list2, transform);
			meshTarget.SetVertices(vertices, false);
			meshTarget.faces = faces;
			meshTarget.sharedVertices = sharedVertices;
			meshTarget.sharedTextures = ((list != null) ? list.ToArray() : null);
			meshTarget.renderer.sharedMaterials = list2.ToArray();
			meshTarget.ToMesh(MeshTopology.Triangles);
			meshTarget.Refresh(RefreshMask.All);
			UvUnwrapping.SetAutoAndAlignUnwrapParamsToUVs(meshTarget, facesToConvert);
			int num2;
			MeshValidation.EnsureMeshIsValid(meshTarget, out num2);
			List<ProBuilderMesh> list5 = new List<ProBuilderMesh>
			{
				meshTarget
			};
			if (list4.Count > 1)
			{
				using (List<ProBuilderMesh>.Enumerator enumerator2 = CombineMeshes.CombineToNewMeshes(list4).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ProBuilderMesh proBuilderMesh2 = enumerator2.Current;
						MeshValidation.EnsureMeshIsValid(proBuilderMesh2, out num2);
						list5.Add(proBuilderMesh2);
					}
					return list5;
				}
			}
			if (list4.Count == 1)
			{
				list5.Add(list4[0]);
			}
			return list5;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		private static List<ProBuilderMesh> CombineToNewMeshes(IEnumerable<ProBuilderMesh> meshes)
		{
			if (meshes == null)
			{
				throw new ArgumentNullException("meshes");
			}
			if (!meshes.Any<ProBuilderMesh>() || meshes.Count<ProBuilderMesh>() < 2)
			{
				return null;
			}
			List<Vertex> vertices = new List<Vertex>();
			List<Face> faces = new List<Face>();
			List<Face> facesToConvert = new List<Face>();
			List<SharedVertex> sharedVertices = new List<SharedVertex>();
			List<SharedVertex> sharedTextures = new List<SharedVertex>();
			int offset = 0;
			List<Material> list = new List<Material>();
			CombineMeshes.AccumulateMeshesInfo(meshes, offset, ref vertices, ref faces, ref facesToConvert, ref sharedVertices, ref sharedTextures, ref list, null);
			List<ProBuilderMesh> list2 = CombineMeshes.SplitByMaxVertexCount(vertices, faces, sharedVertices, sharedTextures, 65535U);
			Vector3 position = meshes.LastOrDefault<ProBuilderMesh>().transform.position;
			foreach (ProBuilderMesh proBuilderMesh in list2)
			{
				proBuilderMesh.renderer.sharedMaterials = list.ToArray();
				InternalMeshUtility.FilterUnusedSubmeshIndexes(proBuilderMesh);
				proBuilderMesh.SetPivot(position);
				UvUnwrapping.SetAutoAndAlignUnwrapParamsToUVs(proBuilderMesh, facesToConvert);
			}
			return list2;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0002CD98 File Offset: 0x0002AF98
		private static void AccumulateMeshesInfo(IEnumerable<ProBuilderMesh> meshes, int offset, ref List<Vertex> vertices, ref List<Face> faces, ref List<Face> autoUvFaces, ref List<SharedVertex> sharedVertices, ref List<SharedVertex> sharedTextures, ref List<Material> materialMap, Transform targetTransform = null)
		{
			foreach (ProBuilderMesh proBuilderMesh in meshes)
			{
				int vertexCount = proBuilderMesh.vertexCount;
				Transform transform = proBuilderMesh.transform;
				Vertex[] vertices2 = proBuilderMesh.GetVertices(null);
				Face[] facesInternal = proBuilderMesh.facesInternal;
				IList<SharedVertex> sharedVertices2 = proBuilderMesh.sharedVertices;
				SharedVertex[] sharedTextures2 = proBuilderMesh.sharedTextures;
				Material[] sharedMaterials = proBuilderMesh.renderer.sharedMaterials;
				int num = sharedMaterials.Length;
				for (int i = 0; i < vertexCount; i++)
				{
					Vertex vertex = transform.TransformVertex(vertices2[i]);
					if (targetTransform != null)
					{
						vertices.Add(targetTransform.InverseTransformVertex(vertex));
					}
					else
					{
						vertices.Add(vertex);
					}
				}
				foreach (Face face in facesInternal)
				{
					Face face2 = new Face(face);
					face2.ShiftIndexes(offset);
					if (!face2.manualUV && !face2.uv.useWorldSpace)
					{
						face2.manualUV = true;
						autoUvFaces.Add(face2);
					}
					Material material = (num > 0) ? sharedMaterials[Math.Clamp(face.submeshIndex, 0, num - 1)] : null;
					int num2 = materialMap.IndexOf(material);
					if (num2 > -1)
					{
						face2.submeshIndex = num2;
					}
					else if (material == null)
					{
						face2.submeshIndex = 0;
					}
					else
					{
						face2.submeshIndex = materialMap.Count;
						materialMap.Add(material);
					}
					faces.Add(face2);
				}
				foreach (SharedVertex sharedVertex in sharedVertices2)
				{
					SharedVertex sharedVertex2 = new SharedVertex(sharedVertex);
					sharedVertex2.ShiftIndexes(offset);
					sharedVertices.Add(sharedVertex2);
				}
				SharedVertex[] array2 = sharedTextures2;
				for (int j = 0; j < array2.Length; j++)
				{
					SharedVertex sharedVertex3 = new SharedVertex(array2[j]);
					sharedVertex3.ShiftIndexes(offset);
					sharedTextures.Add(sharedVertex3);
				}
				offset += vertexCount;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		private static ProBuilderMesh CreateMeshFromSplit(List<Vertex> vertices, List<Face> faces, Dictionary<int, int> sharedVertexLookup, Dictionary<int, int> sharedTextureLookup, Dictionary<int, int> remap, Material[] materials)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			foreach (Face face in faces)
			{
				int i = 0;
				int num = face.indexesInternal.Length;
				while (i < num)
				{
					face.indexesInternal[i] = remap[face.indexesInternal[i]];
					i++;
				}
				face.InvalidateCache();
			}
			foreach (KeyValuePair<int, int> keyValuePair in remap)
			{
				int value;
				if (sharedVertexLookup.TryGetValue(keyValuePair.Key, out value))
				{
					dictionary.Add(keyValuePair.Value, value);
				}
				if (sharedTextureLookup.TryGetValue(keyValuePair.Key, out value))
				{
					dictionary2.Add(keyValuePair.Value, value);
				}
			}
			return ProBuilderMesh.Create(vertices, faces, SharedVertex.ToSharedVertices(dictionary), (dictionary2.Count > 0) ? SharedVertex.ToSharedVertices(dictionary2) : null, materials);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0002D0FC File Offset: 0x0002B2FC
		internal static List<ProBuilderMesh> SplitByMaxVertexCount(IList<Vertex> vertices, IList<Face> faces, IList<SharedVertex> sharedVertices, IList<SharedVertex> sharedTextures, uint maxVertexCount = 65535U)
		{
			uint count = (uint)vertices.Count;
			int num = (int)Math.Max(1U, count / maxVertexCount);
			int num2 = faces.Max((Face x) => x.submeshIndex) + 1;
			if (num < 2)
			{
				return new List<ProBuilderMesh>
				{
					ProBuilderMesh.Create(vertices, faces, sharedVertices, sharedTextures, new Material[num2])
				};
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			SharedVertex.GetSharedVertexLookup(sharedVertices, dictionary);
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			SharedVertex.GetSharedVertexLookup(sharedTextures, dictionary2);
			List<ProBuilderMesh> list = new List<ProBuilderMesh>();
			List<Vertex> list2 = new List<Vertex>();
			List<Face> list3 = new List<Face>();
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
			foreach (Face face in faces)
			{
				if ((long)(list2.Count + face.distinctIndexes.Count) > (long)((ulong)maxVertexCount))
				{
					list.Add(CombineMeshes.CreateMeshFromSplit(list2, list3, dictionary, dictionary2, dictionary3, new Material[num2]));
					list2.Clear();
					list3.Clear();
					dictionary3.Clear();
				}
				foreach (int num3 in face.distinctIndexes)
				{
					list2.Add(vertices[num3]);
					dictionary3.Add(num3, list2.Count - 1);
				}
				list3.Add(face);
			}
			if (list2.Count > 0)
			{
				list.Add(CombineMeshes.CreateMeshFromSplit(list2, list3, dictionary, dictionary2, dictionary3, new Material[num2]));
			}
			return list;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000580 RID: 1408 RVA: 0x00036125 File Offset: 0x00034325
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00036131 File Offset: 0x00034331
			public <>c()
			{
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00036139 File Offset: 0x00034339
			internal int <SplitByMaxVertexCount>b__5_0(Face x)
			{
				return x.submeshIndex;
			}

			// Token: 0x040002DB RID: 731
			public static readonly CombineMeshes.<>c <>9 = new CombineMeshes.<>c();

			// Token: 0x040002DC RID: 732
			public static Func<Face, int> <>9__5_0;
		}
	}
}
