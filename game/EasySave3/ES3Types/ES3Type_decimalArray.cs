using System;

namespace ES3Types
{
	// Token: 0x0200003C RID: 60
	public class ES3Type_decimalArray : ES3ArrayType
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00009E0F File Offset: 0x0000800F
		public ES3Type_decimalArray() : base(typeof(decimal[]), ES3Type_decimal.Instance)
		{
			ES3Type_decimalArray.Instance = this;
		}

		// Token: 0x04000090 RID: 144
		public static ES3Type Instance;
	}
}
