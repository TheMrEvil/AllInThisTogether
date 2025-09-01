using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTail
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class FTail_SkinningVertexData
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public FTail_SkinningVertexData(Vector3 pos)
		{
			this.position = pos;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001F704 File Offset: 0x0001D904
		public float DistanceToLine(Vector3 pos, Vector3 lineStart, Vector3 lineEnd)
		{
			Vector3 rhs = pos - lineStart;
			Vector3 normalized = (lineEnd - lineStart).normalized;
			float num = Vector3.Distance(lineStart, lineEnd);
			float num2 = Vector3.Dot(normalized, rhs);
			if (num2 <= 0f)
			{
				return Vector3.Distance(pos, lineStart);
			}
			if (num2 >= num)
			{
				return Vector3.Distance(pos, lineEnd);
			}
			Vector3 b = normalized * num2;
			Vector3 b2 = lineStart + b;
			return Vector3.Distance(pos, b2);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001F774 File Offset: 0x0001D974
		public void CalculateVertexParameters(Vector3[] bonesPos, Quaternion[] bonesRot, Vector3[] boneAreas, int maxWeightedBones, float spread, Vector3 spreadOffset, float spreadPower = 1f)
		{
			this.allMeshBonesCount = bonesPos.Length;
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < bonesPos.Length; i++)
			{
				Vector3 vector;
				if (i != bonesPos.Length - 1)
				{
					vector = Vector3.Lerp(bonesPos[i], bonesPos[i + 1], 0.9f);
				}
				else
				{
					vector = Vector3.Lerp(bonesPos[i], bonesPos[i] + (bonesPos[i] - bonesPos[i - 1]), 0.9f);
				}
				vector += bonesRot[i] * spreadOffset;
				float y = this.DistanceToLine(this.position, bonesPos[i], vector);
				list.Add(new Vector2((float)i, y));
			}
			list.Sort((Vector2 a, Vector2 b) => a.y.CompareTo(b.y));
			int num = Mathf.Min(maxWeightedBones, bonesPos.Length);
			this.bonesIndexes = new int[num];
			float[] array = new float[num];
			for (int j = 0; j < num; j++)
			{
				this.bonesIndexes[j] = (int)list[j].x;
				array[j] = list[j].y;
			}
			float[] array2 = new float[num];
			this.AutoSetBoneWeights(array2, array, spread, spreadPower, boneAreas);
			float num2 = 1f;
			this.weights = new float[num];
			int num3 = 0;
			while (num3 < num && (spread != 0f || num3 <= 0))
			{
				if (num2 <= 0f)
				{
					this.weights[num3] = 0f;
				}
				else
				{
					float num4 = array2[num3];
					num2 -= num4;
					if (num2 <= 0f)
					{
						num4 += num2;
					}
					else if (num3 == num - 1)
					{
						num4 += num2;
					}
					this.weights[num3] = num4;
				}
				num3++;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public void AutoSetBoneWeights(float[] weightForBone, float[] distToBone, float spread, float spreadPower, Vector3[] boneAreas)
		{
			int num = weightForBone.Length;
			float[] array = new float[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = boneAreas[i].magnitude;
			}
			float[] array2 = new float[num];
			for (int j = 0; j < weightForBone.Length; j++)
			{
				weightForBone[j] = 0f;
			}
			float num2 = 0f;
			for (int k = 0; k < num; k++)
			{
				num2 += distToBone[k];
			}
			for (int l = 0; l < num; l++)
			{
				array2[l] = 1f - distToBone[l] / num2;
			}
			this.debugDists = distToBone;
			if (num == 1 || spread == 0f)
			{
				weightForBone[0] = 1f;
				return;
			}
			if (num == 2)
			{
				float num3 = 1f;
				weightForBone[0] = 1f;
				float num4 = Mathf.InverseLerp(distToBone[0] + array[0] / 1.25f * spread, distToBone[0], distToBone[1]);
				this.debugDists[0] = num4;
				float num5 = FTail_SkinningVertexData.DistributionIn(Mathf.Lerp(0f, 1f, num4), Mathf.Lerp(1.5f, 16f, spreadPower));
				weightForBone[1] = num5;
				num3 += num5;
				this.debugDistWeights = new float[weightForBone.Length];
				weightForBone.CopyTo(this.debugDistWeights, 0);
				for (int m = 0; m < num; m++)
				{
					weightForBone[m] /= num3;
				}
				this.debugWeights = weightForBone;
				return;
			}
			float num6 = array[0] / 10f;
			float num7 = array[0] / 2f;
			float num8 = 0f;
			for (int n = 0; n < num; n++)
			{
				float t = Mathf.InverseLerp(0f, num6 + num7 * spread, distToBone[n]);
				float num9 = Mathf.Lerp(1f, 0f, t);
				if (n == 0 && num9 == 0f)
				{
					num9 = 1f;
				}
				weightForBone[n] = num9;
				num8 += num9;
			}
			this.debugDistWeights = new float[weightForBone.Length];
			weightForBone.CopyTo(this.debugDistWeights, 0);
			for (int num10 = 0; num10 < num; num10++)
			{
				weightForBone[num10] /= num8;
			}
			this.debugWeights = weightForBone;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001FB7D File Offset: 0x0001DD7D
		public static float DistributionIn(float k, float power)
		{
			return Mathf.Pow(k, power + 1f);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		public static Color GetBoneIndicatorColor(int boneIndex, int bonesCount, float s = 0.9f, float v = 0.9f)
		{
			return Color.HSVToRGB(((float)boneIndex * 1.125f / (float)bonesCount + 0.125f * (float)boneIndex + 0.3f) % 1f, s, v);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		public Color GetWeightColor()
		{
			Color color = FTail_SkinningVertexData.GetBoneIndicatorColor(this.bonesIndexes[0], this.allMeshBonesCount, 1f, 1f);
			for (int i = 1; i < this.bonesIndexes.Length; i++)
			{
				color = Color.Lerp(color, FTail_SkinningVertexData.GetBoneIndicatorColor(this.bonesIndexes[i], this.allMeshBonesCount, 1f, 1f), this.weights[i]);
			}
			return color;
		}

		// Token: 0x040003EB RID: 1003
		public Vector3 position;

		// Token: 0x040003EC RID: 1004
		public int[] bonesIndexes;

		// Token: 0x040003ED RID: 1005
		public int allMeshBonesCount;

		// Token: 0x040003EE RID: 1006
		public float[] weights;

		// Token: 0x040003EF RID: 1007
		public float[] debugDists;

		// Token: 0x040003F0 RID: 1008
		public float[] debugDistWeights;

		// Token: 0x040003F1 RID: 1009
		public float[] debugWeights;

		// Token: 0x020001BF RID: 447
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000F87 RID: 3975 RVA: 0x000637D2 File Offset: 0x000619D2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000F88 RID: 3976 RVA: 0x000637DE File Offset: 0x000619DE
			public <>c()
			{
			}

			// Token: 0x06000F89 RID: 3977 RVA: 0x000637E6 File Offset: 0x000619E6
			internal int <CalculateVertexParameters>b__6_0(Vector2 a, Vector2 b)
			{
				return a.y.CompareTo(b.y);
			}

			// Token: 0x04000DC1 RID: 3521
			public static readonly FTail_SkinningVertexData.<>c <>9 = new FTail_SkinningVertexData.<>c();

			// Token: 0x04000DC2 RID: 3522
			public static Comparison<Vector2> <>9__6_0;
		}
	}
}
