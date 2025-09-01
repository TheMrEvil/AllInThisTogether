using System;

namespace ES3Types
{
	// Token: 0x02000054 RID: 84
	public class ES3Type_UIntPtrArray : ES3ArrayType
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A76C File Offset: 0x0000896C
		public ES3Type_UIntPtrArray() : base(typeof(UIntPtr[]), ES3Type_UIntPtr.Instance)
		{
			ES3Type_UIntPtrArray.Instance = this;
		}

		// Token: 0x040000A9 RID: 169
		public static ES3Type Instance;
	}
}
