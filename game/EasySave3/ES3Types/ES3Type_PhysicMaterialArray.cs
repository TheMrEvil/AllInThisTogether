using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000A7 RID: 167
	public class ES3Type_PhysicMaterialArray : ES3ArrayType
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000159E0 File Offset: 0x00013BE0
		public ES3Type_PhysicMaterialArray() : base(typeof(PhysicMaterial[]), ES3Type_PhysicMaterial.Instance)
		{
			ES3Type_PhysicMaterialArray.Instance = this;
		}

		// Token: 0x040000FA RID: 250
		public static ES3Type Instance;
	}
}
