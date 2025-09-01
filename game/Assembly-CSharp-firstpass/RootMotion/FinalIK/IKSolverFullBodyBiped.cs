using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class IKSolverFullBodyBiped : IKSolverFullBody
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0004683C File Offset: 0x00044A3C
		public IKEffector bodyEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.Body);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00046845 File Offset: 0x00044A45
		public IKEffector leftShoulderEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.LeftShoulder);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0004684E File Offset: 0x00044A4E
		public IKEffector rightShoulderEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.RightShoulder);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00046857 File Offset: 0x00044A57
		public IKEffector leftThighEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.LeftThigh);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00046860 File Offset: 0x00044A60
		public IKEffector rightThighEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.RightThigh);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00046869 File Offset: 0x00044A69
		public IKEffector leftHandEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.LeftHand);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00046872 File Offset: 0x00044A72
		public IKEffector rightHandEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.RightHand);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0004687B File Offset: 0x00044A7B
		public IKEffector leftFootEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.LeftFoot);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00046884 File Offset: 0x00044A84
		public IKEffector rightFootEffector
		{
			get
			{
				return this.GetEffector(FullBodyBipedEffector.RightFoot);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0004688D File Offset: 0x00044A8D
		public FBIKChain leftArmChain
		{
			get
			{
				return this.chain[1];
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00046897 File Offset: 0x00044A97
		public FBIKChain rightArmChain
		{
			get
			{
				return this.chain[2];
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000468A1 File Offset: 0x00044AA1
		public FBIKChain leftLegChain
		{
			get
			{
				return this.chain[3];
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000468AB File Offset: 0x00044AAB
		public FBIKChain rightLegChain
		{
			get
			{
				return this.chain[4];
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000468B5 File Offset: 0x00044AB5
		public IKMappingLimb leftArmMapping
		{
			get
			{
				return this.limbMappings[0];
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000468BF File Offset: 0x00044ABF
		public IKMappingLimb rightArmMapping
		{
			get
			{
				return this.limbMappings[1];
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000468C9 File Offset: 0x00044AC9
		public IKMappingLimb leftLegMapping
		{
			get
			{
				return this.limbMappings[2];
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000468D3 File Offset: 0x00044AD3
		public IKMappingLimb rightLegMapping
		{
			get
			{
				return this.limbMappings[3];
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x000468DD File Offset: 0x00044ADD
		public IKMappingBone headMapping
		{
			get
			{
				return this.boneMappings[0];
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000468E7 File Offset: 0x00044AE7
		public void SetChainWeights(FullBodyBipedChain c, float pull, float reach = 0f)
		{
			this.GetChain(c).pull = pull;
			this.GetChain(c).reach = reach;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00046903 File Offset: 0x00044B03
		public void SetEffectorWeights(FullBodyBipedEffector effector, float positionWeight, float rotationWeight)
		{
			this.GetEffector(effector).positionWeight = Mathf.Clamp(positionWeight, 0f, 1f);
			this.GetEffector(effector).rotationWeight = Mathf.Clamp(rotationWeight, 0f, 1f);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0004693D File Offset: 0x00044B3D
		public FBIKChain GetChain(FullBodyBipedChain c)
		{
			switch (c)
			{
			case FullBodyBipedChain.LeftArm:
				return this.chain[1];
			case FullBodyBipedChain.RightArm:
				return this.chain[2];
			case FullBodyBipedChain.LeftLeg:
				return this.chain[3];
			case FullBodyBipedChain.RightLeg:
				return this.chain[4];
			default:
				return null;
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0004697C File Offset: 0x00044B7C
		public FBIKChain GetChain(FullBodyBipedEffector effector)
		{
			switch (effector)
			{
			case FullBodyBipedEffector.Body:
				return this.chain[0];
			case FullBodyBipedEffector.LeftShoulder:
				return this.chain[1];
			case FullBodyBipedEffector.RightShoulder:
				return this.chain[2];
			case FullBodyBipedEffector.LeftThigh:
				return this.chain[3];
			case FullBodyBipedEffector.RightThigh:
				return this.chain[4];
			case FullBodyBipedEffector.LeftHand:
				return this.chain[1];
			case FullBodyBipedEffector.RightHand:
				return this.chain[2];
			case FullBodyBipedEffector.LeftFoot:
				return this.chain[3];
			case FullBodyBipedEffector.RightFoot:
				return this.chain[4];
			default:
				return null;
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00046A08 File Offset: 0x00044C08
		public IKEffector GetEffector(FullBodyBipedEffector effector)
		{
			switch (effector)
			{
			case FullBodyBipedEffector.Body:
				return this.effectors[0];
			case FullBodyBipedEffector.LeftShoulder:
				return this.effectors[1];
			case FullBodyBipedEffector.RightShoulder:
				return this.effectors[2];
			case FullBodyBipedEffector.LeftThigh:
				return this.effectors[3];
			case FullBodyBipedEffector.RightThigh:
				return this.effectors[4];
			case FullBodyBipedEffector.LeftHand:
				return this.effectors[5];
			case FullBodyBipedEffector.RightHand:
				return this.effectors[6];
			case FullBodyBipedEffector.LeftFoot:
				return this.effectors[7];
			case FullBodyBipedEffector.RightFoot:
				return this.effectors[8];
			default:
				return null;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00046A93 File Offset: 0x00044C93
		public IKEffector GetEndEffector(FullBodyBipedChain c)
		{
			switch (c)
			{
			case FullBodyBipedChain.LeftArm:
				return this.effectors[5];
			case FullBodyBipedChain.RightArm:
				return this.effectors[6];
			case FullBodyBipedChain.LeftLeg:
				return this.effectors[7];
			case FullBodyBipedChain.RightLeg:
				return this.effectors[8];
			default:
				return null;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00046AD2 File Offset: 0x00044CD2
		public IKMappingLimb GetLimbMapping(FullBodyBipedChain chain)
		{
			switch (chain)
			{
			case FullBodyBipedChain.LeftArm:
				return this.limbMappings[0];
			case FullBodyBipedChain.RightArm:
				return this.limbMappings[1];
			case FullBodyBipedChain.LeftLeg:
				return this.limbMappings[2];
			case FullBodyBipedChain.RightLeg:
				return this.limbMappings[3];
			default:
				return null;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00046B14 File Offset: 0x00044D14
		public IKMappingLimb GetLimbMapping(FullBodyBipedEffector effector)
		{
			switch (effector)
			{
			case FullBodyBipedEffector.LeftShoulder:
				return this.limbMappings[0];
			case FullBodyBipedEffector.RightShoulder:
				return this.limbMappings[1];
			case FullBodyBipedEffector.LeftThigh:
				return this.limbMappings[2];
			case FullBodyBipedEffector.RightThigh:
				return this.limbMappings[3];
			case FullBodyBipedEffector.LeftHand:
				return this.limbMappings[0];
			case FullBodyBipedEffector.RightHand:
				return this.limbMappings[1];
			case FullBodyBipedEffector.LeftFoot:
				return this.limbMappings[2];
			case FullBodyBipedEffector.RightFoot:
				return this.limbMappings[3];
			default:
				return null;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00046B94 File Offset: 0x00044D94
		public IKMappingSpine GetSpineMapping()
		{
			return this.spineMapping;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00046B9C File Offset: 0x00044D9C
		public IKMappingBone GetHeadMapping()
		{
			return this.boneMappings[0];
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00046BA8 File Offset: 0x00044DA8
		public IKConstraintBend GetBendConstraint(FullBodyBipedChain limb)
		{
			switch (limb)
			{
			case FullBodyBipedChain.LeftArm:
				return this.chain[1].bendConstraint;
			case FullBodyBipedChain.RightArm:
				return this.chain[2].bendConstraint;
			case FullBodyBipedChain.LeftLeg:
				return this.chain[3].bendConstraint;
			case FullBodyBipedChain.RightLeg:
				return this.chain[4].bendConstraint;
			default:
				return null;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00046C08 File Offset: 0x00044E08
		public override bool IsValid(ref string message)
		{
			if (!base.IsValid(ref message))
			{
				return false;
			}
			if (this.rootNode == null)
			{
				message = "Root Node bone is null. FBBIK will not initiate.";
				return false;
			}
			if (this.chain.Length != 5 || this.chain[0].nodes.Length != 1 || this.chain[1].nodes.Length != 3 || this.chain[2].nodes.Length != 3 || this.chain[3].nodes.Length != 3 || this.chain[4].nodes.Length != 3 || this.effectors.Length != 9 || this.limbMappings.Length != 4)
			{
				message = "Invalid FBBIK setup. Please right-click on the component header and select 'Reinitiate'.";
				return false;
			}
			return true;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00046CC0 File Offset: 0x00044EC0
		public void SetToReferences(BipedReferences references, Transform rootNode = null)
		{
			this.root = references.root;
			if (rootNode == null)
			{
				rootNode = IKSolverFullBodyBiped.DetectRootNodeBone(references);
			}
			this.rootNode = rootNode;
			if (this.chain == null || this.chain.Length != 5)
			{
				this.chain = new FBIKChain[5];
			}
			for (int i = 0; i < this.chain.Length; i++)
			{
				if (this.chain[i] == null)
				{
					this.chain[i] = new FBIKChain();
				}
			}
			this.chain[0].pin = 0f;
			this.chain[0].SetNodes(new Transform[]
			{
				rootNode
			});
			this.chain[0].children = new int[]
			{
				1,
				2,
				3,
				4
			};
			this.chain[1].SetNodes(new Transform[]
			{
				references.leftUpperArm,
				references.leftForearm,
				references.leftHand
			});
			this.chain[2].SetNodes(new Transform[]
			{
				references.rightUpperArm,
				references.rightForearm,
				references.rightHand
			});
			this.chain[3].SetNodes(new Transform[]
			{
				references.leftThigh,
				references.leftCalf,
				references.leftFoot
			});
			this.chain[4].SetNodes(new Transform[]
			{
				references.rightThigh,
				references.rightCalf,
				references.rightFoot
			});
			if (this.effectors.Length != 9)
			{
				this.effectors = new IKEffector[]
				{
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector(),
					new IKEffector()
				};
			}
			this.effectors[0].bone = rootNode;
			this.effectors[0].childBones = new Transform[]
			{
				references.leftThigh,
				references.rightThigh
			};
			this.effectors[1].bone = references.leftUpperArm;
			this.effectors[2].bone = references.rightUpperArm;
			this.effectors[3].bone = references.leftThigh;
			this.effectors[4].bone = references.rightThigh;
			this.effectors[5].bone = references.leftHand;
			this.effectors[6].bone = references.rightHand;
			this.effectors[7].bone = references.leftFoot;
			this.effectors[8].bone = references.rightFoot;
			this.effectors[5].planeBone1 = references.leftUpperArm;
			this.effectors[5].planeBone2 = references.rightUpperArm;
			this.effectors[5].planeBone3 = rootNode;
			this.effectors[6].planeBone1 = references.rightUpperArm;
			this.effectors[6].planeBone2 = references.leftUpperArm;
			this.effectors[6].planeBone3 = rootNode;
			this.effectors[7].planeBone1 = references.leftThigh;
			this.effectors[7].planeBone2 = references.rightThigh;
			this.effectors[7].planeBone3 = rootNode;
			this.effectors[8].planeBone1 = references.rightThigh;
			this.effectors[8].planeBone2 = references.leftThigh;
			this.effectors[8].planeBone3 = rootNode;
			this.chain[0].childConstraints = new FBIKChain.ChildConstraint[]
			{
				new FBIKChain.ChildConstraint(references.leftUpperArm, references.rightThigh, 0f, 1f),
				new FBIKChain.ChildConstraint(references.rightUpperArm, references.leftThigh, 0f, 1f),
				new FBIKChain.ChildConstraint(references.leftUpperArm, references.rightUpperArm, 0f, 0f),
				new FBIKChain.ChildConstraint(references.leftThigh, references.rightThigh, 0f, 0f)
			};
			Transform[] array = new Transform[references.spine.Length + 1];
			array[0] = references.pelvis;
			for (int j = 0; j < references.spine.Length; j++)
			{
				array[j + 1] = references.spine[j];
			}
			if (this.spineMapping == null)
			{
				this.spineMapping = new IKMappingSpine();
				this.spineMapping.iterations = 3;
			}
			this.spineMapping.SetBones(array, references.leftUpperArm, references.rightUpperArm, references.leftThigh, references.rightThigh);
			int num = (references.head != null) ? 1 : 0;
			if (this.boneMappings.Length != num)
			{
				this.boneMappings = new IKMappingBone[num];
				for (int k = 0; k < this.boneMappings.Length; k++)
				{
					this.boneMappings[k] = new IKMappingBone();
				}
				if (num == 1)
				{
					this.boneMappings[0].maintainRotationWeight = 0f;
				}
			}
			if (this.boneMappings.Length != 0)
			{
				this.boneMappings[0].bone = references.head;
			}
			if (this.limbMappings.Length != 4)
			{
				this.limbMappings = new IKMappingLimb[]
				{
					new IKMappingLimb(),
					new IKMappingLimb(),
					new IKMappingLimb(),
					new IKMappingLimb()
				};
				this.limbMappings[2].maintainRotationWeight = 1f;
				this.limbMappings[3].maintainRotationWeight = 1f;
			}
			this.limbMappings[0].SetBones(references.leftUpperArm, references.leftForearm, references.leftHand, IKSolverFullBodyBiped.GetLeftClavicle(references));
			this.limbMappings[1].SetBones(references.rightUpperArm, references.rightForearm, references.rightHand, IKSolverFullBodyBiped.GetRightClavicle(references));
			this.limbMappings[2].SetBones(references.leftThigh, references.leftCalf, references.leftFoot, null);
			this.limbMappings[3].SetBones(references.rightThigh, references.rightCalf, references.rightFoot, null);
			if (Application.isPlaying)
			{
				base.Initiate(references.root);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000472B8 File Offset: 0x000454B8
		public static Transform DetectRootNodeBone(BipedReferences references)
		{
			if (!references.isFilled)
			{
				return null;
			}
			if (references.spine.Length < 1)
			{
				return null;
			}
			int num = references.spine.Length;
			if (num == 1)
			{
				return references.spine[0];
			}
			Vector3 b = Vector3.Lerp(references.leftThigh.position, references.rightThigh.position, 0.5f);
			Vector3 onNormal = Vector3.Lerp(references.leftUpperArm.position, references.rightUpperArm.position, 0.5f) - b;
			float magnitude = onNormal.magnitude;
			if (references.spine.Length < 2)
			{
				return references.spine[0];
			}
			int num2 = 0;
			for (int i = 1; i < num; i++)
			{
				Vector3 vector = Vector3.Project(references.spine[i].position - b, onNormal);
				if (Vector3.Dot(vector.normalized, onNormal.normalized) > 0f && vector.magnitude / magnitude < 0.5f)
				{
					num2 = i;
				}
			}
			return references.spine[num2];
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000473BC File Offset: 0x000455BC
		public void SetLimbOrientations(BipedLimbOrientations o)
		{
			this.SetLimbOrientation(FullBodyBipedChain.LeftArm, o.leftArm);
			this.SetLimbOrientation(FullBodyBipedChain.RightArm, o.rightArm);
			this.SetLimbOrientation(FullBodyBipedChain.LeftLeg, o.leftLeg);
			this.SetLimbOrientation(FullBodyBipedChain.RightLeg, o.rightLeg);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000473F2 File Offset: 0x000455F2
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x000473FA File Offset: 0x000455FA
		public Vector3 pullBodyOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<pullBodyOffset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<pullBodyOffset>k__BackingField = value;
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00047404 File Offset: 0x00045604
		private void SetLimbOrientation(FullBodyBipedChain chain, BipedLimbOrientations.LimbOrientation limbOrientation)
		{
			if (chain == FullBodyBipedChain.LeftArm || chain == FullBodyBipedChain.RightArm)
			{
				this.GetBendConstraint(chain).SetLimbOrientation(-limbOrientation.upperBoneForwardAxis, -limbOrientation.lowerBoneForwardAxis, -limbOrientation.lastBoneLeftAxis);
				this.GetLimbMapping(chain).SetLimbOrientation(-limbOrientation.upperBoneForwardAxis, -limbOrientation.lowerBoneForwardAxis);
				return;
			}
			this.GetBendConstraint(chain).SetLimbOrientation(limbOrientation.upperBoneForwardAxis, limbOrientation.lowerBoneForwardAxis, limbOrientation.lastBoneLeftAxis);
			this.GetLimbMapping(chain).SetLimbOrientation(limbOrientation.upperBoneForwardAxis, limbOrientation.lowerBoneForwardAxis);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000474A3 File Offset: 0x000456A3
		private static Transform GetLeftClavicle(BipedReferences references)
		{
			if (references.leftUpperArm == null)
			{
				return null;
			}
			if (!IKSolverFullBodyBiped.Contains(references.spine, references.leftUpperArm.parent))
			{
				return references.leftUpperArm.parent;
			}
			return null;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000474DA File Offset: 0x000456DA
		private static Transform GetRightClavicle(BipedReferences references)
		{
			if (references.rightUpperArm == null)
			{
				return null;
			}
			if (!IKSolverFullBodyBiped.Contains(references.spine, references.rightUpperArm.parent))
			{
				return references.rightUpperArm.parent;
			}
			return null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00047514 File Offset: 0x00045714
		private static bool Contains(Transform[] array, Transform transform)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == transform)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00047540 File Offset: 0x00045740
		protected override void ReadPose()
		{
			for (int i = 0; i < this.effectors.Length; i++)
			{
				this.effectors[i].SetToTarget();
			}
			this.PullBody();
			float pushElasticity = Mathf.Clamp(1f - this.spineStiffness, 0f, 1f);
			this.chain[0].childConstraints[0].pushElasticity = pushElasticity;
			this.chain[0].childConstraints[1].pushElasticity = pushElasticity;
			base.ReadPose();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000475C0 File Offset: 0x000457C0
		private void PullBody()
		{
			if (this.iterations < 1)
			{
				return;
			}
			if (this.pullBodyVertical != 0f || this.pullBodyHorizontal != 0f)
			{
				Vector3 bodyOffset = this.GetBodyOffset();
				this.pullBodyOffset = V3Tools.ExtractVertical(bodyOffset, this.root.up, this.pullBodyVertical) + V3Tools.ExtractHorizontal(bodyOffset, this.root.up, this.pullBodyHorizontal);
				this.bodyEffector.positionOffset += this.pullBodyOffset;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00047650 File Offset: 0x00045850
		private Vector3 GetBodyOffset()
		{
			Vector3 a = Vector3.zero + this.GetHandBodyPull(this.leftHandEffector, this.leftArmChain, Vector3.zero) * Mathf.Clamp(this.leftHandEffector.positionWeight, 0f, 1f);
			return a + this.GetHandBodyPull(this.rightHandEffector, this.rightArmChain, a) * Mathf.Clamp(this.rightHandEffector.positionWeight, 0f, 1f);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000476D8 File Offset: 0x000458D8
		private Vector3 GetHandBodyPull(IKEffector effector, FBIKChain arm, Vector3 offset)
		{
			Vector3 a = effector.position - (arm.nodes[0].transform.position + offset);
			float num = arm.nodes[0].length + arm.nodes[1].length;
			float magnitude = a.magnitude;
			if (magnitude < num)
			{
				return Vector3.zero;
			}
			float d = magnitude - num;
			return a / magnitude * d;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00047748 File Offset: 0x00045948
		protected override void ApplyBendConstraints()
		{
			if (this.iterations > 0)
			{
				this.chain[1].bendConstraint.rotationOffset = this.leftHandEffector.planeRotationOffset;
				this.chain[2].bendConstraint.rotationOffset = this.rightHandEffector.planeRotationOffset;
				this.chain[3].bendConstraint.rotationOffset = this.leftFootEffector.planeRotationOffset;
				this.chain[4].bendConstraint.rotationOffset = this.rightFootEffector.planeRotationOffset;
			}
			else
			{
				this.offset = Vector3.Lerp(this.effectors[0].positionOffset, this.effectors[0].position - (this.effectors[0].bone.position + this.effectors[0].positionOffset), this.effectors[0].positionWeight);
				for (int i = 0; i < 5; i++)
				{
					this.effectors[i].GetNode(this).solverPosition += this.offset;
				}
			}
			base.ApplyBendConstraints();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00047868 File Offset: 0x00045A68
		protected override void WritePose()
		{
			if (this.iterations == 0)
			{
				this.spineMapping.spineBones[0].position += this.offset;
			}
			base.WritePose();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0004789B File Offset: 0x00045A9B
		public IKSolverFullBodyBiped()
		{
		}

		// Token: 0x0400085B RID: 2139
		public Transform rootNode;

		// Token: 0x0400085C RID: 2140
		[Range(0f, 1f)]
		public float spineStiffness = 0.5f;

		// Token: 0x0400085D RID: 2141
		[Range(-1f, 1f)]
		public float pullBodyVertical = 0.5f;

		// Token: 0x0400085E RID: 2142
		[Range(-1f, 1f)]
		public float pullBodyHorizontal;

		// Token: 0x0400085F RID: 2143
		[CompilerGenerated]
		private Vector3 <pullBodyOffset>k__BackingField;

		// Token: 0x04000860 RID: 2144
		private Vector3 offset;
	}
}
