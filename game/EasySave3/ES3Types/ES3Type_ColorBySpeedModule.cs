using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000084 RID: 132
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"color",
		"range"
	})]
	public class ES3Type_ColorBySpeedModule : ES3Type
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00010372 File Offset: 0x0000E572
		public ES3Type_ColorBySpeedModule() : base(typeof(ParticleSystem.ColorBySpeedModule))
		{
			ES3Type_ColorBySpeedModule.Instance = this;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001038C File Offset: 0x0000E58C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = (ParticleSystem.ColorBySpeedModule)obj;
			writer.WriteProperty("enabled", colorBySpeedModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("color", colorBySpeedModule.color, ES3Type_MinMaxGradient.Instance);
			writer.WriteProperty("range", colorBySpeedModule.range, ES3Type_Vector2.Instance);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = default(ParticleSystem.ColorBySpeedModule);
			this.ReadInto<T>(reader, colorBySpeedModule);
			return colorBySpeedModule;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001041C File Offset: 0x0000E61C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = (ParticleSystem.ColorBySpeedModule)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "enabled"))
				{
					if (!(a == "color"))
					{
						if (!(a == "range"))
						{
							reader.Skip();
						}
						else
						{
							colorBySpeedModule.range = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						}
					}
					else
					{
						colorBySpeedModule.color = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
					}
				}
				else
				{
					colorBySpeedModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000D4 RID: 212
		public static ES3Type Instance;
	}
}
