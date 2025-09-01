using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000A1 RID: 161
	public class ES3Type_Matrix4x4Array : ES3ArrayType
	{
		// Token: 0x06000396 RID: 918 RVA: 0x00013EB7 File Offset: 0x000120B7
		public ES3Type_Matrix4x4Array() : base(typeof(Matrix4x4[]), ES3Type_Matrix4x4.Instance)
		{
			ES3Type_Matrix4x4Array.Instance = this;
		}

		// Token: 0x040000F4 RID: 244
		public static ES3Type Instance;
	}
}
