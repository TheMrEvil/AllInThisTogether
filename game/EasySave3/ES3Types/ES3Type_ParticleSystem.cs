using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006C RID: 108
	[Preserve]
	[ES3Properties(new string[]
	{
		"time",
		"hideFlags",
		"collision",
		"colorBySpeed",
		"colorOverLifetime",
		"emission",
		"externalForces",
		"forceOverLifetime",
		"inheritVelocity",
		"lights",
		"limitVelocityOverLifetime",
		"main",
		"noise",
		"rotatonBySpeed",
		"rotationOverLifetime",
		"shape",
		"sizeBySpeed",
		"sizeOverLifetime",
		"subEmitters",
		"textureSheetAnimation",
		"trails",
		"trigger",
		"useAutoRandomSeed",
		"velocityOverLifetime",
		"isPaused",
		"isPlaying",
		"isStopped"
	})]
	public class ES3Type_ParticleSystem : ES3ComponentType
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000CD75 File Offset: 0x0000AF75
		public ES3Type_ParticleSystem() : base(typeof(ParticleSystem))
		{
			ES3Type_ParticleSystem.Instance = this;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000CD90 File Offset: 0x0000AF90
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ParticleSystem particleSystem = (ParticleSystem)obj;
			writer.WriteProperty("time", particleSystem.time);
			writer.WriteProperty("hideFlags", particleSystem.hideFlags);
			writer.WriteProperty("collision", particleSystem.collision);
			writer.WriteProperty("colorBySpeed", particleSystem.colorBySpeed);
			writer.WriteProperty("colorOverLifetime", particleSystem.colorOverLifetime);
			writer.WriteProperty("emission", particleSystem.emission);
			writer.WriteProperty("externalForces", particleSystem.externalForces);
			writer.WriteProperty("forceOverLifetime", particleSystem.forceOverLifetime);
			writer.WriteProperty("inheritVelocity", particleSystem.inheritVelocity);
			writer.WriteProperty("lights", particleSystem.lights);
			writer.WriteProperty("limitVelocityOverLifetime", particleSystem.limitVelocityOverLifetime);
			writer.WriteProperty("main", particleSystem.main);
			writer.WriteProperty("noise", particleSystem.noise);
			writer.WriteProperty("rotationBySpeed", particleSystem.rotationBySpeed);
			writer.WriteProperty("rotationOverLifetime", particleSystem.rotationOverLifetime);
			writer.WriteProperty("shape", particleSystem.shape);
			writer.WriteProperty("sizeBySpeed", particleSystem.sizeBySpeed);
			writer.WriteProperty("sizeOverLifetime", particleSystem.sizeOverLifetime);
			writer.WriteProperty("subEmitters", particleSystem.subEmitters);
			writer.WriteProperty("textureSheetAnimation", particleSystem.textureSheetAnimation);
			writer.WriteProperty("trails", particleSystem.trails);
			writer.WriteProperty("trigger", particleSystem.trigger);
			writer.WriteProperty("useAutoRandomSeed", particleSystem.useAutoRandomSeed);
			writer.WriteProperty("velocityOverLifetime", particleSystem.velocityOverLifetime);
			writer.WriteProperty("isPaused", particleSystem.isPaused);
			writer.WriteProperty("isPlaying", particleSystem.isPlaying);
			writer.WriteProperty("isStopped", particleSystem.isStopped);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ParticleSystem particleSystem = (ParticleSystem)obj;
			particleSystem.Stop();
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2158511498U)
				{
					if (num <= 1155201246U)
					{
						if (num <= 515337733U)
						{
							if (num != 101377077U)
							{
								if (num != 425118420U)
								{
									if (num == 515337733U)
									{
										if (text == "isPlaying")
										{
											if (reader.Read<bool>(ES3Type_bool.Instance))
											{
												particleSystem.Play();
												continue;
											}
											continue;
										}
									}
								}
								else if (text == "lights")
								{
									reader.ReadInto<ParticleSystem.LightsModule>(particleSystem.lights, ES3Type_LightsModule.Instance);
									continue;
								}
							}
							else if (text == "forceOverLifetime")
							{
								reader.ReadInto<ParticleSystem.ForceOverLifetimeModule>(particleSystem.forceOverLifetime, ES3Type_ForceOverLifetimeModule.Instance);
								continue;
							}
						}
						else if (num != 561437583U)
						{
							if (num != 595173982U)
							{
								if (num == 1155201246U)
								{
									if (text == "limitVelocityOverLifetime")
									{
										reader.ReadInto<ParticleSystem.LimitVelocityOverLifetimeModule>(particleSystem.limitVelocityOverLifetime, ES3Type_LimitVelocityOverLifetimeModule.Instance);
										continue;
									}
								}
							}
							else if (text == "isStopped")
							{
								if (reader.Read<bool>(ES3Type_bool.Instance))
								{
									particleSystem.Stop();
									continue;
								}
								continue;
							}
						}
						else if (text == "collision")
						{
							reader.ReadInto<ParticleSystem.CollisionModule>(particleSystem.collision, ES3Type_CollisionModule.Instance);
							continue;
						}
					}
					else if (num <= 1836398822U)
					{
						if (num != 1564253156U)
						{
							if (num != 1832577939U)
							{
								if (num == 1836398822U)
								{
									if (text == "externalForces")
									{
										reader.ReadInto<ParticleSystem.ExternalForcesModule>(particleSystem.externalForces, ES3Type_ExternalForcesModule.Instance);
										continue;
									}
								}
							}
							else if (text == "velocityOverLifetime")
							{
								reader.ReadInto<ParticleSystem.VelocityOverLifetimeModule>(particleSystem.velocityOverLifetime, ES3Type_VelocityOverLifetimeModule.Instance);
								continue;
							}
						}
						else if (text == "time")
						{
							particleSystem.time = reader.Read<float>();
							continue;
						}
					}
					else if (num <= 1991692046U)
					{
						if (num != 1967206915U)
						{
							if (num == 1991692046U)
							{
								if (text == "emission")
								{
									reader.ReadInto<ParticleSystem.EmissionModule>(particleSystem.emission, ES3Type_EmissionModule.Instance);
									continue;
								}
							}
						}
						else if (text == "trigger")
						{
							reader.ReadInto<ParticleSystem.TriggerModule>(particleSystem.trigger, ES3Type_TriggerModule.Instance);
							continue;
						}
					}
					else if (num != 2020906640U)
					{
						if (num == 2158511498U)
						{
							if (text == "subEmitters")
							{
								reader.ReadInto<ParticleSystem.SubEmittersModule>(particleSystem.subEmitters, ES3Type_SubEmittersModule.Instance);
								continue;
							}
						}
					}
					else if (text == "trails")
					{
						reader.ReadInto<ParticleSystem.TrailModule>(particleSystem.trails, ES3Type_TrailModule.Instance);
						continue;
					}
				}
				else if (num <= 2839690237U)
				{
					if (num <= 2564403952U)
					{
						if (num != 2420381393U)
						{
							if (num != 2427400829U)
							{
								if (num == 2564403952U)
								{
									if (text == "colorBySpeed")
									{
										reader.ReadInto<ParticleSystem.ColorBySpeedModule>(particleSystem.colorBySpeed, ES3Type_ColorBySpeedModule.Instance);
										continue;
									}
								}
							}
							else if (text == "sizeOverLifetime")
							{
								reader.ReadInto<ParticleSystem.SizeOverLifetimeModule>(particleSystem.sizeOverLifetime, ES3Type_SizeOverLifetimeModule.Instance);
								continue;
							}
						}
						else if (text == "noise")
						{
							reader.ReadInto<ParticleSystem.NoiseModule>(particleSystem.noise, ES3Type_NoiseModule.Instance);
							continue;
						}
					}
					else if (num <= 2711625934U)
					{
						if (num != 2646858022U)
						{
							if (num == 2711625934U)
							{
								if (text == "rotationOverLifetime")
								{
									reader.ReadInto<ParticleSystem.RotationOverLifetimeModule>(particleSystem.rotationOverLifetime, ES3Type_RotationOverLifetimeModule.Instance);
									continue;
								}
							}
						}
						else if (text == "shape")
						{
							reader.ReadInto<ParticleSystem.ShapeModule>(particleSystem.shape, ES3Type_ShapeModule.Instance);
							continue;
						}
					}
					else if (num != 2726448367U)
					{
						if (num == 2839690237U)
						{
							if (text == "rotationBySpeed")
							{
								reader.ReadInto<ParticleSystem.RotationBySpeedModule>(particleSystem.rotationBySpeed, ES3Type_RotationBySpeedModule.Instance);
								continue;
							}
						}
					}
					else if (text == "textureSheetAnimation")
					{
						reader.ReadInto<ParticleSystem.TextureSheetAnimationModule>(particleSystem.textureSheetAnimation, ES3Type_TextureSheetAnimationModule.Instance);
						continue;
					}
				}
				else if (num <= 3353895332U)
				{
					if (num != 3024251827U)
					{
						if (num != 3133124763U)
						{
							if (num == 3353895332U)
							{
								if (text == "sizeBySpeed")
								{
									reader.ReadInto<ParticleSystem.SizeBySpeedModule>(particleSystem.sizeBySpeed, ES3Type_SizeBySpeedModule.Instance);
									continue;
								}
							}
						}
						else if (text == "isPaused")
						{
							if (reader.Read<bool>(ES3Type_bool.Instance))
							{
								particleSystem.Pause();
								continue;
							}
							continue;
						}
					}
					else if (text == "inheritVelocity")
					{
						reader.ReadInto<ParticleSystem.InheritVelocityModule>(particleSystem.inheritVelocity, ES3Type_InheritVelocityModule.Instance);
						continue;
					}
				}
				else if (num <= 3935363592U)
				{
					if (num != 3881587361U)
					{
						if (num == 3935363592U)
						{
							if (text == "main")
							{
								reader.ReadInto<ParticleSystem.MainModule>(particleSystem.main, ES3Type_MainModule.Instance);
								continue;
							}
						}
					}
					else if (text == "colorOverLifetime")
					{
						reader.ReadInto<ParticleSystem.ColorOverLifetimeModule>(particleSystem.colorOverLifetime, ES3Type_ColorOverLifetimeModule.Instance);
						continue;
					}
				}
				else if (num != 3944566772U)
				{
					if (num == 4251907809U)
					{
						if (text == "useAutoRandomSeed")
						{
							particleSystem.useAutoRandomSeed = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
				}
				else if (text == "hideFlags")
				{
					particleSystem.hideFlags = reader.Read<HideFlags>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000BB RID: 187
		public static ES3Type Instance;
	}
}
