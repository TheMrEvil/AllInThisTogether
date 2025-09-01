using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D7 RID: 215
	[HelpURL("https://www.youtube.com/watch?v=wT8fViZpLmQ&index=3&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Aim IK")]
	public class AimIK : IK
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0003E6FF File Offset: 0x0003C8FF
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page1.html");
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0003E70B File Offset: 0x0003C90B
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_aim_i_k.html");
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0003E717 File Offset: 0x0003C917
		[ContextMenu("TUTORIAL VIDEO")]
		private void OpenSetupTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=wT8fViZpLmQ");
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0003E723 File Offset: 0x0003C923
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0003E72F File Offset: 0x0003C92F
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003E73B File Offset: 0x0003C93B
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0003E743 File Offset: 0x0003C943
		public AimIK()
		{
		}

		// Token: 0x04000762 RID: 1890
		public IKSolverAim solver = new IKSolverAim();
	}
}
