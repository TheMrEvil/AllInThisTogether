using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000092 RID: 146
	[Preserve]
	[ES3Properties(new string[]
	{
		"color",
		"time"
	})]
	public class ES3Type_GradientColorKey : ES3Type
	{
		// Token: 0x0600036C RID: 876 RVA: 0x000117C5 File Offset: 0x0000F9C5
		public ES3Type_GradientColorKey() : base(typeof(GradientColorKey))
		{
			ES3Type_GradientColorKey.Instance = this;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public override void Write(object obj, ES3Writer writer)
		{
			GradientColorKey gradientColorKey = (GradientColorKey)obj;
			writer.WriteProperty("color", gradientColorKey.color, ES3Type_Color.Instance);
			writer.WriteProperty("time", gradientColorKey.time, ES3Type_float.Instance);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001182A File Offset: 0x0000FA2A
		public override object Read<T>(ES3Reader reader)
		{
			return new GradientColorKey(reader.ReadProperty<Color>(ES3Type_Color.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000E5 RID: 229
		public static ES3Type Instance;
	}
}
