using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000133 RID: 307
	public class EffectorOffset : OffsetModifier
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00056E7C File Offset: 0x0005507C
		protected override void OnModifyOffset()
		{
			this.ik.solver.leftHandEffector.maintainRelativePositionWeight = this.handsMaintainRelativePositionWeight;
			this.ik.solver.rightHandEffector.maintainRelativePositionWeight = this.handsMaintainRelativePositionWeight;
			this.ik.solver.bodyEffector.positionOffset += base.transform.rotation * this.bodyOffset * this.weight;
			this.ik.solver.leftShoulderEffector.positionOffset += base.transform.rotation * this.leftShoulderOffset * this.weight;
			this.ik.solver.rightShoulderEffector.positionOffset += base.transform.rotation * this.rightShoulderOffset * this.weight;
			this.ik.solver.leftThighEffector.positionOffset += base.transform.rotation * this.leftThighOffset * this.weight;
			this.ik.solver.rightThighEffector.positionOffset += base.transform.rotation * this.rightThighOffset * this.weight;
			this.ik.solver.leftHandEffector.positionOffset += base.transform.rotation * this.leftHandOffset * this.weight;
			this.ik.solver.rightHandEffector.positionOffset += base.transform.rotation * this.rightHandOffset * this.weight;
			this.ik.solver.leftFootEffector.positionOffset += base.transform.rotation * this.leftFootOffset * this.weight;
			this.ik.solver.rightFootEffector.positionOffset += base.transform.rotation * this.rightFootOffset * this.weight;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00057108 File Offset: 0x00055308
		public EffectorOffset()
		{
		}

		// Token: 0x04000A4B RID: 2635
		[Range(0f, 1f)]
		public float handsMaintainRelativePositionWeight;

		// Token: 0x04000A4C RID: 2636
		public Vector3 bodyOffset;

		// Token: 0x04000A4D RID: 2637
		public Vector3 leftShoulderOffset;

		// Token: 0x04000A4E RID: 2638
		public Vector3 rightShoulderOffset;

		// Token: 0x04000A4F RID: 2639
		public Vector3 leftThighOffset;

		// Token: 0x04000A50 RID: 2640
		public Vector3 rightThighOffset;

		// Token: 0x04000A51 RID: 2641
		public Vector3 leftHandOffset;

		// Token: 0x04000A52 RID: 2642
		public Vector3 rightHandOffset;

		// Token: 0x04000A53 RID: 2643
		public Vector3 leftFootOffset;

		// Token: 0x04000A54 RID: 2644
		public Vector3 rightFootOffset;
	}
}
