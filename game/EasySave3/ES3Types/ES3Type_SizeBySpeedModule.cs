using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B5 RID: 181
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
		"separateAxes",
		"range"
	})]
	public class ES3Type_SizeBySpeedModule : ES3Type
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0001797C File Offset: 0x00015B7C
		public ES3Type_SizeBySpeedModule() : base(typeof(ParticleSystem.SizeBySpeedModule))
		{
			ES3Type_SizeBySpeedModule.Instance = this;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00017994 File Offset: 0x00015B94
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.SizeBySpeedModule sizeBySpeedModule = (ParticleSystem.SizeBySpeedModule)obj;
			writer.WriteProperty("enabled", sizeBySpeedModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("size", sizeBySpeedModule.size, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("sizeMultiplier", sizeBySpeedModule.sizeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("x", sizeBySpeedModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", sizeBySpeedModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("y", sizeBySpeedModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("yMultiplier", sizeBySpeedModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("z", sizeBySpeedModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("zMultiplier", sizeBySpeedModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", sizeBySpeedModule.separateAxes, ES3Type_bool.Instance);
			writer.WriteProperty("range", sizeBySpeedModule.range, ES3Type_Vector2.Instance);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00017ADC File Offset: 0x00015CDC
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.SizeBySpeedModule sizeBySpeedModule = default(ParticleSystem.SizeBySpeedModule);
			this.ReadInto<T>(reader, sizeBySpeedModule);
			return sizeBySpeedModule;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00017B04 File Offset: 0x00015D04
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.SizeBySpeedModule sizeBySpeedModule = (ParticleSystem.SizeBySpeedModule)obj;
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
									sizeBySpeedModule.size = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							sizeBySpeedModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
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
									sizeBySpeedModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sizeMultiplier")
						{
							sizeBySpeedModule.sizeMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "zMultiplier")
					{
						sizeBySpeedModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 4208725202U)
				{
					if (num != 3281097867U)
					{
						if (num != 3709916316U)
						{
							if (num == 4208725202U)
							{
								if (text == "range")
								{
									sizeBySpeedModule.range = reader.Read<Vector2>(ES3Type_Vector2.Instance);
									continue;
								}
							}
						}
						else if (text == "xMultiplier")
						{
							sizeBySpeedModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "yMultiplier")
					{
						sizeBySpeedModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
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
								sizeBySpeedModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "x")
					{
						sizeBySpeedModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (text == "y")
				{
					sizeBySpeedModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000108 RID: 264
		public static ES3Type Instance;
	}
}
