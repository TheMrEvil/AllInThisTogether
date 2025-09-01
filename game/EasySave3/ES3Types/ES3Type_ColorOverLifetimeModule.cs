using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000085 RID: 133
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"color"
	})]
	public class ES3Type_ColorOverLifetimeModule : ES3Type
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000104A7 File Offset: 0x0000E6A7
		public ES3Type_ColorOverLifetimeModule() : base(typeof(ParticleSystem.ColorOverLifetimeModule))
		{
			ES3Type_ColorOverLifetimeModule.Instance = this;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000104C0 File Offset: 0x0000E6C0
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = (ParticleSystem.ColorOverLifetimeModule)obj;
			writer.WriteProperty("enabled", colorOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("color", colorOverLifetimeModule.color, ES3Type_MinMaxGradient.Instance);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001050C File Offset: 0x0000E70C
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = default(ParticleSystem.ColorOverLifetimeModule);
			this.ReadInto<T>(reader, colorOverLifetimeModule);
			return colorOverLifetimeModule;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010534 File Offset: 0x0000E734
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = (ParticleSystem.ColorOverLifetimeModule)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "enabled"))
				{
					if (!(a == "color"))
					{
						reader.Skip();
					}
					else
					{
						colorOverLifetimeModule.color = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
					}
				}
				else
				{
					colorOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		public static ES3Type Instance;
	}
}
