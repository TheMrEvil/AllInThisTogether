using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200007E RID: 126
	public class ES3Type_BurstArray : ES3ArrayType
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000FB3E File Offset: 0x0000DD3E
		public ES3Type_BurstArray() : base(typeof(ParticleSystem.Burst[]), ES3Type_Burst.Instance)
		{
			ES3Type_BurstArray.Instance = this;
		}

		// Token: 0x040000CE RID: 206
		public static ES3Type Instance;
	}
}
