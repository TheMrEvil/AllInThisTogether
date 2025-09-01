using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000CA RID: 202
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y",
		"z"
	})]
	public class ES3Type_Vector3Int : ES3Type
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0001A7A3 File Offset: 0x000189A3
		public ES3Type_Vector3Int() : base(typeof(Vector3Int))
		{
			ES3Type_Vector3Int.Instance = this;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001A7BC File Offset: 0x000189BC
		public override void Write(object obj, ES3Writer writer)
		{
			Vector3Int vector3Int = (Vector3Int)obj;
			writer.WriteProperty("x", vector3Int.x, ES3Type_int.Instance);
			writer.WriteProperty("y", vector3Int.y, ES3Type_int.Instance);
			writer.WriteProperty("z", vector3Int.z, ES3Type_int.Instance);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001A824 File Offset: 0x00018A24
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector3Int(reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance));
		}

		// Token: 0x0400011D RID: 285
		public static ES3Type Instance;
	}
}
