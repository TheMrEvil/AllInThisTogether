using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class FIK_CCDProcessor : FIK_ProcessorBase
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001542F File Offset: 0x0001362F
		public FIK_CCDProcessor.CCDIKBone StartIKBone
		{
			get
			{
				return this.IKBones[0];
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00015439 File Offset: 0x00013639
		public FIK_CCDProcessor.CCDIKBone EndIKBone
		{
			get
			{
				return this.IKBones[this.IKBones.Length - 1];
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001544C File Offset: 0x0001364C
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00015454 File Offset: 0x00013654
		public float ActiveLength
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveLength>k__BackingField = value;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00015460 File Offset: 0x00013660
		public FIK_CCDProcessor(Transform[] bonesChain)
		{
			this.IKBones = new FIK_CCDProcessor.CCDIKBone[bonesChain.Length];
			FIK_IKBoneBase[] bones = new FIK_CCDProcessor.CCDIKBone[this.IKBones.Length];
			base.Bones = bones;
			for (int i = 0; i < bonesChain.Length; i++)
			{
				this.IKBones[i] = new FIK_CCDProcessor.CCDIKBone(bonesChain[i]);
				base.Bones[i] = this.IKBones[i];
			}
			this.IKTargetPosition = base.EndBone.transform.position;
			this.IKTargetRotation = base.EndBone.transform.rotation;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00015528 File Offset: 0x00013728
		public override void Init(Transform root)
		{
			if (base.Initialized)
			{
				return;
			}
			base.fullLength = 0f;
			for (int i = 0; i < base.Bones.Length; i++)
			{
				FIK_CCDProcessor.CCDIKBone ccdikbone = this.IKBones[i];
				FIK_CCDProcessor.CCDIKBone child = null;
				FIK_CCDProcessor.CCDIKBone parent = null;
				if (i > 0)
				{
					parent = this.IKBones[i - 1];
				}
				else if (i < base.Bones.Length - 1)
				{
					child = this.IKBones[i + 1];
				}
				if (i < base.Bones.Length - 1)
				{
					this.IKBones[i].Init(child, parent);
					base.fullLength += ccdikbone.BoneLength;
					ccdikbone.ForwardOrientation = Quaternion.Inverse(ccdikbone.transform.rotation) * (this.IKBones[i + 1].transform.position - ccdikbone.transform.position);
				}
				else
				{
					this.IKBones[i].Init(child, parent);
					ccdikbone.ForwardOrientation = Quaternion.Inverse(ccdikbone.transform.rotation) * (this.IKBones[this.IKBones.Length - 1].transform.position - this.IKBones[0].transform.position);
				}
			}
			base.Initialized = true;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001566C File Offset: 0x0001386C
		public override void Update()
		{
			if (!base.Initialized)
			{
				return;
			}
			if (this.IKWeight <= 0f)
			{
				return;
			}
			FIK_CCDProcessor.CCDIKBone ccdikbone = this.IKBones[0];
			if (this.ContinousSolving)
			{
				while (ccdikbone != null)
				{
					ccdikbone.LastKeyLocalRotation = ccdikbone.transform.localRotation;
					ccdikbone.transform.localPosition = ccdikbone.LastIKLocPosition;
					ccdikbone.transform.localRotation = ccdikbone.LastIKLocRotation;
					ccdikbone = ccdikbone.IKChild;
				}
			}
			else if (this.SyncWithAnimator > 0f)
			{
				while (ccdikbone != null)
				{
					ccdikbone.LastKeyLocalRotation = ccdikbone.transform.localRotation;
					ccdikbone = ccdikbone.IKChild;
				}
			}
			if (this.ReactionQuality < 0)
			{
				this.ReactionQuality = 1;
			}
			Vector3 b = Vector3.zero;
			if (this.ReactionQuality > 1)
			{
				b = this.GetGoalPivotOffset();
			}
			int num = 0;
			while (num < this.ReactionQuality && (num < 1 || b.sqrMagnitude != 0f || this.Smoothing <= 0f || this.GetVelocityDifference() >= this.Smoothing * this.Smoothing))
			{
				this.LastLocalDirection = this.RefreshLocalDirection();
				Vector3 a = this.IKTargetPosition + b;
				ccdikbone = this.IKBones[this.IKBones.Length - 2];
				if (!this.Use2D)
				{
					while (ccdikbone != null)
					{
						float num2 = ccdikbone.MotionWeight * this.IKWeight;
						if (num2 > 0f)
						{
							Quaternion quaternion = Quaternion.FromToRotation(base.Bones[base.Bones.Length - 1].transform.position - ccdikbone.transform.position, a - ccdikbone.transform.position) * ccdikbone.transform.rotation;
							if (num2 < 1f)
							{
								ccdikbone.transform.rotation = Quaternion.Lerp(ccdikbone.transform.rotation, quaternion, num2);
							}
							else
							{
								ccdikbone.transform.rotation = quaternion;
							}
						}
						ccdikbone.AngleLimiting();
						ccdikbone = ccdikbone.IKParent;
					}
				}
				else
				{
					while (ccdikbone != null)
					{
						float num3 = ccdikbone.MotionWeight * this.IKWeight;
						if (num3 > 0f)
						{
							Vector3 vector = base.Bones[base.Bones.Length - 1].transform.position - ccdikbone.transform.position;
							Vector3 vector2 = a - ccdikbone.transform.position;
							ccdikbone.transform.rotation = Quaternion.AngleAxis(Mathf.DeltaAngle(Mathf.Atan2(vector.x, vector.y) * 57.29578f, Mathf.Atan2(vector2.x, vector2.y) * 57.29578f) * num3, Vector3.back) * ccdikbone.transform.rotation;
						}
						ccdikbone.AngleLimiting();
						ccdikbone = ccdikbone.IKParent;
					}
				}
				num++;
			}
			this.LastLocalDirection = this.RefreshLocalDirection();
			if (this.MaxStretching > 0f)
			{
				this.ActiveLength = Mathf.Epsilon;
				ccdikbone = this.IKBones[0];
				while (ccdikbone.IKChild != null)
				{
					ccdikbone.FrameWorldLength = (ccdikbone.transform.position - ccdikbone.IKChild.transform.position).magnitude;
					this.ActiveLength += ccdikbone.FrameWorldLength;
					ccdikbone = ccdikbone.IKChild;
				}
				Vector3 vector3 = this.IKTargetPosition - base.StartBone.transform.position;
				float num4 = vector3.magnitude / this.ActiveLength;
				if (num4 > 1f)
				{
					for (int i = 1; i < this.IKBones.Length; i++)
					{
						this.IKBones[i].transform.position += vector3.normalized * (this.IKBones[i - 1].BoneLength * this.MaxStretching) * this.StretchCurve.Evaluate(-(1f - num4));
					}
				}
			}
			for (ccdikbone = this.IKBones[0]; ccdikbone != null; ccdikbone = ccdikbone.IKChild)
			{
				ccdikbone.LastIKLocRotation = ccdikbone.transform.localRotation;
				ccdikbone.LastIKLocPosition = ccdikbone.transform.localPosition;
				Quaternion lhs = ccdikbone.LastIKLocRotation * Quaternion.Inverse(ccdikbone.InitialLocalRotation);
				ccdikbone.transform.localRotation = Quaternion.Lerp(ccdikbone.LastIKLocRotation, lhs * ccdikbone.LastKeyLocalRotation, this.SyncWithAnimator);
				if (this.IKWeight < 1f)
				{
					ccdikbone.transform.localRotation = Quaternion.Lerp(ccdikbone.LastKeyLocalRotation, ccdikbone.transform.localRotation, this.IKWeight);
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00015B24 File Offset: 0x00013D24
		protected Vector3 GetGoalPivotOffset()
		{
			if (!this.GoalPivotOffsetDetected())
			{
				return Vector3.zero;
			}
			Vector3 normalized = (this.IKTargetPosition - this.IKBones[0].transform.position).normalized;
			Vector3 rhs = new Vector3(normalized.y, normalized.z, normalized.x);
			if (this.IKBones[this.IKBones.Length - 2].AngleLimit < 180f || this.IKBones[this.IKBones.Length - 2].TwistAngleLimit < 180f)
			{
				rhs = this.IKBones[this.IKBones.Length - 2].transform.rotation * this.IKBones[this.IKBones.Length - 2].ForwardOrientation;
			}
			return Vector3.Cross(normalized, rhs) * this.IKBones[this.IKBones.Length - 2].BoneLength * 0.5f;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00015C1C File Offset: 0x00013E1C
		private bool GoalPivotOffsetDetected()
		{
			if (!base.Initialized)
			{
				return false;
			}
			Vector3 a = base.Bones[base.Bones.Length - 1].transform.position - base.Bones[0].transform.position;
			Vector3 a2 = this.IKTargetPosition - base.Bones[0].transform.position;
			float magnitude = a.magnitude;
			float magnitude2 = a2.magnitude;
			return magnitude2 != 0f && magnitude != 0f && magnitude >= magnitude2 && magnitude >= base.fullLength - base.Bones[base.Bones.Length - 2].BoneLength * 0.1f && magnitude2 <= magnitude && Vector3.Dot(a / magnitude, a2 / magnitude2) >= 0.999f;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00015CFC File Offset: 0x00013EFC
		private Vector3 RefreshLocalDirection()
		{
			this.LocalDirection = base.Bones[0].transform.InverseTransformDirection(base.Bones[base.Bones.Length - 1].transform.position - base.Bones[0].transform.position);
			return this.LocalDirection;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00015D59 File Offset: 0x00013F59
		private float GetVelocityDifference()
		{
			return Vector3.SqrMagnitude(this.LocalDirection - this.LastLocalDirection);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015D74 File Offset: 0x00013F74
		public void AutoLimitAngle(float angleLimit = 60f, float twistAngleLimit = 50f)
		{
			if (this.IKBones == null)
			{
				return;
			}
			float num = 1f / (float)this.IKBones.Length;
			for (int i = 0; i < this.IKBones.Length; i++)
			{
				this.IKBones[i].AngleLimit = angleLimit * Mathf.Min(1f, (float)(i + 1) * num * 3f);
				this.IKBones[i].TwistAngleLimit = twistAngleLimit * Mathf.Min(1f, (float)(i + 1) * num * 4.5f);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public void AutoWeightBones(float baseValue = 1f)
		{
			float num = baseValue / ((float)base.Bones.Length * 1.3f);
			for (int i = 0; i < base.Bones.Length; i++)
			{
				base.Bones[i].MotionWeight = baseValue - num * (float)i;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00015E40 File Offset: 0x00014040
		public void AutoWeightBones(AnimationCurve weightCurve)
		{
			for (int i = 0; i < base.Bones.Length; i++)
			{
				base.Bones[i].MotionWeight = Mathf.Clamp(weightCurve.Evaluate((float)i / (float)base.Bones.Length), 0f, 1f);
			}
		}

		// Token: 0x0400029A RID: 666
		public FIK_CCDProcessor.CCDIKBone[] IKBones;

		// Token: 0x0400029B RID: 667
		public bool ContinousSolving = true;

		// Token: 0x0400029C RID: 668
		[Range(0f, 1f)]
		public float SyncWithAnimator = 1f;

		// Token: 0x0400029D RID: 669
		[Range(1f, 12f)]
		public int ReactionQuality = 2;

		// Token: 0x0400029E RID: 670
		[Range(0f, 1f)]
		public float Smoothing;

		// Token: 0x0400029F RID: 671
		[Range(0f, 1.5f)]
		public float MaxStretching;

		// Token: 0x040002A0 RID: 672
		public AnimationCurve StretchCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040002A1 RID: 673
		public bool Use2D;

		// Token: 0x040002A2 RID: 674
		[CompilerGenerated]
		private float <ActiveLength>k__BackingField;

		// Token: 0x020001AB RID: 427
		[Serializable]
		public class CCDIKBone : FIK_IKBoneBase
		{
			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00062469 File Offset: 0x00060669
			// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00062471 File Offset: 0x00060671
			public FIK_CCDProcessor.CCDIKBone IKParent
			{
				[CompilerGenerated]
				get
				{
					return this.<IKParent>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IKParent>k__BackingField = value;
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0006247A File Offset: 0x0006067A
			// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00062482 File Offset: 0x00060682
			public FIK_CCDProcessor.CCDIKBone IKChild
			{
				[CompilerGenerated]
				get
				{
					return this.<IKChild>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IKChild>k__BackingField = value;
				}
			}

			// Token: 0x06000F10 RID: 3856 RVA: 0x0006248B File Offset: 0x0006068B
			public CCDIKBone(Transform t) : base(t)
			{
			}

			// Token: 0x06000F11 RID: 3857 RVA: 0x000624C0 File Offset: 0x000606C0
			public void Init(FIK_CCDProcessor.CCDIKBone child, FIK_CCDProcessor.CCDIKBone parent)
			{
				this.LastIKLocPosition = base.transform.localPosition;
				this.IKParent = parent;
				if (child != null)
				{
					this.SetChild(child);
				}
				this.IKChild = child;
			}

			// Token: 0x06000F12 RID: 3858 RVA: 0x000624EB File Offset: 0x000606EB
			public override void SetChild(FIK_IKBoneBase child)
			{
				base.SetChild(child);
			}

			// Token: 0x06000F13 RID: 3859 RVA: 0x000624F4 File Offset: 0x000606F4
			public void AngleLimiting()
			{
				Quaternion quaternion = Quaternion.Inverse(this.LastKeyLocalRotation) * base.transform.localRotation;
				Quaternion quaternion2 = quaternion;
				if (this.HingeLimits.VIsZero())
				{
					if (this.AngleLimit < 180f)
					{
						quaternion2 = this.LimitSpherical(quaternion2);
					}
					if (this.TwistAngleLimit < 180f)
					{
						quaternion2 = this.LimitZ(quaternion2);
					}
				}
				else
				{
					quaternion2 = this.LimitHinge(quaternion2);
				}
				if (quaternion2.QIsSame(quaternion))
				{
					return;
				}
				base.transform.localRotation = this.LastKeyLocalRotation * quaternion2;
			}

			// Token: 0x06000F14 RID: 3860 RVA: 0x00062588 File Offset: 0x00060788
			private Quaternion LimitSpherical(Quaternion rotation)
			{
				if (rotation.QIsZero())
				{
					return rotation;
				}
				Vector3 vector = rotation * this.ForwardOrientation;
				Quaternion rotation2 = Quaternion.RotateTowards(Quaternion.identity, Quaternion.FromToRotation(this.ForwardOrientation, vector), this.AngleLimit);
				return Quaternion.FromToRotation(vector, rotation2 * this.ForwardOrientation) * rotation;
			}

			// Token: 0x06000F15 RID: 3861 RVA: 0x000625E4 File Offset: 0x000607E4
			private Quaternion LimitZ(Quaternion currentRotation)
			{
				Vector3 vector = new Vector3(this.ForwardOrientation.y, this.ForwardOrientation.z, this.ForwardOrientation.x);
				Vector3 vector2 = currentRotation * this.ForwardOrientation;
				Vector3 toDirection = vector;
				Vector3.OrthoNormalize(ref vector2, ref toDirection);
				vector = currentRotation * vector;
				Vector3.OrthoNormalize(ref vector2, ref vector);
				Quaternion quaternion = Quaternion.FromToRotation(vector, toDirection) * currentRotation;
				if (this.TwistAngleLimit <= 0f)
				{
					return quaternion;
				}
				return Quaternion.RotateTowards(quaternion, currentRotation, this.TwistAngleLimit);
			}

			// Token: 0x06000F16 RID: 3862 RVA: 0x0006266C File Offset: 0x0006086C
			private Quaternion LimitHinge(Quaternion rotation)
			{
				Quaternion quaternion = Quaternion.FromToRotation(rotation * this.ForwardOrientation, this.ForwardOrientation) * rotation * Quaternion.Inverse(this.PreviousHingeRotation);
				float num = Quaternion.Angle(Quaternion.identity, quaternion);
				Vector3 vector = new Vector3(this.ForwardOrientation.z, this.ForwardOrientation.x, this.ForwardOrientation.y);
				Vector3 rhs = Vector3.Cross(vector, this.ForwardOrientation);
				if (Vector3.Dot(quaternion * vector, rhs) > 0f)
				{
					num = -num;
				}
				this.PreviousHingeAngle = Mathf.Clamp(this.PreviousHingeAngle + num, this.HingeLimits.x, this.HingeLimits.y);
				this.PreviousHingeRotation = Quaternion.AngleAxis(this.PreviousHingeAngle, this.ForwardOrientation);
				return this.PreviousHingeRotation;
			}

			// Token: 0x04000D33 RID: 3379
			[CompilerGenerated]
			private FIK_CCDProcessor.CCDIKBone <IKParent>k__BackingField;

			// Token: 0x04000D34 RID: 3380
			[CompilerGenerated]
			private FIK_CCDProcessor.CCDIKBone <IKChild>k__BackingField;

			// Token: 0x04000D35 RID: 3381
			[Range(0f, 180f)]
			public float AngleLimit = 45f;

			// Token: 0x04000D36 RID: 3382
			[Range(0f, 180f)]
			public float TwistAngleLimit = 5f;

			// Token: 0x04000D37 RID: 3383
			public Vector3 ForwardOrientation;

			// Token: 0x04000D38 RID: 3384
			public float FrameWorldLength = 1f;

			// Token: 0x04000D39 RID: 3385
			public Vector2 HingeLimits = Vector2.zero;

			// Token: 0x04000D3A RID: 3386
			public Quaternion PreviousHingeRotation;

			// Token: 0x04000D3B RID: 3387
			public float PreviousHingeAngle;

			// Token: 0x04000D3C RID: 3388
			public Vector3 LastIKLocPosition;

			// Token: 0x04000D3D RID: 3389
			public Quaternion LastIKLocRotation;
		}
	}
}
