using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000C5 RID: 197
	public class ES3Type_Vector2Array : ES3ArrayType
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0001A618 File Offset: 0x00018818
		public ES3Type_Vector2Array() : base(typeof(Vector2[]), ES3Type_Vector2.Instance)
		{
			ES3Type_Vector2Array.Instance = this;
		}

		// Token: 0x04000118 RID: 280
		public static ES3Type Instance;
	}
}
