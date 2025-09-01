using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000BB RID: 187
	public class ES3Type_SpriteRendererArray : ES3ArrayType
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000193A4 File Offset: 0x000175A4
		public ES3Type_SpriteRendererArray() : base(typeof(SpriteRenderer[]), ES3Type_SpriteRenderer.Instance)
		{
			ES3Type_SpriteRendererArray.Instance = this;
		}

		// Token: 0x0400010E RID: 270
		public static ES3Type Instance;
	}
}
