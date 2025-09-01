using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class ConstraintRotation : Constraint
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x0003AF5A File Offset: 0x0003915A
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
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.rotation, this.weight);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0003AF9A File Offset: 0x0003919A
		public ConstraintRotation()
		{
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0003AFA2 File Offset: 0x000391A2
		public ConstraintRotation(Transform transform)
		{
			this.transform = transform;
		}

		// Token: 0x040006E4 RID: 1764
		public Quaternion rotation;
	}
}
