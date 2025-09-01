using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000086 RID: 134
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"rateOverTime",
		"rateOverTimeMultiplier",
		"rateOverDistance",
		"rateOverDistanceMultiplier"
	})]
	public class ES3Type_EmissionModule : ES3Type
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0001059E File Offset: 0x0000E79E
		public ES3Type_EmissionModule() : base(typeof(ParticleSystem.EmissionModule))
		{
			ES3Type_EmissionModule.Instance = this;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)obj;
			writer.WriteProperty("enabled", emissionModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("rateOverTime", emissionModule.rateOverTime, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("rateOverTimeMultiplier", emissionModule.rateOverTimeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("rateOverDistance", emissionModule.rateOverDistance, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("rateOverDistanceMultiplier", emissionModule.rateOverDistanceMultiplier, ES3Type_float.Instance);
			ParticleSystem.Burst[] array = new ParticleSystem.Burst[emissionModule.burstCount];
			emissionModule.GetBursts(array);
			writer.WriteProperty("bursts", array, ES3Type_BurstArray.Instance);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010680 File Offset: 0x0000E880
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.EmissionModule emissionModule = default(ParticleSystem.EmissionModule);
			this.ReadInto<T>(reader, emissionModule);
			return emissionModule;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000106A8 File Offset: 0x0000E8A8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "enabled"))
				{
					if (!(a == "rateOverTime"))
					{
						if (!(a == "rateOverTimeMultiplier"))
						{
							if (!(a == "rateOverDistance"))
							{
								if (!(a == "rateOverDistanceMultiplier"))
								{
									if (!(a == "bursts"))
									{
										reader.Skip();
									}
									else
									{
										emissionModule.SetBursts(reader.Read<ParticleSystem.Burst[]>(ES3Type_BurstArray.Instance));
									}
								}
								else
								{
									emissionModule.rateOverDistanceMultiplier = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								emissionModule.rateOverDistance = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							}
						}
						else
						{
							emissionModule.rateOverTimeMultiplier = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						emissionModule.rateOverTime = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					}
				}
				else
				{
					emissionModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000D6 RID: 214
		public static ES3Type Instance;
	}
}
