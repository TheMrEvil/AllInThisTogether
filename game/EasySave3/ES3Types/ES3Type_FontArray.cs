using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200008B RID: 139
	public class ES3Type_FontArray : ES3ArrayType
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00010A17 File Offset: 0x0000EC17
		public ES3Type_FontArray() : base(typeof(Font[]), ES3Type_Font.Instance)
		{
			ES3Type_FontArray.Instance = this;
		}

		// Token: 0x040000DB RID: 219
		public static ES3Type Instance;
	}
}
