using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public class ConstraintRotationOffset : Constraint
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x0003AFB4 File Offset: 0x000391B4
		public override void UpdateConstraint()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (!base.isValid)
			{
				return;
			}
			if (!this.initiated)
			{
				this.defaultLocalRotation = this.transform.localRotation;
				this.lastLocalRotation = this.transform.localRotation;
				this.initiated = true;
			}
			if (this.rotationChanged)
			{
				this.defaultLocalRotation = this.transform.localRotation;
			}
			this.transform.localRotation = this.defaultLocalRotation;
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.offset, this.weight);
			this.lastLocalRotation = this.transform.localRotation;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0003B06B File Offset: 0x0003926B
		public ConstraintRotationOffset()
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0003B073 File Offset: 0x00039273
		public ConstraintRotationOffset(Transform transform)
		{
			this.transform = transform;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0003B082 File Offset: 0x00039282
		private bool rotationChanged
		{
			get
			{
				return this.transform.localRotation != this.lastLocalRotation;
			}
		}

		// Token: 0x040006E5 RID: 1765
		public Quaternion offset;

		// Token: 0x040006E6 RID: 1766
		private Quaternion defaultRotation;

		// Token: 0x040006E7 RID: 1767
		private Quaternion defaultLocalRotation;

		// Token: 0x040006E8 RID: 1768
		private Quaternion lastLocalRotation;

		// Token: 0x040006E9 RID: 1769
		private Quaternion defaultTargetLocalRotation;

		// Token: 0x040006EA RID: 1770
		private bool initiated;
	}
}
