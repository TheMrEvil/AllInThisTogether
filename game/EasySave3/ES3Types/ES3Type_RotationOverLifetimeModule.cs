using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B1 RID: 177
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"x",
		"xMultiplier",
		"y",
		"yMultiplier",
		"z",
		"zMultiplier",
		"separateAxes"
	})]
	public class ES3Type_RotationOverLifetimeModule : ES3Type
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x00016E8E File Offset: 0x0001508E
		public ES3Type_RotationOverLifetimeModule() : base(typeof(ParticleSystem.RotationOverLifetimeModule))
		{
			ES3Type_RotationOverLifetimeModule.Instance = this;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00016EA8 File Offset: 0x000150A8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule = (ParticleSystem.RotationOverLifetimeModule)obj;
			writer.WriteProperty("enabled", rotationOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("x", rotationOverLifetimeModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", rotationOverLifetimeModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("y", rotationOverLifetimeModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("yMultiplier", rotationOverLifetimeModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("z", rotationOverLifetimeModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("zMultiplier", rotationOverLifetimeModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", rotationOverLifetimeModule.separateAxes, ES3Type_bool.Instance);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00016F9C File Offset: 0x0001519C
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule = default(ParticleSystem.RotationOverLifetimeModule);
			this.ReadInto<T>(reader, rotationOverLifetimeModule);
			return rotationOverLifetimeModule;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00016FC4 File Offset: 0x000151C4
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule = (ParticleSystem.RotationOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3281097867U)
				{
					if (num <= 726266686U)
					{
						if (num != 49525662U)
						{
							if (num == 726266686U)
							{
								if (text == "zMultiplier")
								{
									rotationOverLifetimeModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							rotationOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 1479031685U)
					{
						if (num == 3281097867U)
						{
							if (text == "yMultiplier")
							{
								rotationOverLifetimeModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "separateAxes")
					{
						rotationOverLifetimeModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 4228665076U)
				{
					if (num != 3709916316U)
					{
						if (num == 4228665076U)
						{
							if (text == "y")
							{
								rotationOverLifetimeModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "xMultiplier")
					{
						rotationOverLifetimeModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 4245442695U)
				{
					if (num == 4278997933U)
					{
						if (text == "z")
						{
							rotationOverLifetimeModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
				}
				else if (text == "x")
				{
					rotationOverLifetimeModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000104 RID: 260
		public static ES3Type Instance;
	}
}
