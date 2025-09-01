using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200009F RID: 159
	public class ES3Type_MaterialArray : ES3ArrayType
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00013D90 File Offset: 0x00011F90
		public ES3Type_MaterialArray() : base(typeof(Material[]), ES3Type_Material.Instance)
		{
			ES3Type_MaterialArray.Instance = this;
		}

		// Token: 0x040000F2 RID: 242
		public static ES3Type Instance;
	}
}
