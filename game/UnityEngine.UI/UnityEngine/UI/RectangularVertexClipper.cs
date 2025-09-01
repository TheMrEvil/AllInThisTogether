using System;

namespace UnityEngine.UI
{
	// Token: 0x0200000C RID: 12
	internal class RectangularVertexClipper
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public Rect GetCanvasRect(RectTransform t, Canvas c)
		{
			if (c == null)
			{
				return default(Rect);
			}
			t.GetWorldCorners(this.m_WorldCorners);
			Transform component = c.GetComponent<Transform>();
			for (int i = 0; i < 4; i++)
			{
				this.m_CanvasCorners[i] = component.InverseTransformPoint(this.m_WorldCorners[i]);
			}
			return new Rect(this.m_CanvasCorners[0].x, this.m_CanvasCorners[0].y, this.m_CanvasCorners[2].x - this.m_CanvasCorners[0].x, this.m_CanvasCorners[2].y - this.m_CanvasCorners[0].y);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B8A File Offset: 0x00000D8A
		public RectangularVertexClipper()
		{
		}

		// Token: 0x04000025 RID: 37
		private readonly Vector3[] m_WorldCorners = new Vector3[4];

		// Token: 0x04000026 RID: 38
		private readonly Vector3[] m_CanvasCorners = new Vector3[4];
	}
}
