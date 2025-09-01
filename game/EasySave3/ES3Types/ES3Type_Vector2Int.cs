using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000C6 RID: 198
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y"
	})]
	public class ES3Type_Vector2Int : ES3Type
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0001A635 File Offset: 0x00018835
		public ES3Type_Vector2Int() : base(typeof(Vector2Int))
		{
			ES3Type_Vector2Int.Instance = this;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001A650 File Offset: 0x00018850
		public override void Write(object obj, ES3Writer writer)
		{
			Vector2Int vector2Int = (Vector2Int)obj;
			writer.WriteProperty("x", vector2Int.x, ES3Type_int.Instance);
			writer.WriteProperty("y", vector2Int.y, ES3Type_int.Instance);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001A69C File Offset: 0x0001889C
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector2Int(reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance));
		}

		// Token: 0x04000119 RID: 281
		public static ES3Type Instance;
	}
}
