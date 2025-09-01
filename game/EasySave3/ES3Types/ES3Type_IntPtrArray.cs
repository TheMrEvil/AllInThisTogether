using System;

namespace ES3Types
{
	// Token: 0x02000048 RID: 72
	public class ES3Type_IntPtrArray : ES3ArrayType
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000A53F File Offset: 0x0000873F
		public ES3Type_IntPtrArray() : base(typeof(IntPtr[]), ES3Type_IntPtr.Instance)
		{
			ES3Type_IntPtrArray.Instance = this;
		}

		// Token: 0x0400009D RID: 157
		public static ES3Type Instance;
	}
}
