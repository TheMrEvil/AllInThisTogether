using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public static class Packer
{
	// Token: 0x0600002A RID: 42 RVA: 0x00002B2C File Offset: 0x00000D2C
	public static float ToFloat(float x, float y, float z, float w)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		z = ((z < 0f) ? 0f : ((1f < z) ? 1f : z));
		w = ((w < 0f) ? 0f : ((1f < w) ? 1f : w));
		return (float)((Mathf.FloorToInt(w * 63f) << 18) + (Mathf.FloorToInt(z * 63f) << 12) + (Mathf.FloorToInt(y * 63f) << 6) + Mathf.FloorToInt(x * 63f));
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002BF9 File Offset: 0x00000DF9
	public static float ToFloat(Vector4 factor)
	{
		return Packer.ToFloat(Mathf.Clamp01(factor.x), Mathf.Clamp01(factor.y), Mathf.Clamp01(factor.z), Mathf.Clamp01(factor.w));
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002C2C File Offset: 0x00000E2C
	public static float ToFloat(float x, float y, float z)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		z = ((z < 0f) ? 0f : ((1f < z) ? 1f : z));
		return (float)((Mathf.FloorToInt(z * 255f) << 16) + (Mathf.FloorToInt(y * 255f) << 8) + Mathf.FloorToInt(x * 255f));
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002CC8 File Offset: 0x00000EC8
	public static float ToFloat(float x, float y)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		return (float)((Mathf.FloorToInt(y * 4095f) << 12) + Mathf.FloorToInt(x * 4095f));
	}
}
