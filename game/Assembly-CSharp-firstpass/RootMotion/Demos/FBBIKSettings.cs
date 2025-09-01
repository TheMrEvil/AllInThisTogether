using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000135 RID: 309
	public class FBBIKSettings : MonoBehaviour
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x00057404 File Offset: 0x00055604
		public void UpdateSettings()
		{
			if (this.ik == null)
			{
				return;
			}
			this.leftArm.Apply(FullBodyBipedChain.LeftArm, this.ik.solver);
			this.rightArm.Apply(FullBodyBipedChain.RightArm, this.ik.solver);
			this.leftLeg.Apply(FullBodyBipedChain.LeftLeg, this.ik.solver);
			this.rightLeg.Apply(FullBodyBipedChain.RightLeg, this.ik.solver);
			this.ik.solver.chain[0].pin = this.rootPin;
			this.ik.solver.bodyEffector.effectChildNodes = this.bodyEffectChildNodes;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000574B4 File Offset: 0x000556B4
		private void Start()
		{
			Debug.Log("FBBIKSettings is deprecated, you can now edit all the settings from the custom inspector of the FullBodyBipedIK component.");
			this.UpdateSettings();
			if (this.disableAfterStart)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000574D5 File Offset: 0x000556D5
		private void Update()
		{
			this.UpdateSettings();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000574DD File Offset: 0x000556DD
		public FBBIKSettings()
		{
		}

		// Token: 0x04000A60 RID: 2656
		public FullBodyBipedIK ik;

		// Token: 0x04000A61 RID: 2657
		public bool disableAfterStart;

		// Token: 0x04000A62 RID: 2658
		public FBBIKSettings.Limb leftArm;

		// Token: 0x04000A63 RID: 2659
		public FBBIKSettings.Limb rightArm;

		// Token: 0x04000A64 RID: 2660
		public FBBIKSettings.Limb leftLeg;

		// Token: 0x04000A65 RID: 2661
		public FBBIKSettings.Limb rightLeg;

		// Token: 0x04000A66 RID: 2662
		public float rootPin;

		// Token: 0x04000A67 RID: 2663
		public bool bodyEffectChildNodes = true;

		// Token: 0x0200022F RID: 559
		[Serializable]
		public class Limb
		{
			// Token: 0x060011A2 RID: 4514 RVA: 0x0006D7AA File Offset: 0x0006B9AA
			public void Apply(FullBodyBipedChain chain, IKSolverFullBodyBiped solver)
			{
				solver.GetChain(chain).reachSmoothing = this.reachSmoothing;
				solver.GetEndEffector(chain).maintainRelativePositionWeight = this.maintainRelativePositionWeight;
				solver.GetLimbMapping(chain).weight = this.mappingWeight;
			}

			// Token: 0x060011A3 RID: 4515 RVA: 0x0006D7E2 File Offset: 0x0006B9E2
			public Limb()
			{
			}

			// Token: 0x04001082 RID: 4226
			public FBIKChain.Smoothing reachSmoothing;

			// Token: 0x04001083 RID: 4227
			public float maintainRelativePositionWeight;

			// Token: 0x04001084 RID: 4228
			public float mappingWeight = 1f;
		}
	}
}
