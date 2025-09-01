using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E0 RID: 224
	[HelpURL("http://www.root-motion.com/finalikdox/html/page12.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Limb IK")]
	public class LimbIK : IK
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0003ED0C File Offset: 0x0003CF0C
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page12.html");
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0003ED18 File Offset: 0x0003CF18
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_limb_i_k.html");
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0003ED24 File Offset: 0x0003CF24
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003ED30 File Offset: 0x0003CF30
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003ED44 File Offset: 0x0003CF44
		public LimbIK()
		{
		}

		// Token: 0x0400076D RID: 1901
		public IKSolverLimb solver = new IKSolverLimb();
	}
}
