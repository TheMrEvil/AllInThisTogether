using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000CC RID: 204
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y",
		"z",
		"w"
	})]
	public class ES3Type_Vector4 : ES3Type
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0001A86E File Offset: 0x00018A6E
		public ES3Type_Vector4() : base(typeof(Vector4))
		{
			ES3Type_Vector4.Instance = this;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001A888 File Offset: 0x00018A88
		public override void Write(object obj, ES3Writer writer)
		{
			Vector4 vector = (Vector4)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
			writer.WriteProperty("z", vector.z, ES3Type_float.Instance);
			writer.WriteProperty("w", vector.w, ES3Type_float.Instance);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001A908 File Offset: 0x00018B08
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector4(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001A940 File Offset: 0x00018B40
		public static bool Equals(Vector4 a, Vector4 b)
		{
			return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z) && Mathf.Approximately(a.w, b.w);
		}

		// Token: 0x0400011F RID: 287
		public static ES3Type Instance;
	}
}
