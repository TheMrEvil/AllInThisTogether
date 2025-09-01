using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DC RID: 220
	[HelpURL("https://www.youtube.com/watch?v=7__IafZGwvI&index=1&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Full Body Biped IK")]
	public class FullBodyBipedIK : IK
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0003E882 File Offset: 0x0003CA82
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page8.html");
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0003E88E File Offset: 0x0003CA8E
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_full_body_biped_i_k.html");
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0003E89A File Offset: 0x0003CA9A
		[ContextMenu("TUTORIAL VIDEO (SETUP)")]
		private void OpenSetupTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=7__IafZGwvI");
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0003E8A6 File Offset: 0x0003CAA6
		[ContextMenu("TUTORIAL VIDEO (INSPECTOR)")]
		private void OpenInspectorTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=tgRMsTphjJo");
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0003E8B2 File Offset: 0x0003CAB2
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0003E8BE File Offset: 0x0003CABE
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0003E8CA File Offset: 0x0003CACA
		public void SetReferences(BipedReferences references, Transform rootNode)
		{
			this.references = references;
			this.solver.SetToReferences(this.references, rootNode);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0003E8E5 File Offset: 0x0003CAE5
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003E8F0 File Offset: 0x0003CAF0
		public bool ReferencesError(ref string errorMessage)
		{
			if (BipedReferences.SetupError(this.references, ref errorMessage))
			{
				return true;
			}
			if (this.references.spine.Length == 0)
			{
				errorMessage = "References has no spine bones assigned, can not initiate the solver.";
				return true;
			}
			if (this.solver.rootNode == null)
			{
				errorMessage = "Root Node bone is null, can not initiate the solver.";
				return true;
			}
			if (this.solver.rootNode != this.references.pelvis)
			{
				bool flag = false;
				for (int i = 0; i < this.references.spine.Length; i++)
				{
					if (this.solver.rootNode == this.references.spine[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					errorMessage = "The Root Node has to be one of the bones in the Spine or the Pelvis, can not initiate the solver.";
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		public bool ReferencesWarning(ref string warningMessage)
		{
			if (BipedReferences.SetupWarning(this.references, ref warningMessage))
			{
				return true;
			}
			Vector3 vector = this.references.rightUpperArm.position - this.references.leftUpperArm.position;
			Vector3 vector2 = this.solver.rootNode.position - this.references.leftUpperArm.position;
			if (Vector3.Dot(vector.normalized, vector2.normalized) > 0.95f)
			{
				warningMessage = "The root node, the left upper arm and the right upper arm bones should ideally form a triangle that is as close to equilateral as possible. Currently the root node bone seems to be very close to the line between the left upper arm and the right upper arm bones. This might cause unwanted behaviour like the spine turning upside down when pulled by a hand effector.Please set the root node bone to be one of the lower bones in the spine.";
				return true;
			}
			Vector3 vector3 = this.references.rightThigh.position - this.references.leftThigh.position;
			Vector3 vector4 = this.solver.rootNode.position - this.references.leftThigh.position;
			if (Vector3.Dot(vector3.normalized, vector4.normalized) > 0.95f)
			{
				warningMessage = "The root node, the left thigh and the right thigh bones should ideally form a triangle that is as close to equilateral as possible. Currently the root node bone seems to be very close to the line between the left thigh and the right thigh bones. This might cause unwanted behaviour like the hip turning upside down when pulled by an effector.Please set the root node bone to be one of the higher bones in the spine.";
				return true;
			}
			return false;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0003EAA4 File Offset: 0x0003CCA4
		[ContextMenu("Reinitiate")]
		private void Reinitiate()
		{
			this.SetReferences(this.references, this.solver.rootNode);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003EAC0 File Offset: 0x0003CCC0
		[ContextMenu("Auto-detect References")]
		private void AutoDetectReferences()
		{
			this.references = new BipedReferences();
			BipedReferences.AutoDetectReferences(ref this.references, base.transform, new BipedReferences.AutoDetectParams(true, false));
			this.solver.rootNode = IKSolverFullBodyBiped.DetectRootNodeBone(this.references);
			this.solver.SetToReferences(this.references, this.solver.rootNode);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003EB23 File Offset: 0x0003CD23
		public FullBodyBipedIK()
		{
		}

		// Token: 0x04000767 RID: 1895
		public BipedReferences references = new BipedReferences();

		// Token: 0x04000768 RID: 1896
		public IKSolverFullBodyBiped solver = new IKSolverFullBodyBiped();
	}
}
