using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200006E RID: 110
	public class ES3Type_PolygonCollider2DArray : ES3ArrayType
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public ES3Type_PolygonCollider2DArray() : base(typeof(PolygonCollider2D[]), ES3Type_PolygonCollider2D.Instance)
		{
			ES3Type_PolygonCollider2DArray.Instance = this;
		}

		// Token: 0x040000BD RID: 189
		public static ES3Type Instance;
	}
}
