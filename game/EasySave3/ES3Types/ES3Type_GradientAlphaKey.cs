using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000090 RID: 144
	[Preserve]
	[ES3Properties(new string[]
	{
		"alpha",
		"time"
	})]
	public class ES3Type_GradientAlphaKey : ES3Type
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00011721 File Offset: 0x0000F921
		public ES3Type_GradientAlphaKey() : base(typeof(GradientAlphaKey))
		{
			ES3Type_GradientAlphaKey.Instance = this;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001173C File Offset: 0x0000F93C
		public override void Write(object obj, ES3Writer writer)
		{
			GradientAlphaKey gradientAlphaKey = (GradientAlphaKey)obj;
			writer.WriteProperty("alpha", gradientAlphaKey.alpha, ES3Type_float.Instance);
			writer.WriteProperty("time", gradientAlphaKey.time, ES3Type_float.Instance);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011786 File Offset: 0x0000F986
		public override object Read<T>(ES3Reader reader)
		{
			return new GradientAlphaKey(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000E3 RID: 227
		public static ES3Type Instance;
	}
}
