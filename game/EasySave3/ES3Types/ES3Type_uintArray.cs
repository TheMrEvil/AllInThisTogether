using System;

namespace ES3Types
{
	// Token: 0x02000052 RID: 82
	public class ES3Type_uintArray : ES3ArrayType
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000A715 File Offset: 0x00008915
		public ES3Type_uintArray() : base(typeof(uint[]), ES3Type_uint.Instance)
		{
			ES3Type_uintArray.Instance = this;
		}

		// Token: 0x040000A7 RID: 167
		public static ES3Type Instance;
	}
}
