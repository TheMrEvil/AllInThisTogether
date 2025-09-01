using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000C8 RID: 200
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y",
		"z"
	})]
	public class ES3Type_Vector3 : ES3Type
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0001A6DB File Offset: 0x000188DB
		public ES3Type_Vector3() : base(typeof(Vector3))
		{
			ES3Type_Vector3.Instance = this;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001A6F4 File Offset: 0x000188F4
		public override void Write(object obj, ES3Writer writer)
		{
			Vector3 vector = (Vector3)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
			writer.WriteProperty("z", vector.z, ES3Type_float.Instance);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001A759 File Offset: 0x00018959
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector3(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x0400011B RID: 283
		public static ES3Type Instance;
	}
}
