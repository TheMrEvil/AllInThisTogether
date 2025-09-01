using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class DebugVisionPoint : MonoBehaviour
{
	// Token: 0x06000BE2 RID: 3042 RVA: 0x0004CFF4 File Offset: 0x0004B1F4
	private void OnDrawGizmos()
	{
		int mask = LayerMask.GetMask(new string[]
		{
			"StaticLevel"
		});
		if (this.point == null || this.point.Position != base.transform.position)
		{
			this.point = new NavVisionPoint(base.transform.position, mask, 120f);
		}
		Color color = Color.blue;
		Gizmos.color = color;
		color.a = 0.3f;
		color.a = 1f;
		this.point.DrawLineGizmos();
		color.a = 0.1f;
		Gizmos.color = color;
		Gizmos.DrawSphere(base.transform.position, Mathf.Sqrt(this.point.ValidRadSqr));
		if (this.CheckTransform == null)
		{
			return;
		}
		Vector3 normalized = (this.CheckTransform.position - this.point.Position).normalized;
		float num = this.point.GradientDistance(normalized, true);
		color = ((Vector3.Distance(this.point.Position, this.CheckTransform.position) <= num) ? Color.green : Color.red);
		Debug.DrawRay(this.point.Position, normalized * num, color);
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0004D141 File Offset: 0x0004B341
	public DebugVisionPoint()
	{
	}

	// Token: 0x040009A6 RID: 2470
	public NavVisionPoint point;

	// Token: 0x040009A7 RID: 2471
	public Transform CheckTransform;
}
