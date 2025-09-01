using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000C2 RID: 194
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"ratio",
		"lifetime",
		"lifetimeMultiplier",
		"minVertexDistance",
		"textureMode",
		"worldSpace",
		"dieWithParticles",
		"sizeAffectsWidth",
		"sizeAffectsLifetime",
		"inheritParticleColor",
		"colorOverLifetime",
		"widthOverTrail",
		"widthOverTrailMultiplier",
		"colorOverTrail"
	})]
	public class ES3Type_TrailModule : ES3Type
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00019E33 File Offset: 0x00018033
		public ES3Type_TrailModule() : base(typeof(ParticleSystem.TrailModule))
		{
			ES3Type_TrailModule.Instance = this;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00019E4C File Offset: 0x0001804C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.TrailModule trailModule = (ParticleSystem.TrailModule)obj;
			writer.WriteProperty("enabled", trailModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("ratio", trailModule.ratio, ES3Type_float.Instance);
			writer.WriteProperty("lifetime", trailModule.lifetime, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("lifetimeMultiplier", trailModule.lifetimeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("minVertexDistance", trailModule.minVertexDistance, ES3Type_float.Instance);
			writer.WriteProperty("textureMode", trailModule.textureMode);
			writer.WriteProperty("worldSpace", trailModule.worldSpace, ES3Type_bool.Instance);
			writer.WriteProperty("dieWithParticles", trailModule.dieWithParticles, ES3Type_bool.Instance);
			writer.WriteProperty("sizeAffectsWidth", trailModule.sizeAffectsWidth, ES3Type_bool.Instance);
			writer.WriteProperty("sizeAffectsLifetime", trailModule.sizeAffectsLifetime, ES3Type_bool.Instance);
			writer.WriteProperty("inheritParticleColor", trailModule.inheritParticleColor, ES3Type_bool.Instance);
			writer.WriteProperty("colorOverLifetime", trailModule.colorOverLifetime, ES3Type_MinMaxGradient.Instance);
			writer.WriteProperty("widthOverTrail", trailModule.widthOverTrail, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("widthOverTrailMultiplier", trailModule.widthOverTrailMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("colorOverTrail", trailModule.colorOverTrail, ES3Type_MinMaxGradient.Instance);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001A000 File Offset: 0x00018200
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.TrailModule trailModule = default(ParticleSystem.TrailModule);
			this.ReadInto<T>(reader, trailModule);
			return trailModule;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001A028 File Offset: 0x00018228
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.TrailModule trailModule = (ParticleSystem.TrailModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1376955374U)
				{
					if (num <= 494095910U)
					{
						if (num != 49525662U)
						{
							if (num != 341298456U)
							{
								if (num == 494095910U)
								{
									if (text == "colorOverTrail")
									{
										trailModule.colorOverTrail = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
										continue;
									}
								}
							}
							else if (text == "widthOverTrailMultiplier")
							{
								trailModule.widthOverTrailMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (text == "enabled")
						{
							trailModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num <= 973733911U)
					{
						if (num != 559929418U)
						{
							if (num == 973733911U)
							{
								if (text == "textureMode")
								{
									trailModule.textureMode = reader.Read<ParticleSystemTrailTextureMode>();
									continue;
								}
							}
						}
						else if (text == "sizeAffectsWidth")
						{
							trailModule.sizeAffectsWidth = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 1024708593U)
					{
						if (num == 1376955374U)
						{
							if (text == "lifetime")
							{
								trailModule.lifetime = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "lifetimeMultiplier")
					{
						trailModule.lifetimeMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 2194260134U)
				{
					if (num <= 1975189197U)
					{
						if (num != 1546474083U)
						{
							if (num == 1975189197U)
							{
								if (text == "sizeAffectsLifetime")
								{
									trailModule.sizeAffectsLifetime = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "widthOverTrail")
						{
							trailModule.widthOverTrail = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num != 2053023684U)
					{
						if (num == 2194260134U)
						{
							if (text == "minVertexDistance")
							{
								trailModule.minVertexDistance = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "dieWithParticles")
					{
						trailModule.dieWithParticles = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 2975387903U)
				{
					if (num != 2732104333U)
					{
						if (num == 2975387903U)
						{
							if (text == "worldSpace")
							{
								trailModule.worldSpace = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "inheritParticleColor")
					{
						trailModule.inheritParticleColor = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 3239190148U)
				{
					if (num == 3881587361U)
					{
						if (text == "colorOverLifetime")
						{
							trailModule.colorOverLifetime = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
							continue;
						}
					}
				}
				else if (text == "ratio")
				{
					trailModule.ratio = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000115 RID: 277
		public static ES3Type Instance;
	}
}
