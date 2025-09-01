using System;
using System.Collections;

namespace ES3Types
{
	// Token: 0x02000027 RID: 39
	public class ES3UserType_ArrayListArray : ES3ArrayType
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public ES3UserType_ArrayListArray() : base(typeof(ArrayList[]), ES3Type_ArrayList.Instance)
		{
			ES3UserType_ArrayListArray.Instance = this;
		}

		// Token: 0x0400006A RID: 106
		public static ES3Type Instance;
	}
}
