using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000093 RID: 147
	public class ES3Type_GradientColorKeyArray : ES3ArrayType
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0001184C File Offset: 0x0000FA4C
		public ES3Type_GradientColorKeyArray() : base(typeof(GradientColorKey[]), ES3Type_GradientColorKey.Instance)
		{
			ES3Type_GradientColorKeyArray.Instance = this;
		}

		// Token: 0x040000E6 RID: 230
		public static ES3Type Instance;
	}
}
