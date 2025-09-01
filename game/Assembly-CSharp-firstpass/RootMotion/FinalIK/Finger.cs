using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public class Finger
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0003B1D7 File Offset: 0x000393D7
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0003B1DF File Offset: 0x000393DF
		public bool initiated
		{
			[CompilerGenerated]
			get
			{
				return this.<initiated>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<initiated>k__BackingField = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0003B1E8 File Offset: 0x000393E8
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0003B1F5 File Offset: 0x000393F5
		public Vector3 IKPosition
		{
			get
			{
				return this.solver.IKPosition;
			}
			set
			{
				this.solver.IKPosition = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0003B203 File Offset: 0x00039403
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x0003B210 File Offset: 0x00039410
		public Quaternion IKRotation
		{
			get
			{
				return this.solver.IKRotation;
			}
			set
			{
				this.solver.IKRotation = value;
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0003B21E File Offset: 0x0003941E
		public bool IsValid(ref string errorMessage)
		{
			if (this.bone1 == null || this.bone2 == null || this.tip == null)
			{
				errorMessage = "One of the bones in the Finger Rig is null, can not initiate solvers.";
				return false;
			}
			return true;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0003B254 File Offset: 0x00039454
		public void Initiate(Transform hand, int index)
		{
			this.initiated = false;
			string empty = string.Empty;
			if (!this.IsValid(ref empty))
			{
				Warning.Log(empty, hand, false);
				return;
			}
			this.solver = new IKSolverLimb();
			this.solver.IKPositionWeight = this.weight;
			this.solver.bendModifier = IKSolverLimb.BendModifier.Target;
			this.solver.bendModifierWeight = 1f;
			this.defaultBendNormal = -Vector3.Cross(this.tip.position - this.bone1.position, this.bone2.position - this.bone1.position).normalized;
			this.solver.bendNormal = this.defaultBendNormal;
			Vector3 point = Vector3.Cross(this.bone2.position - this.bone1.position, this.tip.position - this.bone1.position);
			this.bone1Axis = Quaternion.Inverse(this.bone1.rotation) * point;
			this.tipAxis = Quaternion.Inverse(this.tip.rotation) * point;
			Vector3 vector = this.bone2.position - this.bone1.position;
			Vector3 point2 = -Vector3.Cross(this.tip.position - this.bone1.position, this.bone2.position - this.bone1.position);
			Vector3.OrthoNormalize(ref vector, ref point2);
			this.bone1TwistAxis = Quaternion.Inverse(this.bone1.rotation) * point2;
			this.IKPosition = this.tip.position;
			this.IKRotation = this.tip.rotation;
			if (this.bone3 != null)
			{
				this.bone3RelativeToTarget = Quaternion.Inverse(this.IKRotation) * this.bone3.rotation;
				this.bone3DefaultLocalPosition = this.bone3.localPosition;
				this.bone3DefaultLocalRotation = this.bone3.localRotation;
			}
			this.solver.SetChain(this.bone1, this.bone2, this.tip, hand);
			this.solver.Initiate(hand);
			this.initiated = true;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003B4B4 File Offset: 0x000396B4
		public void FixTransforms()
		{
			if (!this.initiated)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				return;
			}
			this.solver.FixTransforms();
			if (this.bone3 != null)
			{
				this.bone3.localPosition = this.bone3DefaultLocalPosition;
				this.bone3.localRotation = this.bone3DefaultLocalRotation;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003B514 File Offset: 0x00039714
		public void StoreDefaultLocalState()
		{
			if (!this.initiated)
			{
				return;
			}
			this.solver.StoreDefaultLocalState();
			if (this.bone3 != null)
			{
				this.bone3DefaultLocalPosition = this.bone3.localPosition;
				this.bone3DefaultLocalRotation = this.bone3.localRotation;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0003B568 File Offset: 0x00039768
		public void Update(float masterWeight)
		{
			if (!this.initiated)
			{
				return;
			}
			float num = this.weight * masterWeight;
			if (num <= 0f)
			{
				return;
			}
			this.solver.target = this.target;
			if (this.target != null)
			{
				this.IKPosition = this.target.position;
				this.IKRotation = this.target.rotation;
			}
			if (this.rotationDOF == Finger.DOF.One)
			{
				Quaternion lhs = Quaternion.FromToRotation(this.IKRotation * this.tipAxis, this.bone1.rotation * this.bone1Axis);
				this.IKRotation = lhs * this.IKRotation;
			}
			if (this.bone3 != null)
			{
				if (num * this.rotationWeight >= 1f)
				{
					this.bone3.rotation = this.IKRotation * this.bone3RelativeToTarget;
				}
				else
				{
					this.bone3.rotation = Quaternion.Lerp(this.bone3.rotation, this.IKRotation * this.bone3RelativeToTarget, num * this.rotationWeight);
				}
			}
			this.solver.IKPositionWeight = num;
			this.solver.IKRotationWeight = this.rotationWeight;
			this.solver.Update();
			if (this.fixBone1Twist)
			{
				Quaternion rotation = this.bone2.rotation;
				Vector3 vector = Quaternion.Inverse(Quaternion.LookRotation(this.bone1.rotation * this.bone1TwistAxis, this.bone2.position - this.bone1.position)) * this.solver.bendNormal;
				float angle = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
				this.bone1.rotation = Quaternion.AngleAxis(angle, this.bone2.position - this.bone1.position) * this.bone1.rotation;
				this.bone2.rotation = rotation;
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0003B774 File Offset: 0x00039974
		public Finger()
		{
		}

		// Token: 0x040006F3 RID: 1779
		[Tooltip("Master Weight for the finger.")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x040006F4 RID: 1780
		[Tooltip("The weight of rotating the finger tip and bending the finger to the target.")]
		[Range(0f, 1f)]
		public float rotationWeight = 1f;

		// Token: 0x040006F5 RID: 1781
		[Tooltip("Rotational degrees of freedom. When set to 'One' the fingers will be able to be rotated only around a single axis. When 3, all 3 axes are free to rotate around.")]
		public Finger.DOF rotationDOF;

		// Token: 0x040006F6 RID: 1782
		[Tooltip("If enabled, keeps bone1 twist angle fixed relative to bone2.")]
		public bool fixBone1Twist;

		// Token: 0x040006F7 RID: 1783
		[Tooltip("The first bone of the finger.")]
		public Transform bone1;

		// Token: 0x040006F8 RID: 1784
		[Tooltip("The second bone of the finger.")]
		public Transform bone2;

		// Token: 0x040006F9 RID: 1785
		[Tooltip("The (optional) third bone of the finger. This can be ignored for thumbs.")]
		public Transform bone3;

		// Token: 0x040006FA RID: 1786
		[Tooltip("The fingertip object. If your character doesn't have tip bones, you can create an empty GameObject and parent it to the last bone in the finger. Place it to the tip of the finger.")]
		public Transform tip;

		// Token: 0x040006FB RID: 1787
		[Tooltip("The IK target (optional, can use IKPosition and IKRotation directly).")]
		public Transform target;

		// Token: 0x040006FC RID: 1788
		[CompilerGenerated]
		private bool <initiated>k__BackingField;

		// Token: 0x040006FD RID: 1789
		private IKSolverLimb solver;

		// Token: 0x040006FE RID: 1790
		private Quaternion bone3RelativeToTarget;

		// Token: 0x040006FF RID: 1791
		private Vector3 bone3DefaultLocalPosition;

		// Token: 0x04000700 RID: 1792
		private Quaternion bone3DefaultLocalRotation;

		// Token: 0x04000701 RID: 1793
		private Vector3 bone1Axis;

		// Token: 0x04000702 RID: 1794
		private Vector3 tipAxis;

		// Token: 0x04000703 RID: 1795
		private Vector3 bone1TwistAxis;

		// Token: 0x04000704 RID: 1796
		private Vector3 defaultBendNormal;

		// Token: 0x020001E7 RID: 487
		[Serializable]
		public enum DOF
		{
			// Token: 0x04000E6D RID: 3693
			One,
			// Token: 0x04000E6E RID: 3694
			Three
		}
	}
}
