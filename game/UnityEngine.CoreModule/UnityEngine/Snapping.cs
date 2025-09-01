using System;

namespace UnityEngine
{
	// Token: 0x0200023F RID: 575
	public static class Snapping
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x000280F0 File Offset: 0x000262F0
		internal static bool IsCardinalDirection(Vector3 direction)
		{
			return (Mathf.Abs(direction.x) > 0f && Mathf.Approximately(direction.y, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.y) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.z) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.y, 0f));
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000281A8 File Offset: 0x000263A8
		public static float Snap(float val, float snap)
		{
			bool flag = snap == 0f;
			float result;
			if (flag)
			{
				result = val;
			}
			else
			{
				result = snap * Mathf.Round(val / snap);
			}
			return result;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000281D4 File Offset: 0x000263D4
		public static Vector2 Snap(Vector2 val, Vector2 snap)
		{
			return new Vector3((Mathf.Abs(snap.x) < Mathf.Epsilon) ? val.x : (snap.x * Mathf.Round(val.x / snap.x)), (Mathf.Abs(snap.y) < Mathf.Epsilon) ? val.y : (snap.y * Mathf.Round(val.y / snap.y)));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00028258 File Offset: 0x00026458
		public static Vector3 Snap(Vector3 val, Vector3 snap, SnapAxis axis = SnapAxis.All)
		{
			return new Vector3(((axis & SnapAxis.X) == SnapAxis.X) ? Snapping.Snap(val.x, snap.x) : val.x, ((axis & SnapAxis.Y) == SnapAxis.Y) ? Snapping.Snap(val.y, snap.y) : val.y, ((axis & SnapAxis.Z) == SnapAxis.Z) ? Snapping.Snap(val.z, snap.z) : val.z);
		}
	}
}
