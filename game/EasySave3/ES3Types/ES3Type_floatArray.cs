using System;

namespace ES3Types
{
	// Token: 0x02000044 RID: 68
	public class ES3Type_floatArray : ES3ArrayType
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000A473 File Offset: 0x00008673
		public ES3Type_floatArray() : base(typeof(float[]), ES3Type_float.Instance)
		{
			ES3Type_floatArray.Instance = this;
		}

		// Token: 0x04000099 RID: 153
		public static ES3Type Instance;
	}
}
