using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class FTools_IKProcessorek
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00014674 File Offset: 0x00012874
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001467C File Offset: 0x0001287C
		public bool CCDIK
		{
			[CompilerGenerated]
			get
			{
				return this.<CCDIK>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CCDIK>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00014685 File Offset: 0x00012885
		public FTools_IKProcessorek.FTools_IKProcessorBone StartBone
		{
			get
			{
				return this.IKBones[0];
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001468F File Offset: 0x0001288F
		public FTools_IKProcessorek.FTools_IKProcessorBone ElbowBone
		{
			get
			{
				return this.IKBones[1];
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00014699 File Offset: 0x00012899
		public FTools_IKProcessorek.FTools_IKProcessorBone EndBone
		{
			get
			{
				if (!this.CCDIK)
				{
					return this.IKBones[2];
				}
				return this.IKBones[this.IKBones.Length - 1];
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000146C0 File Offset: 0x000128C0
		public void SetLimb(Transform startBone, Transform elbowBone, Transform endBone)
		{
			this.CCDIK = false;
			this.IKBones = new FTools_IKProcessorek.FTools_IKProcessorBone[3];
			this.IKBones[0] = new FTools_IKProcessorek.FTools_IKProcessorBone
			{
				transform = startBone
			};
			this.IKBones[1] = new FTools_IKProcessorek.FTools_IKProcessorBone
			{
				transform = elbowBone
			};
			this.IKBones[2] = new FTools_IKProcessorek.FTools_IKProcessorBone
			{
				transform = endBone
			};
			this.IKTargetPosition = endBone.position;
			this.IKTargetRotation = endBone.rotation;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014734 File Offset: 0x00012934
		public void SetCCD(Transform[] bonesChain)
		{
			this.CCDIK = true;
			this.IKBones = new FTools_IKProcessorek.FTools_IKProcessorBone[bonesChain.Length];
			for (int i = 0; i < bonesChain.Length; i++)
			{
				this.IKBones[i] = new FTools_IKProcessorek.FTools_IKProcessorBone
				{
					transform = bonesChain[i]
				};
			}
			this.IKTargetPosition = this.EndBone.transform.position;
			this.IKTargetRotation = this.EndBone.transform.rotation;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000147A8 File Offset: 0x000129A8
		public void Initialize(Transform root)
		{
			if (this.Initialized)
			{
				return;
			}
			this.initWorldRootRotation = root.rotation;
			Vector3 lhs = Vector3.Cross(this.ElbowBone.transform.position - this.StartBone.transform.position, this.EndBone.transform.position - this.ElbowBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
			if (this.StartBone.transform.parent != null)
			{
				this.startParentWorldRotation = Quaternion.Inverse(this.initWorldRootRotation) * this.StartBone.transform.parent.rotation;
			}
			this.fullLength = 0f;
			if (!this.CCDIK)
			{
				this.StartBone.Init(this.ElbowBone.transform.position, this.targetElbowNormal);
				this.ElbowBone.Init(this.EndBone.transform.position, this.targetElbowNormal);
				this.EndBone.Init(this.EndBone.transform.position + (this.EndBone.transform.position - this.ElbowBone.transform.position), this.targetElbowNormal);
				this.fullLength = this.IKBones[0].BoneLength + this.IKBones[1].BoneLength;
				this.RefreshOrientationNormal();
			}
			else
			{
				float num = 1f / ((float)this.IKBones.Length * 1.3f);
				for (int i = 0; i < this.IKBones.Length; i++)
				{
					FTools_IKProcessorek.FTools_IKProcessorBone ftools_IKProcessorBone = this.IKBones[i];
					if (i < this.IKBones.Length - 1)
					{
						ftools_IKProcessorBone.Init(this.IKBones[i + 1].transform.position, this.targetElbowNormal);
						this.fullLength += ftools_IKProcessorBone.BoneLength;
						ftools_IKProcessorBone.Axis = Quaternion.Inverse(ftools_IKProcessorBone.transform.rotation) * (this.IKBones[i + 1].transform.position - ftools_IKProcessorBone.transform.position);
					}
					else
					{
						ftools_IKProcessorBone.Axis = Quaternion.Inverse(ftools_IKProcessorBone.transform.rotation) * (this.IKBones[this.IKBones.Length - 1].transform.position - this.IKBones[0].transform.position);
					}
					if (this.AutoWeight)
					{
						ftools_IKProcessorBone.MotionWeight = 1f - num * (float)i;
					}
				}
			}
			if (this.CCD_LimitAngle < 180f)
			{
				for (int j = 0; j < this.IKBones.Length; j++)
				{
					this.IKBones[j].angleLimit = this.CCD_LimitAngle;
					this.IKBones[j].twistAngleLimit = Mathf.Min(80f, this.CCD_LimitAngle);
				}
			}
			this.Initialized = true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014ABC File Offset: 0x00012CBC
		public void Update()
		{
			if (this.CCDIK)
			{
				this.UpdateCCDIK();
				return;
			}
			this.UpdateLimbIK();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public void UpdateLimbIK()
		{
			if (!this.Initialized)
			{
				return;
			}
			this.frameEndBoneRotation = this.EndBone.transform.rotation;
			this.StartBone.BoneLength = (this.ElbowBone.transform.position - this.StartBone.transform.position).sqrMagnitude;
			this.ElbowBone.BoneLength = (this.EndBone.transform.position - this.ElbowBone.transform.position).sqrMagnitude;
			this.targetElbowNormal = this.GetOrientationNormal();
			Vector3 vector = this.GetOrientationDirection(this.IKTargetPosition, this.targetElbowNormal);
			if (vector == Vector3.zero)
			{
				vector = this.ElbowBone.transform.position - this.StartBone.transform.position;
			}
			this.StartBone.transform.rotation = this.StartBone.GetRotation(vector, this.targetElbowNormal);
			this.ElbowBone.transform.rotation = this.ElbowBone.GetRotation(this.IKTargetPosition - this.ElbowBone.transform.position, this.ElbowBone.GetCurrentOrientationNormal());
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00014C28 File Offset: 0x00012E28
		public float GetStretchValue(Vector3 targetPos)
		{
			if (!this.CCDIK)
			{
				float num = Mathf.Epsilon;
				num += (this.StartBone.transform.position - this.ElbowBone.transform.position).magnitude;
				num += (this.ElbowBone.transform.position - this.EndBone.transform.position).magnitude;
				return (this.StartBone.transform.position - targetPos).magnitude / num;
			}
			float num2 = Mathf.Epsilon;
			for (int i = 0; i < this.IKBones.Length - 1; i++)
			{
				num2 += (this.IKBones[i].transform.position - this.IKBones[i + 1].transform.position).magnitude;
			}
			return (this.StartBone.transform.position - targetPos).magnitude / num2;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00014D38 File Offset: 0x00012F38
		private Vector3 GetOrientationNormal()
		{
			if (this.IKElbowTargetPosition.sqrMagnitude != 0f)
			{
				return this.CalculateElbowNormalToPosition(this.IKElbowTargetPosition);
			}
			return this.GetAutomaticElbowNormal();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00014D60 File Offset: 0x00012F60
		public Vector3 CalculateElbowNormalToPosition(Vector3 targetElbowPos)
		{
			return Vector3.Cross(targetElbowPos - this.StartBone.transform.position, this.EndBone.transform.position - this.StartBone.transform.position);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public void RefreshOrientationNormal()
		{
			Vector3 lhs = Vector3.Cross(this.ElbowBone.transform.position - this.StartBone.transform.position, this.EndBone.transform.position - this.ElbowBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00014E24 File Offset: 0x00013024
		private Vector3 GetOrientationDirection(Vector3 ikPosition, Vector3 orientationNormal)
		{
			Vector3 vector = ikPosition - this.StartBone.transform.position;
			if (vector == Vector3.zero)
			{
				return Vector3.zero;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			float num = (sqrMagnitude + this.StartBone.BoneLength - this.ElbowBone.BoneLength) / 2f / Mathf.Sqrt(sqrMagnitude);
			float num2 = Mathf.Sqrt(this.StartBone.BoneLength - num * num);
			if (float.IsNaN(num2))
			{
				num2 = 0f;
			}
			Vector3 upwards = Vector3.Cross(vector, orientationNormal);
			return Quaternion.LookRotation(vector, upwards) * new Vector3(0f, num2, num);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00014ED0 File Offset: 0x000130D0
		private Vector3 GetAutomaticElbowNormal()
		{
			Vector3 currentOrientationNormal = this.StartBone.GetCurrentOrientationNormal();
			switch (this.ElbowMode)
			{
			case FTools_IKProcessorek.FIK_ElbowMode.Animation:
				if (!this.maintained)
				{
					this.targetElbowNormal = this.StartBone.GetCurrentOrientationNormal();
				}
				this.maintained = false;
				return Vector3.Lerp(currentOrientationNormal, this.targetElbowNormal, this.weight);
			case FTools_IKProcessorek.FIK_ElbowMode.Target:
			{
				Quaternion b = this.IKTargetRotation * Quaternion.Inverse(this.EndBone.initLocalRotation);
				return Quaternion.Slerp(Quaternion.identity, b, this.weight) * currentOrientationNormal;
			}
			case FTools_IKProcessorek.FIK_ElbowMode.Parent:
			{
				Quaternion lhs = this.StartBone.transform.parent.rotation * Quaternion.Inverse(this.startParentWorldRotation);
				return Quaternion.Slerp(Quaternion.identity, lhs * Quaternion.Inverse(this.initWorldRootRotation), this.weight) * currentOrientationNormal;
			}
			default:
				return currentOrientationNormal;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00014FC0 File Offset: 0x000131C0
		public void UpdateCCDIK()
		{
			if (!this.Initialized)
			{
				return;
			}
			if (this.CCD_ReactionQuality < 0)
			{
				this.CCD_ReactionQuality = 1;
			}
			Vector3 b = Vector3.zero;
			if (this.CCD_ReactionQuality > 1)
			{
				b = this.GetGoalPivotOffset();
			}
			int num = 0;
			while (num < this.CCD_ReactionQuality && (num < 1 || b.sqrMagnitude != 0f || this.CCD_Smoothing <= 0f || this.GetVelocityDifference() >= this.CCD_Smoothing * this.CCD_Smoothing))
			{
				this.LastLocalDirection = this.RefreshLocalDirection();
				Vector3 a = this.IKTargetPosition + b;
				for (int i = this.IKBones.Length - 2; i > -1; i--)
				{
					float num2 = this.IKBones[i].MotionWeight * this.IKWeight;
					if (num2 > 0f)
					{
						Vector3 fromDirection = this.IKBones[this.IKBones.Length - 1].transform.position - this.IKBones[i].transform.position;
						Vector3 toDirection = a - this.IKBones[i].transform.position;
						Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection) * this.IKBones[i].transform.rotation;
						if (num2 < 1f)
						{
							this.IKBones[i].transform.rotation = Quaternion.Lerp(this.IKBones[i].transform.rotation, quaternion, num2);
						}
						else
						{
							this.IKBones[i].transform.rotation = quaternion;
						}
					}
					this.IKBones[i].AngleLimiting();
				}
				num++;
			}
			this.LastLocalDirection = this.RefreshLocalDirection();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015174 File Offset: 0x00013374
		protected Vector3 GetGoalPivotOffset()
		{
			if (!this.GoalPivotOffsetDetected())
			{
				return Vector3.zero;
			}
			Vector3 normalized = (this.IKTargetPosition - this.IKBones[0].transform.position).normalized;
			Vector3 rhs = new Vector3(normalized.y, normalized.z, normalized.x);
			if (this.CCD_LimitAngle > 0f && (this.IKBones[this.IKBones.Length - 2].angleLimit < 180f || this.IKBones[this.IKBones.Length - 2].twistAngleLimit < 180f))
			{
				rhs = this.IKBones[this.IKBones.Length - 2].transform.rotation * this.IKBones[this.IKBones.Length - 2].Axis;
			}
			return Vector3.Cross(normalized, rhs) * this.IKBones[this.IKBones.Length - 2].BoneLength * 0.5f;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00015278 File Offset: 0x00013478
		private bool GoalPivotOffsetDetected()
		{
			if (!this.Initialized)
			{
				return false;
			}
			Vector3 a = this.IKBones[this.IKBones.Length - 1].transform.position - this.IKBones[0].transform.position;
			Vector3 a2 = this.IKTargetPosition - this.IKBones[0].transform.position;
			float magnitude = a.magnitude;
			float magnitude2 = a2.magnitude;
			return magnitude2 != 0f && magnitude != 0f && magnitude >= magnitude2 && magnitude >= this.fullLength - this.IKBones[this.IKBones.Length - 2].BoneLength * 0.1f && magnitude2 <= magnitude && Vector3.Dot(a / magnitude, a2 / magnitude2) >= 0.999f;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00015358 File Offset: 0x00013558
		private Vector3 RefreshLocalDirection()
		{
			this.LocalDirection = this.IKBones[0].transform.InverseTransformDirection(this.IKBones[this.IKBones.Length - 1].transform.position - this.IKBones[0].transform.position);
			return this.LocalDirection;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000153B5 File Offset: 0x000135B5
		private float GetVelocityDifference()
		{
			return Vector3.SqrMagnitude(this.LocalDirection - this.LastLocalDirection);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000153D0 File Offset: 0x000135D0
		public FTools_IKProcessorek()
		{
		}

		// Token: 0x04000284 RID: 644
		public Vector3 IKTargetPosition;

		// Token: 0x04000285 RID: 645
		public Quaternion IKTargetRotation;

		// Token: 0x04000286 RID: 646
		public Vector3 IKElbowTargetPosition = Vector3.zero;

		// Token: 0x04000287 RID: 647
		public FTools_IKProcessorek.FTools_IKProcessorBone[] IKBones;

		// Token: 0x04000288 RID: 648
		public bool Initialized;

		// Token: 0x04000289 RID: 649
		[CompilerGenerated]
		private bool <CCDIK>k__BackingField;

		// Token: 0x0400028A RID: 650
		[Range(0f, 1f)]
		public float IKWeight = 1f;

		// Token: 0x0400028B RID: 651
		public Vector3 targetElbowNormal = Vector3.right;

		// Token: 0x0400028C RID: 652
		public bool LHand;

		// Token: 0x0400028D RID: 653
		public FTools_IKProcessorek.FIK_ElbowMode ElbowMode = FTools_IKProcessorek.FIK_ElbowMode.Target;

		// Token: 0x0400028E RID: 654
		public Quaternion frameEndBoneRotation;

		// Token: 0x0400028F RID: 655
		private float fullLength;

		// Token: 0x04000290 RID: 656
		[Range(1f, 12f)]
		public int CCD_ReactionQuality = 4;

		// Token: 0x04000291 RID: 657
		[Range(0f, 1f)]
		public float CCD_Smoothing;

		// Token: 0x04000292 RID: 658
		[Range(0f, 181f)]
		public float CCD_LimitAngle = 60f;

		// Token: 0x04000293 RID: 659
		public bool AutoWeight = true;

		// Token: 0x04000294 RID: 660
		public Vector3 LastLocalDirection;

		// Token: 0x04000295 RID: 661
		public Vector3 LocalDirection;

		// Token: 0x04000296 RID: 662
		private Quaternion initWorldRootRotation;

		// Token: 0x04000297 RID: 663
		private bool maintained;

		// Token: 0x04000298 RID: 664
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000299 RID: 665
		private Quaternion startParentWorldRotation;

		// Token: 0x020001A9 RID: 425
		public enum FIK_ElbowMode
		{
			// Token: 0x04000D22 RID: 3362
			None,
			// Token: 0x04000D23 RID: 3363
			Animation,
			// Token: 0x04000D24 RID: 3364
			Target,
			// Token: 0x04000D25 RID: 3365
			Parent
		}

		// Token: 0x020001AA RID: 426
		[Serializable]
		public class FTools_IKProcessorBone
		{
			// Token: 0x06000F03 RID: 3843 RVA: 0x000620F0 File Offset: 0x000602F0
			public void Init(Vector3 childPosition, Vector3 orientationNormal)
			{
				Quaternion rotation = Quaternion.LookRotation(childPosition - this.transform.position, orientationNormal);
				this.targetToLocalSpace = FTools_IKProcessorek.FTools_IKProcessorBone.RotationToLocal(this.transform.rotation, rotation);
				this.defaultLocalPoleNormal = Quaternion.Inverse(this.transform.rotation) * orientationNormal;
				this.BoneLength = (childPosition - this.transform.position).sqrMagnitude;
				this.initLocalRotation = this.transform.localRotation;
				this.initWorldRotation = this.transform.rotation;
			}

			// Token: 0x06000F04 RID: 3844 RVA: 0x00062189 File Offset: 0x00060389
			public static Quaternion RotationToLocal(Quaternion parent, Quaternion rotation)
			{
				return Quaternion.Inverse(Quaternion.Inverse(parent) * rotation);
			}

			// Token: 0x06000F05 RID: 3845 RVA: 0x0006219C File Offset: 0x0006039C
			public Quaternion GetRotation(Vector3 direction, Vector3 orientationNormal)
			{
				return Quaternion.LookRotation(direction, orientationNormal) * this.targetToLocalSpace;
			}

			// Token: 0x06000F06 RID: 3846 RVA: 0x000621B0 File Offset: 0x000603B0
			public Vector3 GetCurrentOrientationNormal()
			{
				return this.transform.rotation * this.defaultLocalPoleNormal;
			}

			// Token: 0x06000F07 RID: 3847 RVA: 0x000621C8 File Offset: 0x000603C8
			public void AngleLimiting()
			{
				Quaternion quaternion = Quaternion.Inverse(this.initLocalRotation) * this.transform.localRotation;
				Quaternion quaternion2 = quaternion;
				if (this.hingeLimits.sqrMagnitude == 0f)
				{
					if (this.angleLimit < 180f)
					{
						quaternion2 = this.LimitPY(quaternion2);
					}
					if (this.twistAngleLimit < 180f)
					{
						quaternion2 = this.LimitRoll(quaternion2);
					}
				}
				else
				{
					quaternion2 = this.LimitHinge(quaternion2);
				}
				if (object.Equals(quaternion2, quaternion))
				{
					return;
				}
				this.transform.localRotation = this.initLocalRotation * quaternion2;
			}

			// Token: 0x06000F08 RID: 3848 RVA: 0x00062264 File Offset: 0x00060464
			private Quaternion LimitPY(Quaternion rotation)
			{
				if (object.Equals(rotation, Quaternion.identity))
				{
					return rotation;
				}
				Vector3 vector = rotation * this.Axis;
				Quaternion to = Quaternion.FromToRotation(this.Axis, vector);
				Quaternion rotation2 = Quaternion.RotateTowards(Quaternion.identity, to, this.angleLimit);
				return Quaternion.FromToRotation(vector, rotation2 * this.Axis) * rotation;
			}

			// Token: 0x06000F09 RID: 3849 RVA: 0x000622D0 File Offset: 0x000604D0
			private Quaternion LimitRoll(Quaternion currentRotation)
			{
				Vector3 vector = new Vector3(this.Axis.y, this.Axis.z, this.Axis.x);
				Vector3 vector2 = currentRotation * this.Axis;
				Vector3 toDirection = vector;
				Vector3.OrthoNormalize(ref vector2, ref toDirection);
				Vector3 fromDirection = currentRotation * vector;
				Vector3.OrthoNormalize(ref vector2, ref fromDirection);
				Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection) * currentRotation;
				if (this.twistAngleLimit <= 0f)
				{
					return quaternion;
				}
				return Quaternion.RotateTowards(quaternion, currentRotation, this.twistAngleLimit);
			}

			// Token: 0x06000F0A RID: 3850 RVA: 0x0006235C File Offset: 0x0006055C
			private Quaternion LimitHinge(Quaternion rotation)
			{
				Quaternion quaternion = Quaternion.FromToRotation(rotation * this.Axis, this.Axis) * rotation * Quaternion.Inverse(this.previousHingeRotation);
				float num = Quaternion.Angle(Quaternion.identity, quaternion);
				Vector3 vector = new Vector3(this.Axis.z, this.Axis.x, this.Axis.y);
				Vector3 rhs = Vector3.Cross(vector, this.Axis);
				if (Vector3.Dot(quaternion * vector, rhs) > 0f)
				{
					num = -num;
				}
				this.previousHingeAngle = Mathf.Clamp(this.previousHingeAngle + num, this.hingeLimits.x, this.hingeLimits.y);
				this.previousHingeRotation = Quaternion.AngleAxis(this.previousHingeAngle, this.Axis);
				return this.previousHingeRotation;
			}

			// Token: 0x06000F0B RID: 3851 RVA: 0x00062435 File Offset: 0x00060635
			public FTools_IKProcessorBone()
			{
			}

			// Token: 0x04000D26 RID: 3366
			public Transform transform;

			// Token: 0x04000D27 RID: 3367
			public float BoneLength;

			// Token: 0x04000D28 RID: 3368
			public Vector3 Axis;

			// Token: 0x04000D29 RID: 3369
			public float MotionWeight = 1f;

			// Token: 0x04000D2A RID: 3370
			[SerializeField]
			private Quaternion targetToLocalSpace;

			// Token: 0x04000D2B RID: 3371
			[SerializeField]
			private Vector3 defaultLocalPoleNormal;

			// Token: 0x04000D2C RID: 3372
			public Quaternion initWorldRotation;

			// Token: 0x04000D2D RID: 3373
			[Range(0f, 180f)]
			public float angleLimit = 45f;

			// Token: 0x04000D2E RID: 3374
			[Range(0f, 180f)]
			public float twistAngleLimit = 180f;

			// Token: 0x04000D2F RID: 3375
			public Vector2 hingeLimits = Vector2.zero;

			// Token: 0x04000D30 RID: 3376
			public Quaternion initLocalRotation;

			// Token: 0x04000D31 RID: 3377
			public Quaternion previousHingeRotation;

			// Token: 0x04000D32 RID: 3378
			public float previousHingeAngle;
		}
	}
}
