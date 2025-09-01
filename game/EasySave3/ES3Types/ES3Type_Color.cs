using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000080 RID: 128
	[Preserve]
	[ES3Properties(new string[]
	{
		"r",
		"g",
		"b",
		"a"
	})]
	public class ES3Type_Color : ES3Type
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00010158 File Offset: 0x0000E358
		public ES3Type_Color() : base(typeof(Color))
		{
			ES3Type_Color.Instance = this;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010170 File Offset: 0x0000E370
		public override void Write(object obj, ES3Writer writer)
		{
			Color color = (Color)obj;
			writer.WriteProperty("r", color.r, ES3Type_float.Instance);
			writer.WriteProperty("g", color.g, ES3Type_float.Instance);
			writer.WriteProperty("b", color.b, ES3Type_float.Instance);
			writer.WriteProperty("a", color.a, ES3Type_float.Instance);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public override object Read<T>(ES3Reader reader)
		{
			return new Color(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000D0 RID: 208
		public static ES3Type Instance;
	}
}
