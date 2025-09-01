using System;

namespace ES3Types
{
	// Token: 0x0200003A RID: 58
	public class ES3Type_DateTimeArray : ES3ArrayType
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00009DAE File Offset: 0x00007FAE
		public ES3Type_DateTimeArray() : base(typeof(DateTime[]), ES3Type_DateTime.Instance)
		{
			ES3Type_DateTimeArray.Instance = this;
		}

		// Token: 0x0400008E RID: 142
		public static ES3Type Instance;
	}
}
