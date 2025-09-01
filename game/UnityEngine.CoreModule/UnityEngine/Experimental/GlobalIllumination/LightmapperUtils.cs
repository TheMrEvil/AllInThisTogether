using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000464 RID: 1124
	public static class LightmapperUtils
	{
		// Token: 0x060027DB RID: 10203 RVA: 0x00042250 File Offset: 0x00040450
		public static LightMode Extract(LightmapBakeType baketype)
		{
			return (baketype == LightmapBakeType.Realtime) ? LightMode.Realtime : ((baketype == LightmapBakeType.Mixed) ? LightMode.Mixed : LightMode.Baked);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x00042274 File Offset: 0x00040474
		public static LinearColor ExtractIndirect(Light l)
		{
			return LinearColor.Convert(l.color, l.intensity * l.bounceIntensity);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000422A0 File Offset: 0x000404A0
		public static float ExtractInnerCone(Light l)
		{
			return 2f * Mathf.Atan(Mathf.Tan(l.spotAngle * 0.5f * 0.017453292f) * 46f / 64f);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000422E0 File Offset: 0x000404E0
		private static Color ExtractColorTemperature(Light l)
		{
			Color result = new Color(1f, 1f, 1f);
			bool flag = l.useColorTemperature && GraphicsSettings.lightsUseLinearIntensity;
			if (flag)
			{
				result = Mathf.CorrelatedColorTemperatureToRGB(l.colorTemperature);
			}
			return result;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00042329 File Offset: 0x00040529
		private static void ApplyColorTemperature(Color cct, ref LinearColor lightColor)
		{
			lightColor.red *= cct.r;
			lightColor.green *= cct.g;
			lightColor.blue *= cct.b;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00042368 File Offset: 0x00040568
		public static void Extract(Light l, ref DirectionalLight dir)
		{
			dir.instanceID = l.GetInstanceID();
			dir.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			dir.shadow = (l.shadows > LightShadows.None);
			dir.position = l.transform.position;
			dir.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			dir.color = color;
			dir.indirectColor = indirectColor;
			dir.penumbraWidthRadian = 0f;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x00042414 File Offset: 0x00040614
		public static void Extract(Light l, ref PointLight point)
		{
			point.instanceID = l.GetInstanceID();
			point.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			point.shadow = (l.shadows > LightShadows.None);
			point.position = l.transform.position;
			point.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			point.color = color;
			point.indirectColor = indirectColor;
			point.range = l.range;
			point.sphereRadius = 0f;
			point.falloff = FalloffType.Legacy;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000424D4 File Offset: 0x000406D4
		public static void Extract(Light l, ref SpotLight spot)
		{
			spot.instanceID = l.GetInstanceID();
			spot.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			spot.shadow = (l.shadows > LightShadows.None);
			spot.position = l.transform.position;
			spot.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			spot.color = color;
			spot.indirectColor = indirectColor;
			spot.range = l.range;
			spot.sphereRadius = 0f;
			spot.coneAngle = l.spotAngle * 0.017453292f;
			spot.innerConeAngle = LightmapperUtils.ExtractInnerCone(l);
			spot.falloff = FalloffType.Legacy;
			spot.angularFalloff = AngularFalloffType.LUT;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000425B8 File Offset: 0x000407B8
		public static void Extract(Light l, ref RectangleLight rect)
		{
			rect.instanceID = l.GetInstanceID();
			rect.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			rect.shadow = (l.shadows > LightShadows.None);
			rect.position = l.transform.position;
			rect.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			rect.color = color;
			rect.indirectColor = indirectColor;
			rect.range = l.range;
			rect.width = 0f;
			rect.height = 0f;
			rect.falloff = FalloffType.Legacy;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x00042684 File Offset: 0x00040884
		public static void Extract(Light l, ref DiscLight disc)
		{
			disc.instanceID = l.GetInstanceID();
			disc.mode = LightmapperUtils.Extract(l.bakingOutput.lightmapBakeType);
			disc.shadow = (l.shadows > LightShadows.None);
			disc.position = l.transform.position;
			disc.orientation = l.transform.rotation;
			Color cct = LightmapperUtils.ExtractColorTemperature(l);
			LinearColor color = LinearColor.Convert(l.color, l.intensity);
			LinearColor indirectColor = LightmapperUtils.ExtractIndirect(l);
			LightmapperUtils.ApplyColorTemperature(cct, ref color);
			LightmapperUtils.ApplyColorTemperature(cct, ref indirectColor);
			disc.color = color;
			disc.indirectColor = indirectColor;
			disc.range = l.range;
			disc.radius = 0f;
			disc.falloff = FalloffType.Legacy;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x00042744 File Offset: 0x00040944
		public static void Extract(Light l, out Cookie cookie)
		{
			cookie.instanceID = (l.cookie ? l.cookie.GetInstanceID() : 0);
			cookie.scale = 1f;
			cookie.sizes = ((l.type == LightType.Directional && l.cookie) ? new Vector2(l.cookieSize, l.cookieSize) : new Vector2(1f, 1f));
		}
	}
}
