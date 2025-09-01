using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200009C RID: 156
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"limitX",
		"limitXMultiplier",
		"limitY",
		"limitYMultiplier",
		"limitZ",
		"limitZMultiplier",
		"limit",
		"limitMultiplier",
		"dampen",
		"separateAxes",
		"space"
	})]
	public class ES3Type_LimitVelocityOverLifetimeModule : ES3Type
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00012759 File Offset: 0x00010959
		public ES3Type_LimitVelocityOverLifetimeModule() : base(typeof(ParticleSystem.LimitVelocityOverLifetimeModule))
		{
			ES3Type_LimitVelocityOverLifetimeModule.Instance = this;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00012774 File Offset: 0x00010974
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetimeModule = (ParticleSystem.LimitVelocityOverLifetimeModule)obj;
			writer.WriteProperty("enabled", limitVelocityOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("limitX", limitVelocityOverLifetimeModule.limitX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("limitXMultiplier", limitVelocityOverLifetimeModule.limitXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("limitY", limitVelocityOverLifetimeModule.limitY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("limitYMultiplier", limitVelocityOverLifetimeModule.limitYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("limitZ", limitVelocityOverLifetimeModule.limitZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("limitZMultiplier", limitVelocityOverLifetimeModule.limitZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("limit", limitVelocityOverLifetimeModule.limit, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("limitMultiplier", limitVelocityOverLifetimeModule.limitMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("dampen", limitVelocityOverLifetimeModule.dampen, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", limitVelocityOverLifetimeModule.separateAxes, ES3Type_bool.Instance);
			writer.WriteProperty("space", limitVelocityOverLifetimeModule.space);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000128D4 File Offset: 0x00010AD4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetimeModule = default(ParticleSystem.LimitVelocityOverLifetimeModule);
			this.ReadInto<T>(reader, limitVelocityOverLifetimeModule);
			return limitVelocityOverLifetimeModule;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000128FC File Offset: 0x00010AFC
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetimeModule = (ParticleSystem.LimitVelocityOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1319695307U)
				{
					if (num <= 691593499U)
					{
						if (num != 49525662U)
						{
							if (num != 572581548U)
							{
								if (num == 691593499U)
								{
									if (text == "limitXMultiplier")
									{
										limitVelocityOverLifetimeModule.limitXMultiplier = reader.Read<float>(ES3Type_float.Instance);
										continue;
									}
								}
							}
							else if (text == "limitYMultiplier")
							{
								limitVelocityOverLifetimeModule.limitYMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
						else if (text == "enabled")
						{
							limitVelocityOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 853203252U)
					{
						if (num != 894689925U)
						{
							if (num == 1319695307U)
							{
								if (text == "limitMultiplier")
								{
									limitVelocityOverLifetimeModule.limitMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "space")
						{
							limitVelocityOverLifetimeModule.space = reader.Read<ParticleSystemSimulationSpace>();
							continue;
						}
					}
					else if (text == "limit")
					{
						limitVelocityOverLifetimeModule.limit = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (num <= 2072266391U)
				{
					if (num != 1479031685U)
					{
						if (num != 2055488772U)
						{
							if (num == 2072266391U)
							{
								if (text == "limitY")
								{
									limitVelocityOverLifetimeModule.limitY = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "limitX")
						{
							limitVelocityOverLifetimeModule.limitX = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
					else if (text == "separateAxes")
					{
						limitVelocityOverLifetimeModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 2089044010U)
				{
					if (num != 2318232461U)
					{
						if (num == 3170093300U)
						{
							if (text == "dampen")
							{
								limitVelocityOverLifetimeModule.dampen = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "limitZMultiplier")
					{
						limitVelocityOverLifetimeModule.limitZMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (text == "limitZ")
				{
					limitVelocityOverLifetimeModule.limitZ = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000EF RID: 239
		public static ES3Type Instance;
	}
}
