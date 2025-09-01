using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E1 RID: 225
	[HelpURL("http://www.root-motion.com/finalikdox/html/page13.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Look At IK")]
	public class LookAtIK : IK
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0003ED57 File Offset: 0x0003CF57
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page13.html");
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003ED63 File Offset: 0x0003CF63
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_look_at_i_k.html");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003ED6F File Offset: 0x0003CF6F
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003ED7B File Offset: 0x0003CF7B
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003ED87 File Offset: 0x0003CF87
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003ED8F File Offset: 0x0003CF8F
		public LookAtIK()
		{
		}

		// Token: 0x0400076E RID: 1902
		public IKSolverLookAt solver = new IKSolverLookAt();
	}
}
