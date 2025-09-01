using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000C0 RID: 192
	public class ES3Type_Texture2DArray : ES3ArrayType
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x000199B9 File Offset: 0x00017BB9
		public ES3Type_Texture2DArray() : base(typeof(Texture2D[]), ES3Type_Texture2D.Instance)
		{
			ES3Type_Texture2DArray.Instance = this;
		}

		// Token: 0x04000113 RID: 275
		public static ES3Type Instance;
	}
}
