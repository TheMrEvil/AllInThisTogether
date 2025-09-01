using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class BipedLimbOrientations
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00037005 File Offset: 0x00035205
		public BipedLimbOrientations(BipedLimbOrientations.LimbOrientation leftArm, BipedLimbOrientations.LimbOrientation rightArm, BipedLimbOrientations.LimbOrientation leftLeg, BipedLimbOrientations.LimbOrientation rightLeg)
		{
			this.leftArm = leftArm;
			this.rightArm = rightArm;
			this.leftLeg = leftLeg;
			this.rightLeg = rightLeg;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0003702C File Offset: 0x0003522C
		public static BipedLimbOrientations UMA
		{
			get
			{
				return new BipedLimbOrientations(new BipedLimbOrientations.LimbOrientation(Vector3.forward, Vector3.forward, Vector3.forward), new BipedLimbOrientations.LimbOrientation(Vector3.forward, Vector3.forward, Vector3.back), new BipedLimbOrientations.LimbOrientation(Vector3.forward, Vector3.forward, Vector3.down), new BipedLimbOrientations.LimbOrientation(Vector3.forward, Vector3.forward, Vector3.down));
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00037090 File Offset: 0x00035290
		public static BipedLimbOrientations MaxBiped
		{
			get
			{
				return new BipedLimbOrientations(new BipedLimbOrientations.LimbOrientation(Vector3.down, Vector3.down, Vector3.down), new BipedLimbOrientations.LimbOrientation(Vector3.down, Vector3.down, Vector3.up), new BipedLimbOrientations.LimbOrientation(Vector3.up, Vector3.up, Vector3.back), new BipedLimbOrientations.LimbOrientation(Vector3.up, Vector3.up, Vector3.back));
			}
		}

		// Token: 0x0400067E RID: 1662
		public BipedLimbOrientations.LimbOrientation leftArm;

		// Token: 0x0400067F RID: 1663
		public BipedLimbOrientations.LimbOrientation rightArm;

		// Token: 0x04000680 RID: 1664
		public BipedLimbOrientations.LimbOrientation leftLeg;

		// Token: 0x04000681 RID: 1665
		public BipedLimbOrientations.LimbOrientation rightLeg;

		// Token: 0x020001E2 RID: 482
		[Serializable]
		public class LimbOrientation
		{
			// Token: 0x06001006 RID: 4102 RVA: 0x0006488A File Offset: 0x00062A8A
			public LimbOrientation(Vector3 upperBoneForwardAxis, Vector3 lowerBoneForwardAxis, Vector3 lastBoneLeftAxis)
			{
				this.upperBoneForwardAxis = upperBoneForwardAxis;
				this.lowerBoneForwardAxis = lowerBoneForwardAxis;
				this.lastBoneLeftAxis = lastBoneLeftAxis;
			}

			// Token: 0x04000E5B RID: 3675
			public Vector3 upperBoneForwardAxis;

			// Token: 0x04000E5C RID: 3676
			public Vector3 lowerBoneForwardAxis;

			// Token: 0x04000E5D RID: 3677
			public Vector3 lastBoneLeftAxis;
		}
	}
}
