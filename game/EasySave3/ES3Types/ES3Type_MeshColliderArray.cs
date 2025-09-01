using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000067 RID: 103
	public class ES3Type_MeshColliderArray : ES3ArrayType
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000C64C File Offset: 0x0000A84C
		public ES3Type_MeshColliderArray() : base(typeof(MeshCollider[]), ES3Type_MeshCollider.Instance)
		{
			ES3Type_MeshColliderArray.Instance = this;
		}

		// Token: 0x040000B6 RID: 182
		public static ES3Type Instance;
	}
}
