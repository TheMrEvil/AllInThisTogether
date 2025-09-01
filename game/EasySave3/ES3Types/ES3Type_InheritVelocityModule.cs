using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000096 RID: 150
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"mode",
		"curve",
		"curveMultiplier"
	})]
	public class ES3Type_InheritVelocityModule : ES3Type
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000118E9 File Offset: 0x0000FAE9
		public ES3Type_InheritVelocityModule() : base(typeof(ParticleSystem.InheritVelocityModule))
		{
			ES3Type_InheritVelocityModule.Instance = this;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011904 File Offset: 0x0000FB04
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = (ParticleSystem.InheritVelocityModule)obj;
			writer.WriteProperty("enabled", inheritVelocityModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("mode", inheritVelocityModule.mode);
			writer.WriteProperty("curve", inheritVelocityModule.curve, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("curveMultiplier", inheritVelocityModule.curveMultiplier, ES3Type_float.Instance);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011984 File Offset: 0x0000FB84
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = default(ParticleSystem.InheritVelocityModule);
			this.ReadInto<T>(reader, inheritVelocityModule);
			return inheritVelocityModule;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000119AC File Offset: 0x0000FBAC
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = (ParticleSystem.InheritVelocityModule)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "enabled"))
				{
					if (!(a == "mode"))
					{
						if (!(a == "curve"))
						{
							if (!(a == "curveMultiplier"))
							{
								reader.Skip();
							}
							else
							{
								inheritVelocityModule.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							inheritVelocityModule.curve = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						}
					}
					else
					{
						inheritVelocityModule.mode = reader.Read<ParticleSystemInheritVelocityMode>();
					}
				}
				else
				{
					inheritVelocityModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000E9 RID: 233
		public static ES3Type Instance;
	}
}
