using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class FimpIK_Arm
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000124F8 File Offset: 0x000106F8
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00012500 File Offset: 0x00010700
		public float FullLength
		{
			[CompilerGenerated]
			get
			{
				return this.<FullLength>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<FullLength>k__BackingField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00012509 File Offset: 0x00010709
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00012511 File Offset: 0x00010711
		public bool Initialized
		{
			[CompilerGenerated]
			get
			{
				return this.<Initialized>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Initialized>k__BackingField = value;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001251C File Offset: 0x0001071C
		public void PreCalibrate(float blend = 1f)
		{
			FimpIK_Arm.IKBone ikbone = this.IKBones[0];
			if (blend >= 1f)
			{
				while (ikbone != null)
				{
					ikbone.transform.localRotation = ikbone.InitialLocalRotation;
					ikbone = ikbone.Child;
				}
			}
			else
			{
				while (ikbone != null)
				{
					ikbone.transform.localRotation = Quaternion.LerpUnclamped(ikbone.transform.localRotation, ikbone.InitialLocalRotation, blend);
					ikbone = ikbone.Child;
				}
			}
			this.RefreshScaleReference();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001258B File Offset: 0x0001078B
		public void User_SmoothIKBlend(float target, float duration, float delta, float maxSpeed = 1000f)
		{
			this.IKWeight = Mathf.SmoothDamp(this.IKWeight, target, ref this.sd_ikBlend, duration, maxSpeed, delta);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000125A9 File Offset: 0x000107A9
		public void User_SmoothPositionTowards(Vector3 newIKPos, float duration, float delta, float maxSpeed = 1000f)
		{
			this.IKTargetPosition = Vector3.SmoothDamp(this.IKTargetPosition, newIKPos, ref this.sd_ikTargetPosition, duration, maxSpeed, delta);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000125C7 File Offset: 0x000107C7
		public Vector3 GetHintDefaultPosition()
		{
			return this.ForeArmIKBone.transform.position + this.ForeArmIKBone.transform.rotation * this.UpperArmIKBone.GetDefaultPoleNormal();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000125FE File Offset: 0x000107FE
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00012606 File Offset: 0x00010806
		public Vector3 TargetElbowNormal
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetElbowNormal>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TargetElbowNormal>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0001260F File Offset: 0x0001080F
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00012617 File Offset: 0x00010817
		public Quaternion UpperarmRotationOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<UpperarmRotationOffset>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UpperarmRotationOffset>k__BackingField = value;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00012620 File Offset: 0x00010820
		public void Update()
		{
			if (!this.Initialized)
			{
				return;
			}
			this.CalculateLimbLength();
			this.Refresh();
			this.ComputeShoulder(this.IKTargetPosition);
			Vector3 vector = this.IKTargetPosition;
			if (this.MaxStretching < 1.2f)
			{
				this.CalculateLimbLength();
				if (this.GetStretchValue(vector) > this.MaxStretching)
				{
					float d = this.MaxStretching * this.limbLength;
					vector = this.UpperArmIKBone.transform.position + (vector - this.UpperArmIKBone.transform.position).normalized * d;
				}
			}
			float num = this.IKPositionWeight * this.IKWeight * this._internalIKWeight;
			this.UpperArmIKBone.sqrMagn = (this.ForeArmIKBone.transform.position - this.UpperArmIKBone.transform.position).sqrMagnitude;
			this.ForeArmIKBone.sqrMagn = (this.HandIKBone.transform.position - this.ForeArmIKBone.transform.position).sqrMagnitude;
			this.TargetElbowNormal = this.GetDefaultFlexNormal();
			Vector3 vector2 = this.GetOrientationDirection(vector, this.TargetElbowNormal);
			if (vector2 == Vector3.zero)
			{
				vector2 = this.ForeArmIKBone.transform.position - this.UpperArmIKBone.transform.position;
			}
			if (num > 0f)
			{
				Quaternion quaternion = this.UpperArmIKBone.GetRotation(vector2, this.TargetElbowNormal) * this.UpperarmRotationOffset;
				if (num < 1f)
				{
					quaternion = Quaternion.LerpUnclamped(this.UpperArmIKBone.transform.rotation, quaternion, num);
				}
				this.UpperArmIKBone.transform.rotation = quaternion;
				Quaternion quaternion2 = this.ForeArmIKBone.GetRotation(vector - this.ForeArmIKBone.transform.position, this.ForeArmIKBone.GetCurrentOrientationNormal());
				if (num < 1f)
				{
					quaternion2 = Quaternion.LerpUnclamped(this.ForeArmIKBone.transform.rotation, quaternion2, num);
				}
				this.ForeArmIKBone.transform.rotation = quaternion2;
			}
			this.HandBoneRotation();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001285C File Offset: 0x00010A5C
		private Vector3 GetAutomaticFlexNormal()
		{
			Vector3 vector = this.UpperArmIKBone.GetCurrentOrientationNormal();
			if (this.ikCustomHintOffset != Vector3.zero)
			{
				vector = (vector + this.ikCustomHintOffset).normalized;
			}
			switch (this.AutoHintMode)
			{
			case FimpIK_Arm.FIK_HintMode.MiddleForward:
				return Vector3.LerpUnclamped(vector.normalized, this.ForeArmIKBone.srcRotation * this.ForeArmIKBone.forward, 0.5f);
			case FimpIK_Arm.FIK_HintMode.MiddleBack:
				return this.ForeArmIKBone.srcRotation * -this.ForeArmIKBone.right + this.ikCustomHintOffset;
			case FimpIK_Arm.FIK_HintMode.OnGoal:
				return Vector3.LerpUnclamped(vector, this.IKTargetRotation * this.HandIKBone.right, 0.5f);
			case FimpIK_Arm.FIK_HintMode.EndForward:
			{
				Vector3 vector2 = Vector3.Cross(this.ForeArmIKBone.srcPosition + this.HandIKBone.srcRotation * this.HandIKBone.forward * (this.MirrorMaths ? -1f : 1f) - this.UpperArmIKBone.srcPosition, this.IKTargetPosition - this.UpperArmIKBone.srcPosition);
				if (vector2 == Vector3.zero)
				{
					return vector;
				}
				return vector2;
			}
			default:
				return vector;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000129BD File Offset: 0x00010BBD
		public void OnDrawGizmos()
		{
			bool initialized = this.Initialized;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000129C6 File Offset: 0x00010BC6
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000129CE File Offset: 0x00010BCE
		public Quaternion HandIKBoneMapping
		{
			[CompilerGenerated]
			get
			{
				return this.<HandIKBoneMapping>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<HandIKBoneMapping>k__BackingField = value;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000129D7 File Offset: 0x00010BD7
		public void SetCustomIKRotationMappingOffset(Quaternion mappingCorrection)
		{
			this.HandIKBoneMapping = mappingCorrection;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000129E0 File Offset: 0x00010BE0
		public virtual void SetRootReference(Transform mainParentTransform)
		{
			this.Root = mainParentTransform;
			Quaternion rotation = this.Root.transform.rotation;
			this.Root.transform.rotation = Quaternion.identity;
			Vector3 normalized = (this.HandIKBone.transform.position - this.ForeArmIKBone.transform.position).normalized;
			Vector3 lhs = this.HandIKBone.transform.InverseTransformDirection(normalized);
			Vector3 forward = mainParentTransform.forward;
			Vector3 fromDirection = Vector3.Cross(lhs, forward);
			Vector3 normalized2 = (this.ShoulderIKBone.transform.position - this.ShoulderIKBone.transform.parent.position).normalized;
			this.shoulderForward = this.ShoulderIKBone.transform.InverseTransformDirection(normalized2);
			this.HandIKBoneMapping = Quaternion.FromToRotation(forward, Vector3.right);
			this.HandIKBoneMapping *= Quaternion.FromToRotation(fromDirection, Vector3.up);
			this.shoulderRotate = new UniRotateBone(this.ShoulderTransform, mainParentTransform);
			this.Root.transform.rotation = rotation;
			this.initHandRootSpaceFlatTowards = this.Root.InverseTransformPoint(this.HandTransform.position);
			this.initHandRootSpaceFlatTowards.y = 0f;
			this.initHandRootSpaceFlatTowards.Normalize();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00012B3C File Offset: 0x00010D3C
		public void SetCustomIKRotation(Quaternion rotation, float blend = 1f, bool fromDefault = false)
		{
			if (blend == 1f)
			{
				if (this.UseRotationMapping)
				{
					this.IKTargetRotation = rotation * this.HandIKBoneMapping;
					return;
				}
				this.IKTargetRotation = rotation;
				return;
			}
			else if (this.UseRotationMapping)
			{
				if (fromDefault)
				{
					this.IKTargetRotation = Quaternion.LerpUnclamped(this.IKTargetRotation, rotation * this.HandIKBoneMapping, blend);
					return;
				}
				this.IKTargetRotation = Quaternion.LerpUnclamped(rotation, rotation * this.HandIKBoneMapping, blend);
				return;
			}
			else
			{
				if (fromDefault)
				{
					this.IKTargetRotation = Quaternion.LerpUnclamped(this.IKTargetRotation, rotation, blend);
					return;
				}
				this.IKTargetRotation = Quaternion.LerpUnclamped(rotation, rotation, blend);
				return;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00012BE0 File Offset: 0x00010DE0
		public void CaptureKeyframeAnimation()
		{
			this.shoulderRotate.CaptureKeyframeAnimation();
			for (FimpIK_Arm.IKBone ikbone = this.IKBones[0]; ikbone != null; ikbone = ikbone.Child)
			{
				ikbone.CaptureSourceAnimation();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00012C13 File Offset: 0x00010E13
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00012C1B File Offset: 0x00010E1B
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

		// Token: 0x0600024C RID: 588 RVA: 0x00012C24 File Offset: 0x00010E24
		public void RefreshLength()
		{
			this.ScaleReference = (this.UpperArmIKBone.transform.position - this.ForeArmIKBone.transform.position).magnitude;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00012C64 File Offset: 0x00010E64
		public void RefreshScaleReference()
		{
			this.ScaleReference = (this.UpperArmIKBone.transform.position - this.ForeArmIKBone.transform.position).magnitude;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00012CA4 File Offset: 0x00010EA4
		public float GetStretchValue(Vector3 targetPos)
		{
			return (this.UpperArmIKBone.transform.position - targetPos).magnitude / this.limbLength;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00012CD8 File Offset: 0x00010ED8
		public float GetStretchValueSrc(Vector3 targetPos)
		{
			return (this.UpperArmIKBone.srcPosition - targetPos).magnitude / this.limbLength;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00012D08 File Offset: 0x00010F08
		protected virtual void CalculateLimbLength()
		{
			this.limbLength = Mathf.Epsilon;
			this.limbLength += (this.UpperArmIKBone.transform.position - this.ForeArmIKBone.transform.position).magnitude;
			this.limbLength += (this.ForeArmIKBone.transform.position - this.HandIKBone.transform.position).magnitude;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00012D94 File Offset: 0x00010F94
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00012D9C File Offset: 0x00010F9C
		public bool PreventShoulderThirdQuat
		{
			[CompilerGenerated]
			get
			{
				return this.<PreventShoulderThirdQuat>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PreventShoulderThirdQuat>k__BackingField = value;
			}
		} = true;

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00012DA5 File Offset: 0x00010FA5
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00012DAD File Offset: 0x00010FAD
		public float ShoulderSensitivity
		{
			[CompilerGenerated]
			get
			{
				return this.<ShoulderSensitivity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShoulderSensitivity>k__BackingField = value;
			}
		} = 0.75f;

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00012DB6 File Offset: 0x00010FB6
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00012DBE File Offset: 0x00010FBE
		public float PreventShoulderThirdQuatFactor
		{
			[CompilerGenerated]
			get
			{
				return this.<PreventShoulderThirdQuatFactor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PreventShoulderThirdQuatFactor>k__BackingField = value;
			}
		} = 0.01f;

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00012DC7 File Offset: 0x00010FC7
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00012DCF File Offset: 0x00010FCF
		public float limbLength
		{
			[CompilerGenerated]
			get
			{
				return this.<limbLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<limbLength>k__BackingField = value;
			}
		} = 0.1f;

		// Token: 0x06000259 RID: 601 RVA: 0x00012DD8 File Offset: 0x00010FD8
		private void ComputeShoulder(Vector3 finalIKPos)
		{
			if (!this.Initialized)
			{
				return;
			}
			if (this.ShoulderBlend <= 0f)
			{
				return;
			}
			Vector3 direction = finalIKPos - this.shoulderRotate.transform.position;
			Quaternion rotation2;
			if (this.Root)
			{
				Quaternion rotation = this.shoulderRotate.transform.rotation;
				Vector3 vector = -Quaternion.FromToRotation(this.Root.InverseTransformDirection(direction).normalized, this.initHandRootSpaceFlatTowards).eulerAngles;
				this.shoulderRotate.RotateXBy(vector.x);
				this.shoulderRotate.RotateYBy(vector.y);
				this.shoulderRotate.RotateZBy(vector.z);
				rotation2 = this.shoulderRotate.transform.rotation;
				this.shoulderRotate.transform.rotation = rotation;
			}
			else
			{
				rotation2 = this.ShoulderIKBone.GetRotation(direction.normalized, this.ShoulderIKBone.srcRotation * this.shoulderRotate.upReference);
			}
			float num = this.IKWeight * this.ShoulderBlend;
			float num2 = this.GetStretchValue(finalIKPos);
			num2 *= 0.85f;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			num *= Mathf.InverseLerp(0.6f, 1f, num2) * 0.9f;
			this.ShoulderIKBone.transform.rotation = Quaternion.Slerp(this.shoulderRotate.transform.rotation, rotation2, num);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00012F5A File Offset: 0x0001115A
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00012F62 File Offset: 0x00011162
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00012F6B File Offset: 0x0001116B
		public FimpIK_Arm.IKBone ShoulderIKBone
		{
			get
			{
				return this.IKBones[0];
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00012F75 File Offset: 0x00011175
		public FimpIK_Arm.IKBone UpperArmIKBone
		{
			get
			{
				return this.IKBones[1];
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00012F7F File Offset: 0x0001117F
		public FimpIK_Arm.IKBone ForeArmIKBone
		{
			get
			{
				return this.IKBones[2];
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00012F89 File Offset: 0x00011189
		public FimpIK_Arm.IKBone HandIKBone
		{
			get
			{
				return this.IKBones[3];
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00012F93 File Offset: 0x00011193
		public FimpIK_Arm.IKBone GetBone(int index)
		{
			return this.IKBones[index];
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00012F9D File Offset: 0x0001119D
		public int BonesCount
		{
			get
			{
				return this.IKBones.Length;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00012FA8 File Offset: 0x000111A8
		public void Init(Transform root)
		{
			if (this.Initialized)
			{
				return;
			}
			if (this.IKBones == null)
			{
				return;
			}
			if (this.IKBones.Length == 0)
			{
				this.SetBones(this.ShoulderTransform, this.UpperarmTransform, this.LowerarmTransform, this.HandTransform);
			}
			this.UpperarmRotationOffset = Quaternion.identity;
			this.TargetElbowNormal = Vector3.right;
			Vector3 vector = Vector3.Cross(this.ForeArmIKBone.transform.position - this.UpperArmIKBone.transform.position, this.HandIKBone.transform.position - this.ForeArmIKBone.transform.position);
			if (vector != Vector3.zero)
			{
				this.TargetElbowNormal = vector;
			}
			this.FullLength = 0f;
			this.ShoulderIKBone.Init(root, this.UpperArmIKBone.transform.position, this.TargetElbowNormal);
			this.UpperArmIKBone.Init(root, this.ForeArmIKBone.transform.position, this.TargetElbowNormal);
			this.ForeArmIKBone.Init(root, this.HandIKBone.transform.position, this.TargetElbowNormal);
			this.HandIKBone.Init(root, this.HandIKBone.transform.position + (this.HandIKBone.transform.position - this.ForeArmIKBone.transform.position), this.TargetElbowNormal);
			this.FullLength = this.IKBones[1].BoneLength + this.IKBones[2].BoneLength;
			this.RefreshDefaultFlexNormal();
			if (this.HandIKBone.transform.parent != this.ForeArmIKBone.transform)
			{
				this.everyIsChild = false;
			}
			else if (this.ForeArmIKBone.transform.parent != this.UpperArmIKBone.transform)
			{
				this.everyIsChild = false;
			}
			else
			{
				this.everyIsChild = true;
			}
			this.ChestIKBone = new FimpIK_Arm.IKBone(this.ShoulderIKBone.transform.parent);
			this.ChestIKBone.Init(root, this.ShoulderIKBone.transform.position, this.TargetElbowNormal);
			this.SetRootReference(root);
			this.HandMiddleOffset = Vector3.zero;
			if (this.HandIKBone.transform.childCount > 0)
			{
				this.HandMiddleOffset = this.HandIKBone.transform.GetChild(0).position;
				for (int i = 1; i < this.HandIKBone.transform.childCount; i++)
				{
					this.HandMiddleOffset = Vector3.Lerp(this.HandMiddleOffset, this.HandIKBone.transform.GetChild(i).position, 0.5f);
				}
				this.HandMiddleOffset = Vector3.Lerp(this.HandMiddleOffset, this.HandIKBone.transform.position, 0.4f);
				this.HandMiddleOffset = this.HandIKBone.transform.InverseTransformPoint(this.HandMiddleOffset);
			}
			this.Initialized = true;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000132BC File Offset: 0x000114BC
		public void SetBones(Transform shoulder, Transform upperArm, Transform forearm, Transform hand)
		{
			if (upperArm == null || forearm == null || hand == null)
			{
				return;
			}
			this.ShoulderTransform = shoulder;
			this.UpperarmTransform = upperArm;
			this.LowerarmTransform = forearm;
			this.HandTransform = hand;
			int num = 0;
			if (shoulder == null)
			{
				this.IKBones = new FimpIK_Arm.IKBone[3];
			}
			else
			{
				this.IKBones = new FimpIK_Arm.IKBone[4];
				this.IKBones[0] = new FimpIK_Arm.IKBone(shoulder);
				num = 1;
			}
			this.IKBones[num] = new FimpIK_Arm.IKBone(upperArm);
			this.IKBones[num + 1] = new FimpIK_Arm.IKBone(forearm);
			this.IKBones[num + 2] = new FimpIK_Arm.IKBone(hand);
			this.IKBones[0].SetChild(this.IKBones[1]);
			this.IKBones[1].SetChild(this.IKBones[2]);
			if (shoulder != null)
			{
				this.IKBones[2].SetChild(this.IKBones[3]);
			}
			this.IKTargetPosition = hand.position;
			this.IKTargetRotation = hand.rotation;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000133C9 File Offset: 0x000115C9
		public void SetBones()
		{
			this.SetBones(this.ShoulderTransform, this.UpperarmTransform, this.LowerarmTransform, this.HandTransform);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000133EC File Offset: 0x000115EC
		protected virtual void Refresh()
		{
			this.RefreshAnimatorCoords();
			if (!this.everyIsChild)
			{
				this.UpperArmIKBone.RefreshOrientations(this.ForeArmIKBone.transform.position, this.TargetElbowNormal);
				this.ForeArmIKBone.RefreshOrientations(this.HandIKBone.transform.position, this.TargetElbowNormal);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001344C File Offset: 0x0001164C
		protected virtual void HandBoneRotation()
		{
			float num = this.HandRotationWeight * this.IKWeight * this._internalIKWeight;
			if (num > 0f)
			{
				if (num < 1f)
				{
					this.HandIKBone.transform.rotation = Quaternion.LerpUnclamped(this.HandIKBone.transform.rotation, this.IKTargetRotation, num);
					return;
				}
				this.HandIKBone.transform.rotation = this.IKTargetRotation;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000134C1 File Offset: 0x000116C1
		public void RefreshAnimatorCoords()
		{
			if (this.ShoulderIKBone != null)
			{
				this.ShoulderIKBone.CaptureSourceAnimation();
			}
			this.UpperArmIKBone.CaptureSourceAnimation();
			this.ForeArmIKBone.CaptureSourceAnimation();
			this.HandIKBone.CaptureSourceAnimation();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000134F8 File Offset: 0x000116F8
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

		// Token: 0x06000269 RID: 617 RVA: 0x00013558 File Offset: 0x00011758
		public Vector3 CalculateElbowNormalToPosition(Vector3 targetElbowPos)
		{
			return Vector3.Cross(targetElbowPos - this.UpperArmIKBone.transform.position, this.HandIKBone.transform.position - this.UpperArmIKBone.transform.position);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000135A8 File Offset: 0x000117A8
		public void RefreshDefaultFlexNormal()
		{
			Vector3 vector = Vector3.Cross(this.ForeArmIKBone.transform.position - this.UpperArmIKBone.transform.position, this.HandIKBone.transform.position - this.ForeArmIKBone.transform.position);
			if (vector != Vector3.zero)
			{
				this.TargetElbowNormal = vector;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001361C File Offset: 0x0001181C
		private Vector3 GetOrientationDirection(Vector3 ikPosition, Vector3 orientationNormal)
		{
			Vector3 vector = ikPosition - this.UpperArmIKBone.transform.position;
			if (vector == Vector3.zero)
			{
				return Vector3.zero;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			float num = Mathf.Sqrt(sqrMagnitude);
			float num2 = (sqrMagnitude + this.UpperArmIKBone.sqrMagn - this.ForeArmIKBone.sqrMagn) / 2f / num;
			float y = Mathf.Sqrt(Mathf.Clamp(this.UpperArmIKBone.sqrMagn - num2 * num2, 0f, float.PositiveInfinity));
			Vector3 upwards = Vector3.Cross(vector / num, orientationNormal);
			return Quaternion.LookRotation(vector, upwards) * new Vector3(0f, y, num2);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000136CE File Offset: 0x000118CE
		public void IKHandRotationWeightFadeTo(float to, float duration, float delta)
		{
			this.HandRotationWeight = Mathf.SmoothDamp(this.HandRotationWeight, to, ref this.sd_targetIKRotation, duration, float.PositiveInfinity, delta);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000136F0 File Offset: 0x000118F0
		public bool IsCorrect
		{
			get
			{
				return this.Initialized && !(this.UpperarmTransform == null) && !(this.LowerarmTransform == null) && this.shoulderRotate != null && !(this.shoulderRotate.transform == null);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00013747 File Offset: 0x00011947
		public void IKHandPositionWeightFadeTo(float to, float duration, float delta)
		{
			this.IKPositionWeight = Mathf.SmoothDamp(this.IKPositionWeight, to, ref this.sd_positionWeight, duration, float.PositiveInfinity, delta);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00013768 File Offset: 0x00011968
		public Vector3 GetMiddleHandPosition(Vector3 tgt)
		{
			return tgt - Matrix4x4.TRS(this.IKTargetPosition, this.IKTargetRotation, this.HandIKBone.transform.lossyScale).MultiplyVector(this.HandMiddleOffset);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000137AC File Offset: 0x000119AC
		public Vector3 GetLimitedIKPosToMax(Vector3 targetIKPos, float lengthFactor = 1f)
		{
			Vector3 vector = targetIKPos - this.UpperArmIKBone.transform.position;
			return this.UpperArmIKBone.transform.position + vector.normalized * lengthFactor * this.limbLength;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013800 File Offset: 0x00011A00
		public FimpIK_Arm()
		{
		}

		// Token: 0x04000235 RID: 565
		private Vector3 lastIKBasePosition;

		// Token: 0x04000236 RID: 566
		private Quaternion lastIKBaseRotation;

		// Token: 0x04000237 RID: 567
		public Vector3 IKTargetPosition;

		// Token: 0x04000238 RID: 568
		public Quaternion IKTargetRotation;

		// Token: 0x04000239 RID: 569
		[CompilerGenerated]
		private float <FullLength>k__BackingField;

		// Token: 0x0400023A RID: 570
		[CompilerGenerated]
		private bool <Initialized>k__BackingField;

		// Token: 0x0400023B RID: 571
		private float sd_ikBlend;

		// Token: 0x0400023C RID: 572
		private Vector3 sd_ikTargetPosition = Vector3.zero;

		// Token: 0x0400023D RID: 573
		[NonSerialized]
		public float _internalIKWeight = 1f;

		// Token: 0x0400023E RID: 574
		[Range(0f, 1f)]
		public float IKWeight = 1f;

		// Token: 0x0400023F RID: 575
		[Tooltip("Blend value for goal position")]
		[Space(4f)]
		[Range(0f, 1f)]
		public float IKPositionWeight = 1f;

		// Token: 0x04000240 RID: 576
		[Tooltip("Blend value hand rotation")]
		[Range(0f, 1f)]
		public float HandRotationWeight = 1f;

		// Token: 0x04000241 RID: 577
		[Tooltip("Blend value for shoulder rotation")]
		[Range(0f, 1f)]
		public float ShoulderBlend = 1f;

		// Token: 0x04000242 RID: 578
		[Tooltip("Flex style algorithm for different limbs")]
		public FimpIK_Arm.FIK_HintMode AutoHintMode = FimpIK_Arm.FIK_HintMode.MiddleForward;

		// Token: 0x04000243 RID: 579
		[Tooltip("If left limb behaves wrong in comparison to right one")]
		public bool MirrorMaths;

		// Token: 0x04000244 RID: 580
		[FPD_Header("Bones References", 6f, 4f, 2)]
		public Transform ShoulderTransform;

		// Token: 0x04000245 RID: 581
		public Transform UpperarmTransform;

		// Token: 0x04000246 RID: 582
		public Transform LowerarmTransform;

		// Token: 0x04000247 RID: 583
		public Transform HandTransform;

		// Token: 0x04000248 RID: 584
		[SerializeField]
		[HideInInspector]
		private FimpIK_Arm.IKBone[] IKBones;

		// Token: 0x04000249 RID: 585
		[CompilerGenerated]
		private Vector3 <TargetElbowNormal>k__BackingField;

		// Token: 0x0400024A RID: 586
		[CompilerGenerated]
		private Quaternion <UpperarmRotationOffset>k__BackingField;

		// Token: 0x0400024B RID: 587
		[NonSerialized]
		public Vector3 ikCustomHintOffset = Vector3.zero;

		// Token: 0x0400024C RID: 588
		[CompilerGenerated]
		private Quaternion <HandIKBoneMapping>k__BackingField;

		// Token: 0x0400024D RID: 589
		[NonSerialized]
		public Vector3 HandMiddleOffset;

		// Token: 0x0400024E RID: 590
		private Vector3 shoulderForward;

		// Token: 0x0400024F RID: 591
		private UniRotateBone shoulderRotate;

		// Token: 0x04000250 RID: 592
		private Vector3 initHandRootSpaceFlatTowards;

		// Token: 0x04000251 RID: 593
		internal bool UseRotationMapping = true;

		// Token: 0x04000252 RID: 594
		[NonSerialized]
		public float MaxStretching = 1.2f;

		// Token: 0x04000253 RID: 595
		[CompilerGenerated]
		private float <ScaleReference>k__BackingField;

		// Token: 0x04000254 RID: 596
		[CompilerGenerated]
		private bool <PreventShoulderThirdQuat>k__BackingField;

		// Token: 0x04000255 RID: 597
		[CompilerGenerated]
		private float <ShoulderSensitivity>k__BackingField;

		// Token: 0x04000256 RID: 598
		[CompilerGenerated]
		private float <PreventShoulderThirdQuatFactor>k__BackingField;

		// Token: 0x04000257 RID: 599
		[CompilerGenerated]
		private float <limbLength>k__BackingField;

		// Token: 0x04000258 RID: 600
		[CompilerGenerated]
		private Transform <Root>k__BackingField;

		// Token: 0x04000259 RID: 601
		public FimpIK_Arm.IKBone ChestIKBone;

		// Token: 0x0400025A RID: 602
		private bool everyIsChild;

		// Token: 0x0400025B RID: 603
		[Range(0f, 1f)]
		public float ManualHintPositionWeight;

		// Token: 0x0400025C RID: 604
		[HideInInspector]
		public Vector3 IKManualHintPosition = Vector3.zero;

		// Token: 0x0400025D RID: 605
		private float sd_targetIKRotation;

		// Token: 0x0400025E RID: 606
		private float sd_positionWeight;

		// Token: 0x020001A5 RID: 421
		[Serializable]
		public class IKBone
		{
			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00061C88 File Offset: 0x0005FE88
			// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00061C90 File Offset: 0x0005FE90
			public FimpIK_Arm.IKBone Child
			{
				[CompilerGenerated]
				get
				{
					return this.<Child>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Child>k__BackingField = value;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00061C99 File Offset: 0x0005FE99
			// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x00061CA1 File Offset: 0x0005FEA1
			public Transform transform
			{
				[CompilerGenerated]
				get
				{
					return this.<transform>k__BackingField;
				}
				[CompilerGenerated]
				protected set
				{
					this.<transform>k__BackingField = value;
				}
			}

			// Token: 0x06000EE8 RID: 3816 RVA: 0x00061CAA File Offset: 0x0005FEAA
			public Vector3 GetDefaultPoleNormal()
			{
				return this.defaultLocalPoleNormal;
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00061CB2 File Offset: 0x0005FEB2
			// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00061CBA File Offset: 0x0005FEBA
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

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00061CC3 File Offset: 0x0005FEC3
			// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00061CCB File Offset: 0x0005FECB
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

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06000EED RID: 3821 RVA: 0x00061CD4 File Offset: 0x0005FED4
			// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00061CDC File Offset: 0x0005FEDC
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

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00061CE5 File Offset: 0x0005FEE5
			// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00061CED File Offset: 0x0005FEED
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

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00061CF6 File Offset: 0x0005FEF6
			// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x00061CFE File Offset: 0x0005FEFE
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

			// Token: 0x06000EF3 RID: 3827 RVA: 0x00061D08 File Offset: 0x0005FF08
			public IKBone(Transform t)
			{
				if (t == null)
				{
					return;
				}
				this.transform = t;
				this.InitialLocalPosition = this.transform.localPosition;
				this.InitialLocalRotation = this.transform.localRotation;
				this.LastKeyLocalRotation = t.localRotation;
			}

			// Token: 0x06000EF4 RID: 3828 RVA: 0x00061D7C File Offset: 0x0005FF7C
			public virtual void SetChild(FimpIK_Arm.IKBone child)
			{
				if (child.transform == null)
				{
					return;
				}
				this.Child = child;
				this.sqrMagn = (child.transform.position - this.transform.position).sqrMagnitude;
				this.BoneLength = (child.transform.position - this.transform.position).magnitude;
			}

			// Token: 0x06000EF5 RID: 3829 RVA: 0x00061DF1 File Offset: 0x0005FFF1
			public Vector3 Dir(Vector3 local)
			{
				return this.transform.TransformDirection(local);
			}

			// Token: 0x06000EF6 RID: 3830 RVA: 0x00061E00 File Offset: 0x00060000
			public void Init(Transform root, Vector3 childPosition, Vector3 orientationNormal)
			{
				this.RefreshOrientations(childPosition, orientationNormal);
				this.sqrMagn = (childPosition - this.transform.position).sqrMagnitude;
				this.LastKeyLocalRotation = this.transform.localRotation;
				this.right = this.transform.InverseTransformDirection(root.right);
				this.up = this.transform.InverseTransformDirection(root.up);
				this.forward = this.transform.InverseTransformDirection(root.forward);
				this.CaptureSourceAnimation();
			}

			// Token: 0x06000EF7 RID: 3831 RVA: 0x00061E90 File Offset: 0x00060090
			public void RefreshOrientations(Vector3 childPosition, Vector3 orientationNormal)
			{
				if (this.transform == null)
				{
					return;
				}
				Vector3 vector = childPosition - this.transform.position;
				Quaternion rotation;
				if (vector == Vector3.zero)
				{
					rotation = Quaternion.identity;
				}
				else
				{
					rotation = Quaternion.LookRotation(vector, orientationNormal);
				}
				this.targetToLocalSpace = FimpIK_Arm.IKBone.RotationToLocal(this.transform.rotation, rotation);
				this.defaultLocalPoleNormal = Quaternion.Inverse(this.transform.rotation) * orientationNormal;
			}

			// Token: 0x06000EF8 RID: 3832 RVA: 0x00061F0E File Offset: 0x0006010E
			public void CaptureSourceAnimation()
			{
				this.srcPosition = this.transform.position;
				this.srcRotation = this.transform.rotation;
			}

			// Token: 0x06000EF9 RID: 3833 RVA: 0x00061F32 File Offset: 0x00060132
			public static Quaternion RotationToLocal(Quaternion parent, Quaternion rotation)
			{
				return Quaternion.Inverse(Quaternion.Inverse(parent) * rotation);
			}

			// Token: 0x06000EFA RID: 3834 RVA: 0x00061F45 File Offset: 0x00060145
			public Quaternion GetRotation(Vector3 direction, Vector3 orientationNormal)
			{
				return Quaternion.LookRotation(direction, orientationNormal) * this.targetToLocalSpace;
			}

			// Token: 0x06000EFB RID: 3835 RVA: 0x00061F59 File Offset: 0x00060159
			public Vector3 GetCurrentOrientationNormal()
			{
				return this.transform.rotation * this.defaultLocalPoleNormal;
			}

			// Token: 0x04000CFC RID: 3324
			[CompilerGenerated]
			private FimpIK_Arm.IKBone <Child>k__BackingField;

			// Token: 0x04000CFD RID: 3325
			[CompilerGenerated]
			private Transform <transform>k__BackingField;

			// Token: 0x04000CFE RID: 3326
			public float sqrMagn = 0.1f;

			// Token: 0x04000CFF RID: 3327
			public float BoneLength = 0.1f;

			// Token: 0x04000D00 RID: 3328
			public float MotionWeight = 1f;

			// Token: 0x04000D01 RID: 3329
			public Vector3 InitialLocalPosition;

			// Token: 0x04000D02 RID: 3330
			public Quaternion InitialLocalRotation;

			// Token: 0x04000D03 RID: 3331
			public Quaternion LastKeyLocalRotation;

			// Token: 0x04000D04 RID: 3332
			[SerializeField]
			private Quaternion targetToLocalSpace;

			// Token: 0x04000D05 RID: 3333
			[SerializeField]
			private Vector3 defaultLocalPoleNormal;

			// Token: 0x04000D06 RID: 3334
			[CompilerGenerated]
			private Vector3 <right>k__BackingField;

			// Token: 0x04000D07 RID: 3335
			[CompilerGenerated]
			private Vector3 <up>k__BackingField;

			// Token: 0x04000D08 RID: 3336
			[CompilerGenerated]
			private Vector3 <forward>k__BackingField;

			// Token: 0x04000D09 RID: 3337
			[CompilerGenerated]
			private Vector3 <srcPosition>k__BackingField;

			// Token: 0x04000D0A RID: 3338
			[CompilerGenerated]
			private Quaternion <srcRotation>k__BackingField;
		}

		// Token: 0x020001A6 RID: 422
		public enum FIK_HintMode
		{
			// Token: 0x04000D0C RID: 3340
			Default,
			// Token: 0x04000D0D RID: 3341
			MiddleForward,
			// Token: 0x04000D0E RID: 3342
			MiddleBack,
			// Token: 0x04000D0F RID: 3343
			OnGoal,
			// Token: 0x04000D10 RID: 3344
			EndForward
		}
	}
}
