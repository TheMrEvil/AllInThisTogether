using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DF RID: 223
	[HelpURL("http://www.root-motion.com/finalikdox/html/page11.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Leg IK")]
	public class LegIK : IK
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x0003ECC1 File Offset: 0x0003CEC1
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page11.html");
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0003ECCD File Offset: 0x0003CECD
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_leg_i_k.html");
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003ECD9 File Offset: 0x0003CED9
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0003ECE5 File Offset: 0x0003CEE5
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0003ECF1 File Offset: 0x0003CEF1
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0003ECF9 File Offset: 0x0003CEF9
		public LegIK()
		{
		}

		// Token: 0x0400076C RID: 1900
		public IKSolverLeg solver = new IKSolverLeg();
	}
}
