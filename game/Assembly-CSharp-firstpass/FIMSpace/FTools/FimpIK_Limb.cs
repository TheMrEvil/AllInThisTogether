using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class FimpIK_Limb : FIK_ProcessorBase
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00015E90 File Offset: 0x00014090
		public override void Update()
		{
			if (!base.Initialized)
			{
				return;
			}
			this.Refresh();
			float num = this.IKPositionWeight * this.IKWeight;
			this.StartIKBone.sqrMagn = (this.MiddleIKBone.transform.position - this.StartIKBone.transform.position).sqrMagnitude;
			this.MiddleIKBone.sqrMagn = (this.EndIKBone.transform.position - this.MiddleIKBone.transform.position).sqrMagnitude;
			this.targetElbowNormal = this.GetDefaultFlexNormal();
			Vector3 vector = this.GetOrientationDirection(this.IKTargetPosition, this.targetElbowNormal);
			if (vector == Vector3.zero)
			{
				vector = this.MiddleIKBone.transform.position - this.StartIKBone.transform.position;
			}
			if (num > 0f)
			{
				Quaternion quaternion = this.StartIKBone.GetRotation(vector, this.targetElbowNormal);
				if (num < 1f)
				{
					quaternion = Quaternion.LerpUnclamped(this.StartIKBone.srcRotation, quaternion, num);
				}
				this.StartIKBone.transform.rotation = quaternion;
				Quaternion quaternion2 = this.MiddleIKBone.GetRotation(this.IKTargetPosition - this.MiddleIKBone.transform.position, this.MiddleIKBone.GetCurrentOrientationNormal());
				if (num < 1f)
				{
					quaternion2 = Quaternion.LerpUnclamped(this.MiddleIKBone.srcRotation, quaternion2, num);
				}
				this.MiddleIKBone.transform.rotation = quaternion2;
			}
			this.postIKAnimatorEndBoneRot = this.EndIKBone.transform.rotation;
			this.EndBoneRotation();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00016044 File Offset: 0x00014244
		private Vector3 GetAutomaticFlexNormal()
		{
			Vector3 currentOrientationNormal = this.StartIKBone.GetCurrentOrientationNormal();
			switch (this.AutoHintMode)
			{
			case FimpIK_Limb.FIK_HintMode.MiddleForward:
				return Vector3.LerpUnclamped(currentOrientationNormal.normalized, this.MiddleIKBone.srcRotation * this.MiddleIKBone.right, 0.5f);
			case FimpIK_Limb.FIK_HintMode.MiddleBack:
				return this.MiddleIKBone.srcRotation * -this.MiddleIKBone.right;
			case FimpIK_Limb.FIK_HintMode.OnGoal:
				return Vector3.LerpUnclamped(currentOrientationNormal, this.lateEndBoneRotation * this.EndIKBone.right, 0.5f);
			case FimpIK_Limb.FIK_HintMode.EndForward:
			{
				Vector3 vector = Vector3.Cross(this.MiddleIKBone.srcPosition + this.EndIKBone.srcRotation * this.EndIKBone.forward - this.StartIKBone.srcPosition, this.IKTargetPosition - this.StartIKBone.srcPosition);
				if (vector == Vector3.zero)
				{
					return currentOrientationNormal;
				}
				return vector;
			}
			default:
				return currentOrientationNormal;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001615A File Offset: 0x0001435A
		public void OnDrawGizmos()
		{
			bool initialized = base.Initialized;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00016163 File Offset: 0x00014363
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0001616B File Offset: 0x0001436B
		public Quaternion EndBoneMapping
		{
			[CompilerGenerated]
			get
			{
				return this.<EndBoneMapping>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<EndBoneMapping>k__BackingField = value;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00016174 File Offset: 0x00014374
		public virtual void SetRootReference(Transform mainParentTransform)
		{
			this.Root = mainParentTransform;
			this.EndBoneMapping = Quaternion.FromToRotation(this.EndIKBone.right, Vector3.right);
			this.EndBoneMapping *= Quaternion.FromToRotation(this.EndIKBone.up, Vector3.up);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000161C9 File Offset: 0x000143C9
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000161D1 File Offset: 0x000143D1
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

		// Token: 0x060002CC RID: 716 RVA: 0x000161DC File Offset: 0x000143DC
		public void RefreshLength()
		{
			this.ScaleReference = (this.StartIKBone.transform.position - this.MiddleIKBone.transform.position).magnitude;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001621C File Offset: 0x0001441C
		public void RefreshScaleReference()
		{
			this.ScaleReference = (this.StartIKBone.transform.position - this.MiddleIKBone.transform.position).magnitude;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001625C File Offset: 0x0001445C
		public float GetStretchValue(Vector3 targetPos)
		{
			float num = Mathf.Epsilon;
			num += (this.StartIKBone.transform.position - this.MiddleIKBone.transform.position).magnitude;
			num += (this.MiddleIKBone.transform.position - this.EndIKBone.transform.position).magnitude;
			return (this.StartIKBone.transform.position - targetPos).magnitude / num;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000162EF File Offset: 0x000144EF
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000162F7 File Offset: 0x000144F7
		public Transform Root
		{
			[CompilerGenerated]
			get
			{
				return this.<Root>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Root>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00016300 File Offset: 0x00014500
		public FimpIK_Limb.IKBone StartIKBone
		{
			get
			{
				return this.IKBones[0];
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0001630A File Offset: 0x0001450A
		public FimpIK_Limb.IKBone MiddleIKBone
		{
			get
			{
				return this.IKBones[1];
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00016314 File Offset: 0x00014514
		public FimpIK_Limb.IKBone EndIKBone
		{
			get
			{
				return this.IKBones[2];
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001631E File Offset: 0x0001451E
		public FimpIK_Limb.IKBone GetBone(int index)
		{
			return this.IKBones[index];
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00016328 File Offset: 0x00014528
		public int BonesCount
		{
			get
			{
				return this.IKBones.Length;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00016334 File Offset: 0x00014534
		public override void Init(Transform root)
		{
			if (base.Initialized)
			{
				return;
			}
			Vector3 lhs = Vector3.Cross(this.MiddleIKBone.transform.position - this.StartIKBone.transform.position, this.EndIKBone.transform.position - this.MiddleIKBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
			base.fullLength = 0f;
			this.StartIKBone.Init(root, this.MiddleIKBone.transform.position, this.targetElbowNormal);
			this.MiddleIKBone.Init(root, this.EndIKBone.transform.position, this.targetElbowNormal);
			this.EndIKBone.Init(root, this.EndIKBone.transform.position + (this.EndIKBone.transform.position - this.MiddleIKBone.transform.position), this.targetElbowNormal);
			base.fullLength = base.Bones[0].BoneLength + base.Bones[1].BoneLength;
			this.RefreshDefaultFlexNormal();
			if (this.EndIKBone.transform.parent != this.MiddleIKBone.transform)
			{
				this.everyIsChild = false;
			}
			else if (this.MiddleIKBone.transform.parent != this.StartIKBone.transform)
			{
				this.everyIsChild = false;
			}
			else
			{
				this.everyIsChild = true;
			}
			this.SetRootReference(root);
			if (Application.isPlaying)
			{
				base.Initialized = true;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000164E4 File Offset: 0x000146E4
		public void SetBones(Transform startBone, Transform midBone, Transform endBone)
		{
			this.IKBones = new FimpIK_Limb.IKBone[3];
			this.IKBones[0] = new FimpIK_Limb.IKBone(startBone);
			this.IKBones[1] = new FimpIK_Limb.IKBone(midBone);
			this.IKBones[2] = new FimpIK_Limb.IKBone(endBone);
			base.Bones = new FIK_IKBoneBase[]
			{
				this.IKBones[0],
				this.IKBones[1],
				this.IKBones[2]
			};
			this.IKBones[0].SetChild(this.IKBones[1]);
			this.IKBones[1].SetChild(this.IKBones[2]);
			this.IKTargetPosition = endBone.position;
			this.IKTargetRotation = endBone.rotation;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00016596 File Offset: 0x00014796
		public void SetBones(Transform startBone, Transform endBone)
		{
			this.SetBones(startBone, endBone.parent, endBone);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000165A8 File Offset: 0x000147A8
		protected virtual void Refresh()
		{
			this.RefreshAnimatorCoords();
			if (!this.everyIsChild)
			{
				this.StartIKBone.RefreshOrientations(this.MiddleIKBone.transform.position, this.targetElbowNormal);
				this.MiddleIKBone.RefreshOrientations(this.EndIKBone.transform.position, this.targetElbowNormal);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00016608 File Offset: 0x00014808
		protected virtual void EndBoneRotation()
		{
			float num = this.FootRotationWeight * this.IKWeight;
			if (num > 0f)
			{
				if (num < 1f)
				{
					this.EndIKBone.transform.rotation = Quaternion.SlerpUnclamped(this.postIKAnimatorEndBoneRot, this.IKTargetRotation * this.EndBoneMapping, num);
				}
				else
				{
					this.EndIKBone.transform.rotation = this.IKTargetRotation * this.EndBoneMapping;
				}
			}
			this.lateEndBoneRotation = this.EndIKBone.transform.rotation;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00016699 File Offset: 0x00014899
		public override void PreCalibrate()
		{
			base.PreCalibrate();
			this.RefreshScaleReference();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000166A7 File Offset: 0x000148A7
		public void RefreshAnimatorCoords()
		{
			this.StartIKBone.CaptureSourceAnimation();
			this.MiddleIKBone.CaptureSourceAnimation();
			this.EndIKBone.CaptureSourceAnimation();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000166CC File Offset: 0x000148CC
		private Vector3 GetDefaultFlexNormal()
		{
			if (this.ManualHintPositionWeight <= 0f)
			{
				return this.GetAutomaticFlexNormal();
			}
			if (this.ManualHintPositionWeight >= 1f)
			{
				return this.CalculateElbowNormalToPosition(this.IKManualHintPosition);
			}
			return Vector3.LerpUnclamped(this.GetAutomaticFlexNormal().normalized, this.CalculateElbowNormalToPosition(this.IKManualHintPosition), this.ManualHintPositionWeight);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001672C File Offset: 0x0001492C
		public Vector3 CalculateElbowNormalToPosition(Vector3 targetElbowPos)
		{
			return Vector3.Cross(targetElbowPos - this.StartIKBone.transform.position, this.EndIKBone.transform.position - this.StartIKBone.transform.position);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001677C File Offset: 0x0001497C
		public void RefreshDefaultFlexNormal()
		{
			Vector3 lhs = Vector3.Cross(this.MiddleIKBone.transform.position - this.StartIKBone.transform.position, this.EndIKBone.transform.position - this.MiddleIKBone.transform.position);
			if (lhs != Vector3.zero)
			{
				this.targetElbowNormal = lhs;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000167F0 File Offset: 0x000149F0
		private Vector3 GetOrientationDirection(Vector3 ikPosition, Vector3 orientationNormal)
		{
			Vector3 vector = ikPosition - this.StartIKBone.transform.position;
			if (vector == Vector3.zero)
			{
				return Vector3.zero;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			float num = Mathf.Sqrt(sqrMagnitude);
			float num2 = (sqrMagnitude + this.StartIKBone.sqrMagn - this.MiddleIKBone.sqrMagn) / 2f / num;
			float y = Mathf.Sqrt(Mathf.Clamp(this.StartIKBone.sqrMagn - num2 * num2, 0f, float.PositiveInfinity));
			Vector3 upwards = Vector3.Cross(vector / num, orientationNormal);
			return Quaternion.LookRotation(vector, upwards) * new Vector3(0f, y, num2);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000168A2 File Offset: 0x00014AA2
		public FimpIK_Limb()
		{
		}

		// Token: 0x040002A3 RID: 675
		[Tooltip("3-Bones limb array")]
		private FimpIK_Limb.IKBone[] IKBones;

		// Token: 0x040002A4 RID: 676
		[Tooltip("Blend value for goal position")]
		[Space(4f)]
		[Range(0f, 1f)]
		public float IKPositionWeight = 1f;

		// Token: 0x040002A5 RID: 677
		[Tooltip("Blend value for end bone rotation")]
		[Range(0f, 1f)]
		public float FootRotationWeight = 1f;

		// Token: 0x040002A6 RID: 678
		[Tooltip("Flex style algorithm for different limbs")]
		public FimpIK_Limb.FIK_HintMode AutoHintMode = FimpIK_Limb.FIK_HintMode.MiddleForward;

		// Token: 0x040002A7 RID: 679
		private Vector3 targetElbowNormal = Vector3.right;

		// Token: 0x040002A8 RID: 680
		private Quaternion lateEndBoneRotation;

		// Token: 0x040002A9 RID: 681
		private Quaternion postIKAnimatorEndBoneRot;

		// Token: 0x040002AA RID: 682
		[CompilerGenerated]
		private Quaternion <EndBoneMapping>k__BackingField;

		// Token: 0x040002AB RID: 683
		[CompilerGenerated]
		private float <ScaleReference>k__BackingField;

		// Token: 0x040002AC RID: 684
		[CompilerGenerated]
		private Transform <Root>k__BackingField;

		// Token: 0x040002AD RID: 685
		private bool everyIsChild;

		// Token: 0x040002AE RID: 686
		[HideInInspector]
		[Range(0f, 1f)]
		public float ManualHintPositionWeight;

		// Token: 0x040002AF RID: 687
		[HideInInspector]
		public Vector3 IKManualHintPosition = Vector3.zero;

		// Token: 0x020001AC RID: 428
		[Serializable]
		public class IKBone : FIK_IKBoneBase
		{
			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00062745 File Offset: 0x00060945
			// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0006274D File Offset: 0x0006094D
			public Vector3 right
			{
				[CompilerGenerated]
				get
				{
					return this.<right>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<right>k__BackingField = value;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00062756 File Offset: 0x00060956
			// (set) Token: 0x06000F1A RID: 3866 RVA: 0x0006275E File Offset: 0x0006095E
			public Vector3 up
			{
				[CompilerGenerated]
				get
				{
					return this.<up>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<up>k__BackingField = value;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00062767 File Offset: 0x00060967
			// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0006276F File Offset: 0x0006096F
			public Vector3 forward
			{
				[CompilerGenerated]
				get
				{
					return this.<forward>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<forward>k__BackingField = value;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00062778 File Offset: 0x00060978
			// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00062780 File Offset: 0x00060980
			public Vector3 srcPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<srcPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<srcPosition>k__BackingField = value;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00062789 File Offset: 0x00060989
			// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00062791 File Offset: 0x00060991
			public Quaternion srcRotation
			{
				[CompilerGenerated]
				get
				{
					return this.<srcRotation>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<srcRotation>k__BackingField = value;
				}
			}

			// Token: 0x06000F21 RID: 3873 RVA: 0x0006279A File Offset: 0x0006099A
			public IKBone(Transform t) : base(t)
			{
			}

			// Token: 0x06000F22 RID: 3874 RVA: 0x000627A4 File Offset: 0x000609A4
			public void Init(Transform root, Vector3 childPosition, Vector3 orientationNormal)
			{
				this.RefreshOrientations(childPosition, orientationNormal);
				this.sqrMagn = (childPosition - base.transform.position).sqrMagnitude;
				this.LastKeyLocalRotation = base.transform.localRotation;
				this.right = base.transform.InverseTransformDirection(root.right);
				this.up = base.transform.InverseTransformDirection(root.up);
				this.forward = base.transform.InverseTransformDirection(root.forward);
				this.CaptureSourceAnimation();
			}

			// Token: 0x06000F23 RID: 3875 RVA: 0x00062834 File Offset: 0x00060A34
			public void RefreshOrientations(Vector3 childPosition, Vector3 orientationNormal)
			{
				Quaternion rotation = Quaternion.LookRotation(childPosition - base.transform.position, orientationNormal);
				this.targetToLocalSpace = FimpIK_Limb.IKBone.RotationToLocal(base.transform.rotation, rotation);
				this.defaultLocalPoleNormal = Quaternion.Inverse(base.transform.rotation) * orientationNormal;
			}

			// Token: 0x06000F24 RID: 3876 RVA: 0x0006288C File Offset: 0x00060A8C
			public void CaptureSourceAnimation()
			{
				this.srcPosition = base.transform.position;
				this.srcRotation = base.transform.rotation;
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x000628B0 File Offset: 0x00060AB0
			public static Quaternion RotationToLocal(Quaternion parent, Quaternion rotation)
			{
				return Quaternion.Inverse(Quaternion.Inverse(parent) * rotation);
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x000628C3 File Offset: 0x00060AC3
			public Quaternion GetRotation(Vector3 direction, Vector3 orientationNormal)
			{
				return Quaternion.LookRotation(direction, orientationNormal) * this.targetToLocalSpace;
			}

			// Token: 0x06000F27 RID: 3879 RVA: 0x000628D7 File Offset: 0x00060AD7
			public Vector3 GetCurrentOrientationNormal()
			{
				return base.transform.rotation * this.defaultLocalPoleNormal;
			}

			// Token: 0x04000D3E RID: 3390
			[SerializeField]
			private Quaternion targetToLocalSpace;

			// Token: 0x04000D3F RID: 3391
			[SerializeField]
			private Vector3 defaultLocalPoleNormal;

			// Token: 0x04000D40 RID: 3392
			[CompilerGenerated]
			private Vector3 <right>k__BackingField;

			// Token: 0x04000D41 RID: 3393
			[CompilerGenerated]
			private Vector3 <up>k__BackingField;

			// Token: 0x04000D42 RID: 3394
			[CompilerGenerated]
			private Vector3 <forward>k__BackingField;

			// Token: 0x04000D43 RID: 3395
			[CompilerGenerated]
			private Vector3 <srcPosition>k__BackingField;

			// Token: 0x04000D44 RID: 3396
			[CompilerGenerated]
			private Quaternion <srcRotation>k__BackingField;
		}

		// Token: 0x020001AD RID: 429
		public enum FIK_HintMode
		{
			// Token: 0x04000D46 RID: 3398
			Default,
			// Token: 0x04000D47 RID: 3399
			MiddleForward,
			// Token: 0x04000D48 RID: 3400
			MiddleBack,
			// Token: 0x04000D49 RID: 3401
			OnGoal,
			// Token: 0x04000D4A RID: 3402
			EndForward
		}
	}
}
