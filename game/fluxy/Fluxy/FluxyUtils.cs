using System;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x02000011 RID: 17
	public static class FluxyUtils
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00005AEC File Offset: 0x00003CEC
		public static float RelativeScreenHeight(Camera camera, float distance, float size)
		{
			if (camera.orthographic)
			{
				return size * 0.5f / camera.orthographicSize;
			}
			float num = Mathf.Tan(0.017453292f * camera.fieldOfView * 0.5f);
			return size * 0.5f / (distance * num);
		}

		// Token: 0x0400009E RID: 158
		public const float epsilon = 1E-05f;
	}
}
