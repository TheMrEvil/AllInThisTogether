using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000BE RID: 190
	public class ES3Type_TextureArray : ES3ArrayType
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00019650 File Offset: 0x00017850
		public ES3Type_TextureArray() : base(typeof(Texture[]), ES3Type_Texture.Instance)
		{
			ES3Type_TextureArray.Instance = this;
		}

		// Token: 0x04000111 RID: 273
		public static ES3Type Instance;
	}
}
