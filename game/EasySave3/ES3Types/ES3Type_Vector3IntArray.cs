using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000CB RID: 203
	public class ES3Type_Vector3IntArray : ES3ArrayType
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0001A851 File Offset: 0x00018A51
		public ES3Type_Vector3IntArray() : base(typeof(Vector3Int[]), ES3Type_Vector3Int.Instance)
		{
			ES3Type_Vector3IntArray.Instance = this;
		}

		// Token: 0x0400011E RID: 286
		public static ES3Type Instance;
	}
}
