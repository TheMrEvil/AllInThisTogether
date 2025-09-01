using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000CD RID: 205
	public class ES3Type_Vector4Array : ES3ArrayType
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0001A999 File Offset: 0x00018B99
		public ES3Type_Vector4Array() : base(typeof(Vector4[]), ES3Type_Vector4.Instance)
		{
			ES3Type_Vector4Array.Instance = this;
		}

		// Token: 0x04000120 RID: 288
		public static ES3Type Instance;
	}
}
