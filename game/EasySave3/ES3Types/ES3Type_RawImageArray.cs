using System;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x02000070 RID: 112
	public class ES3Type_RawImageArray : ES3ArrayType
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public ES3Type_RawImageArray() : base(typeof(RawImage[]), ES3Type_RawImage.Instance)
		{
			ES3Type_RawImageArray.Instance = this;
		}

		// Token: 0x040000BF RID: 191
		public static ES3Type Instance;
	}
}
