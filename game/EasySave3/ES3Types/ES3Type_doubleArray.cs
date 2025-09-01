using System;

namespace ES3Types
{
	// Token: 0x0200003E RID: 62
	public class ES3Type_doubleArray : ES3ArrayType
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00009E70 File Offset: 0x00008070
		public ES3Type_doubleArray() : base(typeof(double[]), ES3Type_double.Instance)
		{
			ES3Type_doubleArray.Instance = this;
		}

		// Token: 0x04000092 RID: 146
		public static ES3Type Instance;
	}
}
