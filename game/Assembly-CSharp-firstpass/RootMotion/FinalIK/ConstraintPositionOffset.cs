using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public class ConstraintPositionOffset : Constraint
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0003AE74 File Offset: 0x00039074
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
				this.defaultLocalPosition = this.transform.localPosition;
				this.lastLocalPosition = this.transform.localPosition;
				this.initiated = true;
			}
			if (this.positionChanged)
			{
				this.defaultLocalPosition = this.transform.localPosition;
			}
			this.transform.localPosition = this.defaultLocalPosition;
			this.transform.position += this.offset * this.weight;
			this.lastLocalPosition = this.transform.localPosition;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003AF2B File Offset: 0x0003912B
		public ConstraintPositionOffset()
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0003AF33 File Offset: 0x00039133
		public ConstraintPositionOffset(Transform transform)
		{
			this.transform = transform;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0003AF42 File Offset: 0x00039142
		private bool positionChanged
		{
			get
			{
				return this.transform.localPosition != this.lastLocalPosition;
			}
		}

		// Token: 0x040006E0 RID: 1760
		public Vector3 offset;

		// Token: 0x040006E1 RID: 1761
		private Vector3 defaultLocalPosition;

		// Token: 0x040006E2 RID: 1762
		private Vector3 lastLocalPosition;

		// Token: 0x040006E3 RID: 1763
		private bool initiated;
	}
}
