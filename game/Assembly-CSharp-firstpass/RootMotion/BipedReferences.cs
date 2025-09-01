using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class BipedReferences
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00037974 File Offset: 0x00035B74
		public virtual bool isFilled
		{
			get
			{
				if (this.root == null)
				{
					return false;
				}
				if (this.pelvis == null)
				{
					return false;
				}
				if (this.leftThigh == null || this.leftCalf == null || this.leftFoot == null)
				{
					return false;
				}
				if (this.rightThigh == null || this.rightCalf == null || this.rightFoot == null)
				{
					return false;
				}
				if (this.leftUpperArm == null || this.leftForearm == null || this.leftHand == null)
				{
					return false;
				}
				if (this.rightUpperArm == null || this.rightForearm == null || this.rightHand == null)
				{
					return false;
				}
				Transform[] array = this.spine;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null)
					{
						return false;
					}
				}
				array = this.eyes;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00037A96 File Offset: 0x00035C96
		public bool isEmpty
		{
			get
			{
				return this.IsEmpty(true);
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00037AA0 File Offset: 0x00035CA0
		public virtual bool IsEmpty(bool includeRoot)
		{
			if (includeRoot && this.root != null)
			{
				return false;
			}
			if (this.pelvis != null || this.head != null)
			{
				return false;
			}
			if (this.leftThigh != null || this.leftCalf != null || this.leftFoot != null)
			{
				return false;
			}
			if (this.rightThigh != null || this.rightCalf != null || this.rightFoot != null)
			{
				return false;
			}
			if (this.leftUpperArm != null || this.leftForearm != null || this.leftHand != null)
			{
				return false;
			}
			if (this.rightUpperArm != null || this.rightForearm != null || this.rightHand != null)
			{
				return false;
			}
			Transform[] array = this.spine;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return false;
				}
			}
			array = this.eyes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00037BD4 File Offset: 0x00035DD4
		public virtual bool Contains(Transform t, bool ignoreRoot = false)
		{
			if (!ignoreRoot && this.root == t)
			{
				return true;
			}
			if (this.pelvis == t)
			{
				return true;
			}
			if (this.leftThigh == t)
			{
				return true;
			}
			if (this.leftCalf == t)
			{
				return true;
			}
			if (this.leftFoot == t)
			{
				return true;
			}
			if (this.rightThigh == t)
			{
				return true;
			}
			if (this.rightCalf == t)
			{
				return true;
			}
			if (this.rightFoot == t)
			{
				return true;
			}
			if (this.leftUpperArm == t)
			{
				return true;
			}
			if (this.leftForearm == t)
			{
				return true;
			}
			if (this.leftHand == t)
			{
				return true;
			}
			if (this.rightUpperArm == t)
			{
				return true;
			}
			if (this.rightForearm == t)
			{
				return true;
			}
			if (this.rightHand == t)
			{
				return true;
			}
			if (this.head == t)
			{
				return true;
			}
			Transform[] array = this.spine;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == t)
				{
					return true;
				}
			}
			array = this.eyes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == t)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00037D1C File Offset: 0x00035F1C
		public static bool AutoDetectReferences(ref BipedReferences references, Transform root, BipedReferences.AutoDetectParams autoDetectParams)
		{
			if (references == null)
			{
				references = new BipedReferences();
			}
			references.root = root;
			Animator component = root.GetComponent<Animator>();
			if (component != null && component.isHuman)
			{
				BipedReferences.AssignHumanoidReferences(ref references, component, autoDetectParams);
				return true;
			}
			BipedReferences.DetectReferencesByNaming(ref references, root, autoDetectParams);
			Warning.logged = false;
			if (!references.isFilled)
			{
				Warning.Log("BipedReferences contains one or more missing Transforms.", root, true);
				return false;
			}
			string message = "";
			if (BipedReferences.SetupError(references, ref message))
			{
				Warning.Log(message, references.root, true);
				return false;
			}
			if (BipedReferences.SetupWarning(references, ref message))
			{
				Warning.Log(message, references.root, true);
			}
			return true;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00037DC0 File Offset: 0x00035FC0
		public static void DetectReferencesByNaming(ref BipedReferences references, Transform root, BipedReferences.AutoDetectParams autoDetectParams)
		{
			if (references == null)
			{
				references = new BipedReferences();
			}
			Transform[] componentsInChildren = root.GetComponentsInChildren<Transform>();
			BipedReferences.DetectLimb(BipedNaming.BoneType.Arm, BipedNaming.BoneSide.Left, ref references.leftUpperArm, ref references.leftForearm, ref references.leftHand, componentsInChildren);
			BipedReferences.DetectLimb(BipedNaming.BoneType.Arm, BipedNaming.BoneSide.Right, ref references.rightUpperArm, ref references.rightForearm, ref references.rightHand, componentsInChildren);
			BipedReferences.DetectLimb(BipedNaming.BoneType.Leg, BipedNaming.BoneSide.Left, ref references.leftThigh, ref references.leftCalf, ref references.leftFoot, componentsInChildren);
			BipedReferences.DetectLimb(BipedNaming.BoneType.Leg, BipedNaming.BoneSide.Right, ref references.rightThigh, ref references.rightCalf, ref references.rightFoot, componentsInChildren);
			references.head = BipedNaming.GetBone(componentsInChildren, BipedNaming.BoneType.Head, BipedNaming.BoneSide.Center, Array.Empty<string[]>());
			references.pelvis = BipedNaming.GetNamingMatch(componentsInChildren, new string[][]
			{
				BipedNaming.pelvis
			});
			if ((references.pelvis == null || !Hierarchy.IsAncestor(references.leftThigh, references.pelvis)) && references.leftThigh != null)
			{
				references.pelvis = references.leftThigh.parent;
			}
			if (references.leftUpperArm != null && references.rightUpperArm != null && references.pelvis != null && references.leftThigh != null)
			{
				Transform firstCommonAncestor = Hierarchy.GetFirstCommonAncestor(references.leftUpperArm, references.rightUpperArm);
				if (firstCommonAncestor != null)
				{
					Transform[] array = new Transform[]
					{
						firstCommonAncestor
					};
					Hierarchy.AddAncestors(array[0], references.pelvis, ref array);
					references.spine = new Transform[0];
					for (int i = array.Length - 1; i > -1; i--)
					{
						if (BipedReferences.AddBoneToSpine(array[i], ref references, autoDetectParams))
						{
							Array.Resize<Transform>(ref references.spine, references.spine.Length + 1);
							references.spine[references.spine.Length - 1] = array[i];
						}
					}
					if (references.head == null)
					{
						for (int j = 0; j < firstCommonAncestor.childCount; j++)
						{
							Transform child = firstCommonAncestor.GetChild(j);
							if (!Hierarchy.ContainsChild(child, references.leftUpperArm) && !Hierarchy.ContainsChild(child, references.rightUpperArm))
							{
								references.head = child;
								break;
							}
						}
					}
				}
			}
			Transform[] bonesOfType = BipedNaming.GetBonesOfType(BipedNaming.BoneType.Eye, componentsInChildren);
			references.eyes = new Transform[0];
			if (autoDetectParams.includeEyes)
			{
				for (int k = 0; k < bonesOfType.Length; k++)
				{
					if (BipedReferences.AddBoneToEyes(bonesOfType[k], ref references, autoDetectParams))
					{
						Array.Resize<Transform>(ref references.eyes, references.eyes.Length + 1);
						references.eyes[references.eyes.Length - 1] = bonesOfType[k];
					}
				}
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0003806C File Offset: 0x0003626C
		public static void AssignHumanoidReferences(ref BipedReferences references, Animator animator, BipedReferences.AutoDetectParams autoDetectParams)
		{
			if (references == null)
			{
				references = new BipedReferences();
			}
			if (animator == null || !animator.isHuman)
			{
				return;
			}
			references.spine = new Transform[0];
			references.eyes = new Transform[0];
			references.head = animator.GetBoneTransform(HumanBodyBones.Head);
			references.leftThigh = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
			references.leftCalf = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
			references.leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
			references.rightThigh = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
			references.rightCalf = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
			references.rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
			references.leftUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
			references.leftForearm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
			references.leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
			references.rightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
			references.rightForearm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
			references.rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
			references.pelvis = animator.GetBoneTransform(HumanBodyBones.Hips);
			BipedReferences.AddBoneToHierarchy(ref references.spine, animator.GetBoneTransform(HumanBodyBones.Spine));
			BipedReferences.AddBoneToHierarchy(ref references.spine, animator.GetBoneTransform(HumanBodyBones.Chest));
			if (references.leftUpperArm != null && !BipedReferences.IsNeckBone(animator.GetBoneTransform(HumanBodyBones.Neck), references.leftUpperArm))
			{
				BipedReferences.AddBoneToHierarchy(ref references.spine, animator.GetBoneTransform(HumanBodyBones.Neck));
			}
			if (autoDetectParams.includeEyes)
			{
				BipedReferences.AddBoneToHierarchy(ref references.eyes, animator.GetBoneTransform(HumanBodyBones.LeftEye));
				BipedReferences.AddBoneToHierarchy(ref references.eyes, animator.GetBoneTransform(HumanBodyBones.RightEye));
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003820C File Offset: 0x0003640C
		public static bool SetupError(BipedReferences references, ref string errorMessage)
		{
			if (!references.isFilled)
			{
				errorMessage = "BipedReferences contains one or more missing Transforms.";
				return true;
			}
			return BipedReferences.LimbError(references.leftThigh, references.leftCalf, references.leftFoot, ref errorMessage) || BipedReferences.LimbError(references.rightThigh, references.rightCalf, references.rightFoot, ref errorMessage) || BipedReferences.LimbError(references.leftUpperArm, references.leftForearm, references.leftHand, ref errorMessage) || BipedReferences.LimbError(references.rightUpperArm, references.rightForearm, references.rightHand, ref errorMessage) || BipedReferences.SpineError(references, ref errorMessage) || BipedReferences.EyesError(references, ref errorMessage);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000382B4 File Offset: 0x000364B4
		public static bool SetupWarning(BipedReferences references, ref string warningMessage)
		{
			return BipedReferences.LimbWarning(references.leftThigh, references.leftCalf, references.leftFoot, ref warningMessage) || BipedReferences.LimbWarning(references.rightThigh, references.rightCalf, references.rightFoot, ref warningMessage) || BipedReferences.LimbWarning(references.leftUpperArm, references.leftForearm, references.leftHand, ref warningMessage) || BipedReferences.LimbWarning(references.rightUpperArm, references.rightForearm, references.rightHand, ref warningMessage) || BipedReferences.SpineWarning(references, ref warningMessage) || BipedReferences.EyesWarning(references, ref warningMessage) || BipedReferences.RootHeightWarning(references, ref warningMessage) || BipedReferences.FacingAxisWarning(references, ref warningMessage);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003835E File Offset: 0x0003655E
		private static bool IsNeckBone(Transform bone, Transform leftUpperArm)
		{
			return (!(leftUpperArm.parent != null) || !(leftUpperArm.parent == bone)) && !Hierarchy.IsAncestor(leftUpperArm, bone);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003838A File Offset: 0x0003658A
		private static bool AddBoneToEyes(Transform bone, ref BipedReferences references, BipedReferences.AutoDetectParams autoDetectParams)
		{
			return (!(references.head != null) || Hierarchy.IsAncestor(bone, references.head)) && !(bone.GetComponent<SkinnedMeshRenderer>() != null);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000383C0 File Offset: 0x000365C0
		private static bool AddBoneToSpine(Transform bone, ref BipedReferences references, BipedReferences.AutoDetectParams autoDetectParams)
		{
			if (bone == references.root)
			{
				return false;
			}
			if (bone == references.leftThigh.parent && !autoDetectParams.legsParentInSpine)
			{
				return false;
			}
			if (references.pelvis != null)
			{
				if (bone == references.pelvis)
				{
					return false;
				}
				if (Hierarchy.IsAncestor(references.pelvis, bone))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00038430 File Offset: 0x00036630
		private static void DetectLimb(BipedNaming.BoneType boneType, BipedNaming.BoneSide boneSide, ref Transform firstBone, ref Transform secondBone, ref Transform lastBone, Transform[] transforms)
		{
			Transform[] bonesOfTypeAndSide = BipedNaming.GetBonesOfTypeAndSide(boneType, boneSide, transforms);
			if (bonesOfTypeAndSide.Length < 3)
			{
				return;
			}
			if (bonesOfTypeAndSide.Length == 3)
			{
				firstBone = bonesOfTypeAndSide[0];
				secondBone = bonesOfTypeAndSide[1];
				lastBone = bonesOfTypeAndSide[2];
			}
			if (bonesOfTypeAndSide.Length > 3)
			{
				firstBone = bonesOfTypeAndSide[0];
				secondBone = bonesOfTypeAndSide[2];
				lastBone = bonesOfTypeAndSide[bonesOfTypeAndSide.Length - 1];
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003847E File Offset: 0x0003667E
		private static void AddBoneToHierarchy(ref Transform[] bones, Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			Array.Resize<Transform>(ref bones, bones.Length + 1);
			bones[bones.Length - 1] = transform;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000384A0 File Offset: 0x000366A0
		private static bool LimbError(Transform bone1, Transform bone2, Transform bone3, ref string errorMessage)
		{
			if (bone1 == null)
			{
				errorMessage = "Bone 1 of a BipedReferences limb is null.";
				return true;
			}
			if (bone2 == null)
			{
				errorMessage = "Bone 2 of a BipedReferences limb is null.";
				return true;
			}
			if (bone3 == null)
			{
				errorMessage = "Bone 3 of a BipedReferences limb is null.";
				return true;
			}
			UnityEngine.Object[] objects = new Transform[]
			{
				bone1,
				bone2,
				bone3
			};
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				errorMessage = transform.name + " is represented multiple times in the same BipedReferences limb.";
				return true;
			}
			if (bone2.position == bone1.position)
			{
				errorMessage = "Second bone's position equals first bone's position in the biped's limb.";
				return true;
			}
			if (bone3.position == bone2.position)
			{
				errorMessage = "Third bone's position equals second bone's position in the biped's limb.";
				return true;
			}
			if (!Hierarchy.HierarchyIsValid(new Transform[]
			{
				bone1,
				bone2,
				bone3
			}))
			{
				errorMessage = string.Concat(new string[]
				{
					"BipedReferences limb hierarchy is invalid. Bone transforms in a limb do not belong to the same ancestry. Please make sure the bones are parented to each other. Bones: ",
					bone1.name,
					", ",
					bone2.name,
					", ",
					bone3.name
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000385B4 File Offset: 0x000367B4
		private static bool LimbWarning(Transform bone1, Transform bone2, Transform bone3, ref string warningMessage)
		{
			if (Vector3.Cross(bone2.position - bone1.position, bone3.position - bone1.position) == Vector3.zero)
			{
				warningMessage = string.Concat(new string[]
				{
					"BipedReferences limb is completely stretched out in the initial pose. IK solver can not calculate the default bend plane for the limb. Please make sure you character's limbs are at least slightly bent in the initial pose. First bone: ",
					bone1.name,
					", second bone: ",
					bone2.name,
					"."
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00038630 File Offset: 0x00036830
		private static bool SpineError(BipedReferences references, ref string errorMessage)
		{
			if (references.spine.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < references.spine.Length; i++)
			{
				if (references.spine[i] == null)
				{
					errorMessage = "BipedReferences spine bone at index " + i.ToString() + " is null.";
					return true;
				}
			}
			UnityEngine.Object[] objects = references.spine;
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				errorMessage = transform.name + " is represented multiple times in BipedReferences spine.";
				return true;
			}
			if (!Hierarchy.HierarchyIsValid(references.spine))
			{
				errorMessage = "BipedReferences spine hierarchy is invalid. Bone transforms in the spine do not belong to the same ancestry. Please make sure the bones are parented to each other.";
				return true;
			}
			for (int j = 0; j < references.spine.Length; j++)
			{
				bool flag = false;
				if (j == 0 && references.spine[j].position == references.pelvis.position)
				{
					flag = true;
				}
				if (j != 0 && references.spine.Length > 1 && references.spine[j].position == references.spine[j - 1].position)
				{
					flag = true;
				}
				if (flag)
				{
					errorMessage = "Biped's spine bone nr " + j.ToString() + " position is the same as it's parent spine/pelvis bone's position. Please remove this bone from the spine.";
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003875C File Offset: 0x0003695C
		private static bool SpineWarning(BipedReferences references, ref string warningMessage)
		{
			return false;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00038760 File Offset: 0x00036960
		private static bool EyesError(BipedReferences references, ref string errorMessage)
		{
			if (references.eyes.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < references.eyes.Length; i++)
			{
				if (references.eyes[i] == null)
				{
					errorMessage = "BipedReferences eye bone at index " + i.ToString() + " is null.";
					return true;
				}
			}
			UnityEngine.Object[] objects = references.eyes;
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				errorMessage = transform.name + " is represented multiple times in BipedReferences eyes.";
				return true;
			}
			return false;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000387E6 File Offset: 0x000369E6
		private static bool EyesWarning(BipedReferences references, ref string warningMessage)
		{
			return false;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000387EC File Offset: 0x000369EC
		private static bool RootHeightWarning(BipedReferences references, ref string warningMessage)
		{
			if (references.head == null)
			{
				return false;
			}
			float verticalOffset = BipedReferences.GetVerticalOffset(references.head.position, references.leftFoot.position, references.root.rotation);
			if (BipedReferences.GetVerticalOffset(references.root.position, references.leftFoot.position, references.root.rotation) / verticalOffset > 0.2f)
			{
				warningMessage = "Biped's root Transform's position should be at ground level relative to the character (at the character's feet not at it's pelvis).";
				return true;
			}
			return false;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003886C File Offset: 0x00036A6C
		private static bool FacingAxisWarning(BipedReferences references, ref string warningMessage)
		{
			Vector3 vector = references.rightHand.position - references.leftHand.position;
			Vector3 vector2 = references.rightFoot.position - references.leftFoot.position;
			float num = Vector3.Dot(vector.normalized, references.root.right);
			float num2 = Vector3.Dot(vector2.normalized, references.root.right);
			if (num < 0f || num2 < 0f)
			{
				warningMessage = "Biped does not seem to be facing it's forward axis. Please make sure that in the initial pose the character is facing towards the positive Z axis of the Biped root gameobject.";
				return true;
			}
			return false;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000388F9 File Offset: 0x00036AF9
		private static float GetVerticalOffset(Vector3 p1, Vector3 p2, Quaternion rotation)
		{
			return (Quaternion.Inverse(rotation) * (p1 - p2)).y;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00038912 File Offset: 0x00036B12
		public BipedReferences()
		{
		}

		// Token: 0x04000694 RID: 1684
		public Transform root;

		// Token: 0x04000695 RID: 1685
		public Transform pelvis;

		// Token: 0x04000696 RID: 1686
		public Transform leftThigh;

		// Token: 0x04000697 RID: 1687
		public Transform leftCalf;

		// Token: 0x04000698 RID: 1688
		public Transform leftFoot;

		// Token: 0x04000699 RID: 1689
		public Transform rightThigh;

		// Token: 0x0400069A RID: 1690
		public Transform rightCalf;

		// Token: 0x0400069B RID: 1691
		public Transform rightFoot;

		// Token: 0x0400069C RID: 1692
		public Transform leftUpperArm;

		// Token: 0x0400069D RID: 1693
		public Transform leftForearm;

		// Token: 0x0400069E RID: 1694
		public Transform leftHand;

		// Token: 0x0400069F RID: 1695
		public Transform rightUpperArm;

		// Token: 0x040006A0 RID: 1696
		public Transform rightForearm;

		// Token: 0x040006A1 RID: 1697
		public Transform rightHand;

		// Token: 0x040006A2 RID: 1698
		public Transform head;

		// Token: 0x040006A3 RID: 1699
		public Transform[] spine = new Transform[0];

		// Token: 0x040006A4 RID: 1700
		public Transform[] eyes = new Transform[0];

		// Token: 0x020001E5 RID: 485
		public struct AutoDetectParams
		{
			// Token: 0x06001007 RID: 4103 RVA: 0x000648A7 File Offset: 0x00062AA7
			public AutoDetectParams(bool legsParentInSpine, bool includeEyes)
			{
				this.legsParentInSpine = legsParentInSpine;
				this.includeEyes = includeEyes;
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06001008 RID: 4104 RVA: 0x000648B7 File Offset: 0x00062AB7
			public static BipedReferences.AutoDetectParams Default
			{
				get
				{
					return new BipedReferences.AutoDetectParams(true, true);
				}
			}

			// Token: 0x04000E6A RID: 3690
			public bool legsParentInSpine;

			// Token: 0x04000E6B RID: 3691
			public bool includeEyes;
		}
	}
}
