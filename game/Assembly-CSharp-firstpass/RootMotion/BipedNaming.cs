using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B5 RID: 181
	public static class BipedNaming
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x000370F4 File Offset: 0x000352F4
		public static Transform[] GetBonesOfType(BipedNaming.BoneType boneType, Transform[] bones)
		{
			Transform[] array = new Transform[0];
			foreach (Transform transform in bones)
			{
				if (transform != null && BipedNaming.GetBoneType(transform.name) == boneType)
				{
					Array.Resize<Transform>(ref array, array.Length + 1);
					array[array.Length - 1] = transform;
				}
			}
			return array;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00037148 File Offset: 0x00035348
		public static Transform[] GetBonesOfSide(BipedNaming.BoneSide boneSide, Transform[] bones)
		{
			Transform[] array = new Transform[0];
			foreach (Transform transform in bones)
			{
				if (transform != null && BipedNaming.GetBoneSide(transform.name) == boneSide)
				{
					Array.Resize<Transform>(ref array, array.Length + 1);
					array[array.Length - 1] = transform;
				}
			}
			return array;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0003719C File Offset: 0x0003539C
		public static Transform[] GetBonesOfTypeAndSide(BipedNaming.BoneType boneType, BipedNaming.BoneSide boneSide, Transform[] bones)
		{
			Transform[] bonesOfType = BipedNaming.GetBonesOfType(boneType, bones);
			return BipedNaming.GetBonesOfSide(boneSide, bonesOfType);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000371B8 File Offset: 0x000353B8
		public static Transform GetFirstBoneOfTypeAndSide(BipedNaming.BoneType boneType, BipedNaming.BoneSide boneSide, Transform[] bones)
		{
			Transform[] bonesOfTypeAndSide = BipedNaming.GetBonesOfTypeAndSide(boneType, boneSide, bones);
			if (bonesOfTypeAndSide.Length == 0)
			{
				return null;
			}
			return bonesOfTypeAndSide[0];
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000371D8 File Offset: 0x000353D8
		public static Transform GetNamingMatch(Transform[] transforms, params string[][] namings)
		{
			foreach (Transform transform in transforms)
			{
				bool flag = true;
				foreach (string[] namingConvention in namings)
				{
					if (!BipedNaming.matchesNaming(transform.name, namingConvention))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00037231 File Offset: 0x00035431
		public static BipedNaming.BoneType GetBoneType(string boneName)
		{
			if (BipedNaming.isSpine(boneName))
			{
				return BipedNaming.BoneType.Spine;
			}
			if (BipedNaming.isHead(boneName))
			{
				return BipedNaming.BoneType.Head;
			}
			if (BipedNaming.isArm(boneName))
			{
				return BipedNaming.BoneType.Arm;
			}
			if (BipedNaming.isLeg(boneName))
			{
				return BipedNaming.BoneType.Leg;
			}
			if (BipedNaming.isTail(boneName))
			{
				return BipedNaming.BoneType.Tail;
			}
			if (BipedNaming.isEye(boneName))
			{
				return BipedNaming.BoneType.Eye;
			}
			return BipedNaming.BoneType.Unassigned;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00037270 File Offset: 0x00035470
		public static BipedNaming.BoneSide GetBoneSide(string boneName)
		{
			if (BipedNaming.isLeft(boneName))
			{
				return BipedNaming.BoneSide.Left;
			}
			if (BipedNaming.isRight(boneName))
			{
				return BipedNaming.BoneSide.Right;
			}
			return BipedNaming.BoneSide.Center;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00037287 File Offset: 0x00035487
		public static Transform GetBone(Transform[] transforms, BipedNaming.BoneType boneType, BipedNaming.BoneSide boneSide = BipedNaming.BoneSide.Center, params string[][] namings)
		{
			return BipedNaming.GetNamingMatch(BipedNaming.GetBonesOfTypeAndSide(boneType, boneSide, transforms), namings);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00037297 File Offset: 0x00035497
		private static bool isLeft(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeLeft) || BipedNaming.lastLetter(boneName) == "L" || BipedNaming.firstLetter(boneName) == "L";
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000372CA File Offset: 0x000354CA
		private static bool isRight(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeRight) || BipedNaming.lastLetter(boneName) == "R" || BipedNaming.firstLetter(boneName) == "R";
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000372FD File Offset: 0x000354FD
		private static bool isSpine(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeSpine) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeSpine);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0003731C File Offset: 0x0003551C
		private static bool isHead(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeHead) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeHead);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0003733B File Offset: 0x0003553B
		private static bool isArm(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeArm) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeArm);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0003735A File Offset: 0x0003555A
		private static bool isLeg(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeLeg) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeLeg);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00037379 File Offset: 0x00035579
		private static bool isTail(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeTail) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeTail);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00037398 File Offset: 0x00035598
		private static bool isEye(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeEye) && !BipedNaming.excludesNaming(boneName, BipedNaming.typeExcludeEye);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000373B7 File Offset: 0x000355B7
		private static bool isTypeExclude(string boneName)
		{
			return BipedNaming.matchesNaming(boneName, BipedNaming.typeExclude);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000373C4 File Offset: 0x000355C4
		private static bool matchesNaming(string boneName, string[] namingConvention)
		{
			if (BipedNaming.excludesNaming(boneName, BipedNaming.typeExclude))
			{
				return false;
			}
			foreach (string value in namingConvention)
			{
				if (boneName.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00037400 File Offset: 0x00035600
		private static bool excludesNaming(string boneName, string[] namingConvention)
		{
			foreach (string value in namingConvention)
			{
				if (boneName.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00037430 File Offset: 0x00035630
		private static bool matchesLastLetter(string boneName, string[] namingConvention)
		{
			foreach (string letter in namingConvention)
			{
				if (BipedNaming.LastLetterIs(boneName, letter))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003745D File Offset: 0x0003565D
		private static bool LastLetterIs(string boneName, string letter)
		{
			return boneName.Substring(boneName.Length - 1, 1) == letter;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00037474 File Offset: 0x00035674
		private static string firstLetter(string boneName)
		{
			if (boneName.Length > 0)
			{
				return boneName.Substring(0, 1);
			}
			return "";
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0003748D File Offset: 0x0003568D
		private static string lastLetter(string boneName)
		{
			if (boneName.Length > 0)
			{
				return boneName.Substring(boneName.Length - 1, 1);
			}
			return "";
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000374B0 File Offset: 0x000356B0
		// Note: this type is marked as 'beforefieldinit'.
		static BipedNaming()
		{
		}

		// Token: 0x04000682 RID: 1666
		public static string[] typeLeft = new string[]
		{
			" L ",
			"_L_",
			"-L-",
			" l ",
			"_l_",
			"-l-",
			"Left",
			"left",
			"CATRigL"
		};

		// Token: 0x04000683 RID: 1667
		public static string[] typeRight = new string[]
		{
			" R ",
			"_R_",
			"-R-",
			" r ",
			"_r_",
			"-r-",
			"Right",
			"right",
			"CATRigR"
		};

		// Token: 0x04000684 RID: 1668
		public static string[] typeSpine = new string[]
		{
			"Spine",
			"spine",
			"Pelvis",
			"pelvis",
			"Root",
			"root",
			"Torso",
			"torso",
			"Body",
			"body",
			"Hips",
			"hips",
			"Neck",
			"neck",
			"Chest",
			"chest"
		};

		// Token: 0x04000685 RID: 1669
		public static string[] typeHead = new string[]
		{
			"Head",
			"head"
		};

		// Token: 0x04000686 RID: 1670
		public static string[] typeArm = new string[]
		{
			"Arm",
			"arm",
			"Hand",
			"hand",
			"Wrist",
			"Wrist",
			"Elbow",
			"elbow",
			"Palm",
			"palm"
		};

		// Token: 0x04000687 RID: 1671
		public static string[] typeLeg = new string[]
		{
			"Leg",
			"leg",
			"Thigh",
			"thigh",
			"Calf",
			"calf",
			"Femur",
			"femur",
			"Knee",
			"knee",
			"Foot",
			"foot",
			"Ankle",
			"ankle",
			"Hip",
			"hip"
		};

		// Token: 0x04000688 RID: 1672
		public static string[] typeTail = new string[]
		{
			"Tail",
			"tail"
		};

		// Token: 0x04000689 RID: 1673
		public static string[] typeEye = new string[]
		{
			"Eye",
			"eye"
		};

		// Token: 0x0400068A RID: 1674
		public static string[] typeExclude = new string[]
		{
			"Nub",
			"Dummy",
			"dummy",
			"Tip",
			"IK",
			"Mesh"
		};

		// Token: 0x0400068B RID: 1675
		public static string[] typeExcludeSpine = new string[]
		{
			"Head",
			"head"
		};

		// Token: 0x0400068C RID: 1676
		public static string[] typeExcludeHead = new string[]
		{
			"Top",
			"End"
		};

		// Token: 0x0400068D RID: 1677
		public static string[] typeExcludeArm = new string[]
		{
			"Collar",
			"collar",
			"Clavicle",
			"clavicle",
			"Finger",
			"finger",
			"Index",
			"index",
			"Mid",
			"mid",
			"Pinky",
			"pinky",
			"Ring",
			"Thumb",
			"thumb",
			"Adjust",
			"adjust",
			"Twist",
			"twist"
		};

		// Token: 0x0400068E RID: 1678
		public static string[] typeExcludeLeg = new string[]
		{
			"Toe",
			"toe",
			"Platform",
			"Adjust",
			"adjust",
			"Twist",
			"twist"
		};

		// Token: 0x0400068F RID: 1679
		public static string[] typeExcludeTail = new string[0];

		// Token: 0x04000690 RID: 1680
		public static string[] typeExcludeEye = new string[]
		{
			"Lid",
			"lid",
			"Brow",
			"brow",
			"Lash",
			"lash"
		};

		// Token: 0x04000691 RID: 1681
		public static string[] pelvis = new string[]
		{
			"Pelvis",
			"pelvis",
			"Hip",
			"hip"
		};

		// Token: 0x04000692 RID: 1682
		public static string[] hand = new string[]
		{
			"Hand",
			"hand",
			"Wrist",
			"wrist",
			"Palm",
			"palm"
		};

		// Token: 0x04000693 RID: 1683
		public static string[] foot = new string[]
		{
			"Foot",
			"foot",
			"Ankle",
			"ankle"
		};

		// Token: 0x020001E3 RID: 483
		[Serializable]
		public enum BoneType
		{
			// Token: 0x04000E5F RID: 3679
			Unassigned,
			// Token: 0x04000E60 RID: 3680
			Spine,
			// Token: 0x04000E61 RID: 3681
			Head,
			// Token: 0x04000E62 RID: 3682
			Arm,
			// Token: 0x04000E63 RID: 3683
			Leg,
			// Token: 0x04000E64 RID: 3684
			Tail,
			// Token: 0x04000E65 RID: 3685
			Eye
		}

		// Token: 0x020001E4 RID: 484
		[Serializable]
		public enum BoneSide
		{
			// Token: 0x04000E67 RID: 3687
			Center,
			// Token: 0x04000E68 RID: 3688
			Left,
			// Token: 0x04000E69 RID: 3689
			Right
		}
	}
}
