using System;
using UnityEngine;

namespace ECE
{
	// Token: 0x0200006B RID: 107
	public class SphereColliderData : EasyColliderData
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00022276 File Offset: 0x00020476
		public void Clone(SphereColliderData data)
		{
			base.Clone(data);
			this.Radius = data.Radius;
			this.Center = data.Center;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00022297 File Offset: 0x00020497
		public SphereColliderData()
		{
		}

		// Token: 0x04000419 RID: 1049
		public float Radius;

		// Token: 0x0400041A RID: 1050
		public Vector3 Center;
	}
}
