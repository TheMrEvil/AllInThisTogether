using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000B3 RID: 179
	public class ES3Type_ShaderArray : ES3ArrayType
	{
		// Token: 0x060003CB RID: 971 RVA: 0x000172F8 File Offset: 0x000154F8
		public ES3Type_ShaderArray() : base(typeof(Shader[]), ES3Type_Shader.Instance)
		{
			ES3Type_ShaderArray.Instance = this;
		}

		// Token: 0x04000106 RID: 262
		public static ES3Type Instance;
	}
}
