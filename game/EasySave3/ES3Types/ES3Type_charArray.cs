using System;

namespace ES3Types
{
	// Token: 0x02000038 RID: 56
	public class ES3Type_charArray : ES3ArrayType
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00009D2A File Offset: 0x00007F2A
		public ES3Type_charArray() : base(typeof(char[]), ES3Type_char.Instance)
		{
			ES3Type_charArray.Instance = this;
		}

		// Token: 0x0400008C RID: 140
		public static ES3Type Instance;
	}
}
