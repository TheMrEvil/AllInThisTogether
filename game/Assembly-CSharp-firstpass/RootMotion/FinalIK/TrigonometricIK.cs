using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E2 RID: 226
	[HelpURL("http://www.root-motion.com/finalikdox/html/page15.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Trigonometric IK")]
	public class TrigonometricIK : IK
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0003EDA2 File Offset: 0x0003CFA2
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page15.html");
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0003EDAE File Offset: 0x0003CFAE
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_trigonometric_i_k.html");
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003EDBA File Offset: 0x0003CFBA
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003EDC6 File Offset: 0x0003CFC6
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003EDD2 File Offset: 0x0003CFD2
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003EDDA File Offset: 0x0003CFDA
		public TrigonometricIK()
		{
		}

		// Token: 0x0400076F RID: 1903
		public IKSolverTrigonometric solver = new IKSolverTrigonometric();
	}
}
