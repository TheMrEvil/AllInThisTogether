using System;

namespace ES3Types
{
	// Token: 0x02000046 RID: 70
	public class ES3Type_intArray : ES3ArrayType
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public ES3Type_intArray() : base(typeof(int[]), ES3Type_int.Instance)
		{
			ES3Type_intArray.Instance = this;
		}

		// Token: 0x0400009B RID: 155
		public static ES3Type Instance;
	}
}
