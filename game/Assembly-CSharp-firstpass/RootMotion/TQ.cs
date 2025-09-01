using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000A9 RID: 169
	public class TQ
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00034CA6 File Offset: 0x00032EA6
		public TQ(Vector3 translation, Quaternion rotation)
		{
			this.t = translation;
			this.q = rotation;
		}

		// Token: 0x0400061B RID: 1563
		public Vector3 t;

		// Token: 0x0400061C RID: 1564
		public Quaternion q;
	}
}
