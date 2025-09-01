using System;

namespace ES3Types
{
	// Token: 0x02000056 RID: 86
	public class ES3Type_ulongArray : ES3ArrayType
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000A7CD File Offset: 0x000089CD
		public ES3Type_ulongArray() : base(typeof(ulong[]), ES3Type_ulong.Instance)
		{
			ES3Type_ulongArray.Instance = this;
		}

		// Token: 0x040000AB RID: 171
		public static ES3Type Instance;
	}
}
