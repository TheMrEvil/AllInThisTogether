using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B6 RID: 182
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"size",
		"sizeMultiplier",
		"x",
		"xMultiplier",
		"y",
		"yMultiplier",
		"z",
		"zMultiplier",
		"separateAxes"
	})]
	public class ES3Type_SizeOverLifetimeModule : ES3Type
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00017DA8 File Offset: 0x00015FA8
		public ES3Type_SizeOverLifetimeModule() : base(typeof(ParticleSystem.SizeOverLifetimeModule))
		{
			ES3Type_SizeOverLifetimeModule.Instance = this;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = (ParticleSystem.SizeOverLifetimeModule)obj;
			writer.WriteProperty("enabled", sizeOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("size", sizeOverLifetimeModule.size, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("sizeMultiplier", sizeOverLifetimeModule.sizeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("x", sizeOverLifetimeModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", sizeOverLifetimeModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("y", sizeOverLifetimeModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("yMultiplier", sizeOverLifetimeModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("z", sizeOverLifetimeModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("zMultiplier", sizeOverLifetimeModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", sizeOverLifetimeModule.separateAxes, ES3Type_bool.Instance);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00017EEC File Offset: 0x000160EC
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = default(ParticleSystem.SizeOverLifetimeModule);
			this.ReadInto<T>(reader, sizeOverLifetimeModule);
			return sizeOverLifetimeModule;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017F14 File Offset: 0x00016114
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = (ParticleSystem.SizeOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1479031685U)
				{
					if (num <= 597743964U)
					{
						if (num != 49525662U)
						{
							if (num == 597743964U)
							{
								if (text == "size")
								{
									sizeOverLifetimeModule.size = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							sizeOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 726266686U)
					{
						if (num != 1105436259U)
						{
							if (num == 1479031685U)
							{
								if (text == "separateAxes")
								{
									sizeOverLifetimeModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sizeMultiplier")
						{
							sizeOverLifetimeModule.sizeMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "zMultiplier")
					{
						sizeOverLifetimeModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3709916316U)
				{
					if (num != 3281097867U)
					{
						if (num == 3709916316U)
						{
							if (text == "xMultiplier")
							{
								sizeOverLifetimeModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "yMultiplier")
					{
						sizeOverLifetimeModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 4228665076U)
				{
					if (num != 4245442695U)
					{
						if (num == 4278997933U)
						{
							if (text == "z")
							{
								sizeOverLifetimeModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "x")
					{
						sizeOverLifetimeModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (text == "y")
				{
					sizeOverLifetimeModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000109 RID: 265
		public static ES3Type Instance;
	}
}
