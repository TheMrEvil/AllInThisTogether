using System;
using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x02000044 RID: 68
	public static class TextUtilities
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006CFA File Offset: 0x00004EFA
		public static Vector3[] FakeRandoms
		{
			get
			{
				return TextUtilities.fakeRandoms;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006D04 File Offset: 0x00004F04
		internal static void Initialize()
		{
			if (TextUtilities.initialized)
			{
				return;
			}
			TextUtilities.initialized = true;
			List<Vector3> list = new List<Vector3>();
			for (float num = 0f; num < 360f; num += 14f)
			{
				float f = num * 0.017453292f;
				list.Add(new Vector3(Mathf.Sin(f), Mathf.Cos(f)).normalized);
			}
			TextUtilities.fakeRandoms = new Vector3[25];
			for (int i = 0; i < TextUtilities.fakeRandoms.Length; i++)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				TextUtilities.fakeRandoms[i] = list[index];
				list.RemoveAt(index);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public static Vector3 RotateAround(this Vector3 vec, Vector2 center, float rotDegrees)
		{
			rotDegrees *= 0.017453292f;
			float num = vec.x - center.x;
			float num2 = vec.y - center.y;
			float num3 = num * Mathf.Cos(rotDegrees) - num2 * Mathf.Sin(rotDegrees);
			float num4 = num * Mathf.Sin(rotDegrees) + num2 * Mathf.Cos(rotDegrees);
			vec.x = num3 + center.x;
			vec.y = num4 + center.y;
			return vec;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006E24 File Offset: 0x00005024
		public static void MoveChar(this Vector3[] vec, Vector3 dir)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] += dir;
				b += 1;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006E58 File Offset: 0x00005058
		public static void SetChar(this Vector3[] vec, Vector3 pos)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = pos;
				b += 1;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006E7C File Offset: 0x0000507C
		public static void LerpUnclamped(this Vector3[] vec, Vector3 target, float pct)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = Vector3.LerpUnclamped(vec[(int)b], target, pct);
				b += 1;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006EAD File Offset: 0x000050AD
		public static Vector3 GetMiddlePos(this Vector3[] vec)
		{
			return (vec[0] + vec[2]) / 2f;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006ECC File Offset: 0x000050CC
		public static void RotateChar(this Vector3[] vec, float angle)
		{
			Vector3 middlePos = vec.GetMiddlePos();
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = vec[(int)b].RotateAround(middlePos, angle);
				b += 1;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006F0C File Offset: 0x0000510C
		public static void RotateChar(this Vector3[] vec, float angle, Vector3 pivot)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = vec[(int)b].RotateAround(pivot, angle);
				b += 1;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006F44 File Offset: 0x00005144
		public static void SetColor(this Color32[] col, Color32 target)
		{
			byte b = 0;
			while ((int)b < col.Length)
			{
				col[(int)b] = target;
				b += 1;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006F68 File Offset: 0x00005168
		public static void LerpUnclamped(this Color32[] col, Color32 target, float pct)
		{
			byte b = 0;
			while ((int)b < col.Length)
			{
				col[(int)b] = Color32.LerpUnclamped(col[(int)b], target, pct);
				b += 1;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006F99 File Offset: 0x00005199
		public static float CalculateCurveDuration(this AnimationCurve curve)
		{
			if (curve.keys.Length != 0)
			{
				return curve.keys[curve.length - 1].time;
			}
			return 0f;
		}

		// Token: 0x040000FC RID: 252
		public const int verticesPerChar = 4;

		// Token: 0x040000FD RID: 253
		public const int fakeRandomsCount = 25;

		// Token: 0x040000FE RID: 254
		internal static Vector3[] fakeRandoms;

		// Token: 0x040000FF RID: 255
		private static bool initialized;
	}
}
