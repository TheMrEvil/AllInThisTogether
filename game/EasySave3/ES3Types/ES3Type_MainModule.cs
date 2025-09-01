using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009D RID: 157
	[Preserve]
	[ES3Properties(new string[]
	{
		"duration",
		"loop",
		"prewarm",
		"startDelay",
		"startDelayMultiplier",
		"startLifetime",
		"startLifetimeMultiplier",
		"startSpeed",
		"startSpeedMultiplier",
		"startSize3D",
		"startSize",
		"startSizeMultiplier",
		"startSizeX",
		"startSizeXMultiplier",
		"startSizeY",
		"startSizeYMultiplier",
		"startSizeZ",
		"startSizeZMultiplier",
		"startRotation3D",
		"startRotation",
		"startRotationMultiplier",
		"startRotationX",
		"startRotationXMultiplier",
		"startRotationY",
		"startRotationYMultiplier",
		"startRotationZ",
		"startRotationZMultiplier",
		"randomizeRotationDirection",
		"startColor",
		"gravityModifier",
		"gravityModifierMultiplier",
		"simulationSpace",
		"customSimulationSpace",
		"simulationSpeed",
		"scalingMode",
		"playOnAwake",
		"maxParticles"
	})]
	public class ES3Type_MainModule : ES3Type
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00012BCF File Offset: 0x00010DCF
		public ES3Type_MainModule() : base(typeof(ParticleSystem.MainModule))
		{
			ES3Type_MainModule.Instance = this;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012BE8 File Offset: 0x00010DE8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.MainModule mainModule = (ParticleSystem.MainModule)obj;
			writer.WriteProperty("duration", mainModule.duration, ES3Type_float.Instance);
			writer.WriteProperty("loop", mainModule.loop, ES3Type_bool.Instance);
			writer.WriteProperty("prewarm", mainModule.prewarm, ES3Type_bool.Instance);
			writer.WriteProperty("startDelay", mainModule.startDelay, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startDelayMultiplier", mainModule.startDelayMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startLifetime", mainModule.startLifetime, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startLifetimeMultiplier", mainModule.startLifetimeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSpeed", mainModule.startSpeed, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSpeedMultiplier", mainModule.startSpeedMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSize3D", mainModule.startSize3D, ES3Type_bool.Instance);
			writer.WriteProperty("startSize", mainModule.startSize, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeMultiplier", mainModule.startSizeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeX", mainModule.startSizeX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeXMultiplier", mainModule.startSizeXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeY", mainModule.startSizeY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeYMultiplier", mainModule.startSizeYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeZ", mainModule.startSizeZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeZMultiplier", mainModule.startSizeZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotation3D", mainModule.startRotation3D, ES3Type_bool.Instance);
			writer.WriteProperty("startRotation", mainModule.startRotation, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationMultiplier", mainModule.startRotationMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationX", mainModule.startRotationX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationXMultiplier", mainModule.startRotationXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationY", mainModule.startRotationY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationYMultiplier", mainModule.startRotationYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationZ", mainModule.startRotationZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationZMultiplier", mainModule.startRotationZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("flipRotation", mainModule.flipRotation, ES3Type_float.Instance);
			writer.WriteProperty("startColor", mainModule.startColor, ES3Type_MinMaxGradient.Instance);
			writer.WriteProperty("gravityModifier", mainModule.gravityModifier, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("gravityModifierMultiplier", mainModule.gravityModifierMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("simulationSpace", mainModule.simulationSpace);
			writer.WritePropertyByRef("customSimulationSpace", mainModule.customSimulationSpace);
			writer.WriteProperty("simulationSpeed", mainModule.simulationSpeed, ES3Type_float.Instance);
			writer.WriteProperty("scalingMode", mainModule.scalingMode);
			writer.WriteProperty("playOnAwake", mainModule.playOnAwake, ES3Type_bool.Instance);
			writer.WriteProperty("maxParticles", mainModule.maxParticles, ES3Type_int.Instance);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012FF4 File Offset: 0x000111F4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);
			this.ReadInto<T>(reader, mainModule);
			return mainModule;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001301C File Offset: 0x0001121C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.MainModule mainModule = (ParticleSystem.MainModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2148803305U)
				{
					if (num <= 1029031894U)
					{
						if (num <= 715850849U)
						{
							if (num <= 295072398U)
							{
								if (num != 135653803U)
								{
									if (num == 295072398U)
									{
										if (text == "startColor")
										{
											mainModule.startColor = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
											continue;
										}
									}
								}
								else if (text == "startLifetimeMultiplier")
								{
									mainModule.startLifetimeMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
							else if (num != 584683564U)
							{
								if (num == 715850849U)
								{
									if (text == "startSize3D")
									{
										mainModule.startSize3D = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "flipRotation")
							{
								mainModule.flipRotation = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (num <= 799079693U)
						{
							if (num != 758253138U)
							{
								if (num == 799079693U)
								{
									if (text == "duration")
									{
										mainModule.duration = reader.Read<float>(ES3Type_float.Instance);
										continue;
									}
								}
							}
							else if (text == "startRotationMultiplier")
							{
								mainModule.startRotationMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (num != 804521684U)
						{
							if (num != 897349200U)
							{
								if (num == 1029031894U)
								{
									if (text == "startRotation3D")
									{
										mainModule.startRotation3D = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "simulationSpace")
							{
								mainModule.simulationSpace = reader.Read<ParticleSystemSimulationSpace>();
								continue;
							}
						}
						else if (text == "startLifetime")
						{
							mainModule.startLifetime = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num <= 1718450024U)
					{
						if (num <= 1457636606U)
						{
							if (num != 1402700886U)
							{
								if (num == 1457636606U)
								{
									if (text == "startDelay")
									{
										mainModule.startDelay = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
										continue;
									}
								}
							}
							else if (text == "startSize")
							{
								mainModule.startSize = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
						else if (num != 1527437181U)
						{
							if (num != 1667165089U)
							{
								if (num == 1718450024U)
								{
									if (text == "startRotationY")
									{
										mainModule.startRotationY = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
										continue;
									}
								}
							}
							else if (text == "startRotation")
							{
								mainModule.startRotation = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
						else if (text == "startSpeedMultiplier")
						{
							mainModule.startSpeedMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num <= 1753560193U)
					{
						if (num != 1735227643U)
						{
							if (num == 1753560193U)
							{
								if (text == "customSimulationSpace")
								{
									mainModule.customSimulationSpace = reader.Read<Transform>(ES3Type_Transform.Instance);
									continue;
								}
							}
						}
						else if (text == "startRotationX")
						{
							mainModule.startRotationX = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num != 1768782881U)
					{
						if (num != 1841060562U)
						{
							if (num == 2148803305U)
							{
								if (text == "startSizeMultiplier")
								{
									mainModule.startSizeMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "startRotationZMultiplier")
						{
							mainModule.startRotationZMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "startRotationZ")
					{
						mainModule.startRotationZ = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num <= 2909084456U)
				{
					if (num <= 2605461614U)
					{
						if (num <= 2425545187U)
						{
							if (num != 2284049417U)
							{
								if (num == 2425545187U)
								{
									if (text == "prewarm")
									{
										mainModule.prewarm = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "simulationSpeed")
							{
								mainModule.simulationSpeed = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (num != 2573531909U)
						{
							if (num == 2605461614U)
							{
								if (text == "startSizeYMultiplier")
								{
									mainModule.startSizeYMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "playOnAwake")
						{
							mainModule.playOnAwake = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num <= 2857916256U)
					{
						if (num != 2849038052U)
						{
							if (num == 2857916256U)
							{
								if (text == "startRotationXMultiplier")
								{
									mainModule.startRotationXMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "startSizeZ")
						{
							mainModule.startSizeZ = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (num != 2882593290U)
					{
						if (num != 2899370909U)
						{
							if (num == 2909084456U)
							{
								if (text == "maxParticles")
								{
									mainModule.maxParticles = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "startSizeY")
						{
							mainModule.startSizeY = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (text == "startSizeX")
					{
						mainModule.startSizeX = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num <= 3717996461U)
				{
					if (num <= 3116486863U)
					{
						if (num != 3105407041U)
						{
							if (num == 3116486863U)
							{
								if (text == "startRotationYMultiplier")
								{
									mainModule.startRotationYMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "startDelayMultiplier")
						{
							mainModule.startDelayMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 3234682207U)
					{
						if (num != 3501835195U)
						{
							if (num == 3717996461U)
							{
								if (text == "startSizeXMultiplier")
								{
									mainModule.startSizeXMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "startSizeZMultiplier")
						{
							mainModule.startSizeZMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "randomizeRotationDirection")
					{
						mainModule.flipRotation = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3728258567U)
				{
					if (num != 3723446379U)
					{
						if (num == 3728258567U)
						{
							if (text == "gravityModifierMultiplier")
							{
								mainModule.gravityModifierMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "loop")
					{
						mainModule.loop = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 3730556912U)
				{
					if (num != 4029565769U)
					{
						if (num == 4060675802U)
						{
							if (text == "startSpeed")
							{
								mainModule.startSpeed = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "scalingMode")
					{
						mainModule.scalingMode = reader.Read<ParticleSystemScalingMode>();
						continue;
					}
				}
				else if (text == "gravityModifier")
				{
					mainModule.gravityModifier = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000F0 RID: 240
		public static ES3Type Instance;
	}
}
