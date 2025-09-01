using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A0 RID: 160
	[Preserve]
	[ES3Properties(new string[]
	{
		"col0",
		"col1",
		"col2",
		"col3"
	})]
	public class ES3Type_Matrix4x4 : ES3Type
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00013DAD File Offset: 0x00011FAD
		public ES3Type_Matrix4x4() : base(typeof(Matrix4x4))
		{
			ES3Type_Matrix4x4.Instance = this;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013DC8 File Offset: 0x00011FC8
		public override void Write(object obj, ES3Writer writer)
		{
			Matrix4x4 matrix4x = (Matrix4x4)obj;
			writer.WriteProperty("col0", matrix4x.GetColumn(0), ES3Type_Vector4.Instance);
			writer.WriteProperty("col1", matrix4x.GetColumn(1), ES3Type_Vector4.Instance);
			writer.WriteProperty("col2", matrix4x.GetColumn(2), ES3Type_Vector4.Instance);
			writer.WriteProperty("col3", matrix4x.GetColumn(3), ES3Type_Vector4.Instance);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013E50 File Offset: 0x00012050
		public override object Read<T>(ES3Reader reader)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x.SetColumn(0, reader.ReadProperty<Vector4>(ES3Type_Vector4.Instance));
			matrix4x.SetColumn(1, reader.ReadProperty<Vector4>(ES3Type_Vector4.Instance));
			matrix4x.SetColumn(2, reader.ReadProperty<Vector4>(ES3Type_Vector4.Instance));
			matrix4x.SetColumn(3, reader.ReadProperty<Vector4>(ES3Type_Vector4.Instance));
			return matrix4x;
		}

		// Token: 0x040000F3 RID: 243
		public static ES3Type Instance;
	}
}
