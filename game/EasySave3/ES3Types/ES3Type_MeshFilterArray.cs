using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000069 RID: 105
	public class ES3Type_MeshFilterArray : ES3ArrayType
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000C72C File Offset: 0x0000A92C
		public ES3Type_MeshFilterArray() : base(typeof(MeshFilter[]), ES3Type_MeshFilter.Instance)
		{
			ES3Type_MeshFilterArray.Instance = this;
		}

		// Token: 0x040000B8 RID: 184
		public static ES3Type Instance;
	}
}
