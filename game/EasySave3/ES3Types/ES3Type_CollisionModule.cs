using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200007F RID: 127
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"type",
		"mode",
		"dampen",
		"dampenMultiplier",
		"bounce",
		"bounceMultiplier",
		"lifetimeLoss",
		"lifetimeLossMultiplier",
		"minKillSpeed",
		"maxKillSpeed",
		"collidesWith",
		"enableDynamicColliders",
		"maxCollisionShapes",
		"quality",
		"voxelSize",
		"radiusScale",
		"sendCollisionMessages"
	})]
	public class ES3Type_CollisionModule : ES3Type
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000FB5B File Offset: 0x0000DD5B
		public ES3Type_CollisionModule() : base(typeof(ParticleSystem.CollisionModule))
		{
			ES3Type_CollisionModule.Instance = this;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.CollisionModule collisionModule = (ParticleSystem.CollisionModule)obj;
			writer.WriteProperty("enabled", collisionModule.enabled);
			writer.WriteProperty("type", collisionModule.type);
			writer.WriteProperty("mode", collisionModule.mode);
			writer.WriteProperty("dampen", collisionModule.dampen);
			writer.WriteProperty("dampenMultiplier", collisionModule.dampenMultiplier);
			writer.WriteProperty("bounce", collisionModule.bounce);
			writer.WriteProperty("bounceMultiplier", collisionModule.bounceMultiplier);
			writer.WriteProperty("lifetimeLoss", collisionModule.lifetimeLoss);
			writer.WriteProperty("lifetimeLossMultiplier", collisionModule.lifetimeLossMultiplier);
			writer.WriteProperty("minKillSpeed", collisionModule.minKillSpeed);
			writer.WriteProperty("maxKillSpeed", collisionModule.maxKillSpeed);
			writer.WriteProperty("collidesWith", collisionModule.collidesWith);
			writer.WriteProperty("enableDynamicColliders", collisionModule.enableDynamicColliders);
			writer.WriteProperty("maxCollisionShapes", collisionModule.maxCollisionShapes);
			writer.WriteProperty("quality", collisionModule.quality);
			writer.WriteProperty("voxelSize", collisionModule.voxelSize);
			writer.WriteProperty("radiusScale", collisionModule.radiusScale);
			writer.WriteProperty("sendCollisionMessages", collisionModule.sendCollisionMessages);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.CollisionModule collisionModule = default(ParticleSystem.CollisionModule);
			this.ReadInto<T>(reader, collisionModule);
			return collisionModule;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.CollisionModule collisionModule = (ParticleSystem.CollisionModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2437840781U)
				{
					if (num <= 862152253U)
					{
						if (num <= 465431036U)
						{
							if (num != 49525662U)
							{
								if (num == 465431036U)
								{
									if (text == "enableDynamicColliders")
									{
										collisionModule.enableDynamicColliders = reader.Read<bool>();
										continue;
									}
								}
							}
							else if (text == "enabled")
							{
								collisionModule.enabled = reader.Read<bool>();
								continue;
							}
						}
						else if (num != 570285707U)
						{
							if (num == 862152253U)
							{
								if (text == "bounce")
								{
									collisionModule.bounce = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "dampenMultiplier")
						{
							collisionModule.dampenMultiplier = reader.Read<float>();
							continue;
						}
					}
					else if (num <= 1694773170U)
					{
						if (num != 1361572173U)
						{
							if (num == 1694773170U)
							{
								if (text == "minKillSpeed")
								{
									collisionModule.minKillSpeed = reader.Read<float>();
									continue;
								}
							}
						}
						else if (text == "type")
						{
							collisionModule.type = reader.Read<ParticleSystemCollisionType>();
							continue;
						}
					}
					else if (num != 1805931883U)
					{
						if (num != 2433286904U)
						{
							if (num == 2437840781U)
							{
								if (text == "maxCollisionShapes")
								{
									collisionModule.maxCollisionShapes = reader.Read<int>();
									continue;
								}
							}
						}
						else if (text == "maxKillSpeed")
						{
							collisionModule.maxKillSpeed = reader.Read<float>();
							continue;
						}
					}
					else if (text == "radiusScale")
					{
						collisionModule.radiusScale = reader.Read<float>();
						continue;
					}
				}
				else if (num <= 2773358386U)
				{
					if (num <= 2597670950U)
					{
						if (num != 2541892782U)
						{
							if (num == 2597670950U)
							{
								if (text == "quality")
								{
									collisionModule.quality = reader.Read<ParticleSystemCollisionQuality>();
									continue;
								}
							}
						}
						else if (text == "voxelSize")
						{
							collisionModule.voxelSize = reader.Read<float>();
							continue;
						}
					}
					else if (num != 2641607025U)
					{
						if (num == 2773358386U)
						{
							if (text == "lifetimeLossMultiplier")
							{
								collisionModule.lifetimeLossMultiplier = reader.Read<float>();
								continue;
							}
						}
					}
					else if (text == "sendCollisionMessages")
					{
						collisionModule.sendCollisionMessages = reader.Read<bool>();
						continue;
					}
				}
				else if (num <= 3536893974U)
				{
					if (num != 3170093300U)
					{
						if (num == 3536893974U)
						{
							if (text == "collidesWith")
							{
								collisionModule.collidesWith = reader.Read<LayerMask>();
								continue;
							}
						}
					}
					else if (text == "dampen")
					{
						collisionModule.dampen = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num != 3747271745U)
				{
					if (num != 3944693454U)
					{
						if (num == 3966689298U)
						{
							if (text == "mode")
							{
								collisionModule.mode = reader.Read<ParticleSystemCollisionMode>();
								continue;
							}
						}
					}
					else if (text == "bounceMultiplier")
					{
						collisionModule.bounceMultiplier = reader.Read<float>();
						continue;
					}
				}
				else if (text == "lifetimeLoss")
				{
					collisionModule.lifetimeLoss = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000CF RID: 207
		public static ES3Type Instance;
	}
}
