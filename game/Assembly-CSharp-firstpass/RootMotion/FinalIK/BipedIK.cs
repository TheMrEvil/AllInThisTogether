using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000C6 RID: 198
	[HelpURL("http://www.root-motion.com/finalikdox/html/page4.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/Biped IK")]
	public class BipedIK : SolverManager
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0003A6E7 File Offset: 0x000388E7
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page4.html");
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003A6F3 File Offset: 0x000388F3
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_biped_i_k.html");
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003A6FF File Offset: 0x000388FF
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0003A70B File Offset: 0x0003890B
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0003A717 File Offset: 0x00038917
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			return this.GetGoalIK(goal).GetIKPositionWeight();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003A725 File Offset: 0x00038925
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			return this.GetGoalIK(goal).GetIKRotationWeight();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0003A733 File Offset: 0x00038933
		public void SetIKPositionWeight(AvatarIKGoal goal, float weight)
		{
			this.GetGoalIK(goal).SetIKPositionWeight(weight);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0003A742 File Offset: 0x00038942
		public void SetIKRotationWeight(AvatarIKGoal goal, float weight)
		{
			this.GetGoalIK(goal).SetIKRotationWeight(weight);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0003A751 File Offset: 0x00038951
		public void SetIKPosition(AvatarIKGoal goal, Vector3 IKPosition)
		{
			this.GetGoalIK(goal).SetIKPosition(IKPosition);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0003A760 File Offset: 0x00038960
		public void SetIKRotation(AvatarIKGoal goal, Quaternion IKRotation)
		{
			this.GetGoalIK(goal).SetIKRotation(IKRotation);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0003A76F File Offset: 0x0003896F
		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			return this.GetGoalIK(goal).GetIKPosition();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0003A77D File Offset: 0x0003897D
		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			return this.GetGoalIK(goal).GetIKRotation();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0003A78B File Offset: 0x0003898B
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight, float clampWeightHead, float clampWeightEyes)
		{
			this.solvers.lookAt.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight, clampWeightHead, clampWeightEyes);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0003A7A8 File Offset: 0x000389A8
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.solvers.lookAt.SetIKPosition(lookAtPosition);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0003A7BB File Offset: 0x000389BB
		public void SetSpinePosition(Vector3 spinePosition)
		{
			this.solvers.spine.SetIKPosition(spinePosition);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0003A7CE File Offset: 0x000389CE
		public void SetSpineWeight(float weight)
		{
			this.solvers.spine.SetIKPositionWeight(weight);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0003A7E4 File Offset: 0x000389E4
		public IKSolverLimb GetGoalIK(AvatarIKGoal goal)
		{
			switch (goal)
			{
			case AvatarIKGoal.LeftFoot:
				return this.solvers.leftFoot;
			case AvatarIKGoal.RightFoot:
				return this.solvers.rightFoot;
			case AvatarIKGoal.LeftHand:
				return this.solvers.leftHand;
			case AvatarIKGoal.RightHand:
				return this.solvers.rightHand;
			default:
				return null;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0003A83A File Offset: 0x00038A3A
		public void InitiateBipedIK()
		{
			this.InitiateSolver();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003A842 File Offset: 0x00038A42
		public void UpdateBipedIK()
		{
			this.UpdateSolver();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0003A84C File Offset: 0x00038A4C
		public void SetToDefaults()
		{
			foreach (IKSolverLimb iksolverLimb in this.solvers.limbs)
			{
				iksolverLimb.SetIKPositionWeight(0f);
				iksolverLimb.SetIKRotationWeight(0f);
				iksolverLimb.bendModifier = IKSolverLimb.BendModifier.Animation;
				iksolverLimb.bendModifierWeight = 1f;
			}
			this.solvers.leftHand.maintainRotationWeight = 0f;
			this.solvers.rightHand.maintainRotationWeight = 0f;
			this.solvers.spine.SetIKPositionWeight(0f);
			this.solvers.spine.tolerance = 0f;
			this.solvers.spine.maxIterations = 2;
			this.solvers.spine.useRotationLimits = false;
			this.solvers.aim.SetIKPositionWeight(0f);
			this.solvers.aim.tolerance = 0f;
			this.solvers.aim.maxIterations = 2;
			this.SetLookAtWeight(0f, 0.5f, 1f, 1f, 0.5f, 0.7f, 0.5f);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003A978 File Offset: 0x00038B78
		protected override void FixTransforms()
		{
			this.solvers.lookAt.FixTransforms();
			for (int i = 0; i < this.solvers.limbs.Length; i++)
			{
				this.solvers.limbs[i].FixTransforms();
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0003A9C0 File Offset: 0x00038BC0
		protected override void InitiateSolver()
		{
			string message = "";
			if (BipedReferences.SetupError(this.references, ref message))
			{
				Warning.Log(message, this.references.root, false);
				return;
			}
			this.solvers.AssignReferences(this.references);
			if (this.solvers.spine.bones.Length > 1)
			{
				this.solvers.spine.Initiate(base.transform);
			}
			this.solvers.lookAt.Initiate(base.transform);
			this.solvers.aim.Initiate(base.transform);
			IKSolverLimb[] limbs = this.solvers.limbs;
			for (int i = 0; i < limbs.Length; i++)
			{
				limbs[i].Initiate(base.transform);
			}
			this.solvers.pelvis.Initiate(this.references.pelvis);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003AAA0 File Offset: 0x00038CA0
		protected override void UpdateSolver()
		{
			for (int i = 0; i < this.solvers.limbs.Length; i++)
			{
				this.solvers.limbs[i].MaintainBend();
				this.solvers.limbs[i].MaintainRotation();
			}
			this.solvers.pelvis.Update();
			if (this.solvers.spine.bones.Length > 1)
			{
				this.solvers.spine.Update();
			}
			this.solvers.aim.Update();
			this.solvers.lookAt.Update();
			for (int j = 0; j < this.solvers.limbs.Length; j++)
			{
				this.solvers.limbs[j].Update();
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003AB68 File Offset: 0x00038D68
		public void LogWarning(string message)
		{
			Warning.Log(message, base.transform, false);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003AB77 File Offset: 0x00038D77
		public BipedIK()
		{
		}

		// Token: 0x040006D1 RID: 1745
		public BipedReferences references = new BipedReferences();

		// Token: 0x040006D2 RID: 1746
		public BipedIKSolvers solvers = new BipedIKSolvers();
	}
}
