using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000089 RID: 137
	public class ES3Type_FlareArray : ES3ArrayType
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00010935 File Offset: 0x0000EB35
		public ES3Type_FlareArray() : base(typeof(Flare[]), ES3Type_Flare.Instance)
		{
			ES3Type_FlareArray.Instance = this;
		}

		// Token: 0x040000D9 RID: 217
		public static ES3Type Instance;
	}
}
