using System;

namespace ES3Types
{
	// Token: 0x02000095 RID: 149
	public class ES3Type_GuidArray : ES3ArrayType
	{
		// Token: 0x06000373 RID: 883 RVA: 0x000118CC File Offset: 0x0000FACC
		public ES3Type_GuidArray() : base(typeof(Guid[]), ES3Type_Guid.Instance)
		{
			ES3Type_GuidArray.Instance = this;
		}

		// Token: 0x040000E8 RID: 232
		public static ES3Type Instance;
	}
}
