using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200006B RID: 107
	public class ES3Type_MeshRendererArray : ES3ArrayType
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public ES3Type_MeshRendererArray() : base(typeof(MeshRenderer[]), ES3Type_MeshRenderer.Instance)
		{
			ES3Type_MeshRendererArray.Instance = this;
		}

		// Token: 0x040000BA RID: 186
		public static ES3Type Instance;
	}
}
