using System;

namespace ES3Types
{
	// Token: 0x02000034 RID: 52
	public class ES3Type_boolArray : ES3ArrayType
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00009C46 File Offset: 0x00007E46
		public ES3Type_boolArray() : base(typeof(bool[]), ES3Type_bool.Instance)
		{
			ES3Type_boolArray.Instance = this;
		}

		// Token: 0x04000088 RID: 136
		public static ES3Type Instance;
	}
}
