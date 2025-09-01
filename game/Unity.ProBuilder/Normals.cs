using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200002C RID: 44
	public static class Normals
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x000156E0 File Offset: 0x000138E0
		private static void ClearIntArray(int count)
		{
			if (count > Normals.s_CachedIntArray.Length)
			{
				Array.Resize<int>(ref Normals.s_CachedIntArray, count);
			}
			for (int i = 0; i < count; i++)
			{
				Normals.s_CachedIntArray[i] = 0;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00015718 File Offset: 0x00013918
		public static void CalculateTangents(ProBuilderMesh mesh)
		{
			int vertexCount = mesh.vertexCount;
			if (!mesh.HasArrays(MeshArrays.Tangent))
			{
				mesh.tangentsInternal = new Vector4[vertexCount];
			}
			if (!mesh.HasArrays(MeshArrays.Position) || !mesh.HasArrays(MeshArrays.Texture0))
			{
				return;
			}
			Vector3[] normals = mesh.GetNormals();
			Vector3[] positionsInternal = mesh.positionsInternal;
			Vector2[] texturesInternal = mesh.texturesInternal;
			Vector3[] array = new Vector3[vertexCount];
			Vector3[] array2 = new Vector3[vertexCount];
			Vector4[] tangentsInternal = mesh.tangentsInternal;
			Face[] facesInternal = mesh.facesInternal;
			for (int i = 0; i < facesInternal.Length; i++)
			{
				int[] indexesInternal = facesInternal[i].indexesInternal;
				int j = 0;
				int num = indexesInternal.Length;
				while (j < num)
				{
					long num2 = (long)indexesInternal[j];
					long num3 = (long)indexesInternal[j + 1];
					long num4 = (long)indexesInternal[j + 2];
					Vector3 vector;
					Vector3 vector2;
					Vector3 vector3;
					Vector2 vector4;
					Vector2 vector5;
					Vector2 vector6;
					checked
					{
						vector = positionsInternal[(int)((IntPtr)num2)];
						vector2 = positionsInternal[(int)((IntPtr)num3)];
						vector3 = positionsInternal[(int)((IntPtr)num4)];
						vector4 = texturesInternal[(int)((IntPtr)num2)];
						vector5 = texturesInternal[(int)((IntPtr)num3)];
						vector6 = texturesInternal[(int)((IntPtr)num4)];
					}
					float num5 = vector2.x - vector.x;
					float num6 = vector3.x - vector.x;
					float num7 = vector2.y - vector.y;
					float num8 = vector3.y - vector.y;
					float num9 = vector2.z - vector.z;
					float num10 = vector3.z - vector.z;
					float num11 = vector5.x - vector4.x;
					float num12 = vector6.x - vector4.x;
					float num13 = vector5.y - vector4.y;
					float num14 = vector6.y - vector4.y;
					float num15 = 1f / (num11 * num14 - num12 * num13);
					Vector3 b = new Vector3((num14 * num5 - num13 * num6) * num15, (num14 * num7 - num13 * num8) * num15, (num14 * num9 - num13 * num10) * num15);
					Vector3 b2 = new Vector3((num11 * num6 - num12 * num5) * num15, (num11 * num8 - num12 * num7) * num15, (num11 * num10 - num12 * num9) * num15);
					checked
					{
						array[(int)((IntPtr)num2)] += b;
						array[(int)((IntPtr)num3)] += b;
						array[(int)((IntPtr)num4)] += b;
						array2[(int)((IntPtr)num2)] += b2;
						array2[(int)((IntPtr)num3)] += b2;
						array2[(int)((IntPtr)num4)] += b2;
					}
					j += 3;
				}
			}
			for (long num16 = 0L; num16 < (long)vertexCount; num16 += 1L)
			{
				checked
				{
					Vector3 lhs = normals[(int)((IntPtr)num16)];
					Vector3 vector7 = Math.EnsureUnitVector(array[(int)((IntPtr)num16)]);
					Vector3.OrthoNormalize(ref lhs, ref vector7);
					tangentsInternal[(int)((IntPtr)num16)].x = vector7.x;
					tangentsInternal[(int)((IntPtr)num16)].y = vector7.y;
					tangentsInternal[(int)((IntPtr)num16)].z = vector7.z;
					tangentsInternal[(int)((IntPtr)num16)].w = ((Vector3.Dot(Vector3.Cross(lhs, vector7), array2[(int)((IntPtr)num16)]) < 0f) ? -1f : 1f);
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00015AB0 File Offset: 0x00013CB0
		private static void CalculateHardNormals(ProBuilderMesh mesh)
		{
			int vertexCount = mesh.vertexCount;
			Vector3[] positionsInternal = mesh.positionsInternal;
			Face[] facesInternal = mesh.facesInternal;
			Normals.ClearIntArray(vertexCount);
			if (!mesh.HasArrays(MeshArrays.Normal))
			{
				mesh.normalsInternal = new Vector3[vertexCount];
			}
			Vector3[] normalsInternal = mesh.normalsInternal;
			for (int i = 0; i < vertexCount; i++)
			{
				normalsInternal[i].x = 0f;
				normalsInternal[i].y = 0f;
				normalsInternal[i].z = 0f;
			}
			int j = 0;
			int num = facesInternal.Length;
			while (j < num)
			{
				int[] indexesInternal = facesInternal[j].indexesInternal;
				for (int k = 0; k < indexesInternal.Length; k += 3)
				{
					int num2 = indexesInternal[k];
					int num3 = indexesInternal[k + 1];
					int num4 = indexesInternal[k + 2];
					Vector3 vector = Math.Normal(positionsInternal[num2], positionsInternal[num3], positionsInternal[num4]);
					vector.Normalize();
					Vector3[] array = normalsInternal;
					int num5 = num2;
					array[num5].x = array[num5].x + vector.x;
					Vector3[] array2 = normalsInternal;
					int num6 = num3;
					array2[num6].x = array2[num6].x + vector.x;
					Vector3[] array3 = normalsInternal;
					int num7 = num4;
					array3[num7].x = array3[num7].x + vector.x;
					Vector3[] array4 = normalsInternal;
					int num8 = num2;
					array4[num8].y = array4[num8].y + vector.y;
					Vector3[] array5 = normalsInternal;
					int num9 = num3;
					array5[num9].y = array5[num9].y + vector.y;
					Vector3[] array6 = normalsInternal;
					int num10 = num4;
					array6[num10].y = array6[num10].y + vector.y;
					Vector3[] array7 = normalsInternal;
					int num11 = num2;
					array7[num11].z = array7[num11].z + vector.z;
					Vector3[] array8 = normalsInternal;
					int num12 = num3;
					array8[num12].z = array8[num12].z + vector.z;
					Vector3[] array9 = normalsInternal;
					int num13 = num4;
					array9[num13].z = array9[num13].z + vector.z;
					Normals.s_CachedIntArray[num2]++;
					Normals.s_CachedIntArray[num3]++;
					Normals.s_CachedIntArray[num4]++;
				}
				j++;
			}
			for (int l = 0; l < vertexCount; l++)
			{
				normalsInternal[l].x = normalsInternal[l].x / (float)Normals.s_CachedIntArray[l];
				normalsInternal[l].y = normalsInternal[l].y / (float)Normals.s_CachedIntArray[l];
				normalsInternal[l].z = normalsInternal[l].z / (float)Normals.s_CachedIntArray[l];
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00015D44 File Offset: 0x00013F44
		public static void CalculateNormals(ProBuilderMesh mesh)
		{
			Normals.CalculateHardNormals(mesh);
			SharedVertex[] sharedVerticesInternal = mesh.sharedVerticesInternal;
			Face[] facesInternal = mesh.facesInternal;
			Vector3[] normalsInternal = mesh.normalsInternal;
			int num = 24;
			Normals.ClearIntArray(mesh.vertexCount);
			int i = 0;
			int faceCount = mesh.faceCount;
			while (i < faceCount)
			{
				Face face = facesInternal[i];
				int[] distinctIndexesInternal = face.distinctIndexesInternal;
				int j = 0;
				int num2 = distinctIndexesInternal.Length;
				while (j < num2)
				{
					Normals.s_CachedIntArray[distinctIndexesInternal[j]] = face.smoothingGroup;
					if (face.smoothingGroup >= num)
					{
						num = face.smoothingGroup + 1;
					}
					j++;
				}
				i++;
			}
			if (num > Normals.s_SmoothAvg.Length)
			{
				Array.Resize<Vector3>(ref Normals.s_SmoothAvg, num);
				Array.Resize<float>(ref Normals.s_SmoothAvgCount, num);
			}
			for (int k = 0; k < sharedVerticesInternal.Length; k++)
			{
				for (int l = 0; l < num; l++)
				{
					Normals.s_SmoothAvg[l].x = 0f;
					Normals.s_SmoothAvg[l].y = 0f;
					Normals.s_SmoothAvg[l].z = 0f;
					Normals.s_SmoothAvgCount[l] = 0f;
				}
				for (int m = 0; m < sharedVerticesInternal[k].Count; m++)
				{
					int num3 = sharedVerticesInternal[k][m];
					int num4 = Normals.s_CachedIntArray[num3];
					if (num4 > 0 && (num4 <= 24 || num4 >= 42))
					{
						Vector3[] array = Normals.s_SmoothAvg;
						int num5 = num4;
						array[num5].x = array[num5].x + normalsInternal[num3].x;
						Vector3[] array2 = Normals.s_SmoothAvg;
						int num6 = num4;
						array2[num6].y = array2[num6].y + normalsInternal[num3].y;
						Vector3[] array3 = Normals.s_SmoothAvg;
						int num7 = num4;
						array3[num7].z = array3[num7].z + normalsInternal[num3].z;
						Normals.s_SmoothAvgCount[num4] += 1f;
					}
				}
				for (int n = 0; n < sharedVerticesInternal[k].Count; n++)
				{
					int num8 = sharedVerticesInternal[k][n];
					int num9 = Normals.s_CachedIntArray[num8];
					if (num9 > 0 && (num9 <= 24 || num9 >= 42))
					{
						normalsInternal[num8].x = Normals.s_SmoothAvg[num9].x / Normals.s_SmoothAvgCount[num9];
						normalsInternal[num8].y = Normals.s_SmoothAvg[num9].y / Normals.s_SmoothAvgCount[num9];
						normalsInternal[num8].z = Normals.s_SmoothAvg[num9].z / Normals.s_SmoothAvgCount[num9];
						normalsInternal[num8] = Math.EnsureUnitVector(normalsInternal[num8]);
					}
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00016010 File Offset: 0x00014210
		// Note: this type is marked as 'beforefieldinit'.
		static Normals()
		{
		}

		// Token: 0x04000092 RID: 146
		private static Vector3[] s_SmoothAvg = new Vector3[24];

		// Token: 0x04000093 RID: 147
		private static float[] s_SmoothAvgCount = new float[24];

		// Token: 0x04000094 RID: 148
		private static int[] s_CachedIntArray = new int[65535];
	}
}
