using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000C4 RID: 196
	[Preserve]
	[ES3Properties(new string[]
	{
		"x",
		"y"
	})]
	public class ES3Type_Vector2 : ES3Type
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0001A594 File Offset: 0x00018794
		public ES3Type_Vector2() : base(typeof(Vector2))
		{
			ES3Type_Vector2.Instance = this;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001A5AC File Offset: 0x000187AC
		public override void Write(object obj, ES3Writer writer)
		{
			Vector2 vector = (Vector2)obj;
			writer.WriteProperty("x", vector.x, ES3Type_float.Instance);
			writer.WriteProperty("y", vector.y, ES3Type_float.Instance);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001A5F6 File Offset: 0x000187F6
		public override object Read<T>(ES3Reader reader)
		{
			return new Vector2(reader.ReadProperty<float>(ES3Type_float.Instance), reader.ReadProperty<float>(ES3Type_float.Instance));
		}

		// Token: 0x04000117 RID: 279
		public static ES3Type Instance;
	}
}
