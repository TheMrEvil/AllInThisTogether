using System;
using UnityEngine;

namespace ECE
{
	// Token: 0x0200007F RID: 127
	public struct EasyColliderProperties
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0002234D File Offset: 0x0002054D
		public EasyColliderProperties(bool isTrigger, int layer, PhysicMaterial physicMaterial, GameObject attachTo, COLLIDER_ORIENTATION orientation = COLLIDER_ORIENTATION.NORMAL)
		{
			this.IsTrigger = isTrigger;
			this.Layer = layer;
			this.PhysicMaterial = physicMaterial;
			this.AttachTo = attachTo;
			this.Orientation = orientation;
		}

		// Token: 0x04000467 RID: 1127
		public bool IsTrigger;

		// Token: 0x04000468 RID: 1128
		public int Layer;

		// Token: 0x04000469 RID: 1129
		public PhysicMaterial PhysicMaterial;

		// Token: 0x0400046A RID: 1130
		public COLLIDER_ORIENTATION Orientation;

		// Token: 0x0400046B RID: 1131
		public GameObject AttachTo;
	}
}
