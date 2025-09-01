using System;

namespace ES3Types
{
	// Token: 0x0200004A RID: 74
	public class ES3Type_longArray : ES3ArrayType
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public ES3Type_longArray() : base(typeof(long[]), ES3Type_long.Instance)
		{
			ES3Type_longArray.Instance = this;
		}

		// Token: 0x0400009F RID: 159
		public static ES3Type Instance;
	}
}
