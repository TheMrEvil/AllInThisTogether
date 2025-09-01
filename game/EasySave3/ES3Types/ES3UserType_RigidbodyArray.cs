using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000072 RID: 114
	public class ES3UserType_RigidbodyArray : ES3ArrayType
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public ES3UserType_RigidbodyArray() : base(typeof(Rigidbody[]), ES3Type_Rigidbody.Instance)
		{
			ES3UserType_RigidbodyArray.Instance = this;
		}

		// Token: 0x040000C1 RID: 193
		public static ES3Type Instance;
	}
}
