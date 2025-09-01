using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200007A RID: 122
	public class ES3Type_BoneWeightArray : ES3ArrayType
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000F7EF File Offset: 0x0000D9EF
		public ES3Type_BoneWeightArray() : base(typeof(BoneWeight[]), ES3Type_BoneWeight.Instance)
		{
			ES3Type_BoneWeightArray.Instance = this;
		}

		// Token: 0x040000CA RID: 202
		public static ES3Type Instance;
	}
}
