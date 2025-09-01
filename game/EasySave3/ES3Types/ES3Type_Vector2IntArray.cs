using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000C7 RID: 199
	public class ES3Type_Vector2IntArray : ES3ArrayType
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0001A6BE File Offset: 0x000188BE
		public ES3Type_Vector2IntArray() : base(typeof(Vector2Int[]), ES3Type_Vector2Int.Instance)
		{
			ES3Type_Vector2IntArray.Instance = this;
		}

		// Token: 0x0400011A RID: 282
		public static ES3Type Instance;
	}
}
