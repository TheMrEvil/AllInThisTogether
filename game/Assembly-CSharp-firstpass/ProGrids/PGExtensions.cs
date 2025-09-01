using System;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x02000033 RID: 51
	public static class PGExtensions
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00008F90 File Offset: 0x00007190
		public static bool Contains(this Transform[] t_arr, Transform t)
		{
			for (int i = 0; i < t_arr.Length; i++)
			{
				if (t_arr[i] == t)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008FB9 File Offset: 0x000071B9
		public static float Sum(this Vector3 v)
		{
			return v[0] + v[1] + v[2];
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008FD8 File Offset: 0x000071D8
		public static bool InFrustum(this Camera cam, Vector3 point)
		{
			Vector3 vector = cam.WorldToViewportPoint(point);
			return vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f && vector.z >= 0f;
		}
	}
}
