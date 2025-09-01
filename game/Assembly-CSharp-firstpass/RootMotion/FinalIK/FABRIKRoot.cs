using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DB RID: 219
	[HelpURL("http://www.root-motion.com/finalikdox/html/page7.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/FABRIK Root")]
	public class FABRIKRoot : IK
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0003E837 File Offset: 0x0003CA37
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page7.html");
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0003E843 File Offset: 0x0003CA43
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_f_a_b_r_i_k_root.html");
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0003E84F File Offset: 0x0003CA4F
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0003E85B File Offset: 0x0003CA5B
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0003E867 File Offset: 0x0003CA67
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0003E86F File Offset: 0x0003CA6F
		public FABRIKRoot()
		{
		}

		// Token: 0x04000766 RID: 1894
		public IKSolverFABRIKRoot solver = new IKSolverFABRIKRoot();
	}
}
