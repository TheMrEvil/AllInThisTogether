using System;

namespace ES3Types
{
	// Token: 0x02000058 RID: 88
	public class ES3Type_ushortArray : ES3ArrayType
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000A82E File Offset: 0x00008A2E
		public ES3Type_ushortArray() : base(typeof(ushort[]), ES3Type_ushort.Instance)
		{
			ES3Type_ushortArray.Instance = this;
		}

		// Token: 0x040000AD RID: 173
		public static ES3Type Instance;
	}
}
