using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public abstract class FIK_ProcessorBase
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000138AC File Offset: 0x00011AAC
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000138B4 File Offset: 0x00011AB4
		public float fullLength
		{
			[CompilerGenerated]
			get
			{
				return this.<fullLength>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<fullLength>k__BackingField = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000138BD File Offset: 0x00011ABD
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000138C5 File Offset: 0x00011AC5
		public bool Initialized
		{
			[CompilerGenerated]
			get
			{
				return this.<Initialized>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Initialized>k__BackingField = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000138CE File Offset: 0x00011ACE
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000138D6 File Offset: 0x00011AD6
		public FIK_IKBoneBase[] Bones
		{
			[CompilerGenerated]
			get
			{
				return this.<Bones>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Bones>k__BackingField = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000138DF File Offset: 0x00011ADF
		public FIK_IKBoneBase StartBone
		{
			get
			{
				return this.Bones[0];
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000138E9 File Offset: 0x00011AE9
		public FIK_IKBoneBase EndBone
		{
			get
			{
				return this.Bones[this.Bones.Length - 1];
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000138FC File Offset: 0x00011AFC
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00013904 File Offset: 0x00011B04
		public Quaternion StartBoneRotationOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<StartBoneRotationOffset>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StartBoneRotationOffset>k__BackingField = value;
			}
		} = Quaternion.identity;

		// Token: 0x0600027C RID: 636 RVA: 0x0001390D File Offset: 0x00011B0D
		public virtual void Init(Transform root)
		{
			this.StartBoneRotationOffset = Quaternion.identity;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0001391C File Offset: 0x00011B1C
		public virtual void PreCalibrate()
		{
			FIK_IKBoneBase fik_IKBoneBase = this.Bones[0];
			while (fik_IKBoneBase.Child != null)
			{
				fik_IKBoneBase.transform.localRotation = fik_IKBoneBase.InitialLocalRotation;
				fik_IKBoneBase = fik_IKBoneBase.Child;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00013954 File Offset: 0x00011B54
		public virtual void Update()
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00013958 File Offset: 0x00011B58
		public static float EaseInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value * value * value + 2f) + start;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000139B1 File Offset: 0x00011BB1
		protected FIK_ProcessorBase()
		{
		}

		// Token: 0x0400025F RID: 607
		[Range(0f, 1f)]
		public float IKWeight = 1f;

		// Token: 0x04000260 RID: 608
		public Vector3 IKTargetPosition;

		// Token: 0x04000261 RID: 609
		public Quaternion IKTargetRotation;

		// Token: 0x04000262 RID: 610
		public Vector3 LastLocalDirection;

		// Token: 0x04000263 RID: 611
		public Vector3 LocalDirection;

		// Token: 0x04000264 RID: 612
		[CompilerGenerated]
		private float <fullLength>k__BackingField;

		// Token: 0x04000265 RID: 613
		[CompilerGenerated]
		private bool <Initialized>k__BackingField;

		// Token: 0x04000266 RID: 614
		[CompilerGenerated]
		private FIK_IKBoneBase[] <Bones>k__BackingField;

		// Token: 0x04000267 RID: 615
		[CompilerGenerated]
		private Quaternion <StartBoneRotationOffset>k__BackingField;
	}
}
