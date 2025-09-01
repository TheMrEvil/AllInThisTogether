using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000147 RID: 327
	public class PickUpSphere : PickUp2Handed
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x000594D8 File Offset: 0x000576D8
		protected override void RotatePivot()
		{
			Vector3 b = Vector3.Lerp(this.interactionSystem.ik.solver.leftHandEffector.bone.position, this.interactionSystem.ik.solver.rightHandEffector.bone.position, 0.5f);
			Vector3 forward = this.obj.transform.position - b;
			this.pivot.rotation = Quaternion.LookRotation(forward);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00059556 File Offset: 0x00057756
		public PickUpSphere()
		{
		}
	}
}
