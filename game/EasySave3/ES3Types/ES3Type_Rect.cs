using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AC RID: 172
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y",
		"width",
		"height"
	})]
	public class ES3Type_Rect : ES3Type
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x00015C31 File Offset: 0x00013E31
		public ES3Type_Rect() : base(typeof(Rect))
		{
			ES3Type_Rect.Instance = this;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00015C4C File Offset: 0x00013E4C
		public override void Write(object obj, ES3Writer writer)
		{
			Rect rect = (Rect)obj;
			writer.WriteProperty("x", rect.x, ES3Type_float.Instance);
			writer.WriteProperty("y", rect.y, ES3Type_float.Instance);
			writer.WriteProperty("width", rect.width, ES3Type_float.Instance);
			writer.WriteProperty("height", rect.height, ES3Type_float.Instance);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015CD0 File Offset: 0x00013ED0
		public override object Read<T>(ES3Reader reader)
		{
			return new Rect(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000FF RID: 255
		public static ES3Type Instance;
	}
}
