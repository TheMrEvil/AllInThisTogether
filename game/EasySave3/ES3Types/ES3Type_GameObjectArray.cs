using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200008E RID: 142
	public class ES3Type_GameObjectArray : ES3ArrayType
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0001161B File Offset: 0x0000F81B
		public ES3Type_GameObjectArray() : base(typeof(GameObject[]), ES3Type_GameObject.Instance)
		{
			ES3Type_GameObjectArray.Instance = this;
		}

		// Token: 0x040000E1 RID: 225
		public static ES3Type Instance;
	}
}
