using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000AB RID: 171
	public class ES3Type_QuaternionArray : ES3ArrayType
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00015C14 File Offset: 0x00013E14
		public ES3Type_QuaternionArray() : base(typeof(Quaternion[]), ES3Type_Quaternion.Instance)
		{
			ES3Type_QuaternionArray.Instance = this;
		}

		// Token: 0x040000FE RID: 254
		public static ES3Type Instance;
	}
}
