using System;
using UnityEngine;

namespace Coffee.UIExtensions
{
	// Token: 0x02000097 RID: 151
	public struct Matrix2x3
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x000286C0 File Offset: 0x000268C0
		public Matrix2x3(Rect rect, float cos, float sin)
		{
			float num = -rect.xMin / rect.width - 0.5f;
			float num2 = -rect.yMin / rect.height - 0.5f;
			this.m00 = cos / rect.width;
			this.m01 = -sin / rect.height;
			this.m02 = num * cos - num2 * sin + 0.5f;
			this.m10 = sin / rect.width;
			this.m11 = cos / rect.height;
			this.m12 = num * sin + num2 * cos + 0.5f;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00028760 File Offset: 0x00026960
		public static Vector2 operator *(Matrix2x3 m, Vector2 v)
		{
			return new Vector2(m.m00 * v.x + m.m01 * v.y + m.m02, m.m10 * v.x + m.m11 * v.y + m.m12);
		}

		// Token: 0x0400051F RID: 1311
		public float m00;

		// Token: 0x04000520 RID: 1312
		public float m01;

		// Token: 0x04000521 RID: 1313
		public float m02;

		// Token: 0x04000522 RID: 1314
		public float m10;

		// Token: 0x04000523 RID: 1315
		public float m11;

		// Token: 0x04000524 RID: 1316
		public float m12;
	}
}
