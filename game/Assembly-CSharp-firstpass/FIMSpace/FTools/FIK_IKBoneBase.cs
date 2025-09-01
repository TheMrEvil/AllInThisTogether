using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public abstract class FIK_IKBoneBase
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000139CF File Offset: 0x00011BCF
		// (set) Token: 0x06000282 RID: 642 RVA: 0x000139D7 File Offset: 0x00011BD7
		public FIK_IKBoneBase Child
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000139E0 File Offset: 0x00011BE0
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000139E8 File Offset: 0x00011BE8
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

		// Token: 0x06000285 RID: 645 RVA: 0x000139F4 File Offset: 0x00011BF4
		public FIK_IKBoneBase(Transform t)
		{
			this.transform = t;
			if (this.transform)
			{
				this.InitialLocalPosition = this.transform.localPosition;
				this.InitialLocalRotation = this.transform.localRotation;
				this.LastKeyLocalRotation = t.localRotation;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00013A6C File Offset: 0x00011C6C
		public virtual void SetChild(FIK_IKBoneBase child)
		{
			if (child == null)
			{
				return;
			}
			this.Child = child;
			this.sqrMagn = (child.transform.position - this.transform.position).sqrMagnitude;
			this.BoneLength = (child.transform.position - this.transform.position).sqrMagnitude;
		}

		// Token: 0x04000268 RID: 616
		[CompilerGenerated]
		private FIK_IKBoneBase <Child>k__BackingField;

		// Token: 0x04000269 RID: 617
		[CompilerGenerated]
		private Transform <transform>k__BackingField;

		// Token: 0x0400026A RID: 618
		public float sqrMagn = 0.1f;

		// Token: 0x0400026B RID: 619
		public float BoneLength = 0.1f;

		// Token: 0x0400026C RID: 620
		public float MotionWeight = 1f;

		// Token: 0x0400026D RID: 621
		public Vector3 InitialLocalPosition;

		// Token: 0x0400026E RID: 622
		public Quaternion InitialLocalRotation;

		// Token: 0x0400026F RID: 623
		public Quaternion LastKeyLocalRotation;
	}
}
