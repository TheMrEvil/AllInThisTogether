using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009B RID: 155
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"ratio",
		"useRandomDistribution",
		"light",
		"useParticleColor",
		"sizeAffectsRange",
		"alphaAffectsIntensity",
		"range",
		"rangeMultiplier",
		"intensity",
		"intensityMultiplier",
		"maxLights"
	})]
	public class ES3Type_LightsModule : ES3Type
	{
		// Token: 0x06000382 RID: 898 RVA: 0x000122E8 File Offset: 0x000104E8
		public ES3Type_LightsModule() : base(typeof(ParticleSystem.LightsModule))
		{
			ES3Type_LightsModule.Instance = this;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012300 File Offset: 0x00010500
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.LightsModule lightsModule = (ParticleSystem.LightsModule)obj;
			writer.WriteProperty("enabled", lightsModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("ratio", lightsModule.ratio, ES3Type_float.Instance);
			writer.WriteProperty("useRandomDistribution", lightsModule.useRandomDistribution, ES3Type_bool.Instance);
			writer.WritePropertyByRef("light", lightsModule.light);
			writer.WriteProperty("useParticleColor", lightsModule.useParticleColor, ES3Type_bool.Instance);
			writer.WriteProperty("sizeAffectsRange", lightsModule.sizeAffectsRange, ES3Type_bool.Instance);
			writer.WriteProperty("alphaAffectsIntensity", lightsModule.alphaAffectsIntensity, ES3Type_bool.Instance);
			writer.WriteProperty("range", lightsModule.range, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("rangeMultiplier", lightsModule.rangeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("intensity", lightsModule.intensity, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("intensityMultiplier", lightsModule.intensityMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("maxLights", lightsModule.maxLights, ES3Type_int.Instance);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001245C File Offset: 0x0001065C
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.LightsModule lightsModule = default(ParticleSystem.LightsModule);
			this.ReadInto<T>(reader, lightsModule);
			return lightsModule;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00012484 File Offset: 0x00010684
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.LightsModule lightsModule = (ParticleSystem.LightsModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2690566811U)
				{
					if (num <= 680602789U)
					{
						if (num != 49525662U)
						{
							if (num != 258704408U)
							{
								if (num == 680602789U)
								{
									if (text == "useRandomDistribution")
									{
										lightsModule.useRandomDistribution = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "alphaAffectsIntensity")
							{
								lightsModule.alphaAffectsIntensity = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (text == "enabled")
						{
							lightsModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 2057398597U)
					{
						if (num != 2237916426U)
						{
							if (num == 2690566811U)
							{
								if (text == "sizeAffectsRange")
								{
									lightsModule.sizeAffectsRange = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "intensity")
						{
							lightsModule.intensity = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (text == "rangeMultiplier")
					{
						lightsModule.rangeMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3627856361U)
				{
					if (num != 3239190148U)
					{
						if (num != 3498297520U)
						{
							if (num == 3627856361U)
							{
								if (text == "useParticleColor")
								{
									lightsModule.useParticleColor = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "maxLights")
						{
							lightsModule.maxLights = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (text == "ratio")
					{
						lightsModule.ratio = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3801947695U)
				{
					if (num != 4208725202U)
					{
						if (num == 4269980333U)
						{
							if (text == "intensityMultiplier")
							{
								lightsModule.intensityMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "range")
					{
						lightsModule.range = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (text == "light")
				{
					lightsModule.light = reader.Read<Light>(ES3Type_Light.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000EE RID: 238
		public static ES3Type Instance;
	}
}
