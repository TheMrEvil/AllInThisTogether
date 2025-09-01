using System;

namespace ES3Types
{
	// Token: 0x02000050 RID: 80
	public class ES3Type_StringArray : ES3ArrayType
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public ES3Type_StringArray() : base(typeof(string[]), ES3Type_string.Instance)
		{
			ES3Type_StringArray.Instance = this;
		}

		// Token: 0x040000A5 RID: 165
		public static ES3Type Instance;
	}
}
