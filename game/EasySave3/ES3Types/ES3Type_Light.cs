using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009A RID: 154
	[Preserve]
	[ES3Properties(new string[]
	{
		"type",
		"color",
		"intensity",
		"bounceIntensity",
		"shadows",
		"shadowStrength",
		"shadowResolution",
		"shadowCustomResolution",
		"shadowBias",
		"shadowNormalBias",
		"shadowNearPlane",
		"range",
		"spotAngle",
		"cookieSize",
		"cookie",
		"flare",
		"renderMode",
		"cullingMask",
		"areaSize",
		"lightmappingMode",
		"enabled",
		"hideFlags"
	})]
	public class ES3Type_Light : ES3ComponentType
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00011BE5 File Offset: 0x0000FDE5
		public ES3Type_Light() : base(typeof(Light))
		{
			ES3Type_Light.Instance = this;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011C00 File Offset: 0x0000FE00
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Light light = (Light)obj;
			writer.WriteProperty("type", light.type);
			writer.WriteProperty("color", light.color, ES3Type_Color.Instance);
			writer.WriteProperty("intensity", light.intensity, ES3Type_float.Instance);
			writer.WriteProperty("bounceIntensity", light.bounceIntensity, ES3Type_float.Instance);
			writer.WriteProperty("shadows", light.shadows);
			writer.WriteProperty("shadowStrength", light.shadowStrength, ES3Type_float.Instance);
			writer.WriteProperty("shadowResolution", light.shadowResolution);
			writer.WriteProperty("shadowCustomResolution", light.shadowCustomResolution, ES3Type_int.Instance);
			writer.WriteProperty("shadowBias", light.shadowBias, ES3Type_float.Instance);
			writer.WriteProperty("shadowNormalBias", light.shadowNormalBias, ES3Type_float.Instance);
			writer.WriteProperty("shadowNearPlane", light.shadowNearPlane, ES3Type_float.Instance);
			writer.WriteProperty("range", light.range, ES3Type_float.Instance);
			writer.WriteProperty("spotAngle", light.spotAngle, ES3Type_float.Instance);
			writer.WriteProperty("cookieSize", light.cookieSize, ES3Type_float.Instance);
			writer.WriteProperty("cookie", light.cookie, ES3Type_Texture2D.Instance);
			writer.WriteProperty("flare", light.flare, ES3Type_Texture2D.Instance);
			writer.WriteProperty("renderMode", light.renderMode);
			writer.WriteProperty("cullingMask", light.cullingMask, ES3Type_int.Instance);
			writer.WriteProperty("enabled", light.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("hideFlags", light.hideFlags);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011E10 File Offset: 0x00010010
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Light light = (Light)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1823281443U)
				{
					if (num <= 1308376928U)
					{
						if (num <= 214491439U)
						{
							if (num != 49525662U)
							{
								if (num == 214491439U)
								{
									if (text == "cullingMask")
									{
										light.cullingMask = reader.Read<int>(ES3Type_int.Instance);
										continue;
									}
								}
							}
							else if (text == "enabled")
							{
								light.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 593295258U)
						{
							if (num != 1031692888U)
							{
								if (num == 1308376928U)
								{
									if (text == "shadowCustomResolution")
									{
										light.shadowCustomResolution = reader.Read<int>(ES3Type_int.Instance);
										continue;
									}
								}
							}
							else if (text == "color")
							{
								light.color = reader.Read<Color>(ES3Type_Color.Instance);
								continue;
							}
						}
						else if (text == "renderMode")
						{
							light.renderMode = reader.Read<LightRenderMode>();
							continue;
						}
					}
					else if (num <= 1361572173U)
					{
						if (num != 1361188592U)
						{
							if (num == 1361572173U)
							{
								if (text == "type")
								{
									light.type = reader.Read<LightType>();
									continue;
								}
							}
						}
						else if (text == "shadowBias")
						{
							light.shadowBias = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 1538595811U)
					{
						if (num != 1621849184U)
						{
							if (num == 1823281443U)
							{
								if (text == "shadowNormalBias")
								{
									light.shadowNormalBias = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "spotAngle")
						{
							light.spotAngle = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "shadowNearPlane")
					{
						light.shadowNearPlane = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 2446394339U)
				{
					if (num <= 2215605636U)
					{
						if (num != 2007449791U)
						{
							if (num == 2215605636U)
							{
								if (text == "shadowStrength")
								{
									light.shadowStrength = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "cookie")
						{
							light.cookie = reader.Read<Texture>();
							continue;
						}
					}
					else if (num != 2237916426U)
					{
						if (num != 2404470818U)
						{
							if (num == 2446394339U)
							{
								if (text == "shadowResolution")
								{
									light.shadowResolution = reader.Read<LightShadowResolution>();
									continue;
								}
							}
						}
						else if (text == "bounceIntensity")
						{
							light.bounceIntensity = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "intensity")
					{
						light.intensity = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3400481622U)
				{
					if (num != 2525185847U)
					{
						if (num == 3400481622U)
						{
							if (text == "shadows")
							{
								light.shadows = reader.Read<LightShadows>();
								continue;
							}
						}
					}
					else if (text == "flare")
					{
						light.flare = reader.Read<Flare>();
						continue;
					}
				}
				else if (num != 3944566772U)
				{
					if (num != 4028275702U)
					{
						if (num == 4208725202U)
						{
							if (text == "range")
							{
								light.range = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "cookieSize")
					{
						light.cookieSize = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (text == "hideFlags")
				{
					light.hideFlags = reader.Read<HideFlags>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000ED RID: 237
		public static ES3Type Instance;
	}
}
