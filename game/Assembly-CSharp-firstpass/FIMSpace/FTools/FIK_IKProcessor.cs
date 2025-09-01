using System;
using System.Runtime.CompilerServices;
using FIMSpace.AnimationTools;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class FIK_IKProcessor : FIK_ProcessorBase
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00013AD6 File Offset: 0x00011CD6
		public FIK_IKProcessor.IKBone StartIKBone
		{
			get
			{
				return this.IKBones[0];
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00013AE0 File Offset: 0x00011CE0
		public FIK_IKProcessor.IKBone MiddleIKBone
		{
			get
			{
				return this.IKBones[1];
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00013AEA File Offset: 0x00011CEA
		public FIK_IKProcessor.IKBone EndIKBone
		{
			get
			{
				return this.IKBones[2];
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00013AF4 File Offset: 0x00011CF4
		public FIK_IKProcessor.IKBone GetBone(int index)
		{
			return this.IKBones[index];
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00013AFE File Offset: 0x00011CFE
		public int BonesCount
		{
			get
			{
				return this.IKBones.Length;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00013B08 File Offset: 0x00011D08
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00013B10 File Offset: 0x00011D10
		public float ScaleReference
		{
			[CompilerGenerated]
			get
			{
				return this.<ScaleReference>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ScaleReference>k__BackingField = value;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00013B1C File Offset: 0x00011D1C
		public FIK_IKProcessor(Transform startBone, Transform midBone, Transform endBone)
		{
			this.SetBones(startBone, midBone, endBone);
			this.IKTargetPosition = endBone.position;
			this.IKTargetRotation = endBone.rotation;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public void SetBones(Transform startBone, Transform midBone, Transform endBone)
		{
			this.IKBones = new FIK_IKProcessor.IKBone[3];
			this.IKBones[0] = new FIK_IKProcessor.IKBone(startBone);
			this.IKBones[1] = new FIK_IKProcessor.IKBone(midBone);
			this.IKBones[2] = new FIK_IKProcessor.IKBone(endBone);
			base.Bones = new FIK_IKBoneBase[]
			{
				this.IKBones[0],
				this.IKBones[1],
				this.IKBones[2]
			};
			this.IKBones[0].SetChild(this.IKBones[1]);
			this.IKBones[1].SetChild(this.IKBones[2]);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00013C3E File Offset: 0x00011E3E
		public void SetBones(Transform startBone, Transform endBone)
		{
			this.SetBones(startBone, endBone.parent, endBone);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00013C4E File Offset: 0x00011E4E
		public override void PreCalibrate()
		{
			base.PreCalibrate();
			this.RefreshScaleReference();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00013C5C File Offset: 0x00011E5C
		public void RefreshScaleReference()
		{
			this.ScaleReference = (base.StartBone.transform.position - this.MiddleIKBone.transform.position).magnitude;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00013C9C File Offset: 0x00011E9C
		public override void Init(Transform root)
		{
			if (base.Initialized)
			{
				return;
			}
			this.rootTransform = root;
			Vector3 lhs = Vector3.Cross(this.MiddleIKBone.transform.position - base.StartBone.transform.position, base.EndBone.transform.position - this.MiddleIKBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
			base.fullLength = 0f;
			this.StartIKBone.Init(root, this.MiddleIKBone.transform.position, this.targetElbowNormal, this.UseEnsuredRotation);
			this.MiddleIKBone.Init(root, base.EndBone.transform.position, this.targetElbowNormal, this.UseEnsuredRotation);
			this.EndIKBone.Init(root, base.EndBone.transform.position + (base.EndBone.transform.position - this.MiddleIKBone.transform.position), this.targetElbowNormal, this.UseEnsuredRotation);
			base.fullLength = base.Bones[0].BoneLength + base.Bones[1].BoneLength;
			this.RefreshOrientationNormal();
			this.limbLengthRootScale = root.lossyScale.x;
			this.limbLength = Vector3.Distance(this.StartIKBone.transform.position, this.MiddleIKBone.transform.position);
			this.limbLength += Vector3.Distance(this.EndIKBone.transform.position, this.MiddleIKBone.transform.position);
			if (base.EndBone.transform.parent != this.MiddleIKBone.transform)
			{
				this.everyIsChild = false;
				this.limbMidLength = Vector3.Distance(this.EndIKBone.transform.position, this.MiddleIKBone.transform.position);
			}
			else if (this.MiddleIKBone.transform.parent != base.StartBone.transform)
			{
				this.everyIsChild = false;
			}
			else
			{
				this.everyIsChild = true;
			}
			if (this.AllowEditModeInit)
			{
				base.Initialized = true;
				return;
			}
			if (Application.isPlaying)
			{
				base.Initialized = true;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00013F05 File Offset: 0x00012105
		public void RefreshAnimatorCoords()
		{
			this.StartIKBone.CaptureSourceAnimation();
			this.MiddleIKBone.CaptureSourceAnimation();
			this.EndIKBone.CaptureSourceAnimation();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00013F28 File Offset: 0x00012128
		public override void Update()
		{
			if (!base.Initialized)
			{
				return;
			}
			this.RefreshAnimatorCoords();
			if (!this.everyIsChild)
			{
				this.MiddleIKBone.RefreshOrientations(base.EndBone.transform.position, this.targetElbowNormal);
			}
			float num = this.PositionWeight * this.IKWeight;
			base.StartBone.sqrMagn = (this.MiddleIKBone.transform.position - base.StartBone.transform.position).sqrMagnitude;
			this.MiddleIKBone.sqrMagn = (base.EndBone.transform.position - this.MiddleIKBone.transform.position).sqrMagnitude;
			this.targetElbowNormal = this.GetOrientationNormal();
			Vector3 vector = this.GetOrientationDirection(this.IKTargetPosition, this.targetElbowNormal);
			if (vector == Vector3.zero)
			{
				vector = this.MiddleIKBone.transform.position - base.StartBone.transform.position;
			}
			if (num > 0f)
			{
				Quaternion quaternion = this.StartIKBone.GetRotation(vector, this.targetElbowNormal) * base.StartBoneRotationOffset;
				if (num < 1f)
				{
					quaternion = Quaternion.LerpUnclamped(this.StartIKBone.srcRotation, quaternion, num);
				}
				base.StartBone.transform.rotation = quaternion;
				if (this.UseEnsuredRotation)
				{
					base.StartBone.transform.rotation = AnimationGenerateUtils.EnsureQuaternionContinuity(this.preS, base.StartBone.transform.rotation, false);
					this.preS = base.StartBone.transform.rotation;
				}
				Quaternion quaternion2 = this.MiddleIKBone.GetRotation(this.IKTargetPosition - this.MiddleIKBone.transform.position, this.MiddleIKBone.GetCurrentOrientationNormal());
				if (num < 1f)
				{
					quaternion2 = Quaternion.LerpUnclamped(this.MiddleIKBone.srcRotation, quaternion2, num);
				}
				this.MiddleIKBone.transform.rotation = quaternion2;
				if (this.UseEnsuredRotation)
				{
					this.MiddleIKBone.transform.rotation = AnimationGenerateUtils.EnsureQuaternionContinuity(this.preM, this.MiddleIKBone.transform.rotation, false);
					this.preM = this.MiddleIKBone.transform.rotation;
				}
			}
			this.postIKAnimatorEndBoneRot = base.EndBone.transform.rotation;
			float num2 = this.RotationWeight * this.IKWeight;
			if (num2 > 0f)
			{
				if (num2 < 1f)
				{
					base.EndBone.transform.rotation = Quaternion.LerpUnclamped(this.postIKAnimatorEndBoneRot, this.IKTargetRotation, num2);
				}
				else
				{
					base.EndBone.transform.rotation = this.IKTargetRotation;
				}
				if (this.UseEnsuredRotation)
				{
					base.EndBone.transform.rotation = AnimationGenerateUtils.EnsureQuaternionContinuity(this.preE, base.EndBone.transform.rotation, false);
					this.preE = base.EndBone.transform.rotation;
				}
			}
			this.lastEndBoneRotation = base.EndBone.transform.rotation;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00014260 File Offset: 0x00012460
		public float GetLimbLength()
		{
			if (this.rootTransform.lossyScale.x == 0f)
			{
				return 0f;
			}
			float num = this.rootTransform.lossyScale.x / this.limbLengthRootScale;
			if (!this.everyIsChild)
			{
				float num2 = this.limbMidLength * num - Vector3.Distance(this.EndIKBone.srcPosition, this.MiddleIKBone.srcPosition);
				return this.limbLength * num - num2;
			}
			return this.limbLength * num;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000142E2 File Offset: 0x000124E2
		public Vector3 GetHintDefaultPosition()
		{
			return this.MiddleIKBone.srcPosition + this.MiddleIKBone.srcRotation * this.StartIKBone.GetCurrentOrientationNormal();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00014310 File Offset: 0x00012510
		public float GetStretchValue(Vector3 targetPos)
		{
			return this.GetStretchValue((this.StartIKBone.srcPosition - targetPos).magnitude);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001433C File Offset: 0x0001253C
		public float GetStretchValue(float distance)
		{
			return distance / this.GetLimbLength();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00014348 File Offset: 0x00012548
		private Vector3 GetOrientationNormal()
		{
			if (this.ManualHintPositionWeight <= 0f)
			{
				return this.GetAutomaticElbowNormal();
			}
			if (this.ManualHintPositionWeight >= 1f)
			{
				return this.CalculateElbowNormalToPosition(this.IKManualHintPosition);
			}
			return Vector3.LerpUnclamped(this.GetAutomaticElbowNormal().normalized, this.CalculateElbowNormalToPosition(this.IKManualHintPosition), this.ManualHintPositionWeight);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000143A8 File Offset: 0x000125A8
		public Vector3 CalculateElbowNormalToPosition(Vector3 targetElbowPos)
		{
			return Vector3.Cross(targetElbowPos - base.StartBone.transform.position, base.EndBone.transform.position - base.StartBone.transform.position);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000143F8 File Offset: 0x000125F8
		public void RefreshOrientationNormal()
		{
			Vector3 lhs = Vector3.Cross(this.MiddleIKBone.transform.position - base.StartBone.transform.position, base.EndBone.transform.position - this.MiddleIKBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001446C File Offset: 0x0001266C
		private Vector3 GetOrientationDirection(Vector3 ikPosition, Vector3 orientationNormal)
		{
			Vector3 vector = ikPosition - base.StartBone.transform.position;
			if (vector == Vector3.zero)
			{
				return Vector3.zero;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			float num = Mathf.Sqrt(sqrMagnitude);
			float num2 = (sqrMagnitude + base.StartBone.sqrMagn - this.MiddleIKBone.sqrMagn) / 2f / num;
			float y = Mathf.Sqrt(Mathf.Clamp(base.StartBone.sqrMagn - num2 * num2, 0f, float.PositiveInfinity));
			Vector3 upwards = Vector3.Cross(vector / num, orientationNormal);
			return Quaternion.LookRotation(vector, upwards) * new Vector3(0f, y, num2);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00014520 File Offset: 0x00012720
		private Vector3 GetAutomaticElbowNormal()
		{
			Vector3 currentOrientationNormal = this.StartIKBone.GetCurrentOrientationNormal();
			switch (this.AutoHintMode)
			{
			case FIK_IKProcessor.FIK_HintMode.MiddleForward:
				return Vector3.LerpUnclamped(currentOrientationNormal.normalized, this.MiddleIKBone.srcRotation * this.MiddleIKBone.right, 0.5f);
			case FIK_IKProcessor.FIK_HintMode.MiddleBack:
				return this.MiddleIKBone.srcRotation * -this.MiddleIKBone.right;
			case FIK_IKProcessor.FIK_HintMode.OnGoal:
				return this.lastEndBoneRotation * this.EndIKBone.right;
			case FIK_IKProcessor.FIK_HintMode.EndForward:
			{
				Vector3 vector = Vector3.Cross(this.MiddleIKBone.srcPosition + this.EndIKBone.srcRotation * this.EndIKBone.forward - this.StartIKBone.srcPosition, this.IKTargetPosition - this.StartIKBone.srcPosition);
				if (vector == Vector3.zero)
				{
					return currentOrientationNormal;
				}
				return vector;
			}
			case FIK_IKProcessor.FIK_HintMode.Cross:
				return Vector3.Cross(this.MiddleIKBone.srcPosition - this.StartIKBone.srcPosition, this.EndIKBone.srcPosition - this.MiddleIKBone.srcPosition);
			default:
				return currentOrientationNormal;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001466B File Offset: 0x0001286B
		public void OnDrawGizmos()
		{
			bool initialized = base.Initialized;
		}

		// Token: 0x04000270 RID: 624
		[Space(4f)]
		[SerializeField]
		private FIK_IKProcessor.IKBone[] IKBones;

		// Token: 0x04000271 RID: 625
		[Space(4f)]
		[Range(0f, 1f)]
		public float PositionWeight = 1f;

		// Token: 0x04000272 RID: 626
		[Range(0f, 1f)]
		public float RotationWeight = 1f;

		// Token: 0x04000273 RID: 627
		[HideInInspector]
		public bool UseEnsuredRotation;

		// Token: 0x04000274 RID: 628
		public FIK_IKProcessor.FIK_HintMode AutoHintMode = FIK_IKProcessor.FIK_HintMode.MiddleForward;

		// Token: 0x04000275 RID: 629
		[Range(0f, 1f)]
		public float ManualHintPositionWeight;

		// Token: 0x04000276 RID: 630
		public Vector3 IKManualHintPosition = Vector3.zero;

		// Token: 0x04000277 RID: 631
		[CompilerGenerated]
		private float <ScaleReference>k__BackingField;

		// Token: 0x04000278 RID: 632
		private Transform rootTransform;

		// Token: 0x04000279 RID: 633
		private bool everyIsChild;

		// Token: 0x0400027A RID: 634
		private Vector3 targetElbowNormal = Vector3.right;

		// Token: 0x0400027B RID: 635
		private Quaternion lastEndBoneRotation;

		// Token: 0x0400027C RID: 636
		private Quaternion postIKAnimatorEndBoneRot;

		// Token: 0x0400027D RID: 637
		private Quaternion preS = Quaternion.identity;

		// Token: 0x0400027E RID: 638
		private Quaternion preM = Quaternion.identity;

		// Token: 0x0400027F RID: 639
		private Quaternion preE = Quaternion.identity;

		// Token: 0x04000280 RID: 640
		private float limbLengthRootScale;

		// Token: 0x04000281 RID: 641
		private float limbLength;

		// Token: 0x04000282 RID: 642
		private float limbMidLength;

		// Token: 0x04000283 RID: 643
		[NonSerialized]
		public bool AllowEditModeInit;

		// Token: 0x020001A7 RID: 423
		public enum FIK_HintMode
		{
			// Token: 0x04000D12 RID: 3346
			Default,
			// Token: 0x04000D13 RID: 3347
			MiddleForward,
			// Token: 0x04000D14 RID: 3348
			MiddleBack,
			// Token: 0x04000D15 RID: 3349
			OnGoal,
			// Token: 0x04000D16 RID: 3350
			EndForward,
			// Token: 0x04000D17 RID: 3351
			Cross
		}

		// Token: 0x020001A8 RID: 424
		[Serializable]
		public class IKBone : FIK_IKBoneBase
		{
			// Token: 0x06000EFC RID: 3836 RVA: 0x00061F71 File Offset: 0x00060171
			public IKBone(Transform t) : base(t)
			{
			}

			// Token: 0x06000EFD RID: 3837 RVA: 0x00061F88 File Offset: 0x00060188
			public void Init(Transform root, Vector3 childPosition, Vector3 orientationNormal, bool ensured)
			{
				this.RefreshOrientations(childPosition, orientationNormal);
				this.sqrMagn = (childPosition - base.transform.position).sqrMagnitude;
				this.LastKeyLocalRotation = base.transform.localRotation;
				this.right = base.transform.InverseTransformDirection(root.right);
				this.up = base.transform.InverseTransformDirection(root.up);
				this.forward = base.transform.InverseTransformDirection(root.forward);
				this.CaptureSourceAnimation();
			}

			// Token: 0x06000EFE RID: 3838 RVA: 0x00062018 File Offset: 0x00060218
			public void RefreshOrientations(Vector3 childPosition, Vector3 orientationNormal)
			{
				Quaternion quaternion = Quaternion.LookRotation(childPosition - base.transform.position, orientationNormal);
				if (this.ensured)
				{
					quaternion = AnimationGenerateUtils.EnsureQuaternionContinuity(this.pre, quaternion, false);
					this.pre = quaternion;
				}
				this.targetToLocalSpace = FIK_IKProcessor.IKBone.RotationToLocal(base.transform.rotation, quaternion);
				this.defaultLocalPoleNormal = Quaternion.Inverse(base.transform.rotation) * orientationNormal;
			}

			// Token: 0x06000EFF RID: 3839 RVA: 0x0006208D File Offset: 0x0006028D
			public void CaptureSourceAnimation()
			{
				this.srcPosition = base.transform.position;
				this.srcRotation = base.transform.rotation;
			}

			// Token: 0x06000F00 RID: 3840 RVA: 0x000620B1 File Offset: 0x000602B1
			public static Quaternion RotationToLocal(Quaternion parent, Quaternion rotation)
			{
				return Quaternion.Inverse(Quaternion.Inverse(parent) * rotation);
			}

			// Token: 0x06000F01 RID: 3841 RVA: 0x000620C4 File Offset: 0x000602C4
			public Quaternion GetRotation(Vector3 direction, Vector3 orientationNormal)
			{
				return Quaternion.LookRotation(direction, orientationNormal) * this.targetToLocalSpace;
			}

			// Token: 0x06000F02 RID: 3842 RVA: 0x000620D8 File Offset: 0x000602D8
			public Vector3 GetCurrentOrientationNormal()
			{
				return base.transform.rotation * this.defaultLocalPoleNormal;
			}

			// Token: 0x04000D18 RID: 3352
			[SerializeField]
			private Quaternion targetToLocalSpace;

			// Token: 0x04000D19 RID: 3353
			[SerializeField]
			private Vector3 defaultLocalPoleNormal;

			// Token: 0x04000D1A RID: 3354
			public Vector3 right;

			// Token: 0x04000D1B RID: 3355
			public Vector3 up;

			// Token: 0x04000D1C RID: 3356
			public Vector3 forward;

			// Token: 0x04000D1D RID: 3357
			public Vector3 srcPosition;

			// Token: 0x04000D1E RID: 3358
			public Quaternion srcRotation;

			// Token: 0x04000D1F RID: 3359
			private bool ensured;

			// Token: 0x04000D20 RID: 3360
			private Quaternion pre = Quaternion.identity;
		}
	}
}
