using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000140 RID: 320
	public class KissingRig : MonoBehaviour
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x00058740 File Offset: 0x00056940
		private void Start()
		{
			this.partner1.Initiate();
			this.partner2.Initiate();
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00058758 File Offset: 0x00056958
		private void LateUpdate()
		{
			for (int i = 0; i < this.iterations; i++)
			{
				this.partner1.Update(this.weight);
				this.partner2.Update(this.weight);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00058798 File Offset: 0x00056998
		public KissingRig()
		{
		}

		// Token: 0x04000AA5 RID: 2725
		public KissingRig.Partner partner1;

		// Token: 0x04000AA6 RID: 2726
		public KissingRig.Partner partner2;

		// Token: 0x04000AA7 RID: 2727
		public float weight;

		// Token: 0x04000AA8 RID: 2728
		public int iterations = 3;

		// Token: 0x02000230 RID: 560
		[Serializable]
		public class Partner
		{
			// Token: 0x060011A4 RID: 4516 RVA: 0x0006D7F5 File Offset: 0x0006B9F5
			public void Initiate()
			{
				this.ik.enabled = false;
			}

			// Token: 0x060011A5 RID: 4517 RVA: 0x0006D804 File Offset: 0x0006BA04
			public void Update(float weight)
			{
				this.ik.solver.leftShoulderEffector.positionWeight = weight;
				this.ik.solver.rightShoulderEffector.positionWeight = weight;
				this.ik.solver.leftHandEffector.positionWeight = weight;
				this.ik.solver.rightHandEffector.positionWeight = weight;
				this.ik.solver.leftHandEffector.rotationWeight = weight;
				this.ik.solver.rightHandEffector.rotationWeight = weight;
				this.ik.solver.bodyEffector.positionWeight = weight;
				this.InverseTransformEffector(FullBodyBipedEffector.LeftShoulder, this.mouth, this.mouthTarget.position, weight);
				this.InverseTransformEffector(FullBodyBipedEffector.RightShoulder, this.mouth, this.mouthTarget.position, weight);
				this.InverseTransformEffector(FullBodyBipedEffector.Body, this.mouth, this.mouthTarget.position, weight);
				this.ik.solver.bodyEffector.position = Vector3.Lerp(new Vector3(this.ik.solver.bodyEffector.position.x, this.ik.solver.bodyEffector.bone.position.y, this.ik.solver.bodyEffector.position.z), this.ik.solver.bodyEffector.position, this.bodyWeightVertical * weight);
				this.ik.solver.bodyEffector.position = Vector3.Lerp(new Vector3(this.ik.solver.bodyEffector.bone.position.x, this.ik.solver.bodyEffector.position.y, this.ik.solver.bodyEffector.bone.position.z), this.ik.solver.bodyEffector.position, this.bodyWeightHorizontal * weight);
				this.ik.solver.leftHandEffector.position = this.touchTargetLeftHand.position;
				this.ik.solver.rightHandEffector.position = this.touchTargetRightHand.position;
				this.ik.solver.leftHandEffector.rotation = this.touchTargetLeftHand.rotation;
				this.ik.solver.rightHandEffector.rotation = this.touchTargetRightHand.rotation;
				this.neckRotation = this.neck.rotation;
				this.ik.solver.Update();
				this.neck.rotation = Quaternion.Slerp(this.neck.rotation, this.neckRotation, this.neckRotationWeight * weight);
				this.ik.references.head.localRotation = Quaternion.AngleAxis(this.headTiltAngle * weight, this.headTiltAxis) * this.ik.references.head.localRotation;
			}

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0006DB25 File Offset: 0x0006BD25
			private Transform neck
			{
				get
				{
					return this.ik.solver.spineMapping.spineBones[this.ik.solver.spineMapping.spineBones.Length - 1];
				}
			}

			// Token: 0x060011A7 RID: 4519 RVA: 0x0006DB58 File Offset: 0x0006BD58
			private void InverseTransformEffector(FullBodyBipedEffector effector, Transform target, Vector3 targetPosition, float weight)
			{
				Vector3 b = this.ik.solver.GetEffector(effector).bone.position - target.position;
				this.ik.solver.GetEffector(effector).position = Vector3.Lerp(this.ik.solver.GetEffector(effector).bone.position, targetPosition + b, weight);
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x0006DBCB File Offset: 0x0006BDCB
			public Partner()
			{
			}

			// Token: 0x04001085 RID: 4229
			public FullBodyBipedIK ik;

			// Token: 0x04001086 RID: 4230
			public Transform mouth;

			// Token: 0x04001087 RID: 4231
			public Transform mouthTarget;

			// Token: 0x04001088 RID: 4232
			public Transform touchTargetLeftHand;

			// Token: 0x04001089 RID: 4233
			public Transform touchTargetRightHand;

			// Token: 0x0400108A RID: 4234
			public float bodyWeightHorizontal = 0.4f;

			// Token: 0x0400108B RID: 4235
			public float bodyWeightVertical = 1f;

			// Token: 0x0400108C RID: 4236
			public float neckRotationWeight = 0.3f;

			// Token: 0x0400108D RID: 4237
			public float headTiltAngle = 10f;

			// Token: 0x0400108E RID: 4238
			public Vector3 headTiltAxis;

			// Token: 0x0400108F RID: 4239
			private Quaternion neckRotation;
		}
	}
}
