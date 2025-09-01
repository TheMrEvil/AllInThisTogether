using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000083 RID: 131
	public class ES3Type_Color32Array : ES3ArrayType
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00010355 File Offset: 0x0000E555
		public ES3Type_Color32Array() : base(typeof(Color32[]), ES3Type_Color32.Instance)
		{
			ES3Type_Color32Array.Instance = this;
		}

		// Token: 0x040000D3 RID: 211
		public static ES3Type Instance;
	}
}
