using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000A9 RID: 169
	public class ES3Type_PhysicsMaterial2DArray : ES3ArrayType
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00015B24 File Offset: 0x00013D24
		public ES3Type_PhysicsMaterial2DArray() : base(typeof(PhysicsMaterial2D[]), ES3Type_PhysicsMaterial2D.Instance)
		{
			ES3Type_PhysicsMaterial2DArray.Instance = this;
		}

		// Token: 0x040000FC RID: 252
		public static ES3Type Instance;
	}
}
