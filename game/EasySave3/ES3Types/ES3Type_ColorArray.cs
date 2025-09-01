using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000081 RID: 129
	public class ES3Type_ColorArray : ES3ArrayType
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00010228 File Offset: 0x0000E428
		public ES3Type_ColorArray() : base(typeof(Color[]), ES3Type_Color.Instance)
		{
			ES3Type_ColorArray.Instance = this;
		}

		// Token: 0x040000D1 RID: 209
		public static ES3Type Instance;
	}
}
