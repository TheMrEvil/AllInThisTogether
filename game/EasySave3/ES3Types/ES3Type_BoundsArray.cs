using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200007C RID: 124
	public class ES3Type_BoundsArray : ES3ArrayType
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000F892 File Offset: 0x0000DA92
		public ES3Type_BoundsArray() : base(typeof(Bounds[]), ES3Type_Bounds.Instance)
		{
			ES3Type_BoundsArray.Instance = this;
		}

		// Token: 0x040000CC RID: 204
		public static ES3Type Instance;
	}
}
