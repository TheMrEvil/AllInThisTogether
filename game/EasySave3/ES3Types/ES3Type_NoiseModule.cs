using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A5 RID: 165
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"separateAxes",
		"strength",
		"strengthMultiplier",
		"strengthX",
		"strengthXMultiplier",
		"strengthY",
		"strengthYMultiplier",
		"strengthZ",
		"strengthZMultiplier",
		"frequency",
		"damping",
		"octaveCount",
		"octaveMultiplier",
		"octaveScale",
		"quality",
		"scrollSpeed",
		"scrollSpeedMultiplier",
		"remapEnabled",
		"remap",
		"remapMultiplier",
		"remapX",
		"remapXMultiplier",
		"remapY",
		"remapYMultiplier",
		"remapZ",
		"remapZMultiplier"
	})]
	public class ES3Type_NoiseModule : ES3Type
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00014E62 File Offset: 0x00013062
		public ES3Type_NoiseModule() : base(typeof(ParticleSystem.NoiseModule))
		{
			ES3Type_NoiseModule.Instance = this;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014E7C File Offset: 0x0001307C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.NoiseModule noiseModule = (ParticleSystem.NoiseModule)obj;
			writer.WriteProperty("enabled", noiseModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("separateAxes", noiseModule.separateAxes, ES3Type_bool.Instance);
			writer.WriteProperty("strength", noiseModule.strength, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("strengthMultiplier", noiseModule.strengthMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("strengthX", noiseModule.strengthX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("strengthXMultiplier", noiseModule.strengthXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("strengthY", noiseModule.strengthY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("strengthYMultiplier", noiseModule.strengthYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("strengthZ", noiseModule.strengthZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("strengthZMultiplier", noiseModule.strengthZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("frequency", noiseModule.frequency, ES3Type_float.Instance);
			writer.WriteProperty("damping", noiseModule.damping, ES3Type_bool.Instance);
			writer.WriteProperty("octaveCount", noiseModule.octaveCount, ES3Type_int.Instance);
			writer.WriteProperty("octaveMultiplier", noiseModule.octaveMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("octaveScale", noiseModule.octaveScale, ES3Type_float.Instance);
			writer.WriteProperty("quality", noiseModule.quality);
			writer.WriteProperty("scrollSpeed", noiseModule.scrollSpeed, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("scrollSpeedMultiplier", noiseModule.scrollSpeedMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("remapEnabled", noiseModule.remapEnabled, ES3Type_bool.Instance);
			writer.WriteProperty("remap", noiseModule.remap, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("remapMultiplier", noiseModule.remapMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("remapX", noiseModule.remapX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("remapXMultiplier", noiseModule.remapXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("remapY", noiseModule.remapY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("remapYMultiplier", noiseModule.remapYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("remapZ", noiseModule.remapZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("remapZMultiplier", noiseModule.remapZMultiplier, ES3Type_float.Instance);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015180 File Offset: 0x00013380
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.NoiseModule noiseModule = default(ParticleSystem.NoiseModule);
			this.ReadInto<T>(reader, noiseModule);
			return noiseModule;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000151A8 File Offset: 0x000133A8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.NoiseModule noiseModule = (ParticleSystem.NoiseModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1832685215U)
				{
					if (num <= 854058160U)
					{
						if (num <= 190084455U)
						{
							if (num != 49525662U)
							{
								if (num != 96119838U)
								{
									if (num == 190084455U)
									{
										if (text == "scrollSpeed")
										{
											noiseModule.scrollSpeed = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
											continue;
										}
									}
								}
								else if (text == "remapYMultiplier")
								{
									noiseModule.remapYMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
							else if (text == "enabled")
							{
								noiseModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 608739593U)
						{
							if (num != 800267265U)
							{
								if (num == 854058160U)
								{
									if (text == "strengthYMultiplier")
									{
										noiseModule.strengthYMultiplier = reader.Read<float>(ES3Type_float.Instance);
										continue;
									}
								}
							}
							else if (text == "frequency")
							{
								noiseModule.frequency = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (text == "octaveScale")
						{
							noiseModule.octaveScale = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num <= 1211446584U)
					{
						if (num != 1137906375U)
						{
							if (num != 1174656921U)
							{
								if (num == 1211446584U)
								{
									if (text == "strengthX")
									{
										noiseModule.strengthX = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
										continue;
									}
								}
							}
							else if (text == "remapMultiplier")
							{
								noiseModule.remapMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (text == "strengthMultiplier")
						{
							noiseModule.strengthMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num <= 1245001822U)
					{
						if (num != 1228224203U)
						{
							if (num == 1245001822U)
							{
								if (text == "strengthZ")
								{
									noiseModule.strengthZ = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "strengthY")
						{
							noiseModule.strengthY = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num != 1479031685U)
					{
						if (num == 1832685215U)
						{
							if (text == "strengthXMultiplier")
							{
								noiseModule.strengthXMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "separateAxes")
					{
						noiseModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 3143743637U)
				{
					if (num <= 2597670950U)
					{
						if (num != 1953622726U)
						{
							if (num != 2534285537U)
							{
								if (num == 2597670950U)
								{
									if (text == "quality")
									{
										noiseModule.quality = reader.Read<ParticleSystemNoiseQuality>();
										continue;
									}
								}
							}
							else if (text == "strengthZMultiplier")
							{
								noiseModule.strengthZMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (text == "remap")
						{
							noiseModule.remap = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num <= 2890097203U)
					{
						if (num != 2882586428U)
						{
							if (num == 2890097203U)
							{
								if (text == "remapEnabled")
								{
									noiseModule.remapEnabled = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "scrollSpeedMultiplier")
						{
							noiseModule.scrollSpeedMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 3018868686U)
					{
						if (num == 3143743637U)
						{
							if (text == "damping")
							{
								noiseModule.damping = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "octaveMultiplier")
					{
						noiseModule.octaveMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3981727418U)
				{
					if (num != 3766098096U)
					{
						if (num != 3948172180U)
						{
							if (num == 3981727418U)
							{
								if (text == "remapX")
								{
									noiseModule.remapX = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "remapZ")
						{
							noiseModule.remapZ = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (text == "strength")
					{
						noiseModule.strength = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num <= 4250196843U)
				{
					if (num != 3998505037U)
					{
						if (num == 4250196843U)
						{
							if (text == "remapZMultiplier")
							{
								noiseModule.remapZMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "remapY")
					{
						noiseModule.remapY = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num != 4283522333U)
				{
					if (num == 4287282972U)
					{
						if (text == "octaveCount")
						{
							noiseModule.octaveCount = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
				}
				else if (text == "remapXMultiplier")
				{
					noiseModule.remapXMultiplier = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000F8 RID: 248
		public static ES3Type Instance;
	}
}
