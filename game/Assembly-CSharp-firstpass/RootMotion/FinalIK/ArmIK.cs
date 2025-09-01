using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D8 RID: 216
	[HelpURL("http://www.root-motion.com/finalikdox/html/page2.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Arm IK")]
	public class ArmIK : IK
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0003E756 File Offset: 0x0003C956
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page2.html");
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003E762 File Offset: 0x0003C962
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_arm_i_k.html");
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003E76E File Offset: 0x0003C96E
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0003E77A File Offset: 0x0003C97A
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0003E786 File Offset: 0x0003C986
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0003E78E File Offset: 0x0003C98E
		public ArmIK()
		{
		}

		// Token: 0x04000763 RID: 1891
		public IKSolverArm solver = new IKSolverArm();
	}
}
