using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
internal static class RectExtensions
{
	// Token: 0x06001615 RID: 5653 RVA: 0x0008BD08 File Offset: 0x00089F08
	public static Rect Transform(this Rect r, Transform transform)
	{
		return new Rect
		{
			min = transform.TransformPoint(r.min),
			max = transform.TransformPoint(r.max)
		};
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x0008BD5C File Offset: 0x00089F5C
	public static Rect InverseTransform(this Rect r, Transform transform)
	{
		return new Rect
		{
			min = transform.InverseTransformPoint(r.min),
			max = transform.InverseTransformPoint(r.max)
		};
	}
}
