using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A5 RID: 165
	public static class ColorUtils
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000194CE File Offset: 0x000176CE
		public static float lensImperfectionExposureScale
		{
			get
			{
				return 78f / (100f * ColorUtils.s_LensAttenuation);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000194E1 File Offset: 0x000176E1
		public static float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000194FC File Offset: 0x000176FC
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

		// Token: 0x06000573 RID: 1395 RVA: 0x00019574 File Offset: 0x00017774
		public static Vector3 ColorBalanceToLMSCoeffs(float temperature, float tint)
		{
			float num = temperature / 65f;
			float num2 = tint / 65f;
			float x = 0.31271f - num * ((num < 0f) ? 0.1f : 0.05f);
			float y = ColorUtils.StandardIlluminantY(x) + num2 * 0.05f;
			Vector3 vector = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 vector2 = ColorUtils.CIExyToLMS(x, y);
			return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001960C File Offset: 0x0001780C
		public static ValueTuple<Vector4, Vector4, Vector4> PrepareShadowsMidtonesHighlights(in Vector4 inShadows, in Vector4 inMidtones, in Vector4 inHighlights)
		{
			Vector4 vector = inShadows;
			vector.x = Mathf.GammaToLinearSpace(vector.x);
			vector.y = Mathf.GammaToLinearSpace(vector.y);
			vector.z = Mathf.GammaToLinearSpace(vector.z);
			float num = vector.w * ((Mathf.Sign(vector.w) < 0f) ? 1f : 4f);
			vector.x = Mathf.Max(vector.x + num, 0f);
			vector.y = Mathf.Max(vector.y + num, 0f);
			vector.z = Mathf.Max(vector.z + num, 0f);
			vector.w = 0f;
			Vector4 vector2 = inMidtones;
			vector2.x = Mathf.GammaToLinearSpace(vector2.x);
			vector2.y = Mathf.GammaToLinearSpace(vector2.y);
			vector2.z = Mathf.GammaToLinearSpace(vector2.z);
			num = vector2.w * ((Mathf.Sign(vector2.w) < 0f) ? 1f : 4f);
			vector2.x = Mathf.Max(vector2.x + num, 0f);
			vector2.y = Mathf.Max(vector2.y + num, 0f);
			vector2.z = Mathf.Max(vector2.z + num, 0f);
			vector2.w = 0f;
			Vector4 vector3 = inHighlights;
			vector3.x = Mathf.GammaToLinearSpace(vector3.x);
			vector3.y = Mathf.GammaToLinearSpace(vector3.y);
			vector3.z = Mathf.GammaToLinearSpace(vector3.z);
			num = vector3.w * ((Mathf.Sign(vector3.w) < 0f) ? 1f : 4f);
			vector3.x = Mathf.Max(vector3.x + num, 0f);
			vector3.y = Mathf.Max(vector3.y + num, 0f);
			vector3.z = Mathf.Max(vector3.z + num, 0f);
			vector3.w = 0f;
			return new ValueTuple<Vector4, Vector4, Vector4>(vector, vector2, vector3);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00019850 File Offset: 0x00017A50
		public static ValueTuple<Vector4, Vector4, Vector4> PrepareLiftGammaGain(in Vector4 inLift, in Vector4 inGamma, in Vector4 inGain)
		{
			Vector4 vector = inLift;
			vector.x = Mathf.GammaToLinearSpace(vector.x) * 0.15f;
			vector.y = Mathf.GammaToLinearSpace(vector.y) * 0.15f;
			vector.z = Mathf.GammaToLinearSpace(vector.z) * 0.15f;
			Color color = vector;
			float num = ColorUtils.Luminance(color);
			vector.x = vector.x - num + vector.w;
			vector.y = vector.y - num + vector.w;
			vector.z = vector.z - num + vector.w;
			vector.w = 0f;
			Vector4 vector2 = inGamma;
			vector2.x = Mathf.GammaToLinearSpace(vector2.x) * 0.8f;
			vector2.y = Mathf.GammaToLinearSpace(vector2.y) * 0.8f;
			vector2.z = Mathf.GammaToLinearSpace(vector2.z) * 0.8f;
			color = vector2;
			float num2 = ColorUtils.Luminance(color);
			vector2.w += 1f;
			vector2.x = 1f / Mathf.Max(vector2.x - num2 + vector2.w, 0.001f);
			vector2.y = 1f / Mathf.Max(vector2.y - num2 + vector2.w, 0.001f);
			vector2.z = 1f / Mathf.Max(vector2.z - num2 + vector2.w, 0.001f);
			vector2.w = 0f;
			Vector4 vector3 = inGain;
			vector3.x = Mathf.GammaToLinearSpace(vector3.x) * 0.8f;
			vector3.y = Mathf.GammaToLinearSpace(vector3.y) * 0.8f;
			vector3.z = Mathf.GammaToLinearSpace(vector3.z) * 0.8f;
			color = vector3;
			float num3 = ColorUtils.Luminance(color);
			vector3.w += 1f;
			vector3.x = vector3.x - num3 + vector3.w;
			vector3.y = vector3.y - num3 + vector3.w;
			vector3.z = vector3.z - num3 + vector3.w;
			vector3.w = 0f;
			return new ValueTuple<Vector4, Vector4, Vector4>(vector, vector2, vector3);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019ACC File Offset: 0x00017CCC
		public static ValueTuple<Vector4, Vector4> PrepareSplitToning(in Vector4 inShadows, in Vector4 inHighlights, float balance)
		{
			Vector4 item = inShadows;
			Vector4 item2 = inHighlights;
			item.w = balance / 100f;
			item2.w = 0f;
			return new ValueTuple<Vector4, Vector4>(item, item2);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019B08 File Offset: 0x00017D08
		public static float Luminance(in Color color)
		{
			return color.r * 0.2126729f + color.g * 0.7151522f + color.b * 0.072175f;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00019B30 File Offset: 0x00017D30
		public static float ComputeEV100(float aperture, float shutterSpeed, float ISO)
		{
			return Mathf.Log(aperture * aperture / shutterSpeed * 100f / ISO, 2f);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019B4C File Offset: 0x00017D4C
		public static float ConvertEV100ToExposure(float EV100)
		{
			float num = ColorUtils.lensImperfectionExposureScale * Mathf.Pow(2f, EV100);
			return 1f / num;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00019B72 File Offset: 0x00017D72
		public static float ConvertExposureToEV100(float exposure)
		{
			return Mathf.Log(1f / (ColorUtils.lensImperfectionExposureScale * exposure), 2f);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019B8C File Offset: 0x00017D8C
		public static float ComputeEV100FromAvgLuminance(float avgLuminance)
		{
			float num = ColorUtils.s_LightMeterCalibrationConstant;
			return Mathf.Log(avgLuminance * 100f / num, 2f);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00019BB2 File Offset: 0x00017DB2
		public static float ComputeISO(float aperture, float shutterSpeed, float targetEV100)
		{
			return aperture * aperture * 100f / (shutterSpeed * Mathf.Pow(2f, targetEV100));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00019BCC File Offset: 0x00017DCC
		public static uint ToHex(Color c)
		{
			return (uint)(c.a * 255f) << 24 | (uint)(c.r * 255f) << 16 | (uint)(c.g * 255f) << 8 | (uint)(c.b * 255f);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00019C18 File Offset: 0x00017E18
		public static Color ToRGBA(uint hex)
		{
			return new Color((hex >> 16 & 255U) / 255f, (hex >> 8 & 255U) / 255f, (hex & 255U) / 255f, (hex >> 24 & 255U) / 255f);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019C6E File Offset: 0x00017E6E
		// Note: this type is marked as 'beforefieldinit'.
		static ColorUtils()
		{
		}

		// Token: 0x04000359 RID: 857
		public static float s_LightMeterCalibrationConstant = 12.5f;

		// Token: 0x0400035A RID: 858
		public static float s_LensAttenuation = 0.65f;
	}
}
