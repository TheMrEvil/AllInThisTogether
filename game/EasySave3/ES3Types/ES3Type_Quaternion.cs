using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AA RID: 170
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y",
		"z",
		"w"
	})]
	public class ES3Type_Quaternion : ES3Type
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00015B41 File Offset: 0x00013D41
		public ES3Type_Quaternion() : base(typeof(Quaternion))
		{
			ES3Type_Quaternion.Instance = this;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015B5C File Offset: 0x00013D5C
		public override void Write(object obj, ES3Writer writer)
		{
			Quaternion quaternion = (Quaternion)obj;
			writer.WriteProperty("x", quaternion.x, ES3Type_float.Instance);
			writer.WriteProperty("y", quaternion.y, ES3Type_float.Instance);
			writer.WriteProperty("z", quaternion.z, ES3Type_float.Instance);
			writer.WriteProperty("w", quaternion.w, ES3Type_float.Instance);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015BDC File Offset: 0x00013DDC
		public override object Read<T>(ES3Reader reader)
		{
			return new Quaternion(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x040000FD RID: 253
		public static ES3Type Instance;
	}
}
