using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FA RID: 250
	[Serializable]
	public class IKSolverLimb : IKSolverTrigonometric
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x000485D7 File Offset: 0x000467D7
		public void MaintainRotation()
		{
			if (!base.initiated)
			{
				return;
			}
			this.maintainRotation = this.bone3.transform.rotation;
			this.maintainRotationFor1Frame = true;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000485FF File Offset: 0x000467FF
		public void MaintainBend()
		{
			if (!base.initiated)
			{
				return;
			}
			this.animationNormal = this.bone1.GetBendNormalFromCurrentRotation();
			this.maintainBendFor1Frame = true;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00048624 File Offset: 0x00046824
		protected override void OnInitiateVirtual()
		{
			this.defaultRootRotation = this.root.rotation;
			if (this.bone1.transform.parent != null)
			{
				this.parentDefaultRotation = Quaternion.Inverse(this.defaultRootRotation) * this.bone1.transform.parent.rotation;
			}
			if (this.bone3.rotationLimit != null)
			{
				this.bone3.rotationLimit.Disable();
			}
			this.bone3DefaultRotation = this.bone3.transform.rotation;
			Vector3 vector = Vector3.Cross(this.bone2.transform.position - this.bone1.transform.position, this.bone3.transform.position - this.bone2.transform.position);
			if (vector != Vector3.zero)
			{
				this.bendNormal = vector;
			}
			this.animationNormal = this.bendNormal;
			this.StoreAxisDirections(ref this.axisDirectionsLeft);
			this.StoreAxisDirections(ref this.axisDirectionsRight);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00048748 File Offset: 0x00046948
		protected override void OnUpdateVirtual()
		{
			if (this.IKPositionWeight > 0f)
			{
				this.bendModifierWeight = Mathf.Clamp(this.bendModifierWeight, 0f, 1f);
				this.maintainRotationWeight = Mathf.Clamp(this.maintainRotationWeight, 0f, 1f);
				this._bendNormal = this.bendNormal;
				this.bendNormal = this.GetModifiedBendNormal();
			}
			if (this.maintainRotationWeight * this.IKPositionWeight > 0f)
			{
				this.bone3RotationBeforeSolve = (this.maintainRotationFor1Frame ? this.maintainRotation : this.bone3.transform.rotation);
				this.maintainRotationFor1Frame = false;
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000487F4 File Offset: 0x000469F4
		protected override void OnPostSolveVirtual()
		{
			if (this.IKPositionWeight > 0f)
			{
				this.bendNormal = this._bendNormal;
			}
			if (this.maintainRotationWeight * this.IKPositionWeight > 0f)
			{
				this.bone3.transform.rotation = Quaternion.Slerp(this.bone3.transform.rotation, this.bone3RotationBeforeSolve, this.maintainRotationWeight * this.IKPositionWeight);
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00048866 File Offset: 0x00046A66
		public IKSolverLimb()
		{
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00048891 File Offset: 0x00046A91
		public IKSolverLimb(AvatarIKGoal goal)
		{
			this.goal = goal;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000488C3 File Offset: 0x00046AC3
		private IKSolverLimb.AxisDirection[] axisDirections
		{
			get
			{
				if (this.goal == AvatarIKGoal.LeftHand)
				{
					return this.axisDirectionsLeft;
				}
				return this.axisDirectionsRight;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000488DC File Offset: 0x00046ADC
		private void StoreAxisDirections(ref IKSolverLimb.AxisDirection[] axisDirections)
		{
			axisDirections[0] = new IKSolverLimb.AxisDirection(Vector3.zero, new Vector3(-1f, 0f, 0f));
			axisDirections[1] = new IKSolverLimb.AxisDirection(new Vector3(0.5f, 0f, -0.2f), new Vector3(-0.5f, -1f, 1f));
			axisDirections[2] = new IKSolverLimb.AxisDirection(new Vector3(-0.5f, -1f, -0.2f), new Vector3(0f, 0.5f, -1f));
			axisDirections[3] = new IKSolverLimb.AxisDirection(new Vector3(-0.5f, -0.5f, 1f), new Vector3(-1f, -1f, -1f));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000489B0 File Offset: 0x00046BB0
		private Vector3 GetModifiedBendNormal()
		{
			float num = this.bendModifierWeight;
			if (num <= 0f)
			{
				return this.bendNormal;
			}
			switch (this.bendModifier)
			{
			case IKSolverLimb.BendModifier.Animation:
				if (!this.maintainBendFor1Frame)
				{
					this.MaintainBend();
				}
				this.maintainBendFor1Frame = false;
				return Vector3.Lerp(this.bendNormal, this.animationNormal, num);
			case IKSolverLimb.BendModifier.Target:
			{
				Quaternion b = this.IKRotation * Quaternion.Inverse(this.bone3DefaultRotation);
				return Quaternion.Slerp(Quaternion.identity, b, num) * this.bendNormal;
			}
			case IKSolverLimb.BendModifier.Parent:
			{
				if (this.bone1.transform.parent == null)
				{
					return this.bendNormal;
				}
				Quaternion lhs = this.bone1.transform.parent.rotation * Quaternion.Inverse(this.parentDefaultRotation);
				return Quaternion.Slerp(Quaternion.identity, lhs * Quaternion.Inverse(this.defaultRootRotation), num) * this.bendNormal;
			}
			case IKSolverLimb.BendModifier.Arm:
			{
				if (this.bone1.transform.parent == null)
				{
					return this.bendNormal;
				}
				if (this.goal == AvatarIKGoal.LeftFoot || this.goal == AvatarIKGoal.RightFoot)
				{
					if (!Warning.logged)
					{
						base.LogWarning("Trying to use the 'Arm' bend modifier on a leg.");
					}
					return this.bendNormal;
				}
				Vector3 vector = (this.IKPosition - this.bone1.transform.position).normalized;
				vector = Quaternion.Inverse(this.bone1.transform.parent.rotation * Quaternion.Inverse(this.parentDefaultRotation)) * vector;
				if (this.goal == AvatarIKGoal.LeftHand)
				{
					vector.x = -vector.x;
				}
				for (int i = 1; i < this.axisDirections.Length; i++)
				{
					this.axisDirections[i].dot = Mathf.Clamp(Vector3.Dot(this.axisDirections[i].direction, vector), 0f, 1f);
					this.axisDirections[i].dot = Interp.Float(this.axisDirections[i].dot, InterpolationMode.InOutQuintic);
				}
				Vector3 vector2 = this.axisDirections[0].axis;
				for (int j = 1; j < this.axisDirections.Length; j++)
				{
					vector2 = Vector3.Slerp(vector2, this.axisDirections[j].axis, this.axisDirections[j].dot);
				}
				if (this.goal == AvatarIKGoal.LeftHand)
				{
					vector2.x = -vector2.x;
					vector2 = -vector2;
				}
				Vector3 vector3 = this.bone1.transform.parent.rotation * Quaternion.Inverse(this.parentDefaultRotation) * vector2;
				if (num >= 1f)
				{
					return vector3;
				}
				return Vector3.Lerp(this.bendNormal, vector3, num);
			}
			case IKSolverLimb.BendModifier.Goal:
			{
				if (this.bendGoal == null)
				{
					if (!Warning.logged)
					{
						base.LogWarning("Trying to use the 'Goal' Bend Modifier, but the Bend Goal is unassigned.");
					}
					return this.bendNormal;
				}
				Vector3 vector4 = Vector3.Cross(this.bendGoal.position - this.bone1.transform.position, this.IKPosition - this.bone1.transform.position);
				if (vector4 == Vector3.zero)
				{
					return this.bendNormal;
				}
				if (num >= 1f)
				{
					return vector4;
				}
				return Vector3.Lerp(this.bendNormal, vector4, num);
			}
			default:
				return this.bendNormal;
			}
		}

		// Token: 0x04000874 RID: 2164
		public AvatarIKGoal goal;

		// Token: 0x04000875 RID: 2165
		public IKSolverLimb.BendModifier bendModifier;

		// Token: 0x04000876 RID: 2166
		[Range(0f, 1f)]
		public float maintainRotationWeight;

		// Token: 0x04000877 RID: 2167
		[Range(0f, 1f)]
		public float bendModifierWeight = 1f;

		// Token: 0x04000878 RID: 2168
		public Transform bendGoal;

		// Token: 0x04000879 RID: 2169
		private bool maintainBendFor1Frame;

		// Token: 0x0400087A RID: 2170
		private bool maintainRotationFor1Frame;

		// Token: 0x0400087B RID: 2171
		private Quaternion defaultRootRotation;

		// Token: 0x0400087C RID: 2172
		private Quaternion parentDefaultRotation;

		// Token: 0x0400087D RID: 2173
		private Quaternion bone3RotationBeforeSolve;

		// Token: 0x0400087E RID: 2174
		private Quaternion maintainRotation;

		// Token: 0x0400087F RID: 2175
		private Quaternion bone3DefaultRotation;

		// Token: 0x04000880 RID: 2176
		private Vector3 _bendNormal;

		// Token: 0x04000881 RID: 2177
		private Vector3 animationNormal;

		// Token: 0x04000882 RID: 2178
		private IKSolverLimb.AxisDirection[] axisDirectionsLeft = new IKSolverLimb.AxisDirection[4];

		// Token: 0x04000883 RID: 2179
		private IKSolverLimb.AxisDirection[] axisDirectionsRight = new IKSolverLimb.AxisDirection[4];

		// Token: 0x020001F9 RID: 505
		[Serializable]
		public enum BendModifier
		{
			// Token: 0x04000EE8 RID: 3816
			Animation,
			// Token: 0x04000EE9 RID: 3817
			Target,
			// Token: 0x04000EEA RID: 3818
			Parent,
			// Token: 0x04000EEB RID: 3819
			Arm,
			// Token: 0x04000EEC RID: 3820
			Goal
		}

		// Token: 0x020001FA RID: 506
		[Serializable]
		public struct AxisDirection
		{
			// Token: 0x06001086 RID: 4230 RVA: 0x00066833 File Offset: 0x00064A33
			public AxisDirection(Vector3 direction, Vector3 axis)
			{
				this.direction = direction.normalized;
				this.axis = axis.normalized;
				this.dot = 0f;
			}

			// Token: 0x04000EED RID: 3821
			public Vector3 direction;

			// Token: 0x04000EEE RID: 3822
			public Vector3 axis;

			// Token: 0x04000EEF RID: 3823
			public float dot;
		}
	}
}
