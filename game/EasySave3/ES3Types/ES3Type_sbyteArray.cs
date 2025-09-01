using System;

namespace ES3Types
{
	// Token: 0x0200004C RID: 76
	public class ES3Type_sbyteArray : ES3ArrayType
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000A601 File Offset: 0x00008801
		public ES3Type_sbyteArray() : base(typeof(sbyte[]), ES3Type_sbyte.Instance)
		{
			ES3Type_sbyteArray.Instance = this;
		}

		// Token: 0x040000A1 RID: 161
		public static ES3Type Instance;
	}
}
