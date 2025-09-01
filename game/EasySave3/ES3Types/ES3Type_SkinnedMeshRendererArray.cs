using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000B8 RID: 184
	public class ES3Type_SkinnedMeshRendererArray : ES3ArrayType
	{
		// Token: 0x060003DB RID: 987 RVA: 0x00018A24 File Offset: 0x00016C24
		public ES3Type_SkinnedMeshRendererArray() : base(typeof(SkinnedMeshRenderer[]), ES3Type_SkinnedMeshRenderer.Instance)
		{
			ES3Type_SkinnedMeshRendererArray.Instance = this;
		}

		// Token: 0x0400010B RID: 267
		public static ES3Type Instance;
	}
}
