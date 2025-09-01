using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200007B RID: 123
	[Preserve]
	[ES3Properties(new string[]
	{
		"center",
		"size"
	})]
	public class ES3Type_Bounds : ES3Type
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public ES3Type_Bounds() : base(typeof(Bounds))
		{
			ES3Type_Bounds.Instance = this;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F824 File Offset: 0x0000DA24
		public override void Write(object obj, ES3Writer writer)
		{
			Bounds bounds = (Bounds)obj;
			writer.WriteProperty("center", bounds.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("size", bounds.size, ES3Type_Vector3.Instance);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F870 File Offset: 0x0000DA70
		public override object Read<T>(ES3Reader reader)
		{
			return new Bounds(reader.ReadProperty<Vector3>(ES3Type_Vector3.Instance), reader.ReadProperty<Vector3>(ES3Type_Vector3.Instance));
		}

		// Token: 0x040000CB RID: 203
		public static ES3Type Instance;
	}
}
