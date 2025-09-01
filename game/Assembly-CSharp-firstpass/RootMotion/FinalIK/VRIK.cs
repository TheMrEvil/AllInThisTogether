using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E3 RID: 227
	[AddComponentMenu("Scripts/RootMotion.FinalIK/IK/VR IK")]
	public class VRIK : IK
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x0003EDED File Offset: 0x0003CFED
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page16.html");
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003EDF9 File Offset: 0x0003CFF9
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_v_r_i_k.html");
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0003EE05 File Offset: 0x0003D005
		[ContextMenu("TUTORIAL VIDEO (STEAMVR SETUP)")]
		private void OpenSetupTutorial()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=6Pfx7lYQiIA&feature=youtu.be");
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003EE11 File Offset: 0x0003D011
		[ContextMenu("Auto-detect References")]
		public void AutoDetectReferences()
		{
			VRIK.References.AutoDetectReferences(base.transform, out this.references);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003EE25 File Offset: 0x0003D025
		[ContextMenu("Guess Hand Orientations")]
		public void GuessHandOrientations()
		{
			this.solver.GuessHandOrientations(this.references, false);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0003EE39 File Offset: 0x0003D039
		public override IKSolver GetIKSolver()
		{
			return this.solver;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0003EE41 File Offset: 0x0003D041
		protected override void InitiateSolver()
		{
			if (this.references.isEmpty)
			{
				this.AutoDetectReferences();
			}
			if (this.references.isFilled)
			{
				this.solver.SetToReferences(this.references);
			}
			base.InitiateSolver();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003EE7C File Offset: 0x0003D07C
		protected override void UpdateSolver()
		{
			if (this.references.root != null && this.references.root.localScale == Vector3.zero)
			{
				Debug.LogError("VRIK Root Transform's scale is zero, can not update VRIK. Make sure you have not calibrated the character to a zero scale.", base.transform);
				base.enabled = false;
				return;
			}
			base.UpdateSolver();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003EED6 File Offset: 0x0003D0D6
		public VRIK()
		{
		}

		// Token: 0x04000770 RID: 1904
		[ContextMenuItem("Auto-detect References", "AutoDetectReferences")]
		[Tooltip("Bone mapping. Right-click on the component header and select 'Auto-detect References' of fill in manually if not a Humanoid character. Chest, neck, shoulder and toe bones are optional. VRIK also supports legless characters. If you do not wish to use legs, leave all leg references empty.")]
		public VRIK.References references = new VRIK.References();

		// Token: 0x04000771 RID: 1905
		[Tooltip("The VRIK solver.")]
		public IKSolverVR solver = new IKSolverVR();

		// Token: 0x020001EE RID: 494
		[Serializable]
		public class References
		{
			// Token: 0x06001040 RID: 4160 RVA: 0x00065644 File Offset: 0x00063844
			public Transform[] GetTransforms()
			{
				return new Transform[]
				{
					this.root,
					this.pelvis,
					this.spine,
					this.chest,
					this.neck,
					this.head,
					this.leftShoulder,
					this.leftUpperArm,
					this.leftForearm,
					this.leftHand,
					this.rightShoulder,
					this.rightUpperArm,
					this.rightForearm,
					this.rightHand,
					this.leftThigh,
					this.leftCalf,
					this.leftFoot,
					this.leftToes,
					this.rightThigh,
					this.rightCalf,
					this.rightFoot,
					this.rightToes
				};
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06001041 RID: 4161 RVA: 0x0006572C File Offset: 0x0006392C
			public bool isFilled
			{
				get
				{
					return !(this.root == null) && !(this.pelvis == null) && !(this.spine == null) && !(this.head == null) && !(this.leftUpperArm == null) && !(this.leftForearm == null) && !(this.leftHand == null) && !(this.rightUpperArm == null) && !(this.rightForearm == null) && !(this.rightHand == null) && ((this.leftThigh == null && this.leftCalf == null && this.leftFoot == null && this.rightThigh == null && this.rightCalf == null && this.rightFoot == null) || (!(this.leftThigh == null) && !(this.leftCalf == null) && !(this.leftFoot == null) && !(this.rightThigh == null) && !(this.rightCalf == null) && !(this.rightFoot == null)));
				}
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06001042 RID: 4162 RVA: 0x0006587C File Offset: 0x00063A7C
			public bool isEmpty
			{
				get
				{
					return !(this.root != null) && !(this.pelvis != null) && !(this.spine != null) && !(this.chest != null) && !(this.neck != null) && !(this.head != null) && !(this.leftShoulder != null) && !(this.leftUpperArm != null) && !(this.leftForearm != null) && !(this.leftHand != null) && !(this.rightShoulder != null) && !(this.rightUpperArm != null) && !(this.rightForearm != null) && !(this.rightHand != null) && !(this.leftThigh != null) && !(this.leftCalf != null) && !(this.leftFoot != null) && !(this.leftToes != null) && !(this.rightThigh != null) && !(this.rightCalf != null) && !(this.rightFoot != null) && !(this.rightToes != null);
				}
			}

			// Token: 0x06001043 RID: 4163 RVA: 0x000659E4 File Offset: 0x00063BE4
			public static bool AutoDetectReferences(Transform root, out VRIK.References references)
			{
				references = new VRIK.References();
				Animator componentInChildren = root.GetComponentInChildren<Animator>();
				if (componentInChildren == null || !componentInChildren.isHuman)
				{
					Debug.LogWarning("VRIK needs a Humanoid Animator to auto-detect biped references. Please assign references manually.");
					return false;
				}
				references.root = root;
				references.pelvis = componentInChildren.GetBoneTransform(HumanBodyBones.Hips);
				references.spine = componentInChildren.GetBoneTransform(HumanBodyBones.Spine);
				references.chest = componentInChildren.GetBoneTransform(HumanBodyBones.Chest);
				references.neck = componentInChildren.GetBoneTransform(HumanBodyBones.Neck);
				references.head = componentInChildren.GetBoneTransform(HumanBodyBones.Head);
				references.leftShoulder = componentInChildren.GetBoneTransform(HumanBodyBones.LeftShoulder);
				references.leftUpperArm = componentInChildren.GetBoneTransform(HumanBodyBones.LeftUpperArm);
				references.leftForearm = componentInChildren.GetBoneTransform(HumanBodyBones.LeftLowerArm);
				references.leftHand = componentInChildren.GetBoneTransform(HumanBodyBones.LeftHand);
				references.rightShoulder = componentInChildren.GetBoneTransform(HumanBodyBones.RightShoulder);
				references.rightUpperArm = componentInChildren.GetBoneTransform(HumanBodyBones.RightUpperArm);
				references.rightForearm = componentInChildren.GetBoneTransform(HumanBodyBones.RightLowerArm);
				references.rightHand = componentInChildren.GetBoneTransform(HumanBodyBones.RightHand);
				references.leftThigh = componentInChildren.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
				references.leftCalf = componentInChildren.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
				references.leftFoot = componentInChildren.GetBoneTransform(HumanBodyBones.LeftFoot);
				references.leftToes = componentInChildren.GetBoneTransform(HumanBodyBones.LeftToes);
				references.rightThigh = componentInChildren.GetBoneTransform(HumanBodyBones.RightUpperLeg);
				references.rightCalf = componentInChildren.GetBoneTransform(HumanBodyBones.RightLowerLeg);
				references.rightFoot = componentInChildren.GetBoneTransform(HumanBodyBones.RightFoot);
				references.rightToes = componentInChildren.GetBoneTransform(HumanBodyBones.RightToes);
				return true;
			}

			// Token: 0x06001044 RID: 4164 RVA: 0x00065B57 File Offset: 0x00063D57
			public References()
			{
			}

			// Token: 0x04000E96 RID: 3734
			public Transform root;

			// Token: 0x04000E97 RID: 3735
			public Transform pelvis;

			// Token: 0x04000E98 RID: 3736
			public Transform spine;

			// Token: 0x04000E99 RID: 3737
			[Tooltip("Optional")]
			public Transform chest;

			// Token: 0x04000E9A RID: 3738
			[Tooltip("Optional")]
			public Transform neck;

			// Token: 0x04000E9B RID: 3739
			public Transform head;

			// Token: 0x04000E9C RID: 3740
			[Tooltip("Optional")]
			public Transform leftShoulder;

			// Token: 0x04000E9D RID: 3741
			public Transform leftUpperArm;

			// Token: 0x04000E9E RID: 3742
			public Transform leftForearm;

			// Token: 0x04000E9F RID: 3743
			public Transform leftHand;

			// Token: 0x04000EA0 RID: 3744
			[Tooltip("Optional")]
			public Transform rightShoulder;

			// Token: 0x04000EA1 RID: 3745
			public Transform rightUpperArm;

			// Token: 0x04000EA2 RID: 3746
			public Transform rightForearm;

			// Token: 0x04000EA3 RID: 3747
			public Transform rightHand;

			// Token: 0x04000EA4 RID: 3748
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform leftThigh;

			// Token: 0x04000EA5 RID: 3749
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform leftCalf;

			// Token: 0x04000EA6 RID: 3750
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform leftFoot;

			// Token: 0x04000EA7 RID: 3751
			[Tooltip("Optional")]
			public Transform leftToes;

			// Token: 0x04000EA8 RID: 3752
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform rightThigh;

			// Token: 0x04000EA9 RID: 3753
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform rightCalf;

			// Token: 0x04000EAA RID: 3754
			[Tooltip("VRIK also supports legless characters.If you do not wish to use legs, leave all leg references empty.")]
			public Transform rightFoot;

			// Token: 0x04000EAB RID: 3755
			[Tooltip("Optional")]
			public Transform rightToes;
		}
	}
}
