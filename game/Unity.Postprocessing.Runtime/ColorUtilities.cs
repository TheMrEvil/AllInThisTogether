using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000063 RID: 99
	public static class ColorUtilities
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public static float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		public static Vector3 CIExyToLMS(float x, float y)
		{
			float num = 1f;
			float num2 = num * x / y;
			float num3 = num * (1f - x - y) / y;
			float x2 = 0.7328f * num2 + 0.4296f * num - 0.1624f * num3;
			float y2 = -0.7036f * num2 + 1.6975f * num + 0.0061f * num3;
			float z = 0.003f * num2 + 0.0136f * num + 0.9834f * num3;
			return new Vector3(x2, y2, z);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001002C File Offset: 0x0000E22C
		public static Vector3 ComputeColorBalance(float temperature, float tint)
		{
			float num = temperature / 60f;
			float num2 = tint / 60f;
			float x = 0.31271f - num * ((num < 0f) ? 0.1f : 0.05f);
			float y = ColorUtilities.StandardIlluminantY(x) + num2 * 0.05f;
			Vector3 vector = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 vector2 = ColorUtilities.CIExyToLMS(x, y);
			return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public static Vector3 ColorToLift(Vector4 color)
		{
			Vector3 vector = new Vector3(color.x, color.y, color.z);
			float num = vector.x * 0.2126f + vector.y * 0.7152f + vector.z * 0.0722f;
			vector = new Vector3(vector.x - num, vector.y - num, vector.z - num);
			float w = color.w;
			return new Vector3(vector.x + w, vector.y + w, vector.z + w);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010154 File Offset: 0x0000E354
		public static Vector3 ColorToInverseGamma(Vector4 color)
		{
			Vector3 vector = new Vector3(color.x, color.y, color.z);
			float num = vector.x * 0.2126f + vector.y * 0.7152f + vector.z * 0.0722f;
			vector = new Vector3(vector.x - num, vector.y - num, vector.z - num);
			float num2 = color.w + 1f;
			return new Vector3(1f / Mathf.Max(vector.x + num2, 0.001f), 1f / Mathf.Max(vector.y + num2, 0.001f), 1f / Mathf.Max(vector.z + num2, 0.001f));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001021C File Offset: 0x0000E41C
		public static Vector3 ColorToGain(Vector4 color)
		{
			Vector3 vector = new Vector3(color.x, color.y, color.z);
			float num = vector.x * 0.2126f + vector.y * 0.7152f + vector.z * 0.0722f;
			vector = new Vector3(vector.x - num, vector.y - num, vector.z - num);
			float num2 = color.w + 1f;
			return new Vector3(vector.x + num2, vector.y + num2, vector.z + num2);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000102B2 File Offset: 0x0000E4B2
		public static float LogCToLinear(float x)
		{
			if (x <= 0.1530537f)
			{
				return (x - 0.092819f) / 5.301883f;
			}
			return (Mathf.Pow(10f, (x - 0.386036f) / 0.244161f) - 0.047996f) / 5.555556f;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000102ED File Offset: 0x0000E4ED
		public static float LinearToLogC(float x)
		{
			if (x <= 0.011361f)
			{
				return 5.301883f * x + 0.092819f;
			}
			return 0.244161f * Mathf.Log10(5.555556f * x + 0.047996f) + 0.386036f;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010324 File Offset: 0x0000E524
		public static uint ToHex(Color c)
		{
			return (uint)(c.a * 255f) << 24 | (uint)(c.r * 255f) << 16 | (uint)(c.g * 255f) << 8 | (uint)(c.b * 255f);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00010370 File Offset: 0x0000E570
		public static Color ToRGBA(uint hex)
		{
			return new Color((hex >> 16 & 255U) / 255f, (hex >> 8 & 255U) / 255f, (hex & 255U) / 255f, (hex >> 24 & 255U) / 255f);
		}

		// Token: 0x0400020C RID: 524
		private const float logC_cut = 0.011361f;

		// Token: 0x0400020D RID: 525
		private const float logC_a = 5.555556f;

		// Token: 0x0400020E RID: 526
		private const float logC_b = 0.047996f;

		// Token: 0x0400020F RID: 527
		private const float logC_c = 0.244161f;

		// Token: 0x04000210 RID: 528
		private const float logC_d = 0.386036f;

		// Token: 0x04000211 RID: 529
		private const float logC_e = 5.301883f;

		// Token: 0x04000212 RID: 530
		private const float logC_f = 0.092819f;
	}
}
