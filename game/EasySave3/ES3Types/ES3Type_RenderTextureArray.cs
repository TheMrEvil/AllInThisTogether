using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000AF RID: 175
	public class ES3Type_RenderTextureArray : ES3ArrayType
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00016AF6 File Offset: 0x00014CF6
		public ES3Type_RenderTextureArray() : base(typeof(RenderTexture[]), ES3Type_RenderTexture.Instance)
		{
			ES3Type_RenderTextureArray.Instance = this;
		}

		// Token: 0x04000102 RID: 258
		public static ES3Type Instance;
	}
}
