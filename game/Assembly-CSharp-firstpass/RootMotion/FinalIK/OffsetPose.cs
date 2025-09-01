using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011C RID: 284
	public class OffsetPose : MonoBehaviour
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x000528D4 File Offset: 0x00050AD4
		public void Apply(IKSolverFullBodyBiped solver, float weight)
		{
			for (int i = 0; i < this.effectorLinks.Length; i++)
			{
				this.effectorLinks[i].Apply(solver, weight, solver.GetRoot().rotation);
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00052910 File Offset: 0x00050B10
		public void Apply(IKSolverFullBodyBiped solver, float weight, Quaternion rotation)
		{
			for (int i = 0; i < this.effectorLinks.Length; i++)
			{
				this.effectorLinks[i].Apply(solver, weight, rotation);
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00052940 File Offset: 0x00050B40
		public OffsetPose()
		{
		}

		// Token: 0x040009AB RID: 2475
		public OffsetPose.EffectorLink[] effectorLinks = new OffsetPose.EffectorLink[0];

		// Token: 0x02000225 RID: 549
		[Serializable]
		public class EffectorLink
		{
			// Token: 0x0600118F RID: 4495 RVA: 0x0006D16C File Offset: 0x0006B36C
			public void Apply(IKSolverFullBodyBiped solver, float weight, Quaternion rotation)
			{
				solver.GetEffector(this.effector).positionOffset += rotation * this.offset * weight;
				Vector3 vector = solver.GetRoot().position + rotation * this.pin - solver.GetEffector(this.effector).bone.position;
				Vector3 vector2 = this.pinWeight * Mathf.Abs(weight);
				solver.GetEffector(this.effector).positionOffset = new Vector3(Mathf.Lerp(solver.GetEffector(this.effector).positionOffset.x, vector.x, vector2.x), Mathf.Lerp(solver.GetEffector(this.effector).positionOffset.y, vector.y, vector2.y), Mathf.Lerp(solver.GetEffector(this.effector).positionOffset.z, vector.z, vector2.z));
			}

			// Token: 0x06001190 RID: 4496 RVA: 0x0006D27C File Offset: 0x0006B47C
			public EffectorLink()
			{
			}

			// Token: 0x0400103F RID: 4159
			public FullBodyBipedEffector effector;

			// Token: 0x04001040 RID: 4160
			public Vector3 offset;

			// Token: 0x04001041 RID: 4161
			public Vector3 pin;

			// Token: 0x04001042 RID: 4162
			public Vector3 pinWeight;
		}
	}
}
