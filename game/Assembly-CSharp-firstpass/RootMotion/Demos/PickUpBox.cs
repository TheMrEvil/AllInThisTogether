using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000146 RID: 326
	public class PickUpBox : PickUp2Handed
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x0005943C File Offset: 0x0005763C
		protected override void RotatePivot()
		{
			Vector3 normalized = (this.pivot.position - this.interactionSystem.transform.position).normalized;
			normalized.y = 0f;
			Vector3 axis = QuaTools.GetAxis(this.obj.transform.InverseTransformDirection(normalized));
			Vector3 axis2 = QuaTools.GetAxis(this.obj.transform.InverseTransformDirection(this.interactionSystem.transform.up));
			this.pivot.localRotation = Quaternion.LookRotation(axis, axis2);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000594CD File Offset: 0x000576CD
		public PickUpBox()
		{
		}
	}
}
