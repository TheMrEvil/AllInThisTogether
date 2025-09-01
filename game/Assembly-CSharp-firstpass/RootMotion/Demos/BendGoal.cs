using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000152 RID: 338
	public class BendGoal : MonoBehaviour
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00059E74 File Offset: 0x00058074
		private void Start()
		{
			Debug.Log("BendGoal is deprecated, you can now a bend goal from the custom inspector of the LimbIK component.");
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00059E80 File Offset: 0x00058080
		private void LateUpdate()
		{
			if (this.limbIK == null)
			{
				return;
			}
			this.limbIK.solver.SetBendGoalPosition(base.transform.position, this.weight);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00059EB2 File Offset: 0x000580B2
		public BendGoal()
		{
		}

		// Token: 0x04000AFB RID: 2811
		public LimbIK limbIK;

		// Token: 0x04000AFC RID: 2812
		[Range(0f, 1f)]
		public float weight = 1f;
	}
}
