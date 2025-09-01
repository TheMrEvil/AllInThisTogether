using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class ConstraintPosition : Constraint
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0003AE1D File Offset: 0x0003901D
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
			this.transform.position = Vector3.Lerp(this.transform.position, this.position, this.weight);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0003AE5D File Offset: 0x0003905D
		public ConstraintPosition()
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0003AE65 File Offset: 0x00039065
		public ConstraintPosition(Transform transform)
		{
			this.transform = transform;
		}

		// Token: 0x040006DF RID: 1759
		public Vector3 position;
	}
}
