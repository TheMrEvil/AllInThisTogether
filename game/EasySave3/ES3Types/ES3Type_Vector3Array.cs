using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000C9 RID: 201
	public class ES3Type_Vector3Array : ES3ArrayType
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0001A786 File Offset: 0x00018986
		public ES3Type_Vector3Array() : base(typeof(Vector3[]), ES3Type_Vector3.Instance)
		{
			ES3Type_Vector3Array.Instance = this;
		}

		// Token: 0x0400011C RID: 284
		public static ES3Type Instance;
	}
}
