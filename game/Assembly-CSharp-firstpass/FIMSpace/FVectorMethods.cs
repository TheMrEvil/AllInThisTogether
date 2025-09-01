using System;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003D RID: 61
	public static class FVectorMethods
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000BA0A File Offset: 0x00009C0A
		public static Vector3 RandomVector(float rangeA, float rangeB)
		{
			return new Vector3(UnityEngine.Random.Range(rangeA, rangeB), UnityEngine.Random.Range(rangeA, rangeB), UnityEngine.Random.Range(rangeA, rangeB));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000BA26 File Offset: 0x00009C26
		public static float VectorSum(Vector3 vector)
		{
			return vector.x + vector.y + vector.z;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public static Vector3 RandomVectorNoY(float rangeA, float rangeB)
		{
			return new Vector3(UnityEngine.Random.Range(rangeA, rangeB), 0f, UnityEngine.Random.Range(rangeA, rangeB));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000BA58 File Offset: 0x00009C58
		public static Vector3 RandomVectorMinMax(float min, float max)
		{
			float num = 1f;
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num = -1f;
			}
			float num2 = 1f;
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num2 = -1f;
			}
			float num3 = 1f;
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num3 = -1f;
			}
			return new Vector3(UnityEngine.Random.Range(min, max) * num, UnityEngine.Random.Range(min, max) * num2, UnityEngine.Random.Range(min, max) * num3);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public static Vector3 RandomVectorNoYMinMax(float min, float max)
		{
			float num = 1f;
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num = -1f;
			}
			float num2 = 1f;
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num2 = -1f;
			}
			return new Vector3(UnityEngine.Random.Range(min, max) * num, 0f, UnityEngine.Random.Range(min, max) * num2);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000BB20 File Offset: 0x00009D20
		public static Vector3 GetUIPositionFromWorldPosition(Vector3 position, Camera camera, RectTransform canvas)
		{
			Vector3 result = camera.WorldToViewportPoint(position);
			result.x *= canvas.sizeDelta.x;
			result.y *= canvas.sizeDelta.y;
			result.x -= canvas.sizeDelta.x * canvas.pivot.x;
			result.y -= canvas.sizeDelta.y * canvas.pivot.y;
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000BBA6 File Offset: 0x00009DA6
		public static Vector2 XOZ(this Vector3 toBeFlattened)
		{
			return new Vector2(toBeFlattened.x, toBeFlattened.z);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		public static Vector3 XOZ(this Vector3 toBeFlattened, float yValue = 0f)
		{
			return new Vector3(toBeFlattened.x, yValue, toBeFlattened.z);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000BBCD File Offset: 0x00009DCD
		public static float DistanceTopDown(Vector3 from, Vector3 to)
		{
			return Vector2.Distance(from.XOZ(), to.XOZ());
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public static float DistanceTopDownManhattan(Vector3 from, Vector3 to)
		{
			return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.z - to.z);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000BC08 File Offset: 0x00009E08
		public static float BoundsSizeOnAxis(this Bounds bounds, Vector3 normalized)
		{
			return Vector3.Scale(bounds.size, normalized).magnitude;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public static Vector3 ChooseDominantAxis(Vector3 axis)
		{
			Vector3 vector = new Vector3(Mathf.Abs(axis.x), Mathf.Abs(axis.y), Mathf.Abs(axis.z));
			if (vector.x > vector.y)
			{
				if (vector.z > vector.x)
				{
					return new Vector3(0f, 0f, (axis.z > 0f) ? 1f : -1f);
				}
				return new Vector3((axis.x > 0f) ? 1f : -1f, 0f, 0f);
			}
			else
			{
				if (vector.z > vector.y)
				{
					return new Vector3(0f, 0f, (axis.z > 0f) ? 1f : -1f);
				}
				return new Vector3(0f, (axis.y > 0f) ? 1f : -1f, 0f);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000BD2E File Offset: 0x00009F2E
		public static Vector3 GetRounded(Vector3 dir)
		{
			return new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000BD56 File Offset: 0x00009F56
		public static Vector3 GetCounterAxis(Vector3 axis)
		{
			return new Vector3(axis.z, axis.x, axis.y);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BD6F File Offset: 0x00009F6F
		public static Color GetAxisColor(Vector3 axis, float alpha = 0.75f)
		{
			return new Color(axis.z, axis.x, axis.y, alpha);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public static Vector3 FlattenVector(Vector3 v, float to = 90f)
		{
			v.x = Mathf.Round(v.x / to) * to;
			v.y = Mathf.Round(v.y / to) * to;
			v.z = Mathf.Round(v.z / to) * to;
			return v;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public static Vector3 FlattenVectorFlr(Vector3 v, float to = 90f)
		{
			v.x = Mathf.Floor(v.x / to) * to;
			v.y = Mathf.Floor(v.y / to) * to;
			v.z = Mathf.Floor(v.z / to) * to;
			return v;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000BE2C File Offset: 0x0000A02C
		public static Vector3 FlattenVectorCeil(Vector3 v, float to = 90f)
		{
			v.x = Mathf.Ceil(v.x / to) * to;
			v.y = Mathf.Ceil(v.y / to) * to;
			v.z = Mathf.Ceil(v.z / to) * to;
			return v;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000BE7C File Offset: 0x0000A07C
		public static Vector3 FlattenVector(Vector3 v, Vector3 to)
		{
			v.x = Mathf.Round(v.x / to.x) * to.x;
			v.y = Mathf.Round(v.y / to.y) * to.y;
			v.z = Mathf.Round(v.z / to.z) * to.z;
			return v;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000BEEA File Offset: 0x0000A0EA
		public static Vector3Int V3toV3Int(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000BF14 File Offset: 0x0000A114
		public static Vector3 FlattenNormal(Quaternion orientation, Vector3? forward = null, float to = 90f)
		{
			Vector3 point = (forward == null) ? Vector3.forward : forward.Value;
			return Quaternion.Euler(FVectorMethods.FlattenVector(orientation.eulerAngles, to)) * point;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000BF51 File Offset: 0x0000A151
		public static Vector3 EqualVector(float valueAll)
		{
			return new Vector3(valueAll, valueAll, valueAll);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BF5B File Offset: 0x0000A15B
		public static Quaternion FlattenRotation(Quaternion orientation, float to = 90f)
		{
			return Quaternion.Euler(FVectorMethods.FlattenVector(orientation.eulerAngles, to));
		}
	}
}
