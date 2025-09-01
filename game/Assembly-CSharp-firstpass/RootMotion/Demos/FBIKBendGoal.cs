using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000136 RID: 310
	public class FBIKBendGoal : MonoBehaviour
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x000574EC File Offset: 0x000556EC
		private void Start()
		{
			Debug.Log("FBIKBendGoal is deprecated, you can now a bend goal from the custom inspector of the FullBodyBipedIK component.");
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000574F8 File Offset: 0x000556F8
		private void Update()
		{
			if (this.ik == null)
			{
				return;
			}
			this.ik.solver.GetBendConstraint(this.chain).bendGoal = base.transform;
			this.ik.solver.GetBendConstraint(this.chain).weight = this.weight;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00057556 File Offset: 0x00055756
		public FBIKBendGoal()
		{
		}

		// Token: 0x04000A68 RID: 2664
		public FullBodyBipedIK ik;

		// Token: 0x04000A69 RID: 2665
		public FullBodyBipedChain chain;

		// Token: 0x04000A6A RID: 2666
		public float weight;
	}
}
