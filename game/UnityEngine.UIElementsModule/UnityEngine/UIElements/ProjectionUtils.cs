using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000066 RID: 102
	internal static class ProjectionUtils
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float near, float far)
		{
			Matrix4x4 result = default(Matrix4x4);
			float num = right - left;
			float num2 = top - bottom;
			float num3 = far - near;
			result.m00 = 2f / num;
			result.m11 = 2f / num2;
			result.m22 = 2f / num3;
			result.m03 = -(right + left) / num;
			result.m13 = -(top + bottom) / num2;
			result.m23 = -(far + near) / num3;
			result.m33 = 1f;
			return result;
		}
	}
}
