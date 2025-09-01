using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013C RID: 316
	public class HoldingHands : MonoBehaviour
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x000581D8 File Offset: 0x000563D8
		private void Start()
		{
			this.rightHandRotation = Quaternion.Inverse(this.rightHandChar.solver.rightHandEffector.bone.rotation) * base.transform.rotation;
			this.leftHandRotation = Quaternion.Inverse(this.leftHandChar.solver.leftHandEffector.bone.rotation) * base.transform.rotation;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00058250 File Offset: 0x00056450
		private void LateUpdate()
		{
			Vector3 b = Vector3.Lerp(this.rightHandChar.solver.rightHandEffector.bone.position, this.leftHandChar.solver.leftHandEffector.bone.position, this.crossFade);
			base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.speed);
			base.transform.rotation = Quaternion.Slerp(this.rightHandChar.solver.rightHandEffector.bone.rotation * this.rightHandRotation, this.leftHandChar.solver.leftHandEffector.bone.rotation * this.leftHandRotation, this.crossFade);
			this.rightHandChar.solver.rightHandEffector.position = this.rightHandTarget.position;
			this.rightHandChar.solver.rightHandEffector.rotation = this.rightHandTarget.rotation;
			this.leftHandChar.solver.leftHandEffector.position = this.leftHandTarget.position;
			this.leftHandChar.solver.leftHandEffector.rotation = this.leftHandTarget.rotation;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000583A5 File Offset: 0x000565A5
		public HoldingHands()
		{
		}

		// Token: 0x04000A8E RID: 2702
		public FullBodyBipedIK rightHandChar;

		// Token: 0x04000A8F RID: 2703
		public FullBodyBipedIK leftHandChar;

		// Token: 0x04000A90 RID: 2704
		public Transform rightHandTarget;

		// Token: 0x04000A91 RID: 2705
		public Transform leftHandTarget;

		// Token: 0x04000A92 RID: 2706
		public float crossFade;

		// Token: 0x04000A93 RID: 2707
		public float speed = 10f;

		// Token: 0x04000A94 RID: 2708
		private Quaternion rightHandRotation;

		// Token: 0x04000A95 RID: 2709
		private Quaternion leftHandRotation;
	}
}
