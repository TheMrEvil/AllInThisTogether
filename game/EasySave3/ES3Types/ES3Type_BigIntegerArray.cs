using System;
using System.Numerics;

namespace ES3Types
{
	// Token: 0x02000030 RID: 48
	public class ES3Type_BigIntegerArray : ES3ArrayType
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00009A56 File Offset: 0x00007C56
		public ES3Type_BigIntegerArray() : base(typeof(BigInteger[]), ES3Type_BigInteger.Instance)
		{
			ES3Type_BigIntegerArray.Instance = this;
		}

		// Token: 0x04000081 RID: 129
		public static ES3Type Instance;
	}
}
