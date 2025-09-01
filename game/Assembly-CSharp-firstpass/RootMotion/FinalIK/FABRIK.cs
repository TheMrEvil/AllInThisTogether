using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DA RID: 218
	[HelpURL("http://www.root-motion.com/finalikdox/html/page6.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/FABRIK")]
	public class FABRIK : IK
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page6.html");
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_f_a_b_r_i_k.html");
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0003E804 File Offset: 0x0003CA04
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0003E810 File Offset: 0x0003CA10
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0003E81C File Offset: 0x0003CA1C
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0003E824 File Offset: 0x0003CA24
		public FABRIK()
		{
		}

		// Token: 0x04000765 RID: 1893
		public IKSolverFABRIK solver = new IKSolverFABRIK();
	}
}
