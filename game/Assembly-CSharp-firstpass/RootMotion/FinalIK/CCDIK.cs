using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D9 RID: 217
	[HelpURL("http://www.root-motion.com/finalikdox/html/page5.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/CCD IK")]
	public class CCDIK : IK
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x0003E7A1 File Offset: 0x0003C9A1
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page5.html");
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0003E7AD File Offset: 0x0003C9AD
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_c_c_d_i_k.html");
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0003E7B9 File Offset: 0x0003C9B9
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0003E7C5 File Offset: 0x0003C9C5
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0003E7D1 File Offset: 0x0003C9D1
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0003E7D9 File Offset: 0x0003C9D9
		public CCDIK()
		{
		}

		// Token: 0x04000764 RID: 1892
		public IKSolverCCD solver = new IKSolverCCD();
	}
}
