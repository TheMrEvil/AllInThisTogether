using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A4 RID: 164
	[Preserve]
	[ES3Properties(new string[]
	{
		"mode",
		"gradientMax",
		"gradientMin",
		"colorMax",
		"colorMin",
		"color",
		"gradient"
	})]
	public class ES3Type_MinMaxGradient : ES3Type
	{
		// Token: 0x0600039F RID: 927 RVA: 0x00014BEC File Offset: 0x00012DEC
		public ES3Type_MinMaxGradient() : base(typeof(ParticleSystem.MinMaxGradient))
		{
			ES3Type_MinMaxGradient.Instance = this;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00014C04 File Offset: 0x00012E04
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.MinMaxGradient minMaxGradient = (ParticleSystem.MinMaxGradient)obj;
			writer.WriteProperty("mode", minMaxGradient.mode);
			writer.WriteProperty("gradientMax", minMaxGradient.gradientMax, ES3Type_Gradient.Instance);
			writer.WriteProperty("gradientMin", minMaxGradient.gradientMin, ES3Type_Gradient.Instance);
			writer.WriteProperty("colorMax", minMaxGradient.colorMax, ES3Type_Color.Instance);
			writer.WriteProperty("colorMin", minMaxGradient.colorMin, ES3Type_Color.Instance);
			writer.WriteProperty("color", minMaxGradient.color, ES3Type_Color.Instance);
			writer.WriteProperty("gradient", minMaxGradient.gradient, ES3Type_Gradient.Instance);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.MinMaxGradient minMaxGradient = default(ParticleSystem.MinMaxGradient);
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1650383577U)
				{
					if (num != 1031692888U)
					{
						if (num != 1414216983U)
						{
							if (num == 1650383577U)
							{
								if (text == "gradientMax")
								{
									minMaxGradient.gradientMax = reader.Read<Gradient>(ES3Type_Gradient.Instance);
									continue;
								}
							}
						}
						else if (text == "gradientMin")
						{
							minMaxGradient.gradientMin = reader.Read<Gradient>(ES3Type_Gradient.Instance);
							continue;
						}
					}
					else if (text == "color")
					{
						minMaxGradient.color = reader.Read<Color>(ES3Type_Color.Instance);
						continue;
					}
				}
				else if (num <= 3664058134U)
				{
					if (num != 2633020069U)
					{
						if (num == 3664058134U)
						{
							if (text == "colorMax")
							{
								minMaxGradient.colorMax = reader.Read<Color>(ES3Type_Color.Instance);
								continue;
							}
						}
					}
					else if (text == "gradient")
					{
						minMaxGradient.gradient = reader.Read<Gradient>(ES3Type_Gradient.Instance);
						continue;
					}
				}
				else if (num != 3964775348U)
				{
					if (num == 3966689298U)
					{
						if (text == "mode")
						{
							minMaxGradient.mode = reader.Read<ParticleSystemGradientMode>();
							continue;
						}
					}
				}
				else if (text == "colorMin")
				{
					minMaxGradient.colorMin = reader.Read<Color>(ES3Type_Color.Instance);
					continue;
				}
				reader.Skip();
			}
			return minMaxGradient;
		}

		// Token: 0x040000F7 RID: 247
		public static ES3Type Instance;
	}
}
