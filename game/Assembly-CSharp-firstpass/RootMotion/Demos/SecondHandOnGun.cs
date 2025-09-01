using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000126 RID: 294
	public class SecondHandOnGun : MonoBehaviour
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x00054EE8 File Offset: 0x000530E8
		private void Start()
		{
			this.aim.enabled = false;
			this.leftArmIK.enabled = false;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00054F04 File Offset: 0x00053104
		private void LateUpdate()
		{
			this.leftHandPosRelToRight = this.rightHand.InverseTransformPoint(this.leftHand.position);
			this.leftHandRotRelToRight = Quaternion.Inverse(this.rightHand.rotation) * this.leftHand.rotation;
			this.aim.solver.Update();
			this.leftArmIK.solver.IKPosition = this.rightHand.TransformPoint(this.leftHandPosRelToRight + this.leftHandPositionOffset);
			this.leftArmIK.solver.IKRotation = this.rightHand.rotation * Quaternion.Euler(this.leftHandRotationOffset) * this.leftHandRotRelToRight;
			this.leftArmIK.solver.Update();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00054FD5 File Offset: 0x000531D5
		public SecondHandOnGun()
		{
		}

		// Token: 0x040009D7 RID: 2519
		public AimIK aim;

		// Token: 0x040009D8 RID: 2520
		public LimbIK leftArmIK;

		// Token: 0x040009D9 RID: 2521
		public Transform leftHand;

		// Token: 0x040009DA RID: 2522
		public Transform rightHand;

		// Token: 0x040009DB RID: 2523
		public Vector3 leftHandPositionOffset;

		// Token: 0x040009DC RID: 2524
		public Vector3 leftHandRotationOffset;

		// Token: 0x040009DD RID: 2525
		private Vector3 leftHandPosRelToRight;

		// Token: 0x040009DE RID: 2526
		private Quaternion leftHandRotRelToRight;
	}
}
