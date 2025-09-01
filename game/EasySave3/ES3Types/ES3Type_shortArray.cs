using System;

namespace ES3Types
{
	// Token: 0x0200004E RID: 78
	public class ES3Type_shortArray : ES3ArrayType
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000A662 File Offset: 0x00008862
		public ES3Type_shortArray() : base(typeof(short[]), ES3Type_short.Instance)
		{
			ES3Type_shortArray.Instance = this;
		}

		// Token: 0x040000A3 RID: 163
		public static ES3Type Instance;
	}
}
