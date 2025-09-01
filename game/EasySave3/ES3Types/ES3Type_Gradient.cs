using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200008F RID: 143
	[Preserve]
	[ES3Properties(new string[]
	{
		"colorKeys",
		"alphaKeys",
		"mode"
	})]
	public class ES3Type_Gradient : ES3Type
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00011638 File Offset: 0x0000F838
		public ES3Type_Gradient() : base(typeof(Gradient))
		{
			ES3Type_Gradient.Instance = this;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011650 File Offset: 0x0000F850
		public override void Write(object obj, ES3Writer writer)
		{
			Gradient gradient = (Gradient)obj;
			writer.WriteProperty("colorKeys", gradient.colorKeys, ES3Type_GradientColorKeyArray.Instance);
			writer.WriteProperty("alphaKeys", gradient.alphaKeys, ES3Type_GradientAlphaKeyArray.Instance);
			writer.WriteProperty("mode", gradient.mode);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public override object Read<T>(ES3Reader reader)
		{
			Gradient gradient = new Gradient();
			this.ReadInto<T>(reader, gradient);
			return gradient;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Gradient gradient = (Gradient)obj;
			gradient.SetKeys(reader.ReadProperty<GradientColorKey[]>(ES3Type_GradientColorKeyArray.Instance), reader.ReadProperty<GradientAlphaKey[]>(ES3Type_GradientAlphaKeyArray.Instance));
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (a == "mode")
				{
					gradient.mode = reader.Read<GradientMode>();
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x040000E2 RID: 226
		public static ES3Type Instance;
	}
}
