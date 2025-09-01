using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200005E RID: 94
	public static class TouchUtility
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00010438 File Offset: 0x0000E638
		public static Vector2 AnchorToViewPoint(TouchControlAnchor touchControlAnchor)
		{
			switch (touchControlAnchor)
			{
			case TouchControlAnchor.TopLeft:
				return new Vector2(0f, 1f);
			case TouchControlAnchor.CenterLeft:
				return new Vector2(0f, 0.5f);
			case TouchControlAnchor.BottomLeft:
				return new Vector2(0f, 0f);
			case TouchControlAnchor.TopCenter:
				return new Vector2(0.5f, 1f);
			case TouchControlAnchor.Center:
				return new Vector2(0.5f, 0.5f);
			case TouchControlAnchor.BottomCenter:
				return new Vector2(0.5f, 0f);
			case TouchControlAnchor.TopRight:
				return new Vector2(1f, 1f);
			case TouchControlAnchor.CenterRight:
				return new Vector2(1f, 0.5f);
			case TouchControlAnchor.BottomRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00010509 File Offset: 0x0000E709
		public static Vector2 RoundVector(Vector2 vector)
		{
			return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
		}
	}
}
