using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace MagicaCloth2
{
	// Token: 0x020000F9 RID: 249
	public class VirtualMesh : IDisposable
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public void ImportFrom(RenderSetupData rsetup)
		{
			try
			{
				if (rsetup == null)
				{
					this.result.SetError(Define.Result.VirtualMesh_InvalidSetup);
					throw new NullReferenceException();
				}
				if (rsetup.IsFaild())
				{
					this.result.SetError(Define.Result.VirtualMesh_InvalidSetup);
					throw new InvalidOperationException();
				}
				if (this.transformData == null)
				{
					this.transformData = new TransformData(rsetup.TransformCount);
				}
				int[] array = this.transformData.AddTransformRange(rsetup.transformList, rsetup.transformIdList, rsetup.transformParentIdList, rsetup.rootTransformIdList, rsetup.transformLocalPositins, rsetup.transformLocalRotations, rsetup.transformPositions, rsetup.transformRotations, rsetup.transformScales, rsetup.transformInverseRotations);
				this.centerTransformIndex = array[rsetup.renderTransformIndex];
				this.initLocalToWorld = rsetup.initRenderLocalToWorld;
				this.initWorldToLocal = rsetup.initRenderWorldtoLocal;
				this.initRotation = rsetup.initRenderRotation;
				this.initInverseRotation = math.inverse(this.initRotation);
				this.initScale = rsetup.initRenderScale;
				if (rsetup.setupType == RenderSetupData.SetupType.Mesh)
				{
					this.meshType = VirtualMesh.MeshType.NormalMesh;
					this.isBoneCloth = false;
					this.ImportMeshType(rsetup, array);
					if (rsetup.isSkinning)
					{
						this.ImportMeshSkinning();
					}
				}
				else
				{
					if (rsetup.setupType != RenderSetupData.SetupType.Bone)
					{
						this.result.SetError(Define.Result.RenderSetup_InvalidType);
						throw new IndexOutOfRangeException();
					}
					this.meshType = VirtualMesh.MeshType.NormalBoneMesh;
					this.isBoneCloth = true;
					this.ImportBoneType(rsetup, array);
				}
				this.boundingBox = new NativeReference<AABB>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
				JobUtility.CalcAABBRun(this.localPositions.GetNativeArray(), this.VertexCount, this.boundingBox);
				if (rsetup.setupType == RenderSetupData.SetupType.Bone && this.TriangleCount > 0)
				{
					JobUtility.CalcUVWithSphereMappingRun(this.localPositions.GetNativeArray(), this.VertexCount, this.boundingBox, this.uv.GetNativeArray());
				}
				this.CalcAverageAndMaxVertexDistanceRun();
			}
			catch (Exception)
			{
				if (this.result.IsNone())
				{
					this.result.SetError(Define.Result.VirtualMesh_ImportError);
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00022CB4 File Offset: 0x00020EB4
		private void ImportMeshType(RenderSetupData rsetup, int[] transformIndices)
		{
			this.skinRootIndex = transformIndices[rsetup.skinRootBoneIndex];
			this.skinBoneTransformIndices.AddRange(transformIndices, rsetup.skinBoneCount);
			this.skinBoneBindPoses.AddRange<Matrix4x4>(rsetup.bindPoseList.ToArray());
			Mesh.MeshData meshData = rsetup.meshDataArray[0];
			int vertexCount = meshData.vertexCount;
			this.localPositions.AddRange(vertexCount);
			this.localNormals.AddRange(vertexCount);
			this.localTangents.AddRange(vertexCount);
			this.uv.AddRange(vertexCount);
			this.boneWeights.AddRange(vertexCount);
			meshData.GetVertices(this.localPositions.GetNativeArray<Vector3>());
			meshData.GetNormals(this.localNormals.GetNativeArray<Vector3>());
			if (meshData.HasVertexAttribute(VertexAttribute.Tangent))
			{
				using (NativeArray<Vector4> nativeArray = new NativeArray<Vector4>(vertexCount, Allocator.TempJob, NativeArrayOptions.ClearMemory))
				{
					meshData.GetTangents(nativeArray);
					this.localTangents.CopyFromWithTypeChangeStride<Vector4>(nativeArray);
					goto IL_117;
				}
			}
			new VirtualMesh.Import_GenerateTangentJob
			{
				localNormals = this.localNormals.GetNativeArray(),
				localTangents = this.localTangents.GetNativeArray()
			}.Run(vertexCount);
			IL_117:
			if (meshData.HasVertexAttribute(VertexAttribute.TexCoord0))
			{
				meshData.GetUVs(0, this.uv.GetNativeArray<Vector2>());
			}
			else
			{
				Debug.LogWarning("[" + this.name + "] UV not found!");
			}
			this.attributes.AddRange(vertexCount);
			this.referenceIndices.AddRange(vertexCount);
			using (NativeArray<int> startBoneWeightIndices = new NativeArray<int>(vertexCount, Allocator.TempJob, NativeArrayOptions.ClearMemory))
			{
				JobUtility.SerialNumberRun(this.referenceIndices.GetNativeArray(), vertexCount);
				if (rsetup.isSkinning)
				{
					new VirtualMesh.Import_BoneWeightJob1
					{
						vcnt = vertexCount,
						bonesPerVertexArray = rsetup.bonesPerVertexArray,
						startBoneWeightIndices = startBoneWeightIndices
					}.Run<VirtualMesh.Import_BoneWeightJob1>();
					new VirtualMesh.Import_BoneWeightJob2
					{
						startBoneWeightIndices = startBoneWeightIndices,
						boneWeightArray = rsetup.boneWeightArray,
						bonesPerVertexArray = rsetup.bonesPerVertexArray,
						boneWeights = this.boneWeights.GetNativeArray()
					}.Run(vertexCount);
				}
				else
				{
					JobUtility.FillRun(this.boneWeights.GetNativeArray(), vertexCount, new VirtualMeshBoneWeight(0, new float4(1f, 0f, 0f, 0f)));
				}
				for (int i = 0; i < meshData.subMeshCount; i++)
				{
					using (NativeArray<int> nativeArray2 = new NativeArray<int>(meshData.GetSubMesh(i).indexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
					{
						meshData.GetIndices(nativeArray2, i, true);
						this.triangles.AddRangeTypeChange<int>(nativeArray2);
					}
				}
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00022FAC File Offset: 0x000211AC
		private void ImportMeshSkinning()
		{
			new VirtualMesh.Import_CalcSkinningJob
			{
				localPositions = this.localPositions.GetNativeArray(),
				localNormals = this.localNormals.GetNativeArray(),
				localTangents = this.localTangents.GetNativeArray(),
				boneWeights = this.boneWeights.GetNativeArray(),
				skinBoneTransformIndices = this.skinBoneTransformIndices.GetNativeArray(),
				bindPoses = this.skinBoneBindPoses.GetNativeArray(),
				transformPositionArray = this.transformData.positionArray.GetNativeArray(),
				transformRotationArray = this.transformData.rotationArray.GetNativeArray(),
				transformScaleArray = this.transformData.scaleArray.GetNativeArray(),
				toM = this.initWorldToLocal
			}.Run(this.VertexCount);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0002308C File Offset: 0x0002128C
		private void ImportBoneType(RenderSetupData rsetup, int[] transformIndices)
		{
			int num = rsetup.TransformCount - 1;
			this.localPositions.AddRange(num);
			this.localNormals.AddRange(num);
			this.localTangents.AddRange(num);
			this.uv.AddRange(num);
			this.boneWeights.AddRange(num);
			this.attributes.AddRange(num);
			this.referenceIndices.AddRange(num);
			this.skinBoneTransformIndices.AddRange(transformIndices, rsetup.skinBoneCount);
			this.skinBoneBindPoses.AddRange(num);
			float4x4 initRenderWorldtoLocal = rsetup.initRenderWorldtoLocal;
			float4x4 initRenderLocalToWorld = rsetup.initRenderLocalToWorld;
			new VirtualMesh.Import_BoneVertexJob
			{
				WtoL = initRenderWorldtoLocal,
				LtoW = initRenderLocalToWorld,
				transformPositions = rsetup.transformPositions,
				transformRotations = rsetup.transformRotations,
				transformScales = rsetup.transformScales,
				localPositions = this.localPositions.GetNativeArray(),
				localNormals = this.localNormals.GetNativeArray(),
				localTangents = this.localTangents.GetNativeArray(),
				boneWeights = this.boneWeights.GetNativeArray(),
				skinBoneBindPoses = this.skinBoneBindPoses.GetNativeArray()
			}.Run(num);
			JobUtility.SerialNumberRun(this.referenceIndices.GetNativeArray(), num);
			if (rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.Line)
			{
				List<int2> list = new List<int2>(num);
				for (int i = 0; i < num; i++)
				{
					int parentTransformIndex = rsetup.GetParentTransformIndex(i, true);
					if (parentTransformIndex >= 0)
					{
						int2 item = DataUtility.PackInt2(parentTransformIndex, i);
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					this.lines = new ExSimpleNativeArray<int2>(list.ToArray());
					return;
				}
			}
			else
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>(num);
				for (int j = 0; j < num; j++)
				{
					if (!dictionary.ContainsKey(rsetup.transformIdList[j]))
					{
						dictionary.Add(rsetup.transformIdList[j], j);
					}
				}
				List<List<FixedList128Bytes<int>>> list2 = new List<List<FixedList128Bytes<int>>>();
				int count = rsetup.rootTransformIdList.Count;
				Stack<int> stack = new Stack<int>(num);
				Stack<int> stack2 = new Stack<int>(num);
				for (int k = 0; k < count; k++)
				{
					List<FixedList128Bytes<int>> list3 = new List<FixedList128Bytes<int>>();
					stack.Clear();
					stack.Push(rsetup.rootTransformIdList[k]);
					stack2.Clear();
					stack2.Push(0);
					while (stack.Count > 0)
					{
						int key = stack.Pop();
						int num2 = stack2.Pop();
						int index = dictionary[key];
						if (num2 >= list3.Count)
						{
							list3.Add(default(FixedList128Bytes<int>));
						}
						FixedList128Bytes<int> value = list3[num2];
						value.Add(index);
						list3[num2] = value;
						FixedList512Bytes<int> fixedList512Bytes = rsetup.transformChildIdList[index];
						if (fixedList512Bytes.Length > 0)
						{
							for (int l = 0; l < fixedList512Bytes.Length; l++)
							{
								stack.Push(fixedList512Bytes[l]);
								stack2.Push(num2 + 1);
							}
						}
					}
					list2.Add(list3);
				}
				FixedList128Bytes<int>[] array = new FixedList128Bytes<int>[num];
				for (int m = 0; m < count; m++)
				{
					List<FixedList128Bytes<int>> list4 = list2[m];
					int count2 = list4.Count;
					for (int n = 0; n < count2; n++)
					{
						FixedList128Bytes<int> fixedList128Bytes = list4[n];
						for (int num3 = 0; num3 < fixedList128Bytes.Length; num3++)
						{
							int num4 = fixedList128Bytes[num3];
							FixedList128Bytes<int> fixedList128Bytes2 = default(FixedList128Bytes<int>);
							int key2 = rsetup.transformParentIdList[num4];
							if (dictionary.ContainsKey(key2))
							{
								int num5 = dictionary[key2];
								fixedList128Bytes2.Add(num5);
							}
							FixedList512Bytes<int> fixedList512Bytes2 = rsetup.transformChildIdList[num4];
							if (fixedList512Bytes2.Length > 0)
							{
								for (int num6 = 0; num6 < fixedList512Bytes2.Length; num6++)
								{
									int num5 = dictionary[fixedList512Bytes2[num6]];
									fixedList128Bytes2.Add(num5);
								}
							}
							if (num3 < fixedList128Bytes.Length - 1)
							{
								int num5 = fixedList128Bytes[num3 + 1];
								fixedList128Bytes2.Add(num5);
							}
							else if (rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.AutomaticMesh)
							{
								float3 x = this.localPositions[num4];
								ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
								for (int num7 = 0; num7 < count; num7++)
								{
									List<FixedList128Bytes<int>> list5 = list2[num7];
									if (list5.Count > n)
									{
										FixedList128Bytes<int> fixedList128Bytes3 = list5[n];
										for (int num8 = 0; num8 < fixedList128Bytes3.Length; num8++)
										{
											int num9 = fixedList128Bytes3[num8];
											if (num9 != num4)
											{
												float3 y = this.localPositions[num9];
												float cost = math.distance(x, y);
												exCostSortedList.Add(cost, num9);
											}
										}
									}
								}
								for (int num10 = 0; num10 < exCostSortedList.Count; num10++)
								{
									if (num10 >= 2)
									{
										break;
									}
									int num5 = exCostSortedList.data[num10];
									fixedList128Bytes2.Add(num5);
								}
							}
							else if (rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialLoopMesh || rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialNonLoopMesh)
							{
								int num11 = (m + 1) % count;
								if (num11 != m)
								{
									List<FixedList128Bytes<int>> list6 = list2[num11];
									if (n < list6.Count)
									{
										FixedList128Bytes<int> fixedList128Bytes4 = list6[n];
										if (num11 < m && rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialLoopMesh)
										{
											int num5 = fixedList128Bytes4[fixedList128Bytes4.Length - 1];
											fixedList128Bytes2.Add(num5);
										}
										else if (num11 > m)
										{
											int num5 = fixedList128Bytes4[0];
											fixedList128Bytes2.Add(num5);
										}
									}
								}
								int num12 = (m + count - 1) % count;
								if (num12 != m && num12 != num11)
								{
									List<FixedList128Bytes<int>> list7 = list2[num12];
									if (n < list7.Count)
									{
										FixedList128Bytes<int> fixedList128Bytes5 = list7[n];
										if (num12 < m)
										{
											int num5 = fixedList128Bytes5[fixedList128Bytes5.Length - 1];
											fixedList128Bytes2.Add(num5);
										}
										else if (num11 > m && rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialLoopMesh)
										{
											int num5 = fixedList128Bytes5[0];
											fixedList128Bytes2.Add(num5);
										}
									}
								}
							}
							array[num4] = fixedList128Bytes2;
						}
					}
				}
				HashSet<int2> hashSet = new HashSet<int2>();
				HashSet<int2> hashSet2 = new HashSet<int2>();
				HashSet<int3> hashSet3 = new HashSet<int3>();
				for (int num13 = 0; num13 < num; num13++)
				{
					FixedList128Bytes<int> fixedList128Bytes6 = array[num13];
					if (fixedList128Bytes6.Length == 0)
					{
						Debug.LogError(string.Format("Connection 0! [{0}]", num13));
					}
					else if (fixedList128Bytes6.Length == 1)
					{
						hashSet.Add(DataUtility.PackInt2(num13, fixedList128Bytes6[0]));
					}
					else
					{
						for (int num14 = 0; num14 < fixedList128Bytes6.Length; num14++)
						{
							int d = fixedList128Bytes6[num14];
							hashSet.Add(DataUtility.PackInt2(num13, d));
						}
						float3 rhs = this.localPositions[num13];
						for (int num15 = 0; num15 < fixedList128Bytes6.Length - 1; num15++)
						{
							int num16 = fixedList128Bytes6[num15];
							float3 @float = this.localPositions[num16] - rhs;
							for (int num17 = num15 + 1; num17 < fixedList128Bytes6.Length; num17++)
							{
								int num18 = fixedList128Bytes6[num17];
								float3 float2 = this.localPositions[num18] - rhs;
								if (math.degrees(MathUtility.Angle(@float, float2)) < 120f)
								{
									int3 item2 = DataUtility.PackInt3(num13, num16, num18);
									hashSet3.Add(item2);
									hashSet2.Add(DataUtility.PackInt2(num13, num16));
									hashSet2.Add(DataUtility.PackInt2(num13, num18));
								}
							}
						}
					}
				}
				if (hashSet3.Count > 0)
				{
					this.triangles = new ExSimpleNativeArray<int3>(hashSet3.Count, false);
					int num19 = 0;
					foreach (int3 value2 in hashSet3)
					{
						this.triangles[num19] = value2;
						num19++;
					}
				}
				foreach (int2 item3 in hashSet2)
				{
					hashSet.Remove(item3);
				}
				if (hashSet.Count > 0)
				{
					this.lines = new ExSimpleNativeArray<int2>(hashSet.Count, false);
					int num20 = 0;
					foreach (int2 value3 in hashSet)
					{
						this.lines[num20] = value3;
						num20++;
					}
				}
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00023968 File Offset: 0x00021B68
		public void ImportFrom(RenderData renderData)
		{
			try
			{
				if (renderData == null)
				{
					this.result.SetError(Define.Result.VirtualMesh_InvalidRenderData);
				}
				this.ImportFrom(renderData.setupData);
			}
			catch (Exception)
			{
				this.result.SetError(Define.Result.VirtualMesh_ImportError);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000239BC File Offset: 0x00021BBC
		public void SelectionMesh(SelectionData selectionData, float4x4 selectionLocalToWorldMatrix, float mergin)
		{
			try
			{
				if (selectionData == null || !selectionData.IsValid())
				{
					this.result.SetError(Define.Result.VirtualMesh_InvalidSelection);
					throw new MagicaClothProcessingException();
				}
				int count = selectionData.Count;
				NativeArray<float3> positionNativeArray = selectionData.GetPositionNativeArray();
				try
				{
					NativeArray<VertexAttribute> attributeNativeArray = selectionData.GetAttributeNativeArray();
					try
					{
						if (!MathUtility.CompareMatrix(selectionLocalToWorldMatrix, this.initLocalToWorld))
						{
							float4x4 float4x = MathUtility.Transform(selectionLocalToWorldMatrix, this.initWorldToLocal);
							JobUtility.TransformPositionRun(positionNativeArray, count, float4x);
						}
						float gridSize = mergin * 1f;
						using (GridMap<int> gridMap = SelectionData.CreateGridMapRun(gridSize, positionNativeArray, attributeNativeArray, true, true, true, false))
						{
							using (NativeList<int3> nativeList = new NativeList<int3>(this.TriangleCount, Allocator.Persistent))
							{
								using (NativeArray<int> newVertexRemapIndices = new NativeArray<int>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
								{
									using (NativeReference<int> newVertexCount = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.ClearMemory))
									{
										new VirtualMesh.Select_GridJob
										{
											gridSize = gridSize,
											gridMap = gridMap.GetMultiHashMap(),
											selectionCount = count,
											selectionPositions = positionNativeArray,
											selectionAttributes = attributeNativeArray,
											vertexCount = this.VertexCount,
											triangleCount = this.TriangleCount,
											searchRadius = mergin,
											meshPositions = this.localPositions.GetNativeArray(),
											meshTriangles = this.triangles.GetNativeArray(),
											newTriangles = nativeList,
											newVertexRemapIndices = newVertexRemapIndices,
											newVertexCount = newVertexCount
										}.Run<VirtualMesh.Select_GridJob>();
										if (newVertexCount.Value < this.VertexCount)
										{
											int value = newVertexCount.Value;
											ExSimpleNativeArray<int> exSimpleNativeArray = new ExSimpleNativeArray<int>(value, false);
											ExSimpleNativeArray<VertexAttribute> exSimpleNativeArray2 = new ExSimpleNativeArray<VertexAttribute>(value, false);
											ExSimpleNativeArray<float3> exSimpleNativeArray3 = new ExSimpleNativeArray<float3>(value, false);
											ExSimpleNativeArray<float3> exSimpleNativeArray4 = new ExSimpleNativeArray<float3>(value, false);
											ExSimpleNativeArray<float3> exSimpleNativeArray5 = new ExSimpleNativeArray<float3>(value, false);
											ExSimpleNativeArray<float2> exSimpleNativeArray6 = new ExSimpleNativeArray<float2>(value, false);
											ExSimpleNativeArray<VirtualMeshBoneWeight> exSimpleNativeArray7 = new ExSimpleNativeArray<VirtualMeshBoneWeight>(value, false);
											new VirtualMesh.Select_PackVertexJob
											{
												vertexCount = this.VertexCount,
												newVertexRemapIndices = newVertexRemapIndices,
												attributes = this.attributes.GetNativeArray(),
												localPositions = this.localPositions.GetNativeArray(),
												localNormals = this.localNormals.GetNativeArray(),
												localTangents = this.localTangents.GetNativeArray(),
												uv = this.uv.GetNativeArray(),
												boneWeights = this.boneWeights.GetNativeArray(),
												newReferenceIndices = exSimpleNativeArray.GetNativeArray(),
												newAttributes = exSimpleNativeArray2.GetNativeArray(),
												newLocalPositions = exSimpleNativeArray3.GetNativeArray(),
												newLocalNormals = exSimpleNativeArray4.GetNativeArray(),
												newLocalTangents = exSimpleNativeArray5.GetNativeArray(),
												newUv = exSimpleNativeArray6.GetNativeArray(),
												newBoneWeights = exSimpleNativeArray7.GetNativeArray()
											}.Run<VirtualMesh.Select_PackVertexJob>();
											this.referenceIndices.Dispose();
											this.attributes.Dispose();
											this.localPositions.Dispose();
											this.localNormals.Dispose();
											this.localTangents.Dispose();
											this.uv.Dispose();
											this.boneWeights.Dispose();
											this.referenceIndices = exSimpleNativeArray;
											this.attributes = exSimpleNativeArray2;
											this.localPositions = exSimpleNativeArray3;
											this.localNormals = exSimpleNativeArray4;
											this.localTangents = exSimpleNativeArray5;
											this.uv = exSimpleNativeArray6;
											this.boneWeights = exSimpleNativeArray7;
											ExSimpleNativeArray<int3> exSimpleNativeArray8 = new ExSimpleNativeArray<int3>(nativeList);
											this.triangles.Dispose();
											this.triangles = exSimpleNativeArray8;
										}
									}
								}
							}
						}
					}
					finally
					{
						((IDisposable)attributeNativeArray).Dispose();
					}
				}
				finally
				{
					((IDisposable)positionNativeArray).Dispose();
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!this.result.IsError())
				{
					this.result.SetError(Define.Result.VirtualMesh_SelectionUnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.VirtualMesh_SelectionException);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00023E54 File Offset: 0x00022054
		public float CalcSelectionMergin(ReductionSettings settings)
		{
			float value = this.averageVertexDistance.Value;
			float y = 0f;
			if (settings != null && settings.IsEnabled)
			{
				y = this.boundingBox.Value.MaxSideLength * settings.GetMaxConnectionDistance();
			}
			return math.max(value, y) * 1.5f;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00023EA4 File Offset: 0x000220A4
		public void AddMesh(VirtualMesh cmesh)
		{
			try
			{
				if (this.IsError)
				{
					throw new InvalidOperationException();
				}
				if (cmesh == null || !cmesh.IsSuccess || cmesh.IsError)
				{
					throw new InvalidOperationException();
				}
				int skinBoneCount = this.SkinBoneCount;
				int vertexCount = this.VertexCount;
				int triangleCount = this.TriangleCount;
				int lineCount = this.LineCount;
				int count = this.transformData.Count;
				float4x4 float4x = cmesh.CenterTransformTo(this);
				int skinBoneCount2 = cmesh.SkinBoneCount;
				this.skinBoneTransformIndices.AddRange(skinBoneCount2);
				for (int i = 0; i < skinBoneCount2; i++)
				{
					int srcIndex = cmesh.skinBoneTransformIndices[i];
					int value = this.transformData.AddTransform(cmesh.transformData, srcIndex, true);
					this.skinBoneTransformIndices[skinBoneCount + i] = value;
				}
				int vertexCount2 = cmesh.VertexCount;
				cmesh.mergeChunk = new DataChunk(this.VertexCount, vertexCount2);
				this.attributes.AddRange(vertexCount2);
				this.localPositions.AddRange(vertexCount2);
				this.localNormals.AddRange(vertexCount2);
				this.localTangents.AddRange(vertexCount2);
				this.uv.AddRange(vertexCount2);
				this.boneWeights.AddRange(vertexCount2);
				new VirtualMesh.Add_CopyVerticesJob
				{
					vertexOffset = vertexCount,
					skinBoneOffset = skinBoneCount,
					toM = float4x,
					srcAttributes = cmesh.attributes.GetNativeArray(),
					srclocalPositions = cmesh.localPositions.GetNativeArray(),
					srclocalNormals = cmesh.localNormals.GetNativeArray(),
					srclocalTangents = cmesh.localTangents.GetNativeArray(),
					srcUV = cmesh.uv.GetNativeArray(),
					srcBoneWeights = cmesh.boneWeights.GetNativeArray(),
					dstAttributes = this.attributes.GetNativeArray(),
					dstlocalPositions = this.localPositions.GetNativeArray(),
					dstlocalNormals = this.localNormals.GetNativeArray(),
					dstlocalTangents = this.localTangents.GetNativeArray(),
					dstUV = this.uv.GetNativeArray(),
					dstBoneWeights = this.boneWeights.GetNativeArray(),
					dstSkinBoneIndices = this.skinBoneTransformIndices.GetNativeArray()
				}.Run(cmesh.VertexCount);
				this.skinBoneBindPoses.AddRange(skinBoneCount2);
				new VirtualMesh.Add_CalcBindPoseJob
				{
					skinBoneOffset = skinBoneCount,
					srcSkinBoneTransformIndices = cmesh.skinBoneTransformIndices.GetNativeArray(),
					srcTransformPositionArray = cmesh.transformData.positionArray.GetNativeArray(),
					srcTransformRotationArray = cmesh.transformData.rotationArray.GetNativeArray(),
					srcTransformScaleArray = cmesh.transformData.scaleArray.GetNativeArray(),
					dstCenterLocalToWorldMatrix = this.initLocalToWorld,
					dstSkinBoneBindPoses = this.skinBoneBindPoses.GetNativeArray()
				}.Run(skinBoneCount2);
				if (cmesh.TriangleCount > 0)
				{
					this.triangles.AddRange(cmesh.TriangleCount);
					new JobUtility.AddInt3DataCopyJob
					{
						dstOffset = triangleCount,
						addData = vertexCount,
						srcData = cmesh.triangles.GetNativeArray(),
						dstData = this.triangles.GetNativeArray()
					}.Run(cmesh.TriangleCount);
				}
				if (cmesh.LineCount > 0)
				{
					this.lines.AddRange(cmesh.LineCount);
					new JobUtility.AddInt2DataCopyJob
					{
						dstOffset = lineCount,
						addData = vertexCount,
						srcData = cmesh.lines.GetNativeArray(),
						dstData = this.lines.GetNativeArray()
					}.Run(cmesh.LineCount);
				}
				float3 b = cmesh.boundingBox.Value.Min;
				float3 b2 = cmesh.boundingBox.Value.Max;
				b = math.transform(float4x, b);
				b2 = math.transform(float4x, b2);
				AABB value2 = new AABB(ref b, ref b2);
				if (this.boundingBox.IsCreated)
				{
					this.boundingBox.Value.Encapsulate(value2);
				}
				else
				{
					this.boundingBox = new NativeReference<AABB>(value2, Allocator.Persistent);
				}
				this.averageVertexDistance.Value = math.max(this.averageVertexDistance.Value, cmesh.averageVertexDistance.Value);
				this.maxVertexDistance.Value = math.max(this.maxVertexDistance.Value, cmesh.maxVertexDistance.Value);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.Error);
				throw;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00024350 File Offset: 0x00022550
		public void SetTransform(Transform center, Transform skinRoot = null, int centerId = 0, int skinRootId = 0)
		{
			this.SetCenterTransform(center, centerId);
			if (skinRoot != null)
			{
				this.SetSkinRoot(skinRoot, skinRootId);
			}
			else
			{
				this.SetSkinRoot(center, centerId);
			}
			this.initLocalToWorld = center.localToWorldMatrix;
			this.initWorldToLocal = math.inverse(this.initLocalToWorld);
			this.initRotation = center.rotation;
			this.initInverseRotation = math.inverse(this.initRotation);
			this.initScale = center.lossyScale;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000243D8 File Offset: 0x000225D8
		public void SetTransform(TransformRecord centerRecord, TransformRecord skinRootRecord = null)
		{
			this.centerTransformIndex = this.transformData.AddTransform(centerRecord, 0, 1, true);
			if (skinRootRecord != null)
			{
				this.skinRootIndex = this.transformData.AddTransform(skinRootRecord, 0, 1, true);
			}
			else
			{
				this.skinRootIndex = this.centerTransformIndex;
			}
			this.initLocalToWorld = centerRecord.localToWorldMatrix;
			this.initWorldToLocal = centerRecord.worldToLocalMatrix;
			this.initRotation = centerRecord.rotation;
			this.initInverseRotation = math.inverse(this.initRotation);
			this.initScale = centerRecord.scale;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00024475 File Offset: 0x00022675
		public void SetCenterTransform(Transform t, int tid = 0)
		{
			if (t)
			{
				if (this.centerTransformIndex >= 0)
				{
					this.transformData.ReplaceTransform(this.centerTransformIndex, t, tid, 0, 1);
					return;
				}
				this.centerTransformIndex = this.transformData.AddTransform(t, tid, 0, 1, true);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000244B5 File Offset: 0x000226B5
		public void SetSkinRoot(Transform t, int tid = 0)
		{
			if (t)
			{
				if (this.skinRootIndex >= 0)
				{
					this.transformData.ReplaceTransform(this.skinRootIndex, t, tid, 0, 1);
					return;
				}
				this.skinRootIndex = this.transformData.AddTransform(t, tid, 0, 1, true);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000244F5 File Offset: 0x000226F5
		public Transform GetCenterTransform()
		{
			return this.transformData.GetTransformFromIndex(this.centerTransformIndex);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00024508 File Offset: 0x00022708
		public float4x4 GetCenterLocalToWorldMatrix()
		{
			return this.transformData.GetLocalToWorldMatrix(this.centerTransformIndex);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002451B File Offset: 0x0002271B
		public float4x4 GetCenterWorldToLocalMatrix()
		{
			return this.transformData.GetWorldToLocalMatrix(this.centerTransformIndex);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00024530 File Offset: 0x00022730
		public void SetCustomSkinningBones(TransformRecord clothTransformRecord, List<TransformRecord> bones)
		{
			if (bones == null || bones.Count == 0)
			{
				return;
			}
			this.customSkinningBoneIndices = new int[bones.Count];
			for (int i = 0; i < bones.Count; i++)
			{
				TransformRecord transformRecord = bones[i];
				int num = -1;
				if (transformRecord.IsValid())
				{
					transformRecord.localPosition = clothTransformRecord.worldToLocalMatrix.MultiplyPoint(transformRecord.position);
					num = this.skinBoneTransformIndices.Count;
					int data = this.transformData.AddTransform(transformRecord, 0, 1, false);
					this.skinBoneTransformIndices.Add(data);
					float4x4 data2 = math.mul(transformRecord.worldToLocalMatrix, this.initLocalToWorld);
					this.skinBoneBindPoses.Add(data2);
				}
				this.customSkinningBoneIndices[i] = num;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000245F0 File Offset: 0x000227F0
		public bool CompareSpace(VirtualMesh target)
		{
			return MathUtility.CompareMatrix(this.initLocalToWorld, target.initLocalToWorld);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00024603 File Offset: 0x00022803
		public float4x4 CenterTransformTo(VirtualMesh to)
		{
			if (!this.CompareSpace(to))
			{
				return MathUtility.Transform(this.initLocalToWorld, to.initWorldToLocal);
			}
			return float4x4.identity;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00024628 File Offset: 0x00022828
		public Mesh ExportToMesh(bool buildSkinning = false, bool recalculationNormals = false, bool recalculationBounds = true)
		{
			Mesh mesh = new Mesh();
			mesh.MarkDynamic();
			Vector3[] array = new Vector3[this.VertexCount];
			Vector3[] array2 = new Vector3[this.VertexCount];
			this.localPositions.CopyTo<Vector3>(array);
			this.localNormals.CopyTo<Vector3>(array2);
			NativeArray<Vector4> array3 = new NativeArray<Vector4>(this.VertexCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			JobUtility.FillRun(array3, this.VertexCount, new Vector4(0f, 0f, 0f, -1f));
			Vector4[] array4 = array3.ToArray();
			this.localTangents.CopyToWithTypeChangeStride<Vector4>(array4);
			array3.Dispose();
			mesh.vertices = array;
			if (!recalculationNormals)
			{
				mesh.normals = array2;
			}
			mesh.tangents = array4;
			Vector2[] array5 = new Vector2[this.VertexCount];
			this.uv.CopyTo<Vector2>(array5);
			mesh.uv = array5;
			if (this.TriangleCount > 0)
			{
				int[] array6 = new int[this.TriangleCount * 3];
				this.triangles.CopyToWithTypeChange<int>(array6);
				mesh.triangles = array6;
			}
			if (buildSkinning)
			{
				BoneWeight[] array7 = new BoneWeight[this.VertexCount];
				this.boneWeights.CopyTo<BoneWeight>(array7);
				mesh.boneWeights = array7;
				Matrix4x4[] array8 = new Matrix4x4[this.SkinBoneCount];
				this.skinBoneBindPoses.CopyTo<Matrix4x4>(array8);
				mesh.bindposes = array8;
			}
			if (recalculationNormals)
			{
				mesh.RecalculateNormals();
			}
			if (recalculationBounds)
			{
				mesh.RecalculateBounds();
			}
			return mesh;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000244F5 File Offset: 0x000226F5
		public Transform ExportCenterTransform()
		{
			return this.transformData.GetTransformFromIndex(this.centerTransformIndex);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00024784 File Offset: 0x00022984
		public Transform ExportSkinRootBone()
		{
			return this.transformData.GetTransformFromIndex(this.skinRootIndex);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00024798 File Offset: 0x00022998
		public List<Transform> ExportSkinningBones()
		{
			List<Transform> list = new List<Transform>(this.SkinBoneCount);
			for (int i = 0; i < this.SkinBoneCount; i++)
			{
				list.Add(this.transformData.GetTransformFromIndex(this.skinBoneTransformIndices[i]));
			}
			return list;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000247E0 File Offset: 0x000229E0
		public Mesh ToRenderer(Renderer ren)
		{
			Mesh mesh = null;
			if (!this.IsSuccess)
			{
				return mesh;
			}
			if (ren is MeshRenderer)
			{
				mesh = this.ExportToMesh(false, false, true);
				ren.GetComponent<MeshFilter>().mesh = mesh;
			}
			else if (ren is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
				mesh = this.ExportToMesh(true, false, true);
				Transform rootBone = this.ExportSkinRootBone();
				Transform[] bones = this.ExportSkinningBones().ToArray();
				skinnedMeshRenderer.rootBone = rootBone;
				skinnedMeshRenderer.bones = bones;
				skinnedMeshRenderer.sharedMesh = mesh;
			}
			return mesh;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002485C File Offset: 0x00022A5C
		public void Mapping(VirtualMesh proxyMesh)
		{
			try
			{
				if (this.IsError)
				{
					throw new MagicaClothProcessingException();
				}
				if (proxyMesh == null || !proxyMesh.IsSuccess)
				{
					this.result.SetError(Define.Result.MappingMesh_ProxyError);
					throw new MagicaClothProcessingException();
				}
				this.result.SetProcess();
				float4x4 toP = this.CenterTransformTo(proxyMesh);
				using (NativeArray<VirtualMesh.MappingWorkData> mappingWorkData = new NativeArray<VirtualMesh.MappingWorkData>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
				{
					if (this.mergeChunk.IsValid)
					{
						new VirtualMesh.Mapping_DirectConnectionVertexDataJob
						{
							toP = toP,
							vcnt = this.VertexCount,
							mergeChunk = this.mergeChunk,
							localPositions = this.localPositions.GetNativeArray(),
							attributes = this.attributes.GetNativeArray(),
							joinIndices = proxyMesh.joinIndices,
							proxyAttributes = proxyMesh.attributes.GetNativeArray(),
							proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
							mappingWorkData = mappingWorkData
						}.Run<VirtualMesh.Mapping_DirectConnectionVertexDataJob>();
						float weightLength = proxyMesh.averageVertexDistance.Value * 1.5f;
						new VirtualMesh.Mapping_CalcDirectWeightJob
						{
							vcnt = mappingWorkData.Length,
							weightLength = weightLength,
							mappingWorkData = mappingWorkData,
							attributes = this.attributes.GetNativeArray(),
							boneWeights = this.boneWeights.GetNativeArray(),
							proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
							proxyVertexToVertexIndexArray = proxyMesh.vertexToVertexIndexArray,
							proxyVertexToVertexDataArray = proxyMesh.vertexToVertexDataArray
						}.Run<VirtualMesh.Mapping_CalcDirectWeightJob>();
					}
					else
					{
						float num = MathUtility.TransformLength(this.averageVertexDistance.Value, toP);
						float num2 = num * 2.5f;
						Debug.Log(string.Format("searchRadius:{0}", num2));
						float gridSize = num * 1.5f;
						using (GridMap<int> gridMap = proxyMesh.CreateVertexIndexGridMapRun(gridSize))
						{
							new VirtualMesh.Mapping_CalcConnectionVertexDataJob
							{
								gridSize = gridSize,
								searchRadius = num2,
								toP = toP,
								vcnt = this.VertexCount,
								localPositions = this.localPositions.GetNativeArray(),
								boneWeights = this.boneWeights.GetNativeArray(),
								transformIds = this.transformData.idArray.GetNativeArray(),
								attributes = this.attributes.GetNativeArray(),
								gridMap = gridMap.GetMultiHashMap(),
								proxyAttributes = proxyMesh.attributes.GetNativeArray(),
								proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
								proxyBoneWeights = proxyMesh.boneWeights.GetNativeArray(),
								proxyTransformIds = proxyMesh.transformData.idArray.GetNativeArray(),
								mappingWorkData = mappingWorkData
							}.Run<VirtualMesh.Mapping_CalcConnectionVertexDataJob>();
							if (mappingWorkData.Length > 0)
							{
								new VirtualMesh.Mapping_CalcWeightJob
								{
									mappingWorkData = mappingWorkData,
									attributes = this.attributes.GetNativeArray(),
									boneWeights = this.boneWeights.GetNativeArray(),
									proxyAttributes = proxyMesh.attributes.GetNativeArray(),
									proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
									proxyLocalNormals = proxyMesh.localNormals.GetNativeArray(),
									proxyVertexToVertexIndexArray = proxyMesh.vertexToVertexIndexArray,
									proxyVertexToVertexDataArray = proxyMesh.vertexToVertexDataArray
								}.Run(mappingWorkData.Length);
							}
						}
					}
					this.mappingProxyMesh = proxyMesh;
					this.toProxyMatrix = toP;
					this.toProxyRotation = math.mul(proxyMesh.initInverseRotation, this.initRotation);
					this.meshType = VirtualMesh.MeshType.Mapping;
					this.result.SetSuccess();
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!this.result.IsError())
				{
					this.result.SetError(Define.Result.MappingMesh_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.MappingMesh_Exception);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00024CA8 File Offset: 0x00022EA8
		private static float4 CalcVertexWeights(float4 distances)
		{
			distances = math.max(distances, 0);
			distances = math.pow(distances, 4f);
			float num = distances[0];
			for (int i = 0; i < 4; i++)
			{
				distances[i] = ((distances[i] > 0f) ? (num / distances[i]) : 0f);
			}
			float num2 = math.csum(distances);
			if (num2 <= 0f)
			{
				return new float4(1f, 0f, 0f, 0f);
			}
			distances /= num2;
			for (int j = 3; j >= 1; j--)
			{
				if (distances[j] < 0.01f)
				{
					distances[j] = 0f;
				}
			}
			distances /= math.csum(distances);
			return distances;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00024D7C File Offset: 0x00022F7C
		public void Optimization()
		{
			try
			{
				this.RemoveDuplicateTriangles();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.Optimize_Exception);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00024DB8 File Offset: 0x00022FB8
		private void RemoveDuplicateTriangles()
		{
			if (this.TriangleCount < 2)
			{
				return;
			}
			using (NativeParallelHashMap<int2, FixedList128Bytes<int>> edgeToTriangleList = new NativeParallelHashMap<int2, FixedList128Bytes<int>>(this.TriangleCount * 2, Allocator.Persistent))
			{
				using (NativeList<int3> nativeList = new NativeList<int3>(this.TriangleCount, Allocator.Persistent))
				{
					new VirtualMesh.Optimize_EdgeToTrianlgeJob
					{
						tcnt = this.TriangleCount,
						triangles = this.triangles.GetNativeArray(),
						localPositions = this.localPositions.GetNativeArray(),
						edgeToTriangleList = edgeToTriangleList,
						newTriangles = nativeList
					}.Run<VirtualMesh.Optimize_EdgeToTrianlgeJob>();
					ExSimpleNativeArray<int3> exSimpleNativeArray = this.triangles;
					if (exSimpleNativeArray != null)
					{
						exSimpleNativeArray.Dispose();
					}
					this.triangles = new ExSimpleNativeArray<int3>();
					if (nativeList.Length > 0)
					{
						this.triangles.AddRange(nativeList);
					}
				}
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00024EB4 File Offset: 0x000230B4
		private bool CheckTwoTriangleOpen(in int3 tri1, in int3 tri2, in int2 edge, in float3 tri1n)
		{
			int index = DataUtility.RemainingData(tri2, edge);
			float3 y = math.normalize(this.localPositions[index] - this.localPositions[edge.x]);
			return math.dot(tri1n, y) <= 0f;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00024F08 File Offset: 0x00023108
		private float CalcTwoTriangleAngle(in int3 tri1, in int3 tri2, in int2 edge)
		{
			int index = DataUtility.RemainingData(tri1, edge);
			int index2 = DataUtility.RemainingData(tri2, edge);
			float3 @float = this.localPositions[edge.y] - this.localPositions[edge.x];
			float3 y = this.localPositions[index] - this.localPositions[edge.x];
			float3 x = this.localPositions[index2] - this.localPositions[edge.x];
			float3 float2 = math.cross(@float, y);
			float3 float3 = math.cross(x, @float);
			return math.degrees(MathUtility.Angle(float2, float3));
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00024FB4 File Offset: 0x000231B4
		public void ConvertProxyMesh(ClothSerializeData sdata, TransformRecord clothTransformRecord, List<TransformRecord> customSkinningBoneRecords, TransformRecord normalAdjustmentTransformRecord)
		{
			try
			{
				if (this.IsError)
				{
					throw new MagicaClothProcessingException();
				}
				if (sdata.customSkinningSetting.enable)
				{
					this.SetCustomSkinningBones(clothTransformRecord, customSkinningBoneRecords);
				}
				this.vertexToTriangles = new NativeArray<FixedList32Bytes<int>>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.vertexBindPosePositions = new NativeArray<float3>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.vertexBindPoseRotations = new NativeArray<quaternion>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.vertexToTransformRotations = new NativeArray<quaternion>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				JobUtility.FillRun(this.vertexToTransformRotations, this.VertexCount, quaternion.identity);
				using (MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(this.VertexCount, this.VertexCount * 4))
				{
					using (NativeParallelHashSet<int2> edgeSet = new NativeParallelHashSet<int2>(this.VertexCount * 2, Allocator.Persistent))
					{
						if (this.TriangleCount > 0)
						{
							new VirtualMesh.Proxy_CalcVertexToVertexFromTriangleJob
							{
								triangleCount = this.TriangleCount,
								triangles = this.triangles.GetNativeArray(),
								vertexToVertexMap = multiDataBuilder.Map,
								edgeSet = edgeSet
							}.Run<VirtualMesh.Proxy_CalcVertexToVertexFromTriangleJob>();
						}
						if (this.LineCount > 0)
						{
							new VirtualMesh.Proxy_CalcVertexToVertexFromLineJob
							{
								lineCount = this.LineCount,
								lines = this.lines.GetNativeArray(),
								vertexToVertexMap = multiDataBuilder.Map,
								edgeSet = edgeSet
							}.Run<VirtualMesh.Proxy_CalcVertexToVertexFromLineJob>();
						}
						this.edges = edgeSet.ToNativeArray(Allocator.Persistent);
						multiDataBuilder.ToNativeArray(out this.vertexToVertexIndexArray, out this.vertexToVertexDataArray);
						if (this.TriangleCount > 0)
						{
							this.edgeToTriangles = new NativeParallelMultiHashMap<int2, ushort>(this.TriangleCount * 2, Allocator.Persistent);
							new VirtualMesh.Proxy_CalcEdgeToTriangleJob
							{
								tcnt = this.TriangleCount,
								triangles = this.triangles.GetNativeArray(),
								edgeToTriangles = this.edgeToTriangles
							}.Run<VirtualMesh.Proxy_CalcEdgeToTriangleJob>();
							using (NativeArray<float3> triangleNormals = new NativeArray<float3>(this.TriangleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
							{
								new VirtualMesh.Proxy_CalcTriangleNormalJob
								{
									triangles = this.triangles.GetNativeArray(),
									localPositins = this.localPositions.GetNativeArray(),
									triangleNormals = triangleNormals
								}.Run(this.TriangleCount);
								this.OptimizeTriangleDirection(triangleNormals, 80f);
								using (NativeArray<float3> triangleTangents = new NativeArray<float3>(this.TriangleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
								{
									new VirtualMesh.Proxy_CalcTriangleTangentJob
									{
										triangles = this.triangles.GetNativeArray(),
										localPositins = this.localPositions.GetNativeArray(),
										uv = this.uv.GetNativeArray(),
										triangleTangents = triangleTangents
									}.Run(this.TriangleCount);
									new VirtualMesh.Proxy_CreateVertexToTrianglesJob
									{
										triangles = this.triangles.GetNativeArray(),
										vertexToTriangles = this.vertexToTriangles
									}.Run<VirtualMesh.Proxy_CreateVertexToTrianglesJob>();
									new VirtualMesh.Proxy_OrganizeVertexToTrianglsJob
									{
										vertexToTriangles = this.vertexToTriangles,
										triangleNormals = triangleNormals,
										attributes = this.attributes.GetNativeArray()
									}.Run(this.VertexCount);
									new VirtualMesh.Proxy_CalcVertexNormalTangentFromTriangleJob
									{
										triangleNormals = triangleNormals,
										triangleTangents = triangleTangents,
										vertexToTriangles = this.vertexToTriangles,
										localNormals = this.localNormals.GetNativeArray(),
										localTangents = this.localTangents.GetNativeArray()
									}.Run(this.VertexCount);
								}
								goto IL_37E;
							}
						}
						this.edgeToTriangles = new NativeParallelMultiHashMap<int2, ushort>(1, Allocator.Persistent);
						IL_37E:
						this.ProxyCreateFixedListAndAABB();
						if (this.isBoneCloth)
						{
							this.CreateTransformBaseLine();
						}
						else
						{
							this.CreateMeshBaseLine();
						}
						this.ProxyNormalAdjustment(sdata, normalAdjustmentTransformRecord);
						if (this.isBoneCloth)
						{
							new VirtualMesh.Proxy_CalcVertexToTransformJob
							{
								invRot = this.initInverseRotation,
								localNormals = this.localNormals.GetNativeArray(),
								localTangents = this.localTangents.GetNativeArray(),
								vertexToTransformRotations = this.vertexToTransformRotations,
								transformRotations = this.transformData.rotationArray.GetNativeArray()
							}.Run(this.VertexCount);
						}
						new VirtualMesh.Proxy_CalcVertexBindPoseJob2
						{
							localPositions = this.localPositions.GetNativeArray(),
							localNormals = this.localNormals.GetNativeArray(),
							localTangents = this.localTangents.GetNativeArray(),
							vertexBindPosePositions = this.vertexBindPosePositions,
							vertexBindPoseRotations = this.vertexBindPoseRotations
						}.Run(this.VertexCount);
						this.edgeFlags = new NativeArray<ExBitFlag8>(this.EdgeCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
						if (this.EdgeCount > 0)
						{
							new VirtualMesh.Proxy_CreateEdgeFlagJob
							{
								edges = this.edges,
								edgeToTriangles = this.edgeToTriangles,
								edgeFlags = this.edgeFlags
							}.Run(this.EdgeCount);
						}
						this.CreateBaseLinePose();
						this.CreateVertexRootAndDepth();
						if (sdata.customSkinningSetting.enable)
						{
							this.CreateCustomSkinning(sdata.customSkinningSetting, customSkinningBoneRecords);
						}
						if (this.isBoneCloth)
						{
							this.meshType = VirtualMesh.MeshType.ProxyBoneMesh;
						}
						else
						{
							this.meshType = VirtualMesh.MeshType.ProxyMesh;
						}
					}
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!this.result.IsError())
				{
					this.result.SetError(Define.Result.ProxyMesh_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.ProxyMesh_Exception);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000255BC File Offset: 0x000237BC
		private void ProxyNormalAdjustment(ClothSerializeData sdata, TransformRecord normalAdjustmentTransformRecord)
		{
			int vertexCount = this.VertexCount;
			if (vertexCount == 0)
			{
				return;
			}
			this.normalAdjustmentRotations = new NativeArray<quaternion>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			JobUtility.FillRun(this.normalAdjustmentRotations, vertexCount, quaternion.identity);
			NormalAlignmentSettings.AlignmentMode alignmentMode = sdata.normalAlignmentSetting.alignmentMode;
			if (alignmentMode == NormalAlignmentSettings.AlignmentMode.None)
			{
				return;
			}
			if (alignmentMode == NormalAlignmentSettings.AlignmentMode.BoundingBoxCenter || alignmentMode == NormalAlignmentSettings.AlignmentMode.Transform)
			{
				float3 center;
				if (alignmentMode == NormalAlignmentSettings.AlignmentMode.BoundingBoxCenter)
				{
					center = this.boundingBox.Value.Center;
				}
				else
				{
					center = math.transform(this.initWorldToLocal, normalAdjustmentTransformRecord.position);
				}
				new VirtualMesh.ProxyNormalRadiationAdjustmentJob
				{
					center = center,
					localPositions = this.localPositions.GetNativeArray(),
					vertexParentIndices = this.vertexParentIndices,
					vertexChildIndexArray = this.vertexChildIndexArray,
					vertexChildDataArray = this.vertexChildDataArray,
					localNormals = this.localNormals.GetNativeArray(),
					localTangents = this.localTangents.GetNativeArray(),
					normalAdjustmentRotations = this.normalAdjustmentRotations
				}.Run(vertexCount);
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000256C4 File Offset: 0x000238C4
		private void ProxyCreateFixedListAndAABB()
		{
			this.localCenterPosition = new NativeReference<float3>(0, Allocator.Persistent);
			using (NativeList<ushort> fixedList = new NativeList<ushort>(this.VertexCount / 20 + 1, Allocator.TempJob))
			{
				new VirtualMesh.ProxyCreateFixedListAndAABBJob
				{
					vcnt = this.VertexCount,
					attributes = this.attributes.GetNativeArray(),
					localPositions = this.localPositions.GetNativeArray(),
					vertexToVertexIndexArray = this.vertexToVertexIndexArray,
					vertexToVertexDataArray = this.vertexToVertexDataArray,
					outAABB = this.boundingBox,
					fixedList = fixedList,
					localCenterPosition = this.localCenterPosition
				}.Run<VirtualMesh.ProxyCreateFixedListAndAABBJob>();
				if (fixedList.Length > 0)
				{
					this.centerFixedList = fixedList.ToArray();
				}
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000257B4 File Offset: 0x000239B4
		private unsafe void OptimizeTriangleDirection(NativeArray<float3> triangleNormals, float sameSurfaceAngle)
		{
			if (this.TriangleCount == 0)
			{
				return;
			}
			int i = 0;
			HashSet<int> hashSet = new HashSet<int>();
			Queue<int> queue = new Queue<int>(this.TriangleCount / 2);
			List<int> list = new List<int>(this.TriangleCount);
			while (i < this.TriangleCount)
			{
				if (hashSet.Contains(i))
				{
					i++;
				}
				else
				{
					hashSet.Add(i);
					queue.Clear();
					queue.Enqueue(i);
					list.Clear();
					int num = 0;
					int num2 = 0;
					while (queue.Count > 0)
					{
						int num3 = queue.Dequeue();
						float3 x = triangleNormals[num3];
						int3 @int = this.triangles[num3];
						list.Add(num3);
						int2 xy = @int.xy;
						int2 c = DataUtility.PackInt2(xy);
						int2 yz = @int.yz;
						int2 c2 = DataUtility.PackInt2(yz);
						int2 zx = @int.zx;
						int2x3 int2x = new int2x3(c, c2, DataUtility.PackInt2(zx));
						for (int j = 0; j < 3; j++)
						{
							int2 key = *int2x[j];
							if (this.edgeToTriangles.ContainsKey(key))
							{
								using (NativeParallelMultiHashMap<int2, ushort>.Enumerator enumerator = this.edgeToTriangles.GetValuesForKey(key).GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										int num4 = (int)enumerator.Current;
										if (!hashSet.Contains(num4))
										{
											int3 value = this.triangles[num4];
											float3 @float = triangleNormals[num4];
											if (this.CalcTwoTriangleAngle(@int, value, key) <= sameSurfaceAngle)
											{
												if (math.dot(x, @float) < 0f)
												{
													value = MathUtility.FlipTriangle(value);
													this.triangles[num4] = value;
													triangleNormals[num4] = -@float;
												}
												if (this.CheckTwoTriangleOpen(@int, value, key, x))
												{
													num++;
												}
												else
												{
													num2++;
												}
												hashSet.Add(num4);
												queue.Enqueue(num4);
											}
										}
									}
								}
							}
						}
					}
					if (num2 > num)
					{
						foreach (int num5 in list)
						{
							ExSimpleNativeArray<int3> exSimpleNativeArray = this.triangles;
							int index = num5;
							int3 int2 = this.triangles[num5];
							exSimpleNativeArray[index] = MathUtility.FlipTriangle(int2);
							triangleNormals[num5] = -triangleNormals[num5];
						}
					}
				}
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00025A44 File Offset: 0x00023C44
		private void CreateCustomSkinning(CustomSkinningSettings setting, List<TransformRecord> bones)
		{
			if (this.CustomSkinningBoneCount == 0)
			{
				return;
			}
			using (NativeList<VirtualMesh.SkinningBoneInfo> boneInfoList = new NativeList<VirtualMesh.SkinningBoneInfo>(this.CustomSkinningBoneCount * 2, Allocator.Persistent))
			{
				for (int i = 0; i < this.CustomSkinningBoneCount; i++)
				{
					int num = this.customSkinningBoneIndices[i];
					if (num != -1)
					{
						int pid = bones[i].pid;
						if (pid != 0)
						{
							int num2 = bones.FindIndex((TransformRecord x) => x.id == pid);
							if (num2 >= 0)
							{
								VirtualMesh.SkinningBoneInfo skinningBoneInfo = default(VirtualMesh.SkinningBoneInfo);
								skinningBoneInfo.startTransformIndex = this.customSkinningBoneIndices[num2];
								skinningBoneInfo.startPos = bones[num2].localPosition;
								skinningBoneInfo.endTransformIndex = num;
								skinningBoneInfo.endPos = bones[i].localPosition;
								if (math.distance(skinningBoneInfo.startPos, skinningBoneInfo.endPos) >= 1E-08f)
								{
									boneInfoList.Add(skinningBoneInfo);
								}
							}
						}
					}
				}
				if (boneInfoList.Length != 0)
				{
					new VirtualMesh.Proxy_CalcCustomSkinningWeightsJob
					{
						isBoneCloth = this.isBoneCloth,
						angularAttenuation = 1f,
						distanceReduction = 0.6f,
						distancePow = 2f,
						attributes = this.attributes.GetNativeArray(),
						localPositions = this.localPositions.GetNativeArray(),
						boneInfoList = boneInfoList,
						boneWeights = this.boneWeights.GetNativeArray()
					}.Run(this.VertexCount);
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00025C04 File Offset: 0x00023E04
		public void ApplySelectionAttribute(SelectionData selectionData)
		{
			try
			{
				NativeArray<float3> positionNativeArray = selectionData.GetPositionNativeArray();
				try
				{
					NativeArray<VertexAttribute> attributeNativeArray = selectionData.GetAttributeNativeArray();
					try
					{
						float num = math.max(this.averageVertexDistance.Value, selectionData.maxConnectionDistance);
						float gridSize = num * 1.5f;
						using (GridMap<int> gridMap = SelectionData.CreateGridMapRun(gridSize, positionNativeArray, attributeNativeArray, true, true, true, true))
						{
							new VirtualMesh.Proxy_ApplySelectionJob
							{
								gridSize = gridSize,
								radius = num,
								localPositions = this.localPositions.GetNativeArray(),
								attributes = this.attributes.GetNativeArray(),
								gridMap = gridMap.GetMultiHashMap(),
								selectionPositions = positionNativeArray,
								selectionAttributes = attributeNativeArray
							}.Run(this.VertexCount);
							if (this.isBoneCloth)
							{
								new VirtualMesh.Proxy_BoneClothApplayTransformFlagJob
								{
									attributes = this.attributes.GetNativeArray(),
									transformFlags = this.transformData.flagArray.GetNativeArray()
								}.Run(this.VertexCount);
							}
						}
					}
					finally
					{
						((IDisposable)attributeNativeArray).Dispose();
					}
				}
				finally
				{
					((IDisposable)positionNativeArray).Dispose();
				}
			}
			catch (Exception)
			{
				this.result.SetError(Define.Result.ProxyMesh_ApplySelectionError);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00025DA4 File Offset: 0x00023FA4
		private void CreateMeshBaseLine()
		{
			int vertexCount = this.VertexCount;
			this.vertexParentIndices = new NativeArray<int>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			using (MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount))
			{
				JobUtility.FillRun(this.vertexParentIndices, vertexCount, -1);
				using (NativeList<int> fixedList = new NativeList<int>(vertexCount, Allocator.Persistent))
				{
					new VirtualMesh.BaseLine_Mesh_CareteFixedListJob
					{
						vcnt = vertexCount,
						attribues = this.attributes.GetNativeArray(),
						fixedList = fixedList
					}.Run<VirtualMesh.BaseLine_Mesh_CareteFixedListJob>();
					if (fixedList.Length == 0)
					{
						this.vertexChildIndexArray = new NativeArray<uint>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
						this.vertexChildDataArray = new NativeArray<ushort>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
					}
					else
					{
						using (NativeList<VirtualMesh.BaseLineWork> nextList = new NativeList<VirtualMesh.BaseLineWork>(vertexCount, Allocator.Persistent))
						{
							new VirtualMesh.BaseLine_Mesh_CreateParentJob2
							{
								vcnt = vertexCount,
								avgDist = this.averageVertexDistance.Value,
								attribues = this.attributes.GetNativeArray(),
								localPositions = this.localPositions.GetNativeArray(),
								vertexToVertexIndexArray = this.vertexToVertexIndexArray,
								vertexToVertexDataArray = this.vertexToVertexDataArray,
								vertexParentIndices = this.vertexParentIndices,
								vertexChildMap = multiDataBuilder.Map,
								fixedList = fixedList,
								nextList = nextList
							}.Run<VirtualMesh.BaseLine_Mesh_CreateParentJob2>();
							Stack<int> stack = new Stack<int>(vertexCount);
							List<ExBitFlag8> list = new List<ExBitFlag8>(fixedList.Length);
							List<ushort> list2 = new List<ushort>(fixedList.Length);
							List<ushort> list3 = new List<ushort>(fixedList.Length);
							List<ushort> list4 = new List<ushort>(vertexCount);
							for (int i = 0; i < fixedList.Length; i++)
							{
								int num = fixedList[i];
								if (multiDataBuilder.GetDataCount(num) != 0)
								{
									stack.Clear();
									stack.Push(num);
									ushort item = (ushort)list4.Count;
									ushort num2 = 0;
									ExBitFlag8 item2 = default(ExBitFlag8);
									while (stack.Count > 0)
									{
										int num3 = stack.Pop();
										list4.Add((ushort)num3);
										num2 += 1;
										if (!this.attributes[num3].IsSet(128))
										{
											item2.SetFlag(1, true);
										}
										if (multiDataBuilder.Map.ContainsKey(num3))
										{
											foreach (ushort item3 in multiDataBuilder.Map.GetValuesForKey(num3))
											{
												stack.Push((int)item3);
											}
										}
									}
									list.Add(item2);
									list2.Add(item);
									list3.Add(num2);
								}
							}
							this.baseLineFlags = new NativeArray<ExBitFlag8>(list.ToArray(), Allocator.Persistent);
							this.baseLineStartDataIndices = new NativeArray<ushort>(list2.ToArray(), Allocator.Persistent);
							this.baseLineDataCounts = new NativeArray<ushort>(list3.ToArray(), Allocator.Persistent);
							this.baseLineData = new NativeArray<ushort>(list4.ToArray(), Allocator.Persistent);
							ValueTuple<ushort[], uint[]> valueTuple = multiDataBuilder.ToArray();
							ushort[] item4 = valueTuple.Item1;
							uint[] item5 = valueTuple.Item2;
							this.vertexChildIndexArray = new NativeArray<uint>(item5, Allocator.Persistent);
							this.vertexChildDataArray = new NativeArray<ushort>(item4, Allocator.Persistent);
						}
					}
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00026140 File Offset: 0x00024340
		private void CreateTransformBaseLine()
		{
			int vertexCount = this.VertexCount;
			this.vertexParentIndices = new NativeArray<int>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			using (MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount))
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>(vertexCount);
				NativeArray<int> nativeArray = this.transformData.idArray.GetNativeArray();
				NativeArray<int> nativeArray2 = this.transformData.parentIdArray.GetNativeArray();
				for (int i = 0; i < vertexCount; i++)
				{
					dictionary.Add(nativeArray[i], i);
				}
				for (int j = 0; j < vertexCount; j++)
				{
					int key = nativeArray2[j];
					if (dictionary.ContainsKey(key))
					{
						this.vertexParentIndices[j] = dictionary[key];
					}
					else
					{
						this.vertexParentIndices[j] = -1;
					}
				}
				new VirtualMesh.BaseLine_Bone_CreateBoneChildInfoJob
				{
					vcnt = vertexCount,
					parentIndices = this.vertexParentIndices,
					childMap = multiDataBuilder.Map
				}.Run<VirtualMesh.BaseLine_Bone_CreateBoneChildInfoJob>();
				int rootCount = this.transformData.RootCount;
				Stack<int> stack = new Stack<int>(vertexCount);
				Stack<int> stack2 = new Stack<int>(vertexCount);
				List<ExBitFlag8> list = new List<ExBitFlag8>(rootCount);
				List<ushort> list2 = new List<ushort>(rootCount);
				List<ushort> list3 = new List<ushort>(rootCount);
				List<ushort> list4 = new List<ushort>(vertexCount);
				foreach (int key2 in this.transformData.rootIdList)
				{
					stack.Clear();
					int item = dictionary[key2];
					stack.Push(item);
					while (stack.Count > 0)
					{
						int num = stack.Pop();
						if (this.attributes[num].IsDontMove())
						{
							bool flag = false;
							foreach (ushort index in multiDataBuilder.Map.GetValuesForKey(num))
							{
								if (this.attributes[(int)index].IsMove())
								{
									flag = true;
								}
							}
							if (!flag)
							{
								using (NativeParallelMultiHashMap<int, ushort>.Enumerator enumerator2 = multiDataBuilder.Map.GetValuesForKey(num).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										ushort num2 = enumerator2.Current;
										if (this.attributes[(int)num2].IsDontMove())
										{
											stack.Push((int)num2);
										}
									}
									continue;
								}
							}
							stack2.Clear();
							stack2.Push(num);
							ushort item2 = (ushort)list4.Count;
							ushort num3 = 0;
							ExBitFlag8 item3 = default(ExBitFlag8);
							while (stack2.Count > 0)
							{
								int num4 = stack2.Pop();
								list4.Add((ushort)num4);
								num3 += 1;
								if (!this.attributes[num4].IsSet(128))
								{
									item3.SetFlag(1, true);
								}
								if (multiDataBuilder.Map.ContainsKey(num4))
								{
									foreach (ushort item4 in multiDataBuilder.Map.GetValuesForKey(num4))
									{
										stack2.Push((int)item4);
									}
								}
							}
							list.Add(item3);
							list2.Add(item2);
							list3.Add(num3);
						}
					}
				}
				this.baseLineFlags = new NativeArray<ExBitFlag8>(list.ToArray(), Allocator.Persistent);
				this.baseLineStartDataIndices = new NativeArray<ushort>(list2.ToArray(), Allocator.Persistent);
				this.baseLineDataCounts = new NativeArray<ushort>(list3.ToArray(), Allocator.Persistent);
				this.baseLineData = new NativeArray<ushort>(list4.ToArray(), Allocator.Persistent);
				multiDataBuilder.ToNativeArray(out this.vertexChildIndexArray, out this.vertexChildDataArray);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00026580 File Offset: 0x00024780
		private void CreateBaseLinePose()
		{
			int length = this.baseLineData.Length;
			this.vertexLocalPositions = new NativeArray<float3>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.vertexLocalRotations = new NativeArray<quaternion>(this.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			new VirtualMesh.BaseLine_CalcLocalPositionRotationJob
			{
				parentIndices = this.vertexParentIndices,
				localPositions = this.localPositions.GetNativeArray(),
				localNormals = this.localNormals.GetNativeArray(),
				localTangents = this.localTangents.GetNativeArray(),
				baseLineIndices = this.baseLineData,
				vertexLocalPositions = this.vertexLocalPositions,
				vertexLocalRotations = this.vertexLocalRotations
			}.Run(length);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00026638 File Offset: 0x00024838
		private void CreateVertexRootAndDepth()
		{
			int vertexCount = this.VertexCount;
			this.vertexDepths = new NativeArray<float>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.vertexRootIndices = new NativeArray<int>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			using (NativeArray<float> rootLengthArray = new NativeArray<float>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory))
			{
				new VirtualMesh.BaseLine_CalcMaxBaseLineLengthJob
				{
					vcnt = vertexCount,
					attribues = this.attributes.GetNativeArray(),
					localPositions = this.localPositions.GetNativeArray(),
					vertexParentIndices = this.vertexParentIndices,
					vertexDepths = this.vertexDepths,
					vertexRootIndices = this.vertexRootIndices,
					rootLengthArray = rootLengthArray
				}.Run<VirtualMesh.BaseLine_CalcMaxBaseLineLengthJob>();
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000266FC File Offset: 0x000248FC
		public void Reduction(ReductionSettings settings, CancellationToken ct)
		{
			try
			{
				using (ReductionWorkData reductionWorkData = new ReductionWorkData(this))
				{
					this.InitReductionWorkData(reductionWorkData);
					if (this.result.IsError())
					{
						throw new MagicaClothProcessingException();
					}
					float maxSideLength = this.boundingBox.Value.MaxSideLength;
					if (maxSideLength < 1E-08f)
					{
						this.result.SetError(Define.Result.Reduction_MaxSideLengthZero);
						throw new MagicaClothProcessingException();
					}
					float num = maxSideLength * math.saturate(0.001f);
					float num2 = maxSideLength * math.saturate(settings.simpleDistance);
					float num3 = maxSideLength * math.saturate(settings.shapeDistance);
					ct.ThrowIfCancellationRequested();
					using (SameDistanceReduction sameDistanceReduction = new SameDistanceReduction(this.name, this, reductionWorkData, num))
					{
						sameDistanceReduction.Reduction();
						if (sameDistanceReduction.Result.IsError())
						{
							this.result = sameDistanceReduction.Result;
							throw new MagicaClothProcessingException();
						}
					}
					ct.ThrowIfCancellationRequested();
					if (num2 > num)
					{
						float startMergeLength = math.min(num * 2f, num2);
						using (SimpleDistanceReduction simpleDistanceReduction = new SimpleDistanceReduction(this.name, this, reductionWorkData, startMergeLength, num2, 100, true, 1f))
						{
							simpleDistanceReduction.Reduction();
							if (simpleDistanceReduction.Result.IsError())
							{
								this.result = simpleDistanceReduction.Result;
								throw new MagicaClothProcessingException();
							}
						}
					}
					ct.ThrowIfCancellationRequested();
					if (num3 > 0f && num3 > num2)
					{
						float startMergeLength2 = math.min(math.max(num * 2f, num2), num3);
						using (ShapeDistanceReduction shapeDistanceReduction = new ShapeDistanceReduction(this.name, this, reductionWorkData, startMergeLength2, num3, 100, true, 1f))
						{
							shapeDistanceReduction.Reduction();
							if (shapeDistanceReduction.Result.IsError())
							{
								this.result = shapeDistanceReduction.Result;
								throw new MagicaClothProcessingException();
							}
						}
					}
					ct.ThrowIfCancellationRequested();
					this.Organization(settings, reductionWorkData);
					if (this.result.IsError())
					{
						throw new MagicaClothProcessingException();
					}
					ct.ThrowIfCancellationRequested();
					this.OrganizeStoreVirtualMesh(reductionWorkData);
					if (this.result.IsError())
					{
						throw new MagicaClothProcessingException();
					}
					this.CalcAverageAndMaxVertexDistanceRun();
					ct.ThrowIfCancellationRequested();
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!this.result.IsError())
				{
					this.result.SetError(Define.Result.Reduction_UnknownError);
				}
			}
			catch (OperationCanceledException)
			{
				this.result.SetCancel();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				this.result.SetError(Define.Result.Reduction_Exception);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00026A14 File Offset: 0x00024C14
		private void InitReductionWorkData(ReductionWorkData workData)
		{
			try
			{
				int vertexCount = this.VertexCount;
				int triangleCount = this.TriangleCount;
				workData.vertexJoinIndices = new NativeArray<int>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				JobUtility.FillRun(workData.vertexJoinIndices, vertexCount, -1);
				workData.vertexToVertexMap = new NativeParallelMultiHashMap<ushort, ushort>(vertexCount, Allocator.Persistent);
				new VirtualMesh.Reduction_InitVertexToVertexJob2
				{
					triangleCount = triangleCount,
					triangles = this.triangles.GetNativeArray(),
					vertexToVertexMap = workData.vertexToVertexMap
				}.Run<VirtualMesh.Reduction_InitVertexToVertexJob2>();
			}
			catch (Exception)
			{
				this.result.SetError(Define.Result.Reduction_InitError);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00026AB8 File Offset: 0x00024CB8
		private void Organization(ReductionSettings setting, ReductionWorkData workData)
		{
			try
			{
				this.OrganizationInit(setting, workData);
				this.OrganizationCreateRemapData(workData);
				this.OrganizationCreateBasicData(workData);
				this.OrganizationCreateLineTriangle(workData);
			}
			catch (Exception)
			{
				this.result.SetError(Define.Result.Reduction_OrganizationError);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00026B08 File Offset: 0x00024D08
		private void OrganizationInit(ReductionSettings setting, ReductionWorkData workData)
		{
			workData.oldVertexCount = this.VertexCount;
			workData.newVertexCount = workData.oldVertexCount - workData.removeVertexCount;
			int newVertexCount = workData.newVertexCount;
			workData.vertexRemapIndices = new NativeArray<int>(workData.oldVertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			workData.useSkinBoneMap = new NativeParallelHashMap<int, int>(this.SkinBoneCount, Allocator.Persistent);
			workData.newSkinBoneCount = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
			workData.newSkinBoneTransformIndices = new NativeList<int>(this.SkinBoneCount, Allocator.Persistent);
			workData.newSkinBoneBindPoseList = new NativeList<float4x4>(this.SkinBoneCount, Allocator.Persistent);
			workData.newAttributes = new ExSimpleNativeArray<VertexAttribute>(newVertexCount, false);
			workData.newLocalPositions = new ExSimpleNativeArray<float3>(newVertexCount, false);
			workData.newLocalNormals = new ExSimpleNativeArray<float3>(newVertexCount, false);
			workData.newLocalTangents = new ExSimpleNativeArray<float3>(newVertexCount, false);
			workData.newUv = new ExSimpleNativeArray<float2>(newVertexCount, false);
			workData.newBoneWeights = new ExSimpleNativeArray<VirtualMeshBoneWeight>(newVertexCount, false);
			workData.newVertexToVertexMap = new NativeParallelMultiHashMap<ushort, ushort>(newVertexCount, Allocator.Persistent);
			workData.edgeSet = new NativeParallelHashSet<int2>(newVertexCount * 2, Allocator.Persistent);
			workData.triangleSet = new NativeParallelHashSet<int3>(newVertexCount, Allocator.Persistent);
			workData.newLineList = new NativeList<int2>(newVertexCount, Allocator.Persistent);
			workData.newTriangleList = new NativeList<int3>(newVertexCount, Allocator.Persistent);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00026C50 File Offset: 0x00024E50
		private void OrganizationCreateRemapData(ReductionWorkData workData)
		{
			new VirtualMesh.Organize_RemapVertexJob
			{
				oldVertexCount = workData.oldVertexCount,
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices
			}.Run<VirtualMesh.Organize_RemapVertexJob>();
			new VirtualMesh.Organize_CollectUseSkinBoneJob
			{
				oldVertexCount = workData.oldVertexCount,
				joinIndices = workData.vertexJoinIndices,
				oldBoneWeights = this.boneWeights.GetNativeArray(),
				oldBindPoses = this.skinBoneBindPoses.GetNativeArray(),
				useSkinBoneMap = workData.useSkinBoneMap,
				newSkinBoneTransformIndices = workData.newSkinBoneTransformIndices,
				newSkinBoneBindPoses = workData.newSkinBoneBindPoseList,
				newSkinBoneCount = workData.newSkinBoneCount
			}.Run<VirtualMesh.Organize_CollectUseSkinBoneJob>();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00026D14 File Offset: 0x00024F14
		private void OrganizationCreateBasicData(ReductionWorkData workData)
		{
			int newVertexCount = workData.newVertexCount;
			int oldVertexCount = workData.oldVertexCount;
			new VirtualMesh.Organize_CopyVertexJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				oldAttributes = this.attributes.GetNativeArray(),
				oldLocalPositions = this.localPositions.GetNativeArray(),
				oldLocalNormals = this.localNormals.GetNativeArray(),
				oldLocalTangents = this.localTangents.GetNativeArray(),
				newAttributes = workData.newAttributes.GetNativeArray(),
				newLocalPositions = workData.newLocalPositions.GetNativeArray(),
				newLocalNormals = workData.newLocalNormals.GetNativeArray(),
				newLocalTangents = workData.newLocalTangents.GetNativeArray()
			}.Run(oldVertexCount);
			JobUtility.CalcUVWithSphereMappingRun(workData.newLocalPositions.GetNativeArray(), newVertexCount, workData.vmesh.boundingBox, workData.newUv.GetNativeArray());
			new VirtualMesh.Organize_RemapBoneWeightJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				useSkinBoneMap = workData.useSkinBoneMap,
				oldSkinBoneIndices = this.skinBoneTransformIndices.GetNativeArray(),
				oldBoneWeights = this.boneWeights.GetNativeArray(),
				newBoneWeights = workData.newBoneWeights.GetNativeArray()
			}.Run(oldVertexCount);
			new VirtualMesh.Organize_RemapLinkPointArrayJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				oldVertexToVertexMap = workData.vertexToVertexMap,
				newVertexToVertexMap = workData.newVertexToVertexMap
			}.Run(this.VertexCount);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00026EC4 File Offset: 0x000250C4
		private void OrganizationCreateLineTriangle(ReductionWorkData workData)
		{
			new VirtualMesh.Organize_CreateLineTriangleJob
			{
				newVertexCount = workData.newVertexCount,
				newVertexToVertexMap = workData.newVertexToVertexMap,
				edgeSet = workData.edgeSet
			}.Run<VirtualMesh.Organize_CreateLineTriangleJob>();
			new VirtualMesh.Organize_CreateLineTriangleJob2
			{
				newVertexToVertexMap = workData.newVertexToVertexMap,
				newLineList = workData.newLineList,
				edgeSet = workData.edgeSet,
				triangleSet = workData.triangleSet
			}.Run<VirtualMesh.Organize_CreateLineTriangleJob2>();
			new VirtualMesh.Organize_CreateNewTriangleJob3
			{
				newTriangleList = workData.newTriangleList,
				triangleSet = workData.triangleSet
			}.Run<VirtualMesh.Organize_CreateNewTriangleJob3>();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00026F70 File Offset: 0x00025170
		private void OrganizeStoreVirtualMesh(ReductionWorkData workData)
		{
			try
			{
				int newVertexCount = workData.newVertexCount;
				this.referenceIndices.Dispose();
				this.referenceIndices = new ExSimpleNativeArray<int>(newVertexCount, false);
				JobUtility.SerialNumberRun(this.referenceIndices.GetNativeArray(), newVertexCount);
				this.attributes.Dispose();
				this.attributes = workData.newAttributes;
				workData.newAttributes = null;
				this.localPositions.Dispose();
				this.localPositions = workData.newLocalPositions;
				workData.newLocalPositions = null;
				this.localNormals.Dispose();
				this.localNormals = workData.newLocalNormals;
				workData.newLocalNormals = null;
				this.localTangents.Dispose();
				this.localTangents = workData.newLocalTangents;
				workData.newLocalTangents = null;
				this.uv.Dispose();
				this.uv = workData.newUv;
				workData.newUv = null;
				this.boneWeights.Dispose();
				this.boneWeights = workData.newBoneWeights;
				workData.newBoneWeights = null;
				this.lines.Dispose();
				this.lines = new ExSimpleNativeArray<int2>(workData.newLineList);
				this.triangles.Dispose();
				this.triangles = new ExSimpleNativeArray<int3>(workData.newTriangleList);
				this.transformData.OrganizeReductionTransform(this, workData);
				this.skinBoneTransformIndices.Dispose();
				this.skinBoneTransformIndices = new ExSimpleNativeArray<int>(workData.newSkinBoneTransformIndices);
				this.skinBoneBindPoses.Dispose();
				this.skinBoneBindPoses = new ExSimpleNativeArray<float4x4>(workData.newSkinBoneBindPoseList);
				this.joinIndices = new NativeArray<int>(workData.vertexRemapIndices, Allocator.Persistent);
			}
			catch (Exception)
			{
				this.result.SetError(Define.Result.Reduction_StoreVirtualMeshError);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00027124 File Offset: 0x00025324
		internal void CalcAverageAndMaxVertexDistanceRun()
		{
			try
			{
				if (!this.averageVertexDistance.IsCreated)
				{
					this.averageVertexDistance = new NativeReference<float>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
				}
				if (!this.maxVertexDistance.IsCreated)
				{
					this.maxVertexDistance = new NativeReference<float>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
				}
				this.averageVertexDistance.Value = 0f;
				this.maxVertexDistance.Value = 0f;
				using (NativeReference<int> averageCount = new NativeReference<int>(Allocator.TempJob, NativeArrayOptions.ClearMemory))
				{
					if (this.TriangleCount > 0)
					{
						new VirtualMesh.Work_AverageTriangleDistanceJob
						{
							vcnt = this.VertexCount,
							tcnt = this.TriangleCount,
							localPositions = this.localPositions.GetNativeArray(),
							triangles = this.triangles.GetNativeArray(),
							averageVertexDistance = this.averageVertexDistance,
							averageCount = averageCount,
							maxVertexDistance = this.maxVertexDistance
						}.Run<VirtualMesh.Work_AverageTriangleDistanceJob>();
					}
					if (this.LineCount > 0)
					{
						new VirtualMesh.Work_AverageLineDistanceJob
						{
							vcnt = this.VertexCount,
							lcnt = this.LineCount,
							localPositions = this.localPositions.GetNativeArray(),
							lines = this.lines.GetNativeArray(),
							averageVertexDistance = this.averageVertexDistance,
							averageCount = averageCount,
							maxVertexDistance = this.maxVertexDistance
						}.Run<VirtualMesh.Work_AverageLineDistanceJob>();
					}
					int value = averageCount.Value;
					if (value > 0)
					{
						float value2 = this.averageVertexDistance.Value;
						this.averageVertexDistance.Value = math.sqrt(value2 / (float)value);
						this.maxVertexDistance.Value = math.sqrt(this.maxVertexDistance.Value);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.Reduction_CalcAverageException);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00027334 File Offset: 0x00025534
		internal GridMap<int> CreateVertexIndexGridMapRun(float gridSize)
		{
			int vertexCount = this.VertexCount;
			GridMap<int> gridMap = new GridMap<int>(vertexCount);
			if (vertexCount > 0)
			{
				new VirtualMesh.Work_AddVertexIndexGirdMapJob
				{
					gridSize = gridSize,
					vcnt = vertexCount,
					positins = this.localPositions.GetNativeArray(),
					gridMap = gridMap.GetMultiHashMap()
				}.Run<VirtualMesh.Work_AddVertexIndexGirdMapJob>();
			}
			return gridMap;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00027394 File Offset: 0x00025594
		public VirtualMeshRaycastHit IntersectRayMesh(float3 rayPos, float3 rayDir, bool doubleSide, float pointRadius)
		{
			Transform centerTransform = this.GetCenterTransform();
			float3 localRayPos = centerTransform.InverseTransformPoint(rayPos);
			float3 @float = centerTransform.InverseTransformDirection(rayDir);
			float3 v = rayPos + rayDir * 1000f;
			float3 localRayEndPos = centerTransform.InverseTransformPoint(v);
			float localEdgeRadius = pointRadius / centerTransform.lossyScale.x;
			int initialCapacity = 100;
			VirtualMeshRaycastHit virtualMeshRaycastHit;
			using (NativeList<VirtualMeshRaycastHit> hitList = new NativeList<VirtualMeshRaycastHit>(initialCapacity, Allocator.TempJob))
			{
				JobHandle dependsOn = default(JobHandle);
				dependsOn = new VirtualMesh.Work_IntersectTriangleJob
				{
					localRayPos = localRayPos,
					localRayDir = @float,
					localRayEndPos = localRayEndPos,
					doubleSide = doubleSide,
					localPositions = this.localPositions.GetNativeArray(),
					triangles = this.triangles.GetNativeArray(),
					hitList = hitList.AsParallelWriter()
				}.Schedule(this.TriangleCount, 16, dependsOn);
				dependsOn = new VirtualMesh.Work_IntersectEdgeJob
				{
					localRayPos = localRayPos,
					localRayDir = @float,
					localRayEndPos = localRayEndPos,
					rayDir = @float,
					localEdgeRadius = localEdgeRadius,
					localPositions = this.localPositions.GetNativeArray(),
					edges = this.edges,
					edgeToTriangles = this.edgeToTriangles,
					hitList = hitList.AsParallelWriter()
				}.Schedule(this.EdgeCount, 16, dependsOn);
				new VirtualMesh.Work_IntersetcSortJob
				{
					hitList = hitList
				}.Schedule(dependsOn).Complete();
				VirtualMeshRaycastHit virtualMeshRaycastHit2;
				if (hitList.Length <= 0)
				{
					virtualMeshRaycastHit = default(VirtualMeshRaycastHit);
					virtualMeshRaycastHit2 = virtualMeshRaycastHit;
				}
				else
				{
					virtualMeshRaycastHit2 = hitList[0];
				}
				virtualMeshRaycastHit = virtualMeshRaycastHit2;
			}
			return virtualMeshRaycastHit;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0002757C File Offset: 0x0002577C
		public float InitCalcScale
		{
			get
			{
				return this.initScale.x;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00027589 File Offset: 0x00025789
		public bool IsSuccess
		{
			get
			{
				return this.result.IsSuccess();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00027596 File Offset: 0x00025796
		public bool IsError
		{
			get
			{
				return this.result.IsError();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000275A3 File Offset: 0x000257A3
		public bool IsProcess
		{
			get
			{
				return this.result.IsProcess();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000275B0 File Offset: 0x000257B0
		public int VertexCount
		{
			get
			{
				return this.localPositions.Count;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000275BD File Offset: 0x000257BD
		public int TriangleCount
		{
			get
			{
				return this.triangles.Count;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000275CA File Offset: 0x000257CA
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000275D7 File Offset: 0x000257D7
		public int SkinBoneCount
		{
			get
			{
				return this.skinBoneTransformIndices.Count;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000275E4 File Offset: 0x000257E4
		public int TransformCount
		{
			get
			{
				TransformData transformData = this.transformData;
				if (transformData == null)
				{
					return 0;
				}
				return transformData.Count;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000275F7 File Offset: 0x000257F7
		public bool IsProxy
		{
			get
			{
				return this.meshType == VirtualMesh.MeshType.ProxyMesh || this.meshType == VirtualMesh.MeshType.ProxyBoneMesh;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0002760D File Offset: 0x0002580D
		public bool IsMapping
		{
			get
			{
				return this.meshType == VirtualMesh.MeshType.Mapping;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00027618 File Offset: 0x00025818
		public int BaseLineCount
		{
			get
			{
				if (!this.baseLineStartDataIndices.IsCreated)
				{
					return 0;
				}
				return this.baseLineStartDataIndices.Length;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00027634 File Offset: 0x00025834
		public int EdgeCount
		{
			get
			{
				if (!this.edges.IsCreated)
				{
					return 0;
				}
				return this.edges.Length;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00027650 File Offset: 0x00025850
		public int CustomSkinningBoneCount
		{
			get
			{
				int[] array = this.customSkinningBoneIndices;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00027660 File Offset: 0x00025860
		public int CenterFixedPointCount
		{
			get
			{
				ushort[] array = this.centerFixedList;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00027670 File Offset: 0x00025870
		public int NormalAdjustmentRotationCount
		{
			get
			{
				if (!this.normalAdjustmentRotations.IsCreated)
				{
					return 0;
				}
				return this.normalAdjustmentRotations.Length;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002768C File Offset: 0x0002588C
		public VirtualMesh()
		{
			this.transformData = new TransformData();
			this.averageVertexDistance = new NativeReference<float>(0f, Allocator.Persistent);
			this.maxVertexDistance = new NativeReference<float>(0f, Allocator.Persistent);
			this.result.SetProcess();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00027773 File Offset: 0x00025973
		public VirtualMesh(string name) : this()
		{
			this.name = name;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00027784 File Offset: 0x00025984
		public void Dispose()
		{
			this.result.Clear();
			this.referenceIndices.Dispose();
			this.attributes.Dispose();
			this.localPositions.Dispose();
			this.localNormals.Dispose();
			this.localTangents.Dispose();
			this.uv.Dispose();
			this.boneWeights.Dispose();
			this.triangles.Dispose();
			this.lines.Dispose();
			this.skinBoneTransformIndices.Dispose();
			this.skinBoneBindPoses.Dispose();
			if (this.joinIndices.IsCreated)
			{
				this.joinIndices.Dispose();
			}
			if (this.vertexToTriangles.IsCreated)
			{
				this.vertexToTriangles.Dispose();
			}
			if (this.vertexToVertexIndexArray.IsCreated)
			{
				this.vertexToVertexIndexArray.Dispose();
			}
			if (this.vertexToVertexDataArray.IsCreated)
			{
				this.vertexToVertexDataArray.Dispose();
			}
			if (this.edges.IsCreated)
			{
				this.edges.Dispose();
			}
			if (this.edgeFlags.IsCreated)
			{
				this.edgeFlags.Dispose();
			}
			if (this.edgeToTriangles.IsCreated)
			{
				this.edgeToTriangles.Dispose();
			}
			if (this.vertexBindPosePositions.IsCreated)
			{
				this.vertexBindPosePositions.Dispose();
			}
			if (this.vertexBindPoseRotations.IsCreated)
			{
				this.vertexBindPoseRotations.Dispose();
			}
			if (this.vertexToTransformRotations.IsCreated)
			{
				this.vertexToTransformRotations.Dispose();
			}
			if (this.vertexRootIndices.IsCreated)
			{
				this.vertexRootIndices.Dispose();
			}
			if (this.vertexParentIndices.IsCreated)
			{
				this.vertexParentIndices.Dispose();
			}
			if (this.vertexChildIndexArray.IsCreated)
			{
				this.vertexChildIndexArray.Dispose();
			}
			if (this.vertexChildDataArray.IsCreated)
			{
				this.vertexChildDataArray.Dispose();
			}
			if (this.baseLineFlags.IsCreated)
			{
				this.baseLineFlags.Dispose();
			}
			if (this.baseLineStartDataIndices.IsCreated)
			{
				this.baseLineStartDataIndices.Dispose();
			}
			if (this.baseLineDataCounts.IsCreated)
			{
				this.baseLineDataCounts.Dispose();
			}
			if (this.baseLineData.IsCreated)
			{
				this.baseLineData.Dispose();
			}
			if (this.localCenterPosition.IsCreated)
			{
				this.localCenterPosition.Dispose();
			}
			if (this.vertexLocalPositions.IsCreated)
			{
				this.vertexLocalPositions.Dispose();
			}
			if (this.vertexLocalRotations.IsCreated)
			{
				this.vertexLocalRotations.Dispose();
			}
			if (this.normalAdjustmentRotations.IsCreated)
			{
				this.normalAdjustmentRotations.Dispose();
			}
			if (this.vertexDepths.IsCreated)
			{
				this.vertexDepths.Dispose();
			}
			if (this.boundingBox.IsCreated)
			{
				this.boundingBox.Dispose();
			}
			if (this.averageVertexDistance.IsCreated)
			{
				this.averageVertexDistance.Dispose();
			}
			if (this.maxVertexDistance.IsCreated)
			{
				this.maxVertexDistance.Dispose();
			}
			TransformData transformData = this.transformData;
			if (transformData == null)
			{
				return;
			}
			transformData.Dispose();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00027A95 File Offset: 0x00025C95
		public void SetName(string newName)
		{
			this.name = newName;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00027AA0 File Offset: 0x00025CA0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("===== " + this.name + " =====");
			stringBuilder.Append(string.Format("Result:{0}", this.result));
			stringBuilder.Append(string.Format(", Type:{0}", this.meshType));
			stringBuilder.Append(string.Format(", Vertex:{0}", this.VertexCount));
			stringBuilder.Append(string.Format(", Line:{0}", this.LineCount));
			stringBuilder.Append(string.Format(", Triangle:{0}", this.TriangleCount));
			stringBuilder.Append(string.Format(", Edge:{0}", this.EdgeCount));
			stringBuilder.Append(string.Format(", SkinBone:{0}", this.SkinBoneCount));
			StringBuilder stringBuilder2 = stringBuilder;
			string format = ", Transform:{0}";
			TransformData transformData = this.transformData;
			stringBuilder2.Append(string.Format(format, (transformData != null) ? new int?(transformData.Count) : null));
			stringBuilder.Append(string.Format(", BaseLine:{0}", this.BaseLineCount));
			stringBuilder.AppendLine();
			if (this.averageVertexDistance.IsCreated)
			{
				stringBuilder.Append(string.Format("avgDist:{0}", this.averageVertexDistance.Value));
			}
			if (this.maxVertexDistance.IsCreated)
			{
				stringBuilder.Append(string.Format(", maxDist:{0}", this.maxVertexDistance.Value));
			}
			if (this.boundingBox.IsCreated)
			{
				stringBuilder.Append(string.Format(", AABB:{0}", this.boundingBox.Value));
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x04000652 RID: 1618
		public string name = string.Empty;

		// Token: 0x04000653 RID: 1619
		public ResultCode result;

		// Token: 0x04000654 RID: 1620
		public VirtualMesh.MeshType meshType;

		// Token: 0x04000655 RID: 1621
		public bool isBoneCloth;

		// Token: 0x04000656 RID: 1622
		public ExSimpleNativeArray<int> referenceIndices = new ExSimpleNativeArray<int>();

		// Token: 0x04000657 RID: 1623
		public ExSimpleNativeArray<VertexAttribute> attributes = new ExSimpleNativeArray<VertexAttribute>();

		// Token: 0x04000658 RID: 1624
		public ExSimpleNativeArray<float3> localPositions = new ExSimpleNativeArray<float3>();

		// Token: 0x04000659 RID: 1625
		public ExSimpleNativeArray<float3> localNormals = new ExSimpleNativeArray<float3>();

		// Token: 0x0400065A RID: 1626
		public ExSimpleNativeArray<float3> localTangents = new ExSimpleNativeArray<float3>();

		// Token: 0x0400065B RID: 1627
		public ExSimpleNativeArray<float2> uv = new ExSimpleNativeArray<float2>();

		// Token: 0x0400065C RID: 1628
		public ExSimpleNativeArray<VirtualMeshBoneWeight> boneWeights = new ExSimpleNativeArray<VirtualMeshBoneWeight>();

		// Token: 0x0400065D RID: 1629
		public ExSimpleNativeArray<int3> triangles = new ExSimpleNativeArray<int3>();

		// Token: 0x0400065E RID: 1630
		public ExSimpleNativeArray<int2> lines = new ExSimpleNativeArray<int2>();

		// Token: 0x0400065F RID: 1631
		public int centerTransformIndex = -1;

		// Token: 0x04000660 RID: 1632
		public float4x4 initLocalToWorld;

		// Token: 0x04000661 RID: 1633
		public float4x4 initWorldToLocal;

		// Token: 0x04000662 RID: 1634
		public quaternion initRotation;

		// Token: 0x04000663 RID: 1635
		public quaternion initInverseRotation;

		// Token: 0x04000664 RID: 1636
		public float3 initScale;

		// Token: 0x04000665 RID: 1637
		public int skinRootIndex = -1;

		// Token: 0x04000666 RID: 1638
		public ExSimpleNativeArray<int> skinBoneTransformIndices = new ExSimpleNativeArray<int>();

		// Token: 0x04000667 RID: 1639
		public ExSimpleNativeArray<float4x4> skinBoneBindPoses = new ExSimpleNativeArray<float4x4>();

		// Token: 0x04000668 RID: 1640
		public TransformData transformData;

		// Token: 0x04000669 RID: 1641
		public NativeReference<AABB> boundingBox;

		// Token: 0x0400066A RID: 1642
		public NativeReference<float> averageVertexDistance;

		// Token: 0x0400066B RID: 1643
		public NativeReference<float> maxVertexDistance;

		// Token: 0x0400066C RID: 1644
		public DataChunk mergeChunk;

		// Token: 0x0400066D RID: 1645
		public NativeArray<int> joinIndices;

		// Token: 0x0400066E RID: 1646
		public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

		// Token: 0x0400066F RID: 1647
		public NativeArray<uint> vertexToVertexIndexArray;

		// Token: 0x04000670 RID: 1648
		public NativeArray<ushort> vertexToVertexDataArray;

		// Token: 0x04000671 RID: 1649
		public NativeArray<int2> edges;

		// Token: 0x04000672 RID: 1650
		public const byte EdgeFlag_Cut = 1;

		// Token: 0x04000673 RID: 1651
		public NativeArray<ExBitFlag8> edgeFlags;

		// Token: 0x04000674 RID: 1652
		public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

		// Token: 0x04000675 RID: 1653
		public NativeArray<float3> vertexBindPosePositions;

		// Token: 0x04000676 RID: 1654
		public NativeArray<quaternion> vertexBindPoseRotations;

		// Token: 0x04000677 RID: 1655
		public NativeArray<quaternion> vertexToTransformRotations;

		// Token: 0x04000678 RID: 1656
		public NativeArray<float> vertexDepths;

		// Token: 0x04000679 RID: 1657
		public NativeArray<int> vertexRootIndices;

		// Token: 0x0400067A RID: 1658
		public NativeArray<int> vertexParentIndices;

		// Token: 0x0400067B RID: 1659
		public NativeArray<uint> vertexChildIndexArray;

		// Token: 0x0400067C RID: 1660
		public NativeArray<ushort> vertexChildDataArray;

		// Token: 0x0400067D RID: 1661
		public NativeArray<float3> vertexLocalPositions;

		// Token: 0x0400067E RID: 1662
		public NativeArray<quaternion> vertexLocalRotations;

		// Token: 0x0400067F RID: 1663
		public NativeArray<quaternion> normalAdjustmentRotations;

		// Token: 0x04000680 RID: 1664
		public const byte BaseLineFlag_IncludeLine = 1;

		// Token: 0x04000681 RID: 1665
		public NativeArray<ExBitFlag8> baseLineFlags;

		// Token: 0x04000682 RID: 1666
		public NativeArray<ushort> baseLineStartDataIndices;

		// Token: 0x04000683 RID: 1667
		public NativeArray<ushort> baseLineDataCounts;

		// Token: 0x04000684 RID: 1668
		public NativeArray<ushort> baseLineData;

		// Token: 0x04000685 RID: 1669
		public int[] customSkinningBoneIndices;

		// Token: 0x04000686 RID: 1670
		public ushort[] centerFixedList;

		// Token: 0x04000687 RID: 1671
		public NativeReference<float3> localCenterPosition;

		// Token: 0x04000688 RID: 1672
		public VirtualMesh mappingProxyMesh;

		// Token: 0x04000689 RID: 1673
		public float3 centerWorldPosition;

		// Token: 0x0400068A RID: 1674
		public quaternion centerWorldRotation;

		// Token: 0x0400068B RID: 1675
		public float3 centerWorldScale;

		// Token: 0x0400068C RID: 1676
		public float4x4 toProxyMatrix;

		// Token: 0x0400068D RID: 1677
		public quaternion toProxyRotation;

		// Token: 0x0400068E RID: 1678
		public int mappingId;

		// Token: 0x020000FA RID: 250
		[BurstCompile]
		private struct Import_GenerateTangentJob : IJobParallelFor
		{
			// Token: 0x060004E4 RID: 1252 RVA: 0x00027C84 File Offset: 0x00025E84
			public void Execute(int vindex)
			{
				float3 x = this.localNormals[vindex];
				float3 @float = math.up();
				if ((double)math.dot(x, @float) < 0.9)
				{
					@float = math.normalize(math.cross(x, @float));
				}
				else
				{
					@float = math.normalize(math.cross(x, math.right()));
				}
				this.localTangents[vindex] = @float;
			}

			// Token: 0x0400068F RID: 1679
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x04000690 RID: 1680
			[WriteOnly]
			public NativeArray<float3> localTangents;
		}

		// Token: 0x020000FB RID: 251
		[BurstCompile]
		private struct Import_CalcSkinningJob : IJobParallelFor
		{
			// Token: 0x060004E5 RID: 1253 RVA: 0x00027CE4 File Offset: 0x00025EE4
			public void Execute(int vindex)
			{
				VirtualMeshBoneWeight virtualMeshBoneWeight = this.boneWeights[vindex];
				int count = virtualMeshBoneWeight.Count;
				float3 lhs = 0;
				float3 lhs2 = 0;
				float3 lhs3 = 0;
				for (int i = 0; i < count; i++)
				{
					float rhs = virtualMeshBoneWeight.weights[i];
					int index = virtualMeshBoneWeight.boneIndices[i];
					float4x4 a = this.bindPoses[index];
					float4 b = new float4(this.localPositions[vindex], 1f);
					float4 b2 = new float4(this.localNormals[vindex], 0f);
					float4 b3 = new float4(this.localTangents[vindex], 0f);
					float3 xyz = math.mul(a, b).xyz;
					float3 xyz2 = math.mul(a, b2).xyz;
					float3 xyz3 = math.mul(a, b3).xyz;
					int index2 = this.skinBoneTransformIndices[index];
					float3 @float = this.transformPositionArray[index2];
					quaternion quaternion = this.transformRotationArray[index2];
					float3 float2 = this.transformScaleArray[index2];
					MathUtility.TransformPositionNormalTangent(@float, quaternion, float2, ref xyz, ref xyz2, ref xyz3);
					lhs += xyz * rhs;
					lhs2 += xyz2 * rhs;
					lhs3 += xyz3 * rhs;
				}
				this.localPositions[vindex] = MathUtility.TransformPoint(lhs, this.toM);
				this.localNormals[vindex] = MathUtility.TransformDirection(lhs2, this.toM);
				this.localTangents[vindex] = MathUtility.TransformDirection(lhs3, this.toM);
			}

			// Token: 0x04000691 RID: 1681
			public NativeArray<float3> localPositions;

			// Token: 0x04000692 RID: 1682
			public NativeArray<float3> localNormals;

			// Token: 0x04000693 RID: 1683
			public NativeArray<float3> localTangents;

			// Token: 0x04000694 RID: 1684
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x04000695 RID: 1685
			[ReadOnly]
			public NativeArray<int> skinBoneTransformIndices;

			// Token: 0x04000696 RID: 1686
			[ReadOnly]
			public NativeArray<float4x4> bindPoses;

			// Token: 0x04000697 RID: 1687
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000698 RID: 1688
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000699 RID: 1689
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			// Token: 0x0400069A RID: 1690
			public float4x4 toM;
		}

		// Token: 0x020000FC RID: 252
		[BurstCompile]
		private struct Import_BoneWeightJob1 : IJob
		{
			// Token: 0x060004E6 RID: 1254 RVA: 0x00027EAC File Offset: 0x000260AC
			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < this.vcnt; i++)
				{
					this.startBoneWeightIndices[i] = num;
					num += (int)this.bonesPerVertexArray[i];
				}
			}

			// Token: 0x0400069B RID: 1691
			public int vcnt;

			// Token: 0x0400069C RID: 1692
			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			// Token: 0x0400069D RID: 1693
			[WriteOnly]
			public NativeArray<int> startBoneWeightIndices;
		}

		// Token: 0x020000FD RID: 253
		[BurstCompile]
		private struct Import_BoneWeightJob2 : IJobParallelFor
		{
			// Token: 0x060004E7 RID: 1255 RVA: 0x00027EE8 File Offset: 0x000260E8
			public void Execute(int vindex)
			{
				int num = this.startBoneWeightIndices[vindex];
				int num2 = (int)this.bonesPerVertexArray[vindex];
				VirtualMeshBoneWeight value = default(VirtualMeshBoneWeight);
				int num3 = 0;
				int num4 = 0;
				while (num4 < num2 && num4 < 4)
				{
					BoneWeight1 boneWeight = this.boneWeightArray[num + num4];
					if (boneWeight.weight > 0f)
					{
						value.weights[num3] = boneWeight.weight;
						value.boneIndices[num3] = boneWeight.boneIndex;
						num3++;
					}
					num4++;
				}
				if (num2 > 4)
				{
					value.AdjustWeight();
				}
				this.boneWeights[vindex] = value;
			}

			// Token: 0x0400069E RID: 1694
			[ReadOnly]
			public NativeArray<int> startBoneWeightIndices;

			// Token: 0x0400069F RID: 1695
			[ReadOnly]
			public NativeArray<BoneWeight1> boneWeightArray;

			// Token: 0x040006A0 RID: 1696
			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			// Token: 0x040006A1 RID: 1697
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;
		}

		// Token: 0x020000FE RID: 254
		[BurstCompile]
		private struct Import_BoneVertexJob : IJobParallelFor
		{
			// Token: 0x060004E8 RID: 1256 RVA: 0x00027F94 File Offset: 0x00026194
			public void Execute(int vindex)
			{
				float3 translation = this.transformPositions[vindex];
				quaternion quaternion = this.transformRotations[vindex];
				float3 scale = this.transformScales[vindex];
				float3 value = MathUtility.InverseTransformPoint(translation, this.WtoL);
				float3 value2 = math.mul(quaternion, math.up());
				float3 value3 = math.mul(quaternion, math.forward());
				value2 = MathUtility.InverseTransformDirection(value2, this.WtoL);
				value3 = MathUtility.InverseTransformDirection(value3, this.WtoL);
				this.localPositions[vindex] = value;
				this.localNormals[vindex] = value2;
				this.localTangents[vindex] = value3;
				VirtualMeshBoneWeight value4 = new VirtualMeshBoneWeight(new int4(vindex, 0, 0, 0), new float4(1f, 0f, 0f, 0f));
				this.boneWeights[vindex] = value4;
				float4x4 value5 = math.mul(math.inverse(float4x4.TRS(translation, quaternion, scale)), this.LtoW);
				this.skinBoneBindPoses[vindex] = value5;
			}

			// Token: 0x040006A2 RID: 1698
			public float4x4 WtoL;

			// Token: 0x040006A3 RID: 1699
			public float4x4 LtoW;

			// Token: 0x040006A4 RID: 1700
			[ReadOnly]
			public NativeArray<float3> transformPositions;

			// Token: 0x040006A5 RID: 1701
			[ReadOnly]
			public NativeArray<quaternion> transformRotations;

			// Token: 0x040006A6 RID: 1702
			[ReadOnly]
			public NativeArray<float3> transformScales;

			// Token: 0x040006A7 RID: 1703
			[WriteOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040006A8 RID: 1704
			[WriteOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x040006A9 RID: 1705
			[WriteOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x040006AA RID: 1706
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x040006AB RID: 1707
			[WriteOnly]
			public NativeArray<float4x4> skinBoneBindPoses;
		}

		// Token: 0x020000FF RID: 255
		[BurstCompile]
		private struct Select_PackVertexJob : IJob
		{
			// Token: 0x060004E9 RID: 1257 RVA: 0x00028098 File Offset: 0x00026298
			public void Execute()
			{
				for (int i = 0; i < this.vertexCount; i++)
				{
					int num = this.newVertexRemapIndices[i];
					if (num >= 0)
					{
						this.newReferenceIndices[num] = i;
						this.newAttributes[num] = this.attributes[i];
						this.newLocalPositions[num] = this.localPositions[i];
						this.newLocalNormals[num] = this.localNormals[i];
						this.newLocalTangents[num] = this.localTangents[i];
						this.newUv[num] = this.uv[i];
						this.newBoneWeights[num] = this.boneWeights[i];
					}
				}
			}

			// Token: 0x040006AC RID: 1708
			public int vertexCount;

			// Token: 0x040006AD RID: 1709
			[ReadOnly]
			public NativeArray<int> newVertexRemapIndices;

			// Token: 0x040006AE RID: 1710
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040006AF RID: 1711
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040006B0 RID: 1712
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x040006B1 RID: 1713
			[ReadOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x040006B2 RID: 1714
			[ReadOnly]
			public NativeArray<float2> uv;

			// Token: 0x040006B3 RID: 1715
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x040006B4 RID: 1716
			[WriteOnly]
			public NativeArray<int> newReferenceIndices;

			// Token: 0x040006B5 RID: 1717
			[WriteOnly]
			public NativeArray<VertexAttribute> newAttributes;

			// Token: 0x040006B6 RID: 1718
			[WriteOnly]
			public NativeArray<float3> newLocalPositions;

			// Token: 0x040006B7 RID: 1719
			[WriteOnly]
			public NativeArray<float3> newLocalNormals;

			// Token: 0x040006B8 RID: 1720
			[WriteOnly]
			public NativeArray<float3> newLocalTangents;

			// Token: 0x040006B9 RID: 1721
			[WriteOnly]
			public NativeArray<float2> newUv;

			// Token: 0x040006BA RID: 1722
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> newBoneWeights;
		}

		// Token: 0x02000100 RID: 256
		[BurstCompile]
		private struct Select_GridJob : IJob
		{
			// Token: 0x060004EA RID: 1258 RVA: 0x00028170 File Offset: 0x00026370
			public void Execute()
			{
				for (int i = 0; i < this.vertexCount; i++)
				{
					float3 @float = this.meshPositions[i];
					int num = -1;
					foreach (int3 key in GridMap<int>.GetArea(@float, this.searchRadius, this.gridMap, this.gridSize))
					{
						if (this.gridMap.ContainsKey(key))
						{
							foreach (int index in this.gridMap.GetValuesForKey(key))
							{
								float3 y = this.selectionPositions[index];
								if (math.distance(@float, y) <= this.searchRadius)
								{
									num = 1;
									break;
								}
							}
							if (num >= 0)
							{
								break;
							}
						}
					}
					this.newVertexRemapIndices[i] = num;
				}
				for (int j = 0; j < this.triangleCount; j++)
				{
					int3 @int = this.meshTriangles[j];
					if (this.newVertexRemapIndices[@int.x] >= 0 || this.newVertexRemapIndices[@int.y] >= 0 || this.newVertexRemapIndices[@int.z] >= 0)
					{
						this.newTriangles.Add(@int);
					}
				}
				int length = this.newTriangles.Length;
				for (int k = 0; k < length; k++)
				{
					int3 int2 = this.newTriangles[k];
					this.newVertexRemapIndices[int2.x] = 1;
					this.newVertexRemapIndices[int2.y] = 1;
					this.newVertexRemapIndices[int2.z] = 1;
				}
				int num2 = 0;
				for (int l = 0; l < this.vertexCount; l++)
				{
					if (this.newVertexRemapIndices[l] >= 0)
					{
						this.newVertexRemapIndices[l] = num2;
						num2++;
					}
				}
				this.newVertexCount.Value = num2;
				int length2 = this.newTriangles.Length;
				for (int m = 0; m < length2; m++)
				{
					int3 int3 = this.newTriangles[m];
					int3.x = this.newVertexRemapIndices[int3.x];
					int3.y = this.newVertexRemapIndices[int3.y];
					int3.z = this.newVertexRemapIndices[int3.z];
					this.newTriangles[m] = int3;
				}
			}

			// Token: 0x040006BB RID: 1723
			public float gridSize;

			// Token: 0x040006BC RID: 1724
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x040006BD RID: 1725
			public int selectionCount;

			// Token: 0x040006BE RID: 1726
			[ReadOnly]
			public NativeArray<float3> selectionPositions;

			// Token: 0x040006BF RID: 1727
			[ReadOnly]
			public NativeArray<VertexAttribute> selectionAttributes;

			// Token: 0x040006C0 RID: 1728
			public int vertexCount;

			// Token: 0x040006C1 RID: 1729
			public int triangleCount;

			// Token: 0x040006C2 RID: 1730
			public float searchRadius;

			// Token: 0x040006C3 RID: 1731
			[ReadOnly]
			public NativeArray<float3> meshPositions;

			// Token: 0x040006C4 RID: 1732
			[ReadOnly]
			public NativeArray<int3> meshTriangles;

			// Token: 0x040006C5 RID: 1733
			public NativeList<int3> newTriangles;

			// Token: 0x040006C6 RID: 1734
			public NativeArray<int> newVertexRemapIndices;

			// Token: 0x040006C7 RID: 1735
			[WriteOnly]
			public NativeReference<int> newVertexCount;
		}

		// Token: 0x02000101 RID: 257
		[BurstCompile]
		private struct Add_CalcBindPoseJob : IJobParallelFor
		{
			// Token: 0x060004EB RID: 1259 RVA: 0x00028434 File Offset: 0x00026634
			public void Execute(int boneIndex)
			{
				int index = this.srcSkinBoneTransformIndices[boneIndex];
				float3 translation = this.srcTransformPositionArray[index];
				quaternion rotation = this.srcTransformRotationArray[index];
				float3 scale = this.srcTransformScaleArray[index];
				float4x4 value = math.mul(math.inverse(float4x4.TRS(translation, rotation, scale)), this.dstCenterLocalToWorldMatrix);
				this.dstSkinBoneBindPoses[this.skinBoneOffset + boneIndex] = value;
			}

			// Token: 0x040006C8 RID: 1736
			public int skinBoneOffset;

			// Token: 0x040006C9 RID: 1737
			[ReadOnly]
			public NativeArray<int> srcSkinBoneTransformIndices;

			// Token: 0x040006CA RID: 1738
			[ReadOnly]
			public NativeArray<float3> srcTransformPositionArray;

			// Token: 0x040006CB RID: 1739
			[ReadOnly]
			public NativeArray<quaternion> srcTransformRotationArray;

			// Token: 0x040006CC RID: 1740
			[ReadOnly]
			public NativeArray<float3> srcTransformScaleArray;

			// Token: 0x040006CD RID: 1741
			public float4x4 dstCenterLocalToWorldMatrix;

			// Token: 0x040006CE RID: 1742
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float4x4> dstSkinBoneBindPoses;
		}

		// Token: 0x02000102 RID: 258
		[BurstCompile]
		private struct Add_CopyVerticesJob : IJobParallelFor
		{
			// Token: 0x060004EC RID: 1260 RVA: 0x000284A0 File Offset: 0x000266A0
			public void Execute(int vindex)
			{
				int index = this.vertexOffset + vindex;
				float3 value = this.srclocalPositions[vindex];
				float3 value2 = this.srclocalNormals[vindex];
				float3 value3 = this.srclocalTangents[vindex];
				value = MathUtility.TransformPoint(value, this.toM);
				value2 = MathUtility.TransformDirection(value2, this.toM);
				value3 = MathUtility.TransformDirection(value3, this.toM);
				this.dstlocalPositions[index] = value;
				this.dstlocalNormals[index] = value2;
				this.dstlocalTangents[index] = value3;
				this.dstUV[index] = this.srcUV[vindex];
				if (vindex < this.srcBoneWeights.Length)
				{
					VirtualMeshBoneWeight value4 = this.srcBoneWeights[vindex];
					value4.boneIndices += this.skinBoneOffset;
					for (int i = 0; i < 4; i++)
					{
						if (value4.weights[i] >= 1E-06f)
						{
							int num = value4.boneIndices[i];
							int num2 = this.dstSkinBoneIndices[num];
							for (int j = 0; j < num; j++)
							{
								if (this.dstSkinBoneIndices[j] == num2)
								{
									value4.boneIndices[i] = j;
									break;
								}
							}
						}
					}
					this.dstBoneWeights[index] = value4;
				}
				this.dstAttributes[index] = this.srcAttributes[vindex];
			}

			// Token: 0x040006CF RID: 1743
			public int vertexOffset;

			// Token: 0x040006D0 RID: 1744
			public int skinBoneOffset;

			// Token: 0x040006D1 RID: 1745
			public float4x4 toM;

			// Token: 0x040006D2 RID: 1746
			[ReadOnly]
			public NativeArray<VertexAttribute> srcAttributes;

			// Token: 0x040006D3 RID: 1747
			[ReadOnly]
			public NativeArray<float3> srclocalPositions;

			// Token: 0x040006D4 RID: 1748
			[ReadOnly]
			public NativeArray<float3> srclocalNormals;

			// Token: 0x040006D5 RID: 1749
			[ReadOnly]
			public NativeArray<float3> srclocalTangents;

			// Token: 0x040006D6 RID: 1750
			[ReadOnly]
			public NativeArray<float2> srcUV;

			// Token: 0x040006D7 RID: 1751
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> srcBoneWeights;

			// Token: 0x040006D8 RID: 1752
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> dstAttributes;

			// Token: 0x040006D9 RID: 1753
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalPositions;

			// Token: 0x040006DA RID: 1754
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalNormals;

			// Token: 0x040006DB RID: 1755
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalTangents;

			// Token: 0x040006DC RID: 1756
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float2> dstUV;

			// Token: 0x040006DD RID: 1757
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> dstBoneWeights;

			// Token: 0x040006DE RID: 1758
			[ReadOnly]
			public NativeArray<int> dstSkinBoneIndices;
		}

		// Token: 0x02000103 RID: 259
		private struct MappingWorkData
		{
			// Token: 0x040006DF RID: 1759
			public float3 position;

			// Token: 0x040006E0 RID: 1760
			public int vertexIndex;

			// Token: 0x040006E1 RID: 1761
			public int proxyVertexIndex;

			// Token: 0x040006E2 RID: 1762
			public float proxyVertexDistance;
		}

		// Token: 0x02000104 RID: 260
		[BurstCompile]
		private struct Mapping_DirectConnectionVertexDataJob : IJob
		{
			// Token: 0x060004ED RID: 1261 RVA: 0x00028620 File Offset: 0x00026820
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					int num = this.joinIndices[this.mergeChunk.startIndex + i];
					VertexAttribute value = this.proxyAttributes[num];
					if (value.IsInvalid())
					{
						this.attributes[i] = VertexAttribute.Invalid;
					}
					else
					{
						float3 @float = this.localPositions[i];
						float3 float2 = MathUtility.TransformPoint(@float, this.toP);
						float3 y = this.proxyLocalPositions[num];
						VirtualMesh.MappingWorkData value2 = new VirtualMesh.MappingWorkData
						{
							position = float2,
							vertexIndex = i,
							proxyVertexIndex = num,
							proxyVertexDistance = math.distance(float2, y)
						};
						this.mappingWorkData[i] = value2;
						this.attributes[i] = value;
					}
				}
			}

			// Token: 0x040006E3 RID: 1763
			public float4x4 toP;

			// Token: 0x040006E4 RID: 1764
			public int vcnt;

			// Token: 0x040006E5 RID: 1765
			public DataChunk mergeChunk;

			// Token: 0x040006E6 RID: 1766
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040006E7 RID: 1767
			[WriteOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040006E8 RID: 1768
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x040006E9 RID: 1769
			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			// Token: 0x040006EA RID: 1770
			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			// Token: 0x040006EB RID: 1771
			[WriteOnly]
			public NativeArray<VirtualMesh.MappingWorkData> mappingWorkData;
		}

		// Token: 0x02000105 RID: 261
		private struct Mapping_CalcDirectWeightJob : IJob
		{
			// Token: 0x060004EE RID: 1262 RVA: 0x00028700 File Offset: 0x00026900
			public void Execute()
			{
				NativeParallelHashSet<ushort> nativeParallelHashSet = new NativeParallelHashSet<ushort>(1024, Allocator.Temp);
				FixedList4096Bytes<ushort> fixedList4096Bytes = default(FixedList4096Bytes<ushort>);
				for (int i = 0; i < this.vcnt; i++)
				{
					if (!this.attributes[i].IsInvalid())
					{
						VirtualMesh.MappingWorkData mappingWorkData = this.mappingWorkData[i];
						ushort num = (ushort)mappingWorkData.proxyVertexIndex;
						nativeParallelHashSet.Clear();
						fixedList4096Bytes.Clear();
						ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
						ref fixedList4096Bytes.Push(num);
						while (!fixedList4096Bytes.IsEmpty)
						{
							num = ref fixedList4096Bytes.Pop<ushort>();
							if (!nativeParallelHashSet.Contains(num))
							{
								nativeParallelHashSet.Add(num);
								float num2 = math.distance(mappingWorkData.position, this.proxyLocalPositions[(int)num]);
								if (num2 <= this.weightLength)
								{
									float num3 = Mathf.Clamp01(1f - num2 / this.weightLength + 0.001f);
									num3 = Mathf.Pow(num3, 3f);
									exCostSortedList.Add(1f - num3, (int)num);
									int num4;
									int num5;
									DataUtility.Unpack10_22(this.proxyVertexToVertexIndexArray[(int)num], out num4, out num5);
									int num6 = 0;
									while (num6 < num4 && !ref fixedList4096Bytes.IsCapacity<ushort>())
									{
										ushort num7 = this.proxyVertexToVertexDataArray[num5 + num6];
										if (!nativeParallelHashSet.Contains(num7))
										{
											num2 = math.distance(mappingWorkData.position, this.proxyLocalPositions[(int)num7]);
											if (num2 <= this.weightLength)
											{
												ref fixedList4096Bytes.Push(num7);
											}
										}
										num6++;
									}
								}
							}
						}
						if (exCostSortedList.Count == 0)
						{
							exCostSortedList.Add(1f, (int)num);
						}
						else
						{
							int count = exCostSortedList.Count;
							for (int j = 0; j < 4; j++)
							{
								if (j < count)
								{
									exCostSortedList.costs[j] = 1f - exCostSortedList.costs[j];
								}
								else
								{
									exCostSortedList.costs[j] = 0f;
									exCostSortedList.data[j] = 0;
								}
							}
							float rhs = math.csum(exCostSortedList.costs);
							exCostSortedList.costs /= rhs;
						}
						this.boneWeights[i] = new VirtualMeshBoneWeight(exCostSortedList.data, exCostSortedList.costs);
					}
				}
			}

			// Token: 0x040006EC RID: 1772
			public int vcnt;

			// Token: 0x040006ED RID: 1773
			public float weightLength;

			// Token: 0x040006EE RID: 1774
			[ReadOnly]
			public NativeArray<VirtualMesh.MappingWorkData> mappingWorkData;

			// Token: 0x040006EF RID: 1775
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040006F0 RID: 1776
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x040006F1 RID: 1777
			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			// Token: 0x040006F2 RID: 1778
			[ReadOnly]
			public NativeArray<uint> proxyVertexToVertexIndexArray;

			// Token: 0x040006F3 RID: 1779
			[ReadOnly]
			public NativeArray<ushort> proxyVertexToVertexDataArray;
		}

		// Token: 0x02000106 RID: 262
		[BurstCompile]
		private struct Mapping_CalcConnectionVertexDataJob : IJob
		{
			// Token: 0x060004EF RID: 1263 RVA: 0x00028964 File Offset: 0x00026B64
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					float3 @float = this.localPositions[i];
					float3 float2 = MathUtility.TransformPoint(@float, this.toP);
					int num = this.transformIds[this.boneWeights[i].boneIndices[0]];
					ExCostSortedList1 exCostSortedList = new ExCostSortedList1(-1f);
					ExCostSortedList1 exCostSortedList2 = new ExCostSortedList1(-1f);
					foreach (int3 key in GridMap<int>.GetArea(float2, this.searchRadius, this.gridMap, this.gridSize))
					{
						if (this.gridMap.ContainsKey(key))
						{
							foreach (int num2 in this.gridMap.GetValuesForKey(key))
							{
								float3 y = this.proxyLocalPositions[num2];
								float num3 = math.distance(float2, y);
								if (num3 <= this.searchRadius)
								{
									VirtualMeshBoneWeight virtualMeshBoneWeight = this.proxyBoneWeights[num2];
									bool flag = false;
									int num4 = 0;
									while (num4 < virtualMeshBoneWeight.Count && !flag)
									{
										if (this.proxyTransformIds[virtualMeshBoneWeight.boneIndices[num4]] == num)
										{
											flag = true;
										}
										num4++;
									}
									if (flag)
									{
										exCostSortedList2.Add(num3, num2);
									}
									exCostSortedList.Add(num3, num2);
								}
							}
						}
					}
					ExCostSortedList1 exCostSortedList3 = exCostSortedList;
					if (exCostSortedList2.IsValid && exCostSortedList2.Cost < exCostSortedList.Cost * 3f)
					{
						exCostSortedList3 = exCostSortedList2;
					}
					if (!exCostSortedList3.IsValid)
					{
						this.attributes[i] = VertexAttribute.Invalid;
					}
					else
					{
						VertexAttribute value = this.proxyAttributes[exCostSortedList3.Data];
						if (value.IsInvalid())
						{
							this.attributes[i] = VertexAttribute.Invalid;
						}
						else
						{
							VirtualMesh.MappingWorkData value2 = new VirtualMesh.MappingWorkData
							{
								position = float2,
								vertexIndex = i,
								proxyVertexIndex = exCostSortedList3.Data,
								proxyVertexDistance = exCostSortedList3.Cost
							};
							this.mappingWorkData[i] = value2;
							this.attributes[i] = value;
						}
					}
				}
			}

			// Token: 0x040006F4 RID: 1780
			public float gridSize;

			// Token: 0x040006F5 RID: 1781
			public float searchRadius;

			// Token: 0x040006F6 RID: 1782
			public float4x4 toP;

			// Token: 0x040006F7 RID: 1783
			public int vcnt;

			// Token: 0x040006F8 RID: 1784
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040006F9 RID: 1785
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x040006FA RID: 1786
			[ReadOnly]
			public NativeArray<int> transformIds;

			// Token: 0x040006FB RID: 1787
			[WriteOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040006FC RID: 1788
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x040006FD RID: 1789
			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			// Token: 0x040006FE RID: 1790
			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			// Token: 0x040006FF RID: 1791
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> proxyBoneWeights;

			// Token: 0x04000700 RID: 1792
			[ReadOnly]
			public NativeArray<int> proxyTransformIds;

			// Token: 0x04000701 RID: 1793
			[WriteOnly]
			public NativeArray<VirtualMesh.MappingWorkData> mappingWorkData;
		}

		// Token: 0x02000107 RID: 263
		[BurstCompile]
		private struct Mapping_CalcWeightJob : IJobParallelFor
		{
			// Token: 0x060004F0 RID: 1264 RVA: 0x00028BF4 File Offset: 0x00026DF4
			public void Execute(int vindex)
			{
				if (this.attributes[vindex].IsInvalid())
				{
					return;
				}
				VirtualMesh.MappingWorkData mappingWorkData = this.mappingWorkData[vindex];
				int num = mappingWorkData.proxyVertexIndex;
				float3 @float = mappingWorkData.position;
				float3 float2 = this.proxyLocalPositions[num];
				float3 b = this.proxyLocalNormals[num];
				float3 a = @float - float2;
				@float -= math.project(a, b);
				float num2 = math.distance(@float, float2);
				float num3 = num2 * 4f;
				ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
				exCostSortedList.Add(num2, num);
				int num4;
				int num5;
				DataUtility.Unpack10_22(this.proxyVertexToVertexIndexArray[num], out num4, out num5);
				for (int i = 0; i < num4; i++)
				{
					int num6 = (int)this.proxyVertexToVertexDataArray[num5 + i];
					if (!exCostSortedList.Contains(num6))
					{
						float3 y = this.proxyLocalPositions[num6];
						float num7 = math.distance(@float, y);
						if (num7 <= num3)
						{
							exCostSortedList.Add(num7, num6);
						}
						int num8;
						int num9;
						DataUtility.Unpack10_22(this.proxyVertexToVertexIndexArray[num6], out num8, out num9);
						for (int j = 0; j < num8; j++)
						{
							int num10 = (int)this.proxyVertexToVertexDataArray[num9 + j];
							if (num10 != num && num10 != num6 && !exCostSortedList.Contains(num10))
							{
								y = this.proxyLocalPositions[num10];
								num7 = math.distance(@float, y);
								if (num7 <= num3)
								{
									exCostSortedList.Add(num7, num10);
								}
							}
						}
					}
				}
				float4 weights = VirtualMesh.CalcVertexWeights(exCostSortedList.costs);
				VirtualMeshBoneWeight value = new VirtualMeshBoneWeight(exCostSortedList.data, weights);
				this.boneWeights[vindex] = value;
				float num11 = 0f;
				float num12 = 0f;
				int count = value.Count;
				for (int k = 0; k < count; k++)
				{
					num = value.boneIndices[k];
					VertexAttribute vertexAttribute = this.proxyAttributes[num];
					if (vertexAttribute.IsMove())
					{
						num12 += value.weights[k];
					}
					else if (vertexAttribute.IsFixed())
					{
						num11 += value.weights[k];
					}
				}
				this.attributes[vindex] = ((num12 > num11) ? VertexAttribute.Move : VertexAttribute.Fixed);
			}

			// Token: 0x04000702 RID: 1794
			[ReadOnly]
			public NativeArray<VirtualMesh.MappingWorkData> mappingWorkData;

			// Token: 0x04000703 RID: 1795
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000704 RID: 1796
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x04000705 RID: 1797
			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			// Token: 0x04000706 RID: 1798
			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			// Token: 0x04000707 RID: 1799
			[ReadOnly]
			public NativeArray<float3> proxyLocalNormals;

			// Token: 0x04000708 RID: 1800
			[ReadOnly]
			public NativeArray<uint> proxyVertexToVertexIndexArray;

			// Token: 0x04000709 RID: 1801
			[ReadOnly]
			public NativeArray<ushort> proxyVertexToVertexDataArray;
		}

		// Token: 0x02000108 RID: 264
		[BurstCompile]
		private struct Optimize_EdgeToTrianlgeJob : IJob
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x00028E48 File Offset: 0x00027048
			public unsafe void Execute()
			{
				for (int i = 0; i < this.tcnt; i++)
				{
					int3 @int = this.triangles[i];
					int2x3 int2x = new int2x3(@int.xy, @int.yz, @int.zx);
					for (int j = 0; j < 3; j++)
					{
						int2 key = DataUtility.PackInt2(int2x[j]);
						if (this.edgeToTriangleList.ContainsKey(key))
						{
							FixedList128Bytes<int> value = this.edgeToTriangleList[key];
							ref value.Set(i);
							this.edgeToTriangleList[key] = value;
						}
						else
						{
							FixedList128Bytes<int> item = default(FixedList128Bytes<int>);
							ref item.Set(i);
							this.edgeToTriangleList.Add(key, item);
						}
					}
				}
				NativeParallelHashSet<int4> nativeParallelHashSet = new NativeParallelHashSet<int4>(this.tcnt / 4, Allocator.Temp);
				NativeParallelHashSet<int3> nativeParallelHashSet2 = new NativeParallelHashSet<int3>(this.tcnt / 4, Allocator.Temp);
				foreach (KeyValue<int2, FixedList128Bytes<int>> keyValue in this.edgeToTriangleList)
				{
					int2 key2 = keyValue.Key;
					float3 @float = this.localPositions[key2.x];
					float3 float2 = this.localPositions[key2.y];
					FixedList128Bytes<int> fixedList128Bytes = *keyValue.Value;
					int length = fixedList128Bytes.Length;
					for (int k = 0; k < length - 1; k++)
					{
						int3 item2 = this.triangles[fixedList128Bytes[k]];
						int num = DataUtility.RemainingData(item2, key2);
						float3 float3 = this.localPositions[num];
						for (int l = k + 1; l < length; l++)
						{
							int3 item3 = this.triangles[fixedList128Bytes[l]];
							int num2 = DataUtility.RemainingData(item3, key2);
							float3 float4 = this.localPositions[num2];
							if (math.abs(math.degrees(MathUtility.TriangleAngle(@float, float2, float3, float4))) <= 20f)
							{
								float num3;
								float num4;
								float3 float5;
								float3 float6;
								MathUtility.ClosestPtSegmentSegment(@float, float2, float3, float4, out num3, out num4, out float5, out float6);
								if (num3 != 0f && num3 != 1f && num4 != 0f && num4 != 1f)
								{
									int4 item4 = DataUtility.PackInt4(key2.x, key2.y, num, num2);
									if (nativeParallelHashSet.Contains(item4))
									{
										nativeParallelHashSet2.Add(item2);
										nativeParallelHashSet2.Add(item3);
									}
									else
									{
										nativeParallelHashSet.Add(item4);
									}
								}
							}
						}
					}
				}
				for (int m = 0; m < this.tcnt; m++)
				{
					int3 int2 = this.triangles[m];
					if (!nativeParallelHashSet2.Contains(int2))
					{
						this.newTriangles.AddNoResize(int2);
					}
				}
			}

			// Token: 0x0400070A RID: 1802
			public int tcnt;

			// Token: 0x0400070B RID: 1803
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x0400070C RID: 1804
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x0400070D RID: 1805
			public NativeParallelHashMap<int2, FixedList128Bytes<int>> edgeToTriangleList;

			// Token: 0x0400070E RID: 1806
			[WriteOnly]
			public NativeList<int3> newTriangles;
		}

		// Token: 0x02000109 RID: 265
		[BurstCompile]
		private struct ProxyNormalRadiationAdjustmentJob : IJobParallelFor
		{
			// Token: 0x060004F2 RID: 1266 RVA: 0x0002913C File Offset: 0x0002733C
			public void Execute(int vindex)
			{
				float3 @float = this.localPositions[vindex];
				float3 float2 = @float - this.center;
				if (math.length(float2) < 1E-08f)
				{
					return;
				}
				float2 = math.normalize(float2);
				float3 float3 = this.localNormals[vindex];
				float3 float4 = this.localTangents[vindex];
				quaternion quaternion = MathUtility.ToRotation(float3, float4);
				quaternion b = quaternion;
				int num = this.vertexParentIndices[vindex];
				int num2;
				int num3;
				DataUtility.Unpack10_22(this.vertexChildIndexArray[vindex], out num2, out num3);
				if (num2 > 0)
				{
					float3 float5 = 0;
					for (int i = 0; i < num2; i++)
					{
						int index = (int)this.vertexChildDataArray[num3 + i];
						float5 += this.localPositions[index] - @float;
					}
					if (math.lengthsq(float5) > 1E-08f)
					{
						float5 = math.normalize(float5);
						float3 float6 = math.cross(float5, float2);
						float6 = math.cross(float6, float5);
						if (math.lengthsq(float6) > 1E-08f)
						{
							float6 = math.normalize(float6);
							this.localNormals[vindex] = float6;
							this.localTangents[vindex] = float5;
							b = MathUtility.ToRotation(float6, float5);
						}
					}
				}
				else if (num >= 0)
				{
					float3 rhs = this.localPositions[num];
					float3 float7 = @float - rhs;
					float7 = math.normalize(float7);
					float3 float8 = math.cross(float7, float2);
					float8 = math.cross(float8, float7);
					if (math.lengthsq(float8) > 1E-08f)
					{
						float8 = math.normalize(float8);
						this.localNormals[vindex] = float8;
						this.localTangents[vindex] = float7;
						b = MathUtility.ToRotation(float8, float7);
					}
				}
				this.normalAdjustmentRotations[vindex] = math.mul(math.inverse(quaternion), b);
			}

			// Token: 0x0400070F RID: 1807
			public float3 center;

			// Token: 0x04000710 RID: 1808
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000711 RID: 1809
			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			// Token: 0x04000712 RID: 1810
			[ReadOnly]
			public NativeArray<uint> vertexChildIndexArray;

			// Token: 0x04000713 RID: 1811
			[ReadOnly]
			public NativeArray<ushort> vertexChildDataArray;

			// Token: 0x04000714 RID: 1812
			public NativeArray<float3> localNormals;

			// Token: 0x04000715 RID: 1813
			public NativeArray<float3> localTangents;

			// Token: 0x04000716 RID: 1814
			[WriteOnly]
			public NativeArray<quaternion> normalAdjustmentRotations;
		}

		// Token: 0x0200010A RID: 266
		[BurstCompile]
		private struct ProxyCreateFixedListAndAABBJob : IJob
		{
			// Token: 0x060004F3 RID: 1267 RVA: 0x00029314 File Offset: 0x00027514
			public void Execute()
			{
				this.fixedList.Clear();
				float3 @float = 0;
				int num = 0;
				int num2 = 0;
				float3 x = float.MaxValue;
				float3 x2 = float.MinValue;
				int i = 0;
				while (i < this.vcnt)
				{
					float3 float2 = this.localPositions[i];
					if (this.attributes[i].IsMove())
					{
						goto IL_D7;
					}
					int num3;
					int num4;
					DataUtility.Unpack10_22(this.vertexToVertexIndexArray[i], out num3, out num4);
					int j;
					for (j = 0; j < num3; j++)
					{
						int index = (int)this.vertexToVertexDataArray[num4 + j];
						if (this.attributes[index].IsMove())
						{
							break;
						}
					}
					if (j != num3 || num3 <= 0)
					{
						ushort num5 = (ushort)i;
						this.fixedList.Add(num5);
						@float += float2;
						num++;
						goto IL_D7;
					}
					IL_EF:
					i++;
					continue;
					IL_D7:
					x = math.min(x, float2);
					x2 = math.max(x2, float2);
					num2++;
					goto IL_EF;
				}
				this.outAABB.Value = ((num2 > 0) ? new AABB(ref x, ref x2) : default(AABB));
				if (num > 0)
				{
					@float /= (float)num;
				}
				this.localCenterPosition.Value = @float;
			}

			// Token: 0x04000717 RID: 1815
			public int vcnt;

			// Token: 0x04000718 RID: 1816
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000719 RID: 1817
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x0400071A RID: 1818
			[ReadOnly]
			public NativeArray<uint> vertexToVertexIndexArray;

			// Token: 0x0400071B RID: 1819
			[ReadOnly]
			public NativeArray<ushort> vertexToVertexDataArray;

			// Token: 0x0400071C RID: 1820
			[WriteOnly]
			public NativeReference<AABB> outAABB;

			// Token: 0x0400071D RID: 1821
			[WriteOnly]
			public NativeList<ushort> fixedList;

			// Token: 0x0400071E RID: 1822
			[WriteOnly]
			public NativeReference<float3> localCenterPosition;
		}

		// Token: 0x0200010B RID: 267
		[BurstCompile]
		private struct Proxy_CalcTriangleNormalJob : IJobParallelFor
		{
			// Token: 0x060004F4 RID: 1268 RVA: 0x00029460 File Offset: 0x00027660
			public void Execute(int tindex)
			{
				int3 @int = this.triangles[tindex];
				float3 @float = this.localPositins[@int.x];
				float3 float2 = this.localPositins[@int.y];
				float3 float3 = this.localPositins[@int.z];
				float3 value = MathUtility.TriangleNormal(@float, float2, float3);
				this.triangleNormals[tindex] = value;
			}

			// Token: 0x0400071F RID: 1823
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x04000720 RID: 1824
			[ReadOnly]
			public NativeArray<float3> localPositins;

			// Token: 0x04000721 RID: 1825
			[WriteOnly]
			public NativeArray<float3> triangleNormals;
		}

		// Token: 0x0200010C RID: 268
		[BurstCompile]
		private struct Proxy_CalcTriangleTangentJob : IJobParallelFor
		{
			// Token: 0x060004F5 RID: 1269 RVA: 0x000294CC File Offset: 0x000276CC
			public void Execute(int tindex)
			{
				int3 @int = this.triangles[tindex];
				float3 @float = this.localPositins[@int.x];
				float3 float2 = this.localPositins[@int.y];
				float3 float3 = this.localPositins[@int.z];
				float2 float4 = this.uv[@int.x];
				float2 float5 = this.uv[@int.y];
				float2 float6 = this.uv[@int.z];
				float3 value = MathUtility.TriangleTangent(@float, float2, float3, float4, float5, float6);
				this.triangleTangents[tindex] = value;
			}

			// Token: 0x04000722 RID: 1826
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x04000723 RID: 1827
			[ReadOnly]
			public NativeArray<float3> localPositins;

			// Token: 0x04000724 RID: 1828
			[ReadOnly]
			public NativeArray<float2> uv;

			// Token: 0x04000725 RID: 1829
			[WriteOnly]
			public NativeArray<float3> triangleTangents;
		}

		// Token: 0x0200010D RID: 269
		[BurstCompile]
		private struct Proxy_CreateVertexToTrianglesJob : IJob
		{
			// Token: 0x060004F6 RID: 1270 RVA: 0x00029578 File Offset: 0x00027778
			public unsafe void Execute()
			{
				FixedList32Bytes<int>* unsafePtr = (FixedList32Bytes<int>*)this.vertexToTriangles.GetUnsafePtr<FixedList32Bytes<int>>();
				int length = this.triangles.Length;
				for (int i = 0; i < length; i++)
				{
					int3 @int = this.triangles[i];
					FixedList32Bytes<int>* ptr = unsafePtr + (IntPtr)@int.x * (IntPtr)sizeof(FixedList32Bytes<int>) / (IntPtr)sizeof(FixedList32Bytes<int>);
					FixedList32Bytes<int>* ptr2 = unsafePtr + (IntPtr)@int.y * (IntPtr)sizeof(FixedList32Bytes<int>) / (IntPtr)sizeof(FixedList32Bytes<int>);
					FixedList32Bytes<int>* ptr3 = unsafePtr + (IntPtr)@int.z * (IntPtr)sizeof(FixedList32Bytes<int>) / (IntPtr)sizeof(FixedList32Bytes<int>);
					if (ptr->Length < 7)
					{
						ref *ptr.Set(i);
					}
					if (ptr2->Length < 7)
					{
						ref *ptr2.Set(i);
					}
					if (ptr3->Length < 7)
					{
						ref *ptr3.Set(i);
					}
				}
			}

			// Token: 0x04000726 RID: 1830
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x04000727 RID: 1831
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;
		}

		// Token: 0x0200010E RID: 270
		[BurstCompile]
		private struct Proxy_OrganizeVertexToTrianglsJob : IJobParallelFor
		{
			// Token: 0x060004F7 RID: 1271 RVA: 0x00029628 File Offset: 0x00027828
			public void Execute(int vindex)
			{
				FixedList32Bytes<int> value = this.vertexToTriangles[vindex];
				int length = value.Length;
				if (length == 0)
				{
					return;
				}
				VertexAttribute value2 = this.attributes[vindex];
				value2.SetFlag(128, true);
				this.attributes[vindex] = value2;
				float3 @float = 0;
				for (int i = 0; i < length; i++)
				{
					int index = value[i];
					@float += this.triangleNormals[index];
				}
				if (math.length(@float) < 0.5f)
				{
					float num = -1f;
					@float = 0;
					for (int j = 0; j < length; j++)
					{
						int num2 = value[j];
						float3 float2 = 0;
						float3 float3 = this.triangleNormals[num2];
						for (int k = 0; k < length; k++)
						{
							int num3 = value[k];
							if (num3 != num2)
							{
								float3 float4 = this.triangleNormals[num3];
								if (math.dot(float3, float4) >= 0f)
								{
									float2 += float4;
								}
								else
								{
									float2 += -float4;
								}
							}
						}
						float num4 = math.lengthsq(float2);
						if (num4 > num)
						{
							num = num4;
							@float = float3;
						}
					}
				}
				else
				{
					@float = math.normalize(@float);
				}
				for (int l = 0; l < length; l++)
				{
					int num5 = value[l];
					float3 y = this.triangleNormals[num5];
					int value3 = (math.dot(@float, y) >= 0f) ? (num5 + 1) : (-(num5 + 1));
					value[l] = value3;
				}
				this.vertexToTriangles[vindex] = value;
			}

			// Token: 0x04000728 RID: 1832
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

			// Token: 0x04000729 RID: 1833
			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			// Token: 0x0400072A RID: 1834
			public NativeArray<VertexAttribute> attributes;
		}

		// Token: 0x0200010F RID: 271
		[BurstCompile]
		private struct Proxy_CalcVertexNormalTangentFromTriangleJob : IJobParallelFor
		{
			// Token: 0x060004F8 RID: 1272 RVA: 0x000297DC File Offset: 0x000279DC
			public void Execute(int vindex)
			{
				FixedList32Bytes<int> fixedList32Bytes = this.vertexToTriangles[vindex];
				int length = fixedList32Bytes.Length;
				if (length > 0)
				{
					float3 @float = 0;
					float3 float2 = 0;
					for (int i = 0; i < length; i++)
					{
						int num = fixedList32Bytes[i];
						int index = math.abs(num) - 1;
						float rhs = math.sign((float)num);
						@float += this.triangleNormals[index] * rhs;
						float2 += this.triangleTangents[index];
					}
					@float = math.normalize(@float);
					float2 = math.normalize(float2);
					this.localNormals[vindex] = @float;
					this.localTangents[vindex] = float2;
				}
			}

			// Token: 0x0400072B RID: 1835
			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			// Token: 0x0400072C RID: 1836
			[ReadOnly]
			public NativeArray<float3> triangleTangents;

			// Token: 0x0400072D RID: 1837
			[ReadOnly]
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

			// Token: 0x0400072E RID: 1838
			public NativeArray<float3> localNormals;

			// Token: 0x0400072F RID: 1839
			public NativeArray<float3> localTangents;
		}

		// Token: 0x02000110 RID: 272
		[BurstCompile]
		private struct Proxy_CalcVertexToTransformJob : IJobParallelFor
		{
			// Token: 0x060004F9 RID: 1273 RVA: 0x00029898 File Offset: 0x00027A98
			public void Execute(int vindex)
			{
				quaternion b = math.mul(this.invRot, this.transformRotations[vindex]);
				float3 @float = this.localNormals[vindex];
				float3 float2 = this.localTangents[vindex];
				quaternion q = MathUtility.ToRotation(@float, float2);
				this.vertexToTransformRotations[vindex] = math.mul(math.inverse(q), b);
			}

			// Token: 0x04000730 RID: 1840
			public quaternion invRot;

			// Token: 0x04000731 RID: 1841
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x04000732 RID: 1842
			[ReadOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x04000733 RID: 1843
			[WriteOnly]
			public NativeArray<quaternion> vertexToTransformRotations;

			// Token: 0x04000734 RID: 1844
			[ReadOnly]
			public NativeArray<quaternion> transformRotations;
		}

		// Token: 0x02000111 RID: 273
		[BurstCompile]
		private struct Proxy_CalcEdgeToTriangleJob : IJob
		{
			// Token: 0x060004FA RID: 1274 RVA: 0x000298FC File Offset: 0x00027AFC
			public unsafe void Execute()
			{
				for (int i = 0; i < this.tcnt; i++)
				{
					int3 @int = this.triangles[i];
					int2 xy = @int.xy;
					int2 c = DataUtility.PackInt2(xy);
					int2 yz = @int.yz;
					int2 c2 = DataUtility.PackInt2(yz);
					int2 zx = @int.zx;
					int2x3 int2x = new int2x3(c, c2, DataUtility.PackInt2(zx));
					for (int j = 0; j < 3; j++)
					{
						int2 key = *int2x[j];
						ref this.edgeToTriangles.UniqueAdd(key, (ushort)i);
					}
				}
			}

			// Token: 0x04000735 RID: 1845
			public int tcnt;

			// Token: 0x04000736 RID: 1846
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x04000737 RID: 1847
			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;
		}

		// Token: 0x02000112 RID: 274
		[BurstCompile]
		private struct Proxy_CalcVertexBindPoseJob2 : IJobParallelFor
		{
			// Token: 0x060004FB RID: 1275 RVA: 0x0002998C File Offset: 0x00027B8C
			public void Execute(int vindex)
			{
				float3 val = this.localPositions[vindex];
				float3 @float = this.localNormals[vindex];
				float3 float2 = this.localTangents[vindex];
				quaternion q = MathUtility.ToRotation(@float, float2);
				this.vertexBindPosePositions[vindex] = -val;
				this.vertexBindPoseRotations[vindex] = math.inverse(q);
			}

			// Token: 0x04000738 RID: 1848
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000739 RID: 1849
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x0400073A RID: 1850
			[ReadOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x0400073B RID: 1851
			[WriteOnly]
			public NativeArray<float3> vertexBindPosePositions;

			// Token: 0x0400073C RID: 1852
			[WriteOnly]
			public NativeArray<quaternion> vertexBindPoseRotations;
		}

		// Token: 0x02000113 RID: 275
		[BurstCompile]
		private struct Proxy_CalcVertexToVertexFromTriangleJob : IJob
		{
			// Token: 0x060004FC RID: 1276 RVA: 0x000299F0 File Offset: 0x00027BF0
			public void Execute()
			{
				for (int i = 0; i < this.triangleCount; i++)
				{
					int3 @int = this.triangles[i];
					ushort value = (ushort)@int.x;
					ushort value2 = (ushort)@int.y;
					ushort value3 = (ushort)@int.z;
					ref this.vertexToVertexMap.UniqueAdd(@int.x, value2);
					ref this.vertexToVertexMap.UniqueAdd(@int.x, value3);
					ref this.vertexToVertexMap.UniqueAdd(@int.y, value);
					ref this.vertexToVertexMap.UniqueAdd(@int.y, value3);
					ref this.vertexToVertexMap.UniqueAdd(@int.z, value);
					ref this.vertexToVertexMap.UniqueAdd(@int.z, value2);
					int2 int2 = @int.xy;
					this.edgeSet.Add(DataUtility.PackInt2(int2));
					int2 = @int.yz;
					this.edgeSet.Add(DataUtility.PackInt2(int2));
					int2 = @int.zx;
					this.edgeSet.Add(DataUtility.PackInt2(int2));
				}
			}

			// Token: 0x0400073D RID: 1853
			public int triangleCount;

			// Token: 0x0400073E RID: 1854
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x0400073F RID: 1855
			public NativeParallelMultiHashMap<int, ushort> vertexToVertexMap;

			// Token: 0x04000740 RID: 1856
			public NativeParallelHashSet<int2> edgeSet;
		}

		// Token: 0x02000114 RID: 276
		[BurstCompile]
		private struct Proxy_CalcVertexToVertexFromLineJob : IJob
		{
			// Token: 0x060004FD RID: 1277 RVA: 0x00029AFC File Offset: 0x00027CFC
			public void Execute()
			{
				for (int i = 0; i < this.lineCount; i++)
				{
					int2 @int = this.lines[i];
					ref this.vertexToVertexMap.UniqueAdd(@int.x, (ushort)@int.y);
					ref this.vertexToVertexMap.UniqueAdd(@int.y, (ushort)@int.x);
					this.edgeSet.Add(DataUtility.PackInt2(@int));
				}
			}

			// Token: 0x04000741 RID: 1857
			public int lineCount;

			// Token: 0x04000742 RID: 1858
			[ReadOnly]
			public NativeArray<int2> lines;

			// Token: 0x04000743 RID: 1859
			public NativeParallelMultiHashMap<int, ushort> vertexToVertexMap;

			// Token: 0x04000744 RID: 1860
			public NativeParallelHashSet<int2> edgeSet;
		}

		// Token: 0x02000115 RID: 277
		[BurstCompile]
		private struct Proxy_CreateEdgeFlagJob : IJobParallelFor
		{
			// Token: 0x060004FE RID: 1278 RVA: 0x00029B6C File Offset: 0x00027D6C
			public void Execute(int eindex)
			{
				ExBitFlag8 value = default(ExBitFlag8);
				int2 key = this.edges[eindex];
				if (this.edgeToTriangles.ContainsKey(key) && this.edgeToTriangles.CountValuesForKey(key) <= 1)
				{
					value.SetFlag(1, true);
				}
				this.edgeFlags[eindex] = value;
			}

			// Token: 0x04000745 RID: 1861
			[ReadOnly]
			public NativeArray<int2> edges;

			// Token: 0x04000746 RID: 1862
			[ReadOnly]
			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

			// Token: 0x04000747 RID: 1863
			[WriteOnly]
			public NativeArray<ExBitFlag8> edgeFlags;
		}

		// Token: 0x02000116 RID: 278
		private struct SkinningBoneInfo
		{
			// Token: 0x04000748 RID: 1864
			public int startTransformIndex;

			// Token: 0x04000749 RID: 1865
			public float3 startPos;

			// Token: 0x0400074A RID: 1866
			public int endTransformIndex;

			// Token: 0x0400074B RID: 1867
			public float3 endPos;
		}

		// Token: 0x02000117 RID: 279
		[BurstCompile]
		private struct Proxy_CalcCustomSkinningWeightsJob : IJobParallelFor
		{
			// Token: 0x060004FF RID: 1279 RVA: 0x00029BC4 File Offset: 0x00027DC4
			public void Execute(int vindex)
			{
				if (this.isBoneCloth && this.attributes[vindex].IsFixed())
				{
					return;
				}
				float3 @float = this.localPositions[vindex];
				ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
				int length = this.boneInfoList.Length;
				for (int i = 0; i < length; i++)
				{
					VirtualMesh.SkinningBoneInfo skinningBoneInfo = this.boneInfoList[i];
					float3 rhs = MathUtility.ClosestPtPointSegment(@float, skinningBoneInfo.startPos, skinningBoneInfo.endPos);
					float3 x = @float - rhs;
					float3 x2 = skinningBoneInfo.endPos - skinningBoneInfo.startPos;
					float x3 = math.dot(math.normalize(x), math.normalize(x2));
					float num = 1f + math.abs(x3) * this.angularAttenuation;
					for (int j = 0; j < 2; j++)
					{
						int item = (j == 0) ? skinningBoneInfo.startTransformIndex : skinningBoneInfo.endTransformIndex;
						float num2 = (j == 0) ? math.distance(@float, skinningBoneInfo.startPos) : math.distance(@float, skinningBoneInfo.endPos);
						num2 *= num;
						int num3 = exCostSortedList.indexOf(item);
						if (num3 >= 0)
						{
							if (num2 < exCostSortedList.costs[num3])
							{
								exCostSortedList.RemoveItem(item);
								exCostSortedList.Add(num2, item);
							}
						}
						else
						{
							exCostSortedList.Add(num2, item);
						}
					}
				}
				int count = exCostSortedList.Count;
				float rhs2 = exCostSortedList.MinCost * this.distanceReduction;
				exCostSortedList.costs -= rhs2;
				exCostSortedList.costs = math.pow(exCostSortedList.costs, this.distancePow);
				float num4 = math.max(exCostSortedList.MinCost, 1E-06f);
				float num5 = 0f;
				for (int k = 0; k < count; k++)
				{
					exCostSortedList.costs[k] = num4 / exCostSortedList.costs[k];
					num5 += exCostSortedList.costs[k];
				}
				exCostSortedList.costs /= num5;
				num5 = 0f;
				for (int l = 0; l < 4; l++)
				{
					if (exCostSortedList.costs[l] < 0.01f || l >= count)
					{
						exCostSortedList.costs[l] = 0f;
						exCostSortedList.data[l] = 0;
					}
					else
					{
						num5 += exCostSortedList.costs[l];
					}
				}
				exCostSortedList.costs /= num5;
				VirtualMeshBoneWeight value = new VirtualMeshBoneWeight(exCostSortedList.data, exCostSortedList.costs);
				this.boneWeights[vindex] = value;
			}

			// Token: 0x0400074C RID: 1868
			public bool isBoneCloth;

			// Token: 0x0400074D RID: 1869
			public float angularAttenuation;

			// Token: 0x0400074E RID: 1870
			public float distanceReduction;

			// Token: 0x0400074F RID: 1871
			public float distancePow;

			// Token: 0x04000750 RID: 1872
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000751 RID: 1873
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000752 RID: 1874
			[ReadOnly]
			public NativeList<VirtualMesh.SkinningBoneInfo> boneInfoList;

			// Token: 0x04000753 RID: 1875
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;
		}

		// Token: 0x02000118 RID: 280
		[BurstCompile]
		private struct Proxy_ApplySelectionJob : IJobParallelFor
		{
			// Token: 0x06000500 RID: 1280 RVA: 0x00029E98 File Offset: 0x00028098
			public void Execute(int vindex)
			{
				float3 @float = this.localPositions[vindex];
				VertexAttribute value = this.attributes[vindex];
				float num = float.MaxValue;
				VertexAttribute attr = VertexAttribute.Invalid;
				foreach (int3 key in GridMap<int>.GetArea(@float, this.radius, this.gridMap, this.gridSize))
				{
					if (this.gridMap.ContainsKey(key))
					{
						foreach (int index in this.gridMap.GetValuesForKey(key))
						{
							float3 y = this.selectionPositions[index];
							float num2 = math.distance(@float, y);
							if (num2 <= this.radius && num2 <= num)
							{
								num = num2;
								attr = this.selectionAttributes[index];
							}
						}
					}
				}
				value.SetFlag(attr, true);
				this.attributes[vindex] = value;
			}

			// Token: 0x04000754 RID: 1876
			public float gridSize;

			// Token: 0x04000755 RID: 1877
			public float radius;

			// Token: 0x04000756 RID: 1878
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000757 RID: 1879
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000758 RID: 1880
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x04000759 RID: 1881
			[ReadOnly]
			public NativeArray<float3> selectionPositions;

			// Token: 0x0400075A RID: 1882
			[ReadOnly]
			public NativeArray<VertexAttribute> selectionAttributes;
		}

		// Token: 0x02000119 RID: 281
		[BurstCompile]
		private struct Proxy_BoneClothApplayTransformFlagJob : IJobParallelFor
		{
			// Token: 0x06000501 RID: 1281 RVA: 0x00029FD0 File Offset: 0x000281D0
			public void Execute(int vindex)
			{
				VertexAttribute vertexAttribute = this.attributes[vindex];
				ExBitFlag8 value = this.transformFlags[vindex];
				if (vertexAttribute.IsMove())
				{
					value.SetFlag(4, true);
				}
				else if (vertexAttribute.IsFixed())
				{
					value.SetFlag(2, true);
				}
				if (!vertexAttribute.IsInvalid())
				{
					value.SetFlag(8, true);
				}
				this.transformFlags[vindex] = value;
			}

			// Token: 0x0400075B RID: 1883
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400075C RID: 1884
			public NativeArray<ExBitFlag8> transformFlags;
		}

		// Token: 0x0200011A RID: 282
		private struct BaseLineWork : IComparable<VirtualMesh.BaseLineWork>
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x0002A03C File Offset: 0x0002823C
			public int CompareTo(VirtualMesh.BaseLineWork other)
			{
				return (int)math.sign(this.dist - other.dist);
			}

			// Token: 0x0400075D RID: 1885
			public int vindex;

			// Token: 0x0400075E RID: 1886
			public float dist;
		}

		// Token: 0x0200011B RID: 283
		[BurstCompile]
		private struct BaseLine_Mesh_CreateParentJob2 : IJob
		{
			// Token: 0x06000503 RID: 1283 RVA: 0x0002A054 File Offset: 0x00028254
			public void Execute()
			{
				NativeArray<byte> nativeArray = new NativeArray<byte>(this.vcnt, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeParallelHashMap<int, VirtualMesh.BaseLineWork> nativeParallelHashMap = new NativeParallelHashMap<int, VirtualMesh.BaseLineWork>(this.vcnt, Allocator.Temp);
				foreach (int vindex in this.fixedList)
				{
					VirtualMesh.BaseLineWork item = default(VirtualMesh.BaseLineWork);
					item.vindex = vindex;
					item.dist = 0f;
					this.nextList.Add(item);
				}
				int num = 0;
				while (this.nextList.Length > 0)
				{
					foreach (VirtualMesh.BaseLineWork baseLineWork in this.nextList)
					{
						int vindex2 = baseLineWork.vindex;
						if (!this.attribues[vindex2].IsDontMove())
						{
							float3 @float = this.localPositions[vindex2];
							ExCostSortedList1 exCostSortedList = new ExCostSortedList1(-1f, -1);
							int num2;
							int num3;
							DataUtility.Unpack10_22(this.vertexToVertexIndexArray[vindex2], out num2, out num3);
							for (int i = 0; i < num2; i++)
							{
								int num4 = (int)this.vertexToVertexDataArray[num3 + i];
								if (nativeArray[num4] != 0)
								{
									float3 float2 = this.localPositions[num4];
									if (this.attribues[num4].IsDontMove())
									{
										float cost = math.distance(@float, float2);
										exCostSortedList.Add(cost, num4);
									}
									else
									{
										int index = this.vertexParentIndices[num4];
										float3 float3 = float2 - @float;
										float3 float4 = this.localPositions[index] - float2;
										float cost2 = MathUtility.Angle(float3, float4);
										exCostSortedList.Add(cost2, num4);
									}
								}
							}
							if (exCostSortedList.IsValid)
							{
								int data = exCostSortedList.data;
								this.vertexParentIndices[vindex2] = data;
								nativeArray[vindex2] = 1;
							}
						}
					}
					foreach (VirtualMesh.BaseLineWork baseLineWork2 in this.nextList)
					{
						int vindex3 = baseLineWork2.vindex;
						nativeArray[vindex3] = 2;
						int num5 = this.vertexParentIndices[vindex3];
						if (num5 >= 0)
						{
							ref this.vertexChildMap.UniqueAdd(num5, (ushort)vindex3);
						}
					}
					nativeParallelHashMap.Clear();
					int num6 = 0;
					foreach (VirtualMesh.BaseLineWork baseLineWork3 in this.nextList)
					{
						int vindex4 = baseLineWork3.vindex;
						int num7;
						int num8;
						DataUtility.Unpack10_22(this.vertexToVertexIndexArray[vindex4], out num7, out num8);
						if (num7 != 0)
						{
							float3 x = this.localPositions[vindex4];
							for (int j = 0; j < num7; j++)
							{
								int num9 = (int)this.vertexToVertexDataArray[num8 + j];
								if (!this.attribues[num9].IsInvalid() && nativeArray[num9] == 0)
								{
									float num10 = math.distance(x, this.localPositions[num9]);
									if (nativeParallelHashMap.ContainsKey(num9))
									{
										VirtualMesh.BaseLineWork baseLineWork4 = nativeParallelHashMap[num9];
										if (num10 < baseLineWork4.dist)
										{
											baseLineWork4.dist = num10;
											nativeParallelHashMap[num9] = baseLineWork4;
										}
									}
									else
									{
										int key = num9;
										VirtualMesh.BaseLineWork item = new VirtualMesh.BaseLineWork
										{
											vindex = num9,
											dist = num10
										};
										nativeParallelHashMap.Add(key, item);
										num6++;
									}
								}
							}
						}
					}
					this.nextList.Clear();
					if (num6 > 0)
					{
						this.nextList.AddRange(nativeParallelHashMap.GetValueArray(Allocator.Temp));
						this.nextList.Sort<VirtualMesh.BaseLineWork>();
					}
					num++;
				}
			}

			// Token: 0x0400075F RID: 1887
			public int vcnt;

			// Token: 0x04000760 RID: 1888
			public float avgDist;

			// Token: 0x04000761 RID: 1889
			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			// Token: 0x04000762 RID: 1890
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000763 RID: 1891
			[ReadOnly]
			public NativeArray<uint> vertexToVertexIndexArray;

			// Token: 0x04000764 RID: 1892
			[ReadOnly]
			public NativeArray<ushort> vertexToVertexDataArray;

			// Token: 0x04000765 RID: 1893
			public NativeArray<int> vertexParentIndices;

			// Token: 0x04000766 RID: 1894
			public NativeParallelMultiHashMap<int, ushort> vertexChildMap;

			// Token: 0x04000767 RID: 1895
			[ReadOnly]
			public NativeList<int> fixedList;

			// Token: 0x04000768 RID: 1896
			public NativeList<VirtualMesh.BaseLineWork> nextList;
		}

		// Token: 0x0200011C RID: 284
		[BurstCompile]
		private struct BaseLine_Mesh_CareteFixedListJob : IJob
		{
			// Token: 0x06000504 RID: 1284 RVA: 0x0002A4A0 File Offset: 0x000286A0
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					if (this.attribues[i].IsFixed())
					{
						this.fixedList.Add(i);
					}
				}
			}

			// Token: 0x04000769 RID: 1897
			public int vcnt;

			// Token: 0x0400076A RID: 1898
			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			// Token: 0x0400076B RID: 1899
			public NativeList<int> fixedList;
		}

		// Token: 0x0200011D RID: 285
		[BurstCompile]
		private struct BaseLine_Bone_CreateBoneChildInfoJob : IJob
		{
			// Token: 0x06000505 RID: 1285 RVA: 0x0002A4E4 File Offset: 0x000286E4
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					int num = this.parentIndices[i];
					if (num >= 0)
					{
						this.childMap.Add(num, (ushort)i);
					}
				}
			}

			// Token: 0x0400076C RID: 1900
			public int vcnt;

			// Token: 0x0400076D RID: 1901
			[ReadOnly]
			public NativeArray<int> parentIndices;

			// Token: 0x0400076E RID: 1902
			[WriteOnly]
			public NativeParallelMultiHashMap<int, ushort> childMap;
		}

		// Token: 0x0200011E RID: 286
		[BurstCompile]
		private struct BaseLine_CalcLocalPositionRotationJob : IJobParallelFor
		{
			// Token: 0x06000506 RID: 1286 RVA: 0x0002A524 File Offset: 0x00028724
			public void Execute(int index)
			{
				int index2 = (int)this.baseLineIndices[index];
				int num = this.parentIndices[index2];
				if (num >= 0)
				{
					float3 rhs = this.localPositions[num];
					float3 @float = this.localNormals[num];
					float3 float2 = this.localTangents[num];
					quaternion quaternion = math.inverse(MathUtility.ToRotation(@float, float2));
					float3 lhs = this.localPositions[index2];
					float3 float3 = this.localNormals[index2];
					float3 float4 = this.localTangents[index2];
					quaternion b = MathUtility.ToRotation(float3, float4);
					float3 value = math.mul(quaternion, lhs - rhs);
					quaternion value2 = math.mul(quaternion, b);
					this.vertexLocalPositions[index2] = value;
					this.vertexLocalRotations[index2] = value2;
					return;
				}
				this.vertexLocalPositions[index2] = 0;
				this.vertexLocalRotations[index2] = quaternion.identity;
			}

			// Token: 0x0400076F RID: 1903
			[ReadOnly]
			public NativeArray<int> parentIndices;

			// Token: 0x04000770 RID: 1904
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000771 RID: 1905
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x04000772 RID: 1906
			[ReadOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x04000773 RID: 1907
			[ReadOnly]
			public NativeArray<ushort> baseLineIndices;

			// Token: 0x04000774 RID: 1908
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> vertexLocalPositions;

			// Token: 0x04000775 RID: 1909
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> vertexLocalRotations;
		}

		// Token: 0x0200011F RID: 287
		[BurstCompile]
		private struct BaseLine_CalcMaxBaseLineLengthJob : IJob
		{
			// Token: 0x06000507 RID: 1287 RVA: 0x0002A618 File Offset: 0x00028818
			public void Execute()
			{
				float num = 0f;
				for (int i = 0; i < this.vcnt; i++)
				{
					int value = -1;
					float num2 = 0f;
					if (this.attribues[i].IsMove())
					{
						int index = i;
						for (int j = this.vertexParentIndices[index]; j >= 0; j = this.vertexParentIndices[index])
						{
							float3 x = this.localPositions[index];
							float3 y = this.localPositions[j];
							float num3 = math.distance(x, y);
							num2 += num3;
							value = j;
							if (!this.attribues[j].IsMove())
							{
								break;
							}
							index = j;
						}
					}
					this.vertexRootIndices[i] = value;
					this.rootLengthArray[i] = num2;
					num = math.max(num, num2);
				}
				if (num > 1E-08f)
				{
					for (int k = 0; k < this.vcnt; k++)
					{
						float value2 = math.saturate(this.rootLengthArray[k] / num);
						this.vertexDepths[k] = value2;
					}
				}
			}

			// Token: 0x04000776 RID: 1910
			public int vcnt;

			// Token: 0x04000777 RID: 1911
			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			// Token: 0x04000778 RID: 1912
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000779 RID: 1913
			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			// Token: 0x0400077A RID: 1914
			[WriteOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x0400077B RID: 1915
			[WriteOnly]
			public NativeArray<int> vertexRootIndices;

			// Token: 0x0400077C RID: 1916
			public NativeArray<float> rootLengthArray;
		}

		// Token: 0x02000120 RID: 288
		[BurstCompile]
		private struct Reduction_InitVertexToVertexJob2 : IJob
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x0002A738 File Offset: 0x00028938
			public void Execute()
			{
				for (int i = 0; i < this.triangleCount; i++)
				{
					int3 @int = this.triangles[i];
					ushort num = (ushort)@int.x;
					ushort num2 = (ushort)@int.y;
					ushort num3 = (ushort)@int.z;
					this.vertexToVertexMap.Add(num, num2);
					this.vertexToVertexMap.Add(num, num3);
					this.vertexToVertexMap.Add(num2, num);
					this.vertexToVertexMap.Add(num2, num3);
					this.vertexToVertexMap.Add(num3, num);
					this.vertexToVertexMap.Add(num3, num2);
				}
			}

			// Token: 0x0400077D RID: 1917
			public int triangleCount;

			// Token: 0x0400077E RID: 1918
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x0400077F RID: 1919
			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;
		}

		// Token: 0x02000121 RID: 289
		[BurstCompile]
		private struct Organize_RemapVertexJob : IJob
		{
			// Token: 0x06000509 RID: 1289 RVA: 0x0002A7C8 File Offset: 0x000289C8
			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < this.oldVertexCount; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						this.vertexRemapIndices[i] = num;
						num++;
					}
				}
				for (int j = 0; j < this.oldVertexCount; j++)
				{
					int num2 = this.joinIndices[j];
					if (num2 >= 0)
					{
						this.vertexRemapIndices[j] = this.vertexRemapIndices[num2];
					}
				}
			}

			// Token: 0x04000780 RID: 1920
			public int oldVertexCount;

			// Token: 0x04000781 RID: 1921
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x04000782 RID: 1922
			public NativeArray<int> vertexRemapIndices;
		}

		// Token: 0x02000122 RID: 290
		[BurstCompile]
		private struct Organize_CollectUseSkinBoneJob : IJob
		{
			// Token: 0x0600050A RID: 1290 RVA: 0x0002A844 File Offset: 0x00028A44
			public void Execute()
			{
				for (int i = 0; i < this.oldVertexCount; i++)
				{
					if (this.joinIndices[i] < 0)
					{
						VirtualMeshBoneWeight virtualMeshBoneWeight = this.oldBoneWeights[i];
						for (int j = 0; j < 4; j++)
						{
							if (virtualMeshBoneWeight.weights[j] > 0f)
							{
								this.useSkinBoneMap.TryAdd(virtualMeshBoneWeight.boneIndices[j], 0);
							}
						}
					}
				}
				NativeArray<int> keyArray = this.useSkinBoneMap.GetKeyArray(Allocator.Temp);
				for (int k = 0; k < keyArray.Length; k++)
				{
					int num = keyArray[k];
					this.useSkinBoneMap[num] = k;
					this.newSkinBoneTransformIndices.Add(k);
					float4x4 float4x = this.oldBindPoses[num];
					this.newSkinBoneBindPoses.Add(float4x);
				}
				this.newSkinBoneCount.Value = keyArray.Length;
			}

			// Token: 0x04000783 RID: 1923
			public int oldVertexCount;

			// Token: 0x04000784 RID: 1924
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x04000785 RID: 1925
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> oldBoneWeights;

			// Token: 0x04000786 RID: 1926
			[ReadOnly]
			public NativeArray<float4x4> oldBindPoses;

			// Token: 0x04000787 RID: 1927
			public NativeParallelHashMap<int, int> useSkinBoneMap;

			// Token: 0x04000788 RID: 1928
			public NativeList<int> newSkinBoneTransformIndices;

			// Token: 0x04000789 RID: 1929
			public NativeList<float4x4> newSkinBoneBindPoses;

			// Token: 0x0400078A RID: 1930
			public NativeReference<int> newSkinBoneCount;
		}

		// Token: 0x02000123 RID: 291
		[BurstCompile]
		private struct Organize_CopyVertexJob : IJobParallelFor
		{
			// Token: 0x0600050B RID: 1291 RVA: 0x0002A938 File Offset: 0x00028B38
			public void Execute(int index)
			{
				if (this.joinIndices[index] < 0)
				{
					int index2 = this.vertexRemapIndices[index];
					this.newAttributes[index2] = this.oldAttributes[index];
					this.newLocalPositions[index2] = this.oldLocalPositions[index];
					this.newLocalNormals[index2] = this.oldLocalNormals[index];
					this.newLocalTangents[index2] = this.oldLocalTangents[index];
				}
			}

			// Token: 0x0400078B RID: 1931
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x0400078C RID: 1932
			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			// Token: 0x0400078D RID: 1933
			[ReadOnly]
			public NativeArray<VertexAttribute> oldAttributes;

			// Token: 0x0400078E RID: 1934
			[ReadOnly]
			public NativeArray<float3> oldLocalPositions;

			// Token: 0x0400078F RID: 1935
			[ReadOnly]
			public NativeArray<float3> oldLocalNormals;

			// Token: 0x04000790 RID: 1936
			[ReadOnly]
			public NativeArray<float3> oldLocalTangents;

			// Token: 0x04000791 RID: 1937
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> newAttributes;

			// Token: 0x04000792 RID: 1938
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalPositions;

			// Token: 0x04000793 RID: 1939
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalNormals;

			// Token: 0x04000794 RID: 1940
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalTangents;
		}

		// Token: 0x02000124 RID: 292
		[BurstCompile]
		private struct Organize_RemapBoneWeightJob : IJobParallelFor
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x0002A9C4 File Offset: 0x00028BC4
			public void Execute(int vindex)
			{
				if (this.joinIndices[vindex] < 0)
				{
					int index = this.vertexRemapIndices[vindex];
					VirtualMeshBoneWeight value = this.oldBoneWeights[vindex];
					for (int i = 0; i < 4; i++)
					{
						if (value.weights[i] > 0f)
						{
							int key = value.boneIndices[i];
							value.boneIndices[i] = this.useSkinBoneMap[key];
						}
						else
						{
							value.boneIndices[i] = 0;
						}
					}
					this.newBoneWeights[index] = value;
				}
			}

			// Token: 0x04000795 RID: 1941
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x04000796 RID: 1942
			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			// Token: 0x04000797 RID: 1943
			[ReadOnly]
			public NativeParallelHashMap<int, int> useSkinBoneMap;

			// Token: 0x04000798 RID: 1944
			[ReadOnly]
			public NativeArray<int> oldSkinBoneIndices;

			// Token: 0x04000799 RID: 1945
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> oldBoneWeights;

			// Token: 0x0400079A RID: 1946
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> newBoneWeights;
		}

		// Token: 0x02000125 RID: 293
		[BurstCompile]
		private struct Organize_RemapLinkPointArrayJob : IJobParallelFor
		{
			// Token: 0x0600050D RID: 1293 RVA: 0x0002AA60 File Offset: 0x00028C60
			public void Execute(int vindex)
			{
				if (this.joinIndices[vindex] >= 0)
				{
					return;
				}
				int num = this.vertexRemapIndices[vindex];
				foreach (ushort index in this.oldVertexToVertexMap.GetValuesForKey((ushort)vindex))
				{
					int num2 = this.vertexRemapIndices[(int)index];
					ref this.newVertexToVertexMap.UniqueAdd((ushort)num, (ushort)num2);
				}
			}

			// Token: 0x0400079B RID: 1947
			[ReadOnly]
			public NativeArray<int> joinIndices;

			// Token: 0x0400079C RID: 1948
			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			// Token: 0x0400079D RID: 1949
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> oldVertexToVertexMap;

			// Token: 0x0400079E RID: 1950
			[NativeDisableParallelForRestriction]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;
		}

		// Token: 0x02000126 RID: 294
		[BurstCompile]
		private struct Organize_CreateLineTriangleJob : IJob
		{
			// Token: 0x0600050E RID: 1294 RVA: 0x0002AAF4 File Offset: 0x00028CF4
			public void Execute()
			{
				for (int i = 0; i < this.newVertexCount; i++)
				{
					foreach (ushort d in this.newVertexToVertexMap.GetValuesForKey((ushort)i))
					{
						int2 item = DataUtility.PackInt2(i, (int)d);
						this.edgeSet.Add(item);
					}
				}
			}

			// Token: 0x0400079F RID: 1951
			public int newVertexCount;

			// Token: 0x040007A0 RID: 1952
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

			// Token: 0x040007A1 RID: 1953
			[WriteOnly]
			public NativeParallelHashSet<int2> edgeSet;
		}

		// Token: 0x02000127 RID: 295
		[BurstCompile]
		private struct Organize_CreateLineTriangleJob2 : IJob
		{
			// Token: 0x0600050F RID: 1295 RVA: 0x0002AB74 File Offset: 0x00028D74
			public void Execute()
			{
				foreach (int2 @int in this.edgeSet)
				{
					int num = 0;
					foreach (ushort num2 in this.newVertexToVertexMap.GetValuesForKey((ushort)@int.x))
					{
						if ((int)num2 != @int.x && (int)num2 != @int.y && ref this.newVertexToVertexMap.Contains((ushort)@int.y, num2))
						{
							int3 item = DataUtility.PackInt3(@int.x, @int.y, (int)num2);
							this.triangleSet.Add(item);
							num++;
						}
					}
					if (num == 0)
					{
						this.newLineList.Add(@int);
					}
				}
			}

			// Token: 0x040007A2 RID: 1954
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

			// Token: 0x040007A3 RID: 1955
			[WriteOnly]
			public NativeList<int2> newLineList;

			// Token: 0x040007A4 RID: 1956
			[ReadOnly]
			public NativeParallelHashSet<int2> edgeSet;

			// Token: 0x040007A5 RID: 1957
			[WriteOnly]
			public NativeParallelHashSet<int3> triangleSet;
		}

		// Token: 0x02000128 RID: 296
		[BurstCompile]
		private struct Organize_CreateNewTriangleJob3 : IJob
		{
			// Token: 0x06000510 RID: 1296 RVA: 0x0002AC78 File Offset: 0x00028E78
			public void Execute()
			{
				foreach (int3 @int in this.triangleSet)
				{
					this.newTriangleList.Add(@int);
				}
			}

			// Token: 0x040007A6 RID: 1958
			[WriteOnly]
			public NativeList<int3> newTriangleList;

			// Token: 0x040007A7 RID: 1959
			[ReadOnly]
			public NativeParallelHashSet<int3> triangleSet;
		}

		// Token: 0x02000129 RID: 297
		[BurstCompile]
		private struct Work_AverageTriangleDistanceJob : IJob
		{
			// Token: 0x06000511 RID: 1297 RVA: 0x0002ACD4 File Offset: 0x00028ED4
			public void Execute()
			{
				int num = math.max(this.tcnt / 100, 1);
				float num2 = 0f;
				float num3 = 0f;
				int num4 = 0;
				for (int i = 0; i < this.tcnt; i += num)
				{
					int3 @int = this.triangles[i];
					float3 @float = this.localPositions[@int.x];
					float3 float2 = this.localPositions[@int.y];
					float3 float3 = this.localPositions[@int.z];
					float num5 = math.distancesq(@float, float2);
					float num6 = math.distancesq(float2, float3);
					float num7 = math.distancesq(float3, @float);
					num2 += num5;
					num2 += num6;
					num2 += num7;
					num4 += 3;
					num3 = math.max(num3, num5);
					num3 = math.max(num3, num6);
					num3 = math.max(num3, num7);
				}
				this.averageVertexDistance.Value = this.averageVertexDistance.Value + num2;
				this.averageCount.Value = this.averageCount.Value + num4;
				this.maxVertexDistance.Value = math.max(this.maxVertexDistance.Value, num3);
			}

			// Token: 0x040007A8 RID: 1960
			public int vcnt;

			// Token: 0x040007A9 RID: 1961
			public int tcnt;

			// Token: 0x040007AA RID: 1962
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040007AB RID: 1963
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x040007AC RID: 1964
			public NativeReference<float> averageVertexDistance;

			// Token: 0x040007AD RID: 1965
			public NativeReference<int> averageCount;

			// Token: 0x040007AE RID: 1966
			public NativeReference<float> maxVertexDistance;
		}

		// Token: 0x0200012A RID: 298
		[BurstCompile]
		private struct Work_AverageLineDistanceJob : IJob
		{
			// Token: 0x06000512 RID: 1298 RVA: 0x0002AE00 File Offset: 0x00029000
			public void Execute()
			{
				int num = math.max(this.lcnt / 100, 1);
				float num2 = 0f;
				int num3 = 0;
				float num4 = 0f;
				for (int i = 0; i < this.lcnt; i += num)
				{
					int2 @int = this.lines[i];
					float3 x = this.localPositions[@int.x];
					float3 y = this.localPositions[@int.y];
					float num5 = math.distancesq(x, y);
					num2 += num5;
					num3++;
					num4 = math.max(num4, num5);
				}
				this.averageVertexDistance.Value = this.averageVertexDistance.Value + num2;
				this.averageCount.Value = this.averageCount.Value + num3;
				this.maxVertexDistance.Value = math.max(this.maxVertexDistance.Value, num4);
			}

			// Token: 0x040007AF RID: 1967
			public int vcnt;

			// Token: 0x040007B0 RID: 1968
			public int lcnt;

			// Token: 0x040007B1 RID: 1969
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040007B2 RID: 1970
			[ReadOnly]
			public NativeArray<int2> lines;

			// Token: 0x040007B3 RID: 1971
			public NativeReference<float> averageVertexDistance;

			// Token: 0x040007B4 RID: 1972
			public NativeReference<int> averageCount;

			// Token: 0x040007B5 RID: 1973
			public NativeReference<float> maxVertexDistance;
		}

		// Token: 0x0200012B RID: 299
		[BurstCompile]
		private struct Work_AddVertexIndexGirdMapJob : IJob
		{
			// Token: 0x06000513 RID: 1299 RVA: 0x0002AEDC File Offset: 0x000290DC
			public void Execute()
			{
				for (int i = 0; i < this.vcnt; i++)
				{
					GridMap<int>.AddGrid(this.positins[i], i, this.gridMap, this.gridSize);
				}
			}

			// Token: 0x040007B6 RID: 1974
			public float gridSize;

			// Token: 0x040007B7 RID: 1975
			public int vcnt;

			// Token: 0x040007B8 RID: 1976
			[ReadOnly]
			public NativeArray<float3> positins;

			// Token: 0x040007B9 RID: 1977
			[WriteOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;
		}

		// Token: 0x0200012C RID: 300
		[BurstCompile]
		private struct Work_IntersectTriangleJob : IJobParallelFor
		{
			// Token: 0x06000514 RID: 1300 RVA: 0x0002AF1C File Offset: 0x0002911C
			public void Execute(int tindex)
			{
				int3 @int = this.triangles[tindex];
				float3 @float = this.localPositions[@int.x];
				float3 float2 = this.localPositions[@int.y];
				float3 float3 = this.localPositions[@int.z];
				float3 float4;
				float num;
				MathUtility.GetTriangleSphere(@float, float2, float3, out float4, out num);
				float num2 = 0f;
				float3 float5 = 0;
				if (!MathUtility.IntersectRaySphere(this.localRayPos, this.localRayDir, float4, num, ref num2, ref float5))
				{
					return;
				}
				float num3;
				float num4;
				float num5;
				if (!MathUtility.IntersectSegmentTriangle(this.localRayPos, this.localRayEndPos, @float, float2, float3, this.doubleSide, out num3, out num4, out num5, out num2))
				{
					return;
				}
				float3 position = math.lerp(this.localRayPos, this.localRayEndPos, num2);
				float3 normal = MathUtility.TriangleNormal(@float, float2, float3);
				this.hitList.AddNoResize(new VirtualMeshRaycastHit
				{
					type = VirtualMeshPrimitive.Triangle,
					index = tindex,
					position = position,
					distance = num2,
					normal = normal
				});
			}

			// Token: 0x040007BA RID: 1978
			public float3 localRayPos;

			// Token: 0x040007BB RID: 1979
			public float3 localRayDir;

			// Token: 0x040007BC RID: 1980
			public float3 localRayEndPos;

			// Token: 0x040007BD RID: 1981
			public bool doubleSide;

			// Token: 0x040007BE RID: 1982
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040007BF RID: 1983
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x040007C0 RID: 1984
			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;
		}

		// Token: 0x0200012D RID: 301
		[BurstCompile]
		private struct Work_IntersectEdgeJob : IJobParallelFor
		{
			// Token: 0x06000515 RID: 1301 RVA: 0x0002B02C File Offset: 0x0002922C
			public void Execute(int eindex)
			{
				int2 @int = this.edges[eindex];
				if (this.edgeToTriangles.ContainsKey(@int))
				{
					return;
				}
				float3 @float = this.localPositions[@int.x];
				float3 float2 = this.localPositions[@int.y];
				float num;
				float distance;
				float3 float3;
				float3 float4;
				if (math.sqrt(MathUtility.ClosestPtSegmentSegment(@float, float2, this.localRayPos, this.localRayEndPos, out num, out distance, out float3, out float4)) > this.localEdgeRadius)
				{
					return;
				}
				float3 position = float4;
				this.hitList.AddNoResize(new VirtualMeshRaycastHit
				{
					type = VirtualMeshPrimitive.Edge,
					index = eindex,
					position = position,
					distance = distance,
					normal = -this.rayDir
				});
			}

			// Token: 0x040007C1 RID: 1985
			public float3 localRayPos;

			// Token: 0x040007C2 RID: 1986
			public float3 localRayDir;

			// Token: 0x040007C3 RID: 1987
			public float3 localRayEndPos;

			// Token: 0x040007C4 RID: 1988
			public float3 rayDir;

			// Token: 0x040007C5 RID: 1989
			public float localEdgeRadius;

			// Token: 0x040007C6 RID: 1990
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040007C7 RID: 1991
			[ReadOnly]
			public NativeArray<int2> edges;

			// Token: 0x040007C8 RID: 1992
			[ReadOnly]
			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

			// Token: 0x040007C9 RID: 1993
			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;
		}

		// Token: 0x0200012E RID: 302
		[BurstCompile]
		private struct Work_IntersectPointJob : IJobParallelFor
		{
			// Token: 0x06000516 RID: 1302 RVA: 0x0002B0F4 File Offset: 0x000292F4
			public void Execute(int vindex)
			{
				if (this.vertexToTriangles[vindex].Length > 0)
				{
					return;
				}
				float3 position = this.localPositions[vindex];
				float distance = 0f;
				float3 @float = 0;
				if (!MathUtility.IntersectRaySphere(this.localRayPos, this.localRayDir, position, this.localPointRadius, ref distance, ref @float))
				{
					return;
				}
				this.hitList.AddNoResize(new VirtualMeshRaycastHit
				{
					type = VirtualMeshPrimitive.Point,
					index = vindex,
					position = position,
					distance = distance,
					normal = -this.rayDir
				});
			}

			// Token: 0x040007CA RID: 1994
			public float3 localRayPos;

			// Token: 0x040007CB RID: 1995
			public float3 localRayDir;

			// Token: 0x040007CC RID: 1996
			public float3 rayDir;

			// Token: 0x040007CD RID: 1997
			public float localPointRadius;

			// Token: 0x040007CE RID: 1998
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040007CF RID: 1999
			[ReadOnly]
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

			// Token: 0x040007D0 RID: 2000
			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;
		}

		// Token: 0x0200012F RID: 303
		[BurstCompile]
		private struct Work_IntersetcSortJob : IJob
		{
			// Token: 0x06000517 RID: 1303 RVA: 0x0002B19A File Offset: 0x0002939A
			public void Execute()
			{
				if (this.hitList.Length > 1)
				{
					this.hitList.Sort<VirtualMeshRaycastHit>();
				}
			}

			// Token: 0x040007D1 RID: 2001
			public NativeList<VirtualMeshRaycastHit> hitList;
		}

		// Token: 0x02000130 RID: 304
		public enum MeshType
		{
			// Token: 0x040007D3 RID: 2003
			NormalMesh,
			// Token: 0x040007D4 RID: 2004
			NormalBoneMesh,
			// Token: 0x040007D5 RID: 2005
			ProxyMesh,
			// Token: 0x040007D6 RID: 2006
			ProxyBoneMesh,
			// Token: 0x040007D7 RID: 2007
			Mapping
		}

		// Token: 0x02000131 RID: 305
		[CompilerGenerated]
		private sealed class <>c__DisplayClass62_0
		{
			// Token: 0x06000518 RID: 1304 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass62_0()
			{
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0002B1B5 File Offset: 0x000293B5
			internal bool <CreateCustomSkinning>b__0(TransformRecord x)
			{
				return x.id == this.pid;
			}

			// Token: 0x040007D8 RID: 2008
			public int pid;
		}
	}
}
