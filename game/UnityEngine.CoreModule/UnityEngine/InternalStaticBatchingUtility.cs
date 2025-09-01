using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000241 RID: 577
	internal class InternalStaticBatchingUtility
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00028383 File Offset: 0x00026583
		public static void CombineRoot(GameObject staticBatchRoot, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot, false, false, sorter);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00028390 File Offset: 0x00026590
		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			GameObject[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				GameObject gameObject = array2[i];
				bool flag = staticBatchRoot != null;
				if (!flag)
				{
					goto IL_53;
				}
				bool flag2 = !gameObject.transform.IsChildOf(staticBatchRoot.transform);
				if (!flag2)
				{
					goto IL_53;
				}
				IL_75:
				i++;
				continue;
				IL_53:
				bool flag3 = combineOnlyStatic && !gameObject.isStaticBatchable;
				if (flag3)
				{
					goto IL_75;
				}
				list.Add(gameObject);
				goto IL_75;
			}
			array = list.ToArray();
			InternalStaticBatchingUtility.CombineGameObjects(array, staticBatchRoot, isEditorPostprocessScene, sorter);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00028430 File Offset: 0x00026630
		private static uint GetMeshFormatHash(Mesh mesh)
		{
			bool flag = mesh == null;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				uint num = 1U;
				int vertexAttributeCount = mesh.vertexAttributeCount;
				for (int i = 0; i < vertexAttributeCount; i++)
				{
					VertexAttributeDescriptor vertexAttribute = mesh.GetVertexAttribute(i);
					uint num2 = (uint)(vertexAttribute.attribute | (VertexAttribute)((int)vertexAttribute.format << 4) | (VertexAttribute)((uint)vertexAttribute.dimension << 8));
					num = num * 2654435761U + num2;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000284A8 File Offset: 0x000266A8
		private static GameObject[] SortGameObjectsForStaticBatching(GameObject[] gos, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			gos = (from g in gos
			orderby InternalStaticBatchingUtility.StaticBatcherGOSorter.GetScaleFlip(g)
			select g).ThenBy(delegate(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetMaterialId(renderer);
			}).ThenBy(delegate(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetLightmapIndex(renderer);
			}).ThenBy(delegate(GameObject g)
			{
				Mesh mesh = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetMesh(g);
				return InternalStaticBatchingUtility.GetMeshFormatHash(mesh);
			}).ThenBy(delegate(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return sorter.GetRendererId(renderer);
			}).ToArray<GameObject>();
			return gos;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0002854C File Offset: 0x0002674C
		public static void CombineGameObjects(GameObject[] gos, GameObject staticBatchRoot, bool isEditorPostprocessScene, InternalStaticBatchingUtility.StaticBatcherGOSorter sorter)
		{
			Matrix4x4 lhs = Matrix4x4.identity;
			Transform staticBatchRootTransform = null;
			bool flag = staticBatchRoot;
			if (flag)
			{
				lhs = staticBatchRoot.transform.worldToLocalMatrix;
				staticBatchRootTransform = staticBatchRoot.transform;
			}
			int batchIndex = 0;
			int num = 0;
			List<MeshSubsetCombineUtility.MeshContainer> list = new List<MeshSubsetCombineUtility.MeshContainer>();
			using (StaticBatchingUtility.s_SortMarker.Auto())
			{
				gos = InternalStaticBatchingUtility.SortGameObjectsForStaticBatching(gos, sorter ?? new InternalStaticBatchingUtility.StaticBatcherGOSorter());
			}
			uint num2 = 0U;
			bool flag2 = false;
			foreach (GameObject gameObject in gos)
			{
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				bool flag3 = meshFilter == null;
				if (!flag3)
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					bool flag4 = sharedMesh == null || (!isEditorPostprocessScene && !sharedMesh.canAccess);
					if (!flag4)
					{
						bool flag5 = !StaticBatchingHelper.IsMeshBatchable(sharedMesh);
						if (!flag5)
						{
							Renderer component = meshFilter.GetComponent<Renderer>();
							bool flag6 = component == null || !component.enabled;
							if (!flag6)
							{
								bool flag7 = component.staticBatchIndex != 0;
								if (!flag7)
								{
									Material[] array2 = component.sharedMaterials;
									bool flag8 = array2.Any((Material m) => m != null && m.shader != null && m.shader.disableBatching > DisableBatchingType.False);
									if (!flag8)
									{
										int vertexCount = sharedMesh.vertexCount;
										bool flag9 = vertexCount == 0;
										if (!flag9)
										{
											MeshRenderer meshRenderer = component as MeshRenderer;
											bool flag10 = meshRenderer != null;
											if (flag10)
											{
												bool flag11 = meshRenderer.additionalVertexStreams != null;
												if (flag11)
												{
													bool flag12 = vertexCount != meshRenderer.additionalVertexStreams.vertexCount;
													if (flag12)
													{
														goto IL_485;
													}
												}
												bool flag13 = meshRenderer.enlightenVertexStream != null;
												if (flag13)
												{
													bool flag14 = vertexCount != meshRenderer.enlightenVertexStream.vertexCount;
													if (flag14)
													{
														goto IL_485;
													}
												}
											}
											uint meshFormatHash = InternalStaticBatchingUtility.GetMeshFormatHash(sharedMesh);
											bool scaleFlip = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetScaleFlip(gameObject);
											bool flag15 = num + vertexCount > 64000 || meshFormatHash != num2 || scaleFlip != flag2;
											if (flag15)
											{
												InternalStaticBatchingUtility.MakeBatch(list, staticBatchRootTransform, batchIndex++);
												list.Clear();
												num = 0;
												flag2 = scaleFlip;
											}
											num2 = meshFormatHash;
											MeshSubsetCombineUtility.MeshInstance meshInstance = default(MeshSubsetCombineUtility.MeshInstance);
											meshInstance.meshInstanceID = sharedMesh.GetInstanceID();
											meshInstance.rendererInstanceID = component.GetInstanceID();
											bool flag16 = meshRenderer != null;
											if (flag16)
											{
												bool flag17 = meshRenderer.additionalVertexStreams != null;
												if (flag17)
												{
													meshInstance.additionalVertexStreamsMeshInstanceID = meshRenderer.additionalVertexStreams.GetInstanceID();
												}
												bool flag18 = meshRenderer.enlightenVertexStream != null;
												if (flag18)
												{
													meshInstance.enlightenVertexStreamMeshInstanceID = meshRenderer.enlightenVertexStream.GetInstanceID();
												}
											}
											meshInstance.transform = lhs * meshFilter.transform.localToWorldMatrix;
											meshInstance.lightmapScaleOffset = component.lightmapScaleOffset;
											meshInstance.realtimeLightmapScaleOffset = component.realtimeLightmapScaleOffset;
											MeshSubsetCombineUtility.MeshContainer meshContainer = new MeshSubsetCombineUtility.MeshContainer
											{
												gameObject = gameObject,
												instance = meshInstance,
												subMeshInstances = new List<MeshSubsetCombineUtility.SubMeshInstance>()
											};
											list.Add(meshContainer);
											bool flag19 = array2.Length > sharedMesh.subMeshCount;
											if (flag19)
											{
												Debug.LogWarning(string.Concat(new string[]
												{
													"Mesh '",
													sharedMesh.name,
													"' has more materials (",
													array2.Length.ToString(),
													") than subsets (",
													sharedMesh.subMeshCount.ToString(),
													")"
												}), component);
												Material[] array3 = new Material[sharedMesh.subMeshCount];
												for (int j = 0; j < sharedMesh.subMeshCount; j++)
												{
													array3[j] = component.sharedMaterials[j];
												}
												component.sharedMaterials = array3;
												array2 = array3;
											}
											for (int k = 0; k < Math.Min(array2.Length, sharedMesh.subMeshCount); k++)
											{
												MeshSubsetCombineUtility.SubMeshInstance item = default(MeshSubsetCombineUtility.SubMeshInstance);
												item.meshInstanceID = meshFilter.sharedMesh.GetInstanceID();
												item.vertexOffset = num;
												item.subMeshIndex = k;
												item.gameObjectInstanceID = gameObject.GetInstanceID();
												item.transform = meshInstance.transform;
												meshContainer.subMeshInstances.Add(item);
											}
											num += sharedMesh.vertexCount;
										}
									}
								}
							}
						}
					}
				}
				IL_485:;
			}
			InternalStaticBatchingUtility.MakeBatch(list, staticBatchRootTransform, batchIndex);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00028A0C File Offset: 0x00026C0C
		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshContainer> meshes, Transform staticBatchRootTransform, int batchIndex)
		{
			bool flag = meshes.Count < 2;
			if (!flag)
			{
				using (StaticBatchingUtility.s_MakeBatchMarker.Auto())
				{
					List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
					List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
					foreach (MeshSubsetCombineUtility.MeshContainer meshContainer in meshes)
					{
						list.Add(meshContainer.instance);
						list2.AddRange(meshContainer.subMeshInstances);
					}
					string text = "Combined Mesh";
					text = text + " (root: " + ((staticBatchRootTransform != null) ? staticBatchRootTransform.name : "scene") + ")";
					bool flag2 = batchIndex > 0;
					if (flag2)
					{
						text = text + " " + (batchIndex + 1).ToString();
					}
					Mesh mesh = StaticBatchingHelper.InternalCombineVertices(list.ToArray(), text);
					StaticBatchingHelper.InternalCombineIndices(list2.ToArray(), mesh);
					int num = 0;
					foreach (MeshSubsetCombineUtility.MeshContainer meshContainer2 in meshes)
					{
						MeshFilter meshFilter = (MeshFilter)meshContainer2.gameObject.GetComponent(typeof(MeshFilter));
						meshFilter.sharedMesh = mesh;
						int count = meshContainer2.subMeshInstances.Count;
						Renderer component = meshContainer2.gameObject.GetComponent<Renderer>();
						component.SetStaticBatchInfo(num, count);
						component.staticBatchRootTransform = staticBatchRootTransform;
						component.enabled = false;
						component.enabled = true;
						MeshRenderer meshRenderer = component as MeshRenderer;
						bool flag3 = meshRenderer != null;
						if (flag3)
						{
							meshRenderer.additionalVertexStreams = null;
							meshRenderer.enlightenVertexStream = null;
						}
						num += count;
					}
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00002072 File Offset: 0x00000272
		public InternalStaticBatchingUtility()
		{
		}

		// Token: 0x04000850 RID: 2128
		private const int MaxVerticesInBatch = 64000;

		// Token: 0x04000851 RID: 2129
		private const string CombinedMeshPrefix = "Combined Mesh";

		// Token: 0x02000242 RID: 578
		public class StaticBatcherGOSorter
		{
			// Token: 0x060018B6 RID: 6326 RVA: 0x00028C34 File Offset: 0x00026E34
			public virtual long GetMaterialId(Renderer renderer)
			{
				bool flag = renderer == null || renderer.sharedMaterial == null;
				long result;
				if (flag)
				{
					result = 0L;
				}
				else
				{
					result = (long)renderer.sharedMaterial.GetInstanceID();
				}
				return result;
			}

			// Token: 0x060018B7 RID: 6327 RVA: 0x00028C74 File Offset: 0x00026E74
			public int GetLightmapIndex(Renderer renderer)
			{
				bool flag = renderer == null;
				int result;
				if (flag)
				{
					result = -1;
				}
				else
				{
					result = renderer.lightmapIndex;
				}
				return result;
			}

			// Token: 0x060018B8 RID: 6328 RVA: 0x00028C9C File Offset: 0x00026E9C
			public static Renderer GetRenderer(GameObject go)
			{
				bool flag = go == null;
				Renderer result;
				if (flag)
				{
					result = null;
				}
				else
				{
					MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
					bool flag2 = meshFilter == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = meshFilter.GetComponent<Renderer>();
					}
				}
				return result;
			}

			// Token: 0x060018B9 RID: 6329 RVA: 0x00028CE8 File Offset: 0x00026EE8
			public static Mesh GetMesh(GameObject go)
			{
				bool flag = go == null;
				Mesh result;
				if (flag)
				{
					result = null;
				}
				else
				{
					MeshFilter component = go.GetComponent<MeshFilter>();
					bool flag2 = component == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = component.sharedMesh;
					}
				}
				return result;
			}

			// Token: 0x060018BA RID: 6330 RVA: 0x00028D28 File Offset: 0x00026F28
			public virtual long GetRendererId(Renderer renderer)
			{
				bool flag = renderer == null;
				long result;
				if (flag)
				{
					result = -1L;
				}
				else
				{
					result = (long)renderer.GetInstanceID();
				}
				return result;
			}

			// Token: 0x060018BB RID: 6331 RVA: 0x00028D54 File Offset: 0x00026F54
			public static bool GetScaleFlip(GameObject go)
			{
				Transform transform = go.transform;
				float determinant = transform.localToWorldMatrix.determinant;
				return determinant < 0f;
			}

			// Token: 0x060018BC RID: 6332 RVA: 0x00002072 File Offset: 0x00000272
			public StaticBatcherGOSorter()
			{
			}
		}

		// Token: 0x02000243 RID: 579
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060018BD RID: 6333 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060018BE RID: 6334 RVA: 0x00028D84 File Offset: 0x00026F84
			internal long <SortGameObjectsForStaticBatching>b__1(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return this.sorter.GetMaterialId(renderer);
			}

			// Token: 0x060018BF RID: 6335 RVA: 0x00028DAC File Offset: 0x00026FAC
			internal int <SortGameObjectsForStaticBatching>b__2(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return this.sorter.GetLightmapIndex(renderer);
			}

			// Token: 0x060018C0 RID: 6336 RVA: 0x00028DD4 File Offset: 0x00026FD4
			internal long <SortGameObjectsForStaticBatching>b__4(GameObject g)
			{
				Renderer renderer = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetRenderer(g);
				return this.sorter.GetRendererId(renderer);
			}

			// Token: 0x04000852 RID: 2130
			public InternalStaticBatchingUtility.StaticBatcherGOSorter sorter;
		}

		// Token: 0x02000244 RID: 580
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060018C1 RID: 6337 RVA: 0x00028DF9 File Offset: 0x00026FF9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x060018C3 RID: 6339 RVA: 0x00028E08 File Offset: 0x00027008
			internal bool <SortGameObjectsForStaticBatching>b__5_0(GameObject g)
			{
				return InternalStaticBatchingUtility.StaticBatcherGOSorter.GetScaleFlip(g);
			}

			// Token: 0x060018C4 RID: 6340 RVA: 0x00028E20 File Offset: 0x00027020
			internal uint <SortGameObjectsForStaticBatching>b__5_3(GameObject g)
			{
				Mesh mesh = InternalStaticBatchingUtility.StaticBatcherGOSorter.GetMesh(g);
				return InternalStaticBatchingUtility.GetMeshFormatHash(mesh);
			}

			// Token: 0x060018C5 RID: 6341 RVA: 0x00028E3F File Offset: 0x0002703F
			internal bool <CombineGameObjects>b__6_0(Material m)
			{
				return m != null && m.shader != null && m.shader.disableBatching > DisableBatchingType.False;
			}

			// Token: 0x04000853 RID: 2131
			public static readonly InternalStaticBatchingUtility.<>c <>9 = new InternalStaticBatchingUtility.<>c();

			// Token: 0x04000854 RID: 2132
			public static Func<GameObject, bool> <>9__5_0;

			// Token: 0x04000855 RID: 2133
			public static Func<GameObject, uint> <>9__5_3;

			// Token: 0x04000856 RID: 2134
			public static Func<Material, bool> <>9__6_0;
		}
	}
}
