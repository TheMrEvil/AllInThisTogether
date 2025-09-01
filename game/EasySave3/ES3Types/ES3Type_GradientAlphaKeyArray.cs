using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000091 RID: 145
	public class ES3Type_GradientAlphaKeyArray : ES3ArrayType
	{
		// Token: 0x0600036B RID: 875 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public ES3Type_GradientAlphaKeyArray() : base(typeof(GradientAlphaKey[]), ES3Type_GradientAlphaKey.Instance)
		{
			ES3Type_GradientAlphaKeyArray.Instance = this;
		}

		// Token: 0x040000E4 RID: 228
		public static ES3Type Instance;
	}
}
