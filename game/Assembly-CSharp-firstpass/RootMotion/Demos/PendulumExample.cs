using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000144 RID: 324
	public class PendulumExample : MonoBehaviour
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00058BE0 File Offset: 0x00056DE0
		private void Start()
		{
			this.ik = base.GetComponent<FullBodyBipedIK>();
			Quaternion rotation = this.target.rotation;
			this.target.rotation = this.leftHandTarget.rotation;
			this.target.gameObject.AddComponent<FixedJoint>().connectedBody = this.leftHandTarget.GetComponent<Rigidbody>();
			this.target.GetComponent<Rigidbody>().MoveRotation(rotation);
			this.rootRelativeToPelvis = Quaternion.Inverse(this.pelvisTarget.rotation) * base.transform.rotation;
			this.pelvisToRoot = Quaternion.Inverse(this.ik.references.pelvis.rotation) * (base.transform.position - this.ik.references.pelvis.position);
			this.rootTargetPosition = base.transform.position;
			this.rootTargetRotation = base.transform.rotation;
			this.lastWeight = this.weight;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00058CEC File Offset: 0x00056EEC
		private void LateUpdate()
		{
			if (this.weight > 0f)
			{
				this.ik.solver.leftHandEffector.positionWeight = this.weight;
				this.ik.solver.leftHandEffector.rotationWeight = this.weight;
			}
			else
			{
				this.rootTargetPosition = base.transform.position;
				this.rootTargetRotation = base.transform.rotation;
				if (this.lastWeight > 0f)
				{
					this.ik.solver.leftHandEffector.positionWeight = 0f;
					this.ik.solver.leftHandEffector.rotationWeight = 0f;
				}
			}
			this.lastWeight = this.weight;
			if (this.weight <= 0f)
			{
				return;
			}
			base.transform.position = Vector3.Lerp(this.rootTargetPosition, this.pelvisTarget.position + this.pelvisTarget.rotation * this.pelvisToRoot * this.hangingDistanceMlp, this.weight);
			base.transform.rotation = Quaternion.Lerp(this.rootTargetRotation, this.pelvisTarget.rotation * this.rootRelativeToPelvis, this.weight);
			this.ik.solver.leftHandEffector.position = this.leftHandTarget.position;
			this.ik.solver.leftHandEffector.rotation = this.leftHandTarget.rotation;
			Vector3 fromDirection = this.ik.references.pelvis.rotation * this.pelvisDownAxis;
			Quaternion b = Quaternion.FromToRotation(fromDirection, this.rightHandTarget.position - this.headTarget.position);
			this.ik.references.rightUpperArm.rotation = Quaternion.Lerp(Quaternion.identity, b, this.weight) * this.ik.references.rightUpperArm.rotation;
			Quaternion b2 = Quaternion.FromToRotation(fromDirection, this.leftFootTarget.position - this.bodyTarget.position);
			this.ik.references.leftThigh.rotation = Quaternion.Lerp(Quaternion.identity, b2, this.weight) * this.ik.references.leftThigh.rotation;
			Quaternion b3 = Quaternion.FromToRotation(fromDirection, this.rightFootTarget.position - this.bodyTarget.position);
			this.ik.references.rightThigh.rotation = Quaternion.Lerp(Quaternion.identity, b3, this.weight) * this.ik.references.rightThigh.rotation;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00058FBF File Offset: 0x000571BF
		public PendulumExample()
		{
		}

		// Token: 0x04000AB9 RID: 2745
		[Tooltip("The master weight of this script.")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000ABA RID: 2746
		[Tooltip("Multiplier for the distance of the root to the target.")]
		public float hangingDistanceMlp = 1.3f;

		// Token: 0x04000ABB RID: 2747
		[Tooltip("Where does the root of the character land when weight is blended out?")]
		[HideInInspector]
		public Vector3 rootTargetPosition;

		// Token: 0x04000ABC RID: 2748
		[Tooltip("How is the root of the character rotated when weight is blended out?")]
		[HideInInspector]
		public Quaternion rootTargetRotation;

		// Token: 0x04000ABD RID: 2749
		public Transform target;

		// Token: 0x04000ABE RID: 2750
		public Transform leftHandTarget;

		// Token: 0x04000ABF RID: 2751
		public Transform rightHandTarget;

		// Token: 0x04000AC0 RID: 2752
		public Transform leftFootTarget;

		// Token: 0x04000AC1 RID: 2753
		public Transform rightFootTarget;

		// Token: 0x04000AC2 RID: 2754
		public Transform pelvisTarget;

		// Token: 0x04000AC3 RID: 2755
		public Transform bodyTarget;

		// Token: 0x04000AC4 RID: 2756
		public Transform headTarget;

		// Token: 0x04000AC5 RID: 2757
		public Vector3 pelvisDownAxis = Vector3.right;

		// Token: 0x04000AC6 RID: 2758
		private FullBodyBipedIK ik;

		// Token: 0x04000AC7 RID: 2759
		private Quaternion rootRelativeToPelvis;

		// Token: 0x04000AC8 RID: 2760
		private Vector3 pelvisToRoot;

		// Token: 0x04000AC9 RID: 2761
		private float lastWeight;
	}
}
